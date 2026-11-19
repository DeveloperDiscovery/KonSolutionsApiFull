using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class EmbaladoEmbalajeDetalleService : IEmbaladoEmbalajeDetalleService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EmbaladoEmbalajeDetalleService);
    private readonly IEmbaladoEmbalajeDetalleRepository _repo;
    private readonly ErrorLogService _log;
    public EmbaladoEmbalajeDetalleService(IEmbaladoEmbalajeDetalleRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EmbaladoEmbalajeDetalle>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<EmbaladoEmbalajeDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EmbaladoEmbalajeDetalle>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EmbaladoEmbalajeDetalle>> GetByIdAsync(decimal NUM_EMBALADO, int NUM_SECUENCIA_EMBALAJE)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_EMBALADO, NUM_SECUENCIA_EMBALAJE);
              return d is null ? ApiResponse<EmbaladoEmbalajeDetalle>.Fail("No encontrado.") : ApiResponse<EmbaladoEmbalajeDetalle>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EmbaladoEmbalajeDetalle>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EmbaladoEmbalajeDetalle>>> SearchAsync(EmbaladoEmbalajeDetalleSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<EmbaladoEmbalajeDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<EmbaladoEmbalajeDetalle>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EmbaladoEmbalajeDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(EmbaladoEmbalajeDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_EMBALADO, int NUM_SECUENCIA_EMBALAJE)
    {
        try { var r = await _repo.DeleteAsync(NUM_EMBALADO, NUM_SECUENCIA_EMBALAJE);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}