using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class TiposOperacionCalculoService : ITiposOperacionCalculoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TiposOperacionCalculoService);
    private readonly ITiposOperacionCalculoRepository _repo;
    private readonly ErrorLogService _log;
    public TiposOperacionCalculoService(ITiposOperacionCalculoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposOperacionCalculo>>> GetAllAsync(string codClaseOperacion, string codTipoOperacion)
    {
        try { var d = (await _repo.ShowallAsync(codClaseOperacion, codTipoOperacion)).ToList();
              return ApiResponse<IEnumerable<TiposOperacionCalculo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposOperacionCalculo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposOperacionCalculo>> GetByIdAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_VALOR_CALCULO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_CLASE_OPERACION, COD_TIPO_OPERACION, COD_TIPO_VALOR_CALCULO);
              return d is null ? ApiResponse<TiposOperacionCalculo>.Fail("No encontrado.") : ApiResponse<TiposOperacionCalculo>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposOperacionCalculo>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposOperacionCalculo>>> SearchAsync(string codClaseOperacion, string codTipoOperacion, TiposOperacionCalculoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(codClaseOperacion, codTipoOperacion, dto)).ToList();
              return ApiResponse<IEnumerable<TiposOperacionCalculo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TiposOperacionCalculo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposOperacionCalculoInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(TiposOperacionCalculoUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_VALOR_CALCULO)
    {
        try { var r = await _repo.DeleteAsync(COD_CLASE_OPERACION, COD_TIPO_OPERACION, COD_TIPO_VALOR_CALCULO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> CopyAsync(TiposOperacionCalculoCopyDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.CopyAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Copiado correctamente."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(CopyAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}