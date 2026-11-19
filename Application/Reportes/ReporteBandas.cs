using System.Globalization;
using System.Text.RegularExpressions;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using KonSolutionsApi.Domain.Models.Reportes;

namespace KonSolutionsApi.Application.Reportes;

// Diseño libre (COD_TIPO_DISENO BAN): el lienzo por bandas, como Crystal Reports, dibujado con PDFsharp en coordenadas.
//
// Cómo se arma:
//   1. Las filas del resultado 1 se ordenan por los grupos y se recorren: cada cambio de grupo cierra los pies (GF) y abre
//      los encabezados (GH) que correspondan; cada fila imprime el detalle (DE). Antes, el encabezado del informe (RH);
//      al final, el pie del informe (RF).
//   2. Cada banda impresa se mide (los textos que pueden crecer la estiran) y se parte en bloques: una banda sin tablas es
//      un solo bloque; una con «Tabla de un resultado» se parte en lo de arriba, el título de la tabla, una fila por bloque,
//      el total y lo de abajo, para que la tabla pueda seguir en la página siguiente.
//   3. Los bloques se reparten en páginas: encabezado de página arriba, pie de página abajo, los grupos que siguen en otra
//      página repiten su encabezado, «mantener junto» pasa una banda o un grupo entero a la página siguiente.
//   4. Se dibuja. «Página N de M» se resuelve aquí, cuando ya se sabe cuántas páginas hay. Cada grupo queda como marcador
//      del PDF: el visor lo muestra como árbol para saltar.
// Líneas y cuadros que cruzan secciones (NUM_BANDA_FIN): no son parte de su sección; se dibujan aparte, de la sección
// donde empiezan a la que terminan (del encabezado al pie del mismo grupo, del informe o de la página). Si lo que encierran
// sigue en otra página, se dibujan en cada una, cerrados.
// Fórmulas (ReporteFormulasDatos): los campos de fórmula (FOR) y las referencias {F.X}, {R2.COL}, {Σ.Suma.COL}, {#Pagina}
// de los textos se calculan con la fila y los grupos de la banda. Las condiciones de una sección o de un objeto se
// evalúan donde deciden: no imprimir y negrita al medir; página nueva al repartir; color y fondo al dibujar (ya con la
// página).
// Fase 3: filas cebra (el fondo de las filas pares del detalle, que vuelve a empezar en cada grupo), texto girado (90, 180
// o 270 grados), líneas y cuadros que se estiran hasta el pie de su sección cuando un texto la hace crecer, y códigos de
// barras y QR (objeto BAR, ReporteCodigos). Subinformes, tablas cruzadas y tablas enlazadas: ver ReporteBandasSub.cs. Gráficos
// (objeto GRA): ver ReporteBandasGrafico.cs.
// Todas las medidas del lienzo están en milímetros; PDFsharp dibuja en puntos.

public static class ReporteBandasPdf
{
    public static byte[] Generar(ReporteContexto ctx)
    {
        ReporteFuentesPdf.Asegurar();
        var lienzo = ctx.Definicion.Lienzo ?? throw new InvalidOperationException("El diseño libre no tiene lienzo.");
        return new ReporteBandasMotor(ctx, lienzo).Pdf();
    }

    /// <summary>Qué subinformes hay que ejecutar y con qué valores: recorre el reporte como si lo armara, sin dibujar. El
    /// servicio los ejecuta (el motor no toca la base) y los deja en ctx.Subinformes antes de generar.</summary>
    public static List<ReporteSubPedido> PedidosSubinformes(ReporteContexto ctx)
    {
        ReporteFuentesPdf.Asegurar();
        var lienzo = ctx.Definicion.Lienzo;
        if (lienzo is null || !lienzo.Objetos.Any(o => o.COD_TIPO_OBJETO == "SUB")) return new();
        return new ReporteBandasMotor(ctx, lienzo).Pedidos();
    }
}

/// <summary>Un grupo ya armado: el valor que lo identifica y sus filas [Desde, Hasta) entre las ordenadas.</summary>
internal sealed class ReporteBandasGrupo
{
    public int    Nivel { get; init; }
    public int    Desde { get; init; }
    public int    Hasta { get; init; }
    public string Clave { get; init; } = "";
    public bool   EsPrimero { get; init; }
}

/// <summary>Una banda que se imprime: con qué fila y con qué grupos abiertos.</summary>
internal sealed class ReporteBandasInstancia
{
    public ReporteLienzoBanda    Banda  { get; init; } = new();
    public int                   Fila   { get; init; } = -1;
    public ReporteBandasGrupo?[] Grupos { get; init; } = Array.Empty<ReporteBandasGrupo?>();
    /// <summary>En el detalle: cuántas filas se imprimieron antes en el mismo grupo (para las filas cebra); -1 en las demás.</summary>
    public int                   Renglon { get; init; } = -1;
}

internal enum ReporteBloqueTipo { Estatico, TituloTabla, FilaTabla, TotalTabla, Aviso }

/// <summary>Lo que se reparte en las páginas: un pedazo de banda que no se parte.</summary>
internal sealed class ReporteBandasBloque
{
    public ReporteBandasInstancia     Inst       { get; init; } = new();
    public ReporteBloqueTipo          Tipo       { get; init; }
    public double                     Alto       { get; set; }
    public bool                       Primero    { get; init; }
    /// <summary>Estático: los objetos del pedazo y desde qué Y de la banda empieza.</summary>
    public List<ReporteLienzoObjeto>  Objetos    { get; init; } = new();
    public double                     Desde      { get; init; }
    /// <summary>Dónde va cada objeto dentro del bloque (ya bajado por lo que creció arriba de él).</summary>
    public Dictionary<int, double>    PosY       { get; init; } = new();
    /// <summary>Lo que dice cada objeto (texto ya formateado) y cuánto mide de alto.</summary>
    public Dictionary<int, ReporteBandasTexto> Textos { get; init; } = new();
    /// <summary>El alto de las líneas y los cuadros que llegan al pie de la sección cuando esta creció (mm).</summary>
    public Dictionary<int, double>    Altos      { get; init; } = new();
    /// <summary>Los gráficos del pedazo, ya calculados con la fila y los grupos de la banda.</summary>
    public Dictionary<int, ReporteGrafico> Graficos { get; init; } = new();
    /// <summary>Tabla: la tabla y la fila (o -1 el título / el total).</summary>
    public ReporteBandasTabla?        Tabla      { get; init; }
    public int                        FilaTabla  { get; init; } = -1;
    /// <summary>Un bloque de un subinforme: el motor del subinforme (que lo midió y lo dibuja) y la banda del reporte
    /// principal donde va (la que manda al repartir en páginas). Null en los bloques del principal.</summary>
    public ReporteBandasMotor?        Motor      { get; set; }
    public ReporteBandasInstancia?    Anfitrion  { get; set; }
    /// <summary>Aviso: lo que se escribe en rojo en lugar de un subinforme que no se pudo armar, y su objeto.</summary>
    public string?                    Aviso      { get; init; }
    public ReporteLienzoObjeto?       Objeto     { get; init; }
}

internal sealed class ReporteBandasTexto
{
    public string       Texto  { get; init; } = "";
    public List<string> Lineas { get; init; } = new();
    public double       Alto   { get; init; }
    /// <summary>Depende de la página (N, M): se escribe al dibujar.</summary>
    public bool         DePagina { get; init; }
    /// <summary>La negrita que dio su condición (null: la del objeto). Se decide al medir y se respeta al dibujar.</summary>
    public bool?        Negrita  { get; init; }
}

/// <summary>Una «Tabla de un resultado» ya medida.</summary>
internal sealed class ReporteBandasTabla
{
    public ReporteLienzoObjeto              Objeto    { get; init; } = new();
    public List<ReporteColumnaVista>        Columnas  { get; init; } = new();
    public List<double>                     Anchos    { get; init; } = new();
    public List<List<string>>               Celdas    { get; init; } = new();
    public Dictionary<string, decimal>      Totales   { get; init; } = new();
    public bool                             ConTitulo { get; init; }
    public bool                             UltimaResaltada { get; init; }
    public ReporteBandasBloque?             Titulo    { get; set; }
    public XFont                            Fuente    { get; init; } = null!;
    public XFont                            Negrita   { get; init; } = null!;
    /// <summary>Las filas del resultado que se muestran (una tabla enlazada solo trae las de la fila que se imprime); null:
    /// todas las del resultado.</summary>
    public List<object?[]>?                 Filas     { get; init; }
    /// <summary>Tabla cruzada: la fila de totales ya escrita, los valores sin formato de cada celda y de los totales (para el
    /// Excel) y el formato de Excel de cada columna.</summary>
    public List<string>?                    FilaTotal { get; init; }
    public List<object?[]>?                 Crudos    { get; init; }
    public object?[]?                       CrudosTotal { get; init; }
    public string?[]?                       FormatosExcel { get; init; }
}

/// <summary>Una línea o un cuadro que cruza secciones, armado: de qué bloque a qué bloque (índices entre los bloques).</summary>
internal sealed class ReporteBandasTramo
{
    public ReporteLienzoObjeto     Objeto { get; init; } = null!;
    public ReporteBandasInstancia  Inicio { get; init; } = null!;
    public ReporteBandasInstancia? Fin    { get; set; }
    public int                     Desde  { get; init; }
    public int                     Hasta  { get; set; }
}

internal sealed class ReporteBandasPagina
{
    public List<(ReporteBandasBloque Bloque, double Y)> Bloques { get; } = new();
    public int Numero { get; init; }
}

internal sealed partial class ReporteBandasMotor
{
    private const double MmPt = 72.0 / 25.4;
    private const double Relleno = 0.6;          // mm, dentro de los objetos con fondo o borde
    private static readonly Regex Marca = new(@"\{([^{}\r\n]+)\}", RegexOptions.Compiled);
    private static readonly Regex MarcaSimple = new(@"^@?[A-Za-z0-9_]+$", RegexOptions.Compiled);
    private static readonly XColor Tinta = XColor.FromArgb(0x16, 0x3a, 0x5f);
    private static readonly XColor LineaTabla = XColor.FromArgb(0xcf, 0xd8, 0xdc);
    private static readonly XColor Resalte = XColor.FromArgb(0xe8, 0xea, 0xf6);

    private readonly ReporteContexto _ctx;
    private readonly ReporteLienzo _l;
    private readonly ReporteTabla? _tabla;
    private readonly List<object?[]> _filas;
    private readonly List<ReporteLienzoGrupo> _grupos;
    private readonly double _anchoPagina, _altoPagina, _margen, _anchoUtil;
    private readonly XGraphics _medir;
    private readonly Dictionary<string, XFont> _fuentes;
    private readonly Dictionary<int, string> _ultimoTexto = new();
    private readonly Dictionary<ReporteBandasGrupo, ReporteBandasInstancia> _encabezados = new();
    private readonly List<(int Indice, ReporteBandasGrupo Grupo)> _iniciosGrupo = new();
    private readonly Dictionary<ReporteBandasGrupo, (int Desde, int Hasta)> _rangosGrupo = new();
    private XImage? _logo, _logoAplicacion;
    private bool _logoLeido, _logoAplicacionLeido;
    private readonly Dictionary<int, XImage?> _imagenes = new();
    private readonly Dictionary<(int, string), ReporteColumnaVista?> _vistas = new();
    private readonly ReporteFormulasDatos _f;
    // Dónde empieza a dibujar (mm desde el borde izquierdo de la hoja), cuánto se corre en el Excel y qué ancho tiene la
    // caja: el reporte, desde su margen y a todo el ancho; un subinforme, desde su objeto y del ancho de su objeto.
    private readonly double _x0, _dx, _anchoCaja;
    private readonly bool _esSub;
    // Al juntar los pedidos de los subinformes (antes de ejecutarlos), en vez de armarlos.
    private List<ReporteSubPedido>? _pedidos;
    // Los gráficos que van en el Excel (se agregan al libro al final); los de un subinforme van en la misma lista.
    private readonly List<GraficoXls> _graficosXls;

    public ReporteBandasMotor(ReporteContexto ctx, ReporteLienzo lienzo, ReporteBandasMotor? anfitrion = null, ReporteLienzoObjeto? sub = null)
    {
        _ctx = ctx;
        _l = lienzo;
        _tabla = ctx.Datos.Tabla(1) ?? ctx.Datos.Tablas.FirstOrDefault();
        var (w, h) = ReportePapel.Tamano(ctx.D.COD_PAPEL, ctx.D.COD_ORIENTACION);
        _anchoPagina = w * 10;
        _altoPagina = h * 10;
        _margen = ReportePapel.Margen * 10;
        _anchoUtil = _anchoPagina - 2 * _margen;
        _grupos = lienzo.Grupos.OrderBy(g => g.NUM_NIVEL)
                               .Where(g => _tabla is not null && _tabla.Indice(g.DES_COLUMNA) >= 0).ToList();
        _filas = ReporteListado.Ordenar(_tabla?.Filas ?? new List<object?[]>(), _tabla, ReporteBandasListado.Claves(ctx.Definicion, _tabla));
        _f = new ReporteFormulasDatos(ctx, _tabla, _filas, lienzo);
        // Un subinforme mide con lo de su reporte: puede haber uno por fila.
        _medir = anfitrion?._medir ?? XGraphics.CreateMeasureContext(new XSize(_anchoPagina * MmPt, _altoPagina * MmPt), XGraphicsUnit.Point, XPageDirection.Downwards);
        _fuentes = anfitrion?._fuentes ?? new Dictionary<string, XFont>();
        _graficosXls = anfitrion?._graficosXls ?? new List<GraficoXls>();
        if (anfitrion is not null && sub is not null)
        {
            _esSub = true;
            _x0 = anfitrion._x0 + (double)sub.NUM_X;
            _dx = anfitrion._dx + (double)sub.NUM_X;
            _anchoCaja = Math.Max(1, (double)sub.NUM_ANCHO);
        }
        else
        {
            _x0 = _margen;
            _anchoCaja = _anchoUtil;
        }
    }

    /// <summary>La banda del reporte principal de un bloque (la suya, o la que tiene el subinforme del que es parte).</summary>
    private static ReporteBandasInstancia Raiz(ReporteBandasBloque b) => b.Anfitrion ?? b.Inst;

    /// <summary>El motor que midió el bloque y lo sabe dibujar.</summary>
    private ReporteBandasMotor Dueno(ReporteBandasBloque b) => b.Motor ?? this;

    // ── 1. Qué bandas se imprimen y en qué orden ───────────────────────────────
    /// <summary>Las bandas de un tipo. Una suprimida queda fuera, salvo que una condición decida si se imprime.</summary>
    private IEnumerable<ReporteLienzoBanda> Bandas(string tipo, int nivel = 0)
        => _l.Bandas.Where(b => b.COD_TIPO_BANDA == tipo && b.NUM_NIVEL == nivel
                                && (!b.FLG_SUPRIMIR || _f.TieneCondicion(ReporteFormulasDatos.Seccion, b.NUM_BANDA, "SUPRIMIR")))
                    .OrderBy(b => b.NUM_SUBSECCION);

    /// <summary>Dónde se evalúan las fórmulas de una banda: su fila, sus grupos y la página.</summary>
    private ReporteFormulaLugar Lugar(ReporteBandasInstancia inst, int pagina = 0, int paginas = 0) => new()
    {
        Fila = inst.Fila,
        Grupos = inst.Grupos.Select(g => g is null ? (0, _filas.Count) : (g.Desde, g.Hasta)).ToArray(),
        NivelSeccion = inst.Banda.COD_TIPO_BANDA is "GH" or "GF" ? inst.Banda.NUM_NIVEL : 0,
        Pagina = pagina,
        Paginas = paginas
    };

    /// <summary>Una propiedad de sí o no de la sección: la condición si la hay y dio algo; si no, el valor fijo.</summary>
    private bool DeSeccion(ReporteBandasInstancia inst, string propiedad, bool fijo)
        => _f.Logico(ReporteFormulasDatos.Seccion, inst.Banda.NUM_BANDA, propiedad, Lugar(inst)) ?? fijo;

    /// <summary>La columna (formato y tipo) de un resultado, leída una sola vez: se pide por cada fila y cada objeto.</summary>
    private ReporteColumnaVista? Vista(int resultado, string columna)
    {
        var clave = (resultado, columna.ToUpperInvariant());
        if (!_vistas.TryGetValue(clave, out var v)) _vistas[clave] = v = ReporteColumnas.Buscar(_ctx, resultado, columna);
        return v;
    }

    private string ClaveGrupo(object?[] fila, ReporteLienzoGrupo g)
    {
        var col = Vista(_tabla!.Numero, g.DES_COLUMNA);
        return ReporteFormato.Texto(_tabla.Valor(fila, g.DES_COLUMNA), col?.Formato, col?.Tipo ?? typeof(string), c => _tabla.Valor(fila, c));
    }

    /// <summary>Todas las bandas que se imprimen, en orden, ya partidas en bloques.</summary>
    private List<ReporteBandasBloque> Armar()
    {
        var bloques = new List<ReporteBandasBloque>();
        var sinGrupos = Array.Empty<ReporteBandasGrupo?>();

        // Devuelve cuántos bloques salieron (un detalle suprimido no cuenta como fila para las filas cebra).
        int Emitir(string tipo, int nivel, int fila, ReporteBandasGrupo?[] abiertos, int renglon = -1)
        {
            var antes = bloques.Count;
            foreach (var b in Bandas(tipo, nivel))
            {
                var inst = new ReporteBandasInstancia { Banda = b, Fila = fila, Grupos = abiertos, Renglon = renglon };
                if (tipo == "GH" && nivel > 0 && abiertos[nivel - 1] is { } g && !_encabezados.ContainsKey(g)) _encabezados[g] = inst;
                bloques.AddRange(Partir(inst));
            }
            return bloques.Count - antes;
        }

        // Antes de emitir se sabe dónde empieza y termina cada grupo: los resúmenes se calculan al medir la banda, y
        // pueden ir en el pie del grupo o también en su cabecera (el total del grupo arriba, como en Crystal).
        var claves = _filas.Select(f => _grupos.Select(g => ClaveGrupo(f, g)).ToArray()).ToList();
        var cambios = new int[_filas.Count];
        for (var i = 0; i < _filas.Count; i++)
        {
            cambios[i] = i == 0 ? 0 : -1;
            for (var n = 0; i > 0 && n < _grupos.Count && cambios[i] < 0; n++)
                if (!string.Equals(claves[i][n], claves[i - 1][n], StringComparison.Ordinal)) cambios[i] = n;
        }
        int Fin(int i, int n)
        {
            var j = i + 1;
            while (j < _filas.Count && (cambios[j] < 0 || cambios[j] > n)) j++;
            return j;
        }

        Emitir("RH", 0, _filas.Count > 0 ? 0 : -1, sinGrupos);
        // Un subinforme no tiene páginas propias: su encabezado de página va debajo de su encabezado del informe (y se repite
        // arriba cuando sigue en otra página: ver Continuacion).
        if (_esSub)
            foreach (var b in Bandas("PH"))
                bloques.AddRange(Partir(new ReporteBandasInstancia { Banda = b, Fila = _filas.Count > 0 ? 0 : -1, Grupos = sinGrupos }, true));
        var abiertos = new ReporteBandasGrupo?[_grupos.Count];
        var renglon = 0;
        for (var i = 0; i < _filas.Count; i++)
        {
            var cambio = cambios[i];
            if (cambio >= 0)
            {
                renglon = 0;
                // Cierra los grupos de adentro hacia afuera, con la fila anterior.
                if (i > 0)
                    for (var n = _grupos.Count - 1; n >= cambio; n--)
                    {
                        Emitir("GF", n + 1, i - 1, (ReporteBandasGrupo?[])abiertos.Clone());
                        Cerrar(abiertos[n]!, bloques.Count);
                    }
                for (var n = cambio; n < _grupos.Count; n++)
                {
                    var g = new ReporteBandasGrupo { Nivel = n + 1, Desde = i, Hasta = Fin(i, n), Clave = claves[i][n], EsPrimero = abiertos[n] is null };
                    abiertos[n] = g;
                    _iniciosGrupo.Add((bloques.Count, g));
                    _rangosGrupo[g] = (bloques.Count, -1);
                    Emitir("GH", n + 1, i, (ReporteBandasGrupo?[])abiertos.Clone());
                }
            }
            if (Emitir("DE", 0, i, (ReporteBandasGrupo?[])abiertos.Clone(), renglon) > 0) renglon++;
        }
        if (_filas.Count > 0)
            for (var n = _grupos.Count - 1; n >= 0; n--)
            {
                Emitir("GF", n + 1, _filas.Count - 1, (ReporteBandasGrupo?[])abiertos.Clone());
                Cerrar(abiertos[n]!, bloques.Count);
            }
        Emitir("RF", 0, _filas.Count - 1, sinGrupos);
        return bloques;
    }

    private void Cerrar(ReporteBandasGrupo g, int hastaBloque)
    {
        if (_rangosGrupo.TryGetValue(g, out var r)) _rangosGrupo[g] = (r.Desde, hastaBloque);
    }

    // ── 2. Medir y partir una banda en bloques ─────────────────────────────────
    private List<ReporteBandasBloque> Partir(ReporteBandasInstancia inst, bool repetida = false)
    {
        var lista = new List<ReporteBandasBloque>();
        if (DeSeccion(inst, "SUPRIMIR", inst.Banda.FLG_SUPRIMIR)) return lista;
        var lugar = Lugar(inst);
        var objetos = _l.Objetos.Where(o => o.NUM_BANDA == inst.Banda.NUM_BANDA && !Cruza(o)
                                            && _f.Logico(ReporteFormulasDatos.Objeto, o.NUM_OBJETO, "SUPRIMIR", lugar) != true)
                                .OrderBy(o => o.NUM_ORDEN).ThenBy(o => o.NUM_OBJETO).ToList();
        var textos = new Dictionary<int, ReporteBandasTexto>();
        foreach (var o in objetos.Where(o => o.COD_TIPO_OBJETO is "CAM" or "TXT" or "RES" or "ESP" or "FOR" or "BAR"))
            textos[o.NUM_OBJETO] = o.COD_TIPO_OBJETO == "BAR" ? MedirCodigo(o, inst) : MedirTexto(o, inst, repetida);
        var graficos = new Dictionary<int, ReporteGrafico>();
        foreach (var o in objetos.Where(o => o.COD_TIPO_OBJETO == "GRA"))
            graficos[o.NUM_OBJETO] = DatosGrafico(o, inst);

        // Suprimir si está en blanco: ningún campo, resumen, especial, fórmula ni gráfico tiene algo que mostrar.
        if (inst.Banda.FLG_SUPRIMIR_EN_BLANCO)
        {
            var conValor = objetos.Where(o => o.COD_TIPO_OBJETO is "CAM" or "RES" or "ESP" or "FOR" or "GRA").ToList();
            if (conValor.Count > 0 && conValor.All(o => o.COD_TIPO_OBJETO == "GRA" ? graficos[o.NUM_OBJETO].Vacio
                                                                                   : string.IsNullOrWhiteSpace(textos[o.NUM_OBJETO].Texto))) return lista;
        }
        // Un gráfico sin datos que dice «No imprimir si está vacío» no se dibuja.
        objetos.RemoveAll(o => o.COD_TIPO_OBJETO == "GRA" && o.FLG_SUPRIMIR_VACIO && graficos[o.NUM_OBJETO].Vacio);

        // Lo que ocupa filas (y puede seguir en otra página) parte la banda: tablas, tablas cruzadas y subinformes.
        var tablas = repetida ? new List<ReporteLienzoObjeto>() : objetos.Where(EsTabular).OrderBy(o => o.NUM_Y).ToList();
        var resto = objetos.Where(o => !EsTabular(o)).ToList();
        var alto = (double)inst.Banda.NUM_ALTO;
        double cursor = 0;
        var primero = true;

        void Estatico(double desde, double hasta, IEnumerable<ReporteLienzoObjeto> objs, bool forzar)
        {
            var lista2 = objs.ToList();
            var h = Math.Max(0, hasta - desde);
            // Lo que crece empuja hacia abajo lo que está debajo (como en Crystal): cada objeto baja lo que crecieron los
            // que terminan arriba de él, sumando en cadena (A empuja a B, que crece y empuja a C).
            var posY = new Dictionary<int, double>();
            var baja = new Dictionary<int, double>();
            foreach (var o in lista2.OrderBy(o => o.NUM_Y).ThenBy(o => o.NUM_X))
            {
                double desplazado = 0;
                foreach (var g in lista2)
                {
                    if (g == o || !baja.TryGetValue(g.NUM_OBJETO, out var bg)) continue;
                    if ((double)(g.NUM_Y + g.NUM_ALTO) <= (double)o.NUM_Y + 0.01) desplazado = Math.Max(desplazado, bg);
                }
                var crece = textos.TryGetValue(o.NUM_OBJETO, out var t0) && o.FLG_PUEDE_CRECER ? Math.Max(0, t0.Alto - (double)o.NUM_ALTO) : 0;
                baja[o.NUM_OBJETO] = desplazado + crece;
                posY[o.NUM_OBJETO] = Math.Max(0, (double)o.NUM_Y - desde) + desplazado;
            }
            foreach (var o in lista2)
            {
                var y = posY[o.NUM_OBJETO];
                var oh = textos.TryGetValue(o.NUM_OBJETO, out var t) ? t.Alto : (double)o.NUM_ALTO;
                if (o.COD_TIPO_OBJETO is "LIN" or "CUA" or "IMG" || t is null || !string.IsNullOrEmpty(t.Texto) || t.DePagina)
                    h = Math.Max(h, y + oh);
            }
            if (h <= 0.001 && lista2.Count == 0 && !forzar) return;
            // Hasta el pie de la sección: una línea vertical o un cuadro queda a la misma distancia del borde de abajo, aunque
            // la sección haya crecido.
            var altos = new Dictionary<int, double>();
            foreach (var o in lista2.Where(o => o.FLG_HASTA_ABAJO && (o.COD_TIPO_OBJETO == "CUA" || (o.COD_TIPO_OBJETO == "LIN" && o.NUM_ANCHO == 0))))
            {
                var hueco = Math.Max(0, (hasta - desde) - ((double)o.NUM_Y - desde + (double)o.NUM_ALTO));
                var nuevo = h - hueco - posY[o.NUM_OBJETO];
                if (nuevo > (double)o.NUM_ALTO + 0.01) altos[o.NUM_OBJETO] = nuevo;
            }
            lista.Add(new ReporteBandasBloque
            {
                Inst = inst, Tipo = ReporteBloqueTipo.Estatico, Alto = h, Primero = primero, Objetos = lista2, Desde = desde, Textos = textos,
                PosY = posY, Altos = altos, Graficos = graficos
            });
            primero = false;
        }

        if (tablas.Count == 0)
        {
            Estatico(0, alto, resto, forzar: true);
            return lista;
        }
        for (var k = 0; k < tablas.Count; k++)
        {
            var tab = tablas[k];
            var ty = (double)tab.NUM_Y;
            var tfin = ty + (double)tab.NUM_ALTO;
            var siguiente = k + 1 < tablas.Count ? (double)tablas[k + 1].NUM_Y : double.MaxValue;
            Estatico(cursor, ty, resto.Where(o => (double)o.NUM_Y >= cursor && (double)o.NUM_Y < Math.Min(tfin, siguiente)
                                                && ((double)o.NUM_Y < ty || (double)o.NUM_Y < tfin)), forzar: false);
            if (tab.COD_TIPO_OBJETO == "CRU") lista.AddRange(BloquesCruzada(tab, inst, ref primero));
            else if (tab.COD_TIPO_OBJETO == "SUB") lista.AddRange(BloquesSub(tab, inst, ref primero));
            else lista.AddRange(BloquesTabla(tab, inst, ref primero));
            cursor = tfin;
        }
        Estatico(cursor, Math.Max(alto, cursor), resto.Where(o => (double)o.NUM_Y >= cursor), forzar: false);
        return lista;
    }

    private IEnumerable<ReporteBandasBloque> BloquesTabla(ReporteLienzoObjeto tab, ReporteBandasInstancia inst, ref bool primero)
    {
        var r = tab.NUM_RESULTADO ?? 1;
        var datos = _ctx.Datos.Tabla(r);
        var columnas = ReporteColumnas.De(_ctx, r, incluirOcultas: false);
        var res = new List<ReporteBandasBloque>();
        if (columnas.Count == 0) return res;
        var ancho = Math.Max(10, (double)tab.NUM_ANCHO);
        var suma = columnas.Sum(c => Math.Max(c.Ancho, 1));
        var anchos = columnas.Select(c => ancho * Math.Max(c.Ancho, 1) / suma).ToList();
        var f = Fuente(tab, null);
        var fb = Fuente(tab, true);
        var celdas = new List<List<string>>();
        var totales = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
        // Tabla enlazada: solo las filas que calzan con la que se imprime (el detalle de cada orden en un listado).
        var filas = datos is null ? new List<object?[]>() : Filtrar(datos, datos.Filas, tab.DES_ENLACES, inst);
        if (filas.Count == 0 && tab.FLG_SUPRIMIR_VACIO) return res;
        foreach (var fila in filas)
        {
            celdas.Add(columnas.Select(c => ReporteFormato.Texto(datos!.Valor(fila, c.Nombre), c.Formato, c.Tipo, n => datos.Valor(fila, n))).ToList());
            foreach (var c in columnas.Where(c => c.Totaliza && c.EsNumero))
                if (ReporteFormato.ADecimal(datos!.Valor(fila, c.Nombre), out var v))
                    totales[c.Nombre] = totales.GetValueOrDefault(c.Nombre) + v;
        }
        // Opciones de la tabla, en el origen: SIN_TITULOS (sin la fila de títulos), ULTIMA_NEGRITA (la última fila resaltada,
        // como el total de una ficha), SIN_TOTAL (sin la fila de total, aunque alguna columna sume).
        var opciones = (tab.DES_ORIGEN ?? "").ToUpperInvariant();
        var t = new ReporteBandasTabla
        {
            Objeto = tab, Columnas = columnas, Anchos = anchos, Celdas = celdas, Totales = totales, Fuente = f, Negrita = fb,
            ConTitulo = !opciones.Contains("SIN_TITULOS"),
            UltimaResaltada = opciones.Contains("ULTIMA_NEGRITA"),
            Filas = filas
        };
        if (t.ConTitulo)
        {
            var titulo = new ReporteBandasBloque
            {
                Inst = inst, Tipo = ReporteBloqueTipo.TituloTabla, Primero = primero, Tabla = t,
                Alto = AltoFila(columnas.Select(c => c.Etiqueta).ToList(), anchos, fb)
            };
            t.Titulo = titulo;
            res.Add(titulo);
            primero = false;
        }
        for (var i = 0; i < celdas.Count; i++)
        {
            var negrita = t.UltimaResaltada && i == celdas.Count - 1;
            res.Add(new ReporteBandasBloque
            {
                Inst = inst, Tipo = ReporteBloqueTipo.FilaTabla, Primero = primero, Tabla = t, FilaTabla = i, Alto = AltoFila(celdas[i], anchos, negrita ? fb : f)
            });
            primero = false;
        }
        if (totales.Count > 0 && !opciones.Contains("SIN_TOTAL"))
        {
            res.Add(new ReporteBandasBloque { Inst = inst, Tipo = ReporteBloqueTipo.TotalTabla, Primero = primero, Tabla = t, Alto = AltoFila(new List<string> { "0" }, new List<double> { ancho }, fb) });
            primero = false;
        }
        return res;
    }

    private double AltoFila(List<string> textos, List<double> anchos, XFont f)
    {
        var linea = f.GetHeight() / MmPt;
        var max = 1;
        for (var i = 0; i < textos.Count && i < anchos.Count; i++)
            max = Math.Max(max, Partir(textos[i], f, (anchos[i] - 2 * Relleno) * MmPt).Count);
        return max * linea + 2 * Relleno;
    }

    // ── Valores de los objetos ─────────────────────────────────────────────────
    private static readonly string[] DePaginaEspecial = { "PAGINA", "TOTAL_PAGINAS", "PAGINA_N_DE_M" };

    /// <summary>¿El objeto dice algo que depende de la página (N o M)? Eso se escribe al dibujar, cuando ya se sabe.</summary>
    private bool EsDePagina(ReporteLienzoObjeto o)
    {
        var origen = (o.DES_ORIGEN ?? "").Trim();
        if (o.COD_TIPO_OBJETO == "ESP") return DePaginaEspecial.Contains(origen.ToUpperInvariant());
        if (o.COD_TIPO_OBJETO == "FOR") return _f.FormulaUsaPagina(origen);
        if (o.COD_TIPO_OBJETO is not ("TXT" or "BAR")) return false;
        return Marca.Matches(origen).Any(m =>
        {
            var clave = m.Groups[1].Value.Trim();
            if (ReporteFormulasDatos.EsReferencia(clave)) return _f.MarcaUsaPagina(clave);
            clave = clave.ToUpperInvariant();
            return DePaginaEspecial.Contains(clave) && (_tabla is null || _tabla.Indice(clave) < 0);
        });
    }

    private ReporteBandasTexto MedirTexto(ReporteLienzoObjeto o, ReporteBandasInstancia inst, bool repetida)
    {
        var dePagina = EsDePagina(o);
        // Lo que depende de la página se mide con números grandes, para que al escribirlo no le falte lugar.
        var texto = dePagina ? Valor(o, inst, 999, 999) : Valor(o, inst, 0, 0);
        // Suprimir si se repite: solo en el detalle, comparando con la fila anterior.
        if (!dePagina && !repetida && o.FLG_SUPRIMIR_REPETIDO && inst.Banda.COD_TIPO_BANDA == "DE")
        {
            var igual = _ultimoTexto.TryGetValue(o.NUM_OBJETO, out var antes) && antes == texto;
            _ultimoTexto[o.NUM_OBJETO] = texto;
            if (igual) texto = "";
        }
        if (string.IsNullOrEmpty(texto) && o.FLG_SUPRIMIR_VACIO)
            return new ReporteBandasTexto { Texto = "", Alto = 0 };
        var negrita = _f.Logico(ReporteFormulasDatos.Objeto, o.NUM_OBJETO, "NEGRITA", Lugar(inst));
        var f = Fuente(o, null, negrita);
        var pad = TieneMarco(o) ? Relleno : 0;
        // Girado 90 o 270 grados, el renglón corre a lo alto del objeto y los renglones se apilan a lo ancho (no crece).
        var vertical = Giro(o) is 90 or 270;
        var (largo, apilado) = vertical ? ((double)o.NUM_ALTO, (double)o.NUM_ANCHO) : ((double)o.NUM_ANCHO, (double)o.NUM_ALTO);
        var util = Math.Max(1, largo - 2 * pad) * MmPt;
        var lineas = Partir(texto, f, util);
        var lh = f.GetHeight() / MmPt;
        var alto = (double)o.NUM_ALTO;
        if (o.FLG_PUEDE_CRECER && !vertical) alto = Math.Max(alto, lineas.Count * lh + 2 * pad);
        else
        {
            var caben = Math.Max(1, (int)Math.Floor((apilado - 2 * pad + 0.01) / lh));
            if (lineas.Count > caben) lineas = lineas.Take(caben).ToList();
        }
        return new ReporteBandasTexto { Texto = dePagina ? "" : texto, Lineas = lineas, Alto = alto, DePagina = dePagina, Negrita = negrita };
    }

    /// <summary>El giro del texto: 0, 90, 180 o 270 grados.</summary>
    private static int Giro(ReporteLienzoObjeto o) => o.NUM_GIRO is 90 or 180 or 270 ? o.NUM_GIRO : 0;

    /// <summary>Un código de barras o QR: lo que codifica (como un texto, con sus marcas) y el alto del objeto.</summary>
    private ReporteBandasTexto MedirCodigo(ReporteLienzoObjeto o, ReporteBandasInstancia inst)
    {
        var dePagina = EsDePagina(o);
        var texto = dePagina ? "" : Valor(o, inst, 0, 0);
        if (!dePagina && string.IsNullOrEmpty(texto) && o.FLG_SUPRIMIR_VACIO) return new ReporteBandasTexto { Texto = "", Alto = 0 };
        return new ReporteBandasTexto { Texto = texto, Alto = (double)o.NUM_ALTO, DePagina = dePagina };
    }

    private bool TieneMarco(ReporteLienzoObjeto o)
        => !string.IsNullOrEmpty(o.DES_COLOR_FONDO) || !string.IsNullOrEmpty(o.DES_BORDES)
           || _f.TieneCondicion(ReporteFormulasDatos.Objeto, o.NUM_OBJETO, "FONDO");

    /// <summary>Lo que dice un campo, texto, resumen o especial en esta banda (página y total solo al dibujar).</summary>
    private string Valor(ReporteLienzoObjeto o, ReporteBandasInstancia inst, int pagina, int paginas)
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
                if (t is null || fila is null) return "";
                var vista = Vista(r, col);
                var formato = ReporteColumnas.Vacio(o.DES_FORMATO) ?? vista?.Formato;
                return ReporteFormato.Texto(t.Valor(fila, col), formato, vista?.Tipo ?? typeof(string), c => t.Valor(fila, c));
            }
            case "TXT":
            case "BAR":
                return Reemplazar(o.DES_ORIGEN, inst, pagina, paginas, especiales: true);
            case "FOR":
                return ReporteFormula.Formatear(_f.Formula(o.DES_ORIGEN, Lugar(inst, pagina, paginas)), ReporteColumnas.Vacio(o.DES_FORMATO));
            case "RES":
                return Resumen(o, inst);
            case "ESP":
            {
                var origen = (o.DES_ORIGEN ?? "").Trim();
                if (origen.StartsWith('@'))
                    return _ctx.Valores.FirstOrDefault(x => string.Equals(x.Campo, origen[1..], StringComparison.OrdinalIgnoreCase))?.Texto ?? "";
                return Especial(origen, ReporteColumnas.Vacio(o.DES_FORMATO), inst, pagina, paginas) ?? "";
            }
            default:
                return "";
        }
    }

    /// <summary>{@PARAMETRO} por su valor, {COLUMNA} por el de la fila que se imprime (resultado 1) y, si no es una
    /// columna, un campo especial por su nombre: {USUARIO}, {FECHA_HORA}, {PAGINA}, {TOTAL_PAGINAS}, {FILTROS}...
    /// Y las referencias de las fórmulas: {F.Nombre}, {R2.COLUMNA}, {Σ.Suma.COLUMNA.Grupo1}, {#Pagina}...</summary>
    private string Reemplazar(string? texto, ReporteBandasInstancia inst, int pagina = 0, int paginas = 0, bool especiales = false)
    {
        if (string.IsNullOrEmpty(texto)) return "";
        return Marca.Replace(texto, m =>
        {
            var clave = m.Groups[1].Value.Trim();
            if (ReporteFormulasDatos.EsReferencia(clave)) return _f.Marca(clave, Lugar(inst, pagina, paginas)) ?? m.Value;
            if (!MarcaSimple.IsMatch(clave)) return m.Value;
            if (clave.StartsWith('@'))
            {
                var v = _ctx.Valores.FirstOrDefault(x => string.Equals(x.Campo, clave[1..], StringComparison.OrdinalIgnoreCase));
                return v is null ? m.Value : (v.Texto ?? "");
            }
            if (_tabla is null || _tabla.Indice(clave) < 0)
                return especiales ? Especial(clave, null, inst, pagina, paginas) ?? m.Value : m.Value;
            var fila = inst.Fila >= 0 && inst.Fila < _filas.Count ? _filas[inst.Fila] : _filas.FirstOrDefault();
            if (fila is null) return "";
            var vista = Vista(_tabla.Numero, clave);
            return ReporteFormato.Texto(_tabla.Valor(fila, clave), vista?.Formato, vista?.Tipo ?? typeof(string), c => _tabla.Valor(fila, c));
        });
    }

    private string Resumen(ReporteLienzoObjeto o, ReporteBandasInstancia inst)
    {
        if (_tabla is null) return "";
        var col = o.DES_ORIGEN ?? "";
        if (_tabla.Indice(col) < 0) return "";
        var nivel = o.NUM_NIVEL ?? 0;
        var (desde, hasta) = nivel > 0 && nivel <= inst.Grupos.Length && inst.Grupos[nivel - 1] is { } g
            ? (g.Desde, g.Hasta)
            : (0, _filas.Count);
        hasta = Math.Min(hasta, _filas.Count);
        var vista = Vista(_tabla.Numero, col);
        var valores = new List<object>();
        for (var i = desde; i < hasta; i++)
        {
            var v = _tabla.Valor(_filas[i], col);
            if (v is not null && v is not DBNull) valores.Add(v);
        }
        var (valor, formato, t) = Agregado((o.COD_FUNCION ?? "SUM").ToUpperInvariant(), valores, vista, ReporteColumnas.Vacio(o.DES_FORMATO));
        return valor is null ? "" : ReporteFormato.Texto(valor, formato, t);
    }

    /// <summary>Un campo especial por su nombre; null si no existe. El título y el subtítulo se arman con las marcas de
    /// siempre ({@PARAMETRO}, {COLUMNA}), sin especiales, para que un título no se nombre a sí mismo.</summary>
    private string? Especial(string origen, string? formato, ReporteBandasInstancia inst, int pagina, int paginas)
    {
        var e = _ctx.Empresa;
        return origen.Trim().ToUpperInvariant() switch
        {
            "PAGINA"            => pagina.ToString(CultureInfo.InvariantCulture),
            "TOTAL_PAGINAS"     => paginas.ToString(CultureInfo.InvariantCulture),
            "PAGINA_N_DE_M"     => $"Página {pagina} de {paginas}",
            "FECHA"             => _ctx.Fecha.ToString(formato ?? "dd/MM/yyyy", ReporteFormato.Cultura),
            "FECHA_HORA"        => _ctx.Fecha.ToString(formato ?? "dd/MM/yyyy HH:mm", ReporteFormato.Cultura),
            "USUARIO"           => string.IsNullOrWhiteSpace(e?.DES_USUARIO) ? _ctx.Usuario : e!.DES_USUARIO!,
            "FILTROS"           => ReporteTexto.Filtros(_ctx),
            "TITULO"            => Reemplazar(_ctx.D.DES_TITULO ?? _ctx.D.DES_DISENO, inst),
            "SUBTITULO"         => Reemplazar(_ctx.D.DES_SUBTITULO, inst),
            "EMPRESA_NOMBRE"    => e?.DES_NOMBRE_EMPRESA ?? "",
            "EMPRESA_RUC"       => string.IsNullOrWhiteSpace(e?.NUM_DOCUMENTO_IDENTIDAD) ? "" : $"RUC {e!.NUM_DOCUMENTO_IDENTIDAD}",
            "EMPRESA_DIRECCION" => e?.DES_DOMICILIO_FISCAL ?? "",
            "APLICACION_NOMBRE" => e?.DES_NOMBRE_APLICACION ?? "",
            _                   => null
        };
    }

    private sealed class ComparadorValores : IComparer<object>
    {
        public static readonly ComparadorValores Instancia = new();
        public int Compare(object? a, object? b)
        {
            if (a is null || b is null) return a is null ? (b is null ? 0 : -1) : 1;
            if (a is not string && b is not string && ReporteFormato.ADecimal(a, out var x) && ReporteFormato.ADecimal(b, out var y)) return x.CompareTo(y);
            if (a is IComparable c && a.GetType() == b.GetType()) return c.CompareTo(b);
            return string.Compare(Convert.ToString(a, CultureInfo.InvariantCulture), Convert.ToString(b, CultureInfo.InvariantCulture), StringComparison.CurrentCultureIgnoreCase);
        }
    }

    // ── Fuentes, colores y texto ───────────────────────────────────────────────
    /// <summary>negrita: la que fuerza quien dibuja (las tablas), sin subrayado; condicion: la que dio la condición NEGRITA.</summary>
    private XFont Fuente(ReporteLienzoObjeto o, bool? negrita, bool? condicion = null)
    {
        var familia = (o.DES_FUENTE ?? "SANS").ToUpperInvariant() switch
        {
            "SERIF" => ReporteFuentesPdf.FamiliaSerif,
            "MONO"  => ReporteFuentesPdf.FamiliaMono,
            _       => ReporteFuentesPdf.Familia
        };
        var tam = (double)(o.NUM_TAMANO_LETRA ?? _ctx.D.NUM_TAMANO_LETRA);
        if (tam is < 4 or > 72) tam = 8;
        var b = negrita ?? condicion ?? o.FLG_NEGRITA;
        var estilo = XFontStyleEx.Regular;
        if (b) estilo |= XFontStyleEx.Bold;
        if (o.FLG_CURSIVA) estilo |= XFontStyleEx.Italic;
        if (o.FLG_SUBRAYADO && negrita is null) estilo |= XFontStyleEx.Underline;
        var clave = $"{familia}|{tam.ToString(CultureInfo.InvariantCulture)}|{(int)estilo}";
        if (!_fuentes.TryGetValue(clave, out var f)) _fuentes[clave] = f = new XFont(familia, tam, estilo);
        return f;
    }

    private static XColor Color(string? hex, XColor defecto)
    {
        if (string.IsNullOrWhiteSpace(hex)) return defecto;
        var h = hex.Trim().TrimStart('#');
        if (h.Length != 6 || !int.TryParse(h, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var n)) return defecto;
        return XColor.FromArgb((n >> 16) & 0xFF, (n >> 8) & 0xFF, n & 0xFF);
    }

    /// <summary>Parte un texto en líneas que entran en el ancho (en puntos). Una palabra más larga que el ancho se corta.</summary>
    private List<string> Partir(string texto, XFont f, double anchoPt)
    {
        var lineas = new List<string>();
        if (string.IsNullOrEmpty(texto)) { lineas.Add(""); return lineas; }
        foreach (var parrafo in texto.Replace("\r\n", "\n").Split('\n'))
        {
            if (_medir.MeasureString(parrafo, f).Width <= anchoPt) { lineas.Add(parrafo); continue; }
            var actual = "";
            foreach (var palabra in parrafo.Split(' '))
            {
                var prueba = actual.Length == 0 ? palabra : actual + " " + palabra;
                if (_medir.MeasureString(prueba, f).Width <= anchoPt) { actual = prueba; continue; }
                if (actual.Length > 0) { lineas.Add(actual); actual = ""; }
                // La palabra sola no entra: se corta por letras.
                var resto = palabra;
                while (resto.Length > 0 && _medir.MeasureString(resto, f).Width > anchoPt)
                {
                    var n = resto.Length - 1;
                    while (n > 1 && _medir.MeasureString(resto[..n], f).Width > anchoPt) n--;
                    lineas.Add(resto[..n]);
                    resto = resto[n..];
                }
                actual = resto;
            }
            lineas.Add(actual);
        }
        return lineas;
    }

    // ── 3. Repartir en páginas ─────────────────────────────────────────────────
    private List<ReporteBandasPagina> Paginar(List<ReporteBandasBloque> bloques, out double altoPie)
    {
        var paginas = new List<ReporteBandasPagina>();
        var piePrueba = Bandas("PF").SelectMany(b => Partir(new ReporteBandasInstancia { Banda = b, Fila = _filas.Count > 0 ? 0 : -1 }, true)).ToList();
        altoPie = piePrueba.Sum(b => b.Alto);
        var limite = _altoPagina - _margen - altoPie;
        var disponible = limite - _margen;

        ReporteBandasPagina pagina = null!;
        double y = 0;
        var forzar = false;

        void NuevaPagina(int siguiente)
        {
            pagina = new ReporteBandasPagina { Numero = paginas.Count + 1 };
            paginas.Add(pagina);
            y = _margen;
            var fila = siguiente < bloques.Count ? Raiz(bloques[siguiente]).Fila : _filas.Count - 1;
            var esPrimera = paginas.Count == 1;
            // En la primera página va primero el encabezado del informe; el de página se pone cuando aparece algo más.
            if (!esPrimera) Encabezado(fila, siguiente);
        }

        void Encabezado(int fila, int siguiente)
        {
            foreach (var b in Bandas("PH"))
                foreach (var bl in Partir(new ReporteBandasInstancia { Banda = b, Fila = Math.Max(fila, -1) }, true))
                {
                    pagina.Bloques.Add((bl, y));
                    y += bl.Alto;
                }
            // Los grupos que siguen abiertos repiten su encabezado (si el que viene no es justo el encabezado de ese grupo).
            if (siguiente >= bloques.Count) return;
            var proximo = bloques[siguiente];
            // Los grupos del reporte principal (un bloque de un subinforme está en la banda que lo tiene).
            var raiz = Raiz(proximo);
            var tope = raiz.Banda.COD_TIPO_BANDA == "GH" && proximo.Primero && proximo.Anfitrion is null ? raiz.Banda.NUM_NIVEL - 1 : raiz.Grupos.Length;
            for (var n = 0; n < tope && n < raiz.Grupos.Length && n < _grupos.Count; n++)
            {
                var g = raiz.Grupos[n];
                if (g is null || !_grupos[n].FLG_REPETIR_ENCABEZADO || !_encabezados.TryGetValue(g, out var gh)) continue;
                if (raiz == gh) continue;
                foreach (var bl in Partir(gh, true))
                {
                    pagina.Bloques.Add((bl, y));
                    y += bl.Alto;
                }
            }
            // Un subinforme que sigue repite su encabezado de página y los de sus grupos que lo piden.
            if (proximo.Motor is { } sub && sub != this && proximo.Anfitrion is { } anfitrion)
                foreach (var bl in sub.Continuacion(proximo))
                {
                    bl.Motor = sub;
                    bl.Anfitrion = anfitrion;
                    pagina.Bloques.Add((bl, y));
                    y += bl.Alto;
                }
            // Una tabla que sigue repite su fila de títulos.
            if (proximo.Tipo is ReporteBloqueTipo.FilaTabla or ReporteBloqueTipo.TotalTabla && proximo.Tabla?.Titulo is { } tit)
            {
                pagina.Bloques.Add((tit, y));
                y += tit.Alto;
            }
        }

        var encabezadoPrimera = false;
        NuevaPagina(0);
        for (var i = 0; i < bloques.Count; i++)
        {
            var b = bloques[i];
            // Lo que decide la página es la banda del reporte principal; un bloque de un subinforme solo puede pedir que su
            // propia sección no se parta.
            var raiz = Raiz(b);
            var esSub = b.Anfitrion is not null;
            var banda = raiz.Banda;
            var tipo = banda.COD_TIPO_BANDA;

            // En la primera página, el encabezado de página va después del encabezado del informe.
            if (!encabezadoPrimera && tipo != "RH")
            {
                encabezadoPrimera = true;
                Encabezado(raiz.Fila, i);
            }

            var hayContenido = pagina.Bloques.Any(x => (x.Bloque.Anfitrion is not null || x.Bloque.Inst.Banda.COD_TIPO_BANDA is not ("PH" or "RH"))
                                                       && !EsRepeticion(x.Bloque, b));
            var saltar = forzar && hayContenido;
            forzar = false;
            if (b.Primero && esSub)
            {
                var altoPropio = AltoHasta(bloques, i, FinPropia(bloques, i));
                if (b.Inst.Banda.FLG_MANTENER_JUNTO && y + altoPropio > limite && altoPropio <= disponible && hayContenido) saltar = true;
            }
            else if (b.Primero)
            {
                if (hayContenido && DeSeccion(b.Inst, "NUEVA_PAGINA_ANTES", banda.FLG_NUEVA_PAGINA_ANTES)) saltar = true;
                if (tipo == "GH" && banda.NUM_NIVEL > 0 && banda.NUM_NIVEL <= _grupos.Count && _grupos[banda.NUM_NIVEL - 1].FLG_NUEVA_PAGINA
                    && b.Inst.Grupos[banda.NUM_NIVEL - 1] is { EsPrimero: false } && hayContenido) saltar = true;
                // Mantener junto la banda entera (con sus tablas) o el grupo entero.
                var alto = AltoHasta(bloques, i, FinInstancia(bloques, i));
                if (!saltar && banda.FLG_MANTENER_JUNTO && y + alto > limite && alto <= disponible && hayContenido) saltar = true;
                if (!saltar && tipo == "GH" && banda.NUM_NIVEL > 0 && banda.NUM_NIVEL <= _grupos.Count && _grupos[banda.NUM_NIVEL - 1].FLG_MANTENER_JUNTO
                    && b.Inst.Grupos[banda.NUM_NIVEL - 1] is { } g && _rangosGrupo.TryGetValue(g, out var rg) && rg.Hasta > i)
                {
                    var altoGrupo = AltoHasta(bloques, i, rg.Hasta);
                    if (y + altoGrupo > limite && altoGrupo <= disponible && hayContenido) saltar = true;
                }
            }
            // El título de una tabla no se queda solo al pie: va con su primera fila.
            var conSiguiente = b.Tipo == ReporteBloqueTipo.TituloTabla && i + 1 < bloques.Count ? bloques[i + 1].Alto : 0;
            if (!saltar && y + b.Alto + conSiguiente > limite + 0.01 && hayContenido) saltar = true;
            if (saltar)
            {
                NuevaPagina(i);
                if (b.Tipo == ReporteBloqueTipo.TituloTabla && pagina.Bloques.Count > 0 && pagina.Bloques[^1].Bloque == b) continue;
            }
            pagina.Bloques.Add((b, y));
            y += b.Alto;
            if (EsUltimoDeInstancia(bloques, i) && DeSeccion(raiz, "NUEVA_PAGINA_DESPUES", banda.FLG_NUEVA_PAGINA_DESPUES)) forzar = true;
        }
        if (!encabezadoPrimera) Encabezado(-1, bloques.Count);
        return paginas;
    }

    private static bool EsRepeticion(ReporteBandasBloque puesto, ReporteBandasBloque actual) => false;

    /// <summary>Hasta dónde llega la banda del principal que empieza en i, con los subinformes que tenga adentro.</summary>
    private static int FinInstancia(List<ReporteBandasBloque> bloques, int i)
    {
        var j = i + 1;
        var raiz = Raiz(bloques[i]);
        while (j < bloques.Count && Raiz(bloques[j]) == raiz && !(bloques[j].Primero && bloques[j].Anfitrion is null)) j++;
        return j;
    }

    /// <summary>Hasta dónde llega la banda (de un subinforme) que empieza en i, sin salir de ella.</summary>
    private static int FinPropia(List<ReporteBandasBloque> bloques, int i)
    {
        var j = i + 1;
        while (j < bloques.Count && !bloques[j].Primero && bloques[j].Inst == bloques[i].Inst) j++;
        return j;
    }

    private static bool EsUltimoDeInstancia(List<ReporteBandasBloque> bloques, int i)
        => i + 1 >= bloques.Count || Raiz(bloques[i + 1]) != Raiz(bloques[i]);

    private static double AltoHasta(List<ReporteBandasBloque> bloques, int desde, int hasta)
    {
        double h = 0;
        for (var k = desde; k < hasta && k < bloques.Count; k++) h += bloques[k].Alto;
        return h;
    }

    // ── 4. Dibujar ─────────────────────────────────────────────────────────────
    public byte[] Pdf()
    {
        var bloques = Armar();
        var paginas = Paginar(bloques, out var altoPie);
        var doc = new PdfDocument();
        doc.Info.Title = ReporteTexto.Reemplazar(_ctx.D.DES_TITULO ?? _ctx.D.DES_DISENO, _ctx);
        doc.Info.Author = _ctx.Empresa?.DES_NOMBRE_EMPRESA ?? "";
        var total = paginas.Count;
        var marcadores = new Dictionary<int, PdfOutline>();
        var indice = bloques.Select((b, i) => (b, i)).ToDictionary(x => x.b, x => x.i);
        var inicios = _iniciosGrupo.GroupBy(x => x.Indice).ToDictionary(g => g.Key, g => g.Select(x => x.Grupo).OrderBy(x => x.Nivel).ToList());

        var tramos = Tramos(bloques);

        foreach (var p in paginas)
        {
            var page = doc.AddPage();
            page.Width = XUnit.FromMillimeter(_anchoPagina);
            page.Height = XUnit.FromMillimeter(_altoPagina);
            using var g = XGraphics.FromPdfPage(page);
            // Pie de página: con la última fila que salió en la página.
            var ultima = p.Bloques.Select(x => Raiz(x.Bloque).Fila).Where(f => f >= 0).DefaultIfEmpty(_filas.Count - 1).Max();
            var pie = new List<(ReporteBandasBloque Bloque, double Y)>();
            var yPie = _altoPagina - _margen - altoPie;
            foreach (var bp in Bandas("PF").SelectMany(b => Partir(new ReporteBandasInstancia { Banda = b, Fila = ultima }, true)))
            {
                pie.Add((bp, yPie));
                yPie += bp.Alto;
            }
            var entradas = p.Bloques.Concat(pie).ToList();
            var cruces = Cruces(entradas, tramos, indice, p.Numero, total).ToList();
            // Primero el fondo de las secciones, encima el relleno de los cuadros que cruzan, después lo de cada sección
            // y al final los bordes y las líneas que cruzan.
            foreach (var (b, y) in entradas) DibujarFondo(g, b, y, p.Numero, total);
            foreach (var c in cruces) DibujarCruce(g, c.Objeto, c.Inst, c.Arriba, c.Abajo, p.Numero, total, relleno: true);
            foreach (var (b, y) in p.Bloques)
            {
                Dibujar(g, b, y, p.Numero, total);
                // Árbol de grupos: un marcador por grupo, donde empieza.
                if (b.Primero && indice.TryGetValue(b, out var ix) && inicios.TryGetValue(ix, out var gs))
                    foreach (var gr in gs)
                    {
                        var titulo = string.IsNullOrWhiteSpace(gr.Clave) ? "(sin dato)" : gr.Clave;
                        var padre = gr.Nivel > 1 && marcadores.TryGetValue(gr.Nivel - 1, out var mp) ? mp.Outlines : doc.Outlines;
                        marcadores[gr.Nivel] = padre.Add(titulo, page, gr.Nivel == 1);
                        foreach (var k in marcadores.Keys.Where(k => k > gr.Nivel).ToList()) marcadores.Remove(k);
                    }
            }
            foreach (var (b, y) in pie) Dibujar(g, b, y, p.Numero, total);
            foreach (var c in cruces) DibujarCruce(g, c.Objeto, c.Inst, c.Arriba, c.Abajo, p.Numero, total, relleno: false);
        }
        if (_grupos.Count > 0 && doc.Outlines.Count > 0) doc.PageMode = PdfPageMode.UseOutlines;
        using var ms = new MemoryStream();
        doc.Save(ms, false);
        return ms.ToArray();
    }

    /// <summary>El color de fondo de la sección (el de su condición o el fijo).</summary>
    private void DibujarFondo(XGraphics g, ReporteBandasBloque b, double yMm, int pagina, int paginas)
    {
        if (b.Alto <= 0) return;
        if (b.Motor is { } m && m != this)
        {
            // Detrás de un subinforme sigue el fondo de la banda que lo tiene; encima, el de su sección, en su caja.
            var deAfuera = FondoBanda(b.Anfitrion!, pagina, paginas);
            if (!string.IsNullOrEmpty(deAfuera)) g.DrawRectangle(new XSolidBrush(Color(deAfuera, XColors.White)), Rect(_margen, yMm, _anchoUtil, b.Alto));
            var propio = m.FondoBanda(b.Inst, pagina, paginas);
            if (!string.IsNullOrEmpty(propio)) g.DrawRectangle(new XSolidBrush(Color(propio, XColors.White)), Rect(m._x0, yMm, m._anchoCaja, b.Alto));
            return;
        }
        var fondo = FondoBanda(b.Inst, pagina, paginas);
        if (!string.IsNullOrEmpty(fondo))
            g.DrawRectangle(new XSolidBrush(Color(fondo, XColors.White)), Rect(_margen, yMm, _anchoUtil, b.Alto));
    }

    /// <summary>El fondo de la sección: el de su condición; si no, en las filas pares del detalle el de las filas cebra; si
    /// no, el fijo.</summary>
    private string? FondoBanda(ReporteBandasInstancia inst, int pagina, int paginas)
        => _f.Color(ReporteFormulasDatos.Seccion, inst.Banda.NUM_BANDA, "FONDO", Lugar(inst, pagina, paginas))
           ?? (inst.Banda.COD_TIPO_BANDA == "DE" && inst.Renglon % 2 == 1 && !string.IsNullOrEmpty(inst.Banda.DES_COLOR_CEBRA) ? inst.Banda.DES_COLOR_CEBRA : null)
           ?? inst.Banda.DES_COLOR_FONDO;

    /// <summary>Lo que hay en el bloque (el fondo de la sección va antes, con DibujarFondo).</summary>
    private void Dibujar(XGraphics g, ReporteBandasBloque b, double yMm, int pagina, int paginas)
    {
        if (b.Motor is { } m && m != this)
        {
            // Lo de un subinforme lo dibuja su motor, cortado al ancho de su objeto.
            var estado = g.Save();
            g.IntersectClip(Rect(m._x0, yMm, m._anchoCaja, Math.Max(b.Alto, 0.01)));
            m.Dibujar(g, b, yMm, pagina, paginas);
            g.Restore(estado);
            return;
        }
        var x0 = _x0;
        switch (b.Tipo)
        {
            case ReporteBloqueTipo.Estatico:
                foreach (var o in b.Objetos)
                    DibujarObjeto(g, o, b, x0 + (double)o.NUM_X, yMm + b.PosY.GetValueOrDefault(o.NUM_OBJETO, Math.Max(0, (double)o.NUM_Y - b.Desde)), pagina, paginas);
                break;
            case ReporteBloqueTipo.TituloTabla:
            case ReporteBloqueTipo.FilaTabla:
            case ReporteBloqueTipo.TotalTabla:
                DibujarFilaTabla(g, b, x0 + (double)b.Tabla!.Objeto.NUM_X, yMm);
                break;
            case ReporteBloqueTipo.Aviso:
                DibujarAviso(g, b, x0, yMm);
                break;
        }
    }

    private static XRect Rect(double x, double y, double w, double h) => new(x * MmPt, y * MmPt, Math.Max(0, w) * MmPt, Math.Max(0, h) * MmPt);

    // ── Líneas y cuadros que cruzan secciones ─────────────────────────────────
    /// <summary>¿Termina en otra sección? Solo una línea o un cuadro, y con una sección final que exista.</summary>
    private bool Cruza(ReporteLienzoObjeto o)
        => o.COD_TIPO_OBJETO is "LIN" or "CUA" && o.NUM_BANDA_FIN is { } f && f != o.NUM_BANDA && o.NUM_Y_FIN is not null
           && _l.Bandas.Any(b => b.NUM_BANDA == f);

    /// <summary>Lo que une el principio y el fin de un cruce: el mismo grupo (de un encabezado a su pie), la misma fila
    /// (entre dos partes del detalle) o nada (el informe, que sale una vez).</summary>
    private static object? Clave(ReporteBandasInstancia inst, ReporteLienzoBanda inicio) => inicio.COD_TIPO_BANDA switch
    {
        "GH" or "GF" => inicio.NUM_NIVEL > 0 && inicio.NUM_NIVEL <= inst.Grupos.Length ? inst.Grupos[inicio.NUM_NIVEL - 1] : null,
        "DE" => inst.Fila,
        _ => null
    };

    /// <summary>Los cruces que no son de página, armados sobre los bloques: cada vez que se imprime la sección donde
    /// empieza, hasta la sección donde termina con la misma clave (si no aparece, hasta antes de que vuelva a empezar).</summary>
    private List<ReporteBandasTramo> Tramos(List<ReporteBandasBloque> bloques)
    {
        var res = new List<ReporteBandasTramo>();
        foreach (var o in _l.Objetos.Where(Cruza))
        {
            var inicio = _l.Bandas.First(b => b.NUM_BANDA == o.NUM_BANDA);
            if (inicio.COD_TIPO_BANDA is "PH" or "PF") continue;          // de página: se arman al dibujar cada página
            ReporteBandasTramo? t = null;
            for (var i = 0; i < bloques.Count; i++)
            {
                var b = bloques[i];
                if (b.Inst.Banda.NUM_BANDA == o.NUM_BANDA && b.Primero && b.Motor is null)
                {
                    if (t is not null) res.Add(t);
                    t = new ReporteBandasTramo { Objeto = o, Inicio = b.Inst, Desde = i, Hasta = i };
                    continue;
                }
                if (t is null) continue;
                if (t.Fin is null)
                {
                    if (b.Motor is null && b.Inst.Banda.NUM_BANDA == o.NUM_BANDA_FIN && Equals(Clave(b.Inst, inicio), Clave(t.Inicio, inicio))) t.Fin = b.Inst;
                    t.Hasta = i;
                }
                else if (Raiz(b) == t.Fin) t.Hasta = i;
                else
                {
                    res.Add(t);
                    t = null;
                }
            }
            if (t is not null) res.Add(t);
        }
        return res;
    }

    /// <summary>Dónde va cada cruce en esta página (mm, desde arriba de la hoja). Un tramo que viene de la página anterior
    /// empieza arriba de lo suyo (o en su encabezado repetido); uno que sigue en la siguiente termina abajo de lo suyo.</summary>
    private IEnumerable<(ReporteLienzoObjeto Objeto, ReporteBandasInstancia Inst, double Arriba, double Abajo)> Cruces(
        List<(ReporteBandasBloque Bloque, double Y)> entradas, List<ReporteBandasTramo> tramos, Dictionary<ReporteBandasBloque, int> indice,
        int pagina, int paginas)
    {
        var res = new List<(ReporteLienzoObjeto, ReporteBandasInstancia, double, double)>();
        void Poner(ReporteLienzoObjeto o, ReporteBandasInstancia inst, double arriba, double abajo)
        {
            if (abajo < arriba + 0.01) return;
            if (_f.Logico(ReporteFormulasDatos.Objeto, o.NUM_OBJETO, "SUPRIMIR", Lugar(inst, pagina, paginas)) == true) return;
            res.Add((o, inst, arriba, abajo));
        }
        // De página: del encabezado al pie de página (o entre dos partes de uno de ellos), en cada página.
        foreach (var o in _l.Objetos.Where(Cruza))
        {
            if (_l.Bandas.First(b => b.NUM_BANDA == o.NUM_BANDA).COD_TIPO_BANDA is not ("PH" or "PF")) continue;
            var a = entradas.FirstOrDefault(e => e.Bloque.Motor is null && e.Bloque.Inst.Banda.NUM_BANDA == o.NUM_BANDA && e.Bloque.Primero);
            if (a.Bloque is null) continue;
            var f = entradas.FirstOrDefault(e => e.Bloque.Motor is null && e.Bloque.Inst.Banda.NUM_BANDA == o.NUM_BANDA_FIN && e.Y >= a.Y);
            if (f.Bloque is null) continue;
            Poner(o, a.Bloque.Inst, a.Y - a.Bloque.Desde + (double)o.NUM_Y, f.Y - f.Bloque.Desde + (double)o.NUM_Y_FIN!.Value);
        }
        foreach (var t in tramos)
        {
            var mias = entradas.Where(e => indice.TryGetValue(e.Bloque, out var i) ? i >= t.Desde && i <= t.Hasta : e.Bloque.Inst == t.Inicio).ToList();
            if (mias.Count == 0) continue;
            var ini = mias.FirstOrDefault(e => e.Bloque.Inst == t.Inicio);
            var arriba = ini.Bloque is not null ? ini.Y - ini.Bloque.Desde + (double)t.Objeto.NUM_Y : mias.Min(e => e.Y);
            var fin = t.Fin is null ? default : mias.FirstOrDefault(e => e.Bloque.Inst == t.Fin);
            var abajo = fin.Bloque is not null ? fin.Y - fin.Bloque.Desde + (double)t.Objeto.NUM_Y_FIN!.Value : mias.Max(e => e.Y + e.Bloque.Alto);
            Poner(t.Objeto, t.Inicio, arriba, abajo);
        }
        return res;
    }

    /// <summary>Un cruce: el relleno del cuadro (antes que el contenido) o su borde y las líneas (después). Una línea que
    /// cruza secciones es vertical.</summary>
    private void DibujarCruce(XGraphics g, ReporteLienzoObjeto o, ReporteBandasInstancia inst, double arriba, double abajo, int pagina, int paginas, bool relleno)
    {
        var lugar = Lugar(inst, pagina, paginas);
        var x = _margen + (double)o.NUM_X;
        var grosor = (double)(o.NUM_GROSOR_BORDE ?? 0.5m);
        var color = _f.Color(ReporteFormulasDatos.Objeto, o.NUM_OBJETO, "COLOR", lugar);
        if (o.COD_TIPO_OBJETO == "LIN")
        {
            if (!relleno && grosor > 0)
                g.DrawLine(new XPen(Color(color ?? o.DES_COLOR_BORDE ?? o.DES_COLOR, XColors.Black), grosor), x * MmPt, arriba * MmPt, x * MmPt, abajo * MmPt);
            return;
        }
        var r = Rect(x, arriba, (double)o.NUM_ANCHO, abajo - arriba);
        if (relleno)
        {
            var fondo = _f.Color(ReporteFormulasDatos.Objeto, o.NUM_OBJETO, "FONDO", lugar) ?? o.DES_COLOR_FONDO;
            if (!string.IsNullOrEmpty(fondo)) g.DrawRectangle(new XSolidBrush(Color(fondo, XColors.White)), r);
        }
        else if (grosor > 0)
            g.DrawRectangle(new XPen(Color(color ?? o.DES_COLOR_BORDE, XColors.Black), grosor), r);
    }

    private void DibujarObjeto(XGraphics g, ReporteLienzoObjeto o, ReporteBandasBloque b, double x, double y, int pagina, int paginas)
    {
        var w = (double)o.NUM_ANCHO;
        var h = b.Altos.GetValueOrDefault(o.NUM_OBJETO, (double)o.NUM_ALTO);
        // Color y fondo pueden venir de una condición (con la fila y la página de este bloque).
        var lugar = Lugar(b.Inst, pagina, paginas);
        var colorCond = _f.Color(ReporteFormulasDatos.Objeto, o.NUM_OBJETO, "COLOR", lugar);
        var fondo = _f.Color(ReporteFormulasDatos.Objeto, o.NUM_OBJETO, "FONDO", lugar) ?? o.DES_COLOR_FONDO;
        switch (o.COD_TIPO_OBJETO)
        {
            case "LIN":
            {
                var pen = new XPen(Color(colorCond ?? o.DES_COLOR_BORDE ?? o.DES_COLOR, XColors.Black), (double)(o.NUM_GROSOR_BORDE ?? 0.5m));
                g.DrawLine(pen, x * MmPt, y * MmPt, (x + w) * MmPt, (y + h) * MmPt);
                return;
            }
            case "CUA":
            {
                var r = Rect(x, y, w, h);
                if (!string.IsNullOrEmpty(fondo)) g.DrawRectangle(new XSolidBrush(Color(fondo, XColors.White)), r);
                var grosor = (double)(o.NUM_GROSOR_BORDE ?? 0.5m);
                if (grosor > 0) g.DrawRectangle(new XPen(Color(colorCond ?? o.DES_COLOR_BORDE, XColors.Black), grosor), r);
                return;
            }
            case "IMG":
            {
                var img = Imagen(o);
                if (img is null) return;
                var (iw, ih) = (img.PixelWidth, img.PixelHeight);
                if (iw <= 0 || ih <= 0) return;
                // La imagen entra entera en su recuadro, sin deformarse; la alineación dice si va a la izquierda, al
                // centro o a la derecha.
                var escala = Math.Min(w / iw, h / ih);
                var (dw, dh) = (iw * escala, ih * escala);
                var ix = (o.COD_ALINEACION ?? "C").ToUpperInvariant() switch { "I" => x, "D" => x + w - dw, _ => x + (w - dw) / 2 };
                try { g.DrawImage(img, Rect(ix, y + (h - dh) / 2, dw, dh)); }
                catch { /* una imagen que PDFsharp no puede leer no impide el reporte */ }
                return;
            }
            case "BAR":
            {
                if (!b.Textos.TryGetValue(o.NUM_OBJETO, out var tc)) return;
                var contenido = tc.DePagina ? Valor(o, b.Inst, pagina, paginas) : tc.Texto;
                if (string.IsNullOrEmpty(contenido) && o.FLG_SUPRIMIR_VACIO) return;
                if (!string.IsNullOrEmpty(fondo)) g.DrawRectangle(new XSolidBrush(Color(fondo, XColors.White)), Rect(x, y, w, h));
                DibujarCodigo(g, o, contenido, x, y, w, h, Color(colorCond ?? o.DES_COLOR, XColors.Black));
                return;
            }
            case "GRA":
                DibujarGrafico(g, o, b, x, y, w, h, fondo);
                return;
        }
        if (!b.Textos.TryGetValue(o.NUM_OBJETO, out var t)) return;
        var lineas = t.Lineas;
        var giro = Giro(o);
        var vertical = giro is 90 or 270;
        if (t.DePagina)
        {
            var texto = Valor(o, b.Inst, pagina, paginas);
            lineas = Partir(texto, Fuente(o, null, t.Negrita), Math.Max(1, (vertical ? h : w) - 2 * (TieneMarco(o) ? Relleno : 0)) * MmPt);
        }
        else if (string.IsNullOrEmpty(t.Texto) && o.FLG_SUPRIMIR_VACIO) return;
        var alto = Math.Max(h, t.Alto);
        var caja = Rect(x, y, w, alto);
        if (!string.IsNullOrEmpty(fondo)) g.DrawRectangle(new XSolidBrush(Color(fondo, XColors.White)), caja);
        Bordes(g, o, x, y, w, alto);
        var f = Fuente(o, null, t.Negrita);
        var pad = TieneMarco(o) ? Relleno : 0;
        var lh = f.GetHeight();
        var formato = (o.COD_ALINEACION ?? "I").ToUpperInvariant() switch
        {
            "C" => XStringFormats.TopCenter,
            "D" => XStringFormats.TopRight,
            _   => XStringFormats.TopLeft
        };
        var brocha = new XSolidBrush(Color(colorCond ?? o.DES_COLOR, XColors.Black));
        var estado = g.Save();
        g.IntersectClip(caja);
        // Girado: se gira la hoja alrededor del centro del objeto y el texto se escribe en un marco del mismo centro, con el
        // ancho y el alto cambiados si va a lo alto. 90: se lee de abajo hacia arriba; 270: de arriba hacia abajo.
        var (fw, fh) = vertical ? (alto, w) : (w, alto);
        if (giro != 0)
            g.RotateAtTransform(giro switch { 90 => -90, 270 => 90, _ => 180 }, new XPoint((x + w / 2) * MmPt, (y + alto / 2) * MmPt));
        var fx = x + (w - fw) / 2;
        var yl = (y + (alto - fh) / 2 + pad) * MmPt;
        foreach (var linea in lineas)
        {
            g.DrawString(linea, f, brocha, new XRect((fx + pad) * MmPt, yl, Math.Max(1, fw - 2 * pad) * MmPt, lh), formato);
            yl += lh;
        }
        g.Restore(estado);
    }

    /// <summary>Un código de barras (las barras llenan el ancho, con su margen en blanco a los lados y el texto debajo) o un
    /// QR (cuadrado, del lado menor, alineado como diga el objeto). Si el texto no se puede codificar, lo dice en rojo.</summary>
    private void DibujarCodigo(XGraphics g, ReporteLienzoObjeto o, string contenido, double x, double y, double w, double h, XColor tinta)
    {
        var tipo = (o.COD_CODIGO ?? "C128").ToUpperInvariant();
        var brocha = new XSolidBrush(tinta);
        string? error;
        if (tipo == "QR")
        {
            var qr = ReporteCodigos.Qr(contenido, out error);
            if (qr is not null)
            {
                var n = qr.GetLength(0);
                var lado = Math.Min(w, h);
                var m = lado / n;
                var qx = (o.COD_ALINEACION ?? "C").ToUpperInvariant() switch { "I" => x, "D" => x + w - lado, _ => x + (w - lado) / 2 };
                var qy = y + (h - lado) / 2;
                for (var fy = 0; fy < n; fy++)
                    for (var fx = 0; fx < n; fx++)
                    {
                        if (!qr[fy, fx]) continue;
                        var desde = fx;
                        while (fx + 1 < n && qr[fy, fx + 1]) fx++;
                        g.DrawRectangle(brocha, Rect(qx + desde * m, qy + fy * m, (fx - desde + 1) * m, m));
                    }
                return;
            }
        }
        else
        {
            var barras = ReporteCodigos.Barras(tipo, contenido, out error);
            if (barras is not null)
            {
                var conTexto = o.FLG_TEXTO_CODIGO;
                var letra = Fuente(o, false);
                var th = conTexto ? letra.GetHeight() / MmPt : 0;
                var hb = Math.Max(1, h - th);
                var m = w / barras.Total;
                for (var k = 0; k < barras.Modulos.Length; k++)
                {
                    if (!barras.Modulos[k]) continue;
                    var desde = k;
                    var guarda = barras.Guardas[k];
                    while (k + 1 < barras.Modulos.Length && barras.Modulos[k + 1] && barras.Guardas[k + 1] == guarda) k++;
                    // En un EAN-13 las guardas bajan hasta la mitad del texto.
                    var alto = guarda && conTexto ? hb + th / 2 : hb;
                    g.DrawRectangle(brocha, Rect(x + (barras.MargenIzquierdo + desde) * m, y, (k - desde + 1) * m, alto));
                }
                if (!conTexto) return;
                var ty = (y + hb) * MmPt;
                if (tipo == "EAN" && barras.Texto.Length == 13)
                {
                    // Como en el empaque: el primer dígito afuera, a la izquierda; seis bajo cada mitad.
                    var izq = barras.MargenIzquierdo;
                    g.DrawString(barras.Texto[..1], letra, brocha, new XRect(x * MmPt, ty, (izq - 1) * m * MmPt, th * MmPt), XStringFormats.TopRight);
                    g.DrawString(barras.Texto[1..7], letra, brocha, new XRect((x + (izq + 3) * m) * MmPt, ty, 42 * m * MmPt, th * MmPt), XStringFormats.TopCenter);
                    g.DrawString(barras.Texto[7..], letra, brocha, new XRect((x + (izq + 50) * m) * MmPt, ty, 42 * m * MmPt, th * MmPt), XStringFormats.TopCenter);
                }
                else g.DrawString(barras.Texto, letra, brocha, new XRect(x * MmPt, ty, w * MmPt, th * MmPt), XStringFormats.TopCenter);
                return;
            }
        }
        var aviso = new XFont(ReporteFuentesPdf.Familia, 6, XFontStyleEx.Italic);
        g.DrawString(error ?? "Código no válido.", aviso, new XSolidBrush(XColor.FromArgb(0xc6, 0x28, 0x28)), Rect(x, y, w, h), XStringFormats.TopLeft);
    }

    private static void Bordes(XGraphics g, ReporteLienzoObjeto o, double x, double y, double w, double h)
    {
        var bordes = (o.DES_BORDES ?? "").ToUpperInvariant();
        if (bordes.Length == 0) return;
        var pen = new XPen(Color(o.DES_COLOR_BORDE, XColors.Black), (double)(o.NUM_GROSOR_BORDE ?? 0.5m));
        if (bordes.Contains('A')) g.DrawLine(pen, x * MmPt, y * MmPt, (x + w) * MmPt, y * MmPt);
        if (bordes.Contains('B')) g.DrawLine(pen, x * MmPt, (y + h) * MmPt, (x + w) * MmPt, (y + h) * MmPt);
        if (bordes.Contains('I')) g.DrawLine(pen, x * MmPt, y * MmPt, x * MmPt, (y + h) * MmPt);
        if (bordes.Contains('D')) g.DrawLine(pen, (x + w) * MmPt, y * MmPt, (x + w) * MmPt, (y + h) * MmPt);
    }

    private XImage? Imagen(ReporteLienzoObjeto o)
    {
        if (string.Equals(o.DES_ORIGEN, "LOGO_APLICACION", StringComparison.OrdinalIgnoreCase))
        {
            if (_logoAplicacionLeido) return _logoAplicacion;
            _logoAplicacionLeido = true;
            _logoAplicacion = Leer(_ctx.Empresa?.IMG_LOGO_APLICACION);
            return _logoAplicacion;
        }
        if (string.Equals(o.DES_ORIGEN, "LOGO", StringComparison.OrdinalIgnoreCase) || o.IMG_IMAGEN is not { Length: > 0 })
        {
            if (_logoLeido) return _logo;
            _logoLeido = true;
            _logo = Leer(_ctx.Empresa?.IMG_LOGO_EMPRESA);
            return _logo;
        }
        if (_imagenes.TryGetValue(o.NUM_OBJETO, out var i)) return i;
        return _imagenes[o.NUM_OBJETO] = Leer(o.IMG_IMAGEN);
    }

    private static XImage? Leer(byte[]? bytes)
    {
        if (bytes is not { Length: > 0 } || !ReporteImagen.EsSoportada(bytes)) return null;
        try { return XImage.FromStream(new MemoryStream(bytes)); }
        catch { return null; }
    }

    private void DibujarFilaTabla(XGraphics g, ReporteBandasBloque b, double x, double y)
    {
        var t = b.Tabla!;
        var ancho = t.Anchos.Sum();
        var resaltada = b.Tipo == ReporteBloqueTipo.FilaTabla && t.UltimaResaltada && b.FilaTabla == t.Celdas.Count - 1;
        var f = b.Tipo == ReporteBloqueTipo.FilaTabla && !resaltada ? t.Fuente : t.Negrita;
        var lh = f.GetHeight();
        if (b.Tipo == ReporteBloqueTipo.TituloTabla)
            g.DrawRectangle(new XSolidBrush(Tinta), Rect(x, y, ancho, b.Alto));
        if (resaltada || b.Tipo == ReporteBloqueTipo.TotalTabla)
            g.DrawRectangle(new XSolidBrush(Resalte), Rect(x, y, ancho, b.Alto));
        if (resaltada)
            g.DrawLine(new XPen(Tinta, 0.8), x * MmPt, y * MmPt, (x + ancho) * MmPt, y * MmPt);
        var brocha = new XSolidBrush(b.Tipo == ReporteBloqueTipo.TituloTabla ? XColors.White : XColors.Black);
        // Fila de total: "Total" ocupa las columnas que van antes de la primera que suma (la de una tabla cruzada ya viene
        // escrita, con su «Total» en la primera columna).
        var primera = b.Tipo == ReporteBloqueTipo.TotalTabla && t.FilaTotal is null ? t.Columnas.FindIndex(c => t.Totales.ContainsKey(c.Nombre)) : -1;
        if (primera > 0)
        {
            var wt = t.Anchos.Take(primera).Sum();
            g.DrawString("Total", f, brocha, new XRect((x + Relleno) * MmPt, (y + Relleno) * MmPt, (wt - 2 * Relleno) * MmPt, lh), XStringFormats.TopRight);
        }
        var cx = x;
        for (var c = 0; c < t.Columnas.Count; c++)
        {
            var col = t.Columnas[c];
            var w = t.Anchos[c];
            if (c < primera) { cx += w; continue; }
            var texto = b.Tipo switch
            {
                ReporteBloqueTipo.TituloTabla => col.Etiqueta,
                ReporteBloqueTipo.FilaTabla   => t.Celdas[b.FilaTabla][c],
                _ => t.FilaTotal is { } ft ? (c < ft.Count ? ft[c] : "")
                   : t.Totales.TryGetValue(col.Nombre, out var tot) ? ReporteFormato.Texto(tot, col.Formato, col.Tipo) : ""
            };
            var formato = col.Alineacion switch { 'D' => XStringFormats.TopRight, 'C' => XStringFormats.TopCenter, _ => XStringFormats.TopLeft };
            if (b.Tipo == ReporteBloqueTipo.TituloTabla && col.Alineacion == 'I') formato = XStringFormats.TopLeft;
            var yl = (y + Relleno) * MmPt;
            var estado = g.Save();
            g.IntersectClip(Rect(cx, y, w, b.Alto));
            foreach (var linea in Partir(texto, f, (w - 2 * Relleno) * MmPt))
            {
                g.DrawString(linea, f, brocha, new XRect((cx + Relleno) * MmPt, yl, (w - 2 * Relleno) * MmPt, lh), formato);
                yl += lh;
            }
            g.Restore(estado);
            cx += w;
        }
        // Sin títulos (los totales de una ficha) va sin rayas entre filas.
        if (b.Tipo == ReporteBloqueTipo.FilaTabla && t.ConTitulo && !resaltada)
            g.DrawLine(new XPen(LineaTabla, 0.5), x * MmPt, (y + b.Alto) * MmPt, (x + ancho) * MmPt, (y + b.Alto) * MmPt);
        else if (b.Tipo == ReporteBloqueTipo.TotalTabla)
            g.DrawLine(new XPen(XColors.Black, 0.8), x * MmPt, y * MmPt, (x + ancho) * MmPt, y * MmPt);
    }
}
