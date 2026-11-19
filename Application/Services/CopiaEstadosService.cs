using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class CopiaEstadosService : ICopiaEstadosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(CopiaEstadosService);
    private readonly ICopiaEstadosRepository _repo;
    private readonly ErrorLogService _log;
    public CopiaEstadosService(ICopiaEstadosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    // Los errores 522xx son validaciones del procedimiento (mismo origen y destino, opcion inexistente, etc.): se devuelven como mensaje, no como falla del sistema.
    private static bool EsValidacion(SqlException ex) => ex.Number is >= 52201 and <= 52208;

    public async Task<ApiResponse<IEnumerable<CopiaConfiguracionEstadosFila>>> CopiarConfiguracionAsync(CopiaConfiguracionEstadosDto dto, string codUsuario, string codEstacion)
    {
        try { var d = (await _repo.CopiarConfiguracionAsync(dto, codUsuario, codEstacion)).ToList();
              return ApiResponse<IEnumerable<CopiaConfiguracionEstadosFila>>.Ok(d, total: d.Count); }
        catch (SqlException ex) when (EsValidacion(ex)) { return ApiResponse<IEnumerable<CopiaConfiguracionEstadosFila>>.Fail(ex.Message); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(CopiarConfiguracionAsync));
              return ApiResponse<IEnumerable<CopiaConfiguracionEstadosFila>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<CopiaEstadosTipoFila>>> CopiarEstadosTipoAsync(CopiaEstadosTipoDto dto, string codUsuario, string codEstacion)
    {
        try { var d = (await _repo.CopiarEstadosTipoAsync(dto, codUsuario, codEstacion)).ToList();
              return ApiResponse<IEnumerable<CopiaEstadosTipoFila>>.Ok(d, total: d.Count); }
        catch (SqlException ex) when (EsValidacion(ex)) { return ApiResponse<IEnumerable<CopiaEstadosTipoFila>>.Fail(ex.Message); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(CopiarEstadosTipoAsync));
              return ApiResponse<IEnumerable<CopiaEstadosTipoFila>>.Fail(ex.Message); }
    }
}
