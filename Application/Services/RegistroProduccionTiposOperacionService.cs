using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;

namespace KonSolutionsApi.Application.Services;

public class RegistroProduccionTiposOperacionService : IRegistroProduccionTiposOperacionService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(RegistroProduccionTiposOperacionService);
    private readonly IRegistroProduccionTiposOperacionRepository _repo;
    private readonly ErrorLogService _log;
    public RegistroProduccionTiposOperacionService(IRegistroProduccionTiposOperacionRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<RegistroProduccionTiposOperacion>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<RegistroProduccionTiposOperacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<RegistroProduccionTiposOperacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RegistroProduccionTiposOperacion>> GetByIdAsync(int NUM_REGISTRO_PRODUCCION, int NUM_SECUENCIA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_REGISTRO_PRODUCCION, NUM_SECUENCIA);
              return d is null ? ApiResponse<RegistroProduccionTiposOperacion>.Fail("No encontrado.") : ApiResponse<RegistroProduccionTiposOperacion>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<RegistroProduccionTiposOperacion>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<RegistroProduccionTiposOperacion>>> SearchAsync(RegistroProduccionTiposOperacionSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<RegistroProduccionTiposOperacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<RegistroProduccionTiposOperacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(RegistroProduccionTiposOperacionInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(RegistroProduccionTiposOperacionUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_REGISTRO_PRODUCCION, int NUM_SECUENCIA)
    {
        try { var r = await _repo.DeleteAsync(NUM_REGISTRO_PRODUCCION, NUM_SECUENCIA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}