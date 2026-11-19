using System.Globalization;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Domain.Tizado;

namespace KonSolutionsApi.Application.Services;

/// <summary>Moldes y tizados de una version de diseno (script SQL 115). Los moldes se normalizan (contorno al origen, area y medidas
/// calculadas aqui) y el tizado se calcula con el motor de Domain/Tizado a partir de los moldes guardados.</summary>
public class DisenosTizadoService : IDisenosTizadoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(DisenosTizadoService);
    // limites para proteger al servidor
    public const int MaxMoldes = 500, MaxPiezas = 3000, MaxBytesDxf = 5_000_000;
    public const int TiempoMaxMs = 25_000;

    private readonly IDisenosTizadoRepository _repo;
    private readonly ErrorLogService _log;
    public DisenosTizadoService(IDisenosTizadoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    // 52401 = version bloqueada (script 107); 52480 a 52485 = validaciones del script 115; 52490 a 52494 = las del 117 (gradacion)
    private static bool EsValidacion(SqlException ex) => ex.Number is 52401 or (>= 52480 and <= 52485) or (>= 52490 and <= 52494);
    private sealed class ValidacionException : Exception { public ValidacionException(string m) : base(m) { } }

    private async Task<ApiResponse<T>> Ejecutar<T>(Func<Task<T>> accion, string metodo)
    {
        try { return ApiResponse<T>.Ok(await accion()); }
        catch (ValidacionException ex) { return ApiResponse<T>.Fail(ex.Message); }
        catch (SqlException ex) when (EsValidacion(ex)) { return ApiResponse<T>.Fail(ex.Message); }
        catch (OperationCanceledException) { return ApiResponse<T>.Fail("El calculo se cancelo."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, metodo); return ApiResponse<T>.Fail(ex.Message); }
    }

    // ---------- texto <-> poligono ----------
    private static string AFormato(IEnumerable<Punto> pts) => string.Join(" ", pts.Select(p => N(p.X) + "," + N(p.Y)));
    private static string N(double d) => d.ToString("0.###", CultureInfo.InvariantCulture);

    /// <summary>"x,y x,y ..." -> poligono; null si el texto no es valido.</summary>
    public static Poligono? LeerContorno(string? texto)
    {
        if (string.IsNullOrWhiteSpace(texto)) return null;
        var pts = new List<Punto>();
        foreach (var t in texto.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries))
        {
            var xy = t.Split(',');
            if (xy.Length != 2 || !double.TryParse(xy[0], NumberStyles.Float, CultureInfo.InvariantCulture, out var x) || !double.TryParse(xy[1], NumberStyles.Float, CultureInfo.InvariantCulture, out var y)
                || double.IsNaN(x) || double.IsInfinity(x) || double.IsNaN(y) || double.IsInfinity(y)) return null;
            pts.Add(new Punto(x, y));
        }
        var p = new Poligono(pts);
        return p.EsValido ? p : null;
    }

    /// <summary>Giros "0,180" -> lista de grados (distintos, entre -360 y 360). Nulo o vacio = 0,180.</summary>
    public static double[]? LeerGiros(string? texto)
    {
        if (string.IsNullOrWhiteSpace(texto)) return new[] { 0d, 180d };
        var l = new List<double>();
        foreach (var t in texto.Split(',', StringSplitOptions.TrimEntries))
        {
            if (!double.TryParse(t, NumberStyles.Float, CultureInfo.InvariantCulture, out var g) || g < -360 || g > 360) return null;
            if (!l.Contains(g)) l.Add(g);
        }
        return l.Count == 0 ? null : l.ToArray();
    }

    // ---------- moldes ----------
    public Task<ApiResponse<List<DisenosMolde>>> MoldesAsync(string codDiseno, int numVersion) => Ejecutar(() => _repo.MoldesAsync(codDiseno, numVersion), nameof(MoldesAsync));

    public Task<ApiResponse<DisenosMoldesGuardarResultado>> GuardarMoldesAsync(DisenosMoldesGuardarDto dto, string codUsuario, string codEstacion) => Ejecutar(async () =>
    {
        if (dto.Moldes is null || dto.Moldes.Count == 0) throw new ValidacionException("No hay moldes para guardar.");
        if (dto.Moldes.Count > MaxMoldes) throw new ValidacionException($"Se pueden guardar hasta {MaxMoldes} moldes por vez.");
        var filas = new List<object>();
        for (int i = 0; i < dto.Moldes.Count; i++)
        {
            var m = dto.Moldes[i]; var nombre = string.IsNullOrWhiteSpace(m.DES_MOLDE) ? "(sin nombre)" : m.DES_MOLDE.Trim();
            var poli = LeerContorno(m.DES_CONTORNO) ?? throw new ValidacionException($"El contorno del molde {nombre} no es valido (se necesitan al menos 3 puntos \"x,y\" separados por espacio y que encierren un area).");
            var giros = LeerGiros(m.DES_ROTACIONES) ?? throw new ValidacionException($"Los giros del molde {nombre} no son validos (use grados separados por coma, por ejemplo 0,180).");
            var normal = poli.AlOrigen();                       // el contorno se guarda con su esquina minima en (0,0)
            var (px, py) = PuntoCoincidenciaNormal(m, poli, normal, nombre);
            filas.Add(new
            {
                m.NUM_SECUENCIA, m.DES_MOLDE, m.DES_TALLA, m.NUM_CANTIDAD, m.NUM_CANTIDAD_ESPEJADA,
                DES_ROTACIONES = string.Join(",", giros.Select(N)),
                DES_CONTORNO = AFormato(normal.Puntos),
                VAL_AREA = Math.Round(normal.Area, 3), VAL_LARGO = Math.Round(normal.Ancho, 3), VAL_ALTO = Math.Round(normal.Alto, 3),
                m.FLG_COINCIDE_X, m.FLG_COINCIDE_Y, VAL_PUNTO_X = px, VAL_PUNTO_Y = py, VAL_FASE_X = Math.Round(m.VAL_FASE_X, 3), VAL_FASE_Y = Math.Round(m.VAL_FASE_Y, 3)
            });
        }
        return new DisenosMoldesGuardarResultado { Secuencias = await _repo.GuardarMoldesAsync(dto.COD_DISENO, dto.NUM_VERSION, filas, codUsuario, codEstacion) };
    }, nameof(GuardarMoldesAsync));

    /// <summary>El punto llega en las coordenadas del contorno recibido; como el contorno se guarda con su esquina en (0,0), el punto se corre igual.</summary>
    private static (double? X, double? Y) PuntoCoincidenciaNormal(DisenosMoldeEntrada m, Poligono original, Poligono normal, string nombre)
    {
        if (m.VAL_FASE_X is < -100000 or > 100000 || m.VAL_FASE_Y is < -100000 or > 100000 || double.IsNaN(m.VAL_FASE_X) || double.IsNaN(m.VAL_FASE_Y))
            throw new ValidacionException($"La fase de rayas del molde {nombre} no es valida (entre -100000 y 100000 mm).");
        if ((m.FLG_COINCIDE_X || m.FLG_COINCIDE_Y) && (m.VAL_PUNTO_X is null || m.VAL_PUNTO_Y is null))
            throw new ValidacionException($"El molde {nombre} pide coincidencia de rayas pero no tiene punto de coincidencia.");
        if (m.VAL_PUNTO_X is null || m.VAL_PUNTO_Y is null) return (null, null);
        if (double.IsNaN(m.VAL_PUNTO_X.Value) || double.IsInfinity(m.VAL_PUNTO_X.Value) || double.IsNaN(m.VAL_PUNTO_Y.Value) || double.IsInfinity(m.VAL_PUNTO_Y.Value))
            throw new ValidacionException($"El punto de coincidencia del molde {nombre} no es valido.");
        var l = original.Limites;
        double x = Math.Round(m.VAL_PUNTO_X.Value - l.MinX, 3), y = Math.Round(m.VAL_PUNTO_Y.Value - l.MinY, 3);
        if (x < -0.001 || y < -0.001 || x > normal.Ancho + 0.001 || y > normal.Alto + 0.001)
            throw new ValidacionException($"El punto de coincidencia del molde {nombre} debe estar dentro de la caja del molde (0 a {N(normal.Ancho)} por 0 a {N(normal.Alto)} mm).");
        return (Math.Max(0, x), Math.Max(0, y));
    }

    public Task<ApiResponse<DisenosMoldeEliminarResultado>> EliminarMoldeAsync(DisenosMoldeClaveDto clave) =>
        Ejecutar(async () => new DisenosMoldeEliminarResultado { ELIMINADOS = await _repo.EliminarMoldeAsync(clave) }, nameof(EliminarMoldeAsync));

    public Task<ApiResponse<DisenosMoldeImportarResultado>> ImportarDxfAsync(DisenosMoldeImportarDto dto) => Ejecutar(() =>
    {
        if (string.IsNullOrWhiteSpace(dto.DXF)) throw new ValidacionException("Falta el contenido del archivo DXF.");
        if (dto.DXF.Length > MaxBytesDxf) throw new ValidacionException("El archivo DXF es demasiado grande (maximo 5 MB).");
        if (dto.MM_POR_UNIDAD is double f && (f <= 0 || double.IsNaN(f) || double.IsInfinity(f))) throw new ValidacionException("Los milimetros por unidad deben ser mayores que cero.");
        var r = LectorDxf.Leer(dto.DXF, dto.MM_POR_UNIDAD);
        var res = new DisenosMoldeImportarResultado { Advertencias = r.Advertencias, MilimetrosPorUnidad = r.MilimetrosPorUnidad };
        foreach (var c in r.Contornos.Take(MaxMoldes))
        {
            var n = c.Contorno.AlOrigen();
            res.Contornos.Add(new DisenosMoldeImportado { Capa = c.Capa, DES_CONTORNO = AFormato(n.Puntos), VAL_AREA = (decimal)Math.Round(n.Area, 3), VAL_LARGO = (decimal)Math.Round(n.Ancho, 3), VAL_ALTO = (decimal)Math.Round(n.Alto, 3) });
        }
        if (r.Contornos.Count > MaxMoldes) res.Advertencias.Add($"Se leyeron solo los primeros {MaxMoldes} contornos de {r.Contornos.Count}.");
        return Task.FromResult(res);
    }, nameof(ImportarDxfAsync));

    // ---------- tizados ----------
    private static void ValidarParametros(DisenosTizadoCalcularDto d)
    {
        if (string.IsNullOrWhiteSpace(d.COD_DISENO) || d.NUM_VERSION <= 0) throw new ValidacionException("Falta el diseno o la version.");
        if (!(d.VAL_ANCHO_TELA >= 100 && d.VAL_ANCHO_TELA <= 5000)) throw new ValidacionException("El ancho de tela debe estar entre 100 y 5000 mm.");
        if (!(d.VAL_SEPARACION >= 0 && d.VAL_SEPARACION <= 50)) throw new ValidacionException("La separacion debe estar entre 0 y 50 mm.");
        if (!(d.VAL_MARGEN >= 0 && d.VAL_MARGEN <= 100)) throw new ValidacionException("El margen debe estar entre 0 y 100 mm.");
        if (!(d.VAL_CELDA >= 1 && d.VAL_CELDA <= 20)) throw new ValidacionException("La celda debe estar entre 1 y 20 mm.");
        if (d.NUM_INTENTOS < 1 || d.NUM_INTENTOS > 30) throw new ValidacionException("Los intentos deben estar entre 1 y 30.");
        if (d.Tallas is null || d.Tallas.Count == 0) throw new ValidacionException("Indica al menos una talla con sus prendas.");
        if (d.Tallas.Any(t => string.IsNullOrWhiteSpace(t.DES_TALLA) || t.NUM_PRENDAS < 0 || t.NUM_PRENDAS > 100000)) throw new ValidacionException("Las tallas necesitan nombre y una cantidad de prendas valida.");
        if (d.Tallas.Select(t => t.DES_TALLA.Trim().ToUpperInvariant()).Distinct().Count() != d.Tallas.Count) throw new ValidacionException("Hay tallas repetidas.");
        if (d.Tallas.Sum(t => (long)t.NUM_PRENDAS) <= 0) throw new ValidacionException("Indica al menos una prenda.");
        if (double.IsNaN(d.VAL_PERIODO_X) || double.IsNaN(d.VAL_PERIODO_Y) || !(d.VAL_PERIODO_X == 0 || (d.VAL_PERIODO_X >= 1 && d.VAL_PERIODO_X <= 5000)) || !(d.VAL_PERIODO_Y == 0 || (d.VAL_PERIODO_Y >= 1 && d.VAL_PERIODO_Y <= 5000)))
            throw new ValidacionException("El periodo de la raya debe ser 0 (sin rayas) o estar entre 1 y 5000 mm.");
        if ((d.Defectos?.Count ?? 0) > MaxDefectos) throw new ValidacionException($"Se pueden indicar hasta {MaxDefectos} defectos de tela.");
    }

    public const int MaxDefectos = 100;

    /// <summary>Los defectos de la peticion como poligonos del motor, con su nombre. Lanza ValidacionException con el defecto que falla.</summary>
    public static List<Defecto> LeerDefectos(IEnumerable<DisenosTizadoDefectoEntrada>? entradas, double anchoTela)
    {
        var res = new List<Defecto>(); int i = 0;
        foreach (var e in entradas ?? Array.Empty<DisenosTizadoDefectoEntrada>())
        {
            i++; var nombre = string.IsNullOrWhiteSpace(e.DES_DEFECTO) ? $"Defecto {i}" : e.DES_DEFECTO.Trim();
            if (nombre.Length > 100) throw new ValidacionException($"El nombre del defecto {i} supera los 100 caracteres.");
            var poli = LeerContorno(e.DES_CONTORNO) ?? throw new ValidacionException($"El contorno del defecto {nombre} no es valido (se necesitan al menos 3 puntos \"x,y\" que encierren un area).");
            var l = poli.Limites;
            if (l.MinX < 0 || l.MinY < 0 || l.MaxY > anchoTela) throw new ValidacionException($"El defecto {nombre} queda fuera de la tela (el largo empieza en 0 y el ancho va de 0 a {N(anchoTela)} mm).");
            res.Add(new Defecto { Nombre = nombre, Contorno = poli });
        }
        return res;
    }

    /// <summary>Piezas a cortar: cada molde de una talla pedida por sus prendas, y los de talla U (unica) por todas las prendas.</summary>
    public static List<Pieza> ArmarPiezas(IEnumerable<DisenosMolde> moldes, IReadOnlyList<DisenosTizadoTalla> tallas, out int prendas)
    {
        prendas = tallas.Sum(t => t.NUM_PRENDAS);
        var porTalla = tallas.ToDictionary(t => t.DES_TALLA.Trim().ToUpperInvariant(), t => t.NUM_PRENDAS);
        var res = new List<Pieza>();
        foreach (var m in moldes)
        {
            var talla = (m.DES_TALLA ?? "U").Trim();
            long k = talla.Equals("U", StringComparison.OrdinalIgnoreCase) ? prendas : porTalla.TryGetValue(talla.ToUpperInvariant(), out var n) ? n : 0;
            if (k <= 0) continue;
            var poli = LeerContorno(m.DES_CONTORNO); if (poli is null) continue;
            var giros = LeerGiros(m.DES_ROTACIONES) ?? new[] { 0d };
            long cant = m.NUM_CANTIDAD * k, esp = m.NUM_CANTIDAD_ESPEJADA * k;
            if (cant <= 0) continue;
            if (cant > MaxPiezas) throw new ValidacionException($"El molde {m.DES_MOLDE} pide {cant} piezas: demasiadas para un tizado (maximo {MaxPiezas} en total).");
            res.Add(new Pieza
            {
                Id = m.NUM_SECUENCIA.ToString(CultureInfo.InvariantCulture), Nombre = m.DES_MOLDE, Talla = talla, Contorno = poli, Cantidad = (int)cant, CantidadEspejada = (int)esp, Rotaciones = giros,
                PuntoCoincidencia = m.VAL_PUNTO_X is { } px && m.VAL_PUNTO_Y is { } py ? new Punto((double)px, (double)py) : null,
                CoincideX = m.FLG_COINCIDE_X, CoincideY = m.FLG_COINCIDE_Y, FaseX = (double)m.VAL_FASE_X, FaseY = (double)m.VAL_FASE_Y
            });
        }
        if (res.Sum(p => (long)p.Cantidad) > MaxPiezas) throw new ValidacionException($"El tizado pide mas de {MaxPiezas} piezas: divide las prendas en varios tizados.");
        return res;
    }

    private async Task<(DisenosTizadoCalculo C, string Tallas)> Calcular(DisenosTizadoCalcularDto d, CancellationToken ct)
    {
        ValidarParametros(d);
        var moldes = await _repo.MoldesAsync(d.COD_DISENO, d.NUM_VERSION);
        var piezas = ArmarPiezas(moldes, d.Tallas, out int prendas);
        if (piezas.Count == 0) throw new ValidacionException("No hay moldes para las tallas elegidas: cargue los moldes de esa talla (o de talla U, unica).");
        var defectos = LeerDefectos(d.Defectos, d.VAL_ANCHO_TELA);
        var p = new ParametrosTizado { AnchoTela = d.VAL_ANCHO_TELA, Separacion = d.VAL_SEPARACION, Margen = d.VAL_MARGEN, Celda = d.VAL_CELDA, Intentos = d.NUM_INTENTOS, TiempoMaxMs = TiempoMaxMs, Defectos = defectos, PeriodoX = d.VAL_PERIODO_X, PeriodoY = d.VAL_PERIODO_Y };
        ResultadoTizado r;
        try { r = await Task.Run(() => Tizador.Calcular(piezas, p, ct), ct); }
        catch (ArgumentException ex) { throw new ValidacionException(ex.Message); }      // p. ej. un molde pide coincidencia y el tizado no tiene periodo
        var tallas = string.Join(";", d.Tallas.Select(t => t.DES_TALLA.Trim() + "=" + t.NUM_PRENDAS.ToString(CultureInfo.InvariantCulture)));
        return (ADto(r, prendas, defectos, d.VAL_PERIODO_X, d.VAL_PERIODO_Y, d.VAL_SEPARACION, d.VAL_MARGEN), tallas);
    }

    private static DisenosTizadoCalculo ADto(ResultadoTizado r, int prendas, IReadOnlyList<Defecto> defectos, double periodoX, double periodoY, double separacion, double margen) => new()
    {
        VAL_ANCHO_TELA = r.Ancho, VAL_LARGO = Math.Round(r.Largo, 1), VAL_EFICIENCIA = Math.Round(r.Eficiencia, 4),
        VAL_CONSUMO_PRENDA = prendas > 0 && r.Largo > 0 ? Math.Round(r.Largo / 1000.0 / prendas, 4) : null,
        NUM_PRENDAS = prendas, NUM_PIEZAS = r.Colocadas.Count, NUM_NO_COLOCADAS = r.NoColocadas.Count, IntentoGanador = r.IntentoGanador,
        Geometria = new DisenosTizadoGeometria
        {
            Ancho = r.Ancho, Largo = Math.Round(r.Largo, 1), AreaPiezas = Math.Round(r.AreaPiezas, 1),
            Colocadas = r.Colocadas.Select(c => new DisenosTizadoPieza { PiezaId = c.PiezaId, Nombre = c.Nombre, Talla = c.Talla, Copia = c.Copia, Espejada = c.Espejada, Angulo = c.Angulo, Puntos = AFormato(c.Contorno.Puntos),
                Coincidencia = c.PuntoCoincidencia is { } q ? N(q.X) + "," + N(q.Y) : null, ErrorX = c.ErrorCoincidenciaX is { } ex ? Math.Round(ex, 4) : null, ErrorY = c.ErrorCoincidenciaY is { } ey ? Math.Round(ey, 4) : null, FaseX = c.FaseX, FaseY = c.FaseY }).ToList(),
            NoColocadas = r.NoColocadas.Select(n => new DisenosTizadoNoColocada { PiezaId = n.PiezaId, Nombre = n.Nombre, Talla = n.Talla, Motivo = n.Motivo }).ToList(),
            Defectos = defectos.Select(x => new DisenosTizadoDefecto { Nombre = x.Nombre, Puntos = AFormato(x.Contorno.Puntos) }).ToList(), PeriodoX = periodoX, PeriodoY = periodoY, Separacion = separacion, Margen = margen
        },
        Svg = Exportadores.ASvg(r, true, defectos, periodoX, periodoY)
    };

    /// <summary>Reconstruye el resultado del motor a partir de lo guardado (para el dibujo y la exportacion).</summary>
    private static ResultadoTizado DesdeGeometria(DisenosTizadoGeometria g) => new()
    {
        Ancho = g.Ancho, Largo = g.Largo, AreaPiezas = g.AreaPiezas,
        Colocadas = g.Colocadas.Select(c => new Colocacion { PiezaId = c.PiezaId, Nombre = c.Nombre, Talla = c.Talla, Copia = c.Copia, Espejada = c.Espejada, Angulo = c.Angulo, Contorno = LeerContorno(c.Puntos) ?? new Poligono(Array.Empty<Punto>()), PuntoCoincidencia = LeerPunto(c.Coincidencia), ErrorCoincidenciaX = c.ErrorX, ErrorCoincidenciaY = c.ErrorY, FaseX = c.FaseX, FaseY = c.FaseY }).ToList(),
        NoColocadas = g.NoColocadas.Select(n => new NoColocada { PiezaId = n.PiezaId, Nombre = n.Nombre, Talla = n.Talla, Motivo = n.Motivo }).ToList()
    };

    private static Punto? LeerPunto(string? t)
    {
        var xy = (t ?? "").Split(',');
        return xy.Length == 2 && double.TryParse(xy[0], NumberStyles.Float, CultureInfo.InvariantCulture, out var x) && double.TryParse(xy[1], NumberStyles.Float, CultureInfo.InvariantCulture, out var y) ? new Punto(x, y) : null;
    }

    private static List<Defecto> DefectosDe(DisenosTizadoGeometria g) =>
        g.Defectos.Select(d => new Defecto { Nombre = d.Nombre, Contorno = LeerContorno(d.Puntos) ?? new Poligono(Array.Empty<Punto>()) }).Where(d => d.Contorno.EsValido).ToList();

    public Task<ApiResponse<DisenosTizadoCalculo>> CalcularAsync(DisenosTizadoCalcularDto dto, CancellationToken ct) =>
        Ejecutar(async () => (await Calcular(dto, ct)).C, nameof(CalcularAsync));

    public Task<ApiResponse<DisenosTizadoCalculo>> GuardarTizadoAsync(DisenosTizadoCalcularDto dto, string codUsuario, string codEstacion, CancellationToken ct) => Ejecutar(async () =>
    {
        // el servidor recalcula: no se guarda un resultado que mande el cliente
        var nombre = (dto.DES_TIZADO ?? "").Trim();
        if (nombre == "") throw new ValidacionException("El tizado necesita un nombre.");
        if (nombre.Length > 100) throw new ValidacionException("El nombre del tizado no puede pasar de 100 caracteres.");
        var (c, tallas) = await Calcular(dto, ct);
        if (tallas.Length > 200) throw new ValidacionException("Demasiadas tallas para guardar en un tizado.");
        var json = JsonSerializer.Serialize(c.Geometria);
        c.NUM_SECUENCIA = await _repo.GuardarTizadoAsync(dto.COD_DISENO, dto.NUM_VERSION, nombre, dto, tallas, c.NUM_PRENDAS, c.NUM_PIEZAS, c.NUM_NO_COLOCADAS, c.VAL_LARGO, c.VAL_EFICIENCIA, c.VAL_CONSUMO_PRENDA, json, codUsuario, codEstacion);
        return c;
    }, nameof(GuardarTizadoAsync));

    public Task<ApiResponse<List<DisenosTizadoResumen>>> TizadosAsync(string codDiseno, int numVersion) => Ejecutar(() => _repo.TizadosAsync(codDiseno, numVersion), nameof(TizadosAsync));

    private async Task<(DisenosTizadoFila F, DisenosTizadoGeometria G)> Leer(string codDiseno, int numVersion, int numSecuencia)
    {
        var f = await _repo.TizadoAsync(codDiseno, numVersion, numSecuencia);
        var g = JsonSerializer.Deserialize<DisenosTizadoGeometria>(f.DES_RESULTADO) ?? new DisenosTizadoGeometria();
        return (f, g);
    }

    /// <summary>Los datos con que se calculo un tizado guardado: los de la fila; los que el JSON antiguo no traia se toman de la fila.</summary>
    private static DisenosTizadoGeometria Completar(DisenosTizadoFila f, DisenosTizadoGeometria g)
    {
        if (g.Separacion == 0 && g.Margen == 0) { g.Separacion = (double)f.VAL_SEPARACION; g.Margen = (double)f.VAL_MARGEN; }
        if (g.Ancho <= 0) g.Ancho = (double)f.VAL_ANCHO_TELA;
        return g;
    }

    private static DisenosTizadoCalculo ACalculo(DisenosTizadoFila f, DisenosTizadoGeometria g) => new()
    {
        NUM_SECUENCIA = f.NUM_SECUENCIA, VAL_ANCHO_TELA = (double)f.VAL_ANCHO_TELA, VAL_LARGO = (double)f.VAL_LARGO, VAL_EFICIENCIA = (double)f.VAL_EFICIENCIA, VAL_CONSUMO_PRENDA = (double?)f.VAL_CONSUMO_PRENDA,
        NUM_PRENDAS = f.NUM_PRENDAS, NUM_PIEZAS = f.NUM_PIEZAS, NUM_NO_COLOCADAS = f.NUM_NO_COLOCADAS, Geometria = g, Svg = Exportadores.ASvg(DesdeGeometria(g), true, DefectosDe(g), g.PeriodoX, g.PeriodoY)
    };

    public Task<ApiResponse<DisenosTizadoCalculo>> TizadoAsync(string codDiseno, int numVersion, int numSecuencia) => Ejecutar(async () =>
    {
        var (f, g) = await Leer(codDiseno, numVersion, numSecuencia);
        return ACalculo(f, Completar(f, g));
    }, nameof(TizadoAsync));

    public Task<ApiResponse<string>> TizadoDxfAsync(string codDiseno, int numVersion, int numSecuencia) => Ejecutar(async () =>
    {
        var (_, g) = await Leer(codDiseno, numVersion, numSecuencia);
        return Exportadores.ADxf(DesdeGeometria(g), DefectosDe(g));
    }, nameof(TizadoDxfAsync));

    public Task<ApiResponse<DisenosTizadoEliminarResultado>> EliminarTizadoAsync(DisenosMoldeClaveDto clave) =>
        Ejecutar(async () => new DisenosTizadoEliminarResultado { ELIMINADOS = await _repo.EliminarTizadoAsync(clave) }, nameof(EliminarTizadoAsync));

    // ---------- ajuste manual de un tizado guardado (etapa G) ----------
    /// <summary>
    /// Guarda un tizado nuevo a partir de otro ajustado a mano. El servidor no se fia del cliente: exige las mismas piezas, que cada una sea la original movida
    /// (giro, espejo y traslado, sin deformarla) y que no se pisen, no salgan de la tela ni pisen un defecto; el largo y el aprovechamiento los recalcula el.
    /// </summary>
    public Task<ApiResponse<DisenosTizadoCalculo>> AjustarTizadoAsync(DisenosTizadoAjusteDto dto, string codUsuario, string codEstacion) => Ejecutar(async () =>
    {
        var nombre = (dto.DES_TIZADO ?? "").Trim();
        if (nombre == "") throw new ValidacionException("El tizado ajustado necesita un nombre.");
        if (nombre.Length > 100) throw new ValidacionException("El nombre del tizado no puede pasar de 100 caracteres.");
        if (dto.Piezas is null || dto.Piezas.Count == 0) throw new ValidacionException("No hay piezas para guardar.");
        if (dto.Piezas.Count > MaxPiezas) throw new ValidacionException($"El tizado tiene mas de {MaxPiezas} piezas.");
        var (f, g) = await Leer(dto.COD_DISENO, dto.NUM_VERSION, dto.NUM_SECUENCIA);
        Completar(f, g);
        var originales = g.Colocadas.ToDictionary(c => (c.PiezaId, c.Copia));
        if (originales.Count != g.Colocadas.Count) throw new ValidacionException("El tizado original tiene piezas repetidas: no se puede ajustar.");
        if (dto.Piezas.Count != originales.Count || dto.Piezas.Select(p => (p.PiezaId, p.Copia)).Distinct().Count() != dto.Piezas.Count || dto.Piezas.Any(p => !originales.ContainsKey((p.PiezaId, p.Copia))))
            throw new ValidacionException("El ajuste debe llevar exactamente las mismas piezas del tizado original, cada una una sola vez.");

        var nuevas = new List<Colocacion>(); var piezasGuardar = new List<DisenosTizadoPieza>();
        foreach (var o in g.Colocadas)                                  // se conserva el orden del original
        {
            var p = dto.Piezas.First(x => x.PiezaId == o.PiezaId && x.Copia == o.Copia);
            var etiqueta = $"{o.Nombre} {o.Talla} #{o.Copia}";
            var polyO = LeerContorno(o.Puntos) ?? throw new ValidacionException($"La pieza {etiqueta} del tizado original no tiene un contorno valido.");
            var polyN = LeerContorno(p.Puntos) ?? throw new ValidacionException($"El contorno de la pieza {etiqueta} no es valido.");
            if (!Ajuste.EsCongruente(polyO, polyN)) throw new ValidacionException($"La pieza {etiqueta} fue deformada: solo se puede mover, girar y espejar.");
            Punto? pcO = LeerPunto(o.Coincidencia), pcN = LeerPunto(p.Coincidencia);
            if (pcO is not null && (pcN is null || !Ajuste.PuntoCongruente(polyO, pcO.Value, polyN, pcN.Value))) throw new ValidacionException($"El punto de coincidencia de la pieza {etiqueta} no acompana a la pieza.");
            if (pcO is null) pcN = null;
            if (double.IsNaN(p.Angulo) || double.IsInfinity(p.Angulo)) throw new ValidacionException($"El giro de la pieza {etiqueta} no es valido.");
            nuevas.Add(new Colocacion { PiezaId = o.PiezaId, Nombre = o.Nombre, Talla = o.Talla, Copia = o.Copia, Espejada = p.Espejada, Angulo = ((p.Angulo % 360) + 360) % 360, Contorno = polyN, PuntoCoincidencia = pcN, FaseX = o.FaseX, FaseY = o.FaseY });
        }
        var reglas = new ParametrosAjuste(g).Reglas;
        var problemas = Ajuste.Validar(nuevas, reglas).Where(x => x.Gravedad == GravedadAjuste.Error).ToList();
        if (problemas.Count > 0) throw new ValidacionException("El ajuste no es valido: " + string.Join(" ", problemas.Take(5).Select(x => x.Mensaje)) + (problemas.Count > 5 ? $" (y {problemas.Count - 5} problema(s) mas)" : ""));

        var (largo, area, eficiencia) = Ajuste.Medidas(nuevas, reglas);
        double? consumo = f.NUM_PRENDAS > 0 && largo > 0 ? Math.Round(largo / 1000.0 / f.NUM_PRENDAS, 4) : null;
        largo = Math.Round(largo, 1); eficiencia = Math.Round(eficiencia, 4);
        var g2 = new DisenosTizadoGeometria
        {
            Ancho = g.Ancho, Largo = largo, AreaPiezas = Math.Round(area, 1), NoColocadas = g.NoColocadas, Defectos = g.Defectos, PeriodoX = g.PeriodoX, PeriodoY = g.PeriodoY, Separacion = g.Separacion, Margen = g.Margen, Ajustado = true,
            Colocadas = nuevas.Select(c =>
            {
                var (ex, ey) = Ajuste.Desvio(c, reglas);
                return new DisenosTizadoPieza { PiezaId = c.PiezaId, Nombre = c.Nombre, Talla = c.Talla, Copia = c.Copia, Espejada = c.Espejada, Angulo = c.Angulo, Puntos = AFormato(c.Contorno.Puntos),
                    Coincidencia = c.PuntoCoincidencia is { } q ? N(q.X) + "," + N(q.Y) : null, ErrorX = ex is { } a ? Math.Round(a, 4) : null, ErrorY = ey is { } b ? Math.Round(b, 4) : null, FaseX = c.FaseX, FaseY = c.FaseY };
            }).ToList()
        };
        var parametros = new DisenosTizadoCalcularDto { COD_DISENO = dto.COD_DISENO, NUM_VERSION = dto.NUM_VERSION, VAL_ANCHO_TELA = (double)f.VAL_ANCHO_TELA, VAL_SEPARACION = (double)f.VAL_SEPARACION, VAL_MARGEN = (double)f.VAL_MARGEN, VAL_CELDA = (double)f.VAL_CELDA, NUM_INTENTOS = f.NUM_INTENTOS };
        var nuevo = await _repo.GuardarTizadoAsync(dto.COD_DISENO, dto.NUM_VERSION, nombre, parametros, f.DES_TALLAS, f.NUM_PRENDAS, f.NUM_PIEZAS, f.NUM_NO_COLOCADAS, largo, eficiencia, consumo, JsonSerializer.Serialize(g2), codUsuario, codEstacion);
        var fila = new DisenosTizadoFila { COD_DISENO = dto.COD_DISENO, NUM_VERSION = dto.NUM_VERSION, NUM_SECUENCIA = nuevo, DES_TIZADO = nombre, VAL_ANCHO_TELA = f.VAL_ANCHO_TELA, VAL_LARGO = (decimal)largo, VAL_EFICIENCIA = (decimal)eficiencia, VAL_CONSUMO_PRENDA = consumo is { } cc ? (decimal)cc : null, NUM_PRENDAS = f.NUM_PRENDAS, NUM_PIEZAS = f.NUM_PIEZAS, NUM_NO_COLOCADAS = f.NUM_NO_COLOCADAS };
        return ACalculo(fila, g2);
    }, nameof(AjustarTizadoAsync));

    /// <summary>Las reglas de validacion de un tizado guardado.</summary>
    private sealed class ParametrosAjuste
    {
        public ReglasAjuste Reglas { get; }
        public ParametrosAjuste(DisenosTizadoGeometria g) => Reglas = new ReglasAjuste { AnchoTela = g.Ancho, Separacion = g.Separacion, Margen = g.Margen, PeriodoX = g.PeriodoX, PeriodoY = g.PeriodoY, Defectos = DefectosDe(g) };
    }

    // ---------- gradacion de tallas (script 117) ----------
    public const int MaxTallasGradacion = 20;

    public Task<ApiResponse<List<DisenosGradacionRegla>>> ReglasGradacionAsync(string codDiseno, int numVersion) => Ejecutar(() => _repo.ReglasGradacionAsync(codDiseno, numVersion), nameof(ReglasGradacionAsync));

    /// <summary>Calcula el contorno de cada talla destino a partir del molde base. Lanza ValidacionException con el motivo y la talla.</summary>
    private static List<(DisenosGradacionTalla T, Poligono P, List<DeltaPunto> D)> Gradar(DisenosMolde baseM, DisenosGradacionDto dto)
    {
        if (dto.Tallas is null || dto.Tallas.Count == 0) throw new ValidacionException("Indica al menos una talla destino.");
        if (dto.Tallas.Count > MaxTallasGradacion) throw new ValidacionException($"Se pueden gradar hasta {MaxTallasGradacion} tallas por vez.");
        var basePoli = LeerContorno(baseM.DES_CONTORNO) ?? throw new ValidacionException($"El contorno del molde base {baseM.DES_MOLDE} no es valido.");
        var nombres = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var res = new List<(DisenosGradacionTalla, Poligono, List<DeltaPunto>)>();
        foreach (var t in dto.Tallas)
        {
            var talla = (t.DES_TALLA ?? "").Trim();
            if (talla == "") throw new ValidacionException("Cada talla destino necesita un nombre.");
            if (talla.Length > 20) throw new ValidacionException($"La talla {talla} supera los 20 caracteres.");
            if (talla.Equals((baseM.DES_TALLA ?? "").Trim(), StringComparison.OrdinalIgnoreCase)) throw new ValidacionException($"La talla {talla} es la misma del molde base: elige otra.");
            if (!nombres.Add(talla)) throw new ValidacionException($"La talla {talla} viene repetida.");
            List<DeltaPunto> d;
            try
            {
                if (t.DELTAS is { Count: > 0 })
                {
                    if (t.DELTAS.Any(x => x is null || x.Length != 2)) throw new ArgumentException("Cada regla por punto debe ser un par [dx, dy].");
                    d = t.DELTAS.Select(x => new DeltaPunto(x[0], x[1])).ToList();
                }
                else
                {
                    if (t.VAL_INC_X is null && t.VAL_INC_Y is null) throw new ArgumentException("Indica los incrementos de largo y ancho o las reglas por punto.");
                    var ix = t.VAL_INC_X ?? 0; var iy = t.VAL_INC_Y ?? 0;
                    if (Math.Abs(ix) > 5000 || Math.Abs(iy) > 5000 || double.IsNaN(ix) || double.IsNaN(iy)) throw new ArgumentException("Los incrementos deben estar entre -5000 y 5000 mm.");
                    d = Gradacion.DeltasDesdeIncrementos(basePoli, ix, iy);
                }
                res.Add((t, Gradacion.PorReglas(basePoli, d), d));
            }
            catch (ArgumentException ex) { throw new ValidacionException($"Talla {talla}: {ex.Message}"); }
        }
        return res;
    }

    private async Task<DisenosMolde> Base(DisenosGradacionDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.COD_DISENO) || dto.NUM_VERSION <= 0) throw new ValidacionException("Falta el diseno o la version.");
        var moldes = await _repo.MoldesAsync(dto.COD_DISENO, dto.NUM_VERSION);
        return moldes.FirstOrDefault(m => m.NUM_SECUENCIA == dto.NUM_SECUENCIA) ?? throw new ValidacionException($"El molde base {dto.NUM_SECUENCIA} no existe en esta version.");
    }

    public Task<ApiResponse<DisenosGradacionPrevia>> PrevisualizarGradacionAsync(DisenosGradacionDto dto) => Ejecutar(async () =>
    {
        var b = await Base(dto);
        var r = new DisenosGradacionPrevia { NUM_SECUENCIA = b.NUM_SECUENCIA, DES_MOLDE = b.DES_MOLDE, DES_TALLA_BASE = b.DES_TALLA, DES_CONTORNO_BASE = b.DES_CONTORNO };
        foreach (var (t, p, d) in Gradar(b, dto))
        {
            var n = p.AlOrigen();
            r.Tallas.Add(new DisenosGradacionVista { DES_TALLA = t.DES_TALLA.Trim(), DES_CONTORNO = AFormato(n.Puntos), VAL_AREA = (decimal)Math.Round(n.Area, 3), VAL_LARGO = (decimal)Math.Round(n.Ancho, 3), VAL_ALTO = (decimal)Math.Round(n.Alto, 3), DELTAS = d.Select(x => new[] { x.Dx, x.Dy }).ToList() });
        }
        return r;
    }, nameof(PrevisualizarGradacionAsync));

    /// <summary>Crea (o actualiza, si ya hay un molde con el mismo nombre y la talla destino) el molde de cada talla y guarda las reglas.</summary>
    public Task<ApiResponse<DisenosGradacionResultado>> GenerarGradacionAsync(DisenosGradacionDto dto, string codUsuario, string codEstacion) => Ejecutar(async () =>
    {
        var b = await Base(dto);
        var calculadas = Gradar(b, dto);
        var moldes = await _repo.MoldesAsync(dto.COD_DISENO, dto.NUM_VERSION);
        var filas = new List<object>(); int creados = 0, actualizados = 0;
        foreach (var (t, p, _) in calculadas)
        {
            var talla = t.DES_TALLA.Trim();
            var existente = moldes.FirstOrDefault(m => m.NUM_SECUENCIA != b.NUM_SECUENCIA && string.Equals(m.DES_MOLDE, b.DES_MOLDE, StringComparison.OrdinalIgnoreCase) && string.Equals((m.DES_TALLA ?? "").Trim(), talla, StringComparison.OrdinalIgnoreCase));
            if (existente is null) creados++; else actualizados++;
            var n = p.AlOrigen();
            filas.Add(new
            {
                NUM_SECUENCIA = existente?.NUM_SECUENCIA, b.DES_MOLDE, DES_TALLA = existente?.DES_TALLA ?? talla, b.NUM_CANTIDAD, b.NUM_CANTIDAD_ESPEJADA, b.DES_ROTACIONES,
                DES_CONTORNO = AFormato(n.Puntos), VAL_AREA = Math.Round(n.Area, 3), VAL_LARGO = Math.Round(n.Ancho, 3), VAL_ALTO = Math.Round(n.Alto, 3),
                // coincidencia de rayas: se hereda del base con el punto en la misma posicion relativa de la caja (revisalo si la talla cambia mucho)
                b.FLG_COINCIDE_X, b.FLG_COINCIDE_Y,
                VAL_PUNTO_X = b.VAL_PUNTO_X is { } bx && b.VAL_LARGO is > 0 ? Math.Round((double)bx / (double)b.VAL_LARGO.Value * n.Ancho, 3) : (double?)null,
                VAL_PUNTO_Y = b.VAL_PUNTO_Y is { } by && b.VAL_ALTO is > 0 ? Math.Round((double)by / (double)b.VAL_ALTO.Value * n.Alto, 3) : (double?)null,
                b.VAL_FASE_X, b.VAL_FASE_Y
            });
        }
        var secuencias = await _repo.GuardarMoldesAsync(dto.COD_DISENO, dto.NUM_VERSION, filas, codUsuario, codEstacion);
        try
        {
            await _repo.GuardarReglasGradacionAsync(dto.COD_DISENO, dto.NUM_VERSION, calculadas.Select(c => new
            {
                NUM_SECUENCIA = b.NUM_SECUENCIA, DES_TALLA = c.T.DES_TALLA.Trim(), VAL_INC_X = c.T.VAL_INC_X, VAL_INC_Y = c.T.VAL_INC_Y,
                DES_DELTAS = c.D.Select(x => new[] { x.Dx, x.Dy }).ToList()
            }), codUsuario, codEstacion);
        }
        catch (SqlException ex) when (EsValidacion(ex)) { throw new ValidacionException("Los moldes se generaron, pero no se pudieron guardar las reglas de gradacion: " + ex.Message); }
        return new DisenosGradacionResultado { Secuencias = secuencias, CREADOS = creados, ACTUALIZADOS = actualizados };
    }, nameof(GenerarGradacionAsync));

    public Task<ApiResponse<DisenosGradacionEliminarResultado>> EliminarReglaGradacionAsync(DisenosGradacionClaveDto clave) =>
        Ejecutar(async () => new DisenosGradacionEliminarResultado { ELIMINADOS = await _repo.EliminarReglaGradacionAsync(clave) }, nameof(EliminarReglaGradacionAsync));
}
