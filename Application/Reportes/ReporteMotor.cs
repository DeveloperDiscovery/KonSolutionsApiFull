using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using KonSolutionsApi.Domain.Models.Reportes;

namespace KonSolutionsApi.Application.Reportes;

// Motor del reporteador: lo que comparten los cuatro formatos. No toca la base: recibe la definición
// (PROC_REPORTES_DEFINICION) y los datos que devolvió el SP de la fuente, y arma con eso el listado
// (grupos, subtotales, total) o la ficha (secciones). Cada formato solo dibuja lo que se arma acá, así
// el PDF, el Word, el Excel y el CSV de un mismo diseño dicen siempre lo mismo.

// ── Datos ───────────────────────────────────────────────────────────────────────

/// <summary>Un error del reporte cuyo mensaje es para el usuario (filtros que faltan, demasiadas filas...): no va al
/// log de errores.</summary>
public sealed class ReporteLimiteException : Exception
{
    public ReporteLimiteException(string mensaje) : base(mensaje) { }
}

/// <summary>Una columna tal como la devolvió el SP.</summary>
public sealed class ReporteColumnaDato
{
    public string Nombre  { get; init; } = "";
    public Type   Tipo    { get; init; } = typeof(string);
    public string TipoSql { get; init; } = "";
}

/// <summary>Un resultado del SP (el primer SELECT es el 1).</summary>
public sealed class ReporteTabla
{
    public int                      Numero   { get; init; }
    public List<ReporteColumnaDato> Columnas { get; } = new();
    public List<object?[]>          Filas    { get; } = new();

    private Dictionary<string, int>? _indice;

    /// <summary>Posición de la columna, sin importar mayúsculas; -1 si el SP no la trajo.</summary>
    public int Indice(string columna)
    {
        _indice ??= Columnas.Select((c, i) => (c.Nombre, i))
                            .GroupBy(x => x.Nombre, StringComparer.OrdinalIgnoreCase)
                            .ToDictionary(g => g.Key, g => g.First().i, StringComparer.OrdinalIgnoreCase);
        return _indice.TryGetValue(columna, out var i) ? i : -1;
    }

    public object? Valor(object?[] fila, string columna)
    {
        var i = Indice(columna);
        return i < 0 || i >= fila.Length ? null : fila[i];
    }
}

/// <summary>Todos los resultados del SP.</summary>
public sealed class ReporteDatos
{
    public List<ReporteTabla> Tablas { get; } = new();
    public ReporteTabla? Tabla(int numero) => Tablas.FirstOrDefault(t => t.Numero == numero);
    public int TotalFilas => Tablas.Sum(t => t.Filas.Count);
}

/// <summary>El valor con que se ejecutó un parámetro y cómo se muestra (el texto del combo, la fecha...).</summary>
public sealed class ReporteValorParametro
{
    public ReporteFuenteParametro Parametro { get; init; } = new();
    public object?                Valor     { get; set; }
    public string?                Texto     { get; set; }
    public string Campo => ReporteTexto.SinArroba(Parametro.DES_PARAMETRO);
}

/// <summary>Todo lo que un formato necesita para dibujar.</summary>
public sealed class ReporteContexto
{
    public ReporteDefinicion           Definicion    { get; init; } = new();
    public ReporteDatos                Datos         { get; init; } = new();
    public List<ReporteValorParametro> Valores       { get; init; } = new();
    public ReporteEmpresa?             Empresa       { get; init; }
    public string                      Usuario       { get; init; } = "";
    public DateTime                    Fecha         { get; init; } = DateTime.Now;
    /// <summary>La plantilla propia del formato que se pide (Word o Excel), si la hay.</summary>
    public byte[]?                     Plantilla     { get; init; }
    /// <summary>Generar la plantilla base: en vez de datos, las marcas {{R1.COLUMNA}}.</summary>
    public bool                        ModoPlantilla { get; init; }
    /// <summary>Diseño libre: los subinformes, por número de diseño, con lo que ya se ejecutó (lo llena el servicio).</summary>
    public Dictionary<int, ReporteSubinforme> Subinformes { get; } = new();

    public ReporteDefinicionCabecera D => Definicion.Cabecera;
    public bool EsListado => !string.Equals(D.COD_TIPO_DISENO, "FIC", StringComparison.OrdinalIgnoreCase);
}

/// <summary>Un diseño libre que se imprime dentro de otro: su definición y, por cada combinación de valores con que se
/// ejecutó (la clave del pedido), los datos o el error.</summary>
public sealed class ReporteSubinforme
{
    public ReporteDefinicion Definicion { get; set; } = new();
    /// <summary>Lo que impide usarlo (no es un diseño libre, su procedimiento ya no existe...): sale en su lugar.</summary>
    public string?           Error      { get; set; }
    public Dictionary<string, ReporteDatos>                Datos   { get; } = new(StringComparer.Ordinal);
    public Dictionary<string, List<ReporteValorParametro>> Valores { get; } = new(StringComparer.Ordinal);
    public Dictionary<string, string>                      Errores { get; } = new(StringComparer.Ordinal);
}

/// <summary>Una vez que hay que ejecutar un subinforme: con qué valores. La clave junta los valores (los de usuario, rol y
/// estación los pone el servicio y no cuentan): dos pedidos con la misma clave se ejecutan una sola vez.</summary>
public sealed class ReporteSubPedido
{
    public int                         NumDiseno { get; init; }
    public string                      Clave     { get; init; } = "";
    public List<ReporteValorParametro> Valores   { get; init; } = new();
    /// <summary>Un valor obligatorio que falta o que no se puede convertir: no se ejecuta y sale el aviso.</summary>
    public string?                     Error     { get; init; }
}

/// <summary>Una columna lista para dibujar: lo del diseño, completado con lo de la fuente y el tipo real.</summary>
public sealed class ReporteColumnaVista
{
    public int     Resultado      { get; init; }
    public string  Nombre         { get; init; } = "";
    public string  Etiqueta       { get; init; } = "";
    public double  Ancho          { get; init; } = 10;
    public char    Alineacion     { get; init; } = 'I';
    public string? Formato        { get; init; }
    public bool    Totaliza       { get; init; }
    public int     NivelGrupo     { get; init; }
    public bool    Visible        { get; init; } = true;
    public Type    Tipo           { get; init; } = typeof(string);
    public string? OrdenDir       { get; init; }
    public int     OrdenPrioridad { get; init; } = 99;
    public int     Orden          { get; init; }

    public bool EsNumero => ReporteFormato.EsNumerico(Tipo) && !ReporteFormato.EsLetras(Formato);
    public bool EsFecha  => ReporteFormato.EsFecha(Tipo);
}

// ── Columnas del diseño ────────────────────────────────────────────────────────

public static class ReporteColumnas
{
    /// <summary>Las columnas de un resultado según el diseño. Sin elementos para ese resultado, todas las de la
    /// fuente (o las que trajo el SP). Una columna del diseño que el SP ya no trae se salta (salvo al armar la
    /// plantilla base, que no tiene datos).</summary>
    public static List<ReporteColumnaVista> De(ReporteContexto ctx, int resultado, bool incluirOcultas)
    {
        var tabla = ctx.Datos.Tabla(resultado);
        var fuente = ctx.Definicion.Columnas.Where(c => c.NUM_RESULTADO == resultado)
                                            .OrderBy(c => c.NUM_ORDEN ?? 0).ToList();
        var elementos = ctx.Definicion.Elementos.Where(e => e.NUM_RESULTADO == resultado)
                                                .OrderBy(e => e.NUM_ORDEN).ToList();
        var esCampos = ctx.Definicion.Secciones.Any(s => s.NUM_RESULTADO == resultado && s.COD_PRESENTACION == "CAM");

        if (elementos.Count == 0)
        {
            var nombres = fuente.Count > 0 ? fuente.Select(c => c.DES_COLUMNA).ToList()
                        : tabla?.Columnas.Select(c => c.Nombre).ToList() ?? new();
            elementos = nombres.Select((n, i) => new ReporteDisenoElemento
            {
                NUM_RESULTADO = resultado, DES_COLUMNA = n, NUM_ORDEN = i + 1, FLG_VISIBLE = true
            }).ToList();
        }

        var lista = new List<ReporteColumnaVista>();
        foreach (var e in elementos)
        {
            if (!incluirOcultas && !e.FLG_VISIBLE) continue;
            if (tabla is not null && !ctx.ModoPlantilla && tabla.Indice(e.DES_COLUMNA) < 0) continue;
            var f = fuente.FirstOrDefault(c => string.Equals(c.DES_COLUMNA, e.DES_COLUMNA, StringComparison.OrdinalIgnoreCase));
            var tipo = tabla is not null && tabla.Indice(e.DES_COLUMNA) is var i and >= 0
                ? tabla.Columnas[i].Tipo
                : ReporteFormato.TipoDesdeSql(f?.DES_TIPO_DATO);
            var formato = Vacio(e.DES_FORMATO) ?? Vacio(f?.DES_FORMATO);
            var alin = (Vacio(e.COD_ALINEACION) ?? Vacio(f?.COD_ALINEACION))?.ToUpperInvariant() is { Length: > 0 } a
                ? a[0]
                : ReporteFormato.EsNumerico(tipo) && !ReporteFormato.EsLetras(formato) ? 'D'
                : ReporteFormato.EsFecha(tipo) ? 'C' : 'I';
            lista.Add(new ReporteColumnaVista
            {
                Resultado      = resultado,
                Nombre         = e.DES_COLUMNA,
                Etiqueta       = Vacio(e.DES_ETIQUETA) ?? Vacio(f?.DES_ETIQUETA) ?? ReporteTexto.EtiquetaDesdeNombre(e.DES_COLUMNA),
                Ancho          = e.NUM_ANCHO is > 0 ? e.NUM_ANCHO.Value : (esCampos ? 1 : 10),
                Alineacion     = alin is 'I' or 'C' or 'D' ? alin : 'I',
                Formato        = formato,
                Totaliza       = e.FLG_TOTALIZA,
                NivelGrupo     = e.NUM_NIVEL_GRUPO,
                Visible        = e.FLG_VISIBLE,
                Tipo           = tipo,
                OrdenDir       = Vacio(e.COD_ORDEN)?.ToUpperInvariant(),
                OrdenPrioridad = e.NUM_PRIORIDAD_ORDEN ?? 99,
                Orden          = e.NUM_ORDEN
            });
        }
        return lista;
    }

    /// <summary>La columna (visible u oculta) de un resultado, para formatear una marca; null si el diseño no la usa.</summary>
    public static ReporteColumnaVista? Buscar(ReporteContexto ctx, int resultado, string columna)
        => De(ctx, resultado, incluirOcultas: true)
           .FirstOrDefault(c => string.Equals(c.Nombre, columna, StringComparison.OrdinalIgnoreCase))
        ?? ColumnaSuelta(ctx, resultado, columna);

    /// <summary>Una columna que el diseño no usa pero el SP sí trae (para marcas de plantilla y textos).</summary>
    private static ReporteColumnaVista? ColumnaSuelta(ReporteContexto ctx, int resultado, string columna)
    {
        var tabla = ctx.Datos.Tabla(resultado);
        var f = ctx.Definicion.Columnas.FirstOrDefault(c => c.NUM_RESULTADO == resultado
                    && string.Equals(c.DES_COLUMNA, columna, StringComparison.OrdinalIgnoreCase));
        var i = tabla?.Indice(columna) ?? -1;
        if (f is null && i < 0) return null;
        var tipo = i >= 0 ? tabla!.Columnas[i].Tipo : ReporteFormato.TipoDesdeSql(f?.DES_TIPO_DATO);
        return new ReporteColumnaVista
        {
            Resultado = resultado, Nombre = columna, Visible = false, Tipo = tipo,
            Etiqueta  = Vacio(f?.DES_ETIQUETA) ?? ReporteTexto.EtiquetaDesdeNombre(columna),
            Formato   = Vacio(f?.DES_FORMATO)
        };
    }

    internal static string? Vacio(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();
}

// ── Formatos de valores ─────────────────────────────────────────────────────────

public static class ReporteFormato
{
    /// <summary>Números con punto decimal y coma de miles (1,234.56), como se usan en el Perú.</summary>
    public static readonly CultureInfo Cultura = CultureInfo.InvariantCulture;

    public static bool EsLetras(string? formato) => string.Equals(formato?.Trim(), "LETRAS", StringComparison.OrdinalIgnoreCase);

    public static bool EsNumerico(Type t)
    {
        t = Nullable.GetUnderlyingType(t) ?? t;
        return t == typeof(decimal) || t == typeof(double) || t == typeof(float)
            || t == typeof(int) || t == typeof(long) || t == typeof(short) || t == typeof(byte)
            || t == typeof(sbyte) || t == typeof(uint) || t == typeof(ulong) || t == typeof(ushort);
    }

    public static bool EsEntero(Type t)
    {
        t = Nullable.GetUnderlyingType(t) ?? t;
        return t == typeof(int) || t == typeof(long) || t == typeof(short) || t == typeof(byte)
            || t == typeof(sbyte) || t == typeof(uint) || t == typeof(ulong) || t == typeof(ushort);
    }

    public static bool EsFecha(Type t)
    {
        t = Nullable.GetUnderlyingType(t) ?? t;
        return t == typeof(DateTime) || t == typeof(DateTimeOffset) || t == typeof(DateOnly);
    }

    /// <summary>El tipo .NET de un tipo de SQL Server (para la plantilla base, que no tiene datos).</summary>
    public static Type TipoDesdeSql(string? sql) => (sql ?? "").Trim().ToLowerInvariant() switch
    {
        "date" or "datetime" or "datetime2" or "smalldatetime" => typeof(DateTime),
        "datetimeoffset"                                       => typeof(DateTimeOffset),
        "int"                                                  => typeof(int),
        "smallint"                                             => typeof(short),
        "tinyint"                                              => typeof(byte),
        "bigint"                                               => typeof(long),
        "decimal" or "numeric" or "money" or "smallmoney"      => typeof(decimal),
        "float"                                                => typeof(double),
        "real"                                                 => typeof(float),
        "bit"                                                  => typeof(bool),
        _                                                      => typeof(string)
    };

    /// <summary>El formato que se usa cuando ni el diseño ni la fuente dicen uno.</summary>
    public static string? Defecto(Type tipo, object? valor)
    {
        if (EsFecha(tipo) || valor is DateTime)
            return valor is DateTime d && d.TimeOfDay != TimeSpan.Zero ? "dd/MM/yyyy HH:mm" : "dd/MM/yyyy";
        if (EsEntero(tipo)) return "0";
        if (EsNumerico(tipo)) return "#,##0.00";
        return null;
    }

    /// <summary>El valor como texto, con el formato de la columna. LETRAS escribe el número en letras y le agrega
    /// la moneda de la fila (columna DES_MONEDA) si la hay.</summary>
    public static string Texto(object? valor, string? formato, Type tipo, Func<string, object?>? campo = null)
    {
        if (valor is null || valor is DBNull) return "";
        if (EsLetras(formato))
        {
            if (!ADecimal(valor, out var n)) return Convert.ToString(valor, Cultura) ?? "";
            var moneda = campo?.Invoke("DES_MONEDA") as string;
            return (ReporteLetras.Numero(n) + (string.IsNullOrWhiteSpace(moneda) ? "" : " " + moneda.Trim().ToUpperInvariant())).Trim();
        }
        var fmt = string.IsNullOrWhiteSpace(formato) ? Defecto(tipo, valor) : formato.Trim();
        try
        {
            return valor switch
            {
                bool b             => b ? "Sí" : "No",
                DateTime d         => d.ToString(fmt ?? "dd/MM/yyyy", Cultura),
                DateTimeOffset o   => o.ToString(fmt ?? "dd/MM/yyyy HH:mm", Cultura),
                DateOnly x         => x.ToString(fmt ?? "dd/MM/yyyy", Cultura),
                TimeSpan t         => t.ToString(@"hh\:mm", Cultura),
                byte[]             => "",
                string s           => s.TrimEnd(),
                IFormattable f     => f.ToString(fmt, Cultura),
                _                  => Convert.ToString(valor, Cultura) ?? ""
            };
        }
        catch (FormatException)
        {
            return Convert.ToString(valor, Cultura) ?? "";
        }
    }

    public static bool ADecimal(object? v, out decimal n)
    {
        n = 0;
        if (v is null || v is DBNull) return false;
        try
        {
            switch (v)
            {
                case decimal d: n = d; return true;
                case double x when double.IsFinite(x): n = (decimal)x; return true;
                case float x when float.IsFinite(x): n = (decimal)x; return true;
                case int or long or short or byte or sbyte or uint or ulong or ushort:
                    n = Convert.ToDecimal(v, Cultura); return true;
                case string s: return decimal.TryParse(s, NumberStyles.Number, Cultura, out n);
                default: return false;
            }
        }
        catch (OverflowException) { return false; }
    }

    /// <summary>El formato numérico/fecha de .NET traducido al de Excel (dd/MM/yyyy → dd/mm/yyyy).</summary>
    public static string? Excel(string? formato, ReporteColumnaVista c, object? ejemplo = null)
    {
        if (EsLetras(formato)) return null;
        var f = string.IsNullOrWhiteSpace(formato) ? Defecto(c.Tipo, ejemplo) : formato.Trim();
        if (f is null) return null;
        if (c.EsFecha || ejemplo is DateTime)
            return f.Replace("MM", "\u0001").Replace("mm", "\u0002").Replace("\u0001", "mm").Replace("\u0002", "mm")
                    .Replace("HH", "hh").Replace("tt", "AM/PM");
        return f;
    }
}

// ── Textos y marcas ─────────────────────────────────────────────────────────────

public static class ReporteTexto
{
    private static readonly Regex MarcaSimple = new(@"\{(@?[A-Za-z0-9_]+)\}", RegexOptions.Compiled);

    public static string SinArroba(string? nombre) => (nombre ?? "").Trim().TrimStart('@');

    /// <summary>"NUM_PEDIDO_COMERCIAL" → "N° pedido comercial". Para columnas sin etiqueta en el diseño ni en el diccionario.</summary>
    public static string EtiquetaDesdeNombre(string? nombre)
    {
        var n = SinArroba(nombre).ToUpperInvariant();
        if (n.Length == 0) return "";
        (string pre, string texto)[] prefijos =
        {
            ("FEC_", "Fecha "), ("CAN_", "Cantidad "), ("IMP_", "Importe "), ("PRE_", "Precio "), ("TAS_", "Tasa "),
            ("NUM_", "N° "), ("COD_", "Código "), ("VAL_", "Valor "), ("POR_", "% "), ("DES_", ""), ("FLG_", "")
        };
        var inicio = "";
        foreach (var (pre, texto) in prefijos)
            if (n.StartsWith(pre, StringComparison.Ordinal) && n.Length > pre.Length) { inicio = texto; n = n[pre.Length..]; break; }
        var resto = n.Replace('_', ' ').ToLowerInvariant();
        var t = (inicio + resto).Trim();
        return t.Length == 0 ? "" : char.ToUpperInvariant(t[0]) + t[1..];
    }

    /// <summary>Reemplaza en un texto del diseño (título, subtítulo, pie) {@PARAMETRO} por su valor y {COLUMNA} por
    /// el valor de la primera fila del resultado 1.</summary>
    public static string Reemplazar(string? texto, ReporteContexto ctx)
    {
        if (string.IsNullOrEmpty(texto)) return "";
        return MarcaSimple.Replace(texto, m =>
        {
            var clave = m.Groups[1].Value;
            if (clave.StartsWith('@'))
            {
                var v = ctx.Valores.FirstOrDefault(x => string.Equals(x.Campo, clave[1..], StringComparison.OrdinalIgnoreCase));
                return v is null ? m.Value : (v.Texto ?? "");
            }
            if (ctx.ModoPlantilla) return "{{R1." + clave + "}}";
            var tabla = ctx.Datos.Tabla(1);
            if (tabla is null || tabla.Indice(clave) < 0) return m.Value;
            if (tabla.Filas.Count == 0) return "";
            var col = ReporteColumnas.Buscar(ctx, 1, clave);
            var fila = tabla.Filas[0];
            return ReporteFormato.Texto(tabla.Valor(fila, clave), col?.Formato, col?.Tipo ?? typeof(string), c => tabla.Valor(fila, c));
        });
    }

    /// <summary>"Desde: 01/10/2026 · Hasta: 10/10/2026 · Proveedor: TEXTILES..." con los filtros que se usaron
    /// (los visibles que tienen valor).</summary>
    public static string Filtros(ReporteContexto ctx)
    {
        var partes = ctx.Valores
            .Where(v => !v.Parametro.FLG_SISTEMA && v.Parametro.FLG_VISIBLE && !string.IsNullOrWhiteSpace(v.Texto))
            .Select(v => $"{(string.IsNullOrWhiteSpace(v.Parametro.DES_ETIQUETA) ? EtiquetaDesdeNombre(v.Parametro.DES_PARAMETRO) : v.Parametro.DES_ETIQUETA)}: {v.Texto}");
        return string.Join(" · ", partes);
    }

    public static string Generado(ReporteContexto ctx)
    {
        var quien = string.IsNullOrWhiteSpace(ctx.Empresa?.DES_USUARIO) ? ctx.Usuario : ctx.Empresa!.DES_USUARIO!;
        return $"Generado por {quien} el {ctx.Fecha.ToString("dd/MM/yyyy HH:mm", ReporteFormato.Cultura)}";
    }

    /// <summary>Un nombre de archivo sin caracteres que Windows no acepta.</summary>
    public static string NombreArchivo(string? baseNombre, string extension)
    {
        var limpio = new string((baseNombre ?? "reporte").Select(ch => Path.GetInvalidFileNameChars().Contains(ch) || ch is '/' or '\\' or ':' ? '_' : ch).ToArray()).Trim();
        if (limpio.Length == 0) limpio = "reporte";
        if (limpio.Length > 80) limpio = limpio[..80].Trim();
        return $"{limpio}.{extension}";
    }
}

// ── Número en letras ────────────────────────────────────────────────────────────

public static class ReporteLetras
{
    private static readonly string[] Unidades = { "", "UNO", "DOS", "TRES", "CUATRO", "CINCO", "SEIS", "SIETE", "OCHO", "NUEVE",
        "DIEZ", "ONCE", "DOCE", "TRECE", "CATORCE", "QUINCE", "DIECISÉIS", "DIECISIETE", "DIECIOCHO", "DIECINUEVE",
        "VEINTE", "VEINTIUNO", "VEINTIDÓS", "VEINTITRÉS", "VEINTICUATRO", "VEINTICINCO", "VEINTISÉIS", "VEINTISIETE", "VEINTIOCHO", "VEINTINUEVE" };
    private static readonly string[] Decenas = { "", "", "", "TREINTA", "CUARENTA", "CINCUENTA", "SESENTA", "SETENTA", "OCHENTA", "NOVENTA" };
    private static readonly string[] Centenas = { "", "CIENTO", "DOSCIENTOS", "TRESCIENTOS", "CUATROCIENTOS", "QUINIENTOS",
        "SEISCIENTOS", "SETECIENTOS", "OCHOCIENTOS", "NOVECIENTOS" };

    /// <summary>6656.5 → "SEIS MIL SEISCIENTOS CINCUENTA Y SEIS CON 50/100". Negativos con "MENOS".</summary>
    public static string Numero(decimal n)
    {
        var negativo = n < 0;
        n = Math.Abs(Math.Round(n, 2, MidpointRounding.AwayFromZero));
        var entero = (long)Math.Truncate(n);
        var centimos = (int)Math.Round((n - entero) * 100, MidpointRounding.AwayFromZero);
        var texto = entero == 0 ? "CERO" : Entero(entero, final: true);
        return $"{(negativo ? "MENOS " : "")}{texto} CON {centimos:00}/100";
    }

    private static string Entero(long n, bool final)
    {
        if (n >= 1_000_000_000_000) return n.ToString(ReporteFormato.Cultura);
        var partes = new List<string>();
        var millones = n / 1_000_000;
        var miles = n / 1_000 % 1_000;
        var resto = n % 1_000;
        if (millones > 0)
            partes.Add(millones == 1 ? "UN MILLÓN" : Entero(millones, final: false) + " MILLONES");
        if (miles > 0)
            partes.Add(miles == 1 ? "MIL" : Cientos((int)miles, final: false) + " MIL");
        if (resto > 0)
            partes.Add(Cientos((int)resto, final));
        return string.Join(" ", partes);
    }

    /// <summary>De 1 a 999. "final": si es la última parte del número (UNO); si no, se apocopa (UN, VEINTIÚN).</summary>
    private static string Cientos(int n, bool final)
    {
        if (n == 100) return "CIEN";
        var c = n / 100;
        var r = n % 100;
        var partes = new List<string>();
        if (c > 0) partes.Add(Centenas[c]);
        if (r > 0)
        {
            string t;
            if (r < 30) t = Unidades[r];
            else t = Decenas[r / 10] + (r % 10 > 0 ? " Y " + Unidades[r % 10] : "");
            if (!final)
            {
                if (t.EndsWith("VEINTIUNO", StringComparison.Ordinal)) t = t[..^9] + "VEINTIÚN";
                else if (t.EndsWith("UNO", StringComparison.Ordinal)) t = t[..^3] + "UN";
            }
            partes.Add(t);
        }
        return string.Join(" ", partes);
    }
}

// ── Valores de los parámetros ──────────────────────────────────────────────────

public static class ReporteValores
{
    private static readonly string[] FormatosFecha =
    {
        "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-ddTHH:mm", "yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd HH:mm",
        "yyyy-MM-ddTHH:mm:ss.fff", "yyyy-MM-ddTHH:mm:ss.fffffff", "dd/MM/yyyy", "dd/MM/yyyy HH:mm", "dd/MM/yyyy HH:mm:ss"
    };

    /// <summary>El texto que llega del navegador convertido al tipo del parámetro. Vacío = null (NULL = todos).
    /// Lanza FormatException con un mensaje para el usuario si no se puede convertir.</summary>
    public static object? Convertir(string? texto, string? tipoSql, string etiqueta)
    {
        if (string.IsNullOrWhiteSpace(texto)) return null;
        var t = texto.Trim();
        var tipo = (tipoSql ?? "").Trim().ToLowerInvariant();
        switch (tipo)
        {
            case "date":
            case "datetime":
            case "datetime2":
            case "smalldatetime":
            case "datetimeoffset":
                if (DateTime.TryParseExact(t, FormatosFecha, ReporteFormato.Cultura, DateTimeStyles.AllowWhiteSpaces, out var f)
                    || DateTime.TryParse(t, ReporteFormato.Cultura, DateTimeStyles.AllowWhiteSpaces, out f))
                    return tipo == "date" ? f.Date : f;
                throw new FormatException($"«{t}» no es una fecha válida para {etiqueta}.");
            case "int":
            case "smallint":
            case "tinyint":
            case "bigint":
                if (!long.TryParse(t, NumberStyles.Integer, ReporteFormato.Cultura, out var l))
                {
                    // "123.0" que llega de un campo decimal de la fila
                    if (decimal.TryParse(t, NumberStyles.Number, ReporteFormato.Cultura, out var dd) && dd == Math.Truncate(dd)) l = (long)dd;
                    else throw new FormatException($"«{t}» no es un número entero válido para {etiqueta}.");
                }
                try
                {
                    return tipo switch { "int" => checked((int)l), "smallint" => checked((short)l), "tinyint" => checked((byte)l), _ => l };
                }
                catch (OverflowException) { throw new FormatException($"«{t}» está fuera del rango permitido para {etiqueta}."); }
            case "decimal":
            case "numeric":
            case "money":
            case "smallmoney":
                if (decimal.TryParse(t, NumberStyles.Number, ReporteFormato.Cultura, out var d)) return d;
                throw new FormatException($"«{t}» no es un número válido para {etiqueta} (usa punto decimal).");
            case "float":
            case "real":
                if (double.TryParse(t, NumberStyles.Float | NumberStyles.AllowThousands, ReporteFormato.Cultura, out var x)) return x;
                throw new FormatException($"«{t}» no es un número válido para {etiqueta}.");
            case "bit":
                return t.ToLowerInvariant() switch
                {
                    "1" or "true" or "si" or "sí" or "s" or "yes" => true,
                    "0" or "false" or "no" or "n"                 => false,
                    _ => throw new FormatException($"«{t}» no es Sí/No para {etiqueta}.")
                };
            case "uniqueidentifier":
                if (Guid.TryParse(t, out var g)) return g;
                throw new FormatException($"«{t}» no es un identificador válido para {etiqueta}.");
            default:
                return t;
        }
    }

    /// <summary>El valor por defecto de un parámetro de fecha (HOY, INIMES...) o el fijo (FIJO), como texto ISO.</summary>
    public static string? Defecto(string? codigo, string? valorFijo, DateTime hoy)
    {
        hoy = hoy.Date;
        var inicioMes = new DateTime(hoy.Year, hoy.Month, 1);
        DateTime? f = (codigo ?? "").Trim().ToUpperInvariant() switch
        {
            "HOY"       => hoy,
            "AYER"      => hoy.AddDays(-1),
            "INIMES"    => inicioMes,
            "FINMES"    => inicioMes.AddMonths(1).AddDays(-1),
            "INIMESANT" => inicioMes.AddMonths(-1),
            "FINMESANT" => inicioMes.AddDays(-1),
            "INIANO"    => new DateTime(hoy.Year, 1, 1),
            "HACE7"     => hoy.AddDays(-7),
            "HACE30"    => hoy.AddDays(-30),
            _           => null
        };
        if (f is not null) return f.Value.ToString("yyyy-MM-dd", ReporteFormato.Cultura);
        return string.Equals(codigo, "FIJO", StringComparison.OrdinalIgnoreCase) ? valorFijo : null;
    }

    /// <summary>Lo que se ve de un valor en el pie de filtros: el texto que mandó el navegador (el del combo) o el
    /// valor formateado.</summary>
    public static string? TextoVisible(object? valor, string? textoEnviado, ReporteFuenteParametro p)
    {
        if (!string.IsNullOrWhiteSpace(textoEnviado)) return textoEnviado.Trim();
        if (valor is null) return null;
        if (valor is bool b) return b ? "Sí" : "No";
        return ReporteFormato.Texto(valor, null, valor.GetType());
    }
}

// ── Listado: filas con grupos, subtotales y total ──────────────────────────────

public enum ReporteFilaTipo { GrupoCabecera, Detalle, GrupoPie, TotalGeneral }

public sealed class ReporteFilaListado
{
    public ReporteFilaTipo             Tipo    { get; init; }
    public int                         Nivel   { get; init; }
    public string                      Texto   { get; init; } = "";
    public object?[]?                  Fila    { get; init; }
    public Dictionary<string, decimal> Totales { get; init; } = new(StringComparer.OrdinalIgnoreCase);
    public int                         Cuenta  { get; init; }
}

public sealed class ReporteListado
{
    public ReporteTabla?               Tabla     { get; init; }
    public List<ReporteColumnaVista>   Columnas  { get; init; } = new();
    public List<ReporteColumnaVista>   Grupos    { get; init; } = new();
    public List<ReporteFilaListado>    Filas     { get; init; } = new();
    public List<object?[]>             Ordenadas { get; init; } = new();
    public bool                        HayTotales => Columnas.Any(c => c.Totaliza && c.EsNumero);

    /// <summary>El listado del resultado 1: ordena (grupos primero) y arma cabeceras de grupo, detalle, subtotales
    /// por grupo y total general según el diseño.</summary>
    public static ReporteListado Armar(ReporteContexto ctx)
    {
        var tabla = ctx.Datos.Tabla(1) ?? ctx.Datos.Tablas.FirstOrDefault();
        var numero = tabla?.Numero ?? 1;
        var todas = ReporteColumnas.De(ctx, numero, incluirOcultas: true);
        var visibles = todas.Where(c => c.Visible).ToList();
        var grupos = todas.Where(c => c.NivelGrupo > 0).OrderBy(c => c.NivelGrupo).ThenBy(c => c.Orden).ToList();
        var totalizan = visibles.Where(c => c.Totaliza && c.EsNumero).ToList();

        var filas = tabla?.Filas ?? new List<object?[]>();
        var claves = grupos.Select(g => (g.Nombre, Desc: g.OrdenDir == "D"))
            .Concat(todas.Where(c => c.NivelGrupo == 0 && c.OrdenDir is "A" or "D")
                         .OrderBy(c => c.OrdenPrioridad).ThenBy(c => c.Orden)
                         .Select(c => (c.Nombre, Desc: c.OrdenDir == "D")))
            .Where(k => tabla is not null && tabla.Indice(k.Nombre) >= 0)
            .ToList();
        var ordenadas = Ordenar(filas, tabla, claves);

        var resultado = new ReporteListado { Tabla = tabla, Columnas = visibles, Grupos = grupos, Ordenadas = ordenadas };
        if (tabla is null) return resultado;

        var conSubtotal = ctx.D.FLG_SUBTOTAL_GRUPO && totalizan.Count > 0;
        var actuales = new string?[grupos.Count];
        var abiertos = new List<(Dictionary<string, decimal> Totales, int Cuenta, string Texto)>();
        var general = totalizan.ToDictionary(c => c.Nombre, _ => 0m, StringComparer.OrdinalIgnoreCase);

        void Cerrar(int desdeNivel)
        {
            for (var n = abiertos.Count - 1; n >= desdeNivel; n--)
            {
                var g = abiertos[n];
                if (conSubtotal)
                    resultado.Filas.Add(new ReporteFilaListado
                    {
                        Tipo = ReporteFilaTipo.GrupoPie, Nivel = n + 1, Texto = $"Total {g.Texto}",
                        Totales = new Dictionary<string, decimal>(g.Totales, StringComparer.OrdinalIgnoreCase), Cuenta = g.Cuenta
                    });
                abiertos.RemoveAt(n);
            }
        }

        foreach (var fila in ordenadas)
        {
            // ¿En qué nivel cambia el grupo?
            var cambio = -1;
            for (var n = 0; n < grupos.Count; n++)
            {
                var texto = TextoGrupo(tabla, fila, grupos[n]);
                if (cambio < 0 && (n >= abiertos.Count || !string.Equals(actuales[n], texto, StringComparison.Ordinal))) cambio = n;
                if (cambio >= 0) actuales[n] = texto;
            }
            if (cambio >= 0)
            {
                Cerrar(cambio);
                for (var n = cambio; n < grupos.Count; n++)
                {
                    var t = actuales[n] ?? "";
                    resultado.Filas.Add(new ReporteFilaListado
                    {
                        Tipo = ReporteFilaTipo.GrupoCabecera, Nivel = n + 1, Texto = $"{grupos[n].Etiqueta}: {(t.Length == 0 ? "(sin dato)" : t)}"
                    });
                    abiertos.Add((totalizan.ToDictionary(c => c.Nombre, _ => 0m, StringComparer.OrdinalIgnoreCase), 0, t.Length == 0 ? "(sin dato)" : t));
                }
            }

            resultado.Filas.Add(new ReporteFilaListado { Tipo = ReporteFilaTipo.Detalle, Fila = fila });
            foreach (var c in totalizan)
            {
                if (!ReporteFormato.ADecimal(tabla.Valor(fila, c.Nombre), out var v)) continue;
                general[c.Nombre] += v;
                foreach (var g in abiertos) g.Totales[c.Nombre] += v;
            }
            for (var n = 0; n < abiertos.Count; n++) abiertos[n] = (abiertos[n].Totales, abiertos[n].Cuenta + 1, abiertos[n].Texto);
        }
        Cerrar(0);

        if (ctx.D.FLG_TOTAL_GENERAL && totalizan.Count > 0 && ordenadas.Count > 0)
            resultado.Filas.Add(new ReporteFilaListado
            {
                Tipo = ReporteFilaTipo.TotalGeneral, Texto = "Total general", Totales = general, Cuenta = ordenadas.Count
            });
        return resultado;
    }

    private static string TextoGrupo(ReporteTabla tabla, object?[] fila, ReporteColumnaVista g)
        => ReporteFormato.Texto(tabla.Valor(fila, g.Nombre), g.Formato, g.Tipo, c => tabla.Valor(fila, c));

    /// <summary>Orden estable por varias columnas; los vacíos van al final.</summary>
    public static List<object?[]> Ordenar(List<object?[]> filas, ReporteTabla? tabla, List<(string Nombre, bool Desc)> claves)
    {
        if (tabla is null || claves.Count == 0) return filas.ToList();
        IOrderedEnumerable<object?[]>? q = null;
        foreach (var (nombre, desc) in claves)
        {
            var i = tabla.Indice(nombre);
            Func<object?[], object?> k = f => f[i] is DBNull ? null : f[i];
            q = q is null
                ? (desc ? filas.OrderByDescending(k, Comparador.Instancia) : filas.OrderBy(k, Comparador.Instancia))
                : (desc ? q.ThenByDescending(k, Comparador.Instancia) : q.ThenBy(k, Comparador.Instancia));
        }
        return q!.ToList();
    }

    internal sealed class Comparador : IComparer<object?>
    {
        public static readonly Comparador Instancia = new();
        public int Compare(object? a, object? b)
        {
            if (a is null && b is null) return 0;
            if (a is null) return 1;
            if (b is null) return -1;
            if (ReporteFormato.ADecimal(a, out var x) && ReporteFormato.ADecimal(b, out var y) && a is not string && b is not string) return x.CompareTo(y);
            if (a is IComparable ca && a.GetType() == b.GetType()) return ca.CompareTo(b);
            return string.Compare(Convert.ToString(a, ReporteFormato.Cultura), Convert.ToString(b, ReporteFormato.Cultura), StringComparison.CurrentCultureIgnoreCase);
        }
    }
}

// ── Ficha: secciones ────────────────────────────────────────────────────────────

public sealed class ReporteSeccionFicha
{
    public int                       Resultado    { get; init; }
    public string                    Presentacion { get; init; } = "TAB";
    public string?                   Titulo       { get; init; }
    public int                       NumColumnas  { get; init; } = 3;
    public ReporteTabla?             Tabla        { get; init; }
    public List<ReporteColumnaVista> Columnas     { get; init; } = new();

    /// <summary>Las secciones visibles de la ficha, en su orden. Sin secciones en el diseño: el resultado de
    /// cabecera como campos y los demás como tablas.</summary>
    public static List<ReporteSeccionFicha> Armar(ReporteContexto ctx)
    {
        var secciones = ctx.Definicion.Secciones.OrderBy(s => s.NUM_ORDEN).ThenBy(s => s.NUM_RESULTADO).ToList();
        if (secciones.Count == 0)
            secciones = ctx.Definicion.Resultados.OrderBy(r => r.NUM_RESULTADO).Select((r, i) => new ReporteDisenoSeccion
            {
                NUM_RESULTADO = r.NUM_RESULTADO, NUM_ORDEN = i + 1,
                COD_PRESENTACION = r.COD_PAPEL == "CAB" ? "CAM" : "TAB",
                DES_TITULO = r.COD_PAPEL == "CAB" ? null : r.DES_RESULTADO
            }).ToList();

        return secciones
            .Where(s => s.COD_PRESENTACION != "OCU")
            .Select(s => new ReporteSeccionFicha
            {
                Resultado    = s.NUM_RESULTADO,
                Presentacion = s.COD_PRESENTACION,
                Titulo       = string.IsNullOrWhiteSpace(s.DES_TITULO) ? null : ReporteTexto.Reemplazar(s.DES_TITULO, ctx),
                NumColumnas  = Math.Clamp(s.NUM_COLUMNAS ?? 3, 1, 4),
                Tabla        = ctx.Datos.Tabla(s.NUM_RESULTADO),
                Columnas     = ReporteColumnas.De(ctx, s.NUM_RESULTADO, incluirOcultas: false)
            })
            .Where(s => s.Columnas.Count > 0)
            .ToList();
    }

    /// <summary>Los campos de una sección CAM con su valor (de la primera fila). Los vacíos no se muestran.</summary>
    public List<(ReporteColumnaVista Columna, string Valor)> Campos(bool modoPlantilla)
    {
        var lista = new List<(ReporteColumnaVista, string)>();
        foreach (var c in Columnas)
        {
            if (modoPlantilla) { lista.Add((c, $"{{{{R{Resultado}.{c.Nombre}}}}}")); continue; }
            if (Tabla is null || Tabla.Filas.Count == 0) continue;
            var fila = Tabla.Filas[0];
            var v = ReporteFormato.Texto(Tabla.Valor(fila, c.Nombre), c.Formato, c.Tipo, n => Tabla.Valor(fila, n));
            if (!string.IsNullOrWhiteSpace(v)) lista.Add((c, v));
        }
        return lista;
    }

    /// <summary>Las filas de una sección TAB o TOT ya formateadas. En modo plantilla, una sola fila con las marcas.</summary>
    public List<List<string>> Celdas(bool modoPlantilla)
    {
        if (modoPlantilla)
            return new() { Columnas.Select(c => $"{{{{R{Resultado}.{c.Nombre}}}}}").ToList() };
        if (Tabla is null) return new();
        return Tabla.Filas.Select(f => Columnas.Select(c =>
            ReporteFormato.Texto(Tabla.Valor(f, c.Nombre), c.Formato, c.Tipo, n => Tabla.Valor(f, n))).ToList()).ToList();
    }

    /// <summary>Totales de las columnas marcadas para totalizar en una sección TAB (null si ninguna totaliza).</summary>
    public Dictionary<string, decimal>? Totales()
    {
        var cols = Columnas.Where(c => c.Totaliza && c.EsNumero).ToList();
        if (cols.Count == 0 || Tabla is null) return null;
        return cols.ToDictionary(c => c.Nombre,
            c => Tabla.Filas.Sum(f => ReporteFormato.ADecimal(Tabla.Valor(f, c.Nombre), out var v) ? v : 0m),
            StringComparer.OrdinalIgnoreCase);
    }
}

/// <summary>Firmas del pie de una ficha: "Elaborado por|Aprobado por".</summary>
public static class ReporteFirmas
{
    public static List<string> De(string? texto)
        => string.IsNullOrWhiteSpace(texto) ? new()
         : texto.Split('|', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Take(4).ToList();
}

/// <summary>Medidas de la hoja en centímetros.</summary>
public static class ReportePapel
{
    public static (double Ancho, double Alto) Tamano(string? papel, string? orientacion)
    {
        var (w, h) = (papel ?? "A4").ToUpperInvariant() switch
        {
            "CAR" => (21.59, 27.94),
            "OFI" => (21.59, 35.56),
            _     => (21.0, 29.7)
        };
        return string.Equals(orientacion, "H", StringComparison.OrdinalIgnoreCase) ? (h, w) : (w, h);
    }

    public const double Margen = 1.2;
}

/// <summary>Las marcas de las plantillas propias: {{R2.COLUMNA}}, {{CAB.COLUMNA}}, {{@PARAMETRO}}, {{EMPRESA.NOMBRE}} (y RUC,
/// DIRECCION, APLICACION),
/// {{TOTAL.R2.COLUMNA}}... Con "|formato" al final se cambia el formato: {{R1.FEC_PEDIDO|dd/MM/yyyy}}.</summary>
public static class ReporteMarcas
{
    public static readonly Regex Marca = new(@"\{\{\s*([^{}|]+?)\s*(?:\|\s*([^{}]*?)\s*)?\}\}", RegexOptions.Compiled);

    /// <summary>Lo que dice una marca, ya separado.</summary>
    public sealed record Partes(string Tipo, int Resultado, string Campo, string? Formato);

    /// <summary>Tipo: R (columna de un resultado), T (total de una columna), P (parámetro), E (empresa), G (general:
    /// TITULO, SUBTITULO, FILTROS, USUARIO, FECHA). null si la marca no se entiende.</summary>
    public static Partes? Leer(string cuerpo, string? formato, ReporteDefinicion def)
    {
        var c = cuerpo.Trim();
        if (c.StartsWith('@')) return new("P", 0, c[1..], formato);
        var p = c.Split('.', StringSplitOptions.TrimEntries);
        if (p.Length == 1) return new("G", 0, p[0].ToUpperInvariant(), formato);
        var primero = p[0].ToUpperInvariant();
        if (primero == "P" && p.Length == 2) return new("P", 0, p[1], formato);
        if (primero == "EMPRESA" && p.Length == 2) return new("E", 0, p[1].ToUpperInvariant(), formato);
        if (primero == "TOTAL" && p.Length == 3 && Resultado(p[1], def) is { } rt) return new("T", rt, p[2], formato);
        if (p.Length == 2 && Resultado(p[0], def) is { } r) return new("R", r, p[1], formato);
        return null;
    }

    /// <summary>R1, R2... o el primer resultado con ese papel (CAB, DET, ADI).</summary>
    private static int? Resultado(string s, ReporteDefinicion def)
    {
        var u = s.ToUpperInvariant();
        if (u.Length > 1 && u[0] == 'R' && int.TryParse(u[1..], out var n)) return n;
        if (u is "CAB" or "DET" or "ADI")
        {
            var r = def.Resultados.OrderBy(x => x.NUM_RESULTADO).FirstOrDefault(x => x.COD_PAPEL == u);
            if (r is not null) return r.NUM_RESULTADO;
            return u == "CAB" ? 1 : u == "DET" ? 2 : 3;
        }
        return null;
    }

    /// <summary>¿El resultado se repite en una fila de tabla? Todos menos los de cabecera (una sola fila).</summary>
    public static bool SeRepite(int resultado, ReporteContexto ctx)
    {
        var r = ctx.Definicion.Resultados.FirstOrDefault(x => x.NUM_RESULTADO == resultado);
        if (r is not null) return r.COD_PAPEL != "CAB";
        return !(resultado == 1 && !ctx.EsListado);
    }

    /// <summary>Las filas con que se repite una fila de plantilla. En un listado, el resultado 1 va en el orden del
    /// diseño (las plantillas propias no agrupan ni suman por grupo: eso queda para el diseño).</summary>
    public static List<object?[]> FilasDe(ReporteContexto ctx, int resultado)
    {
        if (ctx.EsListado && resultado == (ctx.Datos.Tabla(1) ?? ctx.Datos.Tablas.FirstOrDefault())?.Numero)
            return ReporteListado.Armar(ctx).Ordenadas;
        return ctx.Datos.Tabla(resultado)?.Filas ?? new List<object?[]>();
    }

    /// <summary>El valor de una marca (para una fila concreta si es de un resultado que se repite).</summary>
    public static string Valor(Partes m, ReporteContexto ctx, object?[]? fila = null)
    {
        switch (m.Tipo)
        {
            case "P":
                var v = ctx.Valores.FirstOrDefault(x => string.Equals(x.Campo, m.Campo, StringComparison.OrdinalIgnoreCase));
                if (v is null) return "";
                return m.Formato is null ? v.Texto ?? "" : ReporteFormato.Texto(v.Valor, m.Formato, v.Valor?.GetType() ?? typeof(string));
            case "E":
                return m.Campo switch
                {
                    "NOMBRE" or "RAZON_SOCIAL" => ctx.Empresa?.DES_NOMBRE_EMPRESA ?? "",
                    "RUC" or "DOCUMENTO"       => ctx.Empresa?.NUM_DOCUMENTO_IDENTIDAD ?? "",
                    "DIRECCION"                => ctx.Empresa?.DES_DOMICILIO_FISCAL ?? "",
                    "APLICACION"               => ctx.Empresa?.DES_NOMBRE_APLICACION ?? "",
                    _ => ""
                };
            case "G":
                return m.Campo switch
                {
                    "TITULO"    => ReporteTexto.Reemplazar(ctx.D.DES_TITULO ?? ctx.D.DES_DISENO, ctx),
                    "SUBTITULO" => ReporteTexto.Reemplazar(ctx.D.DES_SUBTITULO, ctx),
                    "FILTROS"   => ReporteTexto.Filtros(ctx),
                    "USUARIO"   => string.IsNullOrWhiteSpace(ctx.Empresa?.DES_USUARIO) ? ctx.Usuario : ctx.Empresa!.DES_USUARIO!,
                    "FECHA"     => ctx.Fecha.ToString(m.Formato ?? "dd/MM/yyyy HH:mm", ReporteFormato.Cultura),
                    "HOY"       => ctx.Fecha.ToString(m.Formato ?? "dd/MM/yyyy", ReporteFormato.Cultura),
                    _ => ""
                };
            case "T":
            {
                var tabla = ctx.Datos.Tabla(m.Resultado);
                if (tabla is null) return "";
                var col = ReporteColumnas.Buscar(ctx, m.Resultado, m.Campo);
                var suma = tabla.Filas.Sum(f => ReporteFormato.ADecimal(tabla.Valor(f, m.Campo), out var x) ? x : 0m);
                return ReporteFormato.Texto(suma, m.Formato ?? col?.Formato, typeof(decimal));
            }
            default: // R
            {
                var tabla = ctx.Datos.Tabla(m.Resultado);
                if (tabla is null) return "";
                var f = fila ?? tabla.Filas.FirstOrDefault();
                if (f is null) return "";
                var col = ReporteColumnas.Buscar(ctx, m.Resultado, m.Campo);
                return ReporteFormato.Texto(tabla.Valor(f, m.Campo), m.Formato ?? col?.Formato, col?.Tipo ?? typeof(string), n => tabla.Valor(f, n));
            }
        }
    }

    /// <summary>El valor sin formatear (para escribir números y fechas de verdad en Excel).</summary>
    public static object? ValorCrudo(Partes m, ReporteContexto ctx, object?[]? fila = null)
    {
        if (m.Tipo == "R")
        {
            var tabla = ctx.Datos.Tabla(m.Resultado);
            var f = fila ?? tabla?.Filas.FirstOrDefault();
            return tabla is null || f is null ? null : tabla.Valor(f, m.Campo);
        }
        if (m.Tipo == "P")
            return ctx.Valores.FirstOrDefault(x => string.Equals(x.Campo, m.Campo, StringComparison.OrdinalIgnoreCase))?.Valor;
        return null;
    }

    /// <summary>¿Existe lo que nombra la marca? Para avisar al subir una plantilla.</summary>
    public static string? Validar(Partes? m, string texto, ReporteDefinicion def)
    {
        if (m is null) return $"No se entiende la marca {texto}.";
        switch (m.Tipo)
        {
            case "P":
                return def.Parametros.Any(p => string.Equals(ReporteTexto.SinArroba(p.DES_PARAMETRO), m.Campo, StringComparison.OrdinalIgnoreCase))
                    ? null : $"La marca {texto} nombra un parámetro que la fuente no tiene.";
            case "E":
                return m.Campo is "NOMBRE" or "RAZON_SOCIAL" or "RUC" or "DOCUMENTO" or "DIRECCION" or "APLICACION"
                    ? null : $"La marca {texto} no es un dato de la empresa (NOMBRE, RUC, DIRECCION o APLICACION).";
            case "G":
                return m.Campo is "TITULO" or "SUBTITULO" or "FILTROS" or "USUARIO" or "FECHA" or "HOY"
                    ? null : $"La marca {texto} no se reconoce.";
            default:
                return def.Columnas.Any(c => c.NUM_RESULTADO == m.Resultado && string.Equals(c.DES_COLUMNA, m.Campo, StringComparison.OrdinalIgnoreCase))
                       || (m.Resultado == 1 && ReporteFormulasDiseno.EsColumnaCalculada(def, m.Campo))
                    ? null : $"La marca {texto} nombra una columna que el resultado {m.Resultado} de la fuente no tiene.";
        }
    }

    /// <summary>Las marcas de un texto.</summary>
    public static IEnumerable<(string Texto, Partes? Partes)> En(string texto, ReporteDefinicion def)
        => Marca.Matches(texto).Select(x => (x.Value, Leer(x.Groups[1].Value, x.Groups[2].Success && x.Groups[2].Value.Length > 0 ? x.Groups[2].Value : null, def)));

    /// <summary>Reemplaza todas las marcas de un texto. "fila": la fila del resultado que se está repitiendo.</summary>
    public static string Reemplazar(string texto, ReporteContexto ctx, int? resultadoFila = null, object?[]? fila = null)
        => Marca.Replace(texto, x =>
        {
            var m = Leer(x.Groups[1].Value, x.Groups[2].Success && x.Groups[2].Value.Length > 0 ? x.Groups[2].Value : null, ctx.Definicion);
            if (m is null) return x.Value;
            return Valor(m, ctx, m.Tipo == "R" && m.Resultado == resultadoFila ? fila : null);
        });

    /// <summary>El resultado que hace repetir un texto (la primera marca de un resultado que se repite), o null.</summary>
    public static int? ResultadoQueRepite(string texto, ReporteContexto ctx)
    {
        foreach (var (_, p) in En(texto, ctx.Definicion))
            if (p is { Tipo: "R" } && SeRepite(p.Resultado, ctx)) return p.Resultado;
        return null;
    }
}
