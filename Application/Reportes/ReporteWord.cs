using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;

namespace KonSolutionsApi.Application.Reportes;

/// <summary>El reporte en Word (.docx) con OpenXml (licencia MIT). Mismo contenido que el PDF, editable.
/// En modo plantilla escribe las marcas {{R1.COLUMNA}} en vez de los datos: es la plantilla base que el usuario
/// descarga, edita y vuelve a subir.</summary>
public static class ReporteWord
{
    private const string Tinta = "163A5F";
    private const string Grupo1 = "E8EAF6";
    private const string Grupo2 = "F3F4F6";
    private const string Linea = "CFD8DC";
    private const string Gris = "78909C";
    private const int TwipsPorCm = 567;

    public static byte[] Generar(ReporteContexto ctx)
    {
        using var ms = new MemoryStream();
        using (var wd = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
        {
            var main = wd.AddMainDocumentPart();
            main.Document = new Document(new Body());
            Estilos(main, ctx);

            var (ancho, alto) = ReportePapel.Tamano(ctx.D.COD_PAPEL, ctx.D.COD_ORIENTACION);
            var util = (int)((ancho - 2 * ReportePapel.Margen) * TwipsPorCm);

            var header = main.AddNewPart<HeaderPart>();
            header.Header = Encabezado(header, ctx, util);
            var footer = main.AddNewPart<FooterPart>();
            footer.Footer = Pie(ctx, util);

            var body = main.Document.Body!;
            if (ctx.EsListado) Listado(body, ctx, util);
            else Ficha(body, ctx, util);

            body.Append(new SectionProperties(
                new HeaderReference { Type = HeaderFooterValues.Default, Id = main.GetIdOfPart(header) },
                new FooterReference { Type = HeaderFooterValues.Default, Id = main.GetIdOfPart(footer) },
                new PageSize
                {
                    Width = (UInt32Value)(uint)(ancho * TwipsPorCm), Height = (UInt32Value)(uint)(alto * TwipsPorCm),
                    Orient = string.Equals(ctx.D.COD_ORIENTACION, "H", StringComparison.OrdinalIgnoreCase) ? PageOrientationValues.Landscape : PageOrientationValues.Portrait
                },
                new PageMargin
                {
                    Top = (int)(2.9 * TwipsPorCm), Bottom = (int)(1.8 * TwipsPorCm),
                    Left = (UInt32Value)(uint)(ReportePapel.Margen * TwipsPorCm), Right = (UInt32Value)(uint)(ReportePapel.Margen * TwipsPorCm),
                    Header = (UInt32Value)(uint)(0.8 * TwipsPorCm), Footer = (UInt32Value)(uint)(0.7 * TwipsPorCm), Gutter = 0
                }));
            main.Document.Save();
        }
        return ms.ToArray();
    }

    // ── Estilos ────────────────────────────────────────────────────────────────
    private static void Estilos(MainDocumentPart main, ReporteContexto ctx)
    {
        var tam = (Math.Clamp(ctx.D.NUM_TAMANO_LETRA, 6, 14) * 2).ToString();
        var sp = main.AddNewPart<StyleDefinitionsPart>();
        sp.Styles = new Styles(
            new DocDefaults(
                new RunPropertiesDefault(new RunPropertiesBaseStyle(
                    new RunFonts { Ascii = "Arial", HighAnsi = "Arial", ComplexScript = "Arial", EastAsia = "Arial" },
                    new FontSize { Val = tam }, new FontSizeComplexScript { Val = tam }, new Languages { Val = "es-PE" })),
                new ParagraphPropertiesDefault(new ParagraphPropertiesBaseStyle(
                    new SpacingBetweenLines { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto }))),
            new Style(new StyleName { Val = "Normal" }, new PrimaryStyle()) { Type = StyleValues.Paragraph, StyleId = "Normal", Default = true });
        sp.Styles.Save();
    }

    // ── Encabezado y pie ───────────────────────────────────────────────────────
    private static Header Encabezado(HeaderPart part, ReporteContexto ctx, int util)
    {
        var c1 = (int)(util * 0.22); var c3 = (int)(util * 0.26); var c2 = util - c1 - c3;
        var t = TablaNueva(new[] { c1, c2, c3 }, bordes: false);
        var row = new TableRow();

        var celdaLogo = Celda(c1, Parrafo(""));
        if (ctx.D.FLG_LOGO && ctx.Empresa?.IMG_LOGO_EMPRESA is { Length: > 0 } logo && ReporteImagen.EsSoportada(logo))
        {
            try { celdaLogo = Celda(c1, new Paragraph(new Run(Imagen(part, logo, 1.3)))); }
            catch { /* un logo que no se puede leer no impide el reporte */ }
        }
        row.Append(celdaLogo);

        var titulo = ctx.ModoPlantilla ? "{{TITULO}}" : ReporteTexto.Reemplazar(ctx.D.DES_TITULO ?? ctx.D.DES_DISENO, ctx);
        var sub = ctx.ModoPlantilla ? "{{SUBTITULO}}" : ReporteTexto.Reemplazar(ctx.D.DES_SUBTITULO, ctx);
        var centro = new List<OpenXmlElement> { Parrafo(titulo, negrita: true, alin: JustificationValues.Center, tam: 26, color: Tinta) };
        if (!string.IsNullOrWhiteSpace(sub)) centro.Add(Parrafo(sub, alin: JustificationValues.Center, tam: 18));
        row.Append(Celda(c2, centro.ToArray()));

        var der = new List<OpenXmlElement>();
        if (ctx.D.FLG_DATOS_EMPRESA)
        {
            var e = ctx.Empresa;
            var nombre = ctx.ModoPlantilla ? "{{EMPRESA.NOMBRE}}" : e?.DES_NOMBRE_EMPRESA;
            var ruc = ctx.ModoPlantilla ? "{{EMPRESA.RUC}}" : e?.NUM_DOCUMENTO_IDENTIDAD;
            var dir = ctx.ModoPlantilla ? "{{EMPRESA.DIRECCION}}" : e?.DES_DOMICILIO_FISCAL;
            if (!string.IsNullOrWhiteSpace(nombre)) der.Add(Parrafo(nombre, negrita: true, alin: JustificationValues.Right, tam: 15));
            if (!string.IsNullOrWhiteSpace(ruc)) der.Add(Parrafo("RUC " + ruc, alin: JustificationValues.Right, tam: 15));
            if (!string.IsNullOrWhiteSpace(dir)) der.Add(Parrafo(dir, alin: JustificationValues.Right, tam: 15));
        }
        if (der.Count == 0) der.Add(Parrafo(""));
        row.Append(Celda(c3, der.ToArray()));
        t.Append(row);

        var raya = Parrafo("", tam: 4);
        raya.ParagraphProperties ??= new ParagraphProperties();
        raya.ParagraphProperties.Append(new ParagraphBorders(new BottomBorder { Val = BorderValues.Single, Size = 10, Color = Tinta, Space = 1 }));
        return new Header(t, raya);
    }

    private static Footer Pie(ReporteContexto ctx, int util)
    {
        var w = new[] { (int)(util * 0.45), (int)(util * 0.35), util - (int)(util * 0.45) - (int)(util * 0.35) };
        var t = TablaNueva(w, bordes: false);
        var row = new TableRow();
        var filtros = !ctx.EsListado || !ctx.D.FLG_FILTROS_PIE ? "" : ctx.ModoPlantilla ? "Filtros: {{FILTROS}}" : ReporteTexto.Filtros(ctx) is { Length: > 0 } f ? "Filtros: " + f : "";
        row.Append(Celda(w[0], Parrafo(filtros, tam: 13, color: Gris)));
        row.Append(Celda(w[1], Parrafo(ctx.ModoPlantilla ? "Generado por {{USUARIO}} el {{FECHA}}" : ReporteTexto.Generado(ctx), alin: JustificationValues.Center, tam: 13, color: Gris)));
        var pag = new Paragraph(
            new ParagraphProperties(new Justification { Val = JustificationValues.Right }),
            Texto("Página ", 13, Gris),
            new SimpleField(Texto("1", 13, Gris)) { Instruction = " PAGE " },
            Texto(" de ", 13, Gris),
            new SimpleField(Texto("1", 13, Gris)) { Instruction = " NUMPAGES " });
        row.Append(Celda(w[2], pag));
        t.Append(row);
        var linea = Parrafo("", tam: 2);
        linea.ParagraphProperties ??= new ParagraphProperties();
        linea.ParagraphProperties.Append(new ParagraphBorders(new TopBorder { Val = BorderValues.Single, Size = 4, Color = Linea, Space = 1 }));
        return new Footer(linea, t);
    }

    // ── Listado ────────────────────────────────────────────────────────────────
    private static void Listado(Body body, ReporteContexto ctx, int util)
    {
        var l = ReporteListado.Armar(ctx);
        if (l.Columnas.Count == 0) { body.Append(Parrafo("El diseño no tiene columnas visibles.", color: Gris)); return; }
        var anchos = Anchos(l.Columnas.Select(c => c.Ancho).ToList(), util);
        var t = TablaNueva(anchos, bordes: false);
        t.Append(FilaTitulos(l.Columnas, anchos));

        if (ctx.ModoPlantilla)
        {
            // Una fila con las marcas: al llenar la plantilla se repite por cada fila del resultado.
            var r = new TableRow();
            for (var i = 0; i < l.Columnas.Count; i++)
                r.Append(Celda(anchos[i], Parrafo($"{{{{R1.{l.Columnas[i].Nombre}}}}}", alin: Alin(l.Columnas[i].Alineacion))));
            t.Append(r);
            if (l.HayTotales)
            {
                var tot = new TableRow();
                for (var i = 0; i < l.Columnas.Count; i++)
                {
                    var c = l.Columnas[i];
                    var texto = c.Totaliza && c.EsNumero ? $"{{{{TOTAL.R1.{c.Nombre}}}}}" : i == 0 ? "Total" : "";
                    tot.Append(Celda(anchos[i], Parrafo(texto, negrita: true, alin: Alin(c.Alineacion)), Grupo1));
                }
                t.Append(tot);
            }
            body.Append(t);
            body.Append(Parrafo(""));
            Final(body, ctx, util);
            return;
        }

        if (l.Ordenadas.Count == 0) { body.Append(Parrafo("No hay datos para los filtros elegidos.", color: Gris)); return; }
        var primera = true;
        foreach (var f in l.Filas)
        {
            if (f.Tipo == ReporteFilaTipo.GrupoCabecera && f.Nivel == 1 && ctx.D.FLG_SALTO_GRUPO && !primera)
            {
                body.Append(t);
                body.Append(new Paragraph(new Run(new Break { Type = BreakValues.Page })));
                t = TablaNueva(anchos, bordes: false);
                t.Append(FilaTitulos(l.Columnas, anchos));
            }
            primera = false;
            switch (f.Tipo)
            {
                case ReporteFilaTipo.GrupoCabecera:
                {
                    var r = new TableRow();
                    var p = Parrafo(f.Texto, negrita: true, color: Tinta);
                    if (f.Nivel > 1) (p.ParagraphProperties ??= new ParagraphProperties()).Append(new Indentation { Left = "240" });
                    r.Append(Celda(anchos.Sum(), p, f.Nivel == 1 ? Grupo1 : Grupo2, span: anchos.Count));
                    t.Append(r);
                    break;
                }
                case ReporteFilaTipo.Detalle:
                {
                    var r = new TableRow();
                    for (var i = 0; i < l.Columnas.Count; i++)
                    {
                        var c = l.Columnas[i];
                        var v = ReporteFormato.Texto(l.Tabla!.Valor(f.Fila!, c.Nombre), c.Formato, c.Tipo, n => l.Tabla.Valor(f.Fila!, n));
                        var celda = Celda(anchos[i], Parrafo(v, alin: Alin(c.Alineacion)));
                        BordeInferior(celda, Linea, 4);
                        r.Append(celda);
                    }
                    t.Append(r);
                    break;
                }
                default:
                    t.Append(FilaTotales(l.Columnas, anchos, f.Texto, f.Totales, f.Tipo == ReporteFilaTipo.TotalGeneral));
                    break;
            }
        }
        body.Append(t);
        body.Append(Parrafo($"{l.Ordenadas.Count} registro{(l.Ordenadas.Count == 1 ? "" : "s")}.", tam: 14, color: Gris));
        Final(body, ctx, util);
    }

    private static TableRow FilaTitulos(List<ReporteColumnaVista> cols, List<int> anchos)
    {
        var r = new TableRow(new TableRowProperties(new TableHeader()));
        for (var i = 0; i < cols.Count; i++)
            r.Append(Celda(anchos[i], Parrafo(cols[i].Etiqueta, negrita: true, alin: Alin(cols[i].Alineacion), color: "FFFFFF"), Tinta));
        return r;
    }

    private static TableRow FilaTotales(List<ReporteColumnaVista> cols, List<int> anchos, string texto, Dictionary<string, decimal> totales, bool general)
    {
        var r = new TableRow();
        var primera = cols.FindIndex(c => c.Totaliza && c.EsNumero);
        if (primera < 0) primera = cols.Count;
        if (primera > 0)
            r.Append(Celda(anchos.Take(primera).Sum(), Parrafo(texto, negrita: true, alin: JustificationValues.Right), general ? Grupo1 : null, span: primera));
        for (var i = primera; i < cols.Count; i++)
        {
            var c = cols[i];
            var v = c.Totaliza && c.EsNumero && totales.TryGetValue(c.Nombre, out var x) ? ReporteFormato.Texto(x, c.Formato, typeof(decimal)) : "";
            r.Append(Celda(anchos[i], Parrafo(v, negrita: true, alin: Alin(c.Alineacion)), general ? Grupo1 : null));
        }
        foreach (var celda in r.Elements<TableCell>())
            BordeSuperior(celda, general ? Tinta : Gris, general ? 8u : 4u);
        return r;
    }

    // ── Ficha ──────────────────────────────────────────────────────────────────
    private static void Ficha(Body body, ReporteContexto ctx, int util)
    {
        var secciones = ReporteSeccionFicha.Armar(ctx);
        if (!ctx.ModoPlantilla && secciones.All(s => s.Tabla is null || s.Tabla.Filas.Count == 0))
        {
            body.Append(Parrafo("No hay datos para este registro.", color: Gris));
            return;
        }
        foreach (var s in secciones)
        {
            if (!ctx.ModoPlantilla && s.Tabla is null) continue;
            var celdas = s.Presentacion == "CAM" ? null : s.Celdas(ctx.ModoPlantilla);
            if (!string.IsNullOrWhiteSpace(s.Titulo) && (celdas is null || celdas.Count > 0))
            {
                var p = Parrafo(s.Titulo!, negrita: true, color: Tinta);
                (p.ParagraphProperties ??= new ParagraphProperties()).Append(new SpacingBetweenLines { Before = "160", After = "60" });
                body.Append(p);
            }
            switch (s.Presentacion)
            {
                case "CAM": Campos(body, s, util, ctx.ModoPlantilla); break;
                case "TOT": TotalesFicha(body, s, util, celdas!); break;
                default:    TablaFicha(body, s, util, celdas!, ctx.ModoPlantilla); break;
            }
        }
        Final(body, ctx, util);
    }

    private static void Campos(Body body, ReporteSeccionFicha s, int util, bool plantilla)
    {
        var campos = s.Campos(plantilla);
        if (campos.Count == 0) return;
        var n = s.NumColumnas;
        var w = util / n;
        var t = TablaNueva(Enumerable.Repeat(w, n).ToList(), bordes: false);
        TableRow? row = null;
        var usado = n;
        foreach (var (c, v) in campos)
        {
            var span = Math.Clamp((int)Math.Round(c.Ancho), 1, n);
            if (usado + span > n)
            {
                if (row is not null) { Completar(row, n - usado, w); t.Append(row); }
                row = new TableRow();
                usado = 0;
            }
            var p = new Paragraph(new ParagraphProperties(new SpacingBetweenLines { After = "40" }),
                Texto(c.Etiqueta + ": ", null, null, negrita: true), Texto(v, null, null));
            row!.Append(Celda(w * span, p, span: span));
            usado += span;
        }
        if (row is not null) { Completar(row, n - usado, w); t.Append(row); }
        body.Append(t);
        body.Append(Parrafo(""));
    }

    private static void Completar(TableRow row, int faltan, int w)
    {
        if (faltan > 0) row.Append(Celda(w * faltan, Parrafo(""), span: faltan));
    }

    private static void TablaFicha(Body body, ReporteSeccionFicha s, int util, List<List<string>> filas, bool plantilla)
    {
        if (filas.Count == 0) return;
        var anchos = Anchos(s.Columnas.Select(c => c.Ancho).ToList(), util);
        var t = TablaNueva(anchos, bordes: true);
        t.Append(FilaTitulos(s.Columnas, anchos));
        foreach (var f in filas)
        {
            var r = new TableRow();
            for (var i = 0; i < s.Columnas.Count; i++)
                r.Append(Celda(anchos[i], Parrafo(f[i], alin: Alin(s.Columnas[i].Alineacion))));
            t.Append(r);
        }
        if (!plantilla && s.Totales() is { } tot) t.Append(FilaTotales(s.Columnas, anchos, "Total", tot, true));
        body.Append(t);
        body.Append(Parrafo(""));
    }

    private static void TotalesFicha(Body body, ReporteSeccionFicha s, int util, List<List<string>> filas)
    {
        if (filas.Count == 0) return;
        var texto = s.Columnas.Select((c, i) => (c, i)).Where(x => !x.c.EsNumero).Select(x => x.i).ToList();
        var numeros = s.Columnas.Select((c, i) => (c, i)).Where(x => x.c.EsNumero).Select(x => x.i).ToList();
        var ancho = (int)(util * 0.42);
        var t = TablaNueva(new[] { (int)(ancho * 0.6), ancho - (int)(ancho * 0.6) }, bordes: false);
        t.GetFirstChild<TableProperties>()!.Append(new TableJustification { Val = TableRowAlignmentValues.Right });
        for (var k = 0; k < filas.Count; k++)
        {
            var f = filas[k];
            var ultima = k == filas.Count - 1;
            var r = new TableRow();
            var a = Celda((int)(ancho * 0.6), Parrafo(string.Join(" ", texto.Select(i => f[i]).Where(x => x.Length > 0)), negrita: ultima, alin: JustificationValues.Right), ultima ? Grupo1 : null);
            var b = Celda(ancho - (int)(ancho * 0.6), Parrafo(string.Join(" ", numeros.Select(i => f[i]).Where(x => x.Length > 0)), negrita: ultima, alin: JustificationValues.Right), ultima ? Grupo1 : null);
            if (ultima) { BordeSuperior(a, Tinta, 8); BordeSuperior(b, Tinta, 8); }
            r.Append(a, b);
            t.Append(r);
        }
        body.Append(t);
        body.Append(Parrafo(""));
    }

    private static void Final(Body body, ReporteContexto ctx, int util)
    {
        var pie = ctx.ModoPlantilla ? ctx.D.DES_PIE : ReporteTexto.Reemplazar(ctx.D.DES_PIE, ctx);
        if (!string.IsNullOrWhiteSpace(pie)) body.Append(Parrafo(pie));
        var firmas = ReporteFirmas.De(ctx.D.DES_FIRMAS);
        if (firmas.Count == 0) return;
        body.Append(Parrafo(""));
        body.Append(Parrafo(""));
        body.Append(Parrafo(""));
        var w = util / firmas.Count;
        var t = TablaNueva(Enumerable.Repeat(w, firmas.Count).ToList(), bordes: false);
        // una celda por firma: la raya y debajo el texto
        var r = new TableRow(new TableRowProperties(new CantSplit()));
        foreach (var f in firmas)
            r.Append(Celda(w, new OpenXmlElement[] { Parrafo("________________________", alin: JustificationValues.Center), Parrafo(f, alin: JustificationValues.Center, tam: 15) }));
        t.Append(r);
        body.Append(t);
    }

    // ── Utilidades OpenXml ─────────────────────────────────────────────────────
    private static Table TablaNueva(IList<int> anchos, bool bordes)
    {
        var props = new TableProperties(
            new TableWidth { Width = anchos.Sum().ToString(), Type = TableWidthUnitValues.Dxa },
            new TableLayout { Type = TableLayoutValues.Fixed },
            new TableCellMarginDefault(
                new TopMargin { Width = "20", Type = TableWidthUnitValues.Dxa },
                new TableCellLeftMargin { Width = 35, Type = TableWidthValues.Dxa },
                new BottomMargin { Width = "20", Type = TableWidthUnitValues.Dxa },
                new TableCellRightMargin { Width = 35, Type = TableWidthValues.Dxa }));
        if (bordes)
            props.Append(new TableBorders(
                new TopBorder { Val = BorderValues.Single, Size = 4, Color = Linea },
                new LeftBorder { Val = BorderValues.Single, Size = 4, Color = Linea },
                new BottomBorder { Val = BorderValues.Single, Size = 4, Color = Linea },
                new RightBorder { Val = BorderValues.Single, Size = 4, Color = Linea },
                new InsideHorizontalBorder { Val = BorderValues.Single, Size = 4, Color = Linea },
                new InsideVerticalBorder { Val = BorderValues.Single, Size = 4, Color = Linea }));
        var grid = new TableGrid(anchos.Select(w => new GridColumn { Width = w.ToString() }));
        return new Table(props, grid);
    }

    private static TableCell Celda(int ancho, OpenXmlElement contenido, string? fondo = null, int span = 1)
        => Celda(ancho, new[] { contenido }, fondo, span);

    private static TableCell Celda(int ancho, OpenXmlElement[] contenido, string? fondo = null, int span = 1)
    {
        var props = new TableCellProperties(new TableCellWidth { Width = ancho.ToString(), Type = TableWidthUnitValues.Dxa });
        if (span > 1) props.Append(new GridSpan { Val = span });
        if (fondo is not null) props.Append(new Shading { Val = ShadingPatternValues.Clear, Color = "auto", Fill = fondo });
        props.Append(new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center });
        var c = new TableCell(props);
        foreach (var e in contenido) c.Append(e);
        return c;
    }

    private static void BordeInferior(TableCell c, string color, uint tam)
    {
        var p = c.GetFirstChild<TableCellProperties>()!;
        p.InsertAfter(new TableCellBorders(new BottomBorder { Val = BorderValues.Single, Size = tam, Color = color }), p.GetFirstChild<TableCellWidth>());
    }

    private static void BordeSuperior(TableCell c, string color, uint tam)
    {
        var p = c.GetFirstChild<TableCellProperties>()!;
        p.InsertAfter(new TableCellBorders(new TopBorder { Val = BorderValues.Single, Size = tam, Color = color }), p.GetFirstChild<TableCellWidth>());
    }

    private static Paragraph Parrafo(string texto, bool negrita = false, JustificationValues? alin = null, int? tam = null, string? color = null)
    {
        var p = new Paragraph();
        if (alin is not null) p.Append(new ParagraphProperties(new Justification { Val = alin.Value }));
        p.Append(Texto(texto, tam, color, negrita));
        return p;
    }

    private static Run Texto(string texto, int? tam, string? color, bool negrita = false)
    {
        var rp = new RunProperties();
        if (negrita) rp.Append(new Bold());
        if (color is not null) rp.Append(new Color { Val = color });
        if (tam is not null) rp.Append(new FontSize { Val = tam.Value.ToString() });
        var r = new Run();
        if (rp.HasChildren) r.Append(rp);
        var lineas = (texto ?? "").Replace("\r\n", "\n").Split('\n');
        for (var i = 0; i < lineas.Length; i++)
        {
            if (i > 0) r.Append(new Break());
            r.Append(new Text(lineas[i]) { Space = SpaceProcessingModeValues.Preserve });
        }
        return r;
    }

    private static JustificationValues Alin(char a) => a switch
    {
        'D' => JustificationValues.Right,
        'C' => JustificationValues.Center,
        _   => JustificationValues.Left
    };

    private static List<int> Anchos(List<double> pesos, int util)
    {
        var suma = pesos.Sum(p => Math.Max(p, 1));
        var lista = pesos.Select(p => (int)(util * Math.Max(p, 1) / suma)).ToList();
        if (lista.Count > 0) lista[^1] += util - lista.Sum();
        return lista;
    }

    /// <summary>El logo como imagen en línea de altura fija (cm), con el ancho según su proporción.</summary>
    private static Drawing Imagen(OpenXmlPartContainer parte, byte[] bytes, double altoCm)
    {
        var tipo = ReporteImagen.Tipo(bytes) switch
        {
            "jpeg" => ImagePartType.Jpeg,
            "gif"  => ImagePartType.Gif,
            "bmp"  => ImagePartType.Bmp,
            _      => ImagePartType.Png
        };
        ImagePart img = parte switch
        {
            HeaderPart h => h.AddImagePart(tipo),
            MainDocumentPart m => m.AddImagePart(tipo),
            _ => throw new InvalidOperationException("Parte sin imágenes.")
        };
        using (var s = new MemoryStream(bytes)) img.FeedData(s);
        var id = parte.GetIdOfPart(img);
        var (pw, ph) = ReporteImagen.Medidas(bytes) ?? (3, 1);
        long cy = (long)(altoCm * 360000);
        long cx = ph > 0 ? cy * pw / ph : cy * 3;
        return new Drawing(new DW.Inline(
            new DW.Extent { Cx = cx, Cy = cy },
            new DW.EffectExtent { LeftEdge = 0L, TopEdge = 0L, RightEdge = 0L, BottomEdge = 0L },
            new DW.DocProperties { Id = 1U, Name = "Logo" },
            new DW.NonVisualGraphicFrameDrawingProperties(new A.GraphicFrameLocks { NoChangeAspect = true }),
            new A.Graphic(new A.GraphicData(
                new PIC.Picture(
                    new PIC.NonVisualPictureProperties(new PIC.NonVisualDrawingProperties { Id = 0U, Name = "logo" }, new PIC.NonVisualPictureDrawingProperties()),
                    new PIC.BlipFill(new A.Blip { Embed = id }, new A.Stretch(new A.FillRectangle())),
                    new PIC.ShapeProperties(new A.Transform2D(new A.Offset { X = 0L, Y = 0L }, new A.Extents { Cx = cx, Cy = cy }),
                                            new A.PresetGeometry(new A.AdjustValueList()) { Preset = A.ShapeTypeValues.Rectangle })))
                { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" }))
        { DistanceFromTop = 0U, DistanceFromBottom = 0U, DistanceFromLeft = 0U, DistanceFromRight = 0U });
    }
}

/// <summary>Llena una plantilla propia de Word: las filas de tabla con marcas de un resultado que se repite
/// (detalle, cronograma...) se copian una vez por fila; el resto de marcas se reemplaza con la primera fila, los
/// parámetros y los datos de la empresa. Funciona en el cuerpo, los encabezados y los pies.</summary>
public static class ReporteWordPlantilla
{
    public static byte[] Llenar(ReporteContexto ctx)
    {
        using var ms = new MemoryStream();
        ms.Write(ctx.Plantilla!, 0, ctx.Plantilla!.Length);
        using (var wd = WordprocessingDocument.Open(ms, true))
        {
            var main = wd.MainDocumentPart ?? throw new InvalidOperationException("La plantilla de Word no tiene documento.");
            var raices = new List<OpenXmlPartRootElement>();
            if (main.Document is not null) raices.Add(main.Document);
            raices.AddRange(main.HeaderParts.Select(h => h.Header).Where(h => h is not null)!);
            raices.AddRange(main.FooterParts.Select(f => f.Footer).Where(f => f is not null)!);
            foreach (var raiz in raices)
            {
                Unir(raiz);
                foreach (var fila in raiz.Descendants<TableRow>().ToList())
                {
                    if (fila.Parent is null) continue;
                    var n = ReporteMarcas.ResultadoQueRepite(fila.InnerText, ctx);
                    if (n is null) continue;
                    foreach (var datos in ReporteMarcas.FilasDe(ctx, n.Value))
                    {
                        var copia = (TableRow)fila.CloneNode(true);
                        foreach (var p in copia.Descendants<Paragraph>()) Reemplazar(p, ctx, n, datos);
                        fila.InsertBeforeSelf(copia);
                    }
                    fila.Remove();
                }
                foreach (var p in raiz.Descendants<Paragraph>().ToList()) Reemplazar(p, ctx, null, null);
                raiz.Save();
            }
        }
        return ms.ToArray();
    }

    /// <summary>Word parte el texto en varios "runs" (por ortografía, por edición): una marca puede quedar repartida
    /// entre dos o más. Solo esas se juntan, en el run donde empieza la marca: el resto del párrafo (una etiqueta en
    /// negrita, por ejemplo) conserva su formato.</summary>
    private static void Unir(OpenXmlElement raiz)
    {
        foreach (var p in raiz.Descendants<Paragraph>())
        {
            var textos = p.Descendants<Text>().ToList();
            if (textos.Count < 2) continue;
            var todo = string.Concat(textos.Select(t => t.Text));
            if (!todo.Contains("{{")) continue;
            // De atrás hacia adelante, para que cada cambio no corra las posiciones de las marcas que faltan.
            foreach (var m in ReporteMarcas.Marca.Matches(todo).Reverse())
            {
                int inicio = m.Index, fin = m.Index + m.Length, pos = 0;
                var tramo = new List<(Text Nodo, int Desde)>();
                foreach (var t in textos)
                {
                    var largo = t.Text.Length;
                    if (pos + largo > inicio && pos < fin) tramo.Add((t, pos));
                    pos += largo;
                }
                if (tramo.Count < 2) continue;
                var (primero, desdePrimero) = tramo[0];
                var (ultimo, desdeUltimo) = tramo[^1];
                var cola = ultimo.Text[(fin - desdeUltimo)..];
                primero.Text = primero.Text[..(inicio - desdePrimero)] + m.Value;
                primero.Space = SpaceProcessingModeValues.Preserve;
                foreach (var (nodo, _) in tramo.Skip(1).Take(tramo.Count - 2)) nodo.Text = "";
                ultimo.Text = cola;
                ultimo.Space = SpaceProcessingModeValues.Preserve;
            }
        }
    }

    private static void Reemplazar(Paragraph p, ReporteContexto ctx, int? resultado, object?[]? fila)
    {
        foreach (var t in p.Descendants<Text>())
        {
            if (!t.Text.Contains("{{")) continue;
            t.Text = ReporteMarcas.Reemplazar(t.Text, ctx, resultado, fila);
            t.Space = SpaceProcessingModeValues.Preserve;
        }
    }

    /// <summary>Todos los textos de la plantilla (cuerpo, encabezados y pies), por párrafo.</summary>
    public static IEnumerable<string> Textos(byte[] docx)
    {
        using var ms = new MemoryStream(docx);
        using var wd = WordprocessingDocument.Open(ms, false);
        var main = wd.MainDocumentPart ?? throw new InvalidOperationException("El archivo no es un documento de Word válido.");
        var raices = new List<OpenXmlElement?> { main.Document };
        raices.AddRange(main.HeaderParts.Select(h => (OpenXmlElement?)h.Header));
        raices.AddRange(main.FooterParts.Select(f => (OpenXmlElement?)f.Footer));
        var lista = new List<string>();
        foreach (var r in raices.Where(r => r is not null))
            lista.AddRange(r!.Descendants<Paragraph>().Select(p => string.Concat(p.Descendants<Text>().Select(t => t.Text))));
        return lista;
    }
}
