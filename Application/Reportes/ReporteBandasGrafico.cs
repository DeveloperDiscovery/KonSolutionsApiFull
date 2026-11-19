using System.Globalization;
using System.Security;
using System.Text;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using PdfSharp.Drawing;
using KonSolutionsApi.Domain.Models.Reportes;
using S = DocumentFormat.OpenXml.Spreadsheet;
using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;

namespace KonSolutionsApi.Application.Reportes;

// Diseño libre, fase 3c: gráficos (objeto GRA).
//
// Un gráfico resume un resultado como la tabla cruzada: las categorías (DES_CRUCE_FILAS) en el eje, una serie por cada valor
// de DES_CRUCE_COLUMNAS (opcional; sin ella, una sola serie) y en cada punto la función (COD_FUNCION) de lo que resume
// (DES_ORIGEN). Toma las mismas filas que la tabla cruzada: del resultado 1, las del grupo de la sección (en un encabezado o
// pie de grupo) o todas; de otro resultado, todas; con enlaces, solo las que calzan con la fila que se imprime.
// Tipos (COD_GRAFICO): COL columnas, BAR barras horizontales, LIN líneas, TOR torta y ANI anillo (estos dos, de una sola
// serie: las categorías son las porciones).
// Cómo se ve (lo mismo en el PDF y en el Excel con formato, donde es un gráfico de Excel de verdad):
//   - Colores fijos, en un orden pensado para que se distingan entre sí también para quien confunde colores: azul, naranja,
//     verde, amarillo, rosado, verde oscuro, morado y rojo. Una sola serie va toda en azul (o en el color del objeto). Lo
//     que se junta en «Otros», en gris.
//   - A lo más 8 series: si hay más, las 7 de mayor valor y las demás juntas en «Otros». Las categorías, todas, salvo que
//     el objeto diga un máximo (NUM_MAXIMO_CATEGORIAS; en una torta, 6 si no dice): quedan las de mayor valor y las demás
//     se juntan en «Otros». En líneas no se juntan: quedan las últimas (las más recientes, si son fechas).
//   - Leyenda arriba cuando hay más de una serie; en la torta y el anillo, al lado (o debajo si no entra), con el valor y
//     el porcentaje de cada porción si se piden los valores. El anillo lleva el total en el centro.
//   - Un solo eje de valores, con líneas guía finas y números cortos (25 K, 1.5 M); la línea del cero, más marcada. Las
//     columnas y barras siempre parten de cero; las líneas, solo si sus valores quedan cerca de cero.
//   - Con «Mostrar valores» (FLG_VALORES): el valor en la punta de cada columna o barra (si entra; apiladas, el total de
//     la pila) y a la derecha del último punto de cada línea.
// Un punto sin filas vale cero en una suma o un conteo, y queda vacío en un promedio, un máximo o un mínimo. Un gráfico sin
// datos dice «Sin datos»; con «No imprimir si está vacío», no se dibuja.
// En el Excel con formato, cada gráfico es un gráfico de Excel sobre las celdas de su objeto, que lee sus números de la hoja
// «Datos de gráficos» (una tabla por gráfico): se puede cambiar de tipo, de colores o copiar a otro libro. ClosedXML no hace
// gráficos: se agregan con OpenXML al libro ya grabado. Diferencias con el PDF: las columnas o barras apiladas no llevan el
// total escrito; la torta y el anillo escriben el porcentaje en cada porción (no el valor en la leyenda), y el anillo no lleva
// el total en el centro (todo eso está en la hoja de datos).

/// <summary>Un gráfico ya calculado: las categorías, las series (un valor por categoría) y cómo se escribe un valor.</summary>
internal sealed class ReporteGrafico
{
    public string                    Tipo             { get; init; } = "COL";
    public List<string>              Categorias       { get; init; } = new();
    public List<ReporteGraficoSerie> Series           { get; init; } = new();
    /// <summary>El formato de los valores (.NET) y el mismo para Excel.</summary>
    public string?                   Formato          { get; init; }
    public string?                   FormatoExcel     { get; init; }
    public bool                      Apilado          { get; init; }
    public bool                      Valores          { get; init; }
    /// <summary>Cómo se llaman las categorías (la etiqueta de su columna): el título de la hoja de datos del Excel.</summary>
    public string                    TituloCategorias { get; init; } = "";
    public bool   EsTorta => Tipo is "TOR" or "ANI";
    public bool   Vacio   => Categorias.Count == 0 || Series.Count == 0;
    public string Texto(decimal v) => ReporteFormato.Texto(v, Formato, typeof(decimal));
}

internal sealed class ReporteGraficoSerie
{
    public string     Nombre  { get; init; } = "";
    public decimal?[] Valores { get; init; } = Array.Empty<decimal?>();
    /// <summary>El color de la serie (#rrggbb) y, en una torta, el de cada porción.</summary>
    public string     Color   { get; init; } = ReporteBandasMotor.PaletaGrafico[0];
    public string[]?  Colores { get; init; }
}

internal sealed partial class ReporteBandasMotor
{
    /// <summary>Los colores de las series, en este orden (probados entre sí sobre fondo blanco, también para quien confunde
    /// colores). Lo que se junta en «Otros» va en gris.</summary>
    internal static readonly string[] PaletaGrafico = { "#2a78d6", "#eb6834", "#1baf7a", "#eda100", "#e87ba4", "#008300", "#6250d6", "#e34948" };
    internal const string ColorOtros = "#6b6a65";
    internal const int MaximoSeries = 8;
    // El texto del gráfico nunca va en el color de una serie: los valores en casi negro, las categorías y la leyenda en gris
    // oscuro, los números del eje en gris claro. Las líneas guía, apenas visibles; la del cero, un poco más.
    private static readonly XColor GraficoTexto  = XColor.FromArgb(0x0b, 0x0b, 0x0b);
    private static readonly XColor GraficoTexto2 = XColor.FromArgb(0x52, 0x51, 0x4e);
    private static readonly XColor GraficoTenue  = XColor.FromArgb(0x89, 0x87, 0x81);
    private static readonly XColor GraficoGuia   = XColor.FromArgb(0xe1, 0xe0, 0xd9);
    private static readonly XColor GraficoBase   = XColor.FromArgb(0xc3, 0xc2, 0xb7);
    private const double BarraMaxima = 6;     // mm: una columna o barra no es más gruesa que esto
    private const double Separacion  = 0.5;   // mm: entre las barras de un grupo, entre las partes de una pila y las porciones

    // ── Los datos ───────────────────────────────────────────────────────────────
    private ReporteGrafico DatosGrafico(ReporteLienzoObjeto o, ReporteBandasInstancia inst)
    {
        var tipo = (o.COD_GRAFICO ?? "COL").Trim().ToUpperInvariant();
        if (tipo is not ("COL" or "BAR" or "LIN" or "TOR" or "ANI")) tipo = "COL";
        var torta = tipo is "TOR" or "ANI";
        var r = o.NUM_RESULTADO ?? 1;
        var datos = _tabla is not null && r == _tabla.Numero ? _tabla : _ctx.Datos.Tabla(r);
        var colCat = ReporteColumnas.Vacio(o.DES_CRUCE_FILAS);
        var colSerie = torta ? null : ReporteColumnas.Vacio(o.DES_CRUCE_COLUMNAS);
        var colValor = ReporteColumnas.Vacio(o.DES_ORIGEN);
        var funcion = (o.COD_FUNCION ?? "SUM").Trim().ToUpperInvariant();
        var propio = ReporteColumnas.Vacio(o.DES_FORMATO);
        var vacio = new ReporteGrafico { Tipo = tipo };
        if (datos is null || colCat is null) return vacio;
        var iK = datos.Indice(colCat);
        var iS = colSerie is null ? -1 : datos.Indice(colSerie);
        var iV = colValor is null ? -1 : datos.Indice(colValor);
        if (iK < 0 || (colSerie is not null && iS < 0) || (iV < 0 && funcion != "CNT")) return vacio;
        var vK = Vista(r, colCat);
        var vS = colSerie is null ? null : Vista(r, colSerie);
        var vV = colValor is null ? null : Vista(r, colValor);

        // Lo que cae en cada punto (categoría y serie), en cada categoría y en cada serie, sin vacíos. Un conteo sin columna
        // cuenta las filas.
        var deCat = new Dictionary<string, object?>(StringComparer.Ordinal);
        var deSerie = new Dictionary<string, object?>(StringComparer.Ordinal);
        var enPunto = new Dictionary<(string, string), List<object>>();
        var enCat = new Dictionary<string, List<object>>(StringComparer.Ordinal);
        var enSerie = new Dictionary<string, List<object>>(StringComparer.Ordinal);
        foreach (var f in FilasResumen(datos, o.DES_ENLACES, inst))
        {
            var vk = Celda(f, iK);
            var vs = iS < 0 ? null : Celda(f, iS);
            var kk = ClaveValor(vk) ?? "";
            var ks = ClaveValor(vs) ?? "";
            deCat.TryAdd(kk, vk);
            deSerie.TryAdd(ks, vs);
            if (!enPunto.TryGetValue((kk, ks), out var lp)) enPunto[(kk, ks)] = lp = new List<object>();
            if (!enCat.TryGetValue(kk, out var lc)) enCat[kk] = lc = new List<object>();
            if (!enSerie.TryGetValue(ks, out var ls)) enSerie[ks] = ls = new List<object>();
            var v = iV < 0 ? 1m : Celda(f, iV);
            if (v is null) continue;
            lp.Add(v);
            lc.Add(v);
            ls.Add(v);
        }
        if (deCat.Count == 0) return vacio;

        // Un punto es un número: lo que no lo es (el máximo de una fecha) queda vacío. El formato, el propio o el del resumen;
        // en letras no (en un gráfico no se escribe el número en palabras).
        var formato = propio;
        decimal? Calcular(List<object> valores)
        {
            var a = Agregado(funcion, valores, vV, propio);
            formato ??= a.Formato;
            return a.Valor is not (null or string or bool) && ReporteFormato.ADecimal(a.Valor, out var d) ? d : null;
        }
        var cero = funcion is "SUM" or "CNT" or "CND";
        decimal? Punto(IEnumerable<string> cats, IEnumerable<string> series)
        {
            var l = new List<object>();
            var hay = false;
            foreach (var kk in cats)
                foreach (var ks in series)
                    if (enPunto.TryGetValue((kk, ks), out var lp)) { hay = true; l.AddRange(lp); }
            return hay ? Calcular(l) : cero ? 0 : null;
        }

        // Las series en su orden; si son más de 8, quedan las 7 de mayor valor y las demás se juntan en «Otros».
        var ordenSeries = deSerie.OrderBy(x => x.Value, ReporteListado.Comparador.Instancia).Select(x => x.Key).ToList();
        var grupos = ordenSeries.Select(k => new List<string> { k }).ToList();
        var otrasSeries = false;
        if (ordenSeries.Count > MaximoSeries)
        {
            var peso = ordenSeries.ToDictionary(k => k, k => Math.Abs(Calcular(enSerie[k]) ?? 0));
            var quedan = ordenSeries.OrderByDescending(k => peso[k]).Take(MaximoSeries - 1).ToHashSet();
            grupos = ordenSeries.Where(quedan.Contains).Select(k => new List<string> { k }).ToList();
            grupos.Add(ordenSeries.Where(k => !quedan.Contains(k)).ToList());
            otrasSeries = true;
        }

        // Las categorías: en una torta solo las que tienen algo (una porción no puede ser negativa). Si pasan del máximo,
        // quedan las de mayor valor y las demás van en «Otros» (en líneas, quedan las últimas o, por valor, las mayores).
        var ordenCat = deCat.OrderBy(x => x.Value, ReporteListado.Comparador.Instancia).Select(x => x.Key).ToList();
        var pesoCat = ordenCat.ToDictionary(k => k, k => Calcular(enCat[k]) ?? 0);
        if (torta) ordenCat = ordenCat.Where(k => pesoCat[k] > 0).ToList();
        var maximo = o.NUM_MAXIMO_CATEGORIAS is > 1 and var m ? (int)m : torta ? 6 : int.MaxValue;
        if (torta) maximo = Math.Min(maximo, PaletaGrafico.Length);
        var gruposCat = ordenCat.Select(k => new List<string> { k }).ToList();
        var otrasCat = false;
        if (ordenCat.Count > maximo)
        {
            if (tipo == "LIN")
            {
                var quedan = (o.FLG_ORDEN_VALOR ? ordenCat.OrderByDescending(k => pesoCat[k]).Take(maximo) : ordenCat.Skip(ordenCat.Count - maximo)).ToHashSet();
                gruposCat = ordenCat.Where(quedan.Contains).Select(k => new List<string> { k }).ToList();
            }
            else
            {
                var quedan = ordenCat.OrderByDescending(k => pesoCat[k]).Take(maximo - 1).ToHashSet();
                gruposCat = ordenCat.Where(quedan.Contains).Select(k => new List<string> { k }).ToList();
                gruposCat.Add(ordenCat.Where(k => !quedan.Contains(k)).ToList());
                otrasCat = true;
            }
        }
        // Por valor: de mayor a menor (las columnas de izquierda a derecha, las barras de arriba hacia abajo); «Otros» al final.
        if (o.FLG_ORDEN_VALOR)
        {
            var otros = otrasCat ? gruposCat[^1] : null;
            gruposCat = gruposCat.Where(gc => gc != otros).OrderByDescending(gc => pesoCat[gc[0]]).ToList();
            if (otros is not null) gruposCat.Add(otros);
        }

        static string Rotulo(object? v, ReporteColumnaVista? vista)
        {
            var t = v is null ? "" : ReporteFormato.Texto(v, vista?.Formato, vista?.Tipo ?? v.GetType());
            return t.Length == 0 ? "(sin dato)" : t;
        }
        var categorias = gruposCat.Select((gc, i) => otrasCat && i == gruposCat.Count - 1 ? "Otros" : Rotulo(deCat[gc[0]], vK)).ToList();
        var nombreValor = vV?.Etiqueta ?? (colValor is null ? "Cantidad" : ReporteTexto.EtiquetaDesdeNombre(colValor));
        var series = new List<ReporteGraficoSerie>();
        for (var j = 0; j < grupos.Count; j++)
        {
            var otra = otrasSeries && j == grupos.Count - 1;
            var valores = gruposCat.Select(gc => Punto(gc, grupos[j])).ToArray();
            string color = grupos.Count == 1 ? ColorValido(o.DES_COLOR) ?? PaletaGrafico[0] : otra ? ColorOtros : PaletaGrafico[j % PaletaGrafico.Length];
            series.Add(new ReporteGraficoSerie
            {
                Nombre = colSerie is null ? nombreValor : otra ? "Otros" : Rotulo(deSerie[grupos[j][0]], vS),
                Valores = valores,
                Color = color,
                Colores = torta ? categorias.Select((_, i) => otrasCat && i == categorias.Count - 1 ? ColorOtros : PaletaGrafico[i % PaletaGrafico.Length]).ToArray() : null
            });
        }
        if (torta)
        {
            // «Otros» también puede quedar en cero o negativo (un promedio): una porción así no se dibuja.
            var s = series[0];
            var sirven = Enumerable.Range(0, categorias.Count).Where(i => s.Valores[i] > 0).ToList();
            if (sirven.Count < categorias.Count)
            {
                categorias = sirven.Select(i => categorias[i]).ToList();
                series[0] = new ReporteGraficoSerie { Nombre = s.Nombre, Color = s.Color, Valores = sirven.Select(i => s.Valores[i]).ToArray(),
                                                      Colores = sirven.Select(i => s.Colores![i]).ToArray() };
            }
        }
        if (formato is null || ReporteFormato.EsLetras(formato)) formato = "#,##0.##";
        return new ReporteGrafico
        {
            Tipo = tipo, Categorias = categorias, Series = categorias.Count == 0 ? new() : series, Formato = formato,
            FormatoExcel = ReporteFormato.Excel(formato, new ReporteColumnaVista { Tipo = typeof(decimal), Formato = formato }, 0m),
            Apilado = o.FLG_APILADO && tipo is "COL" or "BAR" && series.Count > 1, Valores = o.FLG_VALORES,
            TituloCategorias = vK?.Etiqueta ?? ReporteTexto.EtiquetaDesdeNombre(colCat)
        };
    }

    /// <summary>Un color #rrggbb bien escrito, o null.</summary>
    private static string? ColorValido(string? hex)
    {
        if (string.IsNullOrWhiteSpace(hex)) return null;
        var h = hex.Trim().TrimStart('#');
        return h.Length == 6 && int.TryParse(h, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out _) ? "#" + h.ToLowerInvariant() : null;
    }

    // ── La escala del eje ──────────────────────────────────────────────────────
    /// <summary>Una escala redonda: de dónde a dónde y cada cuánto (1, 2, 2.5 o 5 por una potencia de 10), con unas
    /// marcas como pide el espacio.</summary>
    internal static (double Desde, double Hasta, double Paso) EscalaEje(double min, double max, int marcas)
    {
        if (min > max) (min, max) = (max, min);
        if (max - min < 1e-12)
        {
            if (max == 0) max = 1;
            else if (max > 0) min = 0;
            else max = 0;
        }
        var bruto = (max - min) / Math.Max(1, marcas);
        var p = Math.Pow(10, Math.Floor(Math.Log10(bruto)));
        var paso = new[] { 1, 2, 2.5, 5, 10 }.Select(k => k * p).First(k => k >= bruto * (1 - 1e-9));
        return (Math.Floor(min / paso + 1e-9) * paso, Math.Ceiling(max / paso - 1e-9) * paso, paso);
    }

    /// <summary>Con qué unidad se escriben los números del eje: millones desde 1 000 000, miles desde 10 000; y cuántos
    /// decimales pide el paso.</summary>
    internal static (double Divisor, string Sufijo, int Decimales) UnidadEje(double desde, double hasta, double paso)
    {
        var mayor = Math.Max(Math.Abs(desde), Math.Abs(hasta));
        var (div, suf) = mayor >= 1e6 ? (1e6, " M") : mayor >= 1e4 ? (1e3, " K") : (1.0, "");
        var d = 0;
        while (d < 4 && Math.Abs(Math.Round(paso / div, d) - paso / div) > 1e-9) d++;
        return (div, suf, d);
    }

    private static string NumeroEje(double v, (double Divisor, string Sufijo, int Decimales) u)
    {
        var x = Math.Round(v / u.Divisor, u.Decimales);
        if (Math.Abs(x) < 1e-12) x = 0;
        return x.ToString(u.Decimales == 0 ? "#,##0" : "#,##0." + new string('0', u.Decimales), CultureInfo.InvariantCulture) + u.Sufijo;
    }

    /// <summary>El menor y el mayor valor del gráfico (en una pila, lo que suman sus partes positivas y negativas).</summary>
    private static (double Min, double Max) RangoGrafico(ReporteGrafico gr, bool apilado)
    {
        double min = double.MaxValue, max = double.MinValue;
        for (var i = 0; i < gr.Categorias.Count; i++)
        {
            if (apilado)
            {
                double pos = 0, neg = 0;
                foreach (var s in gr.Series)
                    if (s.Valores[i] is { } v) { if (v >= 0) pos += (double)v; else neg += (double)v; }
                min = Math.Min(min, neg);
                max = Math.Max(max, pos);
            }
            else
                foreach (var s in gr.Series)
                    if (s.Valores[i] is { } v) { min = Math.Min(min, (double)v); max = Math.Max(max, (double)v); }
        }
        return min > max ? (0, 1) : (min, max);
    }

    /// <summary>Lo que va en el eje: columnas y barras desde cero; líneas desde cero solo si sus valores quedan cerca.</summary>
    internal static (double Min, double Max) DominioGrafico(ReporteGrafico gr)
    {
        var (min, max) = RangoGrafico(gr, gr.Apilado);
        var conCero = gr.Tipo != "LIN" || (min >= 0 ? min <= max * 0.5 : max >= 0 || max >= min * 0.5);
        return conCero ? (Math.Min(min, 0), Math.Max(max, 0)) : (min, max);
    }

    // ── Medir texto ─────────────────────────────────────────────────────────────
    private double AnchoTexto(string s, XFont f) => s.Length == 0 ? 0 : _medir.MeasureString(s, f).Width / MmPt;

    /// <summary>El texto cortado con «…» si no entra en el ancho (mm).</summary>
    private string Recortar(string s, XFont f, double ancho)
    {
        if (AnchoTexto(s, f) <= ancho) return s;
        var n = s.Length;
        while (n > 0 && AnchoTexto(s[..n].TrimEnd() + "…", f) > ancho) n--;
        return n == 0 ? "" : s[..n].TrimEnd() + "…";
    }

    /// <summary>El texto en a lo más tantas líneas del ancho (mm); lo que sobra termina en «…».</summary>
    private List<string> Renglones(string s, XFont f, double ancho, int maximo)
    {
        var l = Partir(s, f, Math.Max(1, ancho) * MmPt);
        if (l.Count <= maximo) return l;
        var res = l.Take(maximo - 1).ToList();
        res.Add(Recortar(string.Join(" ", l.Skip(maximo - 1)), f, ancho));
        return res;
    }

    // ── Dibujar en el PDF ───────────────────────────────────────────────────────
    private void DibujarGrafico(XGraphics g, ReporteLienzoObjeto o, ReporteBandasBloque b, double x, double y, double w, double h, string? fondo)
    {
        if (!b.Graficos.TryGetValue(o.NUM_OBJETO, out var gr)) return;
        if (!string.IsNullOrEmpty(fondo)) g.DrawRectangle(new XSolidBrush(Color(fondo, XColors.White)), Rect(x, y, w, h));
        Bordes(g, o, x, y, w, h);
        var papel = Color(fondo, XColors.White);
        var f = Fuente(o, false);
        var estado = g.Save();
        g.IntersectClip(Rect(x, y, w, h));
        const double margen = 1;
        var (ax, ay, aw, ah) = (x + margen, y + margen, Math.Max(1, w - 2 * margen), Math.Max(1, h - 2 * margen));
        if (gr.Vacio) g.DrawString("Sin datos", f, new XSolidBrush(GraficoTenue), Rect(x, y, w, h), XStringFormats.Center);
        else if (gr.EsTorta) DibujarTorta(g, gr, o, f, papel, ax, ay, aw, ah);
        else if (gr.Tipo == "BAR") DibujarBarras(g, gr, f, papel, ax, ay, aw, ah);
        else DibujarColumnas(g, gr, f, papel, ax, ay, aw, ah);
        g.Restore(estado);
    }

    /// <summary>La leyenda arriba (con más de una serie): un cuadradito del color y el nombre de cada una, en una o más filas.
    /// Sin dónde dibujar, solo mide. Devuelve su alto con el espacio de debajo.</summary>
    private double LeyendaArriba(XGraphics? g, ReporteGrafico gr, XFont f, double x, double y, double w)
    {
        if (gr.Series.Count < 2) return 0;
        var lh = f.GetHeight() / MmPt;
        var lado = Math.Min(2.6, lh * 0.72);
        double cx = x, cy = y;
        foreach (var s in gr.Series)
        {
            var nombre = Recortar(s.Nombre, f, Math.Max(8, w * 0.45));
            var ancho = lado + 1 + AnchoTexto(nombre, f);
            if (cx > x && cx + ancho > x + w) { cx = x; cy += lh + 0.6; }
            if (g is not null)
            {
                g.DrawRectangle(new XSolidBrush(Color(s.Color, XColors.Gray)), Rect(cx, cy + (lh - lado) / 2, lado, lado));
                g.DrawString(nombre, f, new XSolidBrush(GraficoTexto2), Rect(cx + lado + 1, cy, ancho, lh), XStringFormats.TopLeft);
            }
            cx += ancho + 3.5;
        }
        return cy - y + lh + 1.8;
    }

    /// <summary>Columnas (COL) y líneas (LIN): las categorías abajo, los valores a la izquierda.</summary>
    private void DibujarColumnas(XGraphics g, ReporteGrafico gr, XFont f, XColor papel, double x, double y, double w, double h)
    {
        var lh = f.GetHeight() / MmPt;
        var linea = gr.Tipo == "LIN";
        var n = gr.Categorias.Count;
        var leyenda = LeyendaArriba(g, gr, f, x, y, w);
        // Arriba, lo que sobresale el número de la marca más alta o el valor escrito sobre la columna más alta.
        var arriba = leyenda + (gr.Valores ? lh + 0.6 : lh / 2);
        var maxLineas = h >= 40 ? 2 : 1;
        var altoEstimado = Math.Max(5, h - arriba - maxLineas * lh - 1);
        var (min, max) = DominioGrafico(gr);
        var (desde, hasta, paso) = EscalaEje(min, max, Math.Clamp((int)(altoEstimado / 9), 2, 6));
        var unidad = UnidadEje(desde, hasta, paso);
        var marcas = new List<double>();
        for (var t = desde; t <= hasta + paso / 2; t += paso) marcas.Add(Math.Round(t / paso) * paso);
        var anchoEje = marcas.Max(t => AnchoTexto(NumeroEje(t, unidad), f)) + 1.5;

        var px = x + anchoEje;
        var pw = Math.Max(1, x + w - px - 0.5);
        // En líneas, el último valor va a la derecha de su punto: lo que no entra en la mitad de la última columna se le
        // quita al gráfico.
        if (linea && gr.Valores)
        {
            var finales = gr.Series.Select(s => Array.FindLast(s.Valores, v => v is not null)).Where(v => v is not null).Select(v => AnchoTexto(gr.Texto(v!.Value), f));
            pw = Math.Max(1, pw - Math.Max(0, finales.DefaultIfEmpty(0).Max() + 2.6 - pw / n / 2));
        }
        var banda = pw / n;
        // Los nombres de las categorías: si no entran, uno de cada k (centrado en su columna, con el ancho de k columnas). Un
        // nombre de varias palabras se puede partir en líneas; uno de una sola palabra (una fecha), no.
        double Pide(string c)
        {
            var palabras = c.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return palabras.Length <= 1 ? AnchoTexto(c, f) : Math.Max(palabras.Max(p => AnchoTexto(p, f)), AnchoTexto(c, f) / maxLineas);
        }
        var pide = Math.Min(gr.Categorias.Max(Pide) + 0.6, 16);
        var k = Math.Clamp((int)Math.Ceiling(pide / banda - 1e-9), 1, n);
        var rotulos = gr.Categorias.Select((c, i) => i % k == 0 ? Renglones(c, f, banda * k - 0.6, maxLineas) : new List<string>()).ToList();
        var lineasUsadas = Math.Max(1, rotulos.Max(r => r.Count));
        var abajo = lineasUsadas * lh + 1;
        var py = y + arriba;
        var ph = Math.Max(1, y + h - abajo - py);
        double Y(double v) => py + ph * (hasta - v) / (hasta - desde);
        double Cx(int i) => px + banda * (i + 0.5);

        // Líneas guía y números del eje.
        var guia = new XPen(GraficoGuia, 0.35);
        var tenue = new XSolidBrush(GraficoTenue);
        foreach (var t in marcas)
        {
            if (Math.Abs(t) > paso / 1e6) g.DrawLine(guia, px * MmPt, Y(t) * MmPt, (px + pw) * MmPt, Y(t) * MmPt);
            g.DrawString(NumeroEje(t, unidad), f, tenue, Rect(x, Y(t) - lh / 2, anchoEje - 1.5, lh), XStringFormats.CenterRight);
        }
        // Las categorías.
        var gris = new XSolidBrush(GraficoTexto2);
        for (var i = 0; i < n; i++)
        {
            var yl = py + ph + 1;
            foreach (var l in rotulos[i])
            {
                g.DrawString(l, f, gris, Rect(Cx(i) - banda * k / 2, yl, banda * k, lh), XStringFormats.TopCenter);
                yl += lh;
            }
        }

        var negro = new XSolidBrush(GraficoTexto);
        if (linea) DibujarLineas(g, gr, f, papel, Cx, Y, banda, px, pw, py, ph, negro);
        else
        {
            var apilado = gr.Apilado;
            var m = apilado ? 1 : gr.Series.Count;
            var hueco = Separacion;
            var bw = Math.Min(BarraMaxima, (banda * 0.72 - hueco * (m - 1)) / m);
            if (bw < 0.4) { hueco = 0; bw = Math.Max(0.15, banda * 0.8 / m); }
            var grupo = bw * m + hueco * (m - 1);
            for (var i = 0; i < n; i++)
            {
                var x0 = Cx(i) - grupo / 2;
                if (apilado)
                {
                    double pos = 0, neg = 0;
                    var ultPos = gr.Series.FindLastIndex(s => s.Valores[i] > 0);
                    var ultNeg = gr.Series.FindLastIndex(s => s.Valores[i] < 0);
                    var cortes = new List<double>();
                    for (var j = 0; j < gr.Series.Count; j++)
                    {
                        if (gr.Series[j].Valores[i] is not { } dv || dv == 0) continue;
                        var v = (double)dv;
                        var brocha = new XSolidBrush(Color(gr.Series[j].Color, XColors.Gray));
                        if (v > 0)
                        {
                            Barra(g, brocha, x0, Y(pos + v), bw, Y(pos) - Y(pos + v), j == ultPos ? 'A' : ' ');
                            if (pos > 0) cortes.Add(Y(pos));
                            pos += v;
                        }
                        else
                        {
                            Barra(g, brocha, x0, Y(neg), bw, Y(neg + v) - Y(neg), j == ultNeg ? 'B' : ' ');
                            if (neg < 0) cortes.Add(Y(neg));
                            neg += v;
                        }
                    }
                    // Entre las partes de la pila, una raya del color del fondo.
                    var raya = new XPen(papel, Separacion * MmPt);
                    foreach (var c in cortes) g.DrawLine(raya, x0 * MmPt, c * MmPt, (x0 + bw) * MmPt, c * MmPt);
                    if (gr.Valores && (pos != 0 || neg != 0))
                    {
                        var total = pos + neg;
                        var texto = gr.Texto((decimal)total);
                        var tw = AnchoTexto(texto, f);
                        // El total de la pila, sobre ella (o debajo, si es negativo y entra).
                        var ty = total >= 0 ? Y(pos) - lh - 0.3 : Y(neg) + 0.3;
                        if (tw <= banda - 0.4 && (total >= 0 || ty + lh <= py + ph + 0.01))
                            g.DrawString(texto, f, negro, Rect(Cx(i) - tw / 2 - 0.2, ty, tw + 0.4, lh), XStringFormats.TopCenter);
                    }
                    continue;
                }
                for (var j = 0; j < m; j++)
                {
                    if (gr.Series[j].Valores[i] is not { } dv) continue;
                    var v = (double)dv;
                    var bx = x0 + j * (bw + hueco);
                    var brocha = new XSolidBrush(Color(gr.Series[j].Color, XColors.Gray));
                    if (v >= 0) Barra(g, brocha, bx, Y(v), bw, Y(0) - Y(v), 'A');
                    else Barra(g, brocha, bx, Y(0), bw, Y(v) - Y(0), 'B');
                    if (!gr.Valores) continue;
                    var texto = gr.Texto(dv);
                    var tw = AnchoTexto(texto, f);
                    // Solo si entra sobre su columna (con una sola serie, en el ancho de la categoría).
                    if (tw > (m == 1 ? banda - 0.4 : bw + hueco + 0.8)) continue;
                    var ty = v >= 0 ? Y(v) - lh - 0.3 : Y(v) + 0.3;
                    if (v < 0 && ty + lh > py + ph + 0.01) continue;
                    g.DrawString(texto, f, negro, Rect(bx + bw / 2 - tw / 2 - 0.2, ty, tw + 0.4, lh), XStringFormats.TopCenter);
                }
            }
        }
        // La línea del cero (o la de abajo, si el eje no llega a cero), encima de las columnas.
        var cero = desde <= 0 && hasta >= 0 ? 0 : desde;
        g.DrawLine(new XPen(GraficoBase, 0.6), px * MmPt, Y(cero) * MmPt, (px + pw) * MmPt, Y(cero) * MmPt);
    }

    /// <summary>Las líneas: una por serie (1.5 pt), con un punto en cada valor si hay espacio y el último valor escrito al
    /// final si se piden los valores. Un valor vacío corta la línea.</summary>
    private void DibujarLineas(XGraphics g, ReporteGrafico gr, XFont f, XColor papel, Func<int, double> cx, Func<double, double> cy,
                               double banda, double px, double pw, double py, double ph, XBrush negro)
    {
        var lh = f.GetHeight() / MmPt;
        var n = gr.Categorias.Count;
        var conPuntos = banda >= 5;
        var radio = banda >= 8 ? 1.06 : 0.8;
        var finales = new List<(double Y, double X, string Texto)>();
        foreach (var s in gr.Series)
        {
            var color = Color(s.Color, XColors.Gray);
            var pluma = new XPen(color, 1.5) { LineJoin = XLineJoin.Round, LineCap = XLineCap.Round };
            var tramo = new List<XPoint>();
            void Cerrar()
            {
                if (tramo.Count >= 2) g.DrawLines(pluma, tramo.ToArray());
                tramo.Clear();
            }
            for (var i = 0; i < n; i++)
            {
                if (s.Valores[i] is not { } v) { Cerrar(); continue; }
                tramo.Add(new XPoint(cx(i) * MmPt, cy((double)v) * MmPt));
            }
            Cerrar();
            var brocha = new XSolidBrush(color);
            var aro = new XSolidBrush(papel);
            for (var i = 0; i < n; i++)
            {
                if (s.Valores[i] is not { } v) continue;
                var solo = (i == 0 || s.Valores[i - 1] is null) && (i == n - 1 || s.Valores[i + 1] is null);
                if (!conPuntos && !solo) continue;
                var (mx, my) = (cx(i), cy((double)v));
                g.DrawEllipse(aro, Rect(mx - radio - 0.5, my - radio - 0.5, 2 * (radio + 0.5), 2 * (radio + 0.5)));
                g.DrawEllipse(brocha, Rect(mx - radio, my - radio, 2 * radio, 2 * radio));
            }
            var ult = Array.FindLastIndex(s.Valores, v => v is not null);
            if (gr.Valores && ult >= 0) finales.Add((cy((double)s.Valores[ult]!.Value), cx(ult) + radio + 1.2, gr.Texto(s.Valores[ult]!.Value)));
        }
        // El último valor de cada línea, a la derecha de su punto y después de dibujar todas (que ninguna lo tape). Si dos
        // quedan muy juntos, el de abajo baja lo que haga falta.
        finales = finales.OrderBy(t => t.Y).ToList();
        for (var i = 1; i < finales.Count; i++)
            if (finales[i].Y < finales[i - 1].Y + lh) finales[i] = (finales[i - 1].Y + lh, finales[i].X, finales[i].Texto);
        var sobra = finales.Count > 0 ? finales[^1].Y + lh / 2 - (py + ph + lh / 2) : 0;
        foreach (var (ty, tx, texto) in finales)
        {
            var yy = sobra > 0 ? ty - sobra : ty;
            g.DrawString(texto, f, negro, Rect(tx, yy - lh / 2, AnchoTexto(texto, f) + 0.4, lh), XStringFormats.CenterLeft);
        }
    }

    /// <summary>Barras horizontales (BAR): las categorías a la izquierda, de arriba hacia abajo; los valores abajo.</summary>
    private void DibujarBarras(XGraphics g, ReporteGrafico gr, XFont f, XColor papel, double x, double y, double w, double h)
    {
        var lh = f.GetHeight() / MmPt;
        var n = gr.Categorias.Count;
        var leyenda = LeyendaArriba(g, gr, f, x, y, w);
        var (min, max) = DominioGrafico(gr);
        var anchoCat = Math.Min(gr.Categorias.Max(c => AnchoTexto(c, f)), w * 0.35) + 1.5;
        var (desde, hasta, paso) = EscalaEje(min, max, Math.Clamp((int)((w - anchoCat) / 18), 2, 6));
        var unidad = UnidadEje(desde, hasta, paso);
        var marcas = new List<double>();
        for (var t = desde; t <= hasta + paso / 2; t += paso) marcas.Add(Math.Round(t / paso) * paso);
        // A la derecha, el valor de la barra más larga o la mitad del último número del eje.
        var derecha = gr.Valores
            ? Math.Min(gr.Series.SelectMany(s => s.Valores).Where(v => v is not null).Select(v => AnchoTexto(gr.Texto(v!.Value), f)).DefaultIfEmpty(0).Max() + 1, w * 0.25)
            : AnchoTexto(NumeroEje(marcas[^1], unidad), f) / 2;
        var izquierda = desde < 0 ? Math.Max(0, AnchoTexto(NumeroEje(marcas[0], unidad), f) / 2 - 1.5) : 0;
        var px = x + anchoCat + izquierda;
        var pw = Math.Max(1, x + w - derecha - px);
        var py = y + leyenda;
        var ph = Math.Max(1, y + h - lh - 1 - py);
        var banda = ph / n;
        double X(double v) => px + pw * (v - desde) / (hasta - desde);
        double Cy(int i) => py + banda * (i + 0.5);

        var guia = new XPen(GraficoGuia, 0.35);
        var tenue = new XSolidBrush(GraficoTenue);
        foreach (var t in marcas)
        {
            if (Math.Abs(t) > paso / 1e6) g.DrawLine(guia, X(t) * MmPt, py * MmPt, X(t) * MmPt, (py + ph) * MmPt);
            var tt = NumeroEje(t, unidad);
            var tw = AnchoTexto(tt, f) + 0.4;
            g.DrawString(tt, f, tenue, Rect(X(t) - tw / 2, py + ph + 1, tw, lh), XStringFormats.TopCenter);
        }
        // Los nombres: en una línea, o en dos si la barra da para eso; si no entran todos, uno de cada k.
        var lineas = banda >= 2 * lh + 0.4 ? 2 : 1;
        var k = Math.Clamp((int)Math.Ceiling(lh / banda - 1e-9), 1, n);
        var gris = new XSolidBrush(GraficoTexto2);
        for (var i = 0; i < n; i += k)
        {
            var l = Renglones(gr.Categorias[i], f, anchoCat - 1.5, lineas);
            var yl = Cy(i) - l.Count * lh / 2;
            foreach (var r in l)
            {
                g.DrawString(r, f, gris, Rect(x, yl, anchoCat - 1.5, lh), XStringFormats.TopRight);
                yl += lh;
            }
        }

        var negro = new XSolidBrush(GraficoTexto);
        var apilado = gr.Apilado;
        var m = apilado ? 1 : gr.Series.Count;
        var hueco = Separacion;
        var bh = Math.Min(BarraMaxima, (banda * 0.72 - hueco * (m - 1)) / m);
        if (bh < 0.4) { hueco = 0; bh = Math.Max(0.15, banda * 0.8 / m); }
        var grupo = bh * m + hueco * (m - 1);
        void Valor(double v, double fin, double cy)
        {
            var texto = gr.Texto((decimal)v);
            var tw = AnchoTexto(texto, f) + 0.4;
            if (lh > banda + 0.6) return;
            if (v >= 0) { if (fin + 0.6 + tw <= x + w + 0.01) g.DrawString(texto, f, negro, Rect(fin + 0.6, cy - lh / 2, tw, lh), XStringFormats.CenterLeft); }
            else if (fin - 0.6 - tw >= px - izquierda - 0.01) g.DrawString(texto, f, negro, Rect(fin - 0.6 - tw, cy - lh / 2, tw, lh), XStringFormats.CenterRight);
        }
        for (var i = 0; i < n; i++)
        {
            var y0 = Cy(i) - grupo / 2;
            if (apilado)
            {
                double pos = 0, neg = 0;
                var ultPos = gr.Series.FindLastIndex(s => s.Valores[i] > 0);
                var ultNeg = gr.Series.FindLastIndex(s => s.Valores[i] < 0);
                var cortes = new List<double>();
                for (var j = 0; j < gr.Series.Count; j++)
                {
                    if (gr.Series[j].Valores[i] is not { } dv || dv == 0) continue;
                    var v = (double)dv;
                    var brocha = new XSolidBrush(Color(gr.Series[j].Color, XColors.Gray));
                    if (v > 0)
                    {
                        Barra(g, brocha, X(pos), y0, X(pos + v) - X(pos), bh, j == ultPos ? 'D' : ' ');
                        if (pos > 0) cortes.Add(X(pos));
                        pos += v;
                    }
                    else
                    {
                        Barra(g, brocha, X(neg + v), y0, X(neg) - X(neg + v), bh, j == ultNeg ? 'I' : ' ');
                        if (neg < 0) cortes.Add(X(neg));
                        neg += v;
                    }
                }
                var raya = new XPen(papel, Separacion * MmPt);
                foreach (var c in cortes) g.DrawLine(raya, c * MmPt, y0 * MmPt, c * MmPt, (y0 + bh) * MmPt);
                if (gr.Valores && (pos != 0 || neg != 0)) Valor(pos + neg, pos + neg >= 0 ? X(pos) : X(neg), Cy(i));
                continue;
            }
            for (var j = 0; j < m; j++)
            {
                if (gr.Series[j].Valores[i] is not { } dv) continue;
                var v = (double)dv;
                var by = y0 + j * (bh + hueco);
                var brocha = new XSolidBrush(Color(gr.Series[j].Color, XColors.Gray));
                if (v >= 0) Barra(g, brocha, X(0), by, X(v) - X(0), bh, 'D');
                else Barra(g, brocha, X(v), by, X(0) - X(v), bh, 'I');
                if (gr.Valores && (m == 1 || bh + hueco >= lh * 0.8)) Valor(v, X(v), by + bh / 2);
            }
        }
        var cero = desde <= 0 && hasta >= 0 ? 0 : desde;
        g.DrawLine(new XPen(GraficoBase, 0.6), X(cero) * MmPt, py * MmPt, X(cero) * MmPt, (py + ph) * MmPt);
    }

    /// <summary>Torta (TOR) o anillo (ANI): las porciones desde las 12, en el sentido del reloj, separadas por una raya del
    /// color del fondo. La leyenda al lado (o debajo, si el gráfico es más alto que ancho), con el valor y el porcentaje si se
    /// piden los valores. El anillo, con el total en el centro.</summary>
    private void DibujarTorta(XGraphics g, ReporteGrafico gr, ReporteLienzoObjeto o, XFont f, XColor papel, double x, double y, double w, double h)
    {
        var lh = f.GetHeight() / MmPt;
        var s = gr.Series[0];
        var n = gr.Categorias.Count;
        var total = s.Valores.Sum(v => v ?? 0);
        if (total <= 0) return;
        var lado = Math.Min(2.6, lh * 0.72);
        var cifras = gr.Valores
            ? s.Valores.Select(v => $"{gr.Texto(v!.Value)}  {(v!.Value * 100 / total).ToString("0.0", CultureInfo.InvariantCulture)}%").ToList()
            : s.Valores.Select(_ => "").ToList();
        var anchoCifra = cifras.Max(c => AnchoTexto(c, f));
        var alLado = w >= h * 1.2;
        var gris = new XSolidBrush(GraficoTexto2);
        var negro = new XSolidBrush(GraficoTexto);
        double cxT, cyT, d;
        if (alLado)
        {
            // La leyenda, lo que pida (hasta lo que deje una torta del alto del gráfico, y al menos la mitad del ancho): cada
            // porción en una línea, con su valor a la derecha; si así no entra, en dos (el valor debajo del nombre).
            var anchoNombre = gr.Categorias.Max(c => AnchoTexto(c, f));
            var maximo = Math.Max(w * 0.55, w - Math.Min(h, w * 0.45) - 3);
            var unaLinea = lado + 1 + anchoNombre + (gr.Valores ? 3 + anchoCifra : 0) + 0.5;
            var dos = gr.Valores && unaLinea > maximo && n * (2 * lh + 0.8) - 0.8 <= h;
            var anchoLeyenda = Math.Min(dos ? lado + 1 + Math.Max(anchoNombre, anchoCifra) + 0.5 : unaLinea, maximo);
            var paso = (dos ? 2 : 1) * lh + 0.8;
            var filas = Math.Max(1, Math.Min(n, (int)Math.Floor((h + 0.8) / paso)));
            var altoLeyenda = filas * paso - 0.8;
            var lx = x + w - anchoLeyenda;
            var ly = y + (h - altoLeyenda) / 2;
            for (var i = 0; i < filas; i++)
            {
                var yy = ly + i * paso;
                g.DrawRectangle(new XSolidBrush(Color(s.Colores![i], XColors.Gray)), Rect(lx, yy + (lh - lado) / 2, lado, lado));
                var resto = anchoLeyenda - lado - 1 - (gr.Valores && !dos ? 3 + anchoCifra : 0);
                g.DrawString(Recortar(gr.Categorias[i], f, Math.Max(4, resto)), f, gris, Rect(lx + lado + 1, yy, Math.Max(4, resto), lh), XStringFormats.TopLeft);
                if (dos) g.DrawString(cifras[i], f, negro, Rect(lx + lado + 1, yy + lh, anchoLeyenda - lado - 1, lh), XStringFormats.TopLeft);
                else if (gr.Valores) g.DrawString(cifras[i], f, negro, Rect(lx + anchoLeyenda - anchoCifra, yy, anchoCifra, lh), XStringFormats.TopRight);
            }
            var libre = w - anchoLeyenda - 3;
            d = Math.Max(2, Math.Min(libre, h));
            cxT = x + libre / 2;
            cyT = y + h / 2;
        }
        else
        {
            // Debajo: en filas, como la leyenda de las series.
            var items = gr.Categorias.Select((c, i) => (Texto: Recortar(c, f, Math.Max(8, w - lado - 1 - (gr.Valores ? 1.5 + AnchoTexto(cifras[i], f) : 0))), Cifra: cifras[i])).ToList();
            var posiciones = new List<(double X, double Y)>();
            double ix = 0, iy = 0;
            foreach (var it in items)
            {
                var ancho = lado + 1 + AnchoTexto(it.Texto, f) + (gr.Valores ? 1.5 + AnchoTexto(it.Cifra, f) : 0);
                if (ix > 0 && ix + ancho > w) { ix = 0; iy += lh + 0.6; }
                posiciones.Add((ix, iy));
                ix += ancho + 3.5;
            }
            var altoLeyenda = iy + lh;
            var ly = y + h - altoLeyenda;
            for (var i = 0; i < n; i++)
            {
                var (ox, oy) = (x + posiciones[i].X, ly + posiciones[i].Y);
                g.DrawRectangle(new XSolidBrush(Color(s.Colores![i], XColors.Gray)), Rect(ox, oy + (lh - lado) / 2, lado, lado));
                var tw = AnchoTexto(items[i].Texto, f);
                g.DrawString(items[i].Texto, f, gris, Rect(ox + lado + 1, oy, tw + 0.4, lh), XStringFormats.TopLeft);
                if (gr.Valores) g.DrawString(items[i].Cifra, f, negro, Rect(ox + lado + 1 + tw + 1.5, oy, AnchoTexto(items[i].Cifra, f) + 0.4, lh), XStringFormats.TopLeft);
            }
            var libre = h - altoLeyenda - 2;
            d = Math.Max(2, Math.Min(w, libre));
            cxT = x + w / 2;
            cyT = y + libre / 2;
        }

        var caja = Rect(cxT - d / 2, cyT - d / 2, d, d);
        var raya = new XPen(papel, Separacion * MmPt);
        if (n == 1) g.DrawEllipse(new XSolidBrush(Color(s.Colores![0], XColors.Gray)), caja);
        else
        {
            double angulo = -90;
            for (var i = 0; i < n; i++)
            {
                var barrido = (double)(s.Valores[i]!.Value / total) * 360;
                if (barrido > 0.01) g.DrawPie(raya, new XSolidBrush(Color(s.Colores![i], XColors.Gray)), caja, angulo, barrido);
                angulo += barrido;
            }
        }
        if (gr.Tipo != "ANI") return;
        var hueco = d * 0.55;
        g.DrawEllipse(new XSolidBrush(papel), Rect(cxT - hueco / 2, cyT - hueco / 2, hueco, hueco));
        var texto = gr.Texto(total);
        var fb = Fuente(o, true);
        if (AnchoTexto(texto, fb) <= hueco * 0.85 && hueco >= 2.4 * lh)
        {
            g.DrawString(texto, fb, negro, Rect(cxT - hueco / 2, cyT - lh, hueco, lh), XStringFormats.BottomCenter);
            g.DrawString("Total", f, new XSolidBrush(GraficoTenue), Rect(cxT - hueco / 2, cyT, hueco, lh), XStringFormats.TopCenter);
        }
    }

    /// <summary>Una columna o barra: el extremo de los datos con las esquinas redondeadas y la base recta. lado: A arriba,
    /// B abajo, D derecha, I izquierda; otro, todo recto (las partes de adentro de una pila).</summary>
    private static void Barra(XGraphics g, XBrush brocha, double x, double y, double w, double h, char lado)
    {
        if (w <= 0.01 || h <= 0.01) return;
        var r = lado is 'A' or 'B' ? Math.Min(Math.Min(0.6, w / 2), h) : Math.Min(Math.Min(0.6, h / 2), w);
        if (lado is not ('A' or 'B' or 'D' or 'I') || r < 0.05)
        {
            g.DrawRectangle(brocha, Rect(x, y, w, h));
            return;
        }
        var (x1, y1, x2, y2, d) = (x * MmPt, y * MmPt, (x + w) * MmPt, (y + h) * MmPt, 2 * r * MmPt);
        var p = new XGraphicsPath();
        switch (lado)
        {
            case 'A':
                p.AddLine(x1, y2, x1, y1 + d / 2);
                p.AddArc(x1, y1, d, d, 180, 90);
                p.AddLine(x1 + d / 2, y1, x2 - d / 2, y1);
                p.AddArc(x2 - d, y1, d, d, 270, 90);
                p.AddLine(x2, y1 + d / 2, x2, y2);
                break;
            case 'B':
                p.AddLine(x2, y1, x2, y2 - d / 2);
                p.AddArc(x2 - d, y2 - d, d, d, 0, 90);
                p.AddLine(x2 - d / 2, y2, x1 + d / 2, y2);
                p.AddArc(x1, y2 - d, d, d, 90, 90);
                p.AddLine(x1, y2 - d / 2, x1, y1);
                break;
            case 'D':
                p.AddLine(x1, y1, x2 - d / 2, y1);
                p.AddArc(x2 - d, y1, d, d, 270, 90);
                p.AddLine(x2, y1 + d / 2, x2, y2 - d / 2);
                p.AddArc(x2 - d, y2 - d, d, d, 0, 90);
                p.AddLine(x2 - d / 2, y2, x1, y2);
                break;
            default:
                p.AddLine(x2, y2, x1 + d / 2, y2);
                p.AddArc(x1, y2 - d, d, d, 90, 90);
                p.AddLine(x1, y2 - d / 2, x1, y1 + d / 2);
                p.AddArc(x1, y1, d, d, 180, 90);
                p.AddLine(x1 + d / 2, y1, x2, y1);
                break;
        }
        p.CloseFigure();
        g.DrawPath(brocha, p);
    }

    // ── El Excel ────────────────────────────────────────────────────────────────
    /// <summary>Un gráfico que va en el Excel: dónde (celdas de la hoja, desde 0) y dónde quedaron sus números en la hoja
    /// de datos (filas desde 1).</summary>
    private sealed class GraficoXls
    {
        public ReporteGrafico Grafico { get; init; } = null!;
        public int    Col1 { get; init; }
        public int    Fila1 { get; init; }
        public int    Col2 { get; init; }
        public int    Fila2 { get; init; }
        public string Letra { get; init; } = "Arial";
        public int    Tamano { get; init; } = 800;
        public string? Fondo { get; init; }
        public double AnchoMm { get; init; }
        public double AltoMm { get; init; }
        public string Hoja { get; set; } = "";
        public int    FilaTitulos { get; set; }
    }

    /// <summary>Un gráfico en la hoja: su fondo en las celdas y el pedido para agregarlo al final (o «Sin datos»).</summary>
    private void GraficoExcel(IXLWorksheet ws, ReporteBandasBloque b, ReporteLienzoObjeto o, FilasBloque f, List<double> cols)
    {
        if (!b.Graficos.TryGetValue(o.NUM_OBJETO, out var gr)) return;
        var (y, h) = Vertical(b, o);
        var (r1, r2) = Filas(f, y, h);
        var (c1, c2) = Columnas(cols, X(o), (double)o.NUM_ANCHO);
        var fondo = _f.Color(ReporteFormulasDatos.Objeto, o.NUM_OBJETO, "FONDO", Lugar(b.Inst, 1, 1)) ?? o.DES_COLOR_FONDO;
        if (!string.IsNullOrEmpty(fondo)) ws.Range(r1, c1, r2, c2).Style.Fill.BackgroundColor = ColorXls(fondo, XLColor.NoColor);
        var tamano = (double)(o.NUM_TAMANO_LETRA ?? _ctx.D.NUM_TAMANO_LETRA);
        if (gr.Vacio)
        {
            var celda = ws.Cell(r1, c1);
            if (!celda.IsEmpty()) return;
            celda.Value = "Sin datos";
            celda.Style.Font.FontName = Letra(o.DES_FUENTE);
            celda.Style.Font.FontSize = tamano;
            celda.Style.Font.FontColor = XLColor.FromHtml("#898781");
            return;
        }
        _graficosXls.Add(new GraficoXls
        {
            Grafico = gr, Col1 = c1 - 1, Fila1 = r1 - 1, Col2 = c2, Fila2 = r2, Letra = Letra(o.DES_FUENTE),
            Tamano = (int)Math.Round(Math.Clamp(tamano, 4, 72) * 100), Fondo = ColorValido(fondo),
            AnchoMm = (double)o.NUM_ANCHO, AltoMm = h
        });
    }

    /// <summary>La hoja «Datos de gráficos»: por cada gráfico, su número, los títulos (las categorías y cada serie) y una
    /// fila por categoría.</summary>
    private void HojaGraficos(XLWorkbook wb)
    {
        var nombre = "Datos de gráficos";
        for (var k = 2; wb.Worksheets.Contains(nombre); k++) nombre = $"Datos de gráficos ({k})";
        var ws = wb.Worksheets.Add(nombre);
        ws.Column(1).Width = 32;
        var r = 1;
        for (var k = 0; k < _graficosXls.Count; k++)
        {
            var x = _graficosXls[k];
            var gr = x.Grafico;
            ws.Cell(r, 1).Value = $"Gráfico {k + 1}";
            ws.Cell(r, 1).Style.Font.Bold = true;
            x.Hoja = nombre;
            x.FilaTitulos = r + 1;
            ws.Cell(r + 1, 1).Value = gr.TituloCategorias;
            for (var j = 0; j < gr.Series.Count; j++)
            {
                ws.Cell(r + 1, j + 2).Value = gr.Series[j].Nombre;
                if (ws.Column(j + 2).Width < 16) ws.Column(j + 2).Width = 16;
            }
            ws.Range(r + 1, 1, r + 1, gr.Series.Count + 1).Style.Font.Bold = true;
            ws.Range(r + 1, 1, r + 1, gr.Series.Count + 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            for (var i = 0; i < gr.Categorias.Count; i++)
            {
                ws.Cell(r + 2 + i, 1).Value = gr.Categorias[i];
                for (var j = 0; j < gr.Series.Count; j++)
                {
                    if (gr.Series[j].Valores[i] is not { } v) continue;
                    var celda = ws.Cell(r + 2 + i, j + 2);
                    celda.Value = v;
                    if (gr.FormatoExcel is not null) celda.Style.NumberFormat.Format = gr.FormatoExcel;
                }
            }
            r += gr.Categorias.Count + 3;
        }
    }

    /// <summary>Agrega los gráficos al libro ya grabado (ClosedXML no los hace): una parte de gráfico por cada uno, anclada a
    /// sus celdas en el dibujo de la hoja (el mismo de las imágenes, si ya hay).</summary>
    private void PonerGraficos(MemoryStream ms, string hoja)
    {
        ms.Position = 0;
        using var doc = SpreadsheetDocument.Open(ms, true);
        var wbp = doc.WorkbookPart!;
        var sheet = wbp.Workbook.Sheets!.Elements<S.Sheet>().First(x => x.Name?.Value == hoja);
        var wsp = (WorksheetPart)wbp.GetPartById(sheet.Id!.Value!);
        var dp = wsp.DrawingsPart;
        if (dp is null)
        {
            dp = wsp.AddNewPart<DrawingsPart>();
            dp.WorksheetDrawing = new Xdr.WorksheetDrawing();
            // El dibujo va antes de estos, en el orden que pide Excel.
            var dibujo = new S.Drawing { Id = wsp.GetIdOfPart(dp) };
            var antes = wsp.Worksheet.ChildElements.FirstOrDefault(e => e is S.LegacyDrawing or S.LegacyDrawingHeaderFooter or S.DrawingHeaderFooter
                                                                          or S.Picture or S.OleObjects or S.Controls or S.WebPublishItems
                                                                          or S.TableParts or S.WorksheetExtensionList);
            if (antes is null) wsp.Worksheet.Append(dibujo);
            else wsp.Worksheet.InsertBefore(dibujo, antes);
            wsp.Worksheet.Save();
        }
        dp.WorksheetDrawing ??= new Xdr.WorksheetDrawing();
        var wd = dp.WorksheetDrawing;
        var id = wd.Descendants<Xdr.NonVisualDrawingProperties>().Select(p => (int)(p.Id?.Value ?? 0)).DefaultIfEmpty(1).Max() + 1;
        for (var k = 0; k < _graficosXls.Count; k++)
        {
            var x = _graficosXls[k];
            var cp = dp.AddNewPart<ChartPart>();
            using (var st = cp.GetStream(FileMode.Create))
            {
                var bytes = new UTF8Encoding(false).GetBytes(XmlGrafico(x));
                st.Write(bytes, 0, bytes.Length);
            }
            wd.Append(new Xdr.TwoCellAnchor(
                $"<xdr:twoCellAnchor editAs=\"twoCell\" xmlns:xdr=\"http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing\" xmlns:a=\"http://schemas.openxmlformats.org/drawingml/2006/main\">" +
                $"<xdr:from><xdr:col>{x.Col1}</xdr:col><xdr:colOff>0</xdr:colOff><xdr:row>{x.Fila1}</xdr:row><xdr:rowOff>0</xdr:rowOff></xdr:from>" +
                $"<xdr:to><xdr:col>{x.Col2}</xdr:col><xdr:colOff>0</xdr:colOff><xdr:row>{x.Fila2}</xdr:row><xdr:rowOff>0</xdr:rowOff></xdr:to>" +
                $"<xdr:graphicFrame macro=\"\"><xdr:nvGraphicFramePr><xdr:cNvPr id=\"{id++}\" name=\"Gráfico {k + 1}\"/><xdr:cNvGraphicFramePr/></xdr:nvGraphicFramePr>" +
                "<xdr:xfrm><a:off x=\"0\" y=\"0\"/><a:ext cx=\"0\" cy=\"0\"/></xdr:xfrm>" +
                "<a:graphic><a:graphicData uri=\"http://schemas.openxmlformats.org/drawingml/2006/chart\">" +
                $"<c:chart xmlns:c=\"http://schemas.openxmlformats.org/drawingml/2006/chart\" xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\" r:id=\"{dp.GetIdOfPart(cp)}\"/>" +
                "</a:graphicData></a:graphic></xdr:graphicFrame><xdr:clientData/></xdr:twoCellAnchor>"));
        }
        wd.Save();
    }

    /// <summary>El gráfico de Excel (DrawingML): el mismo tipo, colores, escala y valores que en el PDF.</summary>
    private static string XmlGrafico(GraficoXls x)
    {
        var gr = x.Grafico;
        var n = gr.Categorias.Count;
        static string E(string? s) => SecurityElement.Escape(s ?? "") ?? "";
        static string Hex(string c) => c.TrimStart('#').ToUpperInvariant();
        static string Relleno(string c) => $"<a:solidFill><a:srgbClr val=\"{Hex(c)}\"/></a:solidFill>";
        static string Num(double v) => v.ToString("R", CultureInfo.InvariantCulture);
        static string Sin(string c) => $"<c:{c} val=\"0\"/>";
        string Letras(string color, bool negrita = false)
            => $"<c:txPr><a:bodyPr/><a:lstStyle/><a:p><a:pPr><a:defRPr sz=\"{x.Tamano}\" b=\"{(negrita ? 1 : 0)}\">{Relleno(color)}<a:latin typeface=\"{E(x.Letra)}\"/></a:defRPr></a:pPr><a:endParaRPr lang=\"es-PE\"/></a:p></c:txPr>";
        var papel = x.Fondo ?? "#ffffff";
        var hoja = "'" + x.Hoja.Replace("'", "''") + "'!";
        var primera = x.FilaTitulos + 1;
        var ultima = x.FilaTitulos + n;
        var formato = E(gr.FormatoExcel ?? "General");
        string Categorias()
        {
            var sb = new StringBuilder($"<c:cat><c:strRef><c:f>{E(hoja)}$A${primera}:$A${ultima}</c:f><c:strCache><c:ptCount val=\"{n}\"/>");
            for (var i = 0; i < n; i++) sb.Append($"<c:pt idx=\"{i}\"><c:v>{E(gr.Categorias[i])}</c:v></c:pt>");
            return sb.Append("</c:strCache></c:strRef></c:cat>").ToString();
        }
        string Valores(int j)
        {
            var col = XLHelper.GetColumnLetterFromNumber(j + 2);
            var sb = new StringBuilder($"<c:val><c:numRef><c:f>{E(hoja)}${col}${primera}:${col}${ultima}</c:f><c:numCache><c:formatCode>{formato}</c:formatCode><c:ptCount val=\"{n}\"/>");
            for (var i = 0; i < n; i++)
                if (gr.Series[j].Valores[i] is { } v) sb.Append($"<c:pt idx=\"{i}\"><c:v>{v.ToString(CultureInfo.InvariantCulture)}</c:v></c:pt>");
            return sb.Append("</c:numCache></c:numRef></c:val>").ToString();
        }
        string Nombre(int j)
        {
            var col = XLHelper.GetColumnLetterFromNumber(j + 2);
            return $"<c:tx><c:strRef><c:f>{E(hoja)}${col}${x.FilaTitulos}</c:f><c:strCache><c:ptCount val=\"1\"/><c:pt idx=\"0\"><c:v>{E(gr.Series[j].Nombre)}</c:v></c:pt></c:strCache></c:strRef></c:tx>";
        }
        // Las etiquetas de los valores: en todos los puntos o en uno solo (el último de una línea). En una torta, el porcentaje
        // de cada porción (el valor está en la hoja de datos: Excel no lo pone en la leyenda).
        string Etiquetas(string? posicion, int? solo = null, bool porcentaje = false)
        {
            var cuerpo = $"<c:numFmt formatCode=\"{(porcentaje ? "0.0%" : formato)}\" sourceLinked=\"0\"/><c:spPr><a:noFill/><a:ln><a:noFill/></a:ln></c:spPr>{Letras("#0b0b0b")}" +
                         (posicion is null ? "" : $"<c:dLblPos val=\"{posicion}\"/>") +
                         $"{Sin("showLegendKey")}<c:showVal val=\"{(porcentaje ? 0 : 1)}\"/>{Sin("showCatName")}{Sin("showSerName")}<c:showPercent val=\"{(porcentaje ? 1 : 0)}\"/>{Sin("showBubbleSize")}";
            if (solo is null) return $"<c:dLbls>{cuerpo}</c:dLbls>";
            return $"<c:dLbls><c:dLbl><c:idx val=\"{solo}\"/>{cuerpo}</c:dLbl>{Sin("showLegendKey")}{Sin("showVal")}{Sin("showCatName")}{Sin("showSerName")}{Sin("showPercent")}{Sin("showBubbleSize")}</c:dLbls>";
        }

        var graf = new StringBuilder();
        var conEjes = !gr.EsTorta;
        if (gr.EsTorta)
        {
            var s = gr.Series[0];
            graf.Append(gr.Tipo == "ANI" ? "<c:doughnutChart>" : "<c:pieChart>").Append("<c:varyColors val=\"1\"/>");
            graf.Append($"<c:ser><c:idx val=\"0\"/><c:order val=\"0\"/>{Nombre(0)}<c:spPr><a:ln w=\"18000\">{Relleno(papel)}</a:ln></c:spPr>");
            for (var i = 0; i < n; i++)
                graf.Append($"<c:dPt><c:idx val=\"{i}\"/><c:bubble3D val=\"0\"/><c:spPr>{Relleno(s.Colores![i])}<a:ln w=\"18000\">{Relleno(papel)}</a:ln></c:spPr></c:dPt>");
            if (gr.Valores) graf.Append(Etiquetas(gr.Tipo == "ANI" ? null : "bestFit", porcentaje: true));
            graf.Append(Categorias()).Append(Valores(0)).Append("</c:ser><c:firstSliceAng val=\"0\"/>");
            graf.Append(gr.Tipo == "ANI" ? "<c:holeSize val=\"55\"/></c:doughnutChart>" : "</c:pieChart>");
        }
        else if (gr.Tipo == "LIN")
        {
            var puntos = n <= 40;
            graf.Append("<c:lineChart><c:grouping val=\"standard\"/><c:varyColors val=\"0\"/>");
            for (var j = 0; j < gr.Series.Count; j++)
            {
                var color = gr.Series[j].Color;
                graf.Append($"<c:ser><c:idx val=\"{j}\"/><c:order val=\"{j}\"/>{Nombre(j)}");
                graf.Append($"<c:spPr><a:ln w=\"19050\" cap=\"rnd\">{Relleno(color)}<a:round/></a:ln></c:spPr>");
                graf.Append(puntos
                    ? $"<c:marker><c:symbol val=\"circle\"/><c:size val=\"5\"/><c:spPr>{Relleno(color)}<a:ln w=\"6350\">{Relleno(papel)}</a:ln></c:spPr></c:marker>"
                    : "<c:marker><c:symbol val=\"none\"/></c:marker>");
                var ult = Array.FindLastIndex(gr.Series[j].Valores, v => v is not null);
                if (gr.Valores && ult >= 0) graf.Append(Etiquetas("r", ult));
                graf.Append(Categorias()).Append(Valores(j)).Append("<c:smooth val=\"0\"/></c:ser>");
            }
            graf.Append("<c:marker val=\"1\"/><c:axId val=\"500\"/><c:axId val=\"501\"/></c:lineChart>");
        }
        else
        {
            var barras = gr.Tipo == "BAR";
            graf.Append($"<c:barChart><c:barDir val=\"{(barras ? "bar" : "col")}\"/><c:grouping val=\"{(gr.Apilado ? "stacked" : "clustered")}\"/><c:varyColors val=\"0\"/>");
            for (var j = 0; j < gr.Series.Count; j++)
            {
                graf.Append($"<c:ser><c:idx val=\"{j}\"/><c:order val=\"{j}\"/>{Nombre(j)}<c:spPr>{Relleno(gr.Series[j].Color)}");
                graf.Append(gr.Apilado ? $"<a:ln w=\"6350\">{Relleno(papel)}</a:ln>" : "<a:ln><a:noFill/></a:ln>");
                graf.Append("</c:spPr><c:invertIfNegative val=\"0\"/>");
                if (gr.Valores && !gr.Apilado) graf.Append(Etiquetas("outEnd"));
                graf.Append(Categorias()).Append(Valores(j)).Append("</c:ser>");
            }
            graf.Append($"<c:gapWidth val=\"40\"/><c:overlap val=\"{(gr.Apilado ? 100 : -10)}\"/><c:axId val=\"500\"/><c:axId val=\"501\"/></c:barChart>");
        }

        if (conEjes)
        {
            // La misma escala del PDF (con las marcas que pide el tamaño del objeto) y los números cortos del eje.
            var barras = gr.Tipo == "BAR";
            var (min, max) = DominioGrafico(gr);
            var marcas = barras ? Math.Clamp((int)((x.AnchoMm - 30) / 18), 2, 6) : Math.Clamp((int)((x.AltoMm - 12) / 9), 2, 6);
            var (desde, hasta, paso) = EscalaEje(min, max, marcas);
            var (div, suf, dec) = UnidadEje(desde, hasta, paso);
            var numero = "#,##0" + (dec > 0 ? "." + new string('0', dec) : "") + (div >= 1e6 ? ",," : div >= 1e3 ? "," : "") + (suf.Length > 0 ? $"\"{suf}\"" : "");
            graf.Append("<c:catAx><c:axId val=\"500\"/>");
            graf.Append($"<c:scaling><c:orientation val=\"{(barras ? "maxMin" : "minMax")}\"/></c:scaling><c:delete val=\"0\"/><c:axPos val=\"{(barras ? "l" : "b")}\"/>");
            graf.Append("<c:numFmt formatCode=\"General\" sourceLinked=\"0\"/><c:majorTickMark val=\"none\"/><c:minorTickMark val=\"none\"/><c:tickLblPos val=\"low\"/>");
            graf.Append($"<c:spPr><a:ln w=\"9525\">{Relleno("#c3c2b7")}</a:ln></c:spPr>{Letras("#52514e")}");
            graf.Append("<c:crossAx val=\"501\"/><c:crosses val=\"autoZero\"/><c:auto val=\"1\"/><c:lblAlgn val=\"ctr\"/><c:lblOffset val=\"100\"/><c:noMultiLvlLbl val=\"0\"/></c:catAx>");
            graf.Append("<c:valAx><c:axId val=\"501\"/>");
            graf.Append($"<c:scaling><c:orientation val=\"minMax\"/><c:max val=\"{Num(hasta)}\"/><c:min val=\"{Num(desde)}\"/></c:scaling><c:delete val=\"0\"/><c:axPos val=\"{(barras ? "b" : "l")}\"/>");
            graf.Append($"<c:majorGridlines><c:spPr><a:ln w=\"3175\">{Relleno("#e1e0d9")}</a:ln></c:spPr></c:majorGridlines>");
            graf.Append($"<c:numFmt formatCode=\"{E(numero)}\" sourceLinked=\"0\"/><c:majorTickMark val=\"none\"/><c:minorTickMark val=\"none\"/><c:tickLblPos val=\"nextTo\"/>");
            graf.Append($"<c:spPr><a:ln><a:noFill/></a:ln></c:spPr>{Letras("#898781")}");
            // En las barras, las categorías van de arriba hacia abajo: el eje de los valores cruza en la última (abajo).
            graf.Append($"<c:crossAx val=\"500\"/><c:crosses val=\"{(barras ? "max" : "autoZero")}\"/><c:crossBetween val=\"between\"/><c:majorUnit val=\"{Num(paso)}\"/></c:valAx>");
        }

        var leyenda = gr.EsTorta
            ? $"<c:legend><c:legendPos val=\"{(x.AnchoMm >= x.AltoMm * 1.2 ? "r" : "b")}\"/><c:overlay val=\"0\"/>{Letras("#52514e")}</c:legend>"
            : gr.Series.Count > 1 ? $"<c:legend><c:legendPos val=\"t\"/><c:overlay val=\"0\"/>{Letras("#52514e")}</c:legend>" : "";
        var fondo = x.Fondo is null ? "<a:noFill/>" : Relleno(x.Fondo);
        return "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
               "<c:chartSpace xmlns:c=\"http://schemas.openxmlformats.org/drawingml/2006/chart\" xmlns:a=\"http://schemas.openxmlformats.org/drawingml/2006/main\" xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\">" +
               "<c:date1904 val=\"0\"/><c:roundedCorners val=\"0\"/>" +
               $"<c:chart><c:autoTitleDeleted val=\"1\"/><c:plotArea><c:layout/>{graf}</c:plotArea>{leyenda}<c:plotVisOnly val=\"1\"/><c:dispBlanksAs val=\"gap\"/></c:chart>" +
               $"<c:spPr>{fondo}<a:ln><a:noFill/></a:ln></c:spPr>{Letras("#52514e")}</c:chartSpace>";
    }
}
