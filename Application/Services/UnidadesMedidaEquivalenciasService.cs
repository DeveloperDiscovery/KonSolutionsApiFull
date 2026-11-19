using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class UnidadesMedidaEquivalenciasService : IUnidadesMedidaEquivalenciasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(UnidadesMedidaEquivalenciasService);
    private readonly IUnidadesMedidaEquivalenciasRepository _repo;
    private readonly ErrorLogService _log;
    public UnidadesMedidaEquivalenciasService(IUnidadesMedidaEquivalenciasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<UnidadesMedidaEquivalencias>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<UnidadesMedidaEquivalencias>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<UnidadesMedidaEquivalencias>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<UnidadesMedidaEquivalencias>> GetByIdAsync(string COD_UNIDAD_MEDIDA, string COD_UNIDAD_MEDIDA_EQUIVALENTE)
    {
        try { var d = await _repo.ShowbyidAsync(COD_UNIDAD_MEDIDA, COD_UNIDAD_MEDIDA_EQUIVALENTE);
              return d is null ? ApiResponse<UnidadesMedidaEquivalencias>.Fail("No encontrado.") : ApiResponse<UnidadesMedidaEquivalencias>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<UnidadesMedidaEquivalencias>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<UnidadesMedidaEquivalencias>>> SearchAsync(UnidadesMedidaEquivalenciasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<UnidadesMedidaEquivalencias>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<UnidadesMedidaEquivalencias>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<UnidadesMedidaEquivalencias>>> GetByTipoAsync(string COD_TIPO_UNIDAD_MEDIDA)
    {
        try { var d = (await _repo.GetByTipoAsync(COD_TIPO_UNIDAD_MEDIDA)).ToList();
              return ApiResponse<IEnumerable<UnidadesMedidaEquivalencias>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByTipoAsync));
              return ApiResponse<IEnumerable<UnidadesMedidaEquivalencias>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpsertAsync(UnidadesMedidaEquivalenciasInsertDto dto)
    {
        try { var r = await _repo.UpsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Equivalencia guardada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(UnidadesMedidaEquivalenciasInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(UnidadesMedidaEquivalenciasUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_UNIDAD_MEDIDA, string COD_UNIDAD_MEDIDA_EQUIVALENTE)
    {
        try { var r = await _repo.DeleteAsync(COD_UNIDAD_MEDIDA, COD_UNIDAD_MEDIDA_EQUIVALENTE);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> CargarEstandarAsync(string COD_TIPO_UNIDAD_MEDIDA)
    {
        try { var r = await _repo.CargarEstandarAsync(COD_TIPO_UNIDAD_MEDIDA);
              return ApiResponse<WriteResult>.FromWriteResult(r, $"Se cargaron {r.AffectedRows} equivalencias estándar."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(CargarEstandarAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}