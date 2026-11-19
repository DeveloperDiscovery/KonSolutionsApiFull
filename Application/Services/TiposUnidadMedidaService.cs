using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class TiposUnidadMedidaService : ITiposUnidadMedidaService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TiposUnidadMedidaService);
    private readonly ITiposUnidadMedidaRepository _repo;
    private readonly ErrorLogService _log;
    public TiposUnidadMedidaService(ITiposUnidadMedidaRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposUnidadMedida>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<TiposUnidadMedida>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposUnidadMedida>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposUnidadMedida>> GetByIdAsync(string COD_TIPO_UNIDAD_MEDIDA)
    {
        try { var d = await _repo.ShowbyidAsync(COD_TIPO_UNIDAD_MEDIDA);
              return d is null ? ApiResponse<TiposUnidadMedida>.Fail("No encontrado.") : ApiResponse<TiposUnidadMedida>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposUnidadMedida>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposUnidadMedida>>> SearchAsync(TiposUnidadMedidaSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<TiposUnidadMedida>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TiposUnidadMedida>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposUnidadMedidaInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(string id, TiposUnidadMedidaInsertDto dto)
    {
        try { dto.COD_TIPO_UNIDAD_MEDIDA = id;
              var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro actualizado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_UNIDAD_MEDIDA)
    {
        try { var r = await _repo.DeleteAsync(COD_TIPO_UNIDAD_MEDIDA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UploadJsonAsync(string jsonArray)
    {
        try { var r = await _repo.UploadJsonAsync(jsonArray);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Tipos de magnitud cargados."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UploadJsonAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}