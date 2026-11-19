using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Services;

public class ProyectosExpedienteService : IProyectosExpedienteService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ProyectosExpedienteService);
    private const long MaxArchivo = 50L * 1024 * 1024;
    private static readonly HashSet<string> Prohibidas = new(StringComparer.OrdinalIgnoreCase)
        { ".exe", ".dll", ".bat", ".cmd", ".com", ".msi", ".ps1", ".vbs", ".js", ".jar", ".scr", ".sh", ".html", ".htm", ".svg" };

    private readonly IProyectosExpedienteRepository _repo;
    private readonly IRegistroComprasRepository _compras;
    private readonly IPathFilesRepository _paths;
    private readonly ErrorLogService _log;

    private readonly IProyectosParametrosRepository _parametros;

    public ProyectosExpedienteService(IProyectosExpedienteRepository repo, IRegistroComprasRepository compras, IPathFilesRepository paths, IProyectosParametrosRepository parametros, ErrorLogService log)
    { _repo = repo; _compras = compras; _paths = paths; _parametros = parametros; _log = log; }

    private async Task<ApiResponse<T>> Corre<T>(Func<Task<T>> accion, string metodo)
    {
        try { return ApiResponse<T>.Ok(await accion()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, metodo); return ApiResponse<T>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ExpedienteResumen>> ResumenAsync(int num)
    {
        try { var r = await _repo.ResumenAsync(num); return r is null ? ApiResponse<ExpedienteResumen>.Fail("El proyecto no existe.") : ApiResponse<ExpedienteResumen>.Ok(r); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ResumenAsync)); return ApiResponse<ExpedienteResumen>.Fail(ex.Message); }
    }

    public Task<ApiResponse<ExpedientePlanificacion>> PlanificacionAsync(int num) => Corre(() => _repo.PlanificacionAsync(num), nameof(PlanificacionAsync));
    public Task<ApiResponse<IEnumerable<ExpedienteRecurso>>> RecursosAsync(int num) => Corre(() => _repo.RecursosAsync(num), nameof(RecursosAsync));
    public Task<ApiResponse<ExpedienteCostos>> CostosAsync(int num) => Corre(() => _repo.CostosAsync(num), nameof(CostosAsync));
    public Task<ApiResponse<ExpedienteRiesgos>> RiesgosAsync(int num) => Corre(() => _repo.RiesgosAsync(num), nameof(RiesgosAsync));
    public Task<ApiResponse<ExpedienteDocumentos>> DocumentosAsync(int num) => Corre(() => _repo.DocumentosAsync(num), nameof(DocumentosAsync));
    public Task<ApiResponse<ExpedienteMateriales>> MaterialesAsync(int num) => Corre(() => _repo.MaterialesAsync(num), nameof(MaterialesAsync));
    public Task<ApiResponse<IEnumerable<ExpedienteTipoEntidad>>> TiposEntidadAsync(int num) => Corre(() => _repo.TiposEntidadAsync(num), nameof(TiposEntidadAsync));
    public Task<ApiResponse<IEnumerable<ComboboxItem>>> EntidadesElegiblesAsync(int num) => Corre(() => _repo.EntidadesElegiblesAsync(num), nameof(EntidadesElegiblesAsync));
    public Task<ApiResponse<IEnumerable<PortafolioProyecto>>> PortafolioAsync(string? estado, string? tipo, string? search) => Corre(() => _repo.PortafolioAsync(estado, tipo, search), nameof(PortafolioAsync));
    public Task<ApiResponse<IEnumerable<MiTarea>>> MisTareasAsync(decimal codEntidad, bool soloAbiertas) => Corre(() => _repo.MisTareasAsync(codEntidad, soloAbiertas), nameof(MisTareasAsync));

    public Task<ApiResponse<bool>> GuardarTiposEntidadAsync(int num, ExpedienteTiposEntidadDto dto, string codUsuario, string codEstacion)
        => Corre(async () => { await _repo.GuardarTiposEntidadAsync(num, dto.Tipos, codUsuario, codEstacion); return true; }, nameof(GuardarTiposEntidadAsync));

    public Task<ApiResponse<bool>> RecalcularAsync(int num, string codUsuario, string codEstacion, DateTime? fecReal = null)
        => Corre(async () => { await _repo.RecalcularAsync(num, codUsuario, codEstacion, fecReal); return true; }, nameof(RecalcularAsync));

    public Task<ApiResponse<bool>> QuitarRecursoAsync(int num, int codTarea, decimal codEntidad, string codRol)
        => Corre(async () => { await _repo.QuitarRecursoAsync(num, codTarea, codEntidad, codRol); return true; }, nameof(QuitarRecursoAsync));

    public Task<ApiResponse<IEnumerable<ExpedienteMovido>>> MoverAsync(int num, ExpedienteMoverDto dto, string codUsuario, string codEstacion)
        => Corre(() => _repo.MoverAsync(num, dto, codUsuario, codEstacion), nameof(MoverAsync));

    public async Task<ApiResponse<ExpedienteAvanceResult>> AvanceAsync(int num, int codTarea, ExpedienteAvanceDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            var r = await _repo.AvanceAsync(num, codTarea, dto, codUsuario, codEstacion);
            return r is null ? ApiResponse<ExpedienteAvanceResult>.Fail("La tarea no existe.") : ApiResponse<ExpedienteAvanceResult>.Ok(r, r.FLG_POR_CONFIRMAR ? "La tarea llegó al 100%: queda por confirmar." : "Avance registrado.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(AvanceAsync)); return ApiResponse<ExpedienteAvanceResult>.Fail(ex.Message); }
    }

    public Task<ApiResponse<ExpedienteCargoArea?>> CargoAreaAsync(string codEntidad) => Corre(() => _repo.CargoAreaAsync(codEntidad), nameof(CargoAreaAsync));
    public Task<ApiResponse<IEnumerable<ExpedienteOrganigramaNodo>>> OrganigramaAsync(int num) => Corre(() => _repo.OrganigramaAsync(num), nameof(OrganigramaAsync));
    public Task<ApiResponse<IEnumerable<ExpedienteMemoria>>> MemoriasAsync(int num) => Corre(() => _repo.MemoriasAsync(num), nameof(MemoriasAsync));
    public Task<ApiResponse<IEnumerable<ExpedienteEstadoIcono>>> EstadosIconosAsync(int num) => Corre(() => _repo.EstadosIconosAsync(num), nameof(EstadosIconosAsync));
    public Task<ApiResponse<IEnumerable<ExpedienteCompromiso>>> CompromisosAsync(int num) => Corre(() => _repo.CompromisosAsync(num), nameof(CompromisosAsync));
    public Task<ApiResponse<IEnumerable<ExpedienteCompromisoPersona>>> CompromisoPersonasAsync(int num, int secuencia) => Corre(() => _repo.CompromisoPersonasAsync(num, secuencia), nameof(CompromisoPersonasAsync));
    public Task<ApiResponse<IEnumerable<ExpedienteCompromisoDecision>>> CompromisoDecisionesAsync(int num, int secuencia) => Corre(() => _repo.CompromisoDecisionesAsync(num, secuencia), nameof(CompromisoDecisionesAsync));
    public Task<ApiResponse<IEnumerable<ComboboxItem>>> CompromisoTiposAsync() => Corre(() => _repo.CompromisoTiposAsync(), nameof(CompromisoTiposAsync));
    public Task<ApiResponse<int>> CompromisoGuardarAsync(int num, ExpedienteCompromisoDto dto, string codUsuario, string codEstacion) => Corre(() => _repo.CompromisoGuardarAsync(num, dto, codUsuario, codEstacion), nameof(CompromisoGuardarAsync));
    public Task<ApiResponse<string?>> CompromisoDecidirAsync(int num, int secuencia, ExpedienteCompromisoDecidirDto dto, string codUsuario, string codEstacion) => Corre(() => _repo.CompromisoDecidirAsync(num, secuencia, dto, codUsuario, codEstacion), nameof(CompromisoDecidirAsync));
    public Task<ApiResponse<bool>> CompromisoQuitarAsync(int num, int secuencia) => Corre(async () => { await _repo.CompromisoQuitarAsync(num, secuencia); return true; }, nameof(CompromisoQuitarAsync));
    public Task<ApiResponse<IEnumerable<ExpedienteEvento>>> EventosAsync(int num) => Corre(() => _repo.EventosAsync(num), nameof(EventosAsync));
    public Task<ApiResponse<IEnumerable<ExpedienteEstadoHistorial>>> EstadosHistorialAsync(int num) => Corre(() => _repo.EstadosHistorialAsync(num), nameof(EstadosHistorialAsync));
    public Task<ApiResponse<int>> EventoGuardarAsync(int num, ExpedienteEventoDto dto, bool esNuevo, string codUsuario, string codEstacion)
        => Corre(() => _repo.EventoGuardarAsync(num, dto, esNuevo, codUsuario, codEstacion), nameof(EventoGuardarAsync));
    public Task<ApiResponse<bool>> EventoQuitarAsync(int num, int secuencia)
        => Corre(async () => { await _repo.EventoQuitarAsync(num, secuencia); return true; }, nameof(EventoQuitarAsync));

    public async Task<ApiResponse<ExpedienteEstadoResult>> EstadoAplicarAsync(string nivel, int num, int cod, ExpedienteEstadoDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            var r = await _repo.EstadoAplicarAsync(nivel, num, cod, dto, codUsuario, codEstacion);
            return r is null ? ApiResponse<ExpedienteEstadoResult>.Fail("No se pudo cambiar el estado.") : ApiResponse<ExpedienteEstadoResult>.Ok(r);
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(EstadoAplicarAsync)); return ApiResponse<ExpedienteEstadoResult>.Fail(ex.Message); }
    }

    public Task<ApiResponse<bool>> ConfirmarAsync(int num, int codTarea, ExpedienteConfirmarDto dto, string codUsuario, string codEstacion)
        => Corre(async () => { await _repo.ConfirmarAsync(num, codTarea, dto, codUsuario, codEstacion); return true; }, nameof(ConfirmarAsync));

    public Task<ApiResponse<bool>> RecursoCostoGuardarAsync(int num, ExpedienteRecursoCostoDto dto, string codUsuario, string codEstacion)
        => Corre(async () => { await _repo.RecursoCostoGuardarAsync(num, dto, codUsuario, codEstacion); return true; }, nameof(RecursoCostoGuardarAsync));
    public Task<ApiResponse<IEnumerable<decimal>>> EntidadesPropiasAsync() => Corre(() => _repo.EntidadesPropiasAsync(), nameof(EntidadesPropiasAsync));
    public Task<ApiResponse<IEnumerable<ExpedienteTipoPropio>>> TiposPropiosAsync() => Corre(() => _repo.TiposPropiosAsync(), nameof(TiposPropiosAsync));
    public Task<ApiResponse<bool>> TiposPropiosGuardarAsync(IEnumerable<string> tipos, string codUsuario, string codEstacion)
        => Corre(async () => { await _repo.TiposPropiosGuardarAsync(tipos, codUsuario, codEstacion); return true; }, nameof(TiposPropiosGuardarAsync));

    public Task<ApiResponse<int>> AsignarGastoAsync(int num, ExpedienteGastoDto dto, string codUsuario, string codEstacion)
        => Corre(() => _repo.AsignarGastoAsync(num, dto, codUsuario, codEstacion), nameof(AsignarGastoAsync));

    // ───────── archivos del proyecto ─────────
    // Se guardan en la ruta de Proyectos > Parámetros de proyectos, en la subcarpeta del proyecto (número a 10 dígitos: 0000000001).
    // Los archivos subidos antes de esta versión (ruta relativa PROYECTOS/{número}/...) siguen leyéndose de la carpeta de Compras.

    private async Task<(string? Carpeta, string? Error)> CarpetaAsync(string? relativa = null)
    {
        if (relativa is null || !relativa.StartsWith("PROYECTOS/", StringComparison.OrdinalIgnoreCase)) return await ProyectosCarpetas.RaizAsync(_parametros, _paths);
        var cfg = await _compras.ConfigXmlAsync();
        var clave = cfg?.COD_KEY_PATHFILE_COMPRAS_XML;
        if (string.IsNullOrWhiteSpace(clave)) return (null, "No hay una carpeta configurada: elígela en Configuración general, pestaña Compras.");
        var ruta = await _paths.ShowbyidAsync(clave);
        if (ruta is null || string.IsNullOrWhiteSpace(ruta.DES_PATH_FILE)) return (null, $"La ruta «{clave}» no existe en Rutas de archivos o no tiene carpeta.");
        var carpeta = ruta.DES_PATH_FILE!.Trim();
        if (!Directory.Exists(carpeta)) return (null, $"La carpeta «{carpeta}» no existe o la API no puede verla.");
        return (carpeta, null);
    }

    /// <summary>Nombre de archivo sin ruta ni caracteres raros.</summary>
    private static string Limpio(string nombre)
    {
        var n = Path.GetFileName(nombre ?? "");
        foreach (var c in Path.GetInvalidFileNameChars()) n = n.Replace(c, '_');
        return string.IsNullOrWhiteSpace(n) ? "archivo" : n;
    }

    public async Task<ApiResponse<int>> ArchivoSubirAsync(int num, int? codFase, int? codTarea, string nombre, Stream contenido, long largo, string codUsuario, string codEstacion)
    {
        string? destino = null;
        try
        {
            var limpio = Limpio(nombre);
            if (Prohibidas.Contains(Path.GetExtension(limpio))) return ApiResponse<int>.Fail("Ese tipo de archivo no se puede guardar en un proyecto.");
            if (largo <= 0) return ApiResponse<int>.Fail("El archivo está vacío.");
            if (largo > MaxArchivo) return ApiResponse<int>.Fail("El archivo pesa más de 50 MB.");
            var (carpeta, error) = await CarpetaAsync();
            if (carpeta is null) return ApiResponse<int>.Fail(error!);

            var relativa = $"{ProyectosCarpetas.Nombre(num)}/{Guid.NewGuid():N}_{limpio}";
            destino = Path.Combine(carpeta, relativa.Replace('/', Path.DirectorySeparatorChar));
            Directory.CreateDirectory(Path.GetDirectoryName(destino)!);
            await using (var fs = File.Create(destino)) await contenido.CopyToAsync(fs);

            var sec = await _repo.ArchivoRegistrarAsync(num, codFase, codTarea, relativa, codUsuario, codEstacion);
            return ApiResponse<int>.Ok(sec, "Archivo guardado.");
        }
        catch (Exception ex)
        {
            if (destino is not null && File.Exists(destino)) { try { File.Delete(destino); } catch { /* ya se informa el error principal */ } }
            _log.LogError(ex, Programa, Clase, nameof(ArchivoSubirAsync));
            return ApiResponse<int>.Fail(ex.Message);
        }
    }

    public async Task<ApiResponse<ProyectoArchivoContenido>> ArchivoLeerAsync(int num, int secuencia)
    {
        try
        {
            var a = await _repo.ArchivoAsync(num, secuencia);
            if (a is null || string.IsNullOrWhiteSpace(a.DES_PATH_FILE)) return ApiResponse<ProyectoArchivoContenido>.Fail("No encontrado.");
            var (carpeta, error) = await CarpetaAsync(a.DES_PATH_FILE);
            if (carpeta is null) return ApiResponse<ProyectoArchivoContenido>.Fail(error!);
            var ruta = RutaSegura(carpeta, a.DES_PATH_FILE!);
            if (ruta is null || !File.Exists(ruta)) return ApiResponse<ProyectoArchivoContenido>.Fail("El archivo no está en el servidor.");
            var nombre = Path.GetFileName(ruta);
            var i = nombre.IndexOf('_');
            if (i == 32) nombre = nombre[(i + 1)..];
            return ApiResponse<ProyectoArchivoContenido>.Ok(new ProyectoArchivoContenido { NOMBRE = nombre, CONTENIDO = await File.ReadAllBytesAsync(ruta) });
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ArchivoLeerAsync)); return ApiResponse<ProyectoArchivoContenido>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<bool>> ArchivoQuitarAsync(int num, int secuencia)
    {
        try
        {
            var a = await _repo.ArchivoAsync(num, secuencia);
            if (a is null) return ApiResponse<bool>.Fail("No encontrado.");
            await _repo.ArchivoQuitarAsync(num, secuencia);
            // el archivo se borra después del registro; si no se puede borrar, no pasa nada grave
            var (carpeta, _) = await CarpetaAsync(a.DES_PATH_FILE);
            if (carpeta is not null && !string.IsNullOrWhiteSpace(a.DES_PATH_FILE))
            {
                var ruta = RutaSegura(carpeta, a.DES_PATH_FILE!);
                if (ruta is not null && File.Exists(ruta)) { try { File.Delete(ruta); } catch (Exception ex) { _log.LogError(ex, Programa, Clase, "BorrarArchivo"); } }
            }
            return ApiResponse<bool>.Ok(true, "Archivo quitado.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ArchivoQuitarAsync)); return ApiResponse<bool>.Fail(ex.Message); }
    }

    /// <summary>La ruta completa solo si queda dentro de la carpeta configurada (nunca sale con «..»).</summary>
    private static string? RutaSegura(string carpeta, string relativa)
    {
        var raiz = Path.GetFullPath(carpeta);
        var completa = Path.GetFullPath(Path.Combine(raiz, relativa.Replace('/', Path.DirectorySeparatorChar)));
        return completa.StartsWith(raiz.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase) ? completa : null;
    }
}
