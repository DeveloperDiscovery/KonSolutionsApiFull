using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;

namespace KonSolutionsApi.Application.Services;

public class SaldosCuentaCorrienteService : ISaldosCuentaCorrienteService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(SaldosCuentaCorrienteService);
    private readonly ISaldosCuentaCorrienteRepository _repo;
    private readonly ErrorLogService _log;
    public SaldosCuentaCorrienteService(ISaldosCuentaCorrienteRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<SaldosCuentaCorriente>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<SaldosCuentaCorriente>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<SaldosCuentaCorriente>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<SaldosCuentaCorriente>> GetByIdAsync(int NUM_PERIODO_CONTABLE, decimal COD_ENTIDAD_CONTABLE, string COD_CUENTA_CONTABLE)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_PERIODO_CONTABLE, COD_ENTIDAD_CONTABLE, COD_CUENTA_CONTABLE);
              return d is null ? ApiResponse<SaldosCuentaCorriente>.Fail("No encontrado.") : ApiResponse<SaldosCuentaCorriente>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<SaldosCuentaCorriente>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<SaldosCuentaCorriente>>> SearchAsync(SaldosCuentaCorrienteSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<SaldosCuentaCorriente>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<SaldosCuentaCorriente>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(SaldosCuentaCorrienteInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(SaldosCuentaCorrienteUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PERIODO_CONTABLE, decimal COD_ENTIDAD_CONTABLE, string COD_CUENTA_CONTABLE)
    {
        try { var r = await _repo.DeleteAsync(NUM_PERIODO_CONTABLE, COD_ENTIDAD_CONTABLE, COD_CUENTA_CONTABLE);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}