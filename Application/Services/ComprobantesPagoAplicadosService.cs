using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Services;

public class ComprobantesPagoAplicadosService : IComprobantesPagoAplicadosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ComprobantesPagoAplicadosService);
    private readonly IComprobantesPagoAplicadosRepository _repo;
    private readonly ErrorLogService _log;
    public ComprobantesPagoAplicadosService(IComprobantesPagoAplicadosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ComprobantesPagoAplicados>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<ComprobantesPagoAplicados>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ComprobantesPagoAplicados>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ComprobantesPagoAplicados>> GetByIdAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA, decimal NUM_REGISTRO_ASOCIADO)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_REGISTRO, NUM_SECUENCIA, NUM_REGISTRO_ASOCIADO);
              return d is null ? ApiResponse<ComprobantesPagoAplicados>.Fail("No encontrado.") : ApiResponse<ComprobantesPagoAplicados>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ComprobantesPagoAplicados>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComprobantesPagoAplicados>>> SearchAsync(ComprobantesPagoAplicadosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<ComprobantesPagoAplicados>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ComprobantesPagoAplicados>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ComprobantesPagoAplicadosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA, decimal NUM_REGISTRO_ASOCIADO)
    {
        try { var r = await _repo.DeleteAsync(NUM_REGISTRO, NUM_SECUENCIA, NUM_REGISTRO_ASOCIADO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}