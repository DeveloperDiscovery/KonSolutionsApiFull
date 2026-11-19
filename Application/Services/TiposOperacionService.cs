using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class TiposOperacionService : ITiposOperacionService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TiposOperacionService);
    private readonly ITiposOperacionRepository _repo;
    private readonly ErrorLogService _log;
    public TiposOperacionService(ITiposOperacionRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposOperacion>>> GetAllAsync(string codClaseOperacion)
    {
        try { var d = (await _repo.ShowallAsync(codClaseOperacion)).ToList();
              return ApiResponse<IEnumerable<TiposOperacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposOperacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposOperacion>> GetByIdAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION)
    {
        try { var d = await _repo.ShowbyidAsync(COD_CLASE_OPERACION, COD_TIPO_OPERACION);
              return d is null ? ApiResponse<TiposOperacion>.Fail("No encontrado.") : ApiResponse<TiposOperacion>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposOperacion>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposOperacion>>> SearchAsync(string codClaseOperacion, TiposOperacionSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(codClaseOperacion, dto)).ToList();
              return ApiResponse<IEnumerable<TiposOperacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TiposOperacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposOperacionInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(TiposOperacionUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION)
    {
        try { var r = await _repo.DeleteAsync(COD_CLASE_OPERACION, COD_TIPO_OPERACION);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposOperacionLoadCombosResult>> LoadcombosAsync(int codTipoEstadoClasesOperacion)
    {
        try { return ApiResponse<TiposOperacionLoadCombosResult>.Ok(await _repo.LoadcombosAsync(codTipoEstadoClasesOperacion)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(LoadcombosAsync));
              return ApiResponse<TiposOperacionLoadCombosResult>.Fail(ex.Message); }
    }

}