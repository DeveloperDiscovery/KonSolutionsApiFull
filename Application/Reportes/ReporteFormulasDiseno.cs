using System.Globalization;
using System.Text.RegularExpressions;
using KonSolutionsApi.Domain.Models.Reportes;

namespace KonSolutionsApi.Application.Reportes;

// Las fórmulas de un diseño libre puestas a trabajar:
//   - ReporteFormulasDatos: las evalúa sobre los datos de un reporte. Lo usan el PDF (motor de bandas), el Excel y el CSV
//     (columnas calculadas) y la prueba del editor de fórmulas.
//   - ReporteFormulasDiseno: comprueba el lienzo antes de grabarlo (una fórmula o una condición con errores no se graba)
//     y arma las columnas calculadas de Excel y CSV.

/// <summary>Dónde se evalúa una fórmula: la fila (índice entre las filas ordenadas del resultado 1), las filas de cada
/// grupo abierto (nivel 1 en la posición 0), el nivel de la sección (para un resumen sin alcance) y la página.</summary>
public sealed class ReporteFormulaLugar
{
    public int                     Fila         { get; init; } = -1;
    public (int Desde, int Hasta)[] Grupos      { get; init; } = Array.Empty<(int, int)>();
    public int                     NivelSeccion { get; init; }
    public int                     Pagina       { get; init; } = 1;
    public int                     Paginas      { get; init; } = 1;
}

public sealed class ReporteFormulasDatos
{
    public const char Seccion = 'B';
    public const char Objeto = 'O';
    private const int Profundidad = 30;
    private static readonly Regex EsMarca = new(@"^(F\.\w+|[ΣSs]\.[^.]+\.[^.]+(\.[^.]+)?|#\w+|(R\d+|CAB|DET|ADI)\.\w+)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex EsCampo = new(@"^(R\d+|CAB|DET|ADI)\.(\w+)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private readonly ReporteContexto _ctx;
    private readonly ReporteTabla? _tabla;
    private readonly IReadOnlyList<object?[]> _filas;
    private readonly Dictionary<string, ReporteFormula> _formulas = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<(char, int, string), ReporteFormula> _condiciones = new();
    private readonly Dictionary<string, ReporteFormula?> _marcas = new(StringComparer.Ordinal);
    private readonly Dictionary<string, bool> _usaPagina = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<(int, string), ReporteColumnaVista?> _vistas = new();
    private readonly Dictionary<(string, string, int, int), object?> _resumenes = new();

    /// <summary>filas: las del resultado 1 en el orden en que se imprimen (los índices del lugar son de esta lista).</summary>
    public ReporteFormulasDatos(ReporteContexto ctx, ReporteTabla? tabla, IReadOnlyList<object?[]> filas, ReporteLienzo? lienzo)
    {
        _ctx = ctx;
        _tabla = tabla;
        _filas = filas;
        // Lo grabado ya pasó la comprobación; si algo no se puede leer, se salta (el objeto sale vacío).
        foreach (var f in lienzo?.Formulas ?? new())
            if (!string.IsNullOrWhiteSpace(f.DES_NOMBRE) && ReporteFormula.Probar(f.DES_FORMULA, out var c, out _)) _formulas[f.DES_NOMBRE.Trim()] = c!;
        foreach (var c in lienzo?.Condiciones ?? new())
            if (!string.IsNullOrEmpty(c.COD_ELEMENTO) && ReporteFormula.Probar(c.DES_FORMULA, out var x, out _))
                _condiciones[(char.ToUpperInvariant(c.COD_ELEMENTO[0]), c.NUM_ELEMENTO, (c.COD_PROPIEDAD ?? "").Trim().ToUpperInvariant())] = x!;
    }

    public bool Hay => _formulas.Count > 0 || _condiciones.Count > 0;
    public bool Existe(string? nombre) => nombre is not null && _formulas.ContainsKey(nombre.Trim());

    public object? Formula(string? nombre, ReporteFormulaLugar l) => nombre is null ? null : new Contexto(this, l).Formula(nombre.Trim());

    /// <summary>Una fórmula suelta (la que se está probando en el editor) con las del diseño a mano.</summary>
    public object? Evaluar(ReporteFormula f, ReporteFormulaLugar l) => f.Evaluar(new Contexto(this, l));

    public bool FormulaUsaPagina(string? nombre)
    {
        if (nombre is null || !_formulas.TryGetValue(nombre.Trim(), out var f)) return false;
        if (!_usaPagina.TryGetValue(nombre.Trim(), out var u)) _usaPagina[nombre.Trim()] = u = f.UsaPagina(n => _formulas.GetValueOrDefault(n));
        return u;
    }

    // ── Condiciones ─────────────────────────────────────────────────────────────
    public bool TieneCondicion(char elemento, int numero, string propiedad) => _condiciones.ContainsKey((elemento, numero, propiedad));

    /// <summary>Verdadero o falso según la condición; null si no hay condición o si dio vacío (queda el valor fijo).</summary>
    public bool? Logico(char elemento, int numero, string propiedad, ReporteFormulaLugar l)
    {
        if (!_condiciones.TryGetValue((elemento, numero, propiedad), out var f)) return null;
        var v = f.Evaluar(new Contexto(this, l));
        return v is null ? null : ReporteFormula.Verdad(v);
    }

    /// <summary>El color que dio la condición (#RRGGBB); null si no hay condición o no dio un color.</summary>
    public string? Color(char elemento, int numero, string propiedad, ReporteFormulaLugar l)
        => _condiciones.TryGetValue((elemento, numero, propiedad), out var f) ? ReporteFormula.AColor(f.Evaluar(new Contexto(this, l))) : null;

    // ── Marcas de los textos ────────────────────────────────────────────────────
    /// <summary>¿Lo que va entre llaves en un texto es una referencia de fórmula ({F.X}, {R2.COL}, {Σ.Suma.COL}, {#Pagina})?
    /// Las marcas de siempre ({COLUMNA}, {@PARAMETRO}, {USUARIO}) no lo son.</summary>
    public static bool EsReferencia(string clave) => EsMarca.IsMatch(clave.Trim());

    private ReporteFormula? Compilada(string clave)
    {
        if (!_marcas.TryGetValue(clave, out var f))
            _marcas[clave] = f = ReporteFormula.Probar("{" + clave + "}", out var c, out _) ? c : null;
        return f;
    }

    public bool MarcaUsaPagina(string clave) => Compilada(clave)?.UsaPagina(n => _formulas.GetValueOrDefault(n)) ?? false;

    /// <summary>El texto de una referencia de fórmula dentro de un texto. Un campo sale con el formato de su columna;
    /// lo demás, con el formato de siempre. null si no es una referencia que se pueda leer (queda como está).</summary>
    public string? Marca(string clave, ReporteFormulaLugar l)
    {
        clave = clave.Trim();
        var f = Compilada(clave);
        if (f is null) return null;
        var v = f.Evaluar(new Contexto(this, l));
        var campo = EsCampo.Match(clave);
        if (campo.Success)
        {
            var c = new Contexto(this, l);
            var cab = campo.Groups[1].Value.ToUpperInvariant();
            var r = cab[0] == 'R' ? int.Parse(cab[1..], CultureInfo.InvariantCulture) : c.Resultado(cab);
            var col = campo.Groups[2].Value;
            var crudo = c.Campo(r, col);
            var vista = Vista(r, col);
            return ReporteFormato.Texto(crudo, vista?.Formato, vista?.Tipo ?? typeof(string), n => c.Campo(r, n));
        }
        return ReporteFormula.Formatear(v, null);
    }

    private ReporteColumnaVista? Vista(int resultado, string columna)
    {
        var k = (resultado, columna.ToUpperInvariant());
        if (!_vistas.TryGetValue(k, out var v)) _vistas[k] = v = ReporteColumnas.Buscar(_ctx, resultado, columna);
        return v;
    }

    // ── Datos ───────────────────────────────────────────────────────────────────
    private object? Campo(int resultado, string columna, ReporteFormulaLugar l)
    {
        if (_tabla is not null && resultado == _tabla.Numero)
            return l.Fila >= 0 && l.Fila < _filas.Count ? _tabla.Valor(_filas[l.Fila], columna) : null;
        var t = _ctx.Datos.Tabla(resultado);
        return t is { Filas.Count: > 0 } ? t.Valor(t.Filas[0], columna) : null;
    }

    private int Resultado(string papel)
        => _ctx.Definicion.Resultados.FirstOrDefault(r => string.Equals(r.COD_PAPEL, papel, StringComparison.OrdinalIgnoreCase))?.NUM_RESULTADO
           ?? papel.ToUpperInvariant() switch { "CAB" => 1, "DET" => 2, _ => 3 };

    /// <summary>Un resumen del resultado 1, sin formato: SUM, AVG, CNT y CND dan un número; MAX y MIN, el valor.
    /// nivel: 0 el informe, 1.. un grupo, null el de la sección (su grupo en un encabezado o pie de grupo; si no, el informe).</summary>
    public object? Resumen(string funcion, string columna, int? nivel, ReporteFormulaLugar l)
    {
        if (_tabla is null || _tabla.Indice(columna) < 0) return null;
        var n = nivel ?? l.NivelSeccion;
        var (desde, hasta) = n > 0 && n <= l.Grupos.Length ? l.Grupos[n - 1] : (0, _filas.Count);
        desde = Math.Max(0, desde);
        hasta = Math.Min(hasta, _filas.Count);
        // Las filas no cambian mientras se arma el reporte: cada resumen de un tramo se calcula una sola vez. Si no, una
        // fórmula como el % del grupo, que está en cada fila, sumaría el grupo entero cada vez (con 20 000 filas, minutos).
        var clave = (funcion.ToUpperInvariant(), columna.ToUpperInvariant(), desde, hasta);
        if (!_resumenes.TryGetValue(clave, out var r)) _resumenes[clave] = r = Resumir(clave.Item1, columna, desde, hasta);
        return r;
    }

    private object? Resumir(string funcion, string columna, int desde, int hasta)
    {
        var valores = new List<object>();
        for (var i = desde; i < hasta; i++)
            if (_tabla!.Valor(_filas[i], columna) is { } v and not DBNull) valores.Add(v);
        switch (funcion)
        {
            case "CNT": return (decimal)valores.Count;
            case "CND": return (decimal)valores.Select(v => Convert.ToString(v, CultureInfo.InvariantCulture)).Distinct().Count();
            case "SUM":
            case "AVG":
            {
                decimal s = 0;
                var k = 0;
                foreach (var v in valores)
                    if (ReporteFormato.ADecimal(v, out var d)) { s += d; k++; }
                return funcion.Equals("AVG", StringComparison.OrdinalIgnoreCase) ? (k == 0 ? null : s / k) : s;
            }
            case "MAX":
            case "MIN":
            {
                if (valores.Count == 0) return null;
                var orden = valores.OrderBy(v => v, Comparador.Instancia).ToList();
                return funcion.Equals("MAX", StringComparison.OrdinalIgnoreCase) ? orden[^1] : orden[0];
            }
            default: return null;
        }
    }

    private object? Especial(string codigo, ReporteFormulaLugar l)
    {
        var e = _ctx.Empresa;
        return codigo switch
        {
            "PAGINA"            => (decimal)l.Pagina,
            "TOTAL_PAGINAS"     => (decimal)l.Paginas,
            "FECHA"             => _ctx.Fecha.Date,
            "FECHA_HORA"        => _ctx.Fecha,
            "USUARIO"           => string.IsNullOrWhiteSpace(e?.DES_USUARIO) ? _ctx.Usuario : e!.DES_USUARIO,
            "FILTROS"           => ReporteTexto.Filtros(_ctx),
            "TITULO"            => ReporteTexto.Reemplazar(_ctx.D.DES_TITULO ?? _ctx.D.DES_DISENO, _ctx),
            "SUBTITULO"         => ReporteTexto.Reemplazar(_ctx.D.DES_SUBTITULO, _ctx),
            "EMPRESA_NOMBRE"    => e?.DES_NOMBRE_EMPRESA,
            "EMPRESA_RUC"       => e?.NUM_DOCUMENTO_IDENTIDAD,
            "EMPRESA_DIRECCION" => e?.DES_DOMICILIO_FISCAL,
            "APLICACION_NOMBRE" => e?.DES_NOMBRE_APLICACION,
            _                   => null
        };
    }

    /// <summary>Las filas de cada grupo, para cada fila: Rangos[i][n] son las filas [Desde, Hasta) del grupo de nivel n + 1
    /// al que pertenece la fila i. Las claves se comparan ya formateadas, igual que en el PDF.</summary>
    public static (int Desde, int Hasta)[][] Rangos(ReporteContexto ctx, ReporteTabla? tabla, IReadOnlyList<object?[]> filas, IReadOnlyList<ReporteLienzoGrupo> grupos)
    {
        var res = new (int, int)[filas.Count][];
        if (tabla is null || grupos.Count == 0)
        {
            for (var i = 0; i < filas.Count; i++) res[i] = Array.Empty<(int, int)>();
            return res;
        }
        var vistas = grupos.Select(g => ReporteColumnas.Buscar(ctx, tabla.Numero, g.DES_COLUMNA)).ToList();
        var claves = filas.Select(f => grupos.Select((g, n) => ReporteFormato.Texto(tabla.Valor(f, g.DES_COLUMNA), vistas[n]?.Formato,
                                                                                    vistas[n]?.Tipo ?? typeof(string), c => tabla.Valor(f, c))).ToArray()).ToList();
        for (var i = 0; i < filas.Count; i++) res[i] = new (int, int)[grupos.Count];
        for (var n = 0; n < grupos.Count; n++)
        {
            var desde = 0;
            for (var i = 1; i <= filas.Count; i++)
            {
                var corta = i == filas.Count;
                for (var k = 0; !corta && k <= n; k++)
                    if (!string.Equals(claves[i][k], claves[i - 1][k], StringComparison.Ordinal)) corta = true;
                if (!corta) continue;
                for (var j = desde; j < i; j++) res[j][n] = (desde, i);
                desde = i;
            }
        }
        return res;
    }

    private sealed class Comparador : IComparer<object>
    {
        public static readonly Comparador Instancia = new();
        public int Compare(object? a, object? b)
        {
            if (a is null || b is null) return a is null ? (b is null ? 0 : -1) : 1;
            if (a is not string && b is not string && ReporteFormato.ADecimal(a, out var x) && ReporteFormato.ADecimal(b, out var y)) return x.CompareTo(y);
            if (a is IComparable c && a.GetType() == b.GetType()) return c.CompareTo(b);
            return string.Compare(Convert.ToString(a, CultureInfo.InvariantCulture), Convert.ToString(b, CultureInfo.InvariantCulture), StringComparison.CurrentCultureIgnoreCase);
        }
    }

    private sealed class Contexto : IFormulaContexto
    {
        private readonly ReporteFormulasDatos _d;
        private readonly ReporteFormulaLugar _l;
        private readonly HashSet<string> _enCurso = new(StringComparer.OrdinalIgnoreCase);

        public Contexto(ReporteFormulasDatos d, ReporteFormulaLugar l) { _d = d; _l = l; }

        public object? Campo(int resultado, string columna) => _d.Campo(resultado, columna, _l);
        public int Resultado(string papel) => _d.Resultado(papel);
        public object? Parametro(string nombre)
            => _d._ctx.Valores.FirstOrDefault(v => string.Equals(v.Campo, ReporteTexto.SinArroba(nombre), StringComparison.OrdinalIgnoreCase))?.Valor;
        public object? Resumen(string funcion, string columna, int? nivel) => _d.Resumen(funcion, columna, nivel, _l);
        public object? Especial(string codigo) => _d.Especial(codigo, _l);
        public DateTime Ahora => _d._ctx.Fecha;

        public object? Formula(string nombre)
        {
            // Un círculo no llega a grabarse; igual se corta, por si acaso.
            if (!_d._formulas.TryGetValue(nombre, out var f) || _enCurso.Count >= Profundidad || !_enCurso.Add(nombre)) return null;
            try { return f.Evaluar(this); }
            finally { _enCurso.Remove(nombre); }
        }
    }
}

public static class ReporteFormulasDiseno
{
    /// <summary>Las propiedades que admiten condición: código → (nombre para el usuario, ¿es un color?).</summary>
    public static readonly IReadOnlyDictionary<string, (string Nombre, bool EsColor)> PropiedadesSeccion = new Dictionary<string, (string, bool)>
    {
        ["SUPRIMIR"] = ("No imprimir", false), ["FONDO"] = ("Color de fondo", true),
        ["NUEVA_PAGINA_ANTES"] = ("Página nueva antes", false), ["NUEVA_PAGINA_DESPUES"] = ("Página nueva después", false)
    };

    public static readonly IReadOnlyDictionary<string, (string Nombre, bool EsColor)> PropiedadesObjeto = new Dictionary<string, (string, bool)>
    {
        ["SUPRIMIR"] = ("No imprimir", false), ["NEGRITA"] = ("Negrita", false), ["COLOR"] = ("Color", true), ["FONDO"] = ("Color de fondo", true)
    };

    /// <summary>Lo que una fórmula puede nombrar en un diseño: columnas, parámetros y papeles de la fuente, las fórmulas
    /// del diseño y cuántos grupos tiene.</summary>
    public static FormulaEsquema Esquema(IEnumerable<ReporteFuenteColumna> columnas, IEnumerable<ReporteFuenteParametro> parametros,
                                         IEnumerable<ReporteFuenteResultado> resultados, IEnumerable<ReporteLienzoFormula> formulas, int grupos)
    {
        var e = new FormulaEsquema { Grupos = grupos };
        foreach (var c in columnas) e.Columna(c.NUM_RESULTADO, c.DES_COLUMNA, c.DES_TIPO_DATO);
        foreach (var p in parametros) e.Parametros[ReporteTexto.SinArroba(p.DES_PARAMETRO)] = ReporteFormula.TipoSql(p.DES_TIPO_DATO);
        foreach (var r in resultados) e.Papeles.TryAdd(r.COD_PAPEL, r.NUM_RESULTADO);
        foreach (var f in formulas.Where(f => !string.IsNullOrWhiteSpace(f.DES_NOMBRE))) e.Formulas[f.DES_NOMBRE.Trim()] = f.DES_FORMULA ?? "";
        return e;
    }

    /// <summary>Comprueba una fórmula para un uso: escritura, nombres, datos de las funciones y que el resultado sirva
    /// para la propiedad (Verdadero o Falso, o un color). elemento: B una sección, O un objeto, null una fórmula.</summary>
    public static List<FormulaError> Comprobar(string? texto, FormulaEsquema esquema, string? nombrePropio, char? elemento, string? propiedad, out FormulaTipo tipo)
    {
        tipo = FormulaTipo.Variante;
        if (!ReporteFormula.Probar(texto, out var f, out var error)) return new List<FormulaError> { error! };
        var prop = (propiedad ?? "").Trim().ToUpperInvariant();
        var esColor = prop is "COLOR" or "FONDO";
        // Lo que decide dónde va cada cosa (suprimir, negrita, página nueva) se resuelve antes de saber la página.
        var conPagina = elemento is null || esColor;
        var errores = f!.Comprobar(esquema, out tipo, nombrePropio, conPagina);
        if (errores.Count > 0 || elemento is null) return errores;
        if (esColor && tipo is not (FormulaTipo.Texto or FormulaTipo.Variante))
            errores.Add(new FormulaError(1, 1, $"Esta condición tiene que dar un color (Rojo, \"#C62828\", RGB(…)); da {ReporteFormula.NombreTipo(tipo)}."));
        else if (!esColor && tipo is not (FormulaTipo.Logico or FormulaTipo.Variante))
            errores.Add(new FormulaError(1, 1, $"Esta condición tiene que dar Verdadero o Falso; da {ReporteFormula.NombreTipo(tipo)}. Compara con =, <, > o usa Y(…), O(…)."));
        return errores;
    }

    private static readonly Regex Marcas = new(@"\{([^{}\r\n]+)\}", RegexOptions.Compiled);

    /// <summary>Todo lo que hay que corregir en las fórmulas de un lienzo antes de grabarlo (vacío si está bien).</summary>
    public static List<string> Validar(ReporteLienzo l, FormulaEsquema esquema)
    {
        var res = new List<string>();
        var bandas = l.Bandas.ToDictionary(b => b.NUM_BANDA);
        string Banda(int num) => bandas.TryGetValue(num, out var b) ? NombreSeccion(b) : "una sección que no existe";

        foreach (var g in l.Formulas.GroupBy(f => (f.DES_NOMBRE ?? "").Trim(), StringComparer.OrdinalIgnoreCase))
        {
            if (!ReporteFormula.NombreValido(g.Key))
                res.Add($"El nombre de fórmula «{g.Key}» no sirve: letras sin tilde, números y _, empezando con una letra (hasta 50).");
            else if (g.Count() > 1)
                res.Add($"Hay {g.Count()} fórmulas con el nombre «{g.Key}».");
        }
        foreach (var f in l.Formulas.Where(f => ReporteFormula.NombreValido((f.DES_NOMBRE ?? "").Trim())))
            foreach (var e in Comprobar(f.DES_FORMULA, esquema, f.DES_NOMBRE.Trim(), null, null, out _))
                res.Add($"Fórmula «{f.DES_NOMBRE.Trim()}», línea {e.Linea}, columna {e.Columna}: {e.Mensaje}");

        var nombres = new HashSet<string>(l.Formulas.Select(f => (f.DES_NOMBRE ?? "").Trim()), StringComparer.OrdinalIgnoreCase);
        foreach (var o in l.Objetos.Where(o => o.COD_TIPO_OBJETO == "FOR" && !nombres.Contains((o.DES_ORIGEN ?? "").Trim())))
            res.Add($"Un campo de fórmula en {Banda(o.NUM_BANDA)} usa «{o.DES_ORIGEN}», que no es una fórmula del diseño.");

        // Las referencias de fórmula dentro de los textos y de lo que codifica un código de barras ({F.X}, {R2.COL}...): las
        // marcas de siempre no se tocan.
        foreach (var o in l.Objetos.Where(o => o.COD_TIPO_OBJETO is "TXT" or "BAR" && !string.IsNullOrEmpty(o.DES_ORIGEN)))
            foreach (Match m in Marcas.Matches(o.DES_ORIGEN!))
            {
                var clave = m.Groups[1].Value.Trim();
                if (!ReporteFormulasDatos.EsReferencia(clave)) continue;
                foreach (var e in Comprobar("{" + clave + "}", esquema, null, null, null, out _))
                    res.Add($"{(o.COD_TIPO_OBJETO == "BAR" ? "El código" : "El texto")} «{Corto(o.DES_ORIGEN!)}» en {Banda(o.NUM_BANDA)}: {e.Mensaje}");
            }

        // Y las de los enlaces de un subinforme o de una tabla (@PARAMETRO={F.X}, COLUMNA={R2.COL}).
        foreach (var o in l.Objetos.Where(o => !string.IsNullOrWhiteSpace(o.DES_ENLACES)))
            foreach (var (nombre, expresion) in ReporteBandasMotor.Enlaces(o.DES_ENLACES))
                foreach (Match m in Marcas.Matches(expresion))
                {
                    var clave = m.Groups[1].Value.Trim();
                    if (!ReporteFormulasDatos.EsReferencia(clave)) continue;
                    foreach (var e in Comprobar("{" + clave + "}", esquema, null, null, null, out _))
                        res.Add($"El enlace {nombre} {De(NombreObjeto(o))} en {Banda(o.NUM_BANDA)}: {e.Mensaje}");
                }

        var objetos = l.Objetos.ToDictionary(o => o.NUM_OBJETO);
        foreach (var c in l.Condiciones)
        {
            var elem = string.IsNullOrEmpty(c.COD_ELEMENTO) ? ' ' : char.ToUpperInvariant(c.COD_ELEMENTO[0]);
            var prop = (c.COD_PROPIEDAD ?? "").Trim().ToUpperInvariant();
            string donde;
            (string Nombre, bool EsColor) info;
            if (elem == ReporteFormulasDatos.Seccion && bandas.TryGetValue(c.NUM_ELEMENTO, out var b) && PropiedadesSeccion.TryGetValue(prop, out info))
                donde = $"«{info.Nombre}» {De(NombreSeccion(b))}";
            else if (elem == ReporteFormulasDatos.Objeto && objetos.TryGetValue(c.NUM_ELEMENTO, out var o) && PropiedadesObjeto.TryGetValue(prop, out info))
                donde = $"«{info.Nombre}» {De(NombreObjeto(o))} en {Banda(o.NUM_BANDA)}";
            else
            {
                res.Add($"Hay una condición ({prop}) de una sección u objeto que ya no existe o que no la admite.");
                continue;
            }
            foreach (var e in Comprobar(c.DES_FORMULA, esquema, null, elem, prop, out _))
                res.Add($"La condición {donde}, línea {e.Linea}, columna {e.Columna}: {e.Mensaje}");
        }
        return res;
    }

    private static string Corto(string s) => s.Length <= 40 ? s : s[..40] + "…";

    /// <summary>"el detalle" → "del detalle"; "la fórmula X" → "de la fórmula X".</summary>
    private static string De(string s) => s.StartsWith("el ", StringComparison.Ordinal) ? "del " + s[3..] : "de " + s;

    public static string NombreSeccion(ReporteLienzoBanda b)
    {
        var nombre = b.COD_TIPO_BANDA switch
        {
            "RH" => "el encabezado del informe", "PH" => "el encabezado de página", "GH" => $"el encabezado del grupo {b.NUM_NIVEL}",
            "DE" => "el detalle", "GF" => $"el pie del grupo {b.NUM_NIVEL}", "RF" => "el pie del informe", "PF" => "el pie de página",
            _ => "una sección"
        };
        return b.NUM_SUBSECCION > 1 ? $"{nombre} ({(char)('a' + b.NUM_SUBSECCION - 1)})" : nombre;
    }

    public static string NombreObjeto(ReporteLienzoObjeto o) => o.COD_TIPO_OBJETO switch
    {
        "CAM" => $"el campo {o.DES_ORIGEN}",
        "FOR" => $"la fórmula {o.DES_ORIGEN}",
        "RES" => $"el resumen de {o.DES_ORIGEN}",
        "ESP" => $"el especial {o.DES_ORIGEN}",
        "TXT" => $"el texto «{Corto(o.DES_ORIGEN ?? "")}»",
        "LIN" => "una línea",
        "CUA" => "un cuadro",
        "IMG" => "una imagen",
        "TAB" => $"la tabla del resultado {o.NUM_RESULTADO}",
        "BAR" => $"el código «{Corto(o.DES_ORIGEN ?? "")}»",
        "CRU" => $"la tabla cruzada del resultado {o.NUM_RESULTADO}",
        "SUB" => "el subinforme",
        "GRA" => $"el gráfico del resultado {o.NUM_RESULTADO}",
        _     => "un objeto"
    };

    // ── Columnas calculadas (Excel, CSV y plantillas) ───────────────────────────
    /// <summary>El nombre de la columna que lleva el valor de una fórmula: el de la fórmula, o F_ y el nombre si el SP ya
    /// trae una columna con ese nombre. En una plantilla propia se usa como {{R1.Nombre}}.</summary>
    public static string ColumnaCalculada(ReporteTabla? tabla, IEnumerable<ReporteFuenteColumna> columnasFuente, string nombre)
    {
        var choca = (tabla is not null && tabla.Indice(nombre) >= 0)
                    || columnasFuente.Any(c => c.NUM_RESULTADO == (tabla?.Numero ?? 1) && string.Equals(c.DES_COLUMNA, nombre, StringComparison.OrdinalIgnoreCase));
        return choca ? "F_" + nombre : nombre;
    }

    /// <summary>¿La columna es una calculada del diseño (para no avisar en la plantilla que la fuente no la tiene)?</summary>
    public static bool EsColumnaCalculada(ReporteDefinicion def, string columna)
        => def.Lienzo?.Formulas.Any(f => string.Equals(f.DES_NOMBRE, columna, StringComparison.OrdinalIgnoreCase)
                                        || string.Equals("F_" + f.DES_NOMBRE, columna, StringComparison.OrdinalIgnoreCase)) ?? false;

    /// <summary>Los datos del resultado 1 como los ve el PDF: con una columna más por cada fórmula pedida, calculada fila
    /// por fila (con los grupos del lienzo, en el orden en que se imprimen), y sin las filas cuyo detalle no se imprime
    /// por una condición. Devuelve qué columna quedó para cada fórmula.</summary>
    public static ReporteDatos Calcular(ReporteContexto ctx, IEnumerable<string> formulas, out Dictionary<string, string> columnas)
    {
        columnas = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var l = ctx.Definicion.Lienzo;
        var tabla = ctx.Datos.Tabla(1) ?? ctx.Datos.Tablas.FirstOrDefault();
        var pedidas = formulas.Select(n => n.Trim()).Where(n => l?.Formulas.Any(f => string.Equals(f.DES_NOMBRE, n, StringComparison.OrdinalIgnoreCase)) == true)
                              .Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        foreach (var n in pedidas) columnas[n] = ColumnaCalculada(tabla, ctx.Definicion.Columnas, n);
        if (tabla is null || l is null) return ctx.Datos;
        var detalles = l.Bandas.Where(b => b.COD_TIPO_BANDA == "DE").ToList();
        var filtrar = detalles.Any(b => l.Condiciones.Any(c => c.COD_ELEMENTO == "B" && c.NUM_ELEMENTO == b.NUM_BANDA
                                                              && string.Equals(c.COD_PROPIEDAD, "SUPRIMIR", StringComparison.OrdinalIgnoreCase)));
        if (pedidas.Count == 0 && !filtrar) return ctx.Datos;

        var grupos = l.Grupos.OrderBy(g => g.NUM_NIVEL).Where(g => tabla.Indice(g.DES_COLUMNA) >= 0).ToList();
        var ordenadas = ReporteListado.Ordenar(tabla.Filas, tabla, ReporteBandasListado.Claves(ctx.Definicion, tabla));
        var rangos = ReporteFormulasDatos.Rangos(ctx, tabla, ordenadas, grupos);
        var datos = new ReporteFormulasDatos(ctx, tabla, ordenadas, l);
        var valores = new Dictionary<object?[], object?[]>(ReferenceEqualityComparer.Instance);
        var fuera = new HashSet<object?[]>(ReferenceEqualityComparer.Instance);
        for (var i = 0; i < ordenadas.Count; i++)
        {
            var lugar = new ReporteFormulaLugar { Fila = i, Grupos = rangos[i] };
            // Una fila sale si al menos uno de sus detalles se imprime.
            if (filtrar && detalles.All(b => datos.Logico(ReporteFormulasDatos.Seccion, b.NUM_BANDA, "SUPRIMIR", lugar) ?? b.FLG_SUPRIMIR))
            {
                fuera.Add(ordenadas[i]);
                continue;
            }
            valores[ordenadas[i]] = pedidas.Select(n => datos.Formula(n, lugar)).ToArray();
        }

        var nueva = new ReporteTabla { Numero = tabla.Numero };
        nueva.Columnas.AddRange(tabla.Columnas);
        for (var k = 0; k < pedidas.Count; k++)
        {
            var ejemplo = valores.Values.Select(v => v[k]).FirstOrDefault(v => v is not null);
            var (tipo, sql) = ejemplo switch
            {
                decimal  => (typeof(decimal), "decimal"),
                DateTime => (typeof(DateTime), "datetime"),
                bool     => (typeof(bool), "bit"),
                _        => (typeof(string), "nvarchar")
            };
            nueva.Columnas.Add(new ReporteColumnaDato { Nombre = columnas[pedidas[k]], Tipo = tipo, TipoSql = sql });
        }
        // Las filas en su orden original (el listado las vuelve a ordenar igual), con los valores al final.
        foreach (var f in tabla.Filas.Where(f => !fuera.Contains(f)))
        {
            var extra = valores.TryGetValue(f, out var v) ? v : new object?[pedidas.Count];
            var fila = new object?[tabla.Columnas.Count + pedidas.Count];
            Array.Copy(f, fila, Math.Min(f.Length, tabla.Columnas.Count));
            Array.Copy(extra, 0, fila, tabla.Columnas.Count, pedidas.Count);
            nueva.Filas.Add(fila);
        }
        var res = new ReporteDatos();
        res.Tablas.AddRange(ctx.Datos.Tablas.Select(t => ReferenceEquals(t, tabla) ? nueva : t));
        return res;
    }
}
