using ClosedXML.Excel;
using KonSolutionsApi.Domain.Models.Reportes;

namespace KonSolutionsApi.Application.Reportes;

// Excel «con formato» de un diseño libre (la opción del formato XLS): la hoja imita el PDF.
//   - Las columnas y las filas salen de dónde empieza y termina cada objeto, en mm: un texto ocupa las celdas que cubre
//     (combinadas), con su letra, color, fondo, bordes y alineación; un cuadro pinta y enmarca sus celdas; una línea es el
//     borde de una fila o de una columna; las imágenes van encima, en su lugar.
//   - Los números y las fechas quedan como números y fechas, con su formato: se pueden sumar y ordenar.
//   - Usa los mismos bloques que el PDF (grupos, condiciones, lo que crece), pero sin páginas: el encabezado de página sale
//     una vez arriba (fijo al desplazarse y repetido al imprimir), el pie de página una vez al final, y lo que depende del
//     número de página no sale. Las filas de cada grupo quedan agrupadas (se pliegan desde el margen) con su pie a la vista.
//   - Las filas cebra salen como el fondo de la sección; un texto girado, girado en su celda (90 hacia arriba o hacia
//     abajo: Excel no pone un texto de cabeza); un código de barras o QR, como imagen, con su texto en la celda de abajo.
//   - Un subinforme sale en las columnas de su objeto (sus objetos corridos a donde está el objeto); una tabla cruzada, con
//     sus números como números.
//   - Un gráfico, como gráfico de Excel sobre las celdas de su objeto, con sus números en la hoja «Datos de gráficos» (ver
//     ReporteBandasGrafico.cs).

public static class ReporteBandasExcel
{
    public static byte[] Generar(ReporteContexto ctx)
    {
        ReporteFuentesPdf.Asegurar();
        var lienzo = ctx.Definicion.Lienzo ?? throw new InvalidOperationException("El diseño libre no tiene lienzo.");
        return new ReporteBandasMotor(ctx, lienzo).Excel();
    }
}

internal sealed partial class ReporteBandasMotor
{
    private const double PuntosMm = 72.0 / 25.4;      // alto de fila: puntos por mm
    private const double PixelesMm = 96.0 / 25.4;     // ancho de columna e imágenes: píxeles por mm
    private const double JuntarX = 1.0, JuntarY = 0.8; // mm: dos bordes más cerca que esto son uno solo
    private static readonly XLColor TintaXls = XLColor.FromHtml("#163A5F");
    private static readonly XLColor ResalteXls = XLColor.FromHtml("#E8EAF6");
    private static readonly XLColor LineaXls = XLColor.FromHtml("#CFD8DC");

    /// <summary>Las filas de la hoja que ocupa un bloque: la primera y los bordes (mm desde arriba del bloque).</summary>
    private sealed class FilasBloque
    {
        public int          Primera { get; init; }
        public List<double> Bordes  { get; init; } = new();
        public int          Ultima  => Primera + Bordes.Count - 2;
    }

    public byte[] Excel()
    {
        var bloques = Armar();
        var tramos = Tramos(bloques);
        var fila0 = _filas.Count > 0 ? 0 : -1;
        var ph = Bandas("PH").SelectMany(b => Partir(new ReporteBandasInstancia { Banda = b, Fila = fila0 }, true)).ToList();
        var pf = Bandas("PF").SelectMany(b => Partir(new ReporteBandasInstancia { Banda = b, Fila = _filas.Count - 1 }, true)).ToList();
        // Como en la primera página: el encabezado del informe, después el de página.
        var corte = bloques.FindIndex(b => Raiz(b).Banda.COD_TIPO_BANDA != "RH");
        if (corte < 0) corte = bloques.Count;
        var orden = bloques.Take(corte).Concat(ph).Concat(bloques.Skip(corte)).Concat(pf).ToList();

        var cols = ColumnasExcel(orden);
        using var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add(ReporteExcel.NombreHoja(_ctx.D.DES_TITULO ?? _ctx.D.DES_DISENO));
        ws.ShowGridLines = false;
        for (var k = 0; k < cols.Count - 1; k++)
            ws.Column(k + 1).Width = Math.Max(0.1, (cols[k + 1] - cols[k]) * PixelesMm / 7.0);

        var filas = new Dictionary<ReporteBandasBloque, FilasBloque>();
        var combinadas = new Combinadas();
        var r = 1;
        foreach (var b in orden)
        {
            var bordes = Dueno(b).BordesFilas(b);
            if (bordes is null) continue;
            var f = new FilasBloque { Primera = r, Bordes = bordes };
            for (var k = 0; k < bordes.Count - 1; k++)
                ws.Row(r + k).Height = Math.Clamp((bordes[k + 1] - bordes[k]) * PuntosMm, 0.75, 409);
            filas[b] = f;
            Celdas(ws, b, f, cols, combinadas);
            r += bordes.Count - 1;
        }
        CrucesExcel(ws, bloques, orden, filas, tramos, cols);
        Plegar(ws, bloques, filas);

        // El encabezado de página: repetido en cada hoja impresa y, si está arriba, fijo al desplazarse.
        var deEncabezado = ph.Where(filas.ContainsKey).Select(b => filas[b]).ToList();
        if (deEncabezado.Count > 0)
        {
            var desde = deEncabezado.Min(x => x.Primera);
            var hasta = deEncabezado.Max(x => x.Ultima);
            ws.PageSetup.SetRowsToRepeatAtTop(desde, hasta);
            if (hasta <= 25) ws.SheetView.FreezeRows(hasta);
        }
        ReporteExcel.Pagina(ws, _ctx);
        if (_graficosXls.Count > 0) HojaGraficos(wb);
        using var ms = new MemoryStream();
        wb.SaveAs(ms);
        if (_graficosXls.Count > 0) PonerGraficos(ms, ws.Name);
        return ms.ToArray();
    }

    // ── La cuadrícula ───────────────────────────────────────────────────────────
    private static List<double> Juntar(IEnumerable<double> valores, double tolerancia)
    {
        var res = new List<double>();
        foreach (var v in valores.Select(v => Math.Round(v, 2)).OrderBy(v => v))
            if (res.Count == 0 || v - res[^1] >= tolerancia) res.Add(v);
        return res;
    }

    private static int Cercano(List<double> bordes, double v)
    {
        var mejor = 0;
        for (var i = 1; i < bordes.Count; i++)
            if (Math.Abs(bordes[i] - v) < Math.Abs(bordes[mejor] - v)) mejor = i;
        return mejor;
    }

    /// <summary>¿El objeto sale en la hoja? Lo que depende del número de página no; un vacío que se suprime, tampoco.</summary>
    private static bool SaleEnExcel(ReporteBandasBloque b, ReporteLienzoObjeto o)
    {
        if (!b.Textos.TryGetValue(o.NUM_OBJETO, out var t)) return true;
        if (t.DePagina) return false;
        return !(string.IsNullOrEmpty(t.Texto) && o.FLG_SUPRIMIR_VACIO);
    }

    /// <summary>Dónde empieza el objeto dentro del bloque y cuánto mide de alto (lo que creció incluido).</summary>
    private static (double Y, double Alto) Vertical(ReporteBandasBloque b, ReporteLienzoObjeto o)
    {
        var y = b.PosY.GetValueOrDefault(o.NUM_OBJETO, Math.Max(0, (double)o.NUM_Y - b.Desde));
        var h = b.Altos.TryGetValue(o.NUM_OBJETO, out var a) ? a
              : b.Textos.TryGetValue(o.NUM_OBJETO, out var t) ? Math.Max(t.Alto, 0) : (double)o.NUM_ALTO;
        return (y, h);
    }

    /// <summary>El alto del texto debajo de un código de barras (0 en un QR o sin texto), en mm.</summary>
    private double AltoTextoCodigo(ReporteLienzoObjeto o)
        => o.COD_TIPO_OBJETO == "BAR" && o.FLG_TEXTO_CODIGO && !string.Equals(o.COD_CODIGO, "QR", StringComparison.OrdinalIgnoreCase)
            ? (double)(o.NUM_TAMANO_LETRA ?? _ctx.D.NUM_TAMANO_LETRA) * 25.4 / 72 * 1.2 : 0;

    /// <summary>Dónde empieza un objeto en la hoja (mm desde el margen): en un subinforme, corrido a donde está su objeto.</summary>
    private double X(ReporteLienzoObjeto o) => (double)o.NUM_X + _dx;

    private List<double> ColumnasExcel(List<ReporteBandasBloque> orden)
    {
        var xs = new List<double> { 0, _anchoUtil };
        var tablas = new HashSet<ReporteBandasTabla>();
        foreach (var b in orden) Dueno(b).BordesX(b, xs, tablas);
        foreach (var o in _l.Objetos.Where(Cruza))
        {
            xs.Add((double)o.NUM_X);
            if (o.COD_TIPO_OBJETO == "CUA") xs.Add((double)(o.NUM_X + o.NUM_ANCHO));
        }
        return Juntar(xs.Select(v => Math.Max(0, v)), JuntarX);
    }

    /// <summary>Dónde empiezan y terminan los objetos de un bloque. Lo de un subinforme no sale de la caja de su objeto.</summary>
    private void BordesX(ReporteBandasBloque b, List<double> xs, HashSet<ReporteBandasTabla> tablas)
    {
        double C(double x) => _esSub ? Math.Clamp(x, _dx, _dx + _anchoCaja) : x;
        if (_esSub)
        {
            xs.Add(_dx);
            xs.Add(_dx + _anchoCaja);
        }
        if (b.Tipo == ReporteBloqueTipo.Estatico)
            foreach (var o in b.Objetos.Where(o => SaleEnExcel(b, o)))
            {
                xs.Add(C(X(o)));
                if (!(o.COD_TIPO_OBJETO == "LIN" && o.NUM_ANCHO == 0)) xs.Add(C(X(o) + (double)o.NUM_ANCHO));
            }
        else if (b.Tipo == ReporteBloqueTipo.Aviso && b.Objeto is { } a)
        {
            xs.Add(C(X(a)));
            xs.Add(C(X(a) + (double)a.NUM_ANCHO));
        }
        else if (b.Tabla is { } t && tablas.Add(t))
        {
            var x = X(t.Objeto);
            xs.Add(C(x));
            foreach (var w in t.Anchos) xs.Add(C(x += w));
        }
    }

    /// <summary>Los bordes de las filas de un bloque (mm); null si no ocupa filas.</summary>
    private List<double>? BordesFilas(ReporteBandasBloque b)
    {
        if (b.Tipo != ReporteBloqueTipo.Estatico) return b.Alto > 0.01 ? new List<double> { 0, b.Alto } : null;
        var visibles = b.Objetos.Where(o => SaleEnExcel(b, o)).ToList();
        if (b.Alto <= 0.01) return null;
        var ys = new List<double> { 0, b.Alto };
        foreach (var o in visibles)
        {
            var (y, h) = Vertical(b, o);
            ys.Add(Math.Clamp(y, 0, b.Alto));
            if (h > 0) ys.Add(Math.Clamp(y + h, 0, b.Alto));
            // Un código de barras con texto: una fila para las barras y otra para el texto.
            if (AltoTextoCodigo(o) is > 0 and var th && h > th) ys.Add(Math.Clamp(y + h - th, 0, b.Alto));
        }
        var res = Juntar(ys, JuntarY);
        if (res.Count < 2) res.Add(b.Alto);
        // El último borde es el de abajo del bloque, aunque haya caído cerca de otro.
        res[^1] = Math.Max(res[^1], b.Alto);
        return res;
    }

    private static (int C1, int C2) Columnas(List<double> cols, double x, double ancho)
    {
        var c1 = Math.Min(Cercano(cols, x) + 1, cols.Count - 1);
        var c2 = Math.Max(c1, Math.Min(Cercano(cols, x + ancho), cols.Count - 1));
        return (c1, c2);
    }

    private static (int R1, int R2) Filas(FilasBloque f, double y, double alto)
    {
        var r1 = Math.Min(f.Primera + Cercano(f.Bordes, y), f.Ultima);
        var r2 = Math.Min(Math.Max(r1, f.Primera + Cercano(f.Bordes, y + alto) - 1), f.Ultima);
        return (r1, r2);
    }

    /// <summary>Las celdas ya combinadas, por fila: un rango nuevo que choca con una no se combina (se juntan los textos).</summary>
    private sealed class Combinadas
    {
        private readonly Dictionary<int, List<(int C1, int C2)>> _porFila = new();

        public bool Choca(int r1, int c1, int r2, int c2)
        {
            for (var r = r1; r <= r2; r++)
                if (_porFila.TryGetValue(r, out var l) && l.Exists(m => c1 <= m.C2 && c2 >= m.C1)) return true;
            return false;
        }

        public void Agregar(int r1, int c1, int r2, int c2)
        {
            for (var r = r1; r <= r2; r++)
            {
                if (!_porFila.TryGetValue(r, out var l)) _porFila[r] = l = new();
                l.Add((c1, c2));
            }
        }
    }

    private static XLBorderStyleValues Trazo(decimal? grosor) => (double)(grosor ?? 0.5m) switch
    {
        <= 0     => XLBorderStyleValues.None,
        <= 0.3   => XLBorderStyleValues.Hair,
        <= 0.9   => XLBorderStyleValues.Thin,
        <= 1.75  => XLBorderStyleValues.Medium,
        _        => XLBorderStyleValues.Thick
    };

    private static XLColor ColorXls(string? hex, XLColor defecto)
    {
        if (string.IsNullOrWhiteSpace(hex)) return defecto;
        try { return XLColor.FromHtml(hex.Trim()); } catch { return defecto; }
    }

    private static string Letra(string? fuente) => (fuente ?? "SANS").ToUpperInvariant() switch
    {
        "SERIF" => "Times New Roman",
        "MONO"  => "Courier New",
        _       => "Arial"
    };

    // ── Un bloque ───────────────────────────────────────────────────────────────
    private void Celdas(IXLWorksheet ws, ReporteBandasBloque b, FilasBloque f, List<double> cols, Combinadas combinadas)
    {
        if (b.Motor is { } m && m != this)
        {
            // Detrás de un subinforme sigue el fondo de la banda que lo tiene; lo demás lo pone su motor.
            var deAfuera = FondoBanda(b.Anfitrion!, 1, 1);
            if (!string.IsNullOrEmpty(deAfuera)) ws.Range(f.Primera, 1, f.Ultima, cols.Count - 1).Style.Fill.BackgroundColor = ColorXls(deAfuera, XLColor.NoColor);
            m.Celdas(ws, b, f, cols, combinadas);
            return;
        }
        var fondo = FondoBanda(b.Inst, 1, 1);
        if (!string.IsNullOrEmpty(fondo))
        {
            var (fc1, fc2) = _esSub ? Columnas(cols, _dx, _anchoCaja) : (1, cols.Count - 1);
            ws.Range(f.Primera, fc1, f.Ultima, fc2).Style.Fill.BackgroundColor = ColorXls(fondo, XLColor.NoColor);
        }
        if (b.Tipo == ReporteBloqueTipo.Aviso)
        {
            AvisoExcel(ws, b, f, cols, combinadas);
            return;
        }
        if (b.Tipo != ReporteBloqueTipo.Estatico)
        {
            FilaTablaExcel(ws, b, f.Primera, cols, combinadas);
            return;
        }
        var objs = b.Objetos.Where(o => SaleEnExcel(b, o)).ToList();
        foreach (var o in objs.Where(o => o.COD_TIPO_OBJETO == "CUA")) CuadroExcel(ws, b, o, f, cols);
        foreach (var o in objs.Where(o => o.COD_TIPO_OBJETO != "BAR" && b.Textos.ContainsKey(o.NUM_OBJETO))) TextoExcel(ws, b, o, f, cols, combinadas);
        foreach (var o in objs.Where(o => o.COD_TIPO_OBJETO == "BAR")) CodigoExcel(ws, b, o, f, cols, combinadas);
        foreach (var o in objs.Where(o => o.COD_TIPO_OBJETO == "LIN")) LineaExcel(ws, b, o, f, cols);
        foreach (var o in objs.Where(o => o.COD_TIPO_OBJETO == "IMG")) ImagenExcel(ws, b, o, f, cols);
        foreach (var o in objs.Where(o => o.COD_TIPO_OBJETO == "GRA")) GraficoExcel(ws, b, o, f, cols);
    }

    private void CuadroExcel(IXLWorksheet ws, ReporteBandasBloque b, ReporteLienzoObjeto o, FilasBloque f, List<double> cols)
    {
        var (y, h) = Vertical(b, o);
        var (r1, r2) = Filas(f, y, h);
        var (c1, c2) = Columnas(cols, X(o), (double)o.NUM_ANCHO);
        var lugar = Lugar(b.Inst, 1, 1);
        var rango = ws.Range(r1, c1, r2, c2);
        var fondo = _f.Color(ReporteFormulasDatos.Objeto, o.NUM_OBJETO, "FONDO", lugar) ?? o.DES_COLOR_FONDO;
        if (!string.IsNullOrEmpty(fondo)) rango.Style.Fill.BackgroundColor = ColorXls(fondo, XLColor.NoColor);
        var trazo = Trazo(o.NUM_GROSOR_BORDE);
        if (trazo == XLBorderStyleValues.None) return;
        rango.Style.Border.OutsideBorder = trazo;
        rango.Style.Border.OutsideBorderColor = ColorXls(_f.Color(ReporteFormulasDatos.Objeto, o.NUM_OBJETO, "COLOR", lugar) ?? o.DES_COLOR_BORDE, XLColor.Black);
    }

    private void LineaExcel(IXLWorksheet ws, ReporteBandasBloque b, ReporteLienzoObjeto o, FilasBloque f, List<double> cols)
    {
        var (y, h) = Vertical(b, o);
        var trazo = Trazo(o.NUM_GROSOR_BORDE);
        if (trazo == XLBorderStyleValues.None) return;
        var color = ColorXls(_f.Color(ReporteFormulasDatos.Objeto, o.NUM_OBJETO, "COLOR", Lugar(b.Inst, 1, 1)) ?? o.DES_COLOR_BORDE ?? o.DES_COLOR, XLColor.Black);
        if ((double)o.NUM_ANCHO >= h)
        {
            // Horizontal: el borde de arriba de la fila donde cae (o el de abajo de la última).
            var (c1, c2) = Columnas(cols, X(o), (double)o.NUM_ANCHO);
            var e = Cercano(f.Bordes, y);
            if (e < f.Bordes.Count - 1)
            {
                var rango = ws.Range(f.Primera + e, c1, f.Primera + e, c2);
                rango.Style.Border.TopBorder = trazo;
                rango.Style.Border.TopBorderColor = color;
            }
            else
            {
                var rango = ws.Range(f.Ultima, c1, f.Ultima, c2);
                rango.Style.Border.BottomBorder = trazo;
                rango.Style.Border.BottomBorderColor = color;
            }
            return;
        }
        // Vertical: el borde izquierdo de la columna donde cae (o el derecho de la última).
        var (r1, r2) = Filas(f, y, h);
        LineaVertical(ws, cols, X(o), r1, r2, trazo, color);
    }

    private static void LineaVertical(IXLWorksheet ws, List<double> cols, double x, int r1, int r2, XLBorderStyleValues trazo, XLColor color)
    {
        var ce = Cercano(cols, x);
        if (ce < cols.Count - 1)
        {
            var rango = ws.Range(r1, ce + 1, r2, ce + 1);
            rango.Style.Border.LeftBorder = trazo;
            rango.Style.Border.LeftBorderColor = color;
        }
        else
        {
            var rango = ws.Range(r1, ce, r2, ce);
            rango.Style.Border.RightBorder = trazo;
            rango.Style.Border.RightBorderColor = color;
        }
    }

    private void TextoExcel(IXLWorksheet ws, ReporteBandasBloque b, ReporteLienzoObjeto o, FilasBloque f, List<double> cols, Combinadas combinadas)
    {
        var t = b.Textos[o.NUM_OBJETO];
        var (y, h) = Vertical(b, o);
        var (r1, r2) = Filas(f, y, h);
        var (c1, c2) = Columnas(cols, X(o), (double)o.NUM_ANCHO);
        var celda = ws.Cell(r1, c1);
        // Un rango solo si ocupa más de una celda: crear rangos en ClosedXML cuesta (con miles de filas se nota).
        var rango = r2 > r1 || c2 > c1 ? ws.Range(r1, c1, r2, c2) : null;
        var ocupada = !celda.IsEmpty();
        if (rango is not null && !combinadas.Choca(r1, c1, r2, c2))
        {
            rango.Merge();
            combinadas.Agregar(r1, c1, r2, c2);
        }
        // Dos textos que caen en la misma celda se juntan, para no perder ninguno.
        if (ocupada) { if (t.Texto.Length > 0) celda.Value = celda.GetString() + " " + t.Texto; }
        else ValorExcel(celda, o, b.Inst, t.Texto);

        var lugar = Lugar(b.Inst, 1, 1);
        var st = rango?.Style ?? celda.Style;
        st.Font.FontName = Letra(o.DES_FUENTE);
        st.Font.FontSize = (double)(o.NUM_TAMANO_LETRA ?? _ctx.D.NUM_TAMANO_LETRA);
        st.Font.Bold = t.Negrita ?? o.FLG_NEGRITA;
        st.Font.Italic = o.FLG_CURSIVA;
        st.Font.Underline = o.FLG_SUBRAYADO ? XLFontUnderlineValues.Single : XLFontUnderlineValues.None;
        st.Font.FontColor = ColorXls(_f.Color(ReporteFormulasDatos.Objeto, o.NUM_OBJETO, "COLOR", lugar) ?? o.DES_COLOR, XLColor.Black);
        var fondo = _f.Color(ReporteFormulasDatos.Objeto, o.NUM_OBJETO, "FONDO", lugar) ?? o.DES_COLOR_FONDO;
        if (!string.IsNullOrEmpty(fondo)) st.Fill.BackgroundColor = ColorXls(fondo, XLColor.NoColor);
        st.Alignment.Horizontal = (o.COD_ALINEACION ?? "I").ToUpperInvariant() switch
        {
            "C" => XLAlignmentHorizontalValues.Center,
            "D" => XLAlignmentHorizontalValues.Right,
            "J" => XLAlignmentHorizontalValues.Justify,
            _   => XLAlignmentHorizontalValues.Left
        };
        st.Alignment.Vertical = XLAlignmentVerticalValues.Top;
        st.Alignment.WrapText = o.FLG_PUEDE_CRECER || t.Texto.Contains('\n') || r2 > r1;
        // Excel gira hasta 90 grados hacia arriba (90) o hacia abajo (-90); de cabeza, no.
        if (Giro(o) is 90 or 270)
        {
            st.Alignment.TextRotation = Giro(o) == 90 ? 90 : -90;
            st.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        }
        var bordes = (o.DES_BORDES ?? "").ToUpperInvariant();
        if (bordes.Length == 0) return;
        var trazo = Trazo(o.NUM_GROSOR_BORDE);
        var color = ColorXls(o.DES_COLOR_BORDE, XLColor.Black);
        if (bordes.Contains('A')) { st.Border.TopBorder = trazo; st.Border.TopBorderColor = color; }
        if (bordes.Contains('B')) { st.Border.BottomBorder = trazo; st.Border.BottomBorderColor = color; }
        if (bordes.Contains('I')) { st.Border.LeftBorder = trazo; st.Border.LeftBorderColor = color; }
        if (bordes.Contains('D')) { st.Border.RightBorder = trazo; st.Border.RightBorderColor = color; }
    }

    /// <summary>El valor de la celda: un número o una fecha con su formato de Excel, o el texto tal como sale en el PDF.</summary>
    private void ValorExcel(IXLCell celda, ReporteLienzoObjeto o, ReporteBandasInstancia inst, string texto)
    {
        if (string.IsNullOrEmpty(texto)) return;            // vacío o suprimido (repetido)
        var (crudo, formato) = Crudo(o, inst);
        switch (crudo)
        {
            case DateTime d:
                celda.Value = d;
                celda.Style.NumberFormat.Format = formato ?? "dd/mm/yyyy";
                return;
            case DateTimeOffset d:
                celda.Value = d.DateTime;
                celda.Style.NumberFormat.Format = formato ?? "dd/mm/yyyy hh:mm";
                return;
            case not null when crudo is not (string or bool) && formato is not null && ReporteFormato.ADecimal(crudo, out var n):
                celda.Value = (double)n;
                celda.Style.NumberFormat.Format = formato;
                return;
        }
        celda.Value = texto;
    }

    /// <summary>El valor sin formato de un campo, resumen o fórmula, y el formato de Excel con que se muestra; (null,
    /// null) si va como texto (textos, especiales, importes en letras).</summary>
    private (object? Valor, string? Formato) Crudo(ReporteLienzoObjeto o, ReporteBandasInstancia inst)
    {
        switch (o.COD_TIPO_OBJETO)
        {
            case "CAM":
            {
                var r = o.NUM_RESULTADO ?? 1;
                var col = o.DES_ORIGEN ?? "";
                ReporteTabla? t;
                object?[]? fila;
                if (_tabla is not null && r == _tabla.Numero)
                {
                    t = _tabla;
                    fila = inst.Fila >= 0 && inst.Fila < _filas.Count ? _filas[inst.Fila] : null;
                }
                else
                {
                    t = _ctx.Datos.Tabla(r);
                    fila = t is { Filas.Count: > 0 } ? t.Filas[0] : null;
                }
                if (t is null || fila is null || t.Valor(fila, col) is not { } crudo || crudo is DBNull) return (null, null);
                var vista = Vista(r, col);
                var formato = ReporteColumnas.Vacio(o.DES_FORMATO) ?? vista?.Formato;
                if (ReporteFormato.EsLetras(formato)) return (null, null);
                return (crudo, ReporteFormato.Excel(formato, vista ?? new ReporteColumnaVista { Tipo = crudo.GetType() }, crudo));
            }
            case "RES":
            {
                var col = o.DES_ORIGEN ?? "";
                var funcion = (o.COD_FUNCION ?? "SUM").ToUpperInvariant();
                if (_f.Resumen(funcion, col, o.NUM_NIVEL ?? 0, Lugar(inst)) is not { } crudo) return (null, null);
                var vista = _tabla is null ? null : Vista(_tabla.Numero, col);
                var propio = ReporteColumnas.Vacio(o.DES_FORMATO);
                string? formato = funcion switch
                {
                    "CNT" or "CND" => propio ?? "#,##0",
                    "SUM" or "AVG" => propio ?? vista?.Formato ?? (vista is not null && ReporteFormato.EsEntero(vista.Tipo) && funcion == "SUM" ? "#,##0" : "#,##0.00"),
                    _ => propio ?? vista?.Formato
                };
                if (ReporteFormato.EsLetras(formato))
                {
                    if (funcion is "MAX" or "MIN") return (null, null);
                    formato = "#,##0.00";
                }
                var tipo = funcion is "MAX" or "MIN" ? vista?.Tipo ?? crudo.GetType() : typeof(decimal);
                return (crudo, ReporteFormato.Excel(formato, new ReporteColumnaVista { Tipo = tipo, Formato = formato }, crudo));
            }
            case "FOR":
            {
                var v = _f.Formula(o.DES_ORIGEN, Lugar(inst, 1, 1));
                var propio = ReporteColumnas.Vacio(o.DES_FORMATO);
                switch (v)
                {
                    case decimal d when !ReporteFormato.EsLetras(propio):
                    {
                        var formato = propio ?? (d == decimal.Truncate(d) ? "#,##0" : "#,##0.00");
                        return (d, ReporteFormato.Excel(formato, new ReporteColumnaVista { Tipo = typeof(decimal) }, d));
                    }
                    case DateTime f:
                        return (f, ReporteFormato.Excel(propio, new ReporteColumnaVista { Tipo = typeof(DateTime) }, f));
                    default:
                        return (null, null);
                }
            }
            default:
                return (null, null);
        }
    }

    private void ImagenExcel(IXLWorksheet ws, ReporteBandasBloque b, ReporteLienzoObjeto o, FilasBloque f, List<double> cols)
    {
        var bytes = string.Equals(o.DES_ORIGEN, "LOGO_APLICACION", StringComparison.OrdinalIgnoreCase) ? _ctx.Empresa?.IMG_LOGO_APLICACION
            : string.Equals(o.DES_ORIGEN, "LOGO", StringComparison.OrdinalIgnoreCase) || o.IMG_IMAGEN is not { Length: > 0 }
            ? _ctx.Empresa?.IMG_LOGO_EMPRESA : o.IMG_IMAGEN;
        if (bytes is not { Length: > 0 } || !ReporteImagen.EsSoportada(bytes) || ReporteImagen.Medidas(bytes) is not { } medidas) return;
        var (iw, ih) = medidas;
        if (iw <= 0 || ih <= 0) return;
        var (y, h) = Vertical(b, o);
        var w = (double)o.NUM_ANCHO;
        // Entera en su recuadro, sin deformarse, alineada como en el PDF.
        var escala = Math.Min(w / iw, h / ih);
        var (dw, dh) = (iw * escala, ih * escala);
        var x = X(o) + (o.COD_ALINEACION ?? "C").ToUpperInvariant() switch { "I" => 0, "D" => w - dw, _ => (w - dw) / 2 };
        PonerImagen(ws, bytes, f, cols, x, y + (h - dh) / 2, dw, dh);
    }

    /// <summary>Una imagen encima de las celdas, en su lugar y con su tamaño (mm).</summary>
    private static void PonerImagen(IXLWorksheet ws, byte[] bytes, FilasBloque f, List<double> cols, double x, double yy, double dw, double dh)
    {
        var ce = Math.Min(Cercano(cols, x), cols.Count - 2);
        if (cols[ce] > x && ce > 0) ce--;
        var fe = Cercano(f.Bordes, yy);
        if (f.Bordes[fe] > yy && fe > 0) fe--;
        try
        {
            var pic = ws.AddPicture(new MemoryStream(bytes));
            pic.MoveTo(ws.Cell(f.Primera + Math.Min(fe, f.Bordes.Count - 2), ce + 1),
                       (int)Math.Round((x - cols[ce]) * PixelesMm), (int)Math.Round((yy - f.Bordes[fe]) * PixelesMm));
            pic.WithSize((int)Math.Max(1, Math.Round(dw * PixelesMm)), (int)Math.Max(1, Math.Round(dh * PixelesMm)));
        }
        catch { /* una imagen que no se puede poner no impide el Excel */ }
    }

    /// <summary>Un código de barras o QR: una imagen PNG (armada aquí, al doble de resolución de la pantalla) en su lugar.
    /// Debajo de las barras, el texto en su celda; si no se puede codificar, el motivo en rojo.</summary>
    private void CodigoExcel(IXLWorksheet ws, ReporteBandasBloque b, ReporteLienzoObjeto o, FilasBloque f, List<double> cols, Combinadas combinadas)
    {
        if (!b.Textos.TryGetValue(o.NUM_OBJETO, out var t) || string.IsNullOrEmpty(t.Texto)) return;
        var tipo = (o.COD_CODIGO ?? "C128").ToUpperInvariant();
        var (y, h) = Vertical(b, o);
        var w = (double)o.NUM_ANCHO;
        var x = X(o);
        var lugar = Lugar(b.Inst, 1, 1);
        var tinta = Rgb(_f.Color(ReporteFormulasDatos.Objeto, o.NUM_OBJETO, "COLOR", lugar) ?? o.DES_COLOR, (0, 0, 0));
        var papel = Rgb(_f.Color(ReporteFormulasDatos.Objeto, o.NUM_OBJETO, "FONDO", lugar) ?? o.DES_COLOR_FONDO, (255, 255, 255));
        string? error;
        if (tipo == "QR")
        {
            var qr = ReporteCodigos.Qr(t.Texto, out error);
            if (qr is not null)
            {
                var lado = Math.Min(w, h);
                var escala = Math.Max(2, (int)Math.Ceiling(lado * PixelesMm * 2 / qr.GetLength(0)));
                var qx = x + (o.COD_ALINEACION ?? "C").ToUpperInvariant() switch { "I" => 0, "D" => w - lado, _ => (w - lado) / 2 };
                PonerImagen(ws, ReporteCodigos.Png(ReporteCodigos.Puntos(qr, escala), tinta, papel), f, cols, qx, y + (h - lado) / 2, lado, lado);
                return;
            }
        }
        else
        {
            var barras = ReporteCodigos.Barras(tipo, t.Texto, out error);
            if (barras is not null)
            {
                var th = AltoTextoCodigo(o);
                var hb = Math.Max(1, h - th);
                var escala = Math.Max(1, (int)Math.Round(w * PixelesMm * 2 / barras.Total));
                var altoPx = Math.Max(8, (int)Math.Round(hb * PixelesMm * 2));
                PonerImagen(ws, ReporteCodigos.Png(ReporteCodigos.Puntos(barras, escala, altoPx), tinta, papel), f, cols, x, y, w, hb);
                if (th > 0) TextoCodigo(ws, o, barras.Texto, f, cols, combinadas, y + hb, th, color: null);
                return;
            }
        }
        TextoCodigo(ws, o, error ?? "Código no válido.", f, cols, combinadas, y, h, color: XLColor.FromHtml("#C62828"));
    }

    private void TextoCodigo(IXLWorksheet ws, ReporteLienzoObjeto o, string texto, FilasBloque f, List<double> cols, Combinadas combinadas,
                             double y, double alto, XLColor? color)
    {
        var (r1, r2) = Filas(f, y, alto);
        var (c1, c2) = Columnas(cols, X(o), (double)o.NUM_ANCHO);
        if ((r2 > r1 || c2 > c1) && !combinadas.Choca(r1, c1, r2, c2))
        {
            ws.Range(r1, c1, r2, c2).Merge();
            combinadas.Agregar(r1, c1, r2, c2);
        }
        var celda = ws.Cell(r1, c1);
        celda.Value = texto;
        var st = celda.Style;
        st.Font.FontName = Letra(o.DES_FUENTE);
        st.Font.FontSize = (double)(o.NUM_TAMANO_LETRA ?? _ctx.D.NUM_TAMANO_LETRA);
        st.Font.FontColor = color ?? ColorXls(o.DES_COLOR, XLColor.Black);
        st.Font.Italic = color is not null;
        st.Alignment.Horizontal = color is null ? XLAlignmentHorizontalValues.Center : XLAlignmentHorizontalValues.Left;
        st.Alignment.Vertical = XLAlignmentVerticalValues.Top;
        st.Alignment.WrapText = color is not null;
    }

    private static (byte R, byte G, byte B) Rgb(string? hex, (byte R, byte G, byte B) defecto)
    {
        if (string.IsNullOrWhiteSpace(hex) || hex.Trim().Length != 7 || hex.Trim()[0] != '#') return defecto;
        try
        {
            var v = Convert.ToInt32(hex.Trim()[1..], 16);
            return ((byte)(v >> 16), (byte)(v >> 8), (byte)v);
        }
        catch { return defecto; }
    }

    /// <summary>Una fila de una «Tabla de un resultado» o de una tabla cruzada: los títulos, una fila de datos o el total.</summary>
    private void FilaTablaExcel(IXLWorksheet ws, ReporteBandasBloque b, int fila, List<double> cols, Combinadas combinadas)
    {
        var t = b.Tabla!;
        var x0 = X(t.Objeto);
        var resaltada = b.Tipo == ReporteBloqueTipo.FilaTabla && t.UltimaResaltada && b.FilaTabla == t.Celdas.Count - 1;
        var negrita = b.Tipo != ReporteBloqueTipo.FilaTabla || resaltada;
        var datos = _ctx.Datos.Tabla(t.Objeto.NUM_RESULTADO ?? 1);
        // Las filas que muestra (las de la fila que se imprime, en una tabla enlazada).
        var filasTabla = t.Filas ?? datos?.Filas;
        var primera = b.Tipo == ReporteBloqueTipo.TotalTabla && t.FilaTotal is null ? t.Columnas.FindIndex(c => t.Totales.ContainsKey(c.Nombre)) : -1;
        var (cIni, cFin) = Columnas(cols, x0, t.Anchos.Sum());
        var toda = ws.Range(fila, cIni, fila, cFin);
        toda.Style.Font.FontName = Letra(t.Objeto.DES_FUENTE);
        toda.Style.Font.FontSize = (double)(t.Objeto.NUM_TAMANO_LETRA ?? _ctx.D.NUM_TAMANO_LETRA);
        toda.Style.Font.Bold = negrita;
        toda.Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
        toda.Style.Alignment.WrapText = true;
        if (b.Tipo == ReporteBloqueTipo.TituloTabla)
        {
            toda.Style.Fill.BackgroundColor = TintaXls;
            toda.Style.Font.FontColor = XLColor.White;
        }
        if (resaltada || b.Tipo == ReporteBloqueTipo.TotalTabla) toda.Style.Fill.BackgroundColor = ResalteXls;
        if (resaltada) { toda.Style.Border.TopBorder = XLBorderStyleValues.Medium; toda.Style.Border.TopBorderColor = TintaXls; }
        if (b.Tipo == ReporteBloqueTipo.TotalTabla) { toda.Style.Border.TopBorder = XLBorderStyleValues.Thin; toda.Style.Border.TopBorderColor = XLColor.Black; }
        if (b.Tipo == ReporteBloqueTipo.FilaTabla && t.ConTitulo && !resaltada)
        {
            toda.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            toda.Style.Border.BottomBorderColor = LineaXls;
        }
        if (primera > 0)
        {
            // «Total» ocupa las columnas que van antes de la primera que suma.
            var (c1, c2) = Columnas(cols, x0, t.Anchos.Take(primera).Sum());
            Combinar(ws, fila, c1, c2, combinadas);
            ws.Cell(fila, c1).Value = "Total";
            ws.Range(fila, c1, fila, c2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
        }
        var x = x0;
        for (var j = 0; j < t.Columnas.Count; j++)
        {
            var col = t.Columnas[j];
            var (c1, c2) = Columnas(cols, x, t.Anchos[j]);
            x += t.Anchos[j];
            if (j < primera) continue;
            Combinar(ws, fila, c1, c2, combinadas);
            var celda = ws.Cell(fila, c1);
            ws.Range(fila, c1, fila, c2).Style.Alignment.Horizontal = col.Alineacion switch
            {
                'C' => XLAlignmentHorizontalValues.Center,
                'D' => XLAlignmentHorizontalValues.Right,
                _   => XLAlignmentHorizontalValues.Left
            };
            switch (b.Tipo)
            {
                case ReporteBloqueTipo.TituloTabla:
                    celda.Value = col.Etiqueta;
                    break;
                case ReporteBloqueTipo.FilaTabla when t.Crudos is { } crudos:
                    CeldaCruzada(celda, crudos[b.FilaTabla][j], t.FormatosExcel?[j], t.Celdas[b.FilaTabla][j]);
                    break;
                case ReporteBloqueTipo.FilaTabla:
                {
                    var crudo = datos is not null && filasTabla is not null && b.FilaTabla < filasTabla.Count ? datos.Valor(filasTabla[b.FilaTabla], col.Nombre) : null;
                    var texto = t.Celdas[b.FilaTabla][j];
                    if (crudo is DateTime d && texto.Length > 0)
                    {
                        celda.Value = d;
                        celda.Style.NumberFormat.Format = ReporteFormato.Excel(col.Formato, col, d) ?? "dd/mm/yyyy";
                    }
                    else if (crudo is not (null or DBNull or string or bool) && col.EsNumero && ReporteFormato.ADecimal(crudo, out var n))
                    {
                        celda.Value = (double)n;
                        if (ReporteFormato.Excel(col.Formato, col, crudo) is { } fx) celda.Style.NumberFormat.Format = fx;
                    }
                    else celda.Value = texto;
                    break;
                }
                default:
                    if (t.FilaTotal is { } ft)
                        CeldaCruzada(celda, t.CrudosTotal?[j], t.FormatosExcel?[j], j < ft.Count ? ft[j] : "");
                    else if (t.Totales.TryGetValue(col.Nombre, out var tot))
                    {
                        celda.Value = (double)tot;
                        celda.Style.NumberFormat.Format = ReporteFormato.Excel(col.Formato, col) ?? "#,##0.00";
                    }
                    break;
            }
        }
    }

    /// <summary>Una celda de una tabla cruzada: el número o la fecha con su formato; si no, el texto como en el PDF.</summary>
    private static void CeldaCruzada(IXLCell celda, object? crudo, string? formato, string texto)
    {
        switch (crudo)
        {
            case DateTime d:
                celda.Value = d;
                celda.Style.NumberFormat.Format = formato ?? "dd/mm/yyyy";
                return;
            case not (null or string or bool) when formato is not null && ReporteFormato.ADecimal(crudo, out var n):
                celda.Value = (double)n;
                celda.Style.NumberFormat.Format = formato;
                return;
        }
        if (texto.Length > 0) celda.Value = texto;
    }

    /// <summary>Lo que dice en rojo un subinforme que no se pudo armar, en las celdas de su objeto.</summary>
    private void AvisoExcel(IXLWorksheet ws, ReporteBandasBloque b, FilasBloque f, List<double> cols, Combinadas combinadas)
    {
        if (b.Objeto is not { } o || string.IsNullOrEmpty(b.Aviso)) return;
        var (c1, c2) = Columnas(cols, X(o), (double)o.NUM_ANCHO);
        if ((f.Ultima > f.Primera || c2 > c1) && !combinadas.Choca(f.Primera, c1, f.Ultima, c2))
        {
            ws.Range(f.Primera, c1, f.Ultima, c2).Merge();
            combinadas.Agregar(f.Primera, c1, f.Ultima, c2);
        }
        var celda = ws.Cell(f.Primera, c1);
        celda.Value = b.Aviso;
        var st = celda.Style;
        st.Font.FontName = Letra(null);
        st.Font.FontSize = 7;
        st.Font.Italic = true;
        st.Font.FontColor = XLColor.FromHtml("#C62828");
        st.Alignment.Vertical = XLAlignmentVerticalValues.Top;
        st.Alignment.WrapText = true;
    }

    private static void Combinar(IXLWorksheet ws, int fila, int c1, int c2, Combinadas combinadas)
    {
        if (c2 <= c1 || combinadas.Choca(fila, c1, fila, c2)) return;
        ws.Range(fila, c1, fila, c2).Merge();
        combinadas.Agregar(fila, c1, fila, c2);
    }

    // ── Cruces y grupos ─────────────────────────────────────────────────────────
    /// <summary>Las líneas y los cuadros que cruzan secciones: de la fila donde empiezan a la fila donde terminan (sin
    /// páginas, el tramo es uno solo). El relleno de un cuadro no tapa las celdas que ya tienen color.</summary>
    private void CrucesExcel(IXLWorksheet ws, List<ReporteBandasBloque> bloques, List<ReporteBandasBloque> orden,
                             Dictionary<ReporteBandasBloque, FilasBloque> filas, List<ReporteBandasTramo> tramos, List<double> cols)
    {
        // La fila que empieza en el borde más cercano, o la que termina en él (en el borde de arriba de un bloque es la
        // última del bloque anterior, como en el PDF, donde un cruce que termina en Y 0 no entra en la sección).
        int? FilaEn(ReporteBandasBloque? b, double mm, bool fin)
        {
            if (b is null || !filas.TryGetValue(b, out var f)) return null;
            var e = Cercano(f.Bordes, mm - b.Desde);
            return fin ? f.Primera + e - 1 : f.Primera + e;
        }
        void Poner(ReporteLienzoObjeto o, ReporteBandasInstancia inst, int r1, int r2)
        {
            if (r2 < r1 || _f.Logico(ReporteFormulasDatos.Objeto, o.NUM_OBJETO, "SUPRIMIR", Lugar(inst, 1, 1)) == true) return;
            var lugar = Lugar(inst, 1, 1);
            var trazo = Trazo(o.NUM_GROSOR_BORDE);
            var color = ColorXls(_f.Color(ReporteFormulasDatos.Objeto, o.NUM_OBJETO, "COLOR", lugar) ?? o.DES_COLOR_BORDE ?? o.DES_COLOR, XLColor.Black);
            if (o.COD_TIPO_OBJETO == "LIN")
            {
                if (trazo != XLBorderStyleValues.None) LineaVertical(ws, cols, (double)o.NUM_X, r1, r2, trazo, color);
                return;
            }
            var (c1, c2) = Columnas(cols, (double)o.NUM_X, (double)o.NUM_ANCHO);
            var rango = ws.Range(r1, c1, r2, c2);
            var fondo = _f.Color(ReporteFormulasDatos.Objeto, o.NUM_OBJETO, "FONDO", lugar) ?? o.DES_COLOR_FONDO;
            if (!string.IsNullOrEmpty(fondo))
            {
                var xc = ColorXls(fondo, XLColor.NoColor);
                foreach (var c in rango.Cells())
                    if (c.Style.Fill.BackgroundColor == XLColor.NoColor) c.Style.Fill.BackgroundColor = xc;
            }
            if (trazo == XLBorderStyleValues.None) return;
            rango.Style.Border.OutsideBorder = trazo;
            rango.Style.Border.OutsideBorderColor = color;
        }

        // De página (encabezado y pie de página salen una vez): del primero al segundo.
        foreach (var o in _l.Objetos.Where(Cruza))
        {
            if (_l.Bandas.First(b => b.NUM_BANDA == o.NUM_BANDA).COD_TIPO_BANDA is not ("PH" or "PF")) continue;
            var a = orden.FirstOrDefault(b => b.Motor is null && b.Inst.Banda.NUM_BANDA == o.NUM_BANDA && b.Primero);
            var z = a is null ? null : orden.Skip(orden.IndexOf(a)).FirstOrDefault(b => b.Motor is null && b.Inst.Banda.NUM_BANDA == o.NUM_BANDA_FIN);
            if (FilaEn(a, (double)o.NUM_Y, false) is { } r1 && FilaEn(z, (double)o.NUM_Y_FIN!.Value, true) is { } r2) Poner(o, a!.Inst, r1, r2);
        }
        foreach (var t in tramos)
        {
            var bloquesTramo = bloques.Skip(t.Desde).Take(t.Hasta - t.Desde + 1).Where(filas.ContainsKey).ToList();
            if (bloquesTramo.Count == 0) continue;
            var inicio = bloques[t.Desde];
            var r1 = FilaEn(inicio, (double)t.Objeto.NUM_Y, false) ?? filas[bloquesTramo[0]].Primera;
            var fin = t.Fin is null ? null : bloquesTramo.FirstOrDefault(b => b.Inst == t.Fin);
            var r2 = FilaEn(fin, (double)t.Objeto.NUM_Y_FIN!.Value, true) ?? filas[bloquesTramo[^1]].Ultima;
            Poner(t.Objeto, t.Inicio, r1, r2);
        }
    }

    /// <summary>Las filas de cada grupo (sin su encabezado ni su pie) quedan agrupadas, para plegarlas desde el margen.</summary>
    private void Plegar(IXLWorksheet ws, List<ReporteBandasBloque> bloques, Dictionary<ReporteBandasBloque, FilasBloque> filas)
    {
        foreach (var (g, (desde, hasta)) in _rangosGrupo.OrderBy(x => x.Key.Nivel).ThenBy(x => x.Value.Desde))
        {
            if (hasta < 0 || g.Nivel > 7) continue;
            int? r1 = null, r2 = null;
            for (var i = desde; i < hasta && i < bloques.Count; i++)
            {
                var b = bloques[i];
                // Un subinforme se pliega con la banda que lo tiene.
                var raiz = Raiz(b);
                var banda = raiz.Banda;
                var propio = banda.COD_TIPO_BANDA is "GH" or "GF" && banda.NUM_NIVEL == g.Nivel
                             && g.Nivel <= raiz.Grupos.Length && raiz.Grupos[g.Nivel - 1] == g;
                if (propio || !filas.TryGetValue(b, out var f)) continue;
                r1 = Math.Min(r1 ?? f.Primera, f.Primera);
                r2 = Math.Max(r2 ?? f.Ultima, f.Ultima);
            }
            if (r1 is { } a && r2 is { } z && z >= a) ws.Rows(a, z).Group();
        }
    }
}
