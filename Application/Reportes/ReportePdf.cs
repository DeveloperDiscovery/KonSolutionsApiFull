using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;

namespace KonSolutionsApi.Application.Reportes;

/// <summary>El reporte en PDF, con MigraDoc (licencia MIT). Encabezado en cada página (logo, título, empresa),
/// pie con los filtros, quién lo generó y "Página N de M". El listado repite la fila de títulos en cada página.</summary>
public static class ReportePdf
{
    private const string Fuente = ReporteFuentesPdf.Familia;
    private static readonly Color Tinta     = new(0x16, 0x3a, 0x5f);
    private static readonly Color Dorado    = new(0xff, 0xd5, 0x4f);
    private static readonly Color Grupo1    = new(0xe8, 0xea, 0xf6);
    private static readonly Color Grupo2    = new(0xf3, 0xf4, 0xf6);
    private static readonly Color Cebra     = new(0xfa, 0xfa, 0xfa);
    private static readonly Color Linea     = new(0xcf, 0xd8, 0xdc);
    private static readonly Color Gris      = new(0x78, 0x90, 0x9c);

    public static byte[] Generar(ReporteContexto ctx)
    {
        ReporteFuentesPdf.Asegurar();
        var doc = new Document();
        doc.Info.Title = ReporteTexto.Reemplazar(ctx.D.DES_TITULO ?? ctx.D.DES_DISENO, ctx);
        doc.Info.Author = ctx.Empresa?.DES_NOMBRE_EMPRESA ?? "";
        var normal = doc.Styles[StyleNames.Normal]!;
        normal.Font.Name = Fuente;
        normal.Font.Size = Math.Clamp(ctx.D.NUM_TAMANO_LETRA, 6, 14);

        var (ancho, alto) = ReportePapel.Tamano(ctx.D.COD_PAPEL, ctx.D.COD_ORIENTACION);
        var sec = doc.AddSection();
        sec.PageSetup.PageWidth  = Unit.FromCentimeter(ancho);
        sec.PageSetup.PageHeight = Unit.FromCentimeter(alto);
        sec.PageSetup.LeftMargin = sec.PageSetup.RightMargin = Unit.FromCentimeter(ReportePapel.Margen);
        sec.PageSetup.TopMargin = Unit.FromCentimeter(3.0);
        sec.PageSetup.BottomMargin = Unit.FromCentimeter(1.8);
        sec.PageSetup.HeaderDistance = Unit.FromCentimeter(0.8);
        sec.PageSetup.FooterDistance = Unit.FromCentimeter(0.7);
        var util = ancho - 2 * ReportePapel.Margen;

        Encabezado(sec, ctx, util);
        Pie(sec, ctx, util);

        if (ctx.EsListado) Listado(sec, ctx, util);
        else Ficha(sec, ctx, util);

        var r = new PdfDocumentRenderer { Document = doc };
        r.RenderDocument();
        using var ms = new MemoryStream();
        r.PdfDocument.Save(ms, false);
        return ms.ToArray();
    }

    // ── Encabezado y pie ───────────────────────────────────────────────────────
    private static void Encabezado(Section sec, ReporteContexto ctx, double util)
    {
        var h = sec.Headers.Primary;
        var t = h.AddTable();
        t.Borders.Visible = false;
        var c1 = util * 0.22; var c3 = util * 0.26; var c2 = util - c1 - c3;
        t.AddColumn(Unit.FromCentimeter(c1)); t.AddColumn(Unit.FromCentimeter(c2)); t.AddColumn(Unit.FromCentimeter(c3));
        var row = t.AddRow();
        row.VerticalAlignment = VerticalAlignment.Center;

        if (ctx.D.FLG_LOGO && ctx.Empresa?.IMG_LOGO_EMPRESA is { Length: > 0 } logo && ReporteImagen.EsSoportada(logo))
        {
            try
            {
                var img = row.Cells[0].AddParagraph().AddImage("base64:" + Convert.ToBase64String(logo));
                img.Height = Unit.FromCentimeter(1.3);
                img.LockAspectRatio = true;
            }
            catch { /* un logo que MigraDoc no puede leer no impide el reporte */ }
        }

        var titulo = row.Cells[1].AddParagraph(ReporteTexto.Reemplazar(ctx.D.DES_TITULO ?? ctx.D.DES_DISENO, ctx));
        titulo.Format.Alignment = ParagraphAlignment.Center;
        titulo.Format.Font.Size = 13;
        titulo.Format.Font.Bold = true;
        titulo.Format.Font.Color = Tinta;
        var sub = ReporteTexto.Reemplazar(ctx.D.DES_SUBTITULO, ctx);
        if (!string.IsNullOrWhiteSpace(sub))
        {
            var p = row.Cells[1].AddParagraph(sub);
            p.Format.Alignment = ParagraphAlignment.Center;
            p.Format.Font.Size = 9;
        }

        if (ctx.D.FLG_DATOS_EMPRESA && ctx.Empresa is { } e)
        {
            var p = row.Cells[2].AddParagraph();
            p.Format.Alignment = ParagraphAlignment.Right;
            p.Format.Font.Size = 7.5;
            if (!string.IsNullOrWhiteSpace(e.DES_NOMBRE_EMPRESA)) p.AddFormattedText(e.DES_NOMBRE_EMPRESA, TextFormat.Bold);
            if (!string.IsNullOrWhiteSpace(e.NUM_DOCUMENTO_IDENTIDAD)) { p.AddLineBreak(); p.AddText("RUC " + e.NUM_DOCUMENTO_IDENTIDAD); }
            if (!string.IsNullOrWhiteSpace(e.DES_DOMICILIO_FISCAL)) { p.AddLineBreak(); p.AddText(e.DES_DOMICILIO_FISCAL); }
        }
        var raya = h.AddParagraph();
        raya.Format.Borders.Bottom.Width = 1.2;
        raya.Format.Borders.Bottom.Color = Tinta;
        raya.Format.SpaceAfter = Unit.FromPoint(2);
        raya.Format.Font.Size = 2;
    }

    private static void Pie(Section sec, ReporteContexto ctx, double util)
    {
        var f = sec.Footers.Primary;
        var t = f.AddTable();
        t.Borders.Visible = false;
        t.AddColumn(Unit.FromCentimeter(util * 0.45)); t.AddColumn(Unit.FromCentimeter(util * 0.35)); t.AddColumn(Unit.FromCentimeter(util * 0.20));
        var row = t.AddRow();
        row.Format.Font.Size = 6.5;
        row.Format.Font.Color = Gris;
        row.Borders.Top.Width = 0.4;
        row.Borders.Top.Color = Linea;
        var filtros = ctx.EsListado && ctx.D.FLG_FILTROS_PIE ? ReporteTexto.Filtros(ctx) : "";
        row.Cells[0].AddParagraph(string.IsNullOrWhiteSpace(filtros) ? "" : "Filtros: " + filtros);
        var g = row.Cells[1].AddParagraph(ReporteTexto.Generado(ctx));
        g.Format.Alignment = ParagraphAlignment.Center;
        var p = row.Cells[2].AddParagraph();
        p.Format.Alignment = ParagraphAlignment.Right;
        p.AddText("Página ");
        p.AddPageField();
        p.AddText(" de ");
        p.AddNumPagesField();
    }

    // ── Listado ────────────────────────────────────────────────────────────────
    private static void Listado(Section sec, ReporteContexto ctx, double util)
    {
        var l = ReporteListado.Armar(ctx);
        if (l.Columnas.Count == 0) { Aviso(sec, "El diseño no tiene columnas visibles."); return; }
        if (l.Ordenadas.Count == 0) { Aviso(sec, "No hay datos para los filtros elegidos."); return; }

        var anchos = Anchos(l.Columnas.Select(c => c.Ancho).ToList(), util);
        Table? tabla = null;
        var primeraDelGrupo = true;
        var cebra = false;

        Table Nueva()
        {
            var t = sec.AddTable();
            t.Borders.Width = 0;
            t.Format.Font.Size = Math.Clamp(ctx.D.NUM_TAMANO_LETRA, 6, 14);
            for (var i = 0; i < anchos.Count; i++) t.AddColumn(Unit.FromCentimeter(anchos[i]));
            var h = t.AddRow();
            h.HeadingFormat = true;
            h.Shading.Color = Tinta;
            h.Format.Font.Bold = true;
            h.Format.Font.Color = Colors.White;
            h.TopPadding = h.BottomPadding = Unit.FromPoint(2);
            for (var i = 0; i < l.Columnas.Count; i++)
            {
                var p = h.Cells[i].AddParagraph(l.Columnas[i].Etiqueta);
                p.Format.Alignment = Alineacion(l.Columnas[i].Alineacion);
            }
            return t;
        }

        foreach (var f in l.Filas)
        {
            if (f.Tipo == ReporteFilaTipo.GrupoCabecera && f.Nivel == 1 && ctx.D.FLG_SALTO_GRUPO && tabla is not null && !primeraDelGrupo)
            {
                sec.AddPageBreak();
                tabla = null;
            }
            tabla ??= Nueva();
            primeraDelGrupo = false;
            var row = tabla.AddRow();
            row.TopPadding = row.BottomPadding = Unit.FromPoint(1.2);
            switch (f.Tipo)
            {
                case ReporteFilaTipo.GrupoCabecera:
                {
                    row.Shading.Color = f.Nivel == 1 ? Grupo1 : Grupo2;
                    row.KeepWith = 1;
                    var c = row.Cells[0];
                    c.MergeRight = l.Columnas.Count - 1;
                    var p = c.AddParagraph(f.Texto);
                    p.Format.Font.Bold = true;
                    p.Format.Font.Color = Tinta;
                    p.Format.LeftIndent = Unit.FromPoint((f.Nivel - 1) * 8);
                    cebra = false;
                    break;
                }
                case ReporteFilaTipo.Detalle:
                {
                    if (cebra) row.Shading.Color = Cebra;
                    cebra = !cebra;
                    for (var i = 0; i < l.Columnas.Count; i++)
                    {
                        var c = l.Columnas[i];
                        var v = ReporteFormato.Texto(l.Tabla!.Valor(f.Fila!, c.Nombre), c.Formato, c.Tipo, n => l.Tabla.Valor(f.Fila!, n));
                        var p = row.Cells[i].AddParagraph(v);
                        p.Format.Alignment = Alineacion(c.Alineacion);
                    }
                    row.Borders.Bottom.Width = 0.25;
                    row.Borders.Bottom.Color = Linea;
                    break;
                }
                default: // pie de grupo o total general
                {
                    var total = f.Tipo == ReporteFilaTipo.TotalGeneral;
                    row.Shading.Color = total ? Grupo1 : Colors.White;
                    row.Format.Font.Bold = true;
                    row.Borders.Top.Width = total ? 1.0 : 0.5;
                    row.Borders.Top.Color = total ? Tinta : Gris;
                    Totales(row, l.Columnas, f.Texto, f.Totales);
                    if (total) row.Format.Font.Color = Tinta;
                    break;
                }
            }
        }
        var cuenta = sec.AddParagraph($"{l.Ordenadas.Count} registro{(l.Ordenadas.Count == 1 ? "" : "s")}.");
        cuenta.Format.Font.Size = 7;
        cuenta.Format.Font.Color = Gris;
        cuenta.Format.SpaceBefore = Unit.FromPoint(4);
        Final(sec, ctx, util);
    }

    /// <summary>Fila de totales: el texto ocupa las columnas que van antes de la primera que totaliza.</summary>
    private static void Totales(Row row, List<ReporteColumnaVista> cols, string texto, Dictionary<string, decimal> totales)
    {
        var primera = cols.FindIndex(c => c.Totaliza && c.EsNumero);
        if (primera < 0) primera = cols.Count;
        if (primera > 0)
        {
            row.Cells[0].MergeRight = primera - 1;
            var p = row.Cells[0].AddParagraph(texto);
            p.Format.Alignment = ParagraphAlignment.Right;
        }
        for (var i = primera; i < cols.Count; i++)
        {
            var c = cols[i];
            if (!c.Totaliza || !c.EsNumero || !totales.TryGetValue(c.Nombre, out var v)) continue;
            var p = row.Cells[i].AddParagraph(ReporteFormato.Texto(v, c.Formato, typeof(decimal)));
            p.Format.Alignment = Alineacion(c.Alineacion);
        }
    }

    // ── Ficha ──────────────────────────────────────────────────────────────────
    private static void Ficha(Section sec, ReporteContexto ctx, double util)
    {
        var secciones = ReporteSeccionFicha.Armar(ctx);
        var cab = ctx.Datos.Tabla(secciones.FirstOrDefault()?.Resultado ?? 1);
        if (secciones.All(s => s.Tabla is null || s.Tabla.Filas.Count == 0)) { Aviso(sec, "No hay datos para este registro."); return; }

        foreach (var s in secciones)
        {
            if (s.Tabla is null) continue;
            if (!string.IsNullOrWhiteSpace(s.Titulo) && (s.Presentacion != "TAB" || s.Tabla.Filas.Count > 0))
            {
                var t = sec.AddParagraph(s.Titulo!);
                t.Format.Font.Bold = true;
                t.Format.Font.Color = Tinta;
                t.Format.SpaceBefore = Unit.FromPoint(8);
                t.Format.SpaceAfter = Unit.FromPoint(3);
                t.Format.KeepWithNext = true;
            }
            switch (s.Presentacion)
            {
                case "CAM": Campos(sec, s, util); break;
                case "TOT": TotalesFicha(sec, s, util); break;
                default:    TablaFicha(sec, s, util); break;
            }
        }
        Final(sec, ctx, util);
    }

    private static void Campos(Section sec, ReporteSeccionFicha s, double util)
    {
        var campos = s.Campos(false);
        if (campos.Count == 0) return;
        var n = s.NumColumnas;
        var t = sec.AddTable();
        t.Borders.Visible = false;
        var w = util / n;
        for (var i = 0; i < n; i++) t.AddColumn(Unit.FromCentimeter(w));
        Row? row = null;
        var usado = n;
        foreach (var (c, v) in campos)
        {
            var span = Math.Clamp((int)Math.Round(c.Ancho), 1, n);
            if (usado + span > n) { row = t.AddRow(); row.TopPadding = row.BottomPadding = Unit.FromPoint(1.5); usado = 0; }
            var celda = row!.Cells[usado];
            if (span > 1) celda.MergeRight = span - 1;
            var p = celda.AddParagraph();
            p.AddFormattedText(c.Etiqueta + ": ", TextFormat.Bold);
            p.AddText(v);
            usado += span;
        }
        sec.AddParagraph().Format.SpaceAfter = Unit.FromPoint(2);
    }

    private static void TablaFicha(Section sec, ReporteSeccionFicha s, double util)
    {
        var filas = s.Celdas(false);
        if (filas.Count == 0) return;
        var anchos = Anchos(s.Columnas.Select(c => c.Ancho).ToList(), util);
        var t = sec.AddTable();
        t.Borders.Width = 0.25;
        t.Borders.Color = Linea;
        for (var i = 0; i < anchos.Count; i++) t.AddColumn(Unit.FromCentimeter(anchos[i]));
        var h = t.AddRow();
        h.HeadingFormat = true;
        h.Shading.Color = Tinta;
        h.Format.Font.Bold = true;
        h.Format.Font.Color = Colors.White;
        for (var i = 0; i < s.Columnas.Count; i++)
            h.Cells[i].AddParagraph(s.Columnas[i].Etiqueta).Format.Alignment = Alineacion(s.Columnas[i].Alineacion);
        foreach (var f in filas)
        {
            var row = t.AddRow();
            row.TopPadding = row.BottomPadding = Unit.FromPoint(1.2);
            for (var i = 0; i < s.Columnas.Count; i++)
                row.Cells[i].AddParagraph(f[i]).Format.Alignment = Alineacion(s.Columnas[i].Alineacion);
        }
        if (s.Totales() is { } tot)
        {
            var row = t.AddRow();
            row.Format.Font.Bold = true;
            row.Shading.Color = Grupo1;
            Totales(row, s.Columnas, "Total", tot);
        }
        sec.AddParagraph().Format.SpaceAfter = Unit.FromPoint(2);
    }

    /// <summary>Totales de una ficha (valor, impuestos, total): etiqueta y valor a la derecha, la última fila resaltada.</summary>
    private static void TotalesFicha(Section sec, ReporteSeccionFicha s, double util)
    {
        var filas = s.Celdas(false);
        if (filas.Count == 0) return;
        var texto = s.Columnas.Select((c, i) => (c, i)).Where(x => !x.c.EsNumero).Select(x => x.i).ToList();
        var numeros = s.Columnas.Select((c, i) => (c, i)).Where(x => x.c.EsNumero).Select(x => x.i).ToList();
        var anchoTotal = util * 0.42;
        var t = sec.AddTable();
        t.Borders.Visible = false;
        t.Rows.LeftIndent = Unit.FromCentimeter(util - anchoTotal);
        t.AddColumn(Unit.FromCentimeter(anchoTotal * 0.6));
        t.AddColumn(Unit.FromCentimeter(anchoTotal * 0.4));
        for (var k = 0; k < filas.Count; k++)
        {
            var f = filas[k];
            var row = t.AddRow();
            row.TopPadding = row.BottomPadding = Unit.FromPoint(1.2);
            var ultima = k == filas.Count - 1;
            if (ultima) { row.Format.Font.Bold = true; row.Shading.Color = Grupo1; row.Borders.Top.Width = 0.8; row.Borders.Top.Color = Tinta; }
            row.Cells[0].AddParagraph(string.Join(" ", texto.Select(i => f[i]).Where(x => x.Length > 0))).Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].AddParagraph(string.Join(" ", numeros.Select(i => f[i]).Where(x => x.Length > 0))).Format.Alignment = ParagraphAlignment.Right;
        }
        sec.AddParagraph().Format.SpaceAfter = Unit.FromPoint(2);
    }

    /// <summary>El texto libre del pie y las firmas, al final del documento.</summary>
    private static void Final(Section sec, ReporteContexto ctx, double util)
    {
        var pie = ReporteTexto.Reemplazar(ctx.D.DES_PIE, ctx);
        if (!string.IsNullOrWhiteSpace(pie))
        {
            var p = sec.AddParagraph(pie);
            p.Format.SpaceBefore = Unit.FromPoint(8);
        }
        var firmas = ReporteFirmas.De(ctx.D.DES_FIRMAS);
        if (firmas.Count == 0) return;
        var t = sec.AddTable();
        t.Borders.Visible = false;
        t.KeepTogether = true;
        var w = util / firmas.Count;
        foreach (var _ in firmas) t.AddColumn(Unit.FromCentimeter(w));
        var esp = t.AddRow();
        esp.Height = Unit.FromCentimeter(1.6);
        var row = t.AddRow();
        for (var i = 0; i < firmas.Count; i++)
        {
            var p = row.Cells[i].AddParagraph("________________________");
            p.Format.Alignment = ParagraphAlignment.Center;
            var q = row.Cells[i].AddParagraph(firmas[i]);
            q.Format.Alignment = ParagraphAlignment.Center;
            q.Format.Font.Size = 7.5;
        }
    }

    // ── Utilidades ─────────────────────────────────────────────────────────────
    private static void Aviso(Section sec, string texto)
    {
        var p = sec.AddParagraph(texto);
        p.Format.SpaceBefore = Unit.FromCentimeter(1);
        p.Format.Alignment = ParagraphAlignment.Center;
        p.Format.Font.Color = Gris;
        p.Format.Font.Italic = true;
    }

    private static List<double> Anchos(List<double> pesos, double util)
    {
        var suma = pesos.Sum(p => Math.Max(p, 1));
        return pesos.Select(p => util * Math.Max(p, 1) / suma).ToList();
    }

    private static ParagraphAlignment Alineacion(char a) => a switch
    {
        'D' => ParagraphAlignment.Right,
        'C' => ParagraphAlignment.Center,
        _   => ParagraphAlignment.Left
    };
}

/// <summary>Detecta si el logo es una imagen que los tres formatos saben leer (PNG, JPEG, GIF o BMP).</summary>
public static class ReporteImagen
{
    public static bool EsSoportada(byte[] b) => Tipo(b) is not null;

    public static string? Tipo(byte[] b)
    {
        if (b.Length < 8) return null;
        if (b[0] == 0x89 && b[1] == 0x50 && b[2] == 0x4E && b[3] == 0x47) return "png";
        if (b[0] == 0xFF && b[1] == 0xD8) return "jpeg";
        if (b[0] == 0x47 && b[1] == 0x49 && b[2] == 0x46) return "gif";
        if (b[0] == 0x42 && b[1] == 0x4D) return "bmp";
        return null;
    }

    /// <summary>Ancho y alto en píxeles (PNG, JPEG, GIF, BMP); null si no se pudo leer.</summary>
    public static (int Ancho, int Alto)? Medidas(byte[] b)
    {
        try
        {
            switch (Tipo(b))
            {
                case "png": return (Be32(b, 16), Be32(b, 20));
                case "gif": return (b[6] | b[7] << 8, b[8] | b[9] << 8);
                case "bmp": return (BitConverter.ToInt32(b, 18), Math.Abs(BitConverter.ToInt32(b, 22)));
                case "jpeg":
                    var i = 2;
                    while (i + 9 < b.Length)
                    {
                        if (b[i] != 0xFF) { i++; continue; }
                        var marca = b[i + 1];
                        var largo = b[i + 2] << 8 | b[i + 3];
                        if (marca is >= 0xC0 and <= 0xCF and not 0xC4 and not 0xC8 and not 0xCC)
                            return (b[i + 7] << 8 | b[i + 8], b[i + 5] << 8 | b[i + 6]);
                        i += 2 + largo;
                    }
                    return null;
                default: return null;
            }
        }
        catch { return null; }
    }

    private static int Be32(byte[] b, int i) => b[i] << 24 | b[i + 1] << 16 | b[i + 2] << 8 | b[i + 3];
}

/// <summary>Fuentes para PDFsharp. En Windows usa Arial de C:\Windows\Fonts; en Linux, Liberation Sans o DejaVu Sans
/// (mismas medidas que Arial). Se registra una sola vez y solo si nadie registró otro resolvedor antes.
/// El diseño libre usa además una con serifa (Times New Roman, Liberation Serif o DejaVu Serif) y una monoespaciada
/// (Courier New, Liberation Mono o DejaVu Sans Mono); si no están, sale con la de siempre.</summary>
public sealed class ReporteFuentesPdf : IFontResolver
{
    public const string Familia = "Arial";
    public const string FamiliaSerif = "Times New Roman";
    public const string FamiliaMono = "Courier New";
    private static readonly object Candado = new();
    private static bool _listo;
    private readonly Dictionary<string, string> _archivos;

    private ReporteFuentesPdf(Dictionary<string, string> archivos) => _archivos = archivos;

    public static void Asegurar()
    {
        if (_listo) return;
        lock (Candado)
        {
            if (_listo) return;
            if (GlobalFontSettings.FontResolver is null)
                GlobalFontSettings.FontResolver = new ReporteFuentesPdf(Buscar());
            _listo = true;
        }
    }

    private static Dictionary<string, string> Buscar()
    {
        var carpetas = new List<string>();
        var win = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
        if (!string.IsNullOrEmpty(win)) carpetas.Add(win);
        carpetas.Add(Path.Combine(AppContext.BaseDirectory, "Fonts"));
        carpetas.AddRange(new[] { "/usr/share/fonts", "/usr/local/share/fonts", "/Library/Fonts", "/System/Library/Fonts/Supplemental" });

        // Clave: familia (SA sin serifa, SE con serifa, MO monoespaciada) + estilo (R, B, I, BI).
        var candidatos = new Dictionary<string, string[]>
        {
            ["SAR"]  = new[] { "arial.ttf", "LiberationSans-Regular.ttf", "DejaVuSans.ttf", "FreeSans.ttf" },
            ["SAB"]  = new[] { "arialbd.ttf", "LiberationSans-Bold.ttf", "DejaVuSans-Bold.ttf", "FreeSansBold.ttf" },
            ["SAI"]  = new[] { "ariali.ttf", "LiberationSans-Italic.ttf", "DejaVuSans-Oblique.ttf", "FreeSansOblique.ttf" },
            ["SABI"] = new[] { "arialbi.ttf", "LiberationSans-BoldItalic.ttf", "DejaVuSans-BoldOblique.ttf", "FreeSansBoldOblique.ttf" },
            ["SER"]  = new[] { "times.ttf", "LiberationSerif-Regular.ttf", "DejaVuSerif.ttf", "FreeSerif.ttf" },
            ["SEB"]  = new[] { "timesbd.ttf", "LiberationSerif-Bold.ttf", "DejaVuSerif-Bold.ttf", "FreeSerifBold.ttf" },
            ["SEI"]  = new[] { "timesi.ttf", "LiberationSerif-Italic.ttf", "DejaVuSerif-Italic.ttf", "FreeSerifItalic.ttf" },
            ["SEBI"] = new[] { "timesbi.ttf", "LiberationSerif-BoldItalic.ttf", "DejaVuSerif-BoldItalic.ttf", "FreeSerifBoldItalic.ttf" },
            ["MOR"]  = new[] { "cour.ttf", "LiberationMono-Regular.ttf", "DejaVuSansMono.ttf", "FreeMono.ttf" },
            ["MOB"]  = new[] { "courbd.ttf", "LiberationMono-Bold.ttf", "DejaVuSansMono-Bold.ttf", "FreeMonoBold.ttf" },
            ["MOI"]  = new[] { "couri.ttf", "LiberationMono-Italic.ttf", "DejaVuSansMono-Oblique.ttf", "FreeMonoOblique.ttf" },
            ["MOBI"] = new[] { "courbi.ttf", "LiberationMono-BoldItalic.ttf", "DejaVuSansMono-BoldOblique.ttf", "FreeMonoBoldOblique.ttf" }
        };
        var todos = new List<string>();
        foreach (var c in carpetas.Where(Directory.Exists))
        {
            try { todos.AddRange(Directory.EnumerateFiles(c, "*.ttf", SearchOption.AllDirectories)); }
            catch { /* carpeta sin permiso */ }
        }
        var halladas = new Dictionary<string, string>();
        foreach (var (estilo, nombres) in candidatos)
            foreach (var n in nombres)
            {
                var f = todos.FirstOrDefault(x => string.Equals(Path.GetFileName(x), n, StringComparison.OrdinalIgnoreCase));
                if (f is not null) { halladas[estilo] = f; break; }
            }
        if (!halladas.ContainsKey("SAR"))
            throw new InvalidOperationException("No se encontró una fuente para el PDF (Arial, Liberation Sans o DejaVu Sans). Copia arial.ttf en la carpeta Fonts de la API.");
        // Lo que falta sale de lo más parecido: el estilo sin cursiva, la normal de la familia, o la sin serifa.
        foreach (var fam in new[] { "SA", "SE", "MO" })
            foreach (var e in new[] { "R", "B", "I", "BI" })
                if (!halladas.ContainsKey(fam + e))
                    halladas[fam + e] = halladas.GetValueOrDefault(fam + (e == "BI" ? "B" : "R"))
                                        ?? halladas.GetValueOrDefault("SA" + e) ?? halladas["SAR"];
        return halladas;
    }

    /// <summary>La familia que pide el documento: Arial (y cualquier otra) es la sin serifa; Times y "Serif", la con serifa;
    /// Courier y "Mono", la monoespaciada.</summary>
    private static string Codigo(string? familia)
    {
        var f = (familia ?? "").ToUpperInvariant();
        if (f.Contains("TIMES") || f.Contains("SERIF") && !f.Contains("SANS")) return "SE";
        if (f.Contains("COURIER") || f.Contains("MONO")) return "MO";
        return "SA";
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
        => new($"REP#{Codigo(familyName)}{(bold ? "B" : "")}{(italic ? "I" : "")}");

    public byte[]? GetFont(string faceName)
    {
        var clave = faceName.Replace("REP#", "");
        if (clave.Length <= 2) clave += "R";
        return File.ReadAllBytes(_archivos.GetValueOrDefault(clave) ?? _archivos["SAR"]);
    }
}
