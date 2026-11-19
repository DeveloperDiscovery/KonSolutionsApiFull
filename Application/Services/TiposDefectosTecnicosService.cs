using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Controlcalidad;

namespace KonSolutionsApi.Application.Services;

public class TiposDefectosTecnicosService : ITiposDefectosTecnicosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TiposDefectosTecnicosService);
    private readonly ITiposDefectosTecnicosRepository _repo;
    private readonly ErrorLogService _log;
    public TiposDefectosTecnicosService(ITiposDefectosTecnicosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposDefectosTecnicos>>> GetAllAsync(string COD_CLASE_DEFECTO_TECNICO)
    {
        try { var d = (await _repo.ShowallAsync(COD_CLASE_DEFECTO_TECNICO)).ToList();
              return ApiResponse<IEnumerable<TiposDefectosTecnicos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposDefectosTecnicos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposDefectosTecnicos>> GetByIdAsync(string COD_CLASE_DEFECTO_TECNICO, string COD_TIPO_DEFECTO_TECNICO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_CLASE_DEFECTO_TECNICO, COD_TIPO_DEFECTO_TECNICO);
              return d is null ? ApiResponse<TiposDefectosTecnicos>.Fail("No encontrado.") : ApiResponse<TiposDefectosTecnicos>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposDefectosTecnicos>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposDefectosTecnicos>>> SearchAsync(TiposDefectosTecnicosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<TiposDefectosTecnicos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TiposDefectosTecnicos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposDefectosTecnicosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(TiposDefectosTecnicosUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_DEFECTO_TECNICO, string COD_TIPO_DEFECTO_TECNICO)
    {
        try { var r = await _repo.DeleteAsync(COD_CLASE_DEFECTO_TECNICO, COD_TIPO_DEFECTO_TECNICO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}