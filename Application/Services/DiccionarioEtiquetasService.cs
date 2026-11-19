using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Services;

public class DiccionarioEtiquetasService : IDiccionarioEtiquetasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(DiccionarioEtiquetasService);
    private readonly IDiccionarioEtiquetasRepository _repo;
    private readonly ErrorLogService _log;
    public DiccionarioEtiquetasService(IDiccionarioEtiquetasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<DiccionarioEtiquetas>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<DiccionarioEtiquetas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<DiccionarioEtiquetas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<DiccionarioEtiquetas>> GetByIdAsync(string codEtiqueta)
    {
        try { var d = await _repo.ShowbyidAsync(codEtiqueta);
              return d is null ? ApiResponse<DiccionarioEtiquetas>.Fail("No encontrado.") : ApiResponse<DiccionarioEtiquetas>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<DiccionarioEtiquetas>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(DiccionarioEtiquetasInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string codEtiqueta)
    {
        try { var r = await _repo.DeleteAsync(codEtiqueta);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
