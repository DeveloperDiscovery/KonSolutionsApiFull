using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Services;

public class PostItsService : IPostItsService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PostItsService);
    private readonly IPostItsRepository _repo;
    private readonly ErrorLogService _log;
    public PostItsService(IPostItsRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<PostIts>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<PostIts>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<PostIts>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<PostIts>> GetByIdAsync(long NUM_POSTIT)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_POSTIT);
              return d is null ? ApiResponse<PostIts>.Fail("No encontrado.") : ApiResponse<PostIts>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<PostIts>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(PostItsInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(long NUM_POSTIT)
    {
        try { var r = await _repo.DeleteAsync(NUM_POSTIT);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
