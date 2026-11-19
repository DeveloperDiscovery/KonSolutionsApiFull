using ClosedXML.Excel;

namespace KonSolutionsApi.Application.Reportes;

/// <summary>El reporte en Excel (.xlsx) con ClosedXML (licencia MIT). Las fechas y los montos se escriben como
/// fecha y número de verdad (se pueden filtrar y sumar); los subtotales y totales son fórmulas SUBTOTALES, así
/// que si el usuario filtra, se recalculan con lo visible. Sin grupos, la tabla lleva filtros en la fila de títulos.</summary>
public static class ReporteExcel
{
    private static readonly XLColor Tinta  = XLColor.FromHtml("#163A5F");
    private static readonly XLColor Grupo1 = XLColor.FromHtml("#E8EAF6");
    private static readonly XLColor Grupo2 = XLColor.FromHtml("#F3F4F6");
    private static readonly XLColor Linea  = XLColor.FromHtml("#CFD8DC");
    private static readonly XLColor Gris   = XLColor.FromHtml("#78909C");

    public static byte[] Generar(ReporteContexto ctx)
    {
        using var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add(NombreHoja(ctx.D.DES_DISENO));
        ws.Style.Font.FontName = "Arial";
        ws.Style.Font.FontSize = Math.Clamp(ctx.D.NUM_TAMANO_LETRA + 1, 7, 14);
        // La ficha se lee como documento: sin la cuadrícula de Excel.
        ws.ShowGridLines = ctx.EsListado;

        int columnas;
        if (ctx.EsListado) columnas = Listado(ws, ctx);
        else columnas = Ficha(ws, ctx);

        Logo(ws, ctx, columnas);
        Pagina(ws, ctx);
        using var ms = new MemoryStream();
        wb.SaveAs(ms);
        return ms.ToArray();
    }

    // ── Encabezado común ───────────────────────────────────────────────────────
    /// <summary>Título, subtítulo, empresa y filtros arriba. Devuelve la siguiente fila libre.</summary>
    private static int Encabezado(IXLWorksheet ws, ReporteContexto ctx)
    {
        var r = 1;
        var titulo = ctx.ModoPlantilla ? "{{TITULO}}" : ReporteTexto.Reemplazar(ctx.D.DES_TITULO ?? ctx.D.DES_DISENO, ctx);
        var c = ws.Cell(r++, 1);
        c.Value = titulo;
        c.Style.Font.Bold = true;
        c.Style.Font.FontSize = 14;
        c.Style.Font.FontColor = Tinta;
        var sub = ctx.ModoPlantilla ? "{{SUBTITULO}}" : ReporteTexto.Reemplazar(ctx.D.DES_SUBTITULO, ctx);
        if (!string.IsNullOrWhiteSpace(sub)) ws.Cell(r++, 1).Value = sub;
        if (ctx.D.FLG_DATOS_EMPRESA && (ctx.ModoPlantilla || !string.IsNullOrWhiteSpace(ctx.Empresa?.DES_NOMBRE_EMPRESA)))
        {
            var e = ctx.ModoPlantilla ? "{{EMPRESA.NOMBRE}} · RUC {{EMPRESA.RUC}}"
                  : ctx.Empresa!.DES_NOMBRE_EMPRESA + (string.IsNullOrWhiteSpace(ctx.Empresa.NUM_DOCUMENTO_IDENTIDAD) ? "" : " · RUC " + ctx.Empresa.NUM_DOCUMENTO_IDENTIDAD);
            var ce = ws.Cell(r++, 1);
            ce.Value = e;
            ce.Style.Font.FontColor = Gris;
        }
        if (ctx.EsListado && ctx.D.FLG_FILTROS_PIE)
        {
            var f = ctx.ModoPlantilla ? "{{FILTROS}}" : ReporteTexto.Filtros(ctx);
            if (!string.IsNullOrWhiteSpace(f))
            {
                var cf = ws.Cell(r++, 1);
                cf.Value = "Filtros: " + f;
                cf.Style.Font.FontColor = Gris;
            }
        }
        return r + 1;
    }

    // ── Listado ────────────────────────────────────────────────────────────────
    private static int Listado(IXLWorksheet ws, ReporteContexto ctx)
    {
        var l = ReporteListado.Armar(ctx);
        var n = l.Columnas.Count;
        var h = Encabezado(ws, ctx);
        if (n == 0) { ws.Cell(h, 1).Value = "El diseño no tiene columnas visibles."; return 1; }

        for (var i = 0; i < n; i++) Titulo(ws.Cell(h, i + 1), l.Columnas[i]);
        var r = h + 1;
        var primera = r;

        if (ctx.ModoPlantilla)
        {
            for (var i = 0; i < n; i++)
            {
                var c = ws.Cell(r, i + 1);
                c.Value = $"{{{{R1.{l.Columnas[i].Nombre}}}}}";
                Alinear(c, l.Columnas[i].Alineacion);
                if (ReporteFormato.Excel(l.Columnas[i].Formato, l.Columnas[i]) is { } fx) c.Style.NumberFormat.Format = fx;
            }
            r++;
            if (l.HayTotales)
            {
                for (var i = 0; i < n; i++)
                {
                    var col = l.Columnas[i];
                    var c = ws.Cell(r, i + 1);
                    c.Style.Font.Bold = true;
                    c.Style.Fill.BackgroundColor = Grupo1;
                    if (col.Totaliza && col.EsNumero) { c.Value = $"{{{{TOTAL.R1.{col.Nombre}}}}}"; Alinear(c, 'D'); }
                    else if (i == 0) c.Value = "Total";
                }
                r++;
            }
            Anchos(ws, l.Columnas, h, r);
            ws.SheetView.FreezeRows(h);
            ws.PageSetup.SetRowsToRepeatAtTop(h, h);
            return n;
        }

        if (l.Ordenadas.Count == 0) { ws.Cell(r, 1).Value = "No hay datos para los filtros elegidos."; Anchos(ws, l.Columnas, h, r); return n; }

        var inicioGrupo = new Dictionary<int, int>();
        foreach (var f in l.Filas)
        {
            switch (f.Tipo)
            {
                case ReporteFilaTipo.GrupoCabecera:
                {
                    if (f.Nivel == 1 && ctx.D.FLG_SALTO_GRUPO && r > primera) ws.PageSetup.AddHorizontalPageBreak(r - 1);
                    var rango = ws.Range(r, 1, r, n);
                    rango.Merge();
                    rango.Value = f.Texto;
                    rango.Style.Font.Bold = true;
                    rango.Style.Font.FontColor = Tinta;
                    rango.Style.Fill.BackgroundColor = f.Nivel == 1 ? Grupo1 : Grupo2;
                    rango.Style.Alignment.Indent = f.Nivel - 1;
                    inicioGrupo[f.Nivel] = r + 1;
                    r++;
                    break;
                }
                case ReporteFilaTipo.Detalle:
                    for (var i = 0; i < n; i++)
                    {
                        var col = l.Columnas[i];
                        var c = ws.Cell(r, i + 1);
                        var v = l.Tabla!.Valor(f.Fila!, col.Nombre);
                        Escribir(c, v, col, campo => l.Tabla.Valor(f.Fila!, campo));
                    }
                    ws.Range(r, 1, r, n).Style.Border.BottomBorder = XLBorderStyleValues.Hair;
                    ws.Range(r, 1, r, n).Style.Border.BottomBorderColor = Linea;
                    r++;
                    break;
                default:
                {
                    var general = f.Tipo == ReporteFilaTipo.TotalGeneral;
                    var desde = general ? primera : inicioGrupo.GetValueOrDefault(f.Nivel, primera);
                    FilaTotal(ws, l.Columnas, r, f.Texto, desde, r - 1, general);
                    r++;
                    break;
                }
            }
        }
        if (l.Grupos.Count == 0) ws.Range(h, 1, r - 1 - (l.Filas.Any(x => x.Tipo == ReporteFilaTipo.TotalGeneral) ? 1 : 0), n).SetAutoFilter();
        Anchos(ws, l.Columnas, h, Math.Min(r, h + 400));
        ws.SheetView.FreezeRows(h);
        ws.PageSetup.SetRowsToRepeatAtTop(h, h);
        Final(ws, ctx, r + 1, n);
        return n;
    }

    /// <summary>Fila de subtotal o total con SUBTOTALES(9;...) sobre el rango del grupo (las filas de subtotal de
    /// adentro no se suman dos veces: SUBTOTALES las ignora).</summary>
    private static void FilaTotal(IXLWorksheet ws, List<ReporteColumnaVista> cols, int r, string texto, int desde, int hasta, bool general)
    {
        var n = cols.Count;
        var primera = cols.FindIndex(c => c.Totaliza && c.EsNumero);
        if (primera < 0) primera = n;
        if (primera > 0)
        {
            var rango = ws.Range(r, 1, r, primera);
            if (primera > 1) rango.Merge();
            rango.Value = texto;
            rango.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
        }
        for (var i = primera; i < n; i++)
        {
            var col = cols[i];
            if (!col.Totaliza || !col.EsNumero) continue;
            var c = ws.Cell(r, i + 1);
            var letra = ws.Column(i + 1).ColumnLetter();
            c.FormulaA1 = $"SUBTOTAL(9,{letra}{desde}:{letra}{Math.Max(desde, hasta)})";
            c.Style.NumberFormat.Format = ReporteFormato.Excel(col.Formato, col) ?? "#,##0.00";
            Alinear(c, 'D');
        }
        var fila = ws.Range(r, 1, r, n);
        fila.Style.Font.Bold = true;
        if (general) { fila.Style.Fill.BackgroundColor = Grupo1; fila.Style.Font.FontColor = Tinta; }
        fila.Style.Border.TopBorder = general ? XLBorderStyleValues.Medium : XLBorderStyleValues.Thin;
        fila.Style.Border.TopBorderColor = general ? Tinta : Gris;
    }

    // ── Ficha ──────────────────────────────────────────────────────────────────
    private static int Ficha(IXLWorksheet ws, ReporteContexto ctx)
    {
        var secciones = ReporteSeccionFicha.Armar(ctx);
        var cols = Math.Max(2, secciones.Select(s => s.Presentacion == "CAM" ? s.NumColumnas * 2 : s.Columnas.Count).DefaultIfEmpty(2).Max());
        var r = Encabezado(ws, ctx);
        if (!ctx.ModoPlantilla && secciones.All(s => s.Tabla is null || s.Tabla.Filas.Count == 0))
        {
            ws.Cell(r, 1).Value = "No hay datos para este registro.";
            return cols;
        }
        var anchos = new double[cols];
        foreach (var s in secciones)
        {
            if (!ctx.ModoPlantilla && s.Tabla is null) continue;
            if (!string.IsNullOrWhiteSpace(s.Titulo) && (s.Presentacion == "CAM" || ctx.ModoPlantilla || s.Tabla!.Filas.Count > 0))
            {
                var t = ws.Cell(r++, 1);
                t.Value = s.Titulo;
                t.Style.Font.Bold = true;
                t.Style.Font.FontColor = Tinta;
            }
            switch (s.Presentacion)
            {
                case "CAM":
                {
                    var porCampo = Math.Max(2, cols / s.NumColumnas);
                    var usado = s.NumColumnas;
                    foreach (var (c, v) in s.Campos(ctx.ModoPlantilla))
                    {
                        var span = Math.Clamp((int)Math.Round(c.Ancho), 1, s.NumColumnas);
                        if (usado + span > s.NumColumnas) { r++; usado = 0; }
                        var col = usado * porCampo + 1;
                        var et = ws.Cell(r, col);
                        et.Value = c.Etiqueta;
                        et.Style.Font.Bold = true;
                        var fin = col + porCampo * span - 1;
                        var valor = ws.Range(r, col + 1, r, fin);
                        if (fin > col + 1) valor.Merge();
                        valor.Value = v;
                        valor.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        valor.Style.Alignment.WrapText = true;
                        anchos[col - 1] = Math.Max(anchos[col - 1], c.Etiqueta.Length + 2);
                        usado += span;
                    }
                    r += 2;
                    break;
                }
                case "TOT":
                {
                    // Etiqueta en la penúltima columna y el importe (con su tipo) en la última; la última fila resaltada.
                    var etiquetas = s.Columnas.Select((c, i) => (c, i)).Where(x => !x.c.EsNumero).Select(x => x.i).ToList();
                    var numero = s.Columnas.FindLastIndex(c => c.EsNumero);
                    var celdas = s.Celdas(ctx.ModoPlantilla);
                    for (var k = 0; k < celdas.Count; k++)
                    {
                        var f = celdas[k];
                        var e = ws.Cell(r, cols - 1);
                        e.Value = string.Join(" ", etiquetas.Select(i => f[i]).Where(x => x.Length > 0));
                        e.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        var v = ws.Cell(r, cols);
                        if (numero >= 0)
                        {
                            if (ctx.ModoPlantilla) v.Value = f[numero];
                            else
                            {
                                var fila = s.Tabla!.Filas[k];
                                Escribir(v, s.Tabla.Valor(fila, s.Columnas[numero].Nombre), s.Columnas[numero], x => s.Tabla.Valor(fila, x));
                            }
                        }
                        if (k == celdas.Count - 1)
                        {
                            var ult = ws.Range(r, cols - 1, r, cols);
                            ult.Style.Font.Bold = true;
                            ult.Style.Fill.BackgroundColor = Grupo1;
                            ult.Style.Border.TopBorder = XLBorderStyleValues.Medium;
                            ult.Style.Border.TopBorderColor = Tinta;
                        }
                        r++;
                    }
                    r++;
                    break;
                }
                default:
                {
                    var h = r;
                    for (var i = 0; i < s.Columnas.Count; i++) Titulo(ws.Cell(h, i + 1), s.Columnas[i]);
                    r++;
                    if (ctx.ModoPlantilla)
                    {
                        for (var i = 0; i < s.Columnas.Count; i++) ws.Cell(r, i + 1).Value = $"{{{{R{s.Resultado}.{s.Columnas[i].Nombre}}}}}";
                        r++;
                    }
                    else
                    {
                        foreach (var fila in s.Tabla!.Filas)
                        {
                            for (var i = 0; i < s.Columnas.Count; i++)
                                Escribir(ws.Cell(r, i + 1), s.Tabla.Valor(fila, s.Columnas[i].Nombre), s.Columnas[i], x => s.Tabla.Valor(fila, x));
                            ws.Range(r, 1, r, s.Columnas.Count).Style.Border.BottomBorder = XLBorderStyleValues.Hair;
                            ws.Range(r, 1, r, s.Columnas.Count).Style.Border.BottomBorderColor = Linea;
                            r++;
                        }
                        if (s.Columnas.Any(c => c.Totaliza && c.EsNumero) && s.Tabla.Filas.Count > 0)
                            FilaTotal(ws, s.Columnas, r++, "Total", h + 1, r - 2, true);
                    }
                    for (var i = 0; i < s.Columnas.Count; i++)
                    {
                        var largo = new[] { s.Columnas[i].Etiqueta.Length }
                            .Concat(s.Celdas(ctx.ModoPlantilla).Take(200).Select(x => x[i].Length)).Max();
                        anchos[i] = Math.Max(anchos[i], largo + 2);
                    }
                    r++;
                    break;
                }
            }
        }
        for (var i = 0; i < cols; i++) ws.Column(i + 1).Width = Math.Clamp(anchos[i] == 0 ? 12 : anchos[i] * 1.05, 6, 60);
        Final(ws, ctx, r, cols);
        return cols;
    }

    private static void Final(IXLWorksheet ws, ReporteContexto ctx, int r, int cols)
    {
        var pie = ctx.ModoPlantilla ? ctx.D.DES_PIE : ReporteTexto.Reemplazar(ctx.D.DES_PIE, ctx);
        if (!string.IsNullOrWhiteSpace(pie)) ws.Cell(r++, 1).Value = pie;
        var firmas = ReporteFirmas.De(ctx.D.DES_FIRMAS);
        if (firmas.Count == 0) return;
        r += 2;
        var porFirma = Math.Max(1, cols / firmas.Count);
        for (var i = 0; i < firmas.Count; i++)
        {
            var col = i * porFirma + 1;
            var raya = ws.Range(r, col, r, col + porFirma - 1);
            if (porFirma > 1) raya.Merge();
            raya.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            var texto = ws.Range(r + 1, col, r + 1, col + porFirma - 1);
            if (porFirma > 1) texto.Merge();
            texto.Value = firmas[i];
            texto.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }
    }

    // ── Utilidades ─────────────────────────────────────────────────────────────
    private static void Titulo(IXLCell c, ReporteColumnaVista col)
    {
        c.Value = col.Etiqueta;
        c.Style.Font.Bold = true;
        c.Style.Font.FontColor = XLColor.White;
        c.Style.Fill.BackgroundColor = Tinta;
        c.Style.Alignment.WrapText = true;
        c.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        Alinear(c, col.Alineacion);
    }

    private static void Alinear(IXLCell c, char a) => c.Style.Alignment.Horizontal = a switch
    {
        'D' => XLAlignmentHorizontalValues.Right,
        'C' => XLAlignmentHorizontalValues.Center,
        _   => XLAlignmentHorizontalValues.Left
    };

    /// <summary>Escribe el valor con su tipo: número, fecha o texto; con el formato de la columna.</summary>
    internal static void Escribir(IXLCell c, object? v, ReporteColumnaVista col, Func<string, object?>? campo)
    {
        Alinear(c, col.Alineacion);
        if (v is null || v is DBNull) { c.Value = Blank.Value; return; }
        if (ReporteFormato.EsLetras(col.Formato)) { c.Value = ReporteFormato.Texto(v, col.Formato, col.Tipo, campo); return; }
        switch (v)
        {
            case DateTime d:
                c.Value = d;
                c.Style.NumberFormat.Format = ReporteFormato.Excel(col.Formato, col, d) ?? "dd/mm/yyyy";
                return;
            case DateOnly d:
                c.Value = d.ToDateTime(TimeOnly.MinValue);
                c.Style.NumberFormat.Format = ReporteFormato.Excel(col.Formato, col) ?? "dd/mm/yyyy";
                return;
            case DateTimeOffset o:
                c.Value = o.DateTime;
                c.Style.NumberFormat.Format = ReporteFormato.Excel(col.Formato, col, o.DateTime) ?? "dd/mm/yyyy hh:mm";
                return;
            case bool b:
                c.Value = b ? "Sí" : "No";
                return;
            case string s:
                c.Value = s.TrimEnd();
                return;
        }
        if (ReporteFormato.ADecimal(v, out var n))
        {
            c.Value = (double)n;
            if (ReporteFormato.Excel(col.Formato, col) is { } fx) c.Style.NumberFormat.Format = fx;
            return;
        }
        c.Value = ReporteFormato.Texto(v, col.Formato, col.Tipo, campo);
    }

    private static void Anchos(IXLWorksheet ws, List<ReporteColumnaVista> cols, int h, int hasta)
    {
        for (var i = 0; i < cols.Count; i++)
        {
            var largo = cols[i].Etiqueta.Length;
            for (var r = h + 1; r < hasta; r++)
            {
                var c = ws.Cell(r, i + 1);
                if (c.IsMerged()) continue;
                largo = Math.Max(largo, c.HasFormula ? 12 : c.GetFormattedString().Length);
            }
            ws.Column(i + 1).Width = Math.Clamp(largo * 1.1 + 2, 6, 60);
        }
    }

    private static void Logo(IXLWorksheet ws, ReporteContexto ctx, int columnas)
    {
        if (!ctx.D.FLG_LOGO || ctx.Empresa?.IMG_LOGO_EMPRESA is not { Length: > 0 } logo || !ReporteImagen.EsSoportada(logo)) return;
        try
        {
            var (pw, ph) = ReporteImagen.Medidas(logo) ?? (3, 1);
            const int alto = 46;
            var ancho = ph > 0 ? alto * pw / ph : alto * 3;
            var pic = ws.AddPicture(new MemoryStream(logo));
            pic.MoveTo(ws.Cell(1, Math.Max(1, columnas)), 0, 0);
            pic.WithSize(ancho, alto);
        }
        catch { /* sin logo antes que sin reporte */ }
    }

    internal static void Pagina(IXLWorksheet ws, ReporteContexto ctx)
    {
        ws.PageSetup.PageOrientation = string.Equals(ctx.D.COD_ORIENTACION, "H", StringComparison.OrdinalIgnoreCase)
            ? XLPageOrientation.Landscape : XLPageOrientation.Portrait;
        ws.PageSetup.PaperSize = (ctx.D.COD_PAPEL ?? "A4").ToUpperInvariant() switch
        {
            "CAR" => XLPaperSize.LetterPaper,
            "OFI" => XLPaperSize.LegalPaper,
            _     => XLPaperSize.A4Paper
        };
        ws.PageSetup.FitToPages(1, 0);
        ws.PageSetup.Margins.Left = ws.PageSetup.Margins.Right = 0.5;
        ws.PageSetup.Footer.Right.AddText("Página ");
        ws.PageSetup.Footer.Right.AddText(XLHFPredefinedText.PageNumber);
        ws.PageSetup.Footer.Right.AddText(" de ");
        ws.PageSetup.Footer.Right.AddText(XLHFPredefinedText.NumberOfPages);
    }

    internal static string NombreHoja(string? nombre)
    {
        var limpio = new string((nombre ?? "Reporte").Where(ch => ch is not (':' or '\\' or '/' or '?' or '*' or '[' or ']')).ToArray()).Trim();
        if (limpio.Length == 0) limpio = "Reporte";
        return limpio.Length > 31 ? limpio[..31] : limpio;
    }
}

/// <summary>Llena una plantilla propia de Excel. Una fila con marcas de un resultado que se repite se copia una vez
/// por fila (con su formato); {{TOTAL.R2.COLUMNA}} se vuelve SUBTOTALES(9;...) sobre las filas que se llenaron de esa
/// columna. Una celda que es solo una marca recibe el valor con su tipo (número o fecha), no como texto.</summary>
public static class ReporteExcelPlantilla
{
    public static byte[] Llenar(ReporteContexto ctx)
    {
        using var entrada = new MemoryStream(ctx.Plantilla!);
        using var wb = new XLWorkbook(entrada);
        foreach (var ws in wb.Worksheets)
        {
            var usado = ws.RangeUsed();
            if (usado is null) continue;
            var ultimaCol = usado.LastColumn().ColumnNumber();
            var r = usado.FirstRow().RowNumber();
            var ultima = usado.LastRow().RowNumber();
            // (resultado, columna) → letra y rango de filas llenadas, para las marcas TOTAL
            var rangos = new Dictionary<(int, string), (string Letra, int Desde, int Hasta)>();

            while (r <= ultima)
            {
                var texto = string.Concat(Enumerable.Range(1, ultimaCol).Select(c => ws.Cell(r, c).GetString()));
                var n = texto.Contains("{{") ? ReporteMarcas.ResultadoQueRepite(texto, ctx) : null;
                if (n is null) { r++; continue; }
                var filas = ReporteMarcas.FilasDe(ctx, n.Value);
                if (filas.Count == 0) { ws.Row(r).Delete(); ultima--; continue; }
                if (filas.Count > 1)
                {
                    ws.Row(r).InsertRowsBelow(filas.Count - 1);
                    for (var k = 1; k < filas.Count; k++)
                        for (var c = 1; c <= ultimaCol; c++)
                            ws.Cell(r + k, c).CopyFrom(ws.Cell(r, c));
                    ultima += filas.Count - 1;
                }
                for (var c = 1; c <= ultimaCol; c++)
                {
                    var plantilla = ws.Cell(r, c).GetString();
                    if (!plantilla.Contains("{{")) continue;
                    var unica = ReporteMarcas.Marca.Match(plantilla);
                    var sola = unica.Success && unica.Value.Trim() == plantilla.Trim();
                    var partes = sola ? ReporteMarcas.Leer(unica.Groups[1].Value, unica.Groups[2].Success && unica.Groups[2].Value.Length > 0 ? unica.Groups[2].Value : null, ctx.Definicion) : null;
                    if (partes is { Tipo: "R" } && partes.Resultado == n)
                        rangos[(n.Value, partes.Campo.ToUpperInvariant())] = (ws.Column(c).ColumnLetter(), r, r + filas.Count - 1);
                    for (var k = 0; k < filas.Count; k++)
                        Llenar(ws.Cell(r + k, c), plantilla, partes, ctx, n, filas[k]);
                }
                r += filas.Count;
            }

            // El resto: marcas sueltas y totales
            foreach (var cell in ws.CellsUsed().ToList())
            {
                if (cell.HasFormula || cell.DataType != XLDataType.Text) continue;
                var texto = cell.GetString();
                if (!texto.Contains("{{")) continue;
                var unica = ReporteMarcas.Marca.Match(texto);
                var sola = unica.Success && unica.Value.Trim() == texto.Trim();
                var partes = sola ? ReporteMarcas.Leer(unica.Groups[1].Value, unica.Groups[2].Success && unica.Groups[2].Value.Length > 0 ? unica.Groups[2].Value : null, ctx.Definicion) : null;
                if (partes is { Tipo: "T" } && rangos.TryGetValue((partes.Resultado, partes.Campo.ToUpperInvariant()), out var rg))
                {
                    var formato = cell.Style.NumberFormat.Format;
                    cell.FormulaA1 = $"SUBTOTAL(9,{rg.Letra}{rg.Desde}:{rg.Letra}{rg.Hasta})";
                    if (string.IsNullOrEmpty(formato) || formato == "General")
                        cell.Style.NumberFormat.Format = ReporteColumnas.Buscar(ctx, partes.Resultado, partes.Campo) is { } col
                            ? ReporteFormato.Excel(col.Formato, col) ?? "#,##0.00" : "#,##0.00";
                    continue;
                }
                Llenar(cell, texto, partes, ctx, null, null);
            }
        }
        using var salida = new MemoryStream();
        wb.SaveAs(salida);
        return salida.ToArray();
    }

    private static void Llenar(IXLCell cell, string plantilla, ReporteMarcas.Partes? sola, ReporteContexto ctx, int? resultado, object?[]? fila)
    {
        if (sola is { Tipo: "R" or "P" } && sola.Formato is null)
        {
            var crudo = ReporteMarcas.ValorCrudo(sola, ctx, sola.Tipo == "R" && sola.Resultado == resultado ? fila : null);
            var col = sola.Tipo == "R" ? ReporteColumnas.Buscar(ctx, sola.Resultado, sola.Campo) : null;
            var formatoCelda = cell.Style.NumberFormat.Format;
            var general = string.IsNullOrEmpty(formatoCelda) || formatoCelda == "General";
            if (crudo is DateTime d)
            {
                cell.Value = d;
                if (general) cell.Style.NumberFormat.Format = col is null ? "dd/mm/yyyy" : ReporteFormato.Excel(col.Formato, col, d) ?? "dd/mm/yyyy";
                return;
            }
            if (crudo is not (null or string or bool) && (col is null || !ReporteFormato.EsLetras(col.Formato)) && ReporteFormato.ADecimal(crudo, out var n))
            {
                cell.Value = (double)n;
                if (general && col is not null && ReporteFormato.Excel(col.Formato, col) is { } fx) cell.Style.NumberFormat.Format = fx;
                return;
            }
        }
        cell.Value = ReporteMarcas.Reemplazar(plantilla, ctx, resultado, fila);
    }

    /// <summary>Todos los textos de las celdas, para validar las marcas al subir la plantilla.</summary>
    public static IEnumerable<string> Textos(byte[] xlsx)
    {
        using var ms = new MemoryStream(xlsx);
        using var wb = new XLWorkbook(ms);
        return wb.Worksheets.SelectMany(ws => ws.CellsUsed().Where(c => c.DataType == XLDataType.Text).Select(c => c.GetString())).ToList();
    }
}
