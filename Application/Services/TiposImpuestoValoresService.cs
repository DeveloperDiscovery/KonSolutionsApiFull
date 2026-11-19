using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class TiposImpuestoValoresService : ITiposImpuestoValoresService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TiposImpuestoValoresService);
    private readonly ITiposImpuestoValoresRepository _repo;
    private readonly ErrorLogService _log;
    public TiposImpuestoValoresService(ITiposImpuestoValoresRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposImpuestoValores>>> GetAllAsync(string codTipoImpuesto)
    {
        try { var d = (await _repo.ShowallAsync(codTipoImpuesto)).ToList();
              return ApiResponse<IEnumerable<TiposImpuestoValores>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposImpuestoValores>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposImpuestoValores>> GetByIdAsync(string COD_TIPO_IMPUESTO, DateTime FEC_INICIO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_TIPO_IMPUESTO, FEC_INICIO);
              return d is null ? ApiResponse<TiposImpuestoValores>.Fail("No encontrado.") : ApiResponse<TiposImpuestoValores>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposImpuestoValores>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposImpuestoValores>>> SearchAsync(TiposImpuestoValoresSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<TiposImpuestoValores>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TiposImpuestoValores>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposImpuestoValoresInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(TiposImpuestoValoresUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_IMPUESTO, DateTime FEC_INICIO)
    {
        try { var r = await _repo.DeleteAsync(COD_TIPO_IMPUESTO, FEC_INICIO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}