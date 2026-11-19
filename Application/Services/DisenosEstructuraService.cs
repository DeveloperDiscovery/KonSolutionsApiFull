using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Services;

public class DisenosEstructuraService : IDisenosEstructuraService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(DisenosEstructuraService);
    private readonly IDisenosEstructuraRepository _repo;
    private readonly ErrorLogService _log;
    public DisenosEstructuraService(IDisenosEstructuraRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    // 524xx (52400 a 52462) y 526xx (52620 a 52629, script 130) son validaciones de los scripts 107, 112 y 130 (version bloqueada, ciclo, mas de 10 niveles...): se devuelven como mensaje.
    private static bool EsValidacion(SqlException ex) => ex.Number is (>= 52400 and <= 52462) or (>= 52620 and <= 52629);

    private async Task<ApiResponse<T>> Ejecutar<T>(Func<Task<T>> accion, string metodo)
    {
        try { return ApiResponse<T>.Ok(await accion()); }
        catch (SqlException ex) when (EsValidacion(ex)) { return ApiResponse<T>.Fail(ex.Message); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, metodo); return ApiResponse<T>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<DisenosEstructuraNodo>>> EstructuraAsync(string codDiseno, int numVersion, int maxNivel)
    {
        try { var d = (await _repo.EstructuraAsync(codDiseno, numVersion, maxNivel)).ToList(); return ApiResponse<IEnumerable<DisenosEstructuraNodo>>.Ok(d, total: d.Count); }
        catch (SqlException ex) when (EsValidacion(ex)) { return ApiResponse<IEnumerable<DisenosEstructuraNodo>>.Fail(ex.Message); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(EstructuraAsync)); return ApiResponse<IEnumerable<DisenosEstructuraNodo>>.Fail(ex.Message); }
    }

    public Task<ApiResponse<bool>> SectorGuardarAsync(DisenosEstructuraSectorDto dto, string u, string e) =>
        Ejecutar(async () => { await _repo.SectorGuardarAsync(dto, u, e); return true; }, nameof(SectorGuardarAsync));
    public Task<ApiResponse<bool>> SectorEliminarAsync(string codDiseno, int numVersion, string codSector) =>
        Ejecutar(async () => { await _repo.SectorEliminarAsync(codDiseno, numVersion, codSector); return true; }, nameof(SectorEliminarAsync));
    public Task<ApiResponse<bool>> ComponenteAgregarAsync(DisenosEstructuraComponenteDto dto, string u, string e) =>
        Ejecutar(async () => { await _repo.ComponenteAgregarAsync(dto, u, e); return true; }, nameof(ComponenteAgregarAsync));
    public Task<ApiResponse<bool>> ComponenteQuitarAsync(DisenosEstructuraComponenteDto dto) =>
        Ejecutar(async () => { await _repo.ComponenteQuitarAsync(dto); return true; }, nameof(ComponenteQuitarAsync));
    public Task<ApiResponse<bool>> ComponenteCambiarAsync(DisenosEstructuraComponenteCambiarDto dto, string u, string e) =>
        Ejecutar(async () => { await _repo.ComponenteCambiarAsync(dto, u, e); return true; }, nameof(ComponenteCambiarAsync));
    public Task<ApiResponse<DisenosEstructuraInsumoResultado>> InsumoGuardarAsync(DisenosEstructuraInsumoDto dto, string u, string e) =>
        Ejecutar(async () => new DisenosEstructuraInsumoResultado { NUM_SECUENCIA = await _repo.InsumoGuardarAsync(dto, u, e) }, nameof(InsumoGuardarAsync));
    public Task<ApiResponse<bool>> InsumoEliminarAsync(DisenosEstructuraInsumoClaveDto dto) =>
        Ejecutar(async () => { await _repo.InsumoEliminarAsync(dto); return true; }, nameof(InsumoEliminarAsync));
    public Task<ApiResponse<bool>> BloquearAsync(string codDiseno, int numVersion, string u) =>
        Ejecutar(async () => { await _repo.BloquearAsync(codDiseno, numVersion, u); return true; }, nameof(BloquearAsync));
    public Task<ApiResponse<bool>> DesbloquearAsync(string codDiseno, int numVersion, string u) =>
        Ejecutar(async () => { await _repo.DesbloquearAsync(codDiseno, numVersion, u); return true; }, nameof(DesbloquearAsync));
    public Task<ApiResponse<DisenosVersionCopiarResultado>> CopiarVersionAsync(DisenosVersionCopiarDto dto, string u, string e) =>
        Ejecutar(() => _repo.CopiarVersionAsync(dto, u, e), nameof(CopiarVersionAsync));
    public Task<ApiResponse<DisenosSegmentoCopiarResultado>> CopiarSegmentoAsync(DisenosSegmentoCopiarDto dto, string u, string e) =>
        Ejecutar(() => _repo.CopiarSegmentoAsync(dto, u, e), nameof(CopiarSegmentoAsync));

    // ---------- ruta de produccion de la version (script 112) ----------
    public Task<ApiResponse<DisenosRutaCatalogos>> RutaCatalogosAsync() =>
        Ejecutar(() => _repo.RutaCatalogosAsync(), nameof(RutaCatalogosAsync));
    public Task<ApiResponse<DisenosRutaResultado>> RutaCrearAsync(DisenosRutaCrearDto dto, string u, string e) =>
        Ejecutar(async () => new DisenosRutaResultado { COD_RUTA_PRODUCCION = await _repo.RutaCrearAsync(dto, u, e) }, nameof(RutaCrearAsync));
    public Task<ApiResponse<bool>> RutaAsignarAsync(DisenosRutaAsignarDto dto, string u, string e) =>
        Ejecutar(async () => { await _repo.RutaAsignarAsync(dto, u, e); return true; }, nameof(RutaAsignarAsync));
    public Task<ApiResponse<DisenosRutaResultado>> RutaCopiarAsync(DisenosRutaCopiarDto dto, string u, string e) =>
        Ejecutar(() => _repo.RutaCopiarAsync(dto, u, e), nameof(RutaCopiarAsync));
}
