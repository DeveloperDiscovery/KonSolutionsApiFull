using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace KonSolutionsApi.Application.Reportes;

/// <summary>La copia del archivo que genera un botón (REPORTES.OPCIONES_BOTONES_REPORTES: COD_KEY_PATHFILE y
/// DES_NOMBRE_ARCHIVO). Se guarda en la ruta del botón con un nombre armado con los datos, sin pisar otro archivo.
///
/// El nombre: texto fijo y marcas {COLUMNA} o {COLUMNA:formato}, sin extensión (la pone el formato que se genera).
///   {COD_CLASE_OPERACION}_{COD_TIPO_OPERACION}_{NUM_PEDIDO_COMERCIAL:0000000000}  →  PRFRM_LCBIE_0000000001
/// Cada marca se busca, en este orden:
///   1. en la fila (o la pantalla) desde donde se tocó el botón;
///   2. en los parámetros del reporte (sin @);
///   3. en la primera fila del resultado 1 del reporte;
///   4. FECHA (la de generación) y USUARIO.
/// El formato es el de .NET con cultura invariante: 0000000000 rellena un número con ceros; yyyyMMdd o yyyy-MM-dd, una
/// fecha. Una fecha sin formato sale como yyyyMMdd (con -HHmmss si tiene hora). Lo que Windows no acepta en un nombre
/// (\ / : * ? " &lt; &gt; |) se cambia por un guion.
///
/// Si ya hay un archivo con ese nombre, se guarda otra versión: nombre_2, nombre_3...</summary>
public static class ReporteCopiaArchivo
{
    public const int MaxLargo = 200;
    public const int MaxVersiones = 999;

    private static readonly Regex Marca = new(@"\{([^{}:]*)(?::([^{}]*))?\}", RegexOptions.Compiled);
    private static readonly Regex Campo = new(@"^@?[A-Za-z_][A-Za-z0-9_]*$", RegexOptions.Compiled);
    private static readonly Regex FechaIso = new(@"^\d{4}-\d{2}-\d{2}([T ]\d{2}:\d{2}(:\d{2}(\.\d+)?)?)?$", RegexOptions.Compiled);
    private static readonly char[] Prohibidos = { '\\', '/', ':', '*', '?', '"', '<', '>', '|' };
    private static readonly string[] Extensiones = { ".pdf", ".docx", ".doc", ".xlsx", ".xls", ".csv" };

    /// <summary>Qué tiene mal el nombre; null si está bien o si está vacío (sin nombre, la copia lleva el de siempre).</summary>
    public static string? Validar(string? patron)
    {
        if (string.IsNullOrWhiteSpace(patron)) return null;
        var p = patron.Trim();
        if (p.Length > MaxLargo) return $"El nombre del archivo tiene más de {MaxLargo} caracteres.";
        var fuera = Marca.Replace(p, "");
        if (fuera.Contains('{') || fuera.Contains('}'))
            return "En el nombre del archivo, cada { necesita su } y no van una dentro de otra.";
        if (fuera.IndexOfAny(Prohibidos) >= 0 || fuera.Any(char.IsControl))
            return "El nombre del archivo no puede llevar \\ / : * ? \" < > | fuera de las llaves.";
        foreach (Match m in Marca.Matches(p))
        {
            if (!Campo.IsMatch(m.Groups[1].Value.Trim()))
                return $"{m.Value}: entre llaves va el nombre de una columna (letras, números y _), como {{NUM_PEDIDO_COMERCIAL}}.";
            if (m.Groups[2].Success && m.Groups[2].Value.Length == 0)
                return $"{m.Value}: después de los dos puntos va el formato, como {{NUM_PEDIDO_COMERCIAL:0000000000}}.";
        }
        if (Extensiones.Any(e => p.EndsWith(e, StringComparison.OrdinalIgnoreCase)))
            return "No pongas la extensión en el nombre del archivo: la pone el formato que se genera.";
        return null;
    }

    /// <summary>El nombre, sin extensión, con cada marca reemplazada. Una marca que no está en ningún lado no se deja
    /// vacía: ArgumentException con su nombre (así no se guarda un archivo con un nombre a medias).</summary>
    public static string Armar(string patron, IReadOnlyDictionary<string, string?>? fila, IEnumerable<ReporteValorParametro> parametros,
                               ReporteDatos datos, DateTime fecha, string usuario)
    {
        var faltan = new List<string>();
        var nombre = Marca.Replace(patron.Trim(), m =>
        {
            var campo = ReporteTexto.SinArroba(m.Groups[1].Value);
            var formato = m.Groups[2].Success ? m.Groups[2].Value : null;
            if (!Buscar(campo, fila, parametros, datos, fecha, usuario, out var valor)) { faltan.Add(campo); return ""; }
            return Texto(valor, formato);
        });
        if (faltan.Count > 0)
            throw new ArgumentException(faltan.Count == 1
                ? $"{{{faltan[0]}}} no está en la fila ni en los datos del reporte."
                : $"{string.Join(", ", faltan.Select(f => "{" + f + "}"))} no están en la fila ni en los datos del reporte.");
        return Limpiar(nombre);
    }

    /// <summary>Guarda el archivo en la carpeta sin pisar otro: si nombre.ext ya existe, prueba
    /// nombre_2.ext, nombre_3.ext... Devuelve la ruta con que quedó.</summary>
    public static string Guardar(string carpeta, string nombre, string extension, byte[] contenido)
    {
        // La carpeta no se crea: si la ruta tiene un error de tipeo (o la API no llega al servidor), crearla escondería el
        // error en una carpeta nueva que nadie mira. Se avisa, para que se corrija la ruta.
        if (!Directory.Exists(carpeta))
            throw new ArgumentException($"la carpeta {carpeta} no existe, o la API no tiene permiso para verla. Revisa la ruta en Entorno › Rutas de Archivos.");
        for (var n = 1; n <= MaxVersiones; n++)
        {
            var ruta = Path.Combine(carpeta, $"{(n == 1 ? nombre : $"{nombre}_{n}")}.{extension}");
            if (File.Exists(ruta)) continue;
            FileStream fs;
            try { fs = new FileStream(ruta, FileMode.CreateNew, FileAccess.Write, FileShare.None); }
            catch (IOException) when (File.Exists(ruta)) { continue; }   // otro lo creó en ese mismo instante
            try
            {
                using (fs) fs.Write(contenido);
                return ruta;
            }
            catch
            {
                try { File.Delete(ruta); } catch { /* que quede el error original */ }
                throw;
            }
        }
        throw new IOException($"Ya hay {MaxVersiones} versiones de {nombre}.{extension} en {carpeta}.");
    }

    private static bool Buscar(string campo, IReadOnlyDictionary<string, string?>? fila, IEnumerable<ReporteValorParametro> parametros,
                               ReporteDatos datos, DateTime fecha, string usuario, out object? valor)
    {
        if (fila is not null)
            foreach (var (k, v) in fila)
                if (string.Equals(ReporteTexto.SinArroba(k), campo, StringComparison.OrdinalIgnoreCase)) { valor = v; return true; }
        var p = parametros.FirstOrDefault(x => string.Equals(x.Campo, campo, StringComparison.OrdinalIgnoreCase));
        if (p is not null) { valor = p.Valor; return true; }
        var t = datos.Tabla(1);
        if (t is not null && t.Indice(campo) >= 0)
        {
            valor = t.Filas.Count == 0 ? null : t.Valor(t.Filas[0], campo);
            return true;
        }
        switch (campo.ToUpperInvariant())
        {
            case "FECHA":   valor = fecha;   return true;
            case "USUARIO": valor = usuario; return true;
        }
        valor = null;
        return false;
    }

    /// <summary>El texto de un valor con su formato. Los de la fila llegan como texto invariante (fechas ISO): con un
    /// formato, se leen como número o fecha antes de darles forma.</summary>
    private static string Texto(object? valor, string? formato)
    {
        var inv = CultureInfo.InvariantCulture;
        switch (valor)
        {
            case null or DBNull:
                return "";
            case string s:
                s = s.Trim();
                if (formato is not null && decimal.TryParse(s, NumberStyles.Float, inv, out var d)) return d.ToString(formato, inv);
                if (FechaIso.IsMatch(s) && DateTime.TryParse(s, inv, DateTimeStyles.None, out var fs)) return Fecha(fs, formato);
                return s;
            case DateTime dt:
                return Fecha(dt, formato);
            case DateTimeOffset dto:
                return Fecha(dto.DateTime, formato);
            case bool b:
                return b ? "1" : "0";
            case IFormattable x:
                return x.ToString(formato, inv);
            default:
                return Convert.ToString(valor, inv) ?? "";
        }
    }

    private static string Fecha(DateTime f, string? formato)
        => f.ToString(formato ?? (f.TimeOfDay == TimeSpan.Zero ? "yyyyMMdd" : "yyyyMMdd-HHmmss"), CultureInfo.InvariantCulture);

    /// <summary>Sin lo que Windows no acepta en un nombre (cambiado por un guion), sin espacios ni puntos al final y con
    /// el largo máximo.</summary>
    private static string Limpiar(string nombre)
    {
        var sb = new StringBuilder(nombre.Length);
        foreach (var ch in nombre)
            sb.Append(Array.IndexOf(Prohibidos, ch) >= 0 || char.IsControl(ch) || Path.GetInvalidFileNameChars().Contains(ch) ? '-' : ch);
        var limpio = sb.ToString().Trim();
        if (limpio.Length > MaxLargo) limpio = limpio[..MaxLargo];
        limpio = limpio.TrimEnd(' ', '.');
        if (limpio.Length == 0) throw new ArgumentException("el nombre del archivo quedó vacío con los datos de esta fila.");
        return limpio;
    }
}
