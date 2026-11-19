using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class FormasPagoService : IFormasPagoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(FormasPagoService);
    private readonly IFormasPagoRepository _repo;
    private readonly ErrorLogService _log;
    public FormasPagoService(IFormasPagoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<FormasPago>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<FormasPago>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<FormasPago>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<FormasPago>> GetByIdAsync(int COD_FORMA_PAGO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_FORMA_PAGO);
              return d is null ? ApiResponse<FormasPago>.Fail("No encontrado.") : ApiResponse<FormasPago>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<FormasPago>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<FormasPago>>> SearchAsync(FormasPagoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<FormasPago>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<FormasPago>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(FormasPagoInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(FormasPagoUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int COD_FORMA_PAGO)
    {
        try { var r = await _repo.DeleteAsync(COD_FORMA_PAGO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}