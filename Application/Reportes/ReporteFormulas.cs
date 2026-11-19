using System.Globalization;
using System.Text;

namespace KonSolutionsApi.Application.Reportes;

// Fórmulas del diseño libre: un lenguaje corto, en español, parecido al de Excel (bosquejo C3).
//
//   Referencias, entre llaves:
//     {R1.COLUMNA}               campo de un resultado (también {CAB.COLUMNA}, {DET.COLUMNA}, {ADI.COLUMNA}); del
//                                resultado 1, la fila que se imprime; de otro, su primera fila. {COLUMNA} solo = R1.
//     {@PARAMETRO}               valor de un parámetro del reporte
//     {F.Nombre}                 otra fórmula del diseño
//     {Σ.Suma.COLUMNA.Grupo1}    un resumen del resultado 1: Suma, Cuenta, CuentaDistinta, Promedio, Maximo, Minimo;
//                                de Grupo1, Grupo2... o del Informe. Sin el último tramo, el de la sección donde está.
//                                ({S.Suma...} es lo mismo, para no tener que escribir la Σ.)
//     {#Pagina} {#TotalPaginas} {#Usuario} {#Fecha} {#FechaHora} {#Filtros} {#Titulo} {#Subtitulo}
//     {#Empresa} {#EmpresaRuc} {#EmpresaDireccion} {#Aplicacion}
//   Valores: números con punto decimal, "textos entre comillas" ("" dentro es una comilla), Verdadero, Falso, Nulo y
//   colores con nombre (Rojo, Verde, Azul...) o "#C62828".
//   Operadores: + - * / % (resto) & (une textos) = <> < > <= >= y paréntesis. ; separa los datos de una función.
//   // empieza un comentario hasta el final de la línea.
//
// Seguridad: es un intérprete propio. Solo lee la fila, los parámetros, los resúmenes y los campos especiales; no
// puede consultar la base, llamar SP ni ejecutar código. Un dato nulo, una división entre cero o una conversión que no
// se puede hacer dan vacío (null) y el reporte sigue.

public enum FormulaTipo { Variante, Numero, Texto, Fecha, Logico }

/// <summary>Un error de una fórmula, con dónde está (línea y columna desde 1).</summary>
public sealed record FormulaError(int Linea, int Columna, string Mensaje);

public sealed class FormulaException : Exception
{
    public int Linea { get; }
    public int Columna { get; }
    public FormulaException(string mensaje, int linea, int columna) : base(mensaje) { Linea = linea; Columna = columna; }
}

/// <summary>Una función del lenguaje, para la ayuda del editor.</summary>
public sealed record FormulaFuncionInfo(string Grupo, string Nombre, string Firma, string Ayuda, string Ejemplo);

/// <summary>Lo que una fórmula puede nombrar, para comprobarla antes de grabar: columnas de cada resultado con su tipo,
/// parámetros, las demás fórmulas del diseño y cuántos niveles de grupo hay.</summary>
public sealed class FormulaEsquema
{
    public Dictionary<int, Dictionary<string, FormulaTipo>> Columnas { get; } = new();
    public Dictionary<string, FormulaTipo> Parametros { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> Formulas { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, int> Papeles { get; } = new(StringComparer.OrdinalIgnoreCase);
    public int Grupos { get; set; }

    public void Columna(int resultado, string nombre, string? tipoSql)
    {
        if (!Columnas.TryGetValue(resultado, out var d)) Columnas[resultado] = d = new(StringComparer.OrdinalIgnoreCase);
        d[nombre] = ReporteFormula.TipoSql(tipoSql);
    }
}

/// <summary>De dónde saca sus datos una fórmula al evaluarse.</summary>
public interface IFormulaContexto
{
    object? Campo(int resultado, string columna);
    int Resultado(string papel);
    object? Parametro(string nombre);
    object? Formula(string nombre);
    /// <summary>funcion: SUM, CNT, CND, AVG, MAX, MIN. nivel: 0 el informe, 1.. un grupo, null el de la sección.</summary>
    object? Resumen(string funcion, string columna, int? nivel);
    /// <summary>PAGINA, TOTAL_PAGINAS, USUARIO, FECHA, FECHA_HORA, FILTROS, TITULO, SUBTITULO, EMPRESA_NOMBRE, APLICACION_NOMBRE...</summary>
    object? Especial(string codigo);
    DateTime Ahora { get; }
}

/// <summary>Una fórmula ya leída: se compila una vez y se evalúa por cada fila.</summary>
public sealed class ReporteFormula
{
    private readonly Nodo _raiz;
    public string Texto { get; }

    private ReporteFormula(string texto, Nodo raiz) { Texto = texto; _raiz = raiz; }

    /// <summary>Lee la fórmula. Un error de escritura lanza FormulaException con la línea y la columna.</summary>
    public static ReporteFormula Compilar(string? texto)
    {
        var t = texto ?? "";
        var tokens = new Lexico(t).Leer();
        var raiz = new Sintaxis(tokens).Leer();
        return new ReporteFormula(t, raiz);
    }

    public static bool Probar(string? texto, out ReporteFormula? formula, out FormulaError? error)
    {
        try { formula = Compilar(texto); error = null; return true; }
        catch (FormulaException ex) { formula = null; error = new FormulaError(ex.Linea, ex.Columna, ex.Message); return false; }
    }

    /// <summary>Comprueba que todo lo que nombra exista y que los datos de cada función alcancen. Devuelve los errores
    /// (vacío si está bien) y el tipo del resultado. nombrePropio: el de esta fórmula, para que no se use a sí misma.
    /// conPagina: falso en las condiciones de una sección, que se deciden antes de saber en qué página caen.</summary>
    public List<FormulaError> Comprobar(FormulaEsquema esquema, out FormulaTipo tipo, string? nombrePropio = null, bool conPagina = true)
    {
        var c = new Comprobador(esquema, nombrePropio, conPagina);
        tipo = c.Tipo(_raiz);
        return c.Errores.Distinct().ToList();
    }

    /// <summary>El valor: decimal, string, DateTime, bool o null. Nunca lanza: lo que no se puede calcular da null.</summary>
    public object? Evaluar(IFormulaContexto ctx)
    {
        try { return new Evaluador(ctx).Valor(_raiz); }
        catch (Exception ex) when (ex is FormulaException or InvalidCastException or FormatException or OverflowException
                                      or ArgumentException or DivideByZeroException or InvalidOperationException)
        {
            return null;
        }
    }

    /// <summary>¿Depende del número de página (directo o por otra fórmula)? Entonces se escribe al dibujar.</summary>
    public bool UsaPagina(Func<string, ReporteFormula?> formulas) => UsaPagina(_raiz, formulas, new HashSet<string>(StringComparer.OrdinalIgnoreCase));

    private static bool UsaPagina(Nodo n, Func<string, ReporteFormula?> formulas, HashSet<string> vistas) => n switch
    {
        NRef { Tipo: RefTipo.Especial } r => r.Nombre is "PAGINA" or "TOTAL_PAGINAS",
        NRef { Tipo: RefTipo.Formula } r => vistas.Add(r.Nombre) && formulas(r.Nombre) is { } f && UsaPagina(f._raiz, formulas, vistas),
        NLlamada l => l.Args.Any(a => UsaPagina(a, formulas, vistas)),
        NUnario u => UsaPagina(u.X, formulas, vistas),
        NBinario b => UsaPagina(b.A, formulas, vistas) || UsaPagina(b.B, formulas, vistas),
        _ => false
    };

    /// <summary>Las fórmulas que nombra (para avisar al borrar una que se usa).</summary>
    public IEnumerable<string> FormulasUsadas() => Refs(_raiz).Where(r => r.Tipo == RefTipo.Formula).Select(r => r.Nombre).Distinct(StringComparer.OrdinalIgnoreCase);

    private static IEnumerable<NRef> Refs(Nodo n) => n switch
    {
        NRef r => new[] { r },
        NLlamada l => l.Args.SelectMany(Refs),
        NUnario u => Refs(u.X),
        NBinario b => Refs(b.A).Concat(Refs(b.B)),
        _ => Enumerable.Empty<NRef>()
    };

    // ── Ayudas públicas ─────────────────────────────────────────────────────────
    public static FormulaTipo TipoSql(string? sql) => (sql ?? "").Trim().ToLowerInvariant() switch
    {
        "int" or "bigint" or "smallint" or "tinyint" or "decimal" or "numeric" or "money" or "smallmoney" or "float" or "real" => FormulaTipo.Numero,
        "date" or "datetime" or "datetime2" or "smalldatetime" or "datetimeoffset" => FormulaTipo.Fecha,
        "bit" => FormulaTipo.Logico,
        "char" or "nchar" or "varchar" or "nvarchar" or "text" or "ntext" or "uniqueidentifier" or "xml" => FormulaTipo.Texto,
        _ => FormulaTipo.Variante
    };

    public static string NombreTipo(FormulaTipo t) => t switch
    {
        FormulaTipo.Numero => "Número", FormulaTipo.Texto => "Texto", FormulaTipo.Fecha => "Fecha",
        FormulaTipo.Logico => "Verdadero o Falso", _ => "Variable"
    };

    /// <summary>¿Cuenta como verdadero? Verdadero, un número distinto de 0, un texto no vacío o una fecha.</summary>
    public static bool Verdad(object? v) => v switch
    {
        null => false,
        bool b => b,
        decimal d => d != 0,
        string s => s.Length > 0 && !string.Equals(s, "Falso", StringComparison.OrdinalIgnoreCase) && s != "0",
        _ => true
    };

    /// <summary>Un color que devolvió una fórmula: "#RRGGBB", un nombre (Rojo...) o null si no es un color.</summary>
    public static string? AColor(object? v)
    {
        if (v is not string s) return null;
        s = s.Trim();
        if (s.Length == 7 && s[0] == '#' && int.TryParse(s.AsSpan(1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out _)) return s.ToUpperInvariant();
        return Colores.TryGetValue(Normal(s), out var c) ? c : null;
    }

    /// <summary>El valor como texto para mostrarlo en el reporte. Sin formato: los números con separador de miles (y
    /// dos decimales si los tiene), las fechas dd/MM/yyyy, Verdadero/Falso como Sí/No.</summary>
    public static string Formatear(object? v, string? formato)
    {
        switch (v)
        {
            case null: return "";
            case decimal d:
                return ReporteFormato.Texto(d, string.IsNullOrWhiteSpace(formato) ? (d == decimal.Truncate(d) ? "#,##0" : "#,##0.00") : formato, typeof(decimal));
            case DateTime f:
                return ReporteFormato.Texto(f, string.IsNullOrWhiteSpace(formato) ? null : formato, typeof(DateTime));
            case bool b: return b ? "Sí" : "No";
            default: return Convert.ToString(v, CultureInfo.InvariantCulture) ?? "";
        }
    }

    /// <summary>Un valor de la base llevado a los tipos de la fórmula: decimal, string, DateTime, bool o null.</summary>
    public static object? Normalizar(object? v)
    {
        switch (v)
        {
            case null or DBNull: return null;
            case string s: return s.TrimEnd();
            case bool or DateTime or decimal: return v;
            case DateTimeOffset o: return o.DateTime;
            case DateOnly d: return d.ToDateTime(TimeOnly.MinValue);
            case char ch: return ch.ToString();
            case Guid g: return g.ToString();
            case byte[]: return null;
            case TimeSpan t: return t.ToString(@"hh\:mm", CultureInfo.InvariantCulture);
        }
        return ReporteFormato.ADecimal(v, out var n) ? n : Convert.ToString(v, CultureInfo.InvariantCulture);
    }

    /// <summary>Sin tildes y en minúsculas, para comparar nombres de funciones, colores y especiales.</summary>
    public static string Normal(string s)
    {
        var d = s.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder(d.Length);
        foreach (var ch in d)
            if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark) sb.Append(char.ToLowerInvariant(ch));
        return sb.ToString().Replace("_", "");
    }

    /// <summary>¿Sirve como nombre de fórmula? Letras sin tilde, números y _; empieza con letra; hasta 50.</summary>
    public static bool NombreValido(string? n)
        => !string.IsNullOrEmpty(n) && n.Length <= 50 && n[0] is >= 'A' and <= 'Z' or >= 'a' and <= 'z'
           && n.All(ch => ch is >= 'A' and <= 'Z' or >= 'a' and <= 'z' or >= '0' and <= '9' or '_');

    public static readonly Dictionary<string, string> Colores = new()
    {
        ["rojo"] = "#C62828", ["verde"] = "#2E7D32", ["azul"] = "#1565C0", ["azuloscuro"] = "#163A5F", ["celeste"] = "#0288D1",
        ["negro"] = "#000000", ["gris"] = "#757575", ["grisclaro"] = "#BDBDBD", ["blanco"] = "#FFFFFF", ["naranja"] = "#EF6C00",
        ["amarillo"] = "#F9A825", ["morado"] = "#6A1B9A", ["rosado"] = "#AD1457", ["marron"] = "#5D4037"
    };

    /// <summary>Los campos especiales: nombre que se escribe → código.</summary>
    public static readonly Dictionary<string, (string Codigo, FormulaTipo Tipo)> Especiales = new()
    {
        ["pagina"] = ("PAGINA", FormulaTipo.Numero), ["totalpaginas"] = ("TOTAL_PAGINAS", FormulaTipo.Numero),
        ["usuario"] = ("USUARIO", FormulaTipo.Texto), ["fecha"] = ("FECHA", FormulaTipo.Fecha), ["fechahora"] = ("FECHA_HORA", FormulaTipo.Fecha),
        ["filtros"] = ("FILTROS", FormulaTipo.Texto), ["titulo"] = ("TITULO", FormulaTipo.Texto), ["subtitulo"] = ("SUBTITULO", FormulaTipo.Texto),
        ["empresa"] = ("EMPRESA_NOMBRE", FormulaTipo.Texto), ["empresanombre"] = ("EMPRESA_NOMBRE", FormulaTipo.Texto),
        ["empresaruc"] = ("EMPRESA_RUC", FormulaTipo.Texto), ["empresadireccion"] = ("EMPRESA_DIRECCION", FormulaTipo.Texto),
        ["aplicacion"] = ("APLICACION_NOMBRE", FormulaTipo.Texto), ["aplicacionnombre"] = ("APLICACION_NOMBRE", FormulaTipo.Texto)
    };

    private static readonly Dictionary<string, string> FuncionesResumen = new()
    {
        ["suma"] = "SUM", ["cuenta"] = "CNT", ["cuentadistinta"] = "CND", ["promedio"] = "AVG", ["maximo"] = "MAX", ["minimo"] = "MIN"
    };

    // ── Nodos ───────────────────────────────────────────────────────────────────
    private abstract class Nodo
    {
        public int Linea { get; init; }
        public int Columna { get; init; }
    }
    private sealed class NValor : Nodo { public object? Valor { get; init; } public FormulaTipo Tipo { get; init; } }
    private enum RefTipo { Campo, Parametro, Formula, Resumen, Especial }
    private sealed class NRef : Nodo
    {
        public RefTipo Tipo { get; init; }
        public int Resultado { get; init; } = 1;
        public string? Papel { get; init; }
        public string Nombre { get; init; } = "";
        public string Funcion { get; init; } = "";
        public int? Nivel { get; init; }
        public string Escrito { get; init; } = "";
    }
    private sealed class NLlamada : Nodo { public string Nombre { get; init; } = ""; public string Escrito { get; init; } = ""; public List<Nodo> Args { get; init; } = new(); }
    private sealed class NUnario : Nodo { public char Op { get; init; } public Nodo X { get; init; } = null!; }
    private sealed class NBinario : Nodo { public string Op { get; init; } = ""; public Nodo A { get; init; } = null!; public Nodo B { get; init; } = null!; }

    // ── Léxico ──────────────────────────────────────────────────────────────────
    private enum T { Numero, Texto, Ref, Nombre, Op, Abre, Cierra, PuntoComa, Fin }
    private sealed record Tok(T Tipo, string Texto, int Linea, int Columna);

    private sealed class Lexico
    {
        private readonly string _s;
        private int _i, _linea = 1, _inicioLinea;
        public Lexico(string s) => _s = s;
        private int Col => _i - _inicioLinea + 1;
        private FormulaException Error(string m, int linea, int col) => new(m, linea, col);

        public List<Tok> Leer()
        {
            var lista = new List<Tok>();
            while (true)
            {
                Saltar();
                if (_i >= _s.Length) { lista.Add(new Tok(T.Fin, "", _linea, Col)); return lista; }
                int l = _linea, c = Col;
                var ch = _s[_i];
                if (char.IsDigit(ch))
                {
                    var ini = _i;
                    while (_i < _s.Length && char.IsDigit(_s[_i])) _i++;
                    if (_i < _s.Length && _s[_i] == '.')
                    {
                        _i++;
                        if (_i >= _s.Length || !char.IsDigit(_s[_i])) throw Error("Número mal escrito: después del punto decimal van dígitos.", l, c);
                        while (_i < _s.Length && char.IsDigit(_s[_i])) _i++;
                    }
                    if (_i < _s.Length && _s[_i] == ',' && _i + 1 < _s.Length && char.IsDigit(_s[_i + 1]))
                        throw Error("El decimal se escribe con punto (2.5), no con coma.", _linea, Col);
                    lista.Add(new Tok(T.Numero, _s[ini.._i], l, c));
                }
                else if (ch == '"')
                {
                    _i++;
                    var sb = new StringBuilder();
                    while (true)
                    {
                        if (_i >= _s.Length) throw Error("Falta cerrar las comillas del texto.", l, c);
                        if (_s[_i] == '"')
                        {
                            if (_i + 1 < _s.Length && _s[_i + 1] == '"') { sb.Append('"'); _i += 2; continue; }
                            _i++;
                            break;
                        }
                        if (_s[_i] == '\n') { _linea++; _inicioLinea = _i + 1; }
                        sb.Append(_s[_i++]);
                    }
                    lista.Add(new Tok(T.Texto, sb.ToString(), l, c));
                }
                else if (ch == '{')
                {
                    var fin = _s.IndexOfAny(new[] { '}', '{', '\n' }, _i + 1);
                    if (fin < 0 || _s[fin] != '}') throw Error("Falta cerrar la llave de la referencia.", l, c);
                    var dentro = _s[(_i + 1)..fin].Trim();
                    if (dentro.Length == 0) throw Error("Referencia vacía: entre las llaves va un campo, un parámetro, una fórmula...", l, c);
                    lista.Add(new Tok(T.Ref, dentro, l, c));
                    _i = fin + 1;
                }
                else if (char.IsLetter(ch) || ch == '_')
                {
                    var ini = _i;
                    while (_i < _s.Length && (char.IsLetterOrDigit(_s[_i]) || _s[_i] == '_')) _i++;
                    lista.Add(new Tok(T.Nombre, _s[ini.._i], l, c));
                }
                else
                {
                    _i++;
                    switch (ch)
                    {
                        case '(': lista.Add(new Tok(T.Abre, "(", l, c)); break;
                        case ')': lista.Add(new Tok(T.Cierra, ")", l, c)); break;
                        case ';': lista.Add(new Tok(T.PuntoComa, ";", l, c)); break;
                        case '+' or '*' or '/' or '%' or '&' or '=': lista.Add(new Tok(T.Op, ch.ToString(), l, c)); break;
                        case '-' or '−' or '–': lista.Add(new Tok(T.Op, "-", l, c)); break;
                        case '<':
                            if (_i < _s.Length && _s[_i] is '=' or '>') { lista.Add(new Tok(T.Op, "<" + _s[_i], l, c)); _i++; }
                            else lista.Add(new Tok(T.Op, "<", l, c));
                            break;
                        case '>':
                            if (_i < _s.Length && _s[_i] == '=') { lista.Add(new Tok(T.Op, ">=", l, c)); _i++; }
                            else lista.Add(new Tok(T.Op, ">", l, c));
                            break;
                        case ',': throw Error("Los datos de una función se separan con ; (punto y coma).", l, c);
                        case '}': throw Error("Sobra una llave de cierre.", l, c);
                        case '\'': throw Error("Los textos van entre comillas dobles: \"así\".", l, c);
                        default: throw Error($"No se entiende el carácter «{ch}».", l, c);
                    }
                }
            }
        }

        private void Saltar()
        {
            while (_i < _s.Length)
            {
                var ch = _s[_i];
                if (ch == '\n') { _i++; _linea++; _inicioLinea = _i; continue; }
                if (char.IsWhiteSpace(ch)) { _i++; continue; }
                if (ch == '/' && _i + 1 < _s.Length && _s[_i + 1] == '/')
                {
                    while (_i < _s.Length && _s[_i] != '\n') _i++;
                    continue;
                }
                break;
            }
        }
    }

    // ── Sintaxis ────────────────────────────────────────────────────────────────
    private sealed class Sintaxis
    {
        private readonly List<Tok> _t;
        private int _p;
        public Sintaxis(List<Tok> t) => _t = t;
        private Tok Ver => _t[_p];
        private Tok Tomar() => _t[_p++];
        private static FormulaException Error(string m, Tok t) => new(m, t.Linea, t.Columna);
        private static readonly HashSet<string> Comparaciones = new() { "=", "<>", "<", ">", "<=", ">=" };

        public Nodo Leer()
        {
            if (Ver.Tipo == T.Fin) throw Error("La fórmula está vacía.", Ver);
            var n = Comparacion();
            if (Ver.Tipo != T.Fin)
                throw Error(Ver.Tipo == T.Cierra ? "Sobra un paréntesis de cierre." : $"Sobra «{Ver.Texto}»: ¿falta un operador o un ; antes?", Ver);
            return n;
        }

        private Nodo Comparacion()
        {
            var a = Concatenacion();
            if (Ver.Tipo == T.Op && Comparaciones.Contains(Ver.Texto))
            {
                var op = Tomar();
                var b = Concatenacion();
                if (Ver.Tipo == T.Op && Comparaciones.Contains(Ver.Texto))
                    throw Error("Para unir dos comparaciones usa Y(…) u O(…).", Ver);
                return new NBinario { Op = op.Texto, A = a, B = b, Linea = op.Linea, Columna = op.Columna };
            }
            return a;
        }

        private Nodo Concatenacion()
        {
            var a = Suma();
            while (Ver.Tipo == T.Op && Ver.Texto == "&")
            {
                var op = Tomar();
                a = new NBinario { Op = "&", A = a, B = Suma(), Linea = op.Linea, Columna = op.Columna };
            }
            return a;
        }

        private Nodo Suma()
        {
            var a = Producto();
            while (Ver.Tipo == T.Op && Ver.Texto is "+" or "-")
            {
                var op = Tomar();
                a = new NBinario { Op = op.Texto, A = a, B = Producto(), Linea = op.Linea, Columna = op.Columna };
            }
            return a;
        }

        private Nodo Producto()
        {
            var a = Unario();
            while (Ver.Tipo == T.Op && Ver.Texto is "*" or "/" or "%")
            {
                var op = Tomar();
                a = new NBinario { Op = op.Texto, A = a, B = Unario(), Linea = op.Linea, Columna = op.Columna };
            }
            return a;
        }

        private Nodo Unario()
        {
            if (Ver.Tipo == T.Op && Ver.Texto is "-" or "+")
            {
                var op = Tomar();
                return new NUnario { Op = op.Texto[0], X = Unario(), Linea = op.Linea, Columna = op.Columna };
            }
            return Primario();
        }

        private Nodo Primario()
        {
            var t = Ver;
            switch (t.Tipo)
            {
                case T.Numero:
                    Tomar();
                    if (!decimal.TryParse(t.Texto, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var n))
                        throw Error("El número es demasiado grande.", t);
                    return new NValor { Valor = n, Tipo = FormulaTipo.Numero, Linea = t.Linea, Columna = t.Columna };
                case T.Texto:
                    Tomar();
                    return new NValor { Valor = t.Texto, Tipo = FormulaTipo.Texto, Linea = t.Linea, Columna = t.Columna };
                case T.Ref:
                    Tomar();
                    return Referencia(t);
                case T.Abre:
                {
                    Tomar();
                    if (Ver.Tipo == T.Cierra) throw Error("Paréntesis vacíos: falta un valor.", Ver);
                    var x = Comparacion();
                    if (Ver.Tipo != T.Cierra) throw Error("Falta cerrar el paréntesis.", Ver.Tipo == T.Fin ? t : Ver);
                    Tomar();
                    return x;
                }
                case T.Nombre:
                {
                    Tomar();
                    if (Ver.Tipo == T.Abre)
                    {
                        var abre = Tomar();
                        var args = new List<Nodo>();
                        if (Ver.Tipo != T.Cierra)
                            while (true)
                            {
                                if (Ver.Tipo is T.PuntoComa or T.Cierra) throw Error("Falta un dato de la función.", Ver);
                                args.Add(Comparacion());
                                if (Ver.Tipo == T.PuntoComa) { Tomar(); continue; }
                                if (Ver.Tipo == T.Cierra) break;
                                throw Error(Ver.Tipo == T.Fin ? $"Falta cerrar el paréntesis de {t.Texto}." : $"Sobra «{Ver.Texto}»: los datos de {t.Texto} se separan con ;", Ver.Tipo == T.Fin ? abre : Ver);
                            }
                        Tomar();
                        return new NLlamada { Nombre = Normal(t.Texto), Escrito = t.Texto, Args = args, Linea = t.Linea, Columna = t.Columna };
                    }
                    var nombre = Normal(t.Texto);
                    if (nombre == "verdadero") return new NValor { Valor = true, Tipo = FormulaTipo.Logico, Linea = t.Linea, Columna = t.Columna };
                    if (nombre == "falso") return new NValor { Valor = false, Tipo = FormulaTipo.Logico, Linea = t.Linea, Columna = t.Columna };
                    if (nombre == "nulo") return new NValor { Valor = null, Tipo = FormulaTipo.Variante, Linea = t.Linea, Columna = t.Columna };
                    if (Colores.TryGetValue(nombre, out var color)) return new NValor { Valor = color, Tipo = FormulaTipo.Texto, Linea = t.Linea, Columna = t.Columna };
                    if (Funciones.ContainsKey(nombre)) throw Error($"A {t.Texto} le faltan los paréntesis: {t.Texto}(…).", t);
                    throw Error($"No se reconoce «{t.Texto}». Un campo va entre llaves ({{R1.{t.Texto.ToUpperInvariant()}}}) y un texto entre comillas.", t);
                }
                case T.Fin: throw Error("La fórmula termina antes de tiempo: falta un valor.", t);
                case T.Cierra: throw Error("Sobra un paréntesis de cierre, o falta un valor antes.", t);
                case T.PuntoComa: throw Error("Sobra el ; : solo separa los datos de una función.", t);
                default: throw Error($"Falta un valor antes de «{t.Texto}».", t);
            }
        }

        /// <summary>Lo que va entre llaves: {R1.COL}, {CAB.COL}, {COL}, {@PARAM}, {F.Nombre}, {Σ.Suma.COL.Grupo1}, {#Pagina}.</summary>
        private static NRef Referencia(Tok t)
        {
            var s = t.Texto;
            NRef R(RefTipo tipo, string nombre, int resultado = 1, string? papel = null, string funcion = "", int? nivel = null)
                => new() { Tipo = tipo, Nombre = nombre, Resultado = resultado, Papel = papel, Funcion = funcion, Nivel = nivel, Escrito = "{" + s + "}", Linea = t.Linea, Columna = t.Columna };

            if (s[0] == '@') return R(RefTipo.Parametro, s[1..].Trim());
            if (s[0] == '#')
            {
                if (Especiales.TryGetValue(Normal(s[1..]), out var e)) return R(RefTipo.Especial, e.Codigo);
                throw Error($"{{{s}}} no es un campo especial. Hay: #Pagina, #TotalPaginas, #Usuario, #Fecha, #FechaHora, #Filtros, #Titulo, #Subtitulo, #Empresa, #EmpresaRuc, #EmpresaDireccion, #Aplicacion.", t);
            }
            var p = s.Split('.', StringSplitOptions.TrimEntries);
            var cabeza = p[0];
            if (p.Length == 2 && string.Equals(cabeza, "F", StringComparison.OrdinalIgnoreCase)) return R(RefTipo.Formula, p[1]);
            if (cabeza is "Σ" or "S" or "s" && p.Length is 3 or 4)
            {
                if (!FuncionesResumen.TryGetValue(Normal(p[1]), out var fn))
                    throw Error($"«{p[1]}» no es un resumen. Hay: Suma, Cuenta, CuentaDistinta, Promedio, Maximo, Minimo.", t);
                int? nivel = null;
                if (p.Length == 4)
                {
                    var alcance = Normal(p[3]);
                    if (alcance is "informe" or "total") nivel = 0;
                    else if (alcance.StartsWith("grupo") && int.TryParse(alcance[5..], out var g) && g > 0) nivel = g;
                    else throw Error($"«{p[3]}»: un resumen es de Grupo1, Grupo2... o del Informe.", t);
                }
                return R(RefTipo.Resumen, p[2], funcion: fn, nivel: nivel);
            }
            if (p.Length == 2)
            {
                var c = cabeza.ToUpperInvariant();
                if (c.Length > 1 && c[0] == 'R' && int.TryParse(c[1..], out var r) && r > 0) return R(RefTipo.Campo, p[1], resultado: r);
                if (c is "CAB" or "DET" or "ADI") return R(RefTipo.Campo, p[1], papel: c);
            }
            if (p.Length == 1 && p[0].All(ch => char.IsLetterOrDigit(ch) || ch == '_')) return R(RefTipo.Campo, p[0]);
            throw Error($"No se entiende {{{s}}}. Se escribe {{R1.COLUMNA}}, {{@PARAMETRO}}, {{F.Nombre}}, {{Σ.Suma.COLUMNA.Grupo1}} o {{#Pagina}}.", t);
        }
    }

    // ── Funciones ───────────────────────────────────────────────────────────────
    private delegate object? Impl(Evaluador e, List<Nodo> a);
    private sealed record Funcion(string Grupo, string Nombre, int Min, int Max, FormulaTipo Tipo, Impl Hacer, string Firma, string Ayuda, string Ejemplo);

    private static readonly Dictionary<string, Funcion> Funciones = Armar();

    /// <summary>Las funciones para la ayuda del editor, por grupo.</summary>
    public static IReadOnlyList<FormulaFuncionInfo> Catalogo { get; } =
        Funciones.Values.Select(f => new FormulaFuncionInfo(f.Grupo, f.Nombre, f.Firma, f.Ayuda, f.Ejemplo)).ToList();

    private static Dictionary<string, Funcion> Armar()
    {
        var l = new List<Funcion>
        {
            // Lógicas
            new("Lógicas", "SI", 2, 3, FormulaTipo.Variante, (e, a) => Verdad(e.Valor(a[0])) ? e.Valor(a[1]) : a.Count > 2 ? e.Valor(a[2]) : null,
                "SI(condición; si es verdadera; si no)", "Devuelve el segundo valor si la condición se cumple; si no, el tercero (sin tercero: vacío).",
                "SI({R1.POR_ATENDIDO} >= 100; \"ATENDIDA\"; \"PENDIENTE\")"),
            new("Lógicas", "Y", 1, 30, FormulaTipo.Logico, (e, a) => a.All(x => Verdad(e.Valor(x))),
                "Y(cond1; cond2; …)", "Verdadero si se cumplen todas.", "Y({R1.IMP_TOTAL} > 1000; {R1.COD_MONEDA} = \"PEN\")"),
            new("Lógicas", "O", 1, 30, FormulaTipo.Logico, (e, a) => a.Any(x => Verdad(e.Valor(x))),
                "O(cond1; cond2; …)", "Verdadero si se cumple alguna.", "O({R1.DES_ESTADO} = \"ANULADO\"; {R1.IMP_TOTAL} = 0)"),
            new("Lógicas", "NO", 1, 1, FormulaTipo.Logico, (e, a) => !Verdad(e.Valor(a[0])),
                "NO(condición)", "Lo contrario: Verdadero si la condición no se cumple.", "NO(EsNulo({R1.DES_OBSERVACION}))"),
            new("Lógicas", "EsNulo", 1, 1, FormulaTipo.Logico, (e, a) => e.Valor(a[0]) is null or "",
                "EsNulo(valor)", "Verdadero si el valor está vacío.", "EsNulo({R1.FEC_ENTREGA})"),
            new("Lógicas", "SiNulo", 2, 2, FormulaTipo.Variante, (e, a) => e.Valor(a[0]) is { } v && v is not "" ? v : e.Valor(a[1]),
                "SiNulo(valor; si está vacío)", "El valor, o el segundo si el primero está vacío.", "SiNulo({R1.DES_OBSERVACION}; \"(sin observación)\")"),
            // Texto
            new("Texto", "Mayusculas", 1, 1, FormulaTipo.Texto, (e, a) => e.TextoDe(a[0])?.ToUpperInvariant(),
                "Mayusculas(texto)", "El texto en mayúsculas.", "Mayusculas({R1.DES_ENTIDAD})"),
            new("Texto", "Minusculas", 1, 1, FormulaTipo.Texto, (e, a) => e.TextoDe(a[0])?.ToLowerInvariant(),
                "Minusculas(texto)", "El texto en minúsculas.", "Minusculas({R1.DES_ESTADO})"),
            new("Texto", "Izquierda", 2, 2, FormulaTipo.Texto, (e, a) => e.TextoDe(a[0]) is { } s && e.NumeroDe(a[1]) is { } n ? s[..(int)Math.Clamp(n, 0, s.Length)] : null,
                "Izquierda(texto; cuántos)", "Los primeros caracteres del texto.", "Izquierda({R1.DES_ENTIDAD}; 10)"),
            new("Texto", "Derecha", 2, 2, FormulaTipo.Texto, (e, a) => e.TextoDe(a[0]) is { } s && e.NumeroDe(a[1]) is { } n ? s[(s.Length - (int)Math.Clamp(n, 0, s.Length))..] : null,
                "Derecha(texto; cuántos)", "Los últimos caracteres del texto.", "Derecha({R1.NUM_DOCUMENTO_IDENTIDAD}; 4)"),
            new("Texto", "Medio", 3, 3, FormulaTipo.Texto, (e, a) => Medio(e.TextoDe(a[0]), e.NumeroDe(a[1]), e.NumeroDe(a[2])),
                "Medio(texto; desde; cuántos)", "Una parte del texto, desde la posición indicada (la primera es 1).", "Medio({R1.COD_PRODUCTO}; 4; 3)"),
            new("Texto", "Largo", 1, 1, FormulaTipo.Numero, (e, a) => e.TextoDe(a[0]) is { } s ? (decimal)s.Length : null,
                "Largo(texto)", "Cuántos caracteres tiene.", "Largo({R1.DES_OBSERVACION})"),
            new("Texto", "Recortar", 1, 1, FormulaTipo.Texto, (e, a) => e.TextoDe(a[0])?.Trim(),
                "Recortar(texto)", "Sin espacios al principio ni al final.", "Recortar({R1.DES_OBSERVACION})"),
            new("Texto", "Reemplazar", 3, 3, FormulaTipo.Texto, (e, a) => Reemplazar(e.TextoDe(a[0]), e.TextoDe(a[1]), e.TextoDe(a[2]) ?? ""),
                "Reemplazar(texto; buscar; por)", "Cambia cada aparición de un texto por otro (sin distinguir mayúsculas).", "Reemplazar({R1.DES_ESTADO}; \"_\"; \" \")"),
            new("Texto", "Contiene", 2, 2, FormulaTipo.Logico, (e, a) => e.TextoDe(a[0]) is { } s && e.TextoDe(a[1]) is { } b && s.Contains(b, StringComparison.OrdinalIgnoreCase),
                "Contiene(texto; parte)", "Verdadero si el texto incluye la parte (sin distinguir mayúsculas).", "Contiene({R1.DES_ENTIDAD}; \"SAC\")"),
            new("Texto", "Texto", 1, 2, FormulaTipo.Texto, (e, a) => TextoCon(e.Valor(a[0]), a.Count > 1 ? e.TextoDe(a[1]) : null),
                "Texto(valor; \"formato\")", "El valor como texto, con un formato de número o de fecha.", "Texto({R1.IMP_TOTAL}; \"#,##0.00\")"),
            // Números
            new("Números", "Redondear", 1, 2, FormulaTipo.Numero, (e, a) => e.NumeroDe(a[0]) is { } n ? Math.Round(n, Decimales(e, a), MidpointRounding.AwayFromZero) : null,
                "Redondear(número; decimales)", "Redondeado a esos decimales (sin el segundo dato, a entero).", "Redondear({R1.IMP_TOTAL} * 1.18; 2)"),
            new("Números", "Truncar", 1, 2, FormulaTipo.Numero, (e, a) => e.NumeroDe(a[0]) is { } n ? Truncar(n, Decimales(e, a)) : null,
                "Truncar(número; decimales)", "Corta los decimales que sobran, sin redondear.", "Truncar({R1.POR_ATENDIDO})"),
            new("Números", "Abs", 1, 1, FormulaTipo.Numero, (e, a) => e.NumeroDe(a[0]) is { } n ? Math.Abs(n) : null,
                "Abs(número)", "El número sin signo.", "Abs({R1.IMP_DIFERENCIA})"),
            // Fechas
            new("Fechas", "Hoy", 0, 0, FormulaTipo.Fecha, (e, a) => e.Ctx.Ahora.Date,
                "Hoy()", "La fecha de hoy (cuando se genera el reporte).", "DiasEntre({R1.FEC_ENTREGA}; Hoy())"),
            new("Fechas", "Ahora", 0, 0, FormulaTipo.Fecha, (e, a) => e.Ctx.Ahora,
                "Ahora()", "La fecha y la hora en que se genera el reporte.", "Texto(Ahora(); \"HH:mm\")"),
            new("Fechas", "Año", 1, 1, FormulaTipo.Numero, (e, a) => e.FechaDe(a[0]) is { } f ? f.Year : null,
                "Año(fecha)", "El año de la fecha.", "Año({R1.FEC_PEDIDO})"),
            new("Fechas", "Mes", 1, 1, FormulaTipo.Numero, (e, a) => e.FechaDe(a[0]) is { } f ? f.Month : null,
                "Mes(fecha)", "El mes de la fecha (1 a 12).", "Mes({R1.FEC_PEDIDO})"),
            new("Fechas", "Dia", 1, 1, FormulaTipo.Numero, (e, a) => e.FechaDe(a[0]) is { } f ? f.Day : null,
                "Dia(fecha)", "El día del mes.", "Dia({R1.FEC_PEDIDO})"),
            new("Fechas", "DiasEntre", 2, 2, FormulaTipo.Numero, (e, a) => e.FechaDe(a[0]) is { } d && e.FechaDe(a[1]) is { } h ? (decimal)(h.Date - d.Date).Days : null,
                "DiasEntre(desde; hasta)", "Cuántos días hay entre dos fechas. Si «hasta» es anterior, sale negativo.", "DiasEntre({R1.FEC_ENTREGA}; Hoy())"),
            new("Fechas", "SumarDias", 2, 2, FormulaTipo.Fecha, (e, a) => e.FechaDe(a[0]) is { } f && e.NumeroDe(a[1]) is { } n ? f.AddDays((double)n) : null,
                "SumarDias(fecha; días)", "La fecha más (o menos, con negativos) esos días.", "SumarDias({R1.FEC_PEDIDO}; 30)"),
            new("Fechas", "SumarMeses", 2, 2, FormulaTipo.Fecha, (e, a) => e.FechaDe(a[0]) is { } f && e.NumeroDe(a[1]) is { } n ? f.AddMonths((int)n) : null,
                "SumarMeses(fecha; meses)", "La fecha más esos meses.", "SumarMeses({R1.FEC_PEDIDO}; 2)"),
            new("Fechas", "InicioMes", 1, 1, FormulaTipo.Fecha, (e, a) => e.FechaDe(a[0]) is { } f ? new DateTime(f.Year, f.Month, 1) : null,
                "InicioMes(fecha)", "El primer día del mes de la fecha.", "InicioMes({R1.FEC_PEDIDO})"),
            new("Fechas", "FinMes", 1, 1, FormulaTipo.Fecha, (e, a) => e.FechaDe(a[0]) is { } f ? new DateTime(f.Year, f.Month, 1).AddMonths(1).AddDays(-1) : null,
                "FinMes(fecha)", "El último día del mes de la fecha.", "FinMes({R1.FEC_PEDIDO})"),
            new("Fechas", "NombreMes", 1, 1, FormulaTipo.Texto, (e, a) => e.FechaDe(a[0]) is { } f ? Meses[f.Month - 1] : null,
                "NombreMes(fecha)", "El nombre del mes: Enero, Febrero...", "NombreMes({R1.FEC_PEDIDO}) & \" \" & Año({R1.FEC_PEDIDO})"),
            new("Fechas", "NombreDia", 1, 1, FormulaTipo.Texto, (e, a) => e.FechaDe(a[0]) is { } f ? Dias[(int)f.DayOfWeek] : null,
                "NombreDia(fecha)", "El día de la semana: Lunes, Martes...", "NombreDia({R1.FEC_ENTREGA})"),
            // Conversión
            new("Conversión", "ANumero", 1, 1, FormulaTipo.Numero, (e, a) => e.NumeroDe(a[0]),
                "ANumero(texto)", "Un texto con un número (punto decimal) pasado a número.", "ANumero({R1.NUM_DOCUMENTO})"),
            new("Conversión", "AFecha", 1, 1, FormulaTipo.Fecha, (e, a) => e.FechaDe(a[0]),
                "AFecha(texto)", "Un texto con una fecha (dd/MM/yyyy o yyyy-MM-dd) pasado a fecha.", "AFecha(\"31/12/2026\")"),
            new("Conversión", "EnLetras", 1, 2, FormulaTipo.Texto, (e, a) => e.NumeroDe(a[0]) is { } n ? (ReporteLetras.Numero(n) + (a.Count > 1 && e.TextoDe(a[1]) is { Length: > 0 } m ? " " + m.Trim().ToUpperInvariant() : "")).Trim() : null,
                "EnLetras(importe; moneda)", "El importe en letras, con la moneda al final si se indica.", "EnLetras({R1.IMP_TOTAL}; {R1.DES_MONEDA})"),
            // Colores
            new("Colores", "RGB", 3, 3, FormulaTipo.Texto, (e, a) => e.NumeroDe(a[0]) is { } r && e.NumeroDe(a[1]) is { } g && e.NumeroDe(a[2]) is { } b
                    ? $"#{(int)Math.Clamp(r, 0, 255):X2}{(int)Math.Clamp(g, 0, 255):X2}{(int)Math.Clamp(b, 0, 255):X2}" : null,
                "RGB(rojo; verde; azul)", "Un color con sus componentes de 0 a 255. También hay colores con nombre: Rojo, Verde, Azul, AzulOscuro, Celeste, Negro, Gris, GrisClaro, Blanco, Naranja, Amarillo, Morado, Rosado, Marron.",
                "SI({R1.IMP_TOTAL} < 0; Rojo; RGB(22; 58; 95))")
        };
        return l.ToDictionary(f => Normal(f.Nombre));
    }

    private static readonly string[] Meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
    private static readonly string[] Dias = { "Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };

    private static int Decimales(Evaluador e, List<Nodo> a) => a.Count > 1 && e.NumeroDe(a[1]) is { } d ? (int)Math.Clamp(d, 0, 10) : 0;

    private static decimal Truncar(decimal n, int dec)
    {
        var f = (decimal)Math.Pow(10, dec);
        return decimal.Truncate(n * f) / f;
    }

    private static string? Medio(string? s, decimal? desde, decimal? n)
    {
        if (s is null || desde is null || n is null) return null;
        var i = (int)Math.Clamp(desde.Value - 1, 0, s.Length);
        return s.Substring(i, (int)Math.Clamp(n.Value, 0, s.Length - i));
    }

    private static string? Reemplazar(string? s, string? buscar, string por)
        => s is null ? null : string.IsNullOrEmpty(buscar) ? s : s.Replace(buscar, por, StringComparison.OrdinalIgnoreCase);

    private static string? TextoCon(object? v, string? formato) => v switch
    {
        null => null,
        decimal d => ReporteFormato.Texto(d, formato, typeof(decimal)),
        DateTime f => ReporteFormato.Texto(f, formato, typeof(DateTime)),
        _ => Evaluador.ATexto(v)
    };

    // ── Comprobación ────────────────────────────────────────────────────────────
    private sealed class Comprobador
    {
        private readonly FormulaEsquema _e;
        private readonly string? _propio;
        private readonly bool _conPagina;
        // Las fórmulas que se están comprobando, de la de afuera a la de adentro, para ver un círculo (A → B → A).
        private readonly List<string> _camino = new();
        private readonly Dictionary<string, FormulaTipo> _tipos = new(StringComparer.OrdinalIgnoreCase);
        private string? _circulo;
        private bool _pagina;
        public List<FormulaError> Errores { get; } = new();

        public Comprobador(FormulaEsquema e, string? propio, bool conPagina)
        {
            _e = e; _propio = propio; _conPagina = conPagina;
            if (propio is not null) _camino.Add(propio);
        }

        private void Error(Nodo n, string m) => Errores.Add(new FormulaError(n.Linea, n.Columna, m));

        public FormulaTipo Tipo(Nodo n)
        {
            switch (n)
            {
                case NValor v: return v.Tipo;
                case NRef r: return TipoRef(r);
                case NUnario u:
                    Tipo(u.X);
                    return FormulaTipo.Numero;
                case NBinario b:
                {
                    var ta = Tipo(b.A);
                    var tb = Tipo(b.B);
                    switch (b.Op)
                    {
                        case "&": return FormulaTipo.Texto;
                        case "=" or "<>" or "<" or ">" or "<=" or ">=": return FormulaTipo.Logico;
                        case "+": return ta == FormulaTipo.Fecha || tb == FormulaTipo.Fecha ? FormulaTipo.Fecha : NumeroUOtro(b, ta, tb);
                        case "-": return ta == FormulaTipo.Fecha && tb == FormulaTipo.Fecha ? FormulaTipo.Numero
                                       : ta == FormulaTipo.Fecha ? FormulaTipo.Fecha : NumeroUOtro(b, ta, tb);
                        default: return NumeroUOtro(b, ta, tb);
                    }
                }
                case NLlamada l:
                {
                    if (!Funciones.TryGetValue(l.Nombre, out var f))
                    {
                        Error(l, $"No existe la función «{l.Escrito}».");
                        foreach (var a in l.Args) Tipo(a);
                        return FormulaTipo.Variante;
                    }
                    if (l.Args.Count < f.Min || l.Args.Count > f.Max)
                        Error(l, f.Min == f.Max
                            ? f.Min == 0 ? $"{f.Nombre} no lleva datos: {f.Firma}." : $"{f.Nombre} lleva {Cuantos(f.Min)}: {f.Firma}."
                            : $"{f.Nombre} lleva de {f.Min} a {f.Max} datos: {f.Firma}.");
                    var tipos = l.Args.Select(Tipo).ToList();
                    if (f.Nombre is "SI" && tipos.Count >= 2) return tipos.Count > 2 && tipos[2] != tipos[1] ? FormulaTipo.Variante : tipos[1];
                    if (f.Nombre is "SiNulo" && tipos.Count == 2) return tipos[0] == tipos[1] ? tipos[0] : FormulaTipo.Variante;
                    return f.Tipo;
                }
                default: return FormulaTipo.Variante;
            }
        }

        private static string Cuantos(int n) => n == 1 ? "un dato" : $"{n} datos";

        private static string Circulo(string c)
            => c.Contains('→') && c.Split(" → ").Distinct(StringComparer.OrdinalIgnoreCase).Count() > 1
                ? $"Las fórmulas se usan entre sí en círculo: {c}."
                : $"La fórmula «{c.Split(" → ")[0]}» se usa a sí misma.";

        private FormulaTipo NumeroUOtro(NBinario b, FormulaTipo ta, FormulaTipo tb)
        {
            if (ta is FormulaTipo.Texto or FormulaTipo.Logico || tb is FormulaTipo.Texto or FormulaTipo.Logico)
                Error(b, $"«{b.Op}» es para números. Para unir textos usa &.");
            return FormulaTipo.Numero;
        }

        private FormulaTipo TipoRef(NRef r)
        {
            switch (r.Tipo)
            {
                case RefTipo.Parametro:
                    if (_e.Parametros.TryGetValue(r.Nombre, out var tp)) return tp;
                    Error(r, $"El reporte no tiene el parámetro @{r.Nombre}.");
                    return FormulaTipo.Variante;
                case RefTipo.Especial:
                    if (!_conPagina && r.Nombre is "PAGINA" or "TOTAL_PAGINAS")
                    {
                        _pagina = true;
                        Error(r, "Aquí no se puede usar el número de página: la sección se decide antes de saber en qué página cae.");
                    }
                    return Especiales.Values.First(x => x.Codigo == r.Nombre).Tipo;
                case RefTipo.Campo:
                {
                    var res = r.Papel is null ? r.Resultado : _e.Papeles.GetValueOrDefault(r.Papel, r.Papel switch { "CAB" => 1, "DET" => 2, _ => 3 });
                    if (!_e.Columnas.TryGetValue(res, out var cols)) { Error(r, $"La fuente no tiene el resultado {res} ({r.Escrito})."); return FormulaTipo.Variante; }
                    if (cols.TryGetValue(r.Nombre, out var tc)) return tc;
                    Error(r, $"{r.Escrito}: el resultado {res} no tiene la columna {r.Nombre}.");
                    return FormulaTipo.Variante;
                }
                case RefTipo.Resumen:
                {
                    if (r.Nivel is > 0 && r.Nivel > _e.Grupos)
                        Error(r, _e.Grupos == 0 ? $"{r.Escrito}: el diseño no tiene grupos (usa .Informe)." : $"{r.Escrito}: el diseño tiene {_e.Grupos} nivel(es) de grupo.");
                    if (!_e.Columnas.TryGetValue(1, out var cols) || !cols.TryGetValue(r.Nombre, out var tc))
                    {
                        Error(r, $"{r.Escrito}: el resultado 1 no tiene la columna {r.Nombre}.");
                        return FormulaTipo.Numero;
                    }
                    if (r.Funcion is "SUM" or "AVG" && tc is not (FormulaTipo.Numero or FormulaTipo.Variante))
                        Error(r, $"{r.Escrito}: la suma y el promedio son de columnas numéricas.");
                    return r.Funcion is "MAX" or "MIN" ? tc : FormulaTipo.Numero;
                }
                case RefTipo.Formula:
                {
                    if (!_e.Formulas.TryGetValue(r.Nombre, out var texto)) { Error(r, $"No hay una fórmula «{r.Nombre}» en el diseño."); return FormulaTipo.Variante; }
                    var i = _camino.FindIndex(x => string.Equals(x, r.Nombre, StringComparison.OrdinalIgnoreCase));
                    if (i >= 0)
                    {
                        _circulo ??= string.Join(" → ", _camino.Skip(i).Append(r.Nombre));
                        Error(r, Circulo(_circulo));
                        return FormulaTipo.Variante;
                    }
                    if (_tipos.TryGetValue(r.Nombre, out var ya)) return ya;
                    // El tipo de la otra fórmula. Sus propios errores se informan al comprobarla a ella; aquí solo el
                    // círculo y la página, que dependen de dónde se usa.
                    if (!Probar(texto, out var otra, out _)) return FormulaTipo.Variante;
                    var sub = new Comprobador(_e, null, _conPagina);
                    sub._camino.AddRange(_camino);
                    sub._camino.Add(r.Nombre);
                    var t = sub.Tipo(otra!._raiz);
                    if (sub._circulo is { } c)
                    {
                        _circulo ??= c;
                        Error(r, Circulo(c));
                    }
                    else if (sub._pagina)
                    {
                        _pagina = true;
                        Error(r, $"«{r.Nombre}» usa el número de página, y aquí no se puede: la sección se decide antes de saber en qué página cae.");
                    }
                    return _tipos[r.Nombre] = t;
                }
            }
            return FormulaTipo.Variante;
        }
    }

    // ── Evaluación ──────────────────────────────────────────────────────────────
    private sealed class Evaluador
    {
        public IFormulaContexto Ctx { get; }
        public Evaluador(IFormulaContexto ctx) => Ctx = ctx;

        public object? Valor(Nodo n)
        {
            switch (n)
            {
                case NValor v: return v.Valor;
                case NRef r: return Ref(r);
                case NUnario u:
                {
                    var x = NumeroDe(u.X);
                    return x is null ? null : u.Op == '-' ? -x : x;
                }
                case NBinario b: return Binario(b);
                case NLlamada l: return Funciones[l.Nombre].Hacer(this, l.Args);
                default: return null;
            }
        }

        private object? Ref(NRef r) => r.Tipo switch
        {
            RefTipo.Campo => Normalizar(Ctx.Campo(r.Papel is null ? r.Resultado : Ctx.Resultado(r.Papel), r.Nombre)),
            RefTipo.Parametro => Normalizar(Ctx.Parametro(r.Nombre)),
            RefTipo.Formula => Ctx.Formula(r.Nombre),
            RefTipo.Resumen => Normalizar(Ctx.Resumen(r.Funcion, r.Nombre, r.Nivel)),
            RefTipo.Especial => Normalizar(Ctx.Especial(r.Nombre)),
            _ => null
        };

        private object? Binario(NBinario b)
        {
            if (b.Op == "&") return ATexto(Valor(b.A)) + ATexto(Valor(b.B));
            var a = Valor(b.A);
            var c = Valor(b.B);
            switch (b.Op)
            {
                case "=": return Iguales(a, c);
                case "<>": return !Iguales(a, c);
                case "<" or ">" or "<=" or ">=":
                {
                    if (Comparar(a, c) is not { } k) return false;
                    return b.Op switch { "<" => k < 0, ">" => k > 0, "<=" => k <= 0, _ => k >= 0 };
                }
            }
            if (a is null || c is null) return null;
            // Fechas: fecha ± días, y fecha - fecha = días.
            if (a is DateTime fa)
            {
                if (b.Op == "-" && c is DateTime fc) return (decimal)(fa - fc).TotalDays;
                if (b.Op is "+" or "-" && ANumero(c) is { } dias) return fa.AddDays((double)(b.Op == "+" ? dias : -dias));
                return null;
            }
            if (c is DateTime fd && b.Op == "+" && ANumero(a) is { } d2) return fd.AddDays((double)d2);
            var x = ANumero(a);
            var y = ANumero(c);
            if (x is null || y is null) return null;
            return b.Op switch
            {
                "+" => x + y,
                "-" => x - y,
                "*" => x * y,
                "/" => y == 0 ? null : x / y,
                "%" => y == 0 ? null : x % y,
                _ => null
            };
        }

        private static bool Iguales(object? a, object? b)
        {
            if (a is null or "" && b is null or "") return true;
            if (a is null || b is null) return false;
            return Comparar(a, b) == 0;
        }

        /// <summary>Números con números, fechas con fechas; un texto que es número o fecha se compara como tal; lo demás
        /// como texto, sin distinguir mayúsculas. null: no se pueden comparar.</summary>
        private static int? Comparar(object? a, object? b)
        {
            if (a is null || b is null) return null;
            if (a is decimal || b is decimal)
                return ANumero(a) is { } x && ANumero(b) is { } y ? x.CompareTo(y) : null;
            if (a is DateTime || b is DateTime)
                return AFecha(a) is { } x && AFecha(b) is { } y ? x.CompareTo(y) : null;
            if (a is bool ba && b is bool bb) return ba.CompareTo(bb);
            return string.Compare(ATexto(a).TrimEnd(), ATexto(b).TrimEnd(), CultureInfo.InvariantCulture, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace);
        }

        public decimal? NumeroDe(Nodo n) => ANumero(Valor(n));
        public string? TextoDe(Nodo n) => Valor(n) is { } v ? ATexto(v) : null;
        public DateTime? FechaDe(Nodo n) => AFecha(Valor(n));

        public static decimal? ANumero(object? v) => v switch
        {
            null => null,
            decimal d => d,
            bool b => b ? 1 : 0,
            string s => decimal.TryParse(s.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out var n) ? n : null,
            _ => null
        };

        private static readonly string[] FormatosFecha = { "dd/MM/yyyy", "dd/MM/yyyy HH:mm", "dd/MM/yyyy HH:mm:ss", "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-dd HH:mm:ss", "yyyy-MM-ddTHH:mm" };

        public static DateTime? AFecha(object? v) => v switch
        {
            DateTime f => f,
            string s => DateTime.TryParseExact(s.Trim(), FormatosFecha, CultureInfo.InvariantCulture, DateTimeStyles.None, out var f) ? f : null,
            _ => null
        };

        public static string ATexto(object? v) => v switch
        {
            null => "",
            string s => s,
            decimal d => d.ToString("0.##########", CultureInfo.InvariantCulture),
            bool b => b ? "Sí" : "No",
            DateTime f => f.TimeOfDay == TimeSpan.Zero ? f.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : f.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
            _ => Convert.ToString(v, CultureInfo.InvariantCulture) ?? ""
        };
    }
}
