using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Services;

public class EjecutarSentenciasService : IEjecutarSentenciasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EjecutarSentenciasService);
    private readonly IEjecutarSentenciasRepository _repo;
    private readonly ErrorLogService _log;
    public EjecutarSentenciasService(IEjecutarSentenciasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<bool>> ExecuteAsync(EjecutarSentenciaDto dto)
    {
        try { await _repo.ExecuteAsync(dto.DES_SENTENCIA);
              return ApiResponse<bool>.Ok(true, "Acción ejecutada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ExecuteAsync));
              return ApiResponse<bool>.Fail(ex.Message); }
    }
}
