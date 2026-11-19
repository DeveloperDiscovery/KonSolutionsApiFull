using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Services;

public class EntidadesProceduresService : IEntidadesProceduresService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EntidadesProceduresService);
    private readonly IEntidadesProceduresRepository _repo;
    private readonly ErrorLogService _log;
    public EntidadesProceduresService(IEntidadesProceduresRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<string>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<string>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<string>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EntidadProcedureParametro>>> GetParametersAsync(string name)
    {
        try { var d = (await _repo.ParametersShowallAsync(name)).ToList();
              return ApiResponse<IEnumerable<EntidadProcedureParametro>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetParametersAsync));
              return ApiResponse<IEnumerable<EntidadProcedureParametro>>.Fail(ex.Message); }
    }
}
