using System.Text;
using KonSolutionsApi.Domain.Models.Reportes;

namespace KonSolutionsApi.Application.Reportes;

/// <summary>El punto de entrada del motor: elige el formato. PDF siempre sale del diseño; Word y Excel salen de la
/// plantilla propia si el diseño tiene una, y si no, del diseño.
/// Diseño libre (BAN): el PDF dibuja el lienzo por bandas; el Excel «con formato» (opción del formato XLS) imita el PDF
/// en celdas; si no, Excel, CSV y las plantillas leen el lienzo como un listado (o una ficha, si tiene tablas). Word sale
/// solo con plantilla propia: un lienzo dibujado a mano no se pasa bien a un documento que fluye.</summary>
public static class ReporteGenerador
{
    public static readonly string[] Formatos = { "PDF", "DOC", "XLS", "CSV" };

    public static byte[] Generar(ReporteContexto ctx, string formato)
    {
        var propia = ctx.Plantilla is { Length: > 0 } && !ctx.ModoPlantilla;
        var f = (formato ?? "").ToUpperInvariant();
        if (string.Equals(ctx.D.COD_TIPO_DISENO, "BAN", StringComparison.OrdinalIgnoreCase))
        {
            if (f == "PDF" && !ctx.ModoPlantilla) return ReporteBandasPdf.Generar(ctx);
            if (f == "XLS" && !propia && !ctx.ModoPlantilla && ConFormato(ctx)) return ReporteBandasExcel.Generar(ctx);
            if (f == "DOC" && !propia && !ctx.ModoPlantilla)
                throw new ArgumentException("En un diseño libre, Word sale solo con plantilla propia: descarga la plantilla base, ajústala y súbela.");
            ctx = ReporteBandasListado.Sintetizar(ctx);
            if (f == "CSV" && !ctx.EsListado)
                throw new ArgumentException("Este diseño libre tiene tablas de varios resultados (es una ficha): el CSV es solo para listados.");
        }
        return f switch
        {
            "PDF" => ReportePdf.Generar(ctx),
            "DOC" => propia ? ReporteWordPlantilla.Llenar(ctx) : ReporteWord.Generar(ctx),
            "XLS" => propia ? ReporteExcelPlantilla.Llenar(ctx) : ReporteExcel.Generar(ctx),
            "CSV" => ReporteCsv.Generar(ctx),
            _     => throw new NotSupportedException($"El formato {formato} no existe (PDF, DOC, XLS o CSV).")
        };
    }

    /// <summary>¿El Excel de este diseño libre va «con formato» (como el PDF)?</summary>
    public static bool ConFormato(ReporteContexto ctx)
        => ctx.Definicion.Formatos.Any(x => x.COD_FORMATO == "XLS" && x.FLG_CON_FORMATO);

    public static string Extension(string formato) => (formato ?? "").ToUpperInvariant() switch
    {
        "PDF" => "pdf",
        "DOC" => "docx",
        "XLS" => "xlsx",
        "CSV" => "csv",
        _     => "bin"
    };

    public static string ContentType(string formato) => (formato ?? "").ToUpperInvariant() switch
    {
        "PDF" => "application/pdf",
        "DOC" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
        "XLS" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        "CSV" => "text/csv",
        _     => "application/octet-stream"
    };

    /// <summary>Revisa una plantilla propia antes de guardarla: que se pueda abrir y que cada marca nombre algo que
    /// existe (columna del resultado, parámetro, dato de la empresa). No impide guardarla: devuelve avisos.</summary>
    public static ReportePlantillaResultado ValidarPlantilla(ReporteDefinicion def, string formato, byte[] archivo)
    {
        if (archivo.Length < 4 || archivo[0] != 0x50 || archivo[1] != 0x4B)
            throw new InvalidOperationException(formato == "DOC" ? "El archivo no es un .docx (Word 2007 o posterior)." : "El archivo no es un .xlsx (Excel 2007 o posterior).");
        IEnumerable<string> textos;
        try
        {
            textos = formato == "DOC" ? ReporteWordPlantilla.Textos(archivo) : ReporteExcelPlantilla.Textos(archivo);
        }
        catch (Exception ex) when (ex is not InvalidOperationException)
        {
            throw new InvalidOperationException($"No se pudo abrir la plantilla: {ex.Message}");
        }
        var r = new ReportePlantillaResultado();
        foreach (var t in textos)
            foreach (var (texto, partes) in ReporteMarcas.En(t, def))
            {
                r.NUM_MARCAS++;
                var aviso = ReporteMarcas.Validar(partes, texto, def);
                if (aviso is not null && !r.Avisos.Contains(aviso)) r.Avisos.Add(aviso);
            }
        if (r.NUM_MARCAS == 0) r.Avisos.Add("La plantilla no tiene ninguna marca {{...}}: saldrá siempre igual, sin datos.");
        return r;
    }
}

/// <summary>El listado en CSV: solo los datos, en el orden del diseño, sin grupos ni totales. Fechas con el formato
/// de la columna, números sin separador de miles y con punto decimal. UTF-8 con BOM para que Excel lea bien las
/// tildes y la ñ.</summary>
public static class ReporteCsv
{
    public static byte[] Generar(ReporteContexto ctx)
    {
        if (!ctx.EsListado) throw new InvalidOperationException("El formato CSV es solo para listados.");
        var l = ReporteListado.Armar(ctx);
        var sep = (ctx.D.DES_SEPARADOR_CSV ?? ";").Trim().ToUpperInvariant() switch
        {
            "" or null => ";",
            "TAB" or "\\T" => "\t",
            var s => s
        };
        var sb = new StringBuilder();
        sb.AppendLine(string.Join(sep, l.Columnas.Select(c => Campo(c.Etiqueta, sep))));
        if (l.Tabla is not null)
            foreach (var fila in l.Ordenadas)
                sb.AppendLine(string.Join(sep, l.Columnas.Select(c => Campo(Valor(l.Tabla.Valor(fila, c.Nombre), c, n => l.Tabla.Valor(fila, n)), sep))));
        var bom = Encoding.UTF8.GetPreamble();
        var cuerpo = Encoding.UTF8.GetBytes(sb.ToString());
        return bom.Concat(cuerpo).ToArray();
    }

    private static string Valor(object? v, ReporteColumnaVista c, Func<string, object?> campo)
    {
        if (v is null || v is DBNull) return "";
        if (ReporteFormato.EsLetras(c.Formato)) return ReporteFormato.Texto(v, c.Formato, c.Tipo, campo);
        if (v is not string && v is not bool && v is not DateTime && ReporteFormato.ADecimal(v, out var n))
        {
            var f = (c.Formato ?? ReporteFormato.Defecto(c.Tipo, v) ?? "0.##########").Replace(",", "");
            try { return n.ToString(f, ReporteFormato.Cultura); }
            catch (FormatException) { return n.ToString(ReporteFormato.Cultura); }
        }
        return ReporteFormato.Texto(v, c.Formato, c.Tipo, campo);
    }

    private static string Campo(string? texto, string sep)
    {
        var t = texto ?? "";
        if (t.Contains(sep) || t.Contains('"') || t.Contains('\n') || t.Contains('\r'))
            return "\"" + t.Replace("\"", "\"\"") + "\"";
        return t;
    }
}
