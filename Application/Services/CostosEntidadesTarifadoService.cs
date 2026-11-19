using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.Application.Services;

public class CostosEntidadesTarifadoService : ICostosEntidadesTarifadoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(CostosEntidadesTarifadoService);
    private readonly ICostosEntidadesTarifadoRepository _repo;
    private readonly ErrorLogService _log;
    public CostosEntidadesTarifadoService(ICostosEntidadesTarifadoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<CostosEntidadesTarifado>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<CostosEntidadesTarifado>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<CostosEntidadesTarifado>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<CostosEntidadesTarifado>> GetByIdAsync(int NUM_PERIODO_CONTABLE, string COD_PROCESO, string COD_PRODUCTO)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_PERIODO_CONTABLE, COD_PROCESO, COD_PRODUCTO);
              return d is null ? ApiResponse<CostosEntidadesTarifado>.Fail("No encontrado.") : ApiResponse<CostosEntidadesTarifado>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<CostosEntidadesTarifado>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<CostosEntidadesTarifado>>> SearchAsync(CostosEntidadesTarifadoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<CostosEntidadesTarifado>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<CostosEntidadesTarifado>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(CostosEntidadesTarifadoInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(CostosEntidadesTarifadoUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PERIODO_CONTABLE, string COD_PROCESO, string COD_PRODUCTO)
    {
        try { var r = await _repo.DeleteAsync(NUM_PERIODO_CONTABLE, COD_PROCESO, COD_PRODUCTO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}