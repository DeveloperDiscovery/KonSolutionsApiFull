using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Soporte;

namespace KonSolutionsApi.Application.Services;

public class TicketsAtencionDetallePasosService : ITicketsAtencionDetallePasosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TicketsAtencionDetallePasosService);
    private readonly ITicketsAtencionDetallePasosRepository _repo;
    private readonly ErrorLogService _log;
    public TicketsAtencionDetallePasosService(ITicketsAtencionDetallePasosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TicketsAtencionDetallePasos>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<TicketsAtencionDetallePasos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TicketsAtencionDetallePasos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TicketsAtencionDetallePasos>> GetByIdAsync(int NUM_TICKET, int NUM_SECUENCIA, int NUM_PASO)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_TICKET, NUM_SECUENCIA, NUM_PASO);
              return d is null ? ApiResponse<TicketsAtencionDetallePasos>.Fail("No encontrado.") : ApiResponse<TicketsAtencionDetallePasos>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TicketsAtencionDetallePasos>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TicketsAtencionDetallePasos>>> SearchAsync(TicketsAtencionDetallePasosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<TicketsAtencionDetallePasos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TicketsAtencionDetallePasos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TicketsAtencionDetallePasosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(TicketsAtencionDetallePasosUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado.");  }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_TICKET, int NUM_SECUENCIA, int NUM_PASO)
    {
        try { var r = await _repo.DeleteAsync(NUM_TICKET, NUM_SECUENCIA, NUM_PASO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}