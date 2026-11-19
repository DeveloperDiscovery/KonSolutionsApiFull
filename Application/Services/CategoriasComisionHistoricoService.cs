using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Networking;

namespace KonSolutionsApi.Application.Services;

public class CategoriasComisionHistoricoService : ICategoriasComisionHistoricoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(CategoriasComisionHistoricoService);
    private readonly ICategoriasComisionHistoricoRepository _repo;
    private readonly ErrorLogService _log;
    public CategoriasComisionHistoricoService(ICategoriasComisionHistoricoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<CategoriasComisionHistorico>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<CategoriasComisionHistorico>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<CategoriasComisionHistorico>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<CategoriasComisionHistorico>> GetByIdAsync(string COD_CATEGORIA_COMISION, int NUM_SECUENCIA)
    {
        try { var d = await _repo.ShowbyidAsync(COD_CATEGORIA_COMISION, NUM_SECUENCIA);
              return d is null ? ApiResponse<CategoriasComisionHistorico>.Fail("No encontrado.") : ApiResponse<CategoriasComisionHistorico>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<CategoriasComisionHistorico>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<CategoriasComisionHistorico>>> SearchAsync(CategoriasComisionHistoricoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<CategoriasComisionHistorico>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<CategoriasComisionHistorico>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(CategoriasComisionHistoricoInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(CategoriasComisionHistoricoUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CATEGORIA_COMISION, int NUM_SECUENCIA)
    {
        try { var r = await _repo.DeleteAsync(COD_CATEGORIA_COMISION, NUM_SECUENCIA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}