using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Services;

public class AutorizacionesEventosService : IAutorizacionesEventosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(AutorizacionesEventosService);
    private readonly IAutorizacionesEventosRepository _repo;
    private readonly ErrorLogService _log;
    public AutorizacionesEventosService(IAutorizacionesEventosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<AutorizacionesEventos>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<AutorizacionesEventos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<AutorizacionesEventos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<AutorizacionesEventos>> GetByIdAsync(int NUM_AUTORIZACION, int NUM_SECUENCIA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_AUTORIZACION, NUM_SECUENCIA);
              return d is null ? ApiResponse<AutorizacionesEventos>.Fail("No encontrado.") : ApiResponse<AutorizacionesEventos>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<AutorizacionesEventos>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<AutorizacionesEventos>>> SearchAsync(AutorizacionesEventosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<AutorizacionesEventos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<AutorizacionesEventos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(AutorizacionesEventosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_AUTORIZACION, int NUM_SECUENCIA)
    {
        try { var r = await _repo.DeleteAsync(NUM_AUTORIZACION, NUM_SECUENCIA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}