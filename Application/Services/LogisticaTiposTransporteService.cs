using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class LogisticaTiposTransporteService : ILogisticaTiposTransporteService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(LogisticaTiposTransporteService);
    private readonly ILogisticaTiposTransporteRepository _repo;
    private readonly ErrorLogService _log;
    public LogisticaTiposTransporteService(ILogisticaTiposTransporteRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<LogisticaTiposTransporte>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<LogisticaTiposTransporte>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<LogisticaTiposTransporte>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<LogisticaTiposTransporte>> GetByIdAsync(string COD_TIPO_TRANSPORTE)
    {
        try { var d = await _repo.ShowbyidAsync(COD_TIPO_TRANSPORTE);
              return d is null ? ApiResponse<LogisticaTiposTransporte>.Fail("No encontrado.") : ApiResponse<LogisticaTiposTransporte>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<LogisticaTiposTransporte>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<LogisticaTiposTransporte>>> SearchAsync(LogisticaTiposTransporteSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<LogisticaTiposTransporte>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<LogisticaTiposTransporte>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(LogisticaTiposTransporteInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(LogisticaTiposTransporteUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro actualizado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_TRANSPORTE)
    {
        try { var r = await _repo.DeleteAsync(COD_TIPO_TRANSPORTE);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}