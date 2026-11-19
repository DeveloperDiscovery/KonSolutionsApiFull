using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class EntidadesProductosTiposPreciosService : IEntidadesProductosTiposPreciosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EntidadesProductosTiposPreciosService);
    private readonly IEntidadesProductosTiposPreciosRepository _repo;
    private readonly ErrorLogService _log;
    public EntidadesProductosTiposPreciosService(IEntidadesProductosTiposPreciosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EntidadesProductosTiposPrecios>>> GetAllAsync(decimal codEntidad, string codProducto, int? codTipoPrecio = null)
    {
        try { var d = (await _repo.ShowallAsync(codEntidad, codProducto, codTipoPrecio)).ToList();
              return ApiResponse<IEnumerable<EntidadesProductosTiposPrecios>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EntidadesProductosTiposPrecios>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EntidadesProductosTiposPrecios>> GetByIdAsync(decimal COD_ENTIDAD, string COD_PRODUCTO, int COD_TIPO_PRECIO, string COD_MONEDA)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ENTIDAD, COD_PRODUCTO, COD_TIPO_PRECIO, COD_MONEDA);
              return d is null ? ApiResponse<EntidadesProductosTiposPrecios>.Fail("No encontrado.") : ApiResponse<EntidadesProductosTiposPrecios>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EntidadesProductosTiposPrecios>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EntidadesProductosTiposPrecios>>> SearchAsync(EntidadesProductosTiposPreciosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<EntidadesProductosTiposPrecios>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<EntidadesProductosTiposPrecios>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EntidadesProductosTiposPreciosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesProductosTiposPreciosUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD, string COD_PRODUCTO, int COD_TIPO_PRECIO, string COD_MONEDA)
    {
        try { var r = await _repo.DeleteAsync(COD_ENTIDAD, COD_PRODUCTO, COD_TIPO_PRECIO, COD_MONEDA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}