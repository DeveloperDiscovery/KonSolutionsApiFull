using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Services;

public class GruposFamiliasSubFamiliasReglasCodificacionService : IGruposFamiliasSubFamiliasReglasCodificacionService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(GruposFamiliasSubFamiliasReglasCodificacionService);
    private readonly IGruposFamiliasSubFamiliasReglasCodificacionRepository _repo;
    private readonly ErrorLogService _log;
    public GruposFamiliasSubFamiliasReglasCodificacionService(IGruposFamiliasSubFamiliasReglasCodificacionRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<GruposFamiliasSubFamiliasReglasCodificacion>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<GruposFamiliasSubFamiliasReglasCodificacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<GruposFamiliasSubFamiliasReglasCodificacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<GruposFamiliasSubFamiliasReglasCodificacion>> GetByIdAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO, COD_SUB_FAMILIA_PRODUCTO);
              return d is null ? ApiResponse<GruposFamiliasSubFamiliasReglasCodificacion>.Fail("No encontrado.") : ApiResponse<GruposFamiliasSubFamiliasReglasCodificacion>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<GruposFamiliasSubFamiliasReglasCodificacion>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<GruposFamiliasSubFamiliasReglasCodificacion>>> SearchAsync(GruposFamiliasSubFamiliasReglasCodificacionSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<GruposFamiliasSubFamiliasReglasCodificacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<GruposFamiliasSubFamiliasReglasCodificacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(GruposFamiliasSubFamiliasReglasCodificacionInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(GruposFamiliasSubFamiliasReglasCodificacionUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO)
    {
        try { var r = await _repo.DeleteAsync(COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO, COD_SUB_FAMILIA_PRODUCTO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}