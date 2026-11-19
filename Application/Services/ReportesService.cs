using System.Globalization;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Application.Reportes;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Reportes;

namespace KonSolutionsApi.Application.Services;

/// <summary>Reporteador: configuración (fuentes, diseños, botones, plantillas) y ejecución.
///
/// Ejecutar un reporte:
///   1. PROC_REPORTES_DEFINICION trae el diseño, la fuente y de dónde sale cada parámetro. Por botón valida, con el
///      rol, que el usuario pueda usarlo (el permiso del reporte es el del botón).
///   2. Se resuelve el valor de cada parámetro según su origen (fila, diálogo, fijo, sistema) y se convierte a su tipo.
///   3. Se ejecuta el SP de la fuente (el nombre sale de la definición, nunca del navegador).
///   4. El motor arma el archivo en el formato pedido. Un diseño libre con subinformes (PDF o Excel con formato): antes se
///      lee el diseño de cada subinforme, el motor dice con qué valores se imprime cada vez y se ejecuta cada combinación
///      distinta una sola vez (con un tope).
///   5. Por botón, si el botón tiene una ruta, se guarda una copia del archivo ahí (ReporteCopiaArchivo). Si no se puede,
///      el archivo sale igual y DES_AVISO_GUARDADO dice por qué.
/// Los errores de los SP (THROW 50xxx) y los de validación vuelven al usuario tal cual, sin pasar por el log.</summary>
public class ReportesService : IReportesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ReportesService);
    private const int TiempoPorDefecto = 120;
    private const int MaxBytesPlantilla = 5 * 1024 * 1024;
    private const int MaxBytesImagen = 2 * 1024 * 1024;

    private readonly IReportesRepository _repo;
    private readonly ErrorLogService _log;
    private readonly int _maxFilas;
    private readonly int _maxFilasPdf;
    private readonly int _maxSubinformes;

    public ReportesService(IReportesRepository repo, ErrorLogService log, IConfiguration config)
    {
        _repo = repo;
        _log = log;
        _maxFilas = config.GetValue<int?>("Reportes:MaxFilas") ?? 100_000;
        _maxFilasPdf = config.GetValue<int?>("Reportes:MaxFilasPdf") ?? 20_000;
        _maxSubinformes = config.GetValue<int?>("Reportes:MaxSubinformes") ?? 500;
    }

    // ── Envoltura común ─────────────────────────────────────────────────────────
    private async Task<ApiResponse<T>> Ejecutar<T>(Func<Task<T>> accion, string metodo, Func<T, int>? total = null)
    {
        try
        {
            var d = await accion();
            return ApiResponse<T>.Ok(d, total: total?.Invoke(d) ?? 0);
        }
        catch (SqlException ex) when (ex.Number >= 50000)
        {
            return ApiResponse<T>.Fail(ex.Message);
        }
        catch (SqlException ex) when (ex.Number == -2)
        {
            return ApiResponse<T>.Fail("El reporte tardó más de lo permitido. Acota los filtros o sube el tiempo máximo de la fuente.");
        }
        catch (SqlException ex) when (ex.Number == 201)
        {
            // "El procedimiento espera el parámetro @X, que no se proporcionó"
            return ApiResponse<T>.Fail($"Falta un dato obligatorio para el reporte: {ex.Message}");
        }
        catch (Exception ex) when (ex is ReporteLimiteException or FormatException or ArgumentException)
        {
            return ApiResponse<T>.Fail(ex.Message);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, metodo);
            return ApiResponse<T>.Fail(ex.Message);
        }
    }

    private static string? Vacio(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();
    private static string Formato(string? f) => (f ?? "").Trim().ToUpperInvariant();

    // ── Catálogo ────────────────────────────────────────────────────────────────
    public Task<ApiResponse<List<ReporteProcedimiento>>> ProcedimientosAsync(string? buscar)
        => Ejecutar(() => _repo.ProcedimientosAsync(buscar), nameof(ProcedimientosAsync), d => d.Count);

    public Task<ApiResponse<List<ReporteParametroSp>>> ParametrosSpAsync(string? esquema, string? procedimiento)
    {
        if (string.IsNullOrWhiteSpace(procedimiento))
            return Task.FromResult(ApiResponse<List<ReporteParametroSp>>.Fail("Elige el procedimiento."));
        return Ejecutar(() => _repo.ParametrosSpAsync(Vacio(esquema) ?? "dbo", procedimiento.Trim()), nameof(ParametrosSpAsync), d => d.Count);
    }

    public Task<ApiResponse<List<ReporteEtiqueta>>> EtiquetasAsync(List<string>? columnas)
        => Ejecutar(() => _repo.EtiquetasAsync(columnas ?? new()), nameof(EtiquetasAsync), d => d.Count);

    /// <summary>Ejecuta el SP con valores de ejemplo y devuelve, por resultado, sus columnas (con etiqueta, formato y
    /// alineación sugeridos) y las primeras filas. Los tipos de los parámetros salen del SP, no de lo que mande la pantalla.</summary>
    public Task<ApiResponse<ReportePrueba>> ProbarAsync(ReportePruebaDto dto, string codUsuario, string? codRol, string codEstacion)
        => Ejecutar(async () =>
        {
            var esquema = Vacio(dto.DES_ESQUEMA) ?? "dbo";
            var proc = Vacio(dto.DES_PROCEDIMIENTO) ?? throw new ArgumentException("Elige el procedimiento.");
            var declarados = await _repo.ParametrosSpAsync(esquema, proc);
            var lista = new List<ReporteParametroEjecucion>();
            foreach (var d in declarados.Where(x => !x.FLG_SALIDA))
            {
                var campo = ReporteTexto.SinArroba(d.DES_PARAMETRO);
                var conf = dto.Parametros.FirstOrDefault(p => string.Equals(p.DES_PARAMETRO, d.DES_PARAMETRO, StringComparison.OrdinalIgnoreCase));
                string? texto = conf is { FLG_SISTEMA: true } ? Sistema(conf, codUsuario, codRol, codEstacion) : Buscar(dto.Valores, campo);
                if (string.IsNullOrWhiteSpace(texto) && conf is not null && !conf.FLG_SISTEMA)
                    texto = ReporteValores.Defecto(conf.COD_VALOR_DEFECTO, conf.DES_VALOR_DEFECTO, DateTime.Now);
                var valor = ReporteValores.Convertir(texto, d.DES_TIPO_DATO, conf?.DES_ETIQUETA ?? campo);
                if (valor is not null) lista.Add(new(d.DES_PARAMETRO, d.DES_TIPO_DATO, valor, d.NUM_LONGITUD, d.NUM_PRECISION, d.NUM_ESCALA));
            }
            var inicio = DateTime.Now;
            var datos = await _repo.EjecutarAsync(esquema, proc, lista, Math.Clamp(dto.NUM_TIEMPO_MAXIMO ?? TiempoPorDefecto, 5, 1800), _maxFilas);
            var segundos = (DateTime.Now - inicio).TotalSeconds;
            var nombres = datos.Tablas.SelectMany(t => t.Columnas.Select(c => c.Nombre)).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
            var etiquetas = (await _repo.EtiquetasAsync(nombres)).GroupBy(e => e.DES_COLUMNA, StringComparer.OrdinalIgnoreCase)
                                                                  .ToDictionary(g => g.Key, g => g.First().DES_ETIQUETA, StringComparer.OrdinalIgnoreCase);
            var prueba = new ReportePrueba { NUM_SEGUNDOS = Math.Round(segundos, 2) };
            foreach (var t in datos.Tablas)
            {
                var r = new ReportePruebaResultado { NUM_RESULTADO = t.Numero, NUM_FILAS = t.Filas.Count };
                for (var i = 0; i < t.Columnas.Count; i++) r.Columnas.Add(Detectada(t, i, etiquetas));
                r.Muestra = t.Filas.Take(20).Select(f => t.Columnas.Select((c, i) => (string?)ReporteFormato.Texto(f[i], null, c.Tipo)).ToList()).ToList();
                prueba.Resultados.Add(r);
            }
            return prueba;
        }, nameof(ProbarAsync));

    /// <summary>Una columna que devolvió el SP, como la guarda la fuente: con la etiqueta del diccionario (o la que sale
    /// del nombre) y el formato y la alineación que van con su tipo.</summary>
    private static ReporteFuenteColumna Detectada(ReporteTabla t, int i, IReadOnlyDictionary<string, string> etiquetas)
    {
        var c = t.Columnas[i];
        var conHora = ReporteFormato.EsFecha(c.Tipo) && t.Filas.Take(500).Any(f => f[i] is DateTime d && d.TimeOfDay != TimeSpan.Zero);
        var formato = ReporteFormato.EsFecha(c.Tipo) ? (conHora ? "dd/MM/yyyy HH:mm" : "dd/MM/yyyy")
                    : ReporteFormato.EsEntero(c.Tipo) ? null
                    : ReporteFormato.EsNumerico(c.Tipo) ? "#,##0.00" : null;
        return new ReporteFuenteColumna
        {
            NUM_RESULTADO  = t.Numero,
            DES_COLUMNA    = c.Nombre,
            NUM_ORDEN      = i + 1,
            DES_TIPO_DATO  = c.TipoSql,
            DES_ETIQUETA   = etiquetas.GetValueOrDefault(c.Nombre) ?? ReporteTexto.EtiquetaDesdeNombre(c.Nombre),
            DES_FORMATO    = formato,
            COD_ALINEACION = ReporteFormato.EsNumerico(c.Tipo) ? "D" : ReporteFormato.EsFecha(c.Tipo) ? "C" : "I"
        };
    }

    /// <summary>Si el SP devolvió columnas que su fuente todavía no tiene (se le agregó un campo), las agrega a la fuente:
    /// así aparecen en el diseño sin volver a probar la fuente. Solo en los resultados que la fuente ya tiene, y sin tocar
    /// las columnas que ya estaban. Si no se puede grabar, el reporte sale igual (queda en el log).
    /// Devuelve las agregadas como "COLUMNA (resultado N)".</summary>
    private async Task<List<string>> ColumnasNuevas(ReporteDefinicion def, ReporteDatos datos)
    {
        var lista = new List<string>();
        if (def.Cabecera.NUM_FUENTE is not { } numFuente) return lista;
        var nuevas = new List<(ReporteTabla Tabla, int Indice)>();
        foreach (var t in datos.Tablas.Where(t => def.Resultados.Any(r => r.NUM_RESULTADO == t.Numero)))
            for (var i = 0; i < t.Columnas.Count; i++)
            {
                var nombre = t.Columnas[i].Nombre;
                if (string.IsNullOrWhiteSpace(nombre)) continue;
                var ya = def.Columnas.Any(c => c.NUM_RESULTADO == t.Numero && string.Equals(c.DES_COLUMNA, nombre, StringComparison.OrdinalIgnoreCase))
                         || nuevas.Any(n => n.Tabla == t && string.Equals(n.Tabla.Columnas[n.Indice].Nombre, nombre, StringComparison.OrdinalIgnoreCase));
                if (!ya) nuevas.Add((t, i));
            }
        if (nuevas.Count == 0) return lista;
        try
        {
            var nombres = nuevas.Select(n => n.Tabla.Columnas[n.Indice].Nombre).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
            var etiquetas = (await _repo.EtiquetasAsync(nombres)).GroupBy(e => e.DES_COLUMNA, StringComparer.OrdinalIgnoreCase)
                                                                  .ToDictionary(g => g.Key, g => g.First().DES_ETIQUETA, StringComparer.OrdinalIgnoreCase);
            var columnas = nuevas.Select(n => Detectada(n.Tabla, n.Indice, etiquetas)).ToList();
            await _repo.FuenteColumnasAgregarAsync(numFuente, columnas);
            // En este mismo reporte ya se formatean como las demás.
            def.Columnas.AddRange(columnas);
            lista.AddRange(columnas.Select(c => $"{c.DES_COLUMNA} (resultado {c.NUM_RESULTADO})"));
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(ColumnasNuevas));
        }
        return lista;
    }

    // ── Fuentes ─────────────────────────────────────────────────────────────────
    public Task<ApiResponse<List<ReporteFuente>>> FuentesAsync()
        => Ejecutar(() => _repo.FuentesAsync(), nameof(FuentesAsync), d => d.Count);

    public Task<ApiResponse<ReporteFuenteDetalle>> FuenteAsync(int numFuente)
        => Ejecutar(async () => await _repo.FuenteAsync(numFuente) ?? throw new ArgumentException("La fuente no existe."), nameof(FuenteAsync));

    public Task<ApiResponse<int>> FuenteGrabarAsync(ReporteFuenteDetalle d, string codUsuario, string codEstacion)
    {
        if (string.IsNullOrWhiteSpace(d.Fuente.DES_FUENTE)) return Task.FromResult(ApiResponse<int>.Fail("Falta la descripción de la fuente."));
        if (string.IsNullOrWhiteSpace(d.Fuente.DES_PROCEDIMIENTO)) return Task.FromResult(ApiResponse<int>.Fail("Elige el procedimiento de la fuente."));
        if (d.Resultados.Count == 0) return Task.FromResult(ApiResponse<int>.Fail("Prueba el procedimiento para detectar sus resultados antes de grabar."));
        return Ejecutar(() => _repo.FuenteGrabarAsync(d, codUsuario, codEstacion), nameof(FuenteGrabarAsync));
    }

    public Task<ApiResponse<bool>> FuenteEliminarAsync(int numFuente)
        => Ejecutar(async () => { await _repo.FuenteEliminarAsync(numFuente); return true; }, nameof(FuenteEliminarAsync));

    // ── Diseños ─────────────────────────────────────────────────────────────────
    public Task<ApiResponse<List<ReporteDiseno>>> DisenosAsync(int? numFuente)
        => Ejecutar(() => _repo.DisenosAsync(numFuente), nameof(DisenosAsync), d => d.Count);

    public Task<ApiResponse<ReporteDisenoDetalle>> DisenoAsync(int numDiseno)
        => Ejecutar(async () => await _repo.DisenoAsync(numDiseno) ?? throw new ArgumentException("El diseño no existe."), nameof(DisenoAsync));

    public Task<ApiResponse<int>> DisenoGrabarAsync(ReporteDisenoDetalle d, string codUsuario, string codEstacion)
    {
        if (string.IsNullOrWhiteSpace(d.Diseno.DES_DISENO)) return Task.FromResult(ApiResponse<int>.Fail("Falta la descripción del diseño."));
        if (d.Diseno.NUM_FUENTE is null) return Task.FromResult(ApiResponse<int>.Fail("Elige la fuente de datos del diseño."));
        // Un diseño libre muestra lo que tiene su lienzo: puede no tener columnas.
        if (!EsLibre(d.Diseno.COD_TIPO_DISENO) && !d.Elementos.Any(e => e.FLG_VISIBLE))
            return Task.FromResult(ApiResponse<int>.Fail("El diseño tiene que mostrar al menos una columna."));
        foreach (var o in d.Lienzo?.Objetos.Where(o => o.IMG_IMAGEN is { Length: > 0 }) ?? Enumerable.Empty<ReporteLienzoObjeto>())
        {
            if (o.IMG_IMAGEN!.Length > MaxBytesImagen) return Task.FromResult(ApiResponse<int>.Fail("Una imagen del lienzo pesa más de 2 MB."));
            if (!ReporteImagen.EsSoportada(o.IMG_IMAGEN)) return Task.FromResult(ApiResponse<int>.Fail("Una imagen del lienzo no es PNG, JPEG, GIF ni BMP."));
        }
        return Ejecutar(async () =>
        {
            if (EsLibre(d.Diseno.COD_TIPO_DISENO) && d.Lienzo is { } l) await ValidarFormulas(d.Diseno.NUM_FUENTE!.Value, l);
            return await Grabar(d, codUsuario, codEstacion);
        }, nameof(DisenoGrabarAsync));
    }

    private static bool EsLibre(string? tipo) => string.Equals(tipo, "BAN", StringComparison.OrdinalIgnoreCase);

    /// <summary>Un lienzo con una fórmula o condición que no está bien no se graba: se devuelve qué corregir.</summary>
    private async Task ValidarFormulas(int numFuente, ReporteLienzo l)
    {
        var usa = l.Formulas.Count > 0 || l.Condiciones.Count > 0
                  || l.Objetos.Any(o => o.COD_TIPO_OBJETO == "FOR" || (o.COD_TIPO_OBJETO == "TXT" && (o.DES_ORIGEN ?? "").Contains('{'))
                                        || (o.DES_ENLACES ?? "").Contains('{'));
        if (!usa) return;
        var fuente = await _repo.FuenteAsync(numFuente) ?? throw new ArgumentException("La fuente del diseño no existe.");
        var esquema = ReporteFormulasDiseno.Esquema(fuente.Columnas, fuente.Parametros, fuente.Resultados, l.Formulas, l.Grupos.Count);
        var errores = ReporteFormulasDiseno.Validar(l, esquema);
        if (errores.Count == 0) return;
        var mas = errores.Count > 8 ? $"\n… y {errores.Count - 8} más." : "";
        throw new ArgumentException("No se grabó: hay fórmulas con errores.\n" + string.Join("\n", errores.Take(8).Select(e => "• " + e)) + mas);
    }

    // ── Fórmulas del diseño libre ───────────────────────────────────────────────
    public ApiResponse<List<ReporteFormulaFuncion>> FormulaFunciones()
        => ApiResponse<List<ReporteFormulaFuncion>>.Ok(ReporteFormula.Catalogo.Select(f => new ReporteFormulaFuncion
        {
            DES_GRUPO = f.Grupo, DES_NOMBRE = f.Nombre, DES_FIRMA = f.Firma, DES_AYUDA = f.Ayuda, DES_EJEMPLO = f.Ejemplo
        }).ToList());

    /// <summary>Comprueba una fórmula o condición del editor y, si se pide, la calcula con la primera fila.</summary>
    public Task<ApiResponse<ReporteFormulaResultado>> FormulaComprobarAsync(ReporteFormulaComprobarDto dto, string codUsuario, string? codRol, string codEstacion)
        => Ejecutar(async () =>
        {
            var fuente = await _repo.FuenteAsync(dto.NUM_FUENTE) ?? throw new ArgumentException("Elige la fuente de datos del diseño.");
            var nombre = Vacio(dto.DES_NOMBRE);
            // Las fórmulas del diseño, con la que se edita tal como está escrita ahora.
            var formulas = dto.Formulas.Where(f => !string.Equals(f.DES_NOMBRE?.Trim(), nombre, StringComparison.OrdinalIgnoreCase)).ToList();
            if (nombre is not null) formulas.Add(new ReporteLienzoFormula { DES_NOMBRE = nombre, DES_FORMULA = dto.DES_FORMULA ?? "" });
            var grupos = dto.Grupos.Where(g => !string.IsNullOrWhiteSpace(g)).Select(g => g.Trim()).ToList();
            var esquema = ReporteFormulasDiseno.Esquema(fuente.Columnas, fuente.Parametros, fuente.Resultados, formulas, grupos.Count);
            char? elemento = Vacio(dto.COD_ELEMENTO) is { } e ? char.ToUpperInvariant(e[0]) : null;
            var errores = ReporteFormulasDiseno.Comprobar(dto.DES_FORMULA, esquema, nombre, elemento, dto.COD_PROPIEDAD, out var tipo);
            var r = new ReporteFormulaResultado
            {
                FLG_VALIDA = errores.Count == 0,
                DES_TIPO = errores.Count == 0 ? ReporteFormula.NombreTipo(tipo) : null,
                Errores = errores.Select(x => new ReporteFormulaError { NUM_LINEA = x.Linea, NUM_COLUMNA = x.Columna, DES_MENSAJE = x.Mensaje }).ToList()
            };
            if (r.FLG_VALIDA && dto.FLG_PROBAR) await ProbarFormula(r, dto.DES_FORMULA ?? "", fuente, formulas, grupos, codUsuario, codRol, codEstacion);
            return r;
        }, nameof(FormulaComprobarAsync));

    /// <summary>Ejecuta el SP de la fuente con los valores por defecto de sus parámetros y calcula la fórmula con la
    /// primera fila (ordenada por los grupos). Lo que impide probar va como aviso, sin tapar que la fórmula está bien.</summary>
    private async Task ProbarFormula(ReporteFormulaResultado r, string texto, ReporteFuenteDetalle fuente, List<ReporteLienzoFormula> formulas,
                                     List<string> grupos, string codUsuario, string? codRol, string codEstacion)
    {
        var valores = new List<ReporteValorParametro>();
        foreach (var p in fuente.Parametros.OrderBy(x => x.NUM_ORDEN))
        {
            var etiqueta = Vacio(p.DES_ETIQUETA) ?? ReporteTexto.EtiquetaDesdeNombre(p.DES_PARAMETRO);
            var t = p.FLG_SISTEMA ? Sistema(p, codUsuario, codRol, codEstacion) : ReporteValores.Defecto(p.COD_VALOR_DEFECTO, p.DES_VALOR_DEFECTO, DateTime.Now);
            if (t is null && p.COD_VALOR_DEFECTO is "USUARIO" or "ROL" or "ESTACION") t = Sistema(p, codUsuario, codRol, codEstacion);
            object? valor;
            try { valor = ReporteValores.Convertir(t, p.DES_TIPO_DATO, etiqueta); }
            catch (FormatException) { valor = null; }
            if (valor is null && p.FLG_OBLIGATORIO)
            {
                r.DES_AVISO = $"Para probarla con datos, «{etiqueta}» necesita un valor por defecto en la fuente.";
                return;
            }
            valores.Add(new ReporteValorParametro { Parametro = p, Valor = valor, Texto = ReporteValores.TextoVisible(valor, null, p) });
        }
        var f = fuente.Fuente;
        ReporteDatos datos;
        try
        {
            datos = await _repo.EjecutarAsync(Vacio(f.DES_ESQUEMA) ?? "dbo", f.DES_PROCEDIMIENTO ?? "",
                valores.Where(v => v.Valor is not null).Select(v => new ReporteParametroEjecucion(v.Parametro.DES_PARAMETRO, v.Parametro.DES_TIPO_DATO, v.Valor!,
                                                                                               v.Parametro.NUM_LONGITUD, v.Parametro.NUM_PRECISION, v.Parametro.NUM_ESCALA)),
                Math.Clamp(f.NUM_TIEMPO_MAXIMO ?? TiempoPorDefecto, 5, 1800), _maxFilas);
        }
        catch (Exception ex) when (ex is SqlException or ReporteLimiteException)
        {
            r.DES_AVISO = $"No se pudo ejecutar la fuente para probarla: {ex.Message}";
            return;
        }
        var tabla = datos.Tabla(1) ?? datos.Tablas.FirstOrDefault();
        var lienzo = new ReporteLienzo
        {
            Formulas = formulas,
            Grupos = grupos.Select((c, i) => new ReporteLienzoGrupo { NUM_NIVEL = i + 1, DES_COLUMNA = c, COD_ORDEN = "A" }).ToList()
        };
        var def = new ReporteDefinicion
        {
            Cabecera = new ReporteDefinicionCabecera { DES_DISENO = f.DES_FUENTE, COD_TIPO_DISENO = "BAN" },
            Parametros = fuente.Parametros, Resultados = fuente.Resultados, Columnas = fuente.Columnas, Lienzo = lienzo
        };
        var ctx = new ReporteContexto
        {
            Definicion = def, Datos = datos, Valores = valores, Empresa = await _repo.EmpresaAsync(codUsuario), Usuario = codUsuario, Fecha = DateTime.Now
        };
        var filas = ReporteListado.Ordenar(tabla?.Filas ?? new List<object?[]>(), tabla, ReporteBandasListado.Claves(def, tabla));
        var deGrupo = lienzo.Grupos.Where(g => tabla is not null && tabla.Indice(g.DES_COLUMNA) >= 0).ToList();
        var rangos = ReporteFormulasDatos.Rangos(ctx, tabla, filas, deGrupo);
        var lugar = new ReporteFormulaLugar { Fila = filas.Count > 0 ? 0 : -1, Grupos = filas.Count > 0 ? rangos[0] : Array.Empty<(int, int)>() };
        var v = new ReporteFormulasDatos(ctx, tabla, filas, lienzo).Evaluar(ReporteFormula.Compilar(texto), lugar);
        r.DES_VALOR = v switch
        {
            null    => "(vacío)",
            bool b  => b ? "Verdadero" : "Falso",
            _       => ReporteFormula.Formatear(v, null)
        };
        if (filas.Count == 0) r.DES_AVISO = "La fuente no trajo filas con los valores por defecto: el valor sale sin datos de la fila.";
    }

    /// <summary>Graba el diseño y, si es libre, su lienzo. Un diseño libre nuevo sin lienzo empieza con el encabezado y
    /// el pie de siempre. Si el lienzo no pasa (THROW del SP) y el diseño era nuevo, se borra para no dejarlo a medias.</summary>
    private async Task<int> Grabar(ReporteDisenoDetalle d, string codUsuario, string codEstacion)
    {
        var nuevo = d.Diseno.NUM_DISENO is null;
        var num = await _repo.DisenoGrabarAsync(d, codUsuario, codEstacion);
        if (!EsLibre(d.Diseno.COD_TIPO_DISENO) || (d.Lienzo is null && !nuevo)) return num;
        var lienzo = d.Lienzo ?? ReporteConversor.Nuevo(new ReporteDefinicion { Cabecera = ReporteBandasListado.Copiar<ReporteDefinicionCabecera>(d.Diseno) });
        try
        {
            await _repo.LienzoGrabarAsync(num, lienzo, codUsuario, codEstacion);
        }
        catch when (nuevo)
        {
            try { await _repo.DisenoEliminarAsync(num); }
            catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(Grabar)); }
            throw;
        }
        return num;
    }

    /// <summary>Convertir a bandas: un diseño libre nuevo, copia del listado o la ficha, con el lienzo que lo dibuja
    /// igual que su PDF. El original no se toca.</summary>
    public Task<ApiResponse<int>> DisenoConvertirAsync(int numDiseno, string codUsuario, string codEstacion)
        => Ejecutar(async () =>
        {
            var d = await _repo.DisenoAsync(numDiseno) ?? throw new ArgumentException("El diseño no existe.");
            if (EsLibre(d.Diseno.COD_TIPO_DISENO)) throw new ArgumentException("El diseño ya es libre (por bandas).");
            var def = await _repo.DefinicionAsync(numDiseno, null, null, null, null, codUsuario);
            return await Grabar(ReporteConversor.Convertir(def, d), codUsuario, codEstacion);
        }, nameof(DisenoConvertirAsync));

    public Task<ApiResponse<bool>> DisenoEliminarAsync(int numDiseno)
        => Ejecutar(async () => { await _repo.DisenoEliminarAsync(numDiseno); return true; }, nameof(DisenoEliminarAsync));

    /// <summary>La plantilla base de Word o Excel: el diseño con las marcas {{R1.COLUMNA}} en vez de datos.</summary>
    public Task<ApiResponse<ReporteArchivo>> PlantillaBaseAsync(int numDiseno, string formato, string codUsuario)
        => Ejecutar(async () =>
        {
            var f = Formato(formato);
            if (f is not ("DOC" or "XLS")) throw new ArgumentException("Solo Word (DOC) y Excel (XLS) tienen plantilla.");
            var def = await _repo.DefinicionAsync(numDiseno, null, null, null, null, codUsuario);
            var ctx = new ReporteContexto
            {
                Definicion = def, Datos = new ReporteDatos(), Empresa = await _repo.EmpresaAsync(codUsuario), Usuario = codUsuario,
                ModoPlantilla = true,
                Valores = def.Parametros.Select(p => new ReporteValorParametro { Parametro = p, Texto = $"{{{{@{ReporteTexto.SinArroba(p.DES_PARAMETRO)}}}}}" }).ToList()
            };
            var bytes = ReporteGenerador.Generar(ctx, f);
            return new ReporteArchivo
            {
                DES_NOMBRE_ARCHIVO = ReporteTexto.NombreArchivo($"{def.Cabecera.DES_DISENO} - plantilla", ReporteGenerador.Extension(f)),
                DES_CONTENT_TYPE = ReporteGenerador.ContentType(f),
                CONTENIDO = bytes
            };
        }, nameof(PlantillaBaseAsync));

    public Task<ApiResponse<ReporteArchivo>> PlantillaDescargarAsync(int numDiseno, string formato)
        => Ejecutar(async () =>
        {
            var f = Formato(formato);
            var (bytes, nombre) = await _repo.PlantillaAsync(numDiseno, f);
            if (bytes is null) throw new ArgumentException("El diseño no tiene plantilla propia en ese formato.");
            return new ReporteArchivo
            {
                DES_NOMBRE_ARCHIVO = string.IsNullOrWhiteSpace(nombre) ? ReporteTexto.NombreArchivo("plantilla", ReporteGenerador.Extension(f)) : nombre!,
                DES_CONTENT_TYPE = ReporteGenerador.ContentType(f),
                CONTENIDO = bytes
            };
        }, nameof(PlantillaDescargarAsync));

    /// <summary>Guarda la plantilla propia. Antes la abre y revisa sus marcas: las que no existen en la fuente vuelven
    /// como avisos (no impiden guardarla, para que el usuario decida).</summary>
    public Task<ApiResponse<ReportePlantillaResultado>> PlantillaSubirAsync(int numDiseno, string formato, ReportePlantillaDto dto, string codUsuario, string codEstacion)
        => Ejecutar(async () =>
        {
            var f = Formato(formato);
            if (f is not ("DOC" or "XLS")) throw new ArgumentException("Solo Word (DOC) y Excel (XLS) admiten plantilla propia.");
            if (dto.CONTENIDO is not { Length: > 0 }) throw new ArgumentException("El archivo está vacío.");
            if (dto.CONTENIDO.Length > MaxBytesPlantilla) throw new ArgumentException("La plantilla pesa más de 5 MB.");
            var ext = Path.GetExtension(dto.DES_NOMBRE_ARCHIVO ?? "").ToLowerInvariant();
            if ((f == "DOC" && ext != ".docx") || (f == "XLS" && ext != ".xlsx"))
                throw new ArgumentException(f == "DOC" ? "La plantilla de Word tiene que ser un .docx." : "La plantilla de Excel tiene que ser un .xlsx.");
            var def = await _repo.DefinicionAsync(numDiseno, null, null, null, null, codUsuario);
            ReportePlantillaResultado r;
            try { r = ReporteGenerador.ValidarPlantilla(def, f, dto.CONTENIDO); }
            catch (InvalidOperationException ex) { throw new ArgumentException(ex.Message); }
            await _repo.PlantillaGrabarAsync(numDiseno, f, dto.CONTENIDO, Path.GetFileName(dto.DES_NOMBRE_ARCHIVO), codUsuario, codEstacion);
            return r;
        }, nameof(PlantillaSubirAsync));

    public Task<ApiResponse<bool>> PlantillaQuitarAsync(int numDiseno, string formato, string codUsuario, string codEstacion)
        => Ejecutar(async () => { await _repo.PlantillaGrabarAsync(numDiseno, Formato(formato), null, null, codUsuario, codEstacion); return true; },
                    nameof(PlantillaQuitarAsync));

    // ── Botones ─────────────────────────────────────────────────────────────────
    public Task<ApiResponse<List<ReporteBoton>>> BotonesAsync(string? codAplicacion, string? buscar, bool soloAsignados)
        => Ejecutar(() => _repo.BotonesAsync(codAplicacion, buscar, soloAsignados), nameof(BotonesAsync), d => d.Count);

    public Task<ApiResponse<ReporteBotonDetalle>> BotonAsync(string? codAplicacion, string? codOpcion, string? codBoton, int? numDiseno)
    {
        if (Vacio(codAplicacion) is null || Vacio(codOpcion) is null || Vacio(codBoton) is null)
            return Task.FromResult(ApiResponse<ReporteBotonDetalle>.Fail("Elige el botón."));
        return Ejecutar(async () => await _repo.BotonAsync(codAplicacion!.Trim(), codOpcion!.Trim(), codBoton!.Trim(), numDiseno)
                                    ?? throw new ArgumentException("El botón no existe en la opción."), nameof(BotonAsync));
    }

    public Task<ApiResponse<bool>> BotonGrabarAsync(ReporteBotonGrabarDto dto, string codUsuario, string codEstacion)
    {
        if (Vacio(dto.COD_APLICACION) is null || Vacio(dto.COD_OPCION_APLICACION) is null || Vacio(dto.COD_BOTON_OPCION) is null)
            return Task.FromResult(ApiResponse<bool>.Fail("Elige el botón."));
        if (dto.NUM_DISENO is null) return Task.FromResult(ApiResponse<bool>.Fail("Elige el diseño que dispara el botón."));
        if (!ReporteGenerador.Formatos.Contains(Formato(dto.COD_FORMATO_DEFECTO)))
            return Task.FromResult(ApiResponse<bool>.Fail("Elige el formato por defecto."));
        dto.COD_FORMATO_DEFECTO = Formato(dto.COD_FORMATO_DEFECTO);
        dto.COD_KEY_PATHFILE = Vacio(dto.COD_KEY_PATHFILE);
        dto.DES_NOMBRE_ARCHIVO = Vacio(dto.DES_NOMBRE_ARCHIVO);
        if (dto.DES_NOMBRE_ARCHIVO is not null && dto.COD_KEY_PATHFILE is null)
            return Task.FromResult(ApiResponse<bool>.Fail("Para ponerle nombre a la copia del archivo, elige la ruta donde se guarda."));
        if (ReporteCopiaArchivo.Validar(dto.DES_NOMBRE_ARCHIVO) is { } malNombre)
            return Task.FromResult(ApiResponse<bool>.Fail(malNombre));
        return Ejecutar(async () => { await _repo.BotonGrabarAsync(dto, codUsuario, codEstacion); return true; }, nameof(BotonGrabarAsync));
    }

    public Task<ApiResponse<bool>> BotonEliminarAsync(string? codAplicacion, string? codOpcion, string? codBoton)
    {
        if (Vacio(codAplicacion) is null || Vacio(codOpcion) is null || Vacio(codBoton) is null)
            return Task.FromResult(ApiResponse<bool>.Fail("Elige el botón."));
        return Ejecutar(async () => { await _repo.BotonEliminarAsync(codAplicacion!.Trim(), codOpcion!.Trim(), codBoton!.Trim()); return true; },
                        nameof(BotonEliminarAsync));
    }

    public Task<ApiResponse<List<ReporteRuta>>> RutasAsync()
        => Ejecutar(() => _repo.RutasAsync(), nameof(RutasAsync), d => d.Count);

    // ── Ejecución ───────────────────────────────────────────────────────────────
    public Task<ApiResponse<List<ReporteOpcionBoton>>> OpcionBotonesAsync(string? codAplicacion, string? codOpcion, string? codRol)
    {
        if (Vacio(codAplicacion) is null || Vacio(codOpcion) is null || Vacio(codRol) is null)
            return Task.FromResult(ApiResponse<List<ReporteOpcionBoton>>.Ok(new()));
        return Ejecutar(() => _repo.OpcionBotonesAsync(codAplicacion!.Trim(), codOpcion!.Trim(), codRol!.Trim()), nameof(OpcionBotonesAsync), d => d.Count);
    }

    /// <summary>Lo que el diálogo necesita: parámetros (sin nombres de SP) y formatos.</summary>
    public Task<ApiResponse<ReporteDialogo>> DialogoAsync(int? numDiseno, string? codAplicacion, string? codOpcion, string? codBoton, string? codRol, string codUsuario)
        => Ejecutar(async () =>
        {
            var def = await _repo.DefinicionAsync(numDiseno, codAplicacion, codOpcion, codBoton, codRol, codUsuario);
            var d = def.Cabecera;
            return new ReporteDialogo
            {
                NUM_DISENO          = d.NUM_DISENO ?? 0,
                DES_DISENO          = d.DES_DISENO,
                COD_TIPO_DISENO     = d.COD_TIPO_DISENO,
                DES_TITULO          = d.DES_TITULO,
                COD_FORMATO_DEFECTO = d.COD_FORMATO_DEFECTO,
                FLG_ELEGIR_FORMATO  = d.FLG_ELEGIR_FORMATO,
                FLG_VISTA_PREVIA    = d.FLG_VISTA_PREVIA,
                // Un diseño libre saca Word solo con su plantilla propia.
                Formatos            = def.Formatos.Where(f => f.FLG_HABILITADO && !(EsLibre(d.COD_TIPO_DISENO) && f.COD_FORMATO == "DOC" && !f.FLG_PLANTILLA_PROPIA))
                                                  .Select(f => f.COD_FORMATO).ToList(),
                Parametros          = def.Parametros.OrderBy(p => p.NUM_ORDEN).Select(p => new ReporteDialogoParametro
                {
                    DES_PARAMETRO     = p.DES_PARAMETRO,
                    DES_CAMPO         = ReporteTexto.SinArroba(p.DES_PARAMETRO),
                    DES_ETIQUETA      = Vacio(p.DES_ETIQUETA) ?? ReporteTexto.EtiquetaDesdeNombre(p.DES_PARAMETRO),
                    DES_TIPO_DATO     = p.DES_TIPO_DATO,
                    COD_CONTROL       = p.COD_CONTROL,
                    FLG_OBLIGATORIO   = p.FLG_OBLIGATORIO,
                    COD_ORIGEN        = p.FLG_SISTEMA ? "SIS" : p.COD_ORIGEN ?? "AUT",
                    DES_CAMPO_FILA    = p.COD_ORIGEN == "FIL" ? p.DES_VALOR : null,
                    COD_VALOR_DEFECTO = p.FLG_SISTEMA ? null : p.COD_VALOR_DEFECTO,
                    DES_VALOR_DEFECTO = p.FLG_SISTEMA ? null : p.DES_VALOR_DEFECTO,
                    FLG_VISIBLE       = p.FLG_VISIBLE && !p.FLG_SISTEMA
                }).ToList()
            };
        }, nameof(DialogoAsync));

    /// <summary>Los ítems del combo de un parámetro. El SP del combo sale de la configuración guardada; recibe por
    /// nombre los valores que ya tiene el diálogo, la fila y el sistema.</summary>
    public Task<ApiResponse<List<ComboboxItem>>> ComboAsync(ReporteComboDto dto, string codUsuario, string? codRolToken, string codEstacion)
        => Ejecutar(async () =>
        {
            var rol = Vacio(dto.COD_ROL_USUARIO) ?? codRolToken;
            var def = await _repo.DefinicionAsync(dto.NUM_DISENO, dto.COD_APLICACION, dto.COD_OPCION_APLICACION, dto.COD_BOTON_OPCION, rol, codUsuario);
            var p = def.Parametros.FirstOrDefault(x => string.Equals(ReporteTexto.SinArroba(x.DES_PARAMETRO), ReporteTexto.SinArroba(dto.DES_PARAMETRO), StringComparison.OrdinalIgnoreCase))
                    ?? throw new ArgumentException("El parámetro no es del reporte.");
            if (p.COD_CONTROL != "CMB" || string.IsNullOrWhiteSpace(p.DES_COMBO_PROCEDIMIENTO))
                throw new ArgumentException("El parámetro no es un combo.");
            // Los valores que el combo puede necesitar, sin validar obligatorios (todavía se está llenando el diálogo).
            var valores = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
            foreach (var x in def.Parametros)
            {
                if (ReferenceEquals(x, p)) continue;
                var texto = Texto(x, dto.Valores, dto.Contexto, codUsuario, rol, codEstacion);
                try { valores[ReporteTexto.SinArroba(x.DES_PARAMETRO)] = ReporteValores.Convertir(texto, x.DES_TIPO_DATO, x.DES_ETIQUETA ?? x.DES_PARAMETRO); }
                catch (FormatException) { /* un filtro a medio escribir no impide cargar el combo */ }
            }
            return await _repo.ComboAsync(p.DES_COMBO_PROCEDIMIENTO!, valores, p.DES_COMBO_COLUMNA_ID, p.DES_COMBO_COLUMNA_TEXTO);
        }, nameof(ComboAsync), d => d.Count);

    public Task<ApiResponse<ReporteArchivo>> GenerarAsync(ReporteGenerarDto dto, string codUsuario, string? codRolToken, string codEstacion)
        => Ejecutar(async () =>
        {
            var formato = Formato(dto.COD_FORMATO);
            if (!ReporteGenerador.Formatos.Contains(formato)) throw new ArgumentException("Elige el formato (PDF, Word, Excel o CSV).");
            var rol = Vacio(dto.COD_ROL_USUARIO) ?? codRolToken;
            var def = await _repo.DefinicionAsync(dto.NUM_DISENO, dto.COD_APLICACION, dto.COD_OPCION_APLICACION, dto.COD_BOTON_OPCION, rol, codUsuario);
            var cab = def.Cabecera;
            if (!cab.FLG_EXISTE) throw new ArgumentException("El procedimiento de la fuente de datos ya no existe en la base.");
            var plantillaFormato = def.Formatos.FirstOrDefault(f => f.COD_FORMATO == formato);
            if (plantillaFormato is not { FLG_HABILITADO: true }) throw new ArgumentException($"Este reporte no sale en {Nombre(formato)}.");
            if (formato == "CSV" && cab.COD_TIPO_DISENO == "FIC") throw new ArgumentException("El formato CSV es solo para listados.");

            var valores = Resolver(def, dto, codUsuario, rol, codEstacion);
            var ejecucion = valores.Where(v => v.Valor is not null)
                .Select(v => new ReporteParametroEjecucion(v.Parametro.DES_PARAMETRO, v.Parametro.DES_TIPO_DATO, v.Valor!,
                                                           v.Parametro.NUM_LONGITUD, v.Parametro.NUM_PRECISION, v.Parametro.NUM_ESCALA));
            // El Excel «con formato» de un diseño libre se arma celda por celda, como el PDF: tiene el mismo tope de filas.
            var excelConFormato = formato == "XLS" && EsLibre(cab.COD_TIPO_DISENO) && plantillaFormato.FLG_CON_FORMATO && !plantillaFormato.FLG_PLANTILLA_PROPIA;
            var max = formato == "PDF" || excelConFormato ? _maxFilasPdf : _maxFilas;
            ReporteDatos datos;
            try
            {
                datos = await _repo.EjecutarAsync(cab.DES_ESQUEMA!, cab.DES_PROCEDIMIENTO!, ejecucion,
                                                  Math.Clamp(cab.NUM_TIEMPO_MAXIMO ?? TiempoPorDefecto, 5, 1800), max);
            }
            catch (ReporteLimiteException) when (formato == "PDF")
            {
                throw new ReporteLimiteException($"El reporte trae más de {_maxFilasPdf:N0} filas, demasiadas para un PDF. Sácalo en Excel o CSV, o acota los filtros.");
            }
            catch (ReporteLimiteException) when (excelConFormato)
            {
                throw new ReporteLimiteException($"El reporte trae más de {_maxFilasPdf:N0} filas, demasiadas para un Excel con formato. Sácalo en CSV, quita «con formato» en el diseño o acota los filtros.");
            }

            var columnasNuevas = await ColumnasNuevas(def, datos);

            byte[]? plantilla = null;
            if (formato is "DOC" or "XLS" && plantillaFormato.FLG_PLANTILLA_PROPIA)
                plantilla = (await _repo.PlantillaAsync(cab.NUM_DISENO ?? 0, formato)).Contenido;

            var ctx = new ReporteContexto
            {
                Definicion = def, Datos = datos, Valores = valores, Empresa = await _repo.EmpresaAsync(codUsuario),
                Usuario = codUsuario, Fecha = DateTime.Now, Plantilla = plantilla
            };
            if (EsLibre(cab.COD_TIPO_DISENO) && (formato == "PDF" || excelConFormato) && def.Lienzo?.Objetos.Any(o => o.COD_TIPO_OBJETO == "SUB") == true)
                await Subinformes(ctx, codUsuario, rol, codEstacion);
            var bytes = ReporteGenerador.Generar(ctx, formato);
            var archivo = new ReporteArchivo
            {
                DES_NOMBRE_ARCHIVO = ReporteTexto.NombreArchivo(NombreBase(cab, valores, ctx.Fecha), ReporteGenerador.Extension(formato)),
                DES_CONTENT_TYPE = ReporteGenerador.ContentType(formato),
                CONTENIDO = bytes,
                NUM_FILAS = datos.TotalFilas,
                COLUMNAS_NUEVAS = columnasNuevas
            };
            // Solo por botón: la definición por diseño (vista previa del diseñador) no trae ruta.
            if (cab.COD_BOTON_OPCION is not null && Vacio(cab.COD_KEY_PATHFILE) is not null)
                GuardarCopia(archivo, cab, dto.Contexto, valores, datos, ctx.Fecha, codUsuario, formato);
            return archivo;
        }, nameof(GenerarAsync));

    /// <summary>La copia del archivo en la ruta del botón. Con un nombre configurado, el archivo que se descarga también
    /// lo lleva (sin la versión _2, _3... que pueda tomar la copia). Si no se puede guardar (la ruta inactiva o sin
    /// servidor, una marca que no está, la carpeta sin permiso...), el archivo sale igual y DES_AVISO_GUARDADO dice por
    /// qué; los errores de disco o de red van además al log.</summary>
    private void GuardarCopia(ReporteArchivo archivo, ReporteDefinicionCabecera cab, Dictionary<string, string?>? fila,
                              List<ReporteValorParametro> valores, ReporteDatos datos, DateTime fecha, string codUsuario, string formato)
    {
        var extension = ReporteGenerador.Extension(formato);
        try
        {
            var nombre = Path.GetFileNameWithoutExtension(archivo.DES_NOMBRE_ARCHIVO);
            if (Vacio(cab.DES_NOMBRE_ARCHIVO) is { } patron)
            {
                nombre = ReporteCopiaArchivo.Armar(patron, fila, valores, datos, fecha, codUsuario);
                archivo.DES_NOMBRE_ARCHIVO = $"{nombre}.{extension}";
            }
            if (!cab.FLG_RUTA_ACTIVA) throw new ArgumentException($"la ruta {cab.COD_KEY_PATHFILE} no está activa en las rutas de archivos.");
            if (Vacio(cab.DES_PATH_FILE) is null) throw new ArgumentException($"la ruta {cab.COD_KEY_PATHFILE} no tiene servidor (o ya no está en las rutas de archivos).");
            archivo.DES_RUTA_GUARDADA = ReporteCopiaArchivo.Guardar(cab.DES_PATH_FILE!.Trim(), nombre, extension, archivo.CONTENIDO);
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            archivo.DES_AVISO_GUARDADO = $"No se guardó la copia del archivo: {ex.Message}";
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(GuardarCopia));
            archivo.DES_AVISO_GUARDADO = $"No se guardó la copia del archivo en {cab.DES_PATH_FILE}: {ex.Message}";
        }
    }

    /// <summary>Los subinformes de un diseño libre, antes de dibujarlo: lee el diseño de cada uno, pregunta al motor con qué
    /// valores se imprime cada vez (por fila, por grupo...) y ejecuta cada combinación distinta una sola vez. Lo que falla en
    /// uno (su procedimiento, un dato que no se puede convertir) sale en rojo en su lugar y no impide el resto del reporte.
    /// Muchas ejecuciones o demasiadas filas en total sí lo impiden, como en el reporte principal.</summary>
    private async Task Subinformes(ReporteContexto ctx, string codUsuario, string? rol, string codEstacion)
    {
        foreach (var n in ctx.Definicion.Lienzo!.Objetos.Where(o => o.COD_TIPO_OBJETO == "SUB" && o.NUM_DISENO_SUB is not null)
                                                         .Select(o => o.NUM_DISENO_SUB!.Value).Distinct())
        {
            var s = new ReporteSubinforme();
            try
            {
                s.Definicion = await _repo.DefinicionAsync(n, null, null, null, null, codUsuario);
                var c = s.Definicion.Cabecera;
                if (!EsLibre(c.COD_TIPO_DISENO) || s.Definicion.Lienzo is null) s.Error = "el subinforme tiene que ser un diseño libre.";
                else if (!c.FLG_EXISTE) s.Error = "el procedimiento de su fuente de datos ya no existe en la base.";
            }
            catch (SqlException ex) when (ex.Number >= 50000)
            {
                s.Error = ex.Message;
            }
            ctx.Subinformes[n] = s;
        }

        var pedidos = ReporteBandasPdf.PedidosSubinformes(ctx)
            .Where(p => p.Error is null)
            .GroupBy(p => (p.NumDiseno, p.Clave)).Select(g => g.First()).ToList();
        if (pedidos.Count > _maxSubinformes)
            throw new ReporteLimiteException($"Los subinformes se tendrían que ejecutar {pedidos.Count:N0} veces (el tope es {_maxSubinformes:N0}). Acota los filtros.");

        var filas = ctx.Datos.TotalFilas;
        foreach (var p in pedidos)
        {
            var s = ctx.Subinformes[p.NumDiseno];
            var c = s.Definicion.Cabecera;
            // Usuario, rol y estación, como en el reporte principal.
            foreach (var v in p.Valores.Where(v => v.Parametro.FLG_SISTEMA
                                                || (v.Valor is null && (v.Parametro.COD_VALOR_DEFECTO ?? "").ToUpperInvariant() is "USUARIO" or "ROL" or "ESTACION")))
            {
                var etiqueta = Vacio(v.Parametro.DES_ETIQUETA) ?? ReporteTexto.EtiquetaDesdeNombre(v.Parametro.DES_PARAMETRO);
                v.Valor = ReporteValores.Convertir(Sistema(v.Parametro, codUsuario, rol, codEstacion), v.Parametro.DES_TIPO_DATO, etiqueta);
                v.Texto = ReporteValores.TextoVisible(v.Valor, null, v.Parametro);
            }
            if (p.Valores.FirstOrDefault(v => v.Valor is null && v.Parametro.FLG_OBLIGATORIO) is { } falta)
            {
                s.Errores[p.Clave] = $"Falta {Vacio(falta.Parametro.DES_ETIQUETA) ?? ReporteTexto.EtiquetaDesdeNombre(falta.Parametro.DES_PARAMETRO)}.";
                continue;
            }
            var ejecucion = p.Valores.Where(v => v.Valor is not null)
                .Select(v => new ReporteParametroEjecucion(v.Parametro.DES_PARAMETRO, v.Parametro.DES_TIPO_DATO, v.Valor!,
                                                           v.Parametro.NUM_LONGITUD, v.Parametro.NUM_PRECISION, v.Parametro.NUM_ESCALA));
            try
            {
                var datos = await _repo.EjecutarAsync(c.DES_ESQUEMA!, c.DES_PROCEDIMIENTO!, ejecucion,
                                                      Math.Clamp(c.NUM_TIEMPO_MAXIMO ?? TiempoPorDefecto, 5, 1800), _maxFilasPdf);
                filas += datos.TotalFilas;
                if (filas > _maxFilasPdf * 2)
                    throw new ReporteLimiteException($"Con sus subinformes, el reporte trae más de {_maxFilasPdf * 2:N0} filas, demasiadas para dibujarlo. Acota los filtros.");
                s.Datos[p.Clave] = datos;
                s.Valores[p.Clave] = p.Valores;
            }
            catch (SqlException ex) when (ex.Number != -2)
            {
                s.Errores[p.Clave] = ex.Message;
            }
            catch (ReporteLimiteException) when (filas <= _maxFilasPdf * 2)
            {
                s.Errores[p.Clave] = $"trae más de {_maxFilasPdf:N0} filas.";
            }
        }
    }

    // ── Valores de los parámetros ───────────────────────────────────────────────
    /// <summary>El valor de cada parámetro según su origen, convertido a su tipo. Valida los obligatorios.
    ///   SIS  usuario, rol o estación (los pone la API; lo que mande el navegador no cuenta)
    ///   FIJ  el valor fijo de la asignación del botón
    ///   FIL  el campo de la fila que dice la asignación
    ///   DLG  lo que el usuario llenó en el diálogo
    ///   AUT  el campo de la fila con el mismo nombre; si no hay, lo del diálogo
    /// Sin valor, se usa el valor por defecto de la fuente (HOY, INIMES, FIJO...).</summary>
    private static List<ReporteValorParametro> Resolver(ReporteDefinicion def, ReporteGenerarDto dto, string codUsuario, string? rol, string codEstacion)
    {
        var lista = new List<ReporteValorParametro>();
        foreach (var p in def.Parametros.OrderBy(x => x.NUM_ORDEN))
        {
            var etiqueta = Vacio(p.DES_ETIQUETA) ?? ReporteTexto.EtiquetaDesdeNombre(p.DES_PARAMETRO);
            var texto = Texto(p, dto.Valores, dto.Contexto, codUsuario, rol, codEstacion);
            var valor = ReporteValores.Convertir(texto, p.DES_TIPO_DATO, etiqueta);
            if (valor is null && p.FLG_OBLIGATORIO) throw new ArgumentException($"Falta {etiqueta}.");
            var campo = ReporteTexto.SinArroba(p.DES_PARAMETRO);
            lista.Add(new ReporteValorParametro
            {
                Parametro = p,
                Valor = valor,
                Texto = ReporteValores.TextoVisible(valor, Buscar(dto.Textos, campo), p)
            });
        }
        return lista;
    }

    private static string? Texto(ReporteFuenteParametro p, Dictionary<string, string?> valores, Dictionary<string, string?> contexto,
                                 string codUsuario, string? rol, string codEstacion)
    {
        var campo = ReporteTexto.SinArroba(p.DES_PARAMETRO);
        var origen = p.FLG_SISTEMA ? "SIS" : (p.COD_ORIGEN ?? "AUT").ToUpperInvariant();
        var texto = origen switch
        {
            "SIS" => Sistema(p, codUsuario, rol, codEstacion),
            "FIJ" => p.DES_VALOR,
            "FIL" => Buscar(contexto, p.DES_VALOR),
            "DLG" => Buscar(valores, campo),
            _     => Vacio(Buscar(contexto, campo)) ?? Buscar(valores, campo)
        };
        if (string.IsNullOrWhiteSpace(texto) && origen != "SIS")
        {
            texto = ReporteValores.Defecto(p.COD_VALOR_DEFECTO, p.DES_VALOR_DEFECTO, DateTime.Now);
            if (texto is null && p.COD_VALOR_DEFECTO is "USUARIO" or "ROL" or "ESTACION") texto = Sistema(p, codUsuario, rol, codEstacion);
        }
        return texto;
    }

    private static string? Sistema(ReporteFuenteParametro p, string codUsuario, string? rol, string codEstacion)
    {
        var cual = (p.COD_VALOR_DEFECTO ?? "").ToUpperInvariant();
        if (cual is not ("USUARIO" or "ROL" or "ESTACION"))
        {
            var n = p.DES_PARAMETRO.ToUpperInvariant();
            cual = n.Contains("ROL") ? "ROL" : n.Contains("ESTACION") ? "ESTACION" : "USUARIO";
        }
        return cual switch { "ROL" => rol, "ESTACION" => codEstacion, _ => codUsuario };
    }

    private static string? Buscar(Dictionary<string, string?>? d, string? clave)
    {
        if (d is null || string.IsNullOrWhiteSpace(clave)) return null;
        var c = ReporteTexto.SinArroba(clave);
        foreach (var (k, v) in d)
            if (string.Equals(ReporteTexto.SinArroba(k), c, StringComparison.OrdinalIgnoreCase)) return v;
        return null;
    }

    private static string Nombre(string formato) => formato switch { "DOC" => "Word", "XLS" => "Excel", _ => formato };

    /// <summary>"Orden de compra OC OCL 123" en una ficha (con los valores que la identifican); "Listado de órdenes de
    /// compra 20261010-1015" en un listado.</summary>
    private static string NombreBase(ReporteDefinicionCabecera cab, List<ReporteValorParametro> valores, DateTime fecha)
    {
        var nombre = Vacio(cab.DES_DISENO) ?? "Reporte";
        if (cab.COD_TIPO_DISENO == "FIC")
        {
            var claves = valores.Where(v => !v.Parametro.FLG_SISTEMA && v.Valor is not null)
                                .Select(v => Convert.ToString(v.Valor, CultureInfo.InvariantCulture)).Where(s => !string.IsNullOrWhiteSpace(s));
            var sufijo = string.Join(" ", claves);
            return string.IsNullOrWhiteSpace(sufijo) ? nombre : $"{nombre} {sufijo}";
        }
        return $"{nombre} {fecha:yyyyMMdd-HHmm}";
    }
}
