using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Services;

public class ComprobantesPagoCronogramaService : IComprobantesPagoCronogramaService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ComprobantesPagoCronogramaService);
    private readonly IComprobantesPagoCronogramaRepository _repo;
    private readonly ErrorLogService _log;
    public ComprobantesPagoCronogramaService(IComprobantesPagoCronogramaRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ComprobantesPagoCronograma>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<ComprobantesPagoCronograma>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ComprobantesPagoCronograma>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ComprobantesPagoCronograma>> GetByIdAsync(decimal NUM_REGISTRO, DateTime FEC_VENCIMIENTO)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_REGISTRO, FEC_VENCIMIENTO);
              return d is null ? ApiResponse<ComprobantesPagoCronograma>.Fail("No encontrado.") : ApiResponse<ComprobantesPagoCronograma>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ComprobantesPagoCronograma>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComprobantesPagoCronograma>>> SearchAsync(ComprobantesPagoCronogramaSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<ComprobantesPagoCronograma>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ComprobantesPagoCronograma>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ComprobantesPagoCronogramaInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(ComprobantesPagoCronogramaUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_REGISTRO, DateTime FEC_VENCIMIENTO)
    {
        try { var r = await _repo.DeleteAsync(NUM_REGISTRO, FEC_VENCIMIENTO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}