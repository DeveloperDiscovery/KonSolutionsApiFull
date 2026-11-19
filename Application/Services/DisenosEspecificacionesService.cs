using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Services;

public class DisenosEspecificacionesService : IDisenosEspecificacionesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(DisenosEspecificacionesService);
    private readonly IDisenosEspecificacionesRepository _repo;
    private readonly ErrorLogService _log;
    public DisenosEspecificacionesService(IDisenosEspecificacionesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    // 52470 a 52476 son validaciones del script 113 (version inexistente o anulada, filas invalidas, especificacion que no corresponde...) y 52401 es
    // "version bloqueada" (script 107): se devuelven como mensaje, sin registrarlas como error del sistema.
    private static bool EsValidacion(SqlException ex) => ex.Number is 52401 or (>= 52470 and <= 52476);

    private async Task<ApiResponse<T>> Ejecutar<T>(Func<Task<T>> accion, string metodo)
    {
        try { return ApiResponse<T>.Ok(await accion()); }
        catch (SqlException ex) when (EsValidacion(ex)) { return ApiResponse<T>.Fail(ex.Message); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, metodo); return ApiResponse<T>.Fail(ex.Message); }
    }

    public Task<ApiResponse<DisenosEspecificacionesPanel>> PanelAsync(string codDiseno, int numVersion) =>
        Ejecutar(() => _repo.PanelAsync(codDiseno, numVersion), nameof(PanelAsync));

    public Task<ApiResponse<DisenosEspecificacionesResultado>> GuardarAsync(DisenosEspecificacionesGuardarDto dto, string codUsuario, string codEstacion) =>
        Ejecutar(() => _repo.GuardarAsync(dto, codUsuario, codEstacion), nameof(GuardarAsync));
}
