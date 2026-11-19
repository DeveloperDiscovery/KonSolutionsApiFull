using System.Text.Json;
using PdfSharp.Drawing;
using KonSolutionsApi.Domain.Models.Reportes;

namespace KonSolutionsApi.Application.Reportes;

// Diseño libre (BAN): lo que no es el PDF.
//   - ReporteBandasListado: el orden de las filas y, para Excel, CSV y las plantillas de Word y Excel, el diseño libre
//     leído como un listado (o como una ficha si tiene tablas): las columnas son los campos del detalle, de izquierda a
//     derecha; los grupos, los del lienzo; los totales, los resúmenes que suman. Así el Excel trae los mismos datos que
//     el PDF, sin intentar copiar su dibujo. Un campo de fórmula es una columna más, calculada fila por fila, y una fila
//     cuyo detalle no se imprime por una condición tampoco sale.
//   - ReporteConversor: «Convertir a bandas», que arma el lienzo de un listado o una ficha para que salga igual que hoy.

public static class ReporteBandasListado
{
    /// <summary>El orden de las filas del resultado 1: primero los grupos del lienzo; después el orden que tengan las
    /// columnas del diseño (A o D, por prioridad), como en un listado.</summary>
    public static List<(string Nombre, bool Desc)> Claves(ReporteDefinicion def, ReporteTabla? tabla)
    {
        if (tabla is null) return new();
        var grupos = (def.Lienzo?.Grupos ?? new()).OrderBy(g => g.NUM_NIVEL).ToList();
        var deGrupo = new HashSet<string>(grupos.Select(g => g.DES_COLUMNA), StringComparer.OrdinalIgnoreCase);
        return grupos.Where(g => g.COD_ORDEN is "A" or "D").Select(g => (g.DES_COLUMNA, Desc: g.COD_ORDEN == "D"))
            .Concat(def.Elementos.Where(e => e.NUM_RESULTADO == tabla.Numero && e.NUM_NIVEL_GRUPO == 0 && !deGrupo.Contains(e.DES_COLUMNA)
                                             && (e.COD_ORDEN ?? "").ToUpperInvariant() is "A" or "D")
                                 .OrderBy(e => e.NUM_PRIORIDAD_ORDEN ?? 99).ThenBy(e => e.NUM_ORDEN)
                                 .Select(e => (e.DES_COLUMNA, Desc: e.COD_ORDEN!.ToUpperInvariant() == "D")))
            .Where(k => tabla.Indice(k.Item1) >= 0)
            .ToList();
    }

    /// <summary>El contexto de un diseño libre visto como listado (o ficha, si el lienzo tiene tablas), para los
    /// formatos que no dibujan el lienzo.</summary>
    public static ReporteContexto Sintetizar(ReporteContexto ctx)
    {
        var def = ctx.Definicion;
        var l = def.Lienzo ?? new ReporteLienzo();
        var bandas = l.Bandas.Where(b => !b.FLG_SUPRIMIR).ToDictionary(b => b.NUM_BANDA);
        var objetos = l.Objetos.Where(o => bandas.ContainsKey(o.NUM_BANDA))
                               .OrderBy(o => ReporteConversor.OrdenBanda(bandas[o.NUM_BANDA]))
                               .ThenBy(o => o.NUM_Y).ThenBy(o => o.NUM_X).ToList();
        string Tipo(ReporteLienzoObjeto o) => bandas[o.NUM_BANDA].COD_TIPO_BANDA;
        var r1 = (ctx.Datos.Tabla(1) ?? ctx.Datos.Tablas.FirstOrDefault())?.Numero ?? 1;

        // Los campos de fórmula salen como columnas calculadas del resultado 1 y se tratan como un campo más; las filas
        // cuyo detalle no se imprime por una condición no salen, como en el PDF.
        var datos = ctx.Datos;
        var formulas = objetos.Where(o => o.COD_TIPO_OBJETO == "FOR" && !string.IsNullOrWhiteSpace(o.DES_ORIGEN) && Tipo(o) is not ("PH" or "PF"))
                              .Select(o => o.DES_ORIGEN!).ToList();
        if (formulas.Count > 0 || l.Condiciones.Count > 0)
        {
            datos = ReporteFormulasDiseno.Calcular(ctx, formulas, out var columnas);
            objetos = objetos.Select(o =>
            {
                if (o.COD_TIPO_OBJETO != "FOR" || !columnas.TryGetValue((o.DES_ORIGEN ?? "").Trim(), out var col)) return o;
                var campo = Copiar<ReporteLienzoObjeto>(o);
                campo.COD_TIPO_OBJETO = "CAM";
                campo.DES_ORIGEN = col;
                campo.NUM_RESULTADO = r1;
                return campo;
            }).ToList();
        }

        var cab = Clonar(def.Cabecera);
        var nueva = new ReporteDefinicion
        {
            Cabecera = cab, Parametros = def.Parametros, Resultados = def.Resultados, Columnas = def.Columnas,
            Formatos = def.Formatos, Lienzo = def.Lienzo, Secciones = def.Secciones
        };

        // Una tabla enlazada (el detalle de cada fila) no hace ficha: es parte del listado, que sale sin ella.
        if (objetos.Any(o => o.COD_TIPO_OBJETO == "TAB" && string.IsNullOrWhiteSpace(o.DES_ENLACES))) Ficha(nueva, def, objetos, Tipo);
        else Listado(nueva, def, l, objetos, Tipo, r1);

        return new ReporteContexto
        {
            Definicion = nueva, Datos = datos, Valores = ctx.Valores, Empresa = ctx.Empresa, Usuario = ctx.Usuario,
            Fecha = ctx.Fecha, Plantilla = ctx.Plantilla, ModoPlantilla = ctx.ModoPlantilla
        };
    }

    private static void Listado(ReporteDefinicion nueva, ReporteDefinicion def, ReporteLienzo l, List<ReporteLienzoObjeto> objetos,
                                Func<ReporteLienzoObjeto, string> tipo, int r1)
    {
        var cab = nueva.Cabecera;
        cab.COD_TIPO_DISENO = "LIS";
        cab.FLG_SUBTOTAL_GRUPO = objetos.Any(o => o.COD_TIPO_OBJETO == "RES" && tipo(o) == "GF");
        cab.FLG_TOTAL_GENERAL = objetos.Any(o => o.COD_TIPO_OBJETO == "RES" && tipo(o) == "RF");
        var grupos = l.Grupos.OrderBy(g => g.NUM_NIVEL).ToList();
        cab.FLG_SALTO_GRUPO = grupos.FirstOrDefault()?.FLG_NUEVA_PAGINA ?? false;

        var campos = objetos.Where(o => tipo(o) == "DE" && o.COD_TIPO_OBJETO == "CAM" && (o.NUM_RESULTADO ?? 1) == r1 && !string.IsNullOrWhiteSpace(o.DES_ORIGEN))
                            .OrderBy(o => o.NUM_X).ThenBy(o => o.NUM_Y)
                            .GroupBy(o => o.DES_ORIGEN!, StringComparer.OrdinalIgnoreCase).Select(g => g.First()).ToList();
        if (campos.Count == 0 && grupos.Count == 0) { nueva.Elementos = def.Elementos; return; }

        var sumas = new HashSet<string>(objetos.Where(o => o.COD_TIPO_OBJETO == "RES" && (o.COD_FUNCION ?? "SUM") == "SUM" && tipo(o) is "GF" or "RF")
                                               .Select(o => o.DES_ORIGEN ?? ""), StringComparer.OrdinalIgnoreCase);
        var titulos = objetos.Where(o => o.COD_TIPO_OBJETO == "TXT" && tipo(o) is "PH" or "RH" && !(o.DES_ORIGEN ?? "").Contains('{')).ToList();
        var previos = def.Elementos.Where(e => e.NUM_RESULTADO == r1).GroupBy(e => e.DES_COLUMNA, StringComparer.OrdinalIgnoreCase)
                                   .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);
        int Nivel(string col) => grupos.FindIndex(g => string.Equals(g.DES_COLUMNA, col, StringComparison.OrdinalIgnoreCase)) + 1;
        static string? Orden(ReporteLienzoGrupo g) => g.COD_ORDEN is "A" or "D" ? g.COD_ORDEN : null;

        var elementos = new List<ReporteDisenoElemento>();
        var n = 0;
        foreach (var c in campos)
        {
            var col = c.DES_ORIGEN!;
            previos.TryGetValue(col, out var p);
            var nivel = Nivel(col);
            elementos.Add(new ReporteDisenoElemento
            {
                NUM_RESULTADO = r1, DES_COLUMNA = col, FLG_VISIBLE = true, NUM_ORDEN = ++n,
                DES_ETIQUETA = Titulo(c, titulos) ?? p?.DES_ETIQUETA,
                NUM_ANCHO = Math.Max(1, (int)Math.Round(c.NUM_ANCHO)),
                COD_ALINEACION = c.COD_ALINEACION is "I" or "C" or "D" ? c.COD_ALINEACION : p?.COD_ALINEACION,
                DES_FORMATO = ReporteColumnas.Vacio(c.DES_FORMATO) ?? p?.DES_FORMATO,
                FLG_TOTALIZA = sumas.Contains(col),
                NUM_NIVEL_GRUPO = nivel,
                COD_ORDEN = nivel > 0 ? Orden(grupos[nivel - 1]) : p?.COD_ORDEN,
                NUM_PRIORIDAD_ORDEN = nivel > 0 ? null : p?.NUM_PRIORIDAD_ORDEN
            });
        }
        // Los grupos que no se ven en el detalle van ocultos (salen en la cabecera del grupo); y las columnas que solo
        // ordenan, también.
        foreach (var g in grupos.Where(g => !elementos.Any(e => string.Equals(e.DES_COLUMNA, g.DES_COLUMNA, StringComparison.OrdinalIgnoreCase))))
        {
            previos.TryGetValue(g.DES_COLUMNA, out var p);
            elementos.Add(new ReporteDisenoElemento
            {
                NUM_RESULTADO = r1, DES_COLUMNA = g.DES_COLUMNA, FLG_VISIBLE = false, NUM_ORDEN = ++n, NUM_NIVEL_GRUPO = g.NUM_NIVEL,
                COD_ORDEN = Orden(g), DES_ETIQUETA = p?.DES_ETIQUETA, DES_FORMATO = p?.DES_FORMATO
            });
        }
        foreach (var p in previos.Values.Where(p => p.NUM_NIVEL_GRUPO == 0 && (p.COD_ORDEN ?? "").ToUpperInvariant() is "A" or "D"
                                                && !elementos.Any(e => string.Equals(e.DES_COLUMNA, p.DES_COLUMNA, StringComparison.OrdinalIgnoreCase))))
        {
            elementos.Add(new ReporteDisenoElemento
            {
                NUM_RESULTADO = r1, DES_COLUMNA = p.DES_COLUMNA, FLG_VISIBLE = false, NUM_ORDEN = ++n,
                COD_ORDEN = p.COD_ORDEN, NUM_PRIORIDAD_ORDEN = p.NUM_PRIORIDAD_ORDEN
            });
        }
        nueva.Elementos = elementos.Concat(def.Elementos.Where(e => e.NUM_RESULTADO != r1)).ToList();
    }

    /// <summary>El título de una columna: el texto del encabezado que está encima del campo (el que más se le
    /// superpone, y el más bajo si hay varios).</summary>
    private static string? Titulo(ReporteLienzoObjeto campo, List<ReporteLienzoObjeto> titulos)
    {
        double x1 = (double)campo.NUM_X, x2 = x1 + (double)campo.NUM_ANCHO;
        return titulos.Select(t =>
                      {
                          double a = (double)t.NUM_X, b = a + (double)t.NUM_ANCHO;
                          var cruce = Math.Min(x2, b) - Math.Max(x1, a);
                          var ok = cruce >= 0.5 * Math.Min(x2 - x1, b - a) && (b - a) <= 3 * Math.Max(1, x2 - x1);
                          return (t, cruce, ok);
                      })
                      .Where(x => x.ok)
                      .OrderByDescending(x => x.cruce).ThenByDescending(x => x.t.NUM_Y)
                      .Select(x => ReporteColumnas.Vacio(x.t.DES_ORIGEN))
                      .FirstOrDefault();
    }

    private static void Ficha(ReporteDefinicion nueva, ReporteDefinicion def, List<ReporteLienzoObjeto> objetos, Func<ReporteLienzoObjeto, string> tipo)
    {
        nueva.Cabecera.COD_TIPO_DISENO = "FIC";
        var secciones = new List<ReporteDisenoSeccion>();
        var elementos = new List<ReporteDisenoElemento>();
        var deCampos = new HashSet<int>();
        foreach (var o in objetos.Where(o => tipo(o) is not ("PH" or "PF")))
        {
            if (o.COD_TIPO_OBJETO is not ("CAM" or "TAB")) continue;
            var r = o.NUM_RESULTADO ?? 1;
            var previa = def.Secciones.FirstOrDefault(s => s.NUM_RESULTADO == r);
            if (!secciones.Any(s => s.NUM_RESULTADO == r))
            {
                var esTabla = o.COD_TIPO_OBJETO == "TAB";
                var opciones = (o.DES_ORIGEN ?? "").ToUpperInvariant();
                secciones.Add(new ReporteDisenoSeccion
                {
                    NUM_RESULTADO = r, NUM_ORDEN = secciones.Count + 1,
                    COD_PRESENTACION = !esTabla ? "CAM" : opciones.Contains("SIN_TITULOS") ? "TOT" : "TAB",
                    NUM_COLUMNAS = previa?.NUM_COLUMNAS,
                    DES_TITULO = previa?.DES_TITULO ?? (esTabla ? def.Resultados.FirstOrDefault(x => x.NUM_RESULTADO == r)?.DES_RESULTADO : null)
                });
                if (!esTabla) deCampos.Add(r);
            }
            if (o.COD_TIPO_OBJETO != "CAM" || !deCampos.Contains(r) || string.IsNullOrWhiteSpace(o.DES_ORIGEN)) continue;
            if (elementos.Any(e => e.NUM_RESULTADO == r && string.Equals(e.DES_COLUMNA, o.DES_ORIGEN, StringComparison.OrdinalIgnoreCase))) continue;
            var p = def.Elementos.FirstOrDefault(e => e.NUM_RESULTADO == r && string.Equals(e.DES_COLUMNA, o.DES_ORIGEN, StringComparison.OrdinalIgnoreCase));
            elementos.Add(new ReporteDisenoElemento
            {
                NUM_RESULTADO = r, DES_COLUMNA = o.DES_ORIGEN!, FLG_VISIBLE = true, NUM_ORDEN = elementos.Count(e => e.NUM_RESULTADO == r) + 1,
                DES_ETIQUETA = Rotulo(o, objetos) ?? p?.DES_ETIQUETA,
                NUM_ANCHO = p?.NUM_ANCHO ?? 1,
                COD_ALINEACION = o.COD_ALINEACION is "I" or "C" or "D" ? o.COD_ALINEACION : p?.COD_ALINEACION,
                DES_FORMATO = ReporteColumnas.Vacio(o.DES_FORMATO) ?? p?.DES_FORMATO
            });
        }
        nueva.Secciones = secciones;
        nueva.Elementos = elementos.Concat(def.Elementos.Where(e => !deCampos.Contains(e.NUM_RESULTADO))).ToList();
    }

    /// <summary>El rótulo de un campo de ficha: el texto que tiene a su izquierda en la misma línea ("Proveedor:").</summary>
    private static string? Rotulo(ReporteLienzoObjeto campo, List<ReporteLienzoObjeto> objetos)
    {
        double y1 = (double)campo.NUM_Y, y2 = y1 + (double)campo.NUM_ALTO, x = (double)campo.NUM_X;
        return objetos.Where(t => t.COD_TIPO_OBJETO == "TXT" && t.NUM_BANDA == campo.NUM_BANDA && !(t.DES_ORIGEN ?? "").Contains('{'))
                      .Select(t => (t, fin: (double)(t.NUM_X + t.NUM_ANCHO), arriba: (double)t.NUM_Y, abajo: (double)(t.NUM_Y + t.NUM_ALTO)))
                      .Where(z => z.fin <= x + 1 && z.fin >= x - 15 && Math.Min(y2, z.abajo) - Math.Max(y1, z.arriba) > 0)
                      .OrderByDescending(z => z.fin)
                      .Select(z => ReporteColumnas.Vacio(z.t.DES_ORIGEN?.Trim().TrimEnd(':').Trim()))
                      .FirstOrDefault();
    }

    internal static T Clonar<T>(T x) => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(x))!;

    /// <summary>Copia las propiedades que se llaman igual (un diseño como cabecera de definición).</summary>
    internal static T Copiar<T>(object x) => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(x, x.GetType()))!;
}

/// <summary>«Convertir a bandas»: el lienzo que dibuja un listado o una ficha igual que el PDF de siempre. Todo en
/// milímetros, dentro de los márgenes (12 mm por lado).</summary>
public static class ReporteConversor
{
    private const double MmPt = 72.0 / 25.4;
    private const string Tinta = "#163A5F";
    private const string Gris = "#78909C";
    private const string ColorLinea = "#CFD8DC";
    private const string Grupo1 = "#E8EAF6";
    private const string Grupo2 = "#F3F4F6";
    private const double Relleno = 0.6;

    /// <summary>Orden en que se imprimen las bandas (para leer el lienzo de arriba abajo).</summary>
    public static int OrdenBanda(ReporteLienzoBanda b) => b.COD_TIPO_BANDA switch
    {
        "RH" => 0,
        "PH" => 100,
        "GH" => 200 + b.NUM_NIVEL * 10 + b.NUM_SUBSECCION,
        "DE" => 300 + b.NUM_SUBSECCION,
        "GF" => 400 + (9 - b.NUM_NIVEL) * 10 + b.NUM_SUBSECCION,
        "RF" => 500 + b.NUM_SUBSECCION,
        _    => 600 + b.NUM_SUBSECCION
    };

    /// <summary>El diseño nuevo: una copia del listado o la ficha, de tipo libre, con su lienzo. Word queda apagado
    /// (en un diseño libre sale solo con plantilla propia) y las plantillas propias no se copian.</summary>
    public static ReporteDisenoDetalle Convertir(ReporteDefinicion def, ReporteDisenoDetalle d)
    {
        var x = ReporteBandasListado.Clonar(d.Diseno);
        x.NUM_DISENO = null;
        var nombre = (x.DES_DISENO ?? "Reporte").Trim();
        x.DES_DISENO = (nombre.Length > 90 ? nombre[..90].Trim() : nombre) + " (bandas)";
        x.COD_TIPO_DISENO = "BAN";
        x.FLG_DOC = false;
        x.FLG_PROPIA_DOC = x.FLG_PROPIA_XLS = false;
        x.NUM_BOTONES = 0;
        var formatos = d.Formatos.Select(f => new ReporteDisenoPlantilla
        {
            COD_FORMATO = f.COD_FORMATO, FLG_HABILITADO = f.FLG_HABILITADO && f.COD_FORMATO != "DOC"
        }).ToList();
        if (!formatos.Any(f => f.FLG_HABILITADO))
        {
            formatos.RemoveAll(f => f.COD_FORMATO == "PDF");
            formatos.Add(new ReporteDisenoPlantilla { COD_FORMATO = "PDF", FLG_HABILITADO = true });
        }
        return new ReporteDisenoDetalle
        {
            Diseno = x,
            Secciones = d.Secciones.Select(s => new ReporteDisenoSeccion
            {
                NUM_RESULTADO = s.NUM_RESULTADO, COD_PRESENTACION = s.COD_PRESENTACION, DES_TITULO = s.DES_TITULO, NUM_ORDEN = s.NUM_ORDEN, NUM_COLUMNAS = s.NUM_COLUMNAS
            }).ToList(),
            Elementos = d.Elementos.Select(e => { var c = ReporteBandasListado.Clonar(e); c.NUM_DISENO = null; return c; }).ToList(),
            Formatos = formatos,
            Lienzo = Lienzo(def)
        };
    }

    /// <summary>El lienzo de un diseño libre nuevo, vacío: el encabezado de siempre, un detalle y el pie.</summary>
    public static ReporteLienzo Nuevo(ReporteDefinicion def)
    {
        var a = new Armador(def);
        var ph = a.Banda("PH", 0);
        ph.NUM_ALTO = R(a.Encabezado(ph));
        a.Banda("DE", 6);
        var pf = a.Banda("PF", 0);
        pf.NUM_ALTO = R(a.Pie(pf, filtros: true));
        return a.L;
    }

    /// <summary>El lienzo que dibuja el listado o la ficha como el PDF de siempre.</summary>
    public static ReporteLienzo Lienzo(ReporteDefinicion def)
    {
        var a = new Armador(def);
        if (string.Equals(def.Cabecera.COD_TIPO_DISENO, "FIC", StringComparison.OrdinalIgnoreCase)) a.Ficha();
        else a.Listado();
        return a.L;
    }

    private static decimal R(double v) => Math.Round((decimal)Math.Max(0, v), 2);

    private sealed class Armador
    {
        public readonly ReporteLienzo L = new();
        private readonly ReporteDefinicion _def;
        private readonly ReporteContexto _ctx;
        private readonly XGraphics _medir;
        private readonly double _w;
        private readonly int _letra;
        private int _banda, _objeto;

        public Armador(ReporteDefinicion def)
        {
            _def = def;
            // Sin datos: las columnas salen de la fuente (como al armar la plantilla base).
            _ctx = new ReporteContexto { Definicion = def, ModoPlantilla = true };
            ReporteFuentesPdf.Asegurar();
            _medir = XGraphics.CreateMeasureContext(new XSize(2000, 2000), XGraphicsUnit.Point, XPageDirection.Downwards);
            var (ancho, _) = ReportePapel.Tamano(def.Cabecera.COD_PAPEL, def.Cabecera.COD_ORIENTACION);
            _w = Math.Round((ancho - 2 * ReportePapel.Margen) * 10, 2);
            _letra = Math.Clamp(def.Cabecera.NUM_TAMANO_LETRA, 6, 14);
        }

        private ReporteDefinicionCabecera D => _def.Cabecera;

        // ── Piezas ─────────────────────────────────────────────────────────────
        public ReporteLienzoBanda Banda(string tipo, double alto, int nivel = 0, int sub = 1, string? fondo = null)
        {
            var b = new ReporteLienzoBanda
            {
                NUM_BANDA = ++_banda, COD_TIPO_BANDA = tipo, NUM_NIVEL = nivel, NUM_SUBSECCION = sub, NUM_ALTO = R(alto),
                DES_COLOR_FONDO = fondo, FLG_MANTENER_JUNTO = true
            };
            L.Bandas.Add(b);
            return b;
        }

        private ReporteLienzoObjeto Objeto(ReporteLienzoBanda b, string tipo, double x, double y, double w, double h)
        {
            var o = new ReporteLienzoObjeto
            {
                NUM_OBJETO = ++_objeto, NUM_BANDA = b.NUM_BANDA, COD_TIPO_OBJETO = tipo,
                NUM_X = R(x), NUM_Y = R(y), NUM_ANCHO = R(w), NUM_ALTO = R(h), NUM_ORDEN = _objeto
            };
            L.Objetos.Add(o);
            return o;
        }

        private ReporteLienzoObjeto Texto(ReporteLienzoBanda b, string tipo, string origen, double x, double y, double w, double tam,
                                          bool negrita = false, string alineacion = "I", string? color = null, bool crecer = false, int lineas = 1)
        {
            var o = Objeto(b, tipo, x, y, w, Linea(tam, negrita) * lineas);
            o.DES_ORIGEN = origen;
            o.NUM_TAMANO_LETRA = tam == _letra ? null : (decimal)tam;
            o.FLG_NEGRITA = negrita;
            o.COD_ALINEACION = alineacion;
            o.DES_COLOR = color;
            o.FLG_PUEDE_CRECER = crecer;
            return o;
        }

        private void Raya(ReporteLienzoBanda b, double x, double y, double w, string color, double grosor)
        {
            var o = Objeto(b, "LIN", x, y, w, 0);
            o.DES_COLOR_BORDE = color;
            o.NUM_GROSOR_BORDE = (decimal)grosor;
        }

        private double Linea(double tam, bool negrita = false) => new XFont(ReporteFuentesPdf.Familia, tam, negrita ? XFontStyleEx.Bold : XFontStyleEx.Regular).GetHeight() / MmPt;

        private double Medir(string texto, double tam, bool negrita)
            => _medir.MeasureString(texto, new XFont(ReporteFuentesPdf.Familia, tam, negrita ? XFontStyleEx.Bold : XFontStyleEx.Regular)).Width / MmPt;

        /// <summary>En cuántas líneas entra un texto en un ancho (en mm).</summary>
        private int Lineas(string texto, double tam, bool negrita, double ancho)
        {
            var n = 1;
            var actual = "";
            foreach (var p in texto.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                var prueba = actual.Length == 0 ? p : actual + " " + p;
                if (actual.Length == 0 || Medir(prueba, tam, negrita) <= ancho) actual = prueba;
                else { n++; actual = p; }
            }
            return n;
        }

        private static string Alin(char a) => a is 'D' ? "D" : a is 'C' ? "C" : "I";

        // ── Encabezado y pie de página (los de siempre) ────────────────────────
        /// <summary>Logo a la izquierda, título y subtítulo al centro, datos de la empresa a la derecha y una raya.
        /// Devuelve el alto que ocupa.</summary>
        public double Encabezado(ReporteLienzoBanda b)
        {
            double c1 = _w * 0.22, c3 = _w * 0.26, c2 = _w - c1 - c3;
            if (D.FLG_LOGO)
            {
                var img = Objeto(b, "IMG", 0, 0, c1, 13);
                img.DES_ORIGEN = "LOGO";
                img.COD_ALINEACION = "I";
            }
            var y = 1.0;
            Texto(b, "ESP", "TITULO", c1, y, c2, 13, negrita: true, alineacion: "C", color: Tinta, crecer: true);
            y += Linea(13, true) + 0.3;
            if (!string.IsNullOrWhiteSpace(D.DES_SUBTITULO))
                Texto(b, "ESP", "SUBTITULO", c1, y, c2, 9, alineacion: "C", crecer: true);
            if (D.FLG_DATOS_EMPRESA)
            {
                var ye = 0.5;
                var lh = Linea(7.5, true);
                Texto(b, "ESP", "EMPRESA_NOMBRE", c1 + c2, ye, c3, 7.5, negrita: true, alineacion: "D", crecer: true).FLG_SUPRIMIR_VACIO = true;
                ye += lh;
                Texto(b, "ESP", "EMPRESA_RUC", c1 + c2, ye, c3, 7.5, alineacion: "D").FLG_SUPRIMIR_VACIO = true;
                ye += lh;
                Texto(b, "ESP", "EMPRESA_DIRECCION", c1 + c2, ye, c3, 7.5, alineacion: "D", crecer: true).FLG_SUPRIMIR_VACIO = true;
            }
            Raya(b, 0, 14.5, _w, Tinta, 1.2);
            return 15.5;
        }

        /// <summary>Raya, filtros (solo listados), quién lo generó y "Página N de M". Devuelve el alto.</summary>
        public double Pie(ReporteLienzoBanda b, bool filtros)
        {
            const double tam = 6.5;
            Raya(b, 0, 0, _w, ColorLinea, 0.4);
            var y = 0.8;
            if (filtros && D.FLG_FILTROS_PIE)
                Texto(b, "TXT", "{FILTROS}", 0, y, _w * 0.45, tam, color: Gris, lineas: 2);
            Texto(b, "TXT", "Generado por {USUARIO} el {FECHA_HORA}", _w * 0.45, y, _w * 0.35, tam, alineacion: "C", color: Gris);
            Texto(b, "ESP", "PAGINA_N_DE_M", _w * 0.80, y, _w * 0.20, tam, alineacion: "D", color: Gris);
            return y + 2 * Linea(tam) + 0.4;
        }

        /// <summary>El texto libre del pie y las firmas, al final. Devuelve dónde termina.</summary>
        private double Final(ReporteLienzoBanda b, double y)
        {
            if (!string.IsNullOrWhiteSpace(D.DES_PIE))
            {
                y += 2.8;
                Texto(b, "TXT", D.DES_PIE!, 0, y, _w, _letra, crecer: true);
                y += Linea(_letra);
            }
            var firmas = ReporteFirmas.De(D.DES_FIRMAS);
            if (firmas.Count == 0) return y;
            y += 16;
            var wf = _w / firmas.Count;
            for (var i = 0; i < firmas.Count; i++)
            {
                Raya(b, i * wf + wf * 0.15, y, wf * 0.7, "#000000", 0.5);
                Texto(b, "TXT", firmas[i], i * wf, y + 1, wf, 7.5, alineacion: "C");
            }
            return y + 1 + Linea(7.5) + 1;
        }

        // ── Listado ────────────────────────────────────────────────────────────
        public void Listado()
        {
            var r1 = 1;
            var todas = ReporteColumnas.De(_ctx, r1, incluirOcultas: true);
            var cols = todas.Where(c => c.Visible).ToList();
            var grupos = todas.Where(c => c.NivelGrupo > 0).OrderBy(c => c.NivelGrupo).ThenBy(c => c.Orden).ToList();
            var suman = cols.Where(c => c.Totaliza && c.EsNumero).ToList();
            var lh = Linea(_letra);
            var lhb = Linea(_letra, true);
            var fila = lhb + 2 * Relleno;

            var suma = cols.Sum(c => Math.Max(c.Ancho, 1));
            var anchos = cols.Select(c => _w * Math.Max(c.Ancho, 1) / Math.Max(suma, 1)).ToList();
            var xs = new List<double>();
            double acum = 0;
            foreach (var a in anchos) { xs.Add(acum); acum += a; }

            // Encabezado de página: el de siempre y la fila de títulos (un solo fondo detrás; tan alta como el título
            // que más líneas ocupa).
            var ph = Banda("PH", 0);
            var y = Encabezado(ph) + 1;
            var lineas = cols.Select((c, i) => Lineas(c.Etiqueta, _letra, true, anchos[i] - 2 * Relleno)).DefaultIfEmpty(1).Max();
            var altoTitulos = lineas * lhb + 2 * Relleno;
            var fondoTitulos = Objeto(ph, "CUA", 0, y, _w, altoTitulos);
            fondoTitulos.DES_COLOR_FONDO = Tinta;
            fondoTitulos.NUM_GROSOR_BORDE = 0;
            for (var i = 0; i < cols.Count; i++)
                Texto(ph, "TXT", cols[i].Etiqueta, xs[i] + Relleno, y + Relleno, anchos[i] - 2 * Relleno, _letra, negrita: true,
                      alineacion: Alin(cols[i].Alineacion), color: "#FFFFFF", lineas: lineas);
            ph.NUM_ALTO = R(y + altoTitulos + 0.6);

            // Grupos: cabecera con su valor y, si suma, pie con el subtotal.
            var primera = cols.FindIndex(c => c.Totaliza && c.EsNumero);
            var anchoTexto = primera > 0 ? xs[primera] : _w;
            for (var n = 1; n <= grupos.Count; n++)
            {
                var g = grupos[n - 1];
                L.Grupos.Add(new ReporteLienzoGrupo
                {
                    NUM_NIVEL = n, DES_COLUMNA = g.Nombre, COD_ORDEN = g.OrdenDir == "D" ? "D" : "A",
                    FLG_NUEVA_PAGINA = n == 1 && D.FLG_SALTO_GRUPO, FLG_REPETIR_ENCABEZADO = true
                });
                var gh = Banda("GH", fila, n, fondo: n == 1 ? Grupo1 : Grupo2);
                var sangria = (n - 1) * 2.8;
                Texto(gh, "TXT", $"{g.Etiqueta}: {{{g.Nombre}}}", sangria + Relleno, Relleno, _w - sangria - 2 * Relleno, _letra, negrita: true, color: Tinta, crecer: true);
                if (D.FLG_SUBTOTAL_GRUPO && suman.Count > 0)
                {
                    var gf = Banda("GF", fila + 0.4, n);
                    Raya(gf, 0, 0, _w, Gris, 0.5);
                    if (primera > 0)
                        Texto(gf, "TXT", $"Total {{{g.Nombre}}}", Relleno, Relleno, anchoTexto - 2 * Relleno, _letra, negrita: true, alineacion: "D");
                    Resumenes(gf, cols, xs, anchos, n, null);
                }
            }

            // Detalle: un campo por columna y una raya debajo (los textos largos crecen y la empujan).
            var de = Banda("DE", lh + 2 * Relleno);
            for (var i = 0; i < cols.Count; i++)
            {
                var c = cols[i];
                var o = Objeto(de, "CAM", xs[i] + Relleno, Relleno, anchos[i] - 2 * Relleno, lh);
                o.NUM_RESULTADO = r1;
                o.DES_ORIGEN = c.Nombre;
                o.COD_ALINEACION = Alin(c.Alineacion);
                o.FLG_PUEDE_CRECER = !c.EsNumero && !c.EsFecha;
            }
            Raya(de, 0, lh + 2 * Relleno, _w, ColorLinea, 0.25);

            // Pie del informe: total general, cuántos registros, el texto del pie y las firmas.
            var rf = Banda("RF", 0);
            y = 0;
            if (D.FLG_TOTAL_GENERAL && suman.Count > 0)
            {
                var fondo = Objeto(rf, "CUA", 0, 0, _w, fila + 0.4);
                fondo.DES_COLOR_FONDO = Grupo1;
                fondo.NUM_GROSOR_BORDE = 0;
                Raya(rf, 0, 0, _w, Tinta, 1.0);
                if (primera > 0)
                    Texto(rf, "TXT", "Total general", Relleno, Relleno, anchoTexto - 2 * Relleno, _letra, negrita: true, alineacion: "D", color: Tinta);
                Resumenes(rf, cols, xs, anchos, 0, Tinta);
                y = fila + 0.4;
            }
            if (cols.Count > 0)
            {
                y += 1.4;
                var cuenta = Texto(rf, "RES", cols[0].Nombre, 0, y, 60, 7, color: Gris);
                cuenta.COD_FUNCION = "CNT";
                cuenta.NUM_NIVEL = 0;
                cuenta.DES_FORMATO = "#,##0 \"registros.\"";
                y += Linea(7);
            }
            rf.NUM_ALTO = R(Final(rf, y) + 0.5);

            var pf = Banda("PF", 0);
            pf.NUM_ALTO = R(Pie(pf, filtros: true));
        }

        private void Resumenes(ReporteLienzoBanda b, List<ReporteColumnaVista> cols, List<double> xs, List<double> anchos, int nivel, string? color)
        {
            for (var i = 0; i < cols.Count; i++)
            {
                var c = cols[i];
                if (!c.Totaliza || !c.EsNumero) continue;
                var o = Texto(b, "RES", c.Nombre, xs[i] + Relleno, Relleno, anchos[i] - 2 * Relleno, _letra, negrita: true, alineacion: Alin(c.Alineacion), color: color);
                o.COD_FUNCION = "SUM";
                o.NUM_NIVEL = nivel;
                o.DES_FORMATO = c.Formato;
            }
        }

        // ── Ficha ──────────────────────────────────────────────────────────────
        public void Ficha()
        {
            var ph = Banda("PH", 0);
            ph.NUM_ALTO = R(Encabezado(ph) + 1);

            // Las secciones como el PDF de siempre: sin secciones en el diseño, la cabecera como campos y los demás
            // resultados como tablas.
            var secciones = _def.Secciones.OrderBy(s => s.NUM_ORDEN).ThenBy(s => s.NUM_RESULTADO).ToList();
            if (secciones.Count == 0)
                secciones = _def.Resultados.OrderBy(r => r.NUM_RESULTADO).Select((r, i) => new ReporteDisenoSeccion
                {
                    NUM_RESULTADO = r.NUM_RESULTADO, NUM_ORDEN = i + 1,
                    COD_PRESENTACION = r.COD_PAPEL == "CAB" ? "CAM" : "TAB",
                    DES_TITULO = r.COD_PAPEL == "CAB" ? null : r.DES_RESULTADO
                }).ToList();

            var lh = Linea(_letra);
            var lhb = Linea(_letra, true);
            var sub = 0;
            ReporteLienzoBanda? de = null;
            double y = 0;
            foreach (var s in secciones.Where(s => s.COD_PRESENTACION != "OCU"))
            {
                var cols = ReporteColumnas.De(_ctx, s.NUM_RESULTADO, incluirOcultas: false);
                if (cols.Count == 0) continue;
                // Una sección por subsección del detalle (hasta 9; las que sobran siguen en la última).
                if (sub < 9) { if (de is not null) de.NUM_ALTO = R(y); de = Banda("DE", 0, sub: ++sub); y = 0; }
                if (!string.IsNullOrWhiteSpace(s.DES_TITULO))
                {
                    y += 2.8;
                    Texto(de!, "TXT", s.DES_TITULO!, 0, y, _w, _letra, negrita: true, color: Tinta, crecer: true);
                    y += lhb + 1;
                }
                switch (s.COD_PRESENTACION)
                {
                    case "CAM":
                    {
                        var n = Math.Clamp(s.NUM_COLUMNAS ?? 3, 1, 4);
                        var cw = _w / n;
                        var usado = n;
                        var filaY = y;
                        var alto = lh + 1.0;
                        foreach (var c in cols)
                        {
                            var span = Math.Clamp((int)Math.Round(c.Ancho), 1, n);
                            if (usado + span > n) { filaY = y; y += alto; usado = 0; }
                            var x = usado * cw;
                            var rotulo = c.Etiqueta + ":";
                            var wr = Math.Min(Medir(rotulo + " ", _letra, true) + 0.5, span * cw * 0.45);
                            Texto(de!, "TXT", rotulo, x, filaY + 0.5, wr, _letra, negrita: true);
                            var campo = Objeto(de!, "CAM", x + wr, filaY + 0.5, span * cw - wr - 1, lh);
                            campo.NUM_RESULTADO = s.NUM_RESULTADO;
                            campo.DES_ORIGEN = c.Nombre;
                            campo.FLG_PUEDE_CRECER = true;
                            usado += span;
                        }
                        y += 1;
                        break;
                    }
                    case "TOT":
                    {
                        var ancho = _w * 0.42;
                        var t = Objeto(de!, "TAB", _w - ancho, y, ancho, 2 * (lh + 2 * Relleno));
                        t.NUM_RESULTADO = s.NUM_RESULTADO;
                        t.DES_ORIGEN = "SIN_TITULOS ULTIMA_NEGRITA";
                        y += 2 * (lh + 2 * Relleno) + 2;
                        break;
                    }
                    default:
                    {
                        var t = Objeto(de!, "TAB", 0, y, _w, 2 * (lh + 2 * Relleno));
                        t.NUM_RESULTADO = s.NUM_RESULTADO;
                        y += 2 * (lh + 2 * Relleno) + 2;
                        break;
                    }
                }
            }
            if (de is null) de = Banda("DE", 6);
            else de.NUM_ALTO = R(y);

            var rf = Banda("RF", 0);
            rf.NUM_ALTO = R(Final(rf, 0) + 0.5);
            if (rf.NUM_ALTO <= 0.5m && !L.Objetos.Any(o => o.NUM_BANDA == rf.NUM_BANDA)) { L.Bandas.Remove(rf); }

            var pf = Banda("PF", 0);
            pf.NUM_ALTO = R(Pie(pf, filtros: false));
        }
    }
}
