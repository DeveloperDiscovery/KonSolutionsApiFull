using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class FeriadosService : IFeriadosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(FeriadosService);
    private readonly IFeriadosRepository _repo;
    private readonly ErrorLogService _log;
    public FeriadosService(IFeriadosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    private async Task<ApiResponse<T>> Corre<T>(Func<Task<ApiResponse<T>>> f, string metodo)
    {
        try { return await f(); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, metodo); return ApiResponse<T>.Fail(ex.Message); }
    }

    public Task<ApiResponse<IEnumerable<Feriados>>> GetAllAsync() => Corre<IEnumerable<Feriados>>(async () => { var d = (await _repo.ShowallAsync()).ToList(); return ApiResponse<IEnumerable<Feriados>>.Ok(d, total: d.Count); }, nameof(GetAllAsync));
    public Task<ApiResponse<Feriados>> GetByIdAsync(DateTime f) => Corre(async () => { var d = await _repo.ShowbyidAsync(f); return d is null ? ApiResponse<Feriados>.Fail("No encontrado.") : ApiResponse<Feriados>.Ok(d); }, nameof(GetByIdAsync));
    public Task<ApiResponse<IEnumerable<Feriados>>> SearchAsync(FeriadosSearchDto dto) => Corre<IEnumerable<Feriados>>(async () => { var d = (await _repo.SearchAsync(dto)).ToList(); return ApiResponse<IEnumerable<Feriados>>.Ok(d, total: d.Count); }, nameof(SearchAsync));
    public Task<ApiResponse<WriteResult>> InsertAsync(FeriadosInsertDto dto, string u, string e) => Corre(async () => ApiResponse<WriteResult>.FromWriteResult(await _repo.InsertAsync(dto, u, e), "Registro guardado."), nameof(InsertAsync));
    public Task<ApiResponse<WriteResult>> UpdateAsync(FeriadosUpdateDto dto, string u, string e) => Corre(async () => ApiResponse<WriteResult>.FromWriteResult(await _repo.UpdateAsync(dto, u, e), "Registro modificado."), nameof(UpdateAsync));
    public Task<ApiResponse<WriteResult>> DeleteAsync(DateTime f) => Corre(async () => ApiResponse<WriteResult>.FromWriteResult(await _repo.DeleteAsync(f), "Registro eliminado."), nameof(DeleteAsync));
}
