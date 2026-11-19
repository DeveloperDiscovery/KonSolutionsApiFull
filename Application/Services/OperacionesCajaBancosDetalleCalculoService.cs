using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Services;

public class OperacionesCajaBancosDetalleCalculoService : IOperacionesCajaBancosDetalleCalculoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(OperacionesCajaBancosDetalleCalculoService);
    private readonly IOperacionesCajaBancosDetalleCalculoRepository _repo;
    private readonly ErrorLogService _log;
    public OperacionesCajaBancosDetalleCalculoService(IOperacionesCajaBancosDetalleCalculoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<OperacionesCajaBancosDetalleCalculo>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<OperacionesCajaBancosDetalleCalculo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<OperacionesCajaBancosDetalleCalculo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<OperacionesCajaBancosDetalleCalculo>> GetByIdAsync(decimal NUM_OPERACION, int NUM_SECUENCIA, string COD_TIPO_VALOR_CALCULO)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_OPERACION, NUM_SECUENCIA, COD_TIPO_VALOR_CALCULO);
              return d is null ? ApiResponse<OperacionesCajaBancosDetalleCalculo>.Fail("No encontrado.") : ApiResponse<OperacionesCajaBancosDetalleCalculo>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<OperacionesCajaBancosDetalleCalculo>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<OperacionesCajaBancosDetalleCalculo>>> SearchAsync(OperacionesCajaBancosDetalleCalculoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<OperacionesCajaBancosDetalleCalculo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<OperacionesCajaBancosDetalleCalculo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(OperacionesCajaBancosDetalleCalculoInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(OperacionesCajaBancosDetalleCalculoUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_OPERACION, int NUM_SECUENCIA, string COD_TIPO_VALOR_CALCULO)
    {
        try { var r = await _repo.DeleteAsync(NUM_OPERACION, NUM_SECUENCIA, COD_TIPO_VALOR_CALCULO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}