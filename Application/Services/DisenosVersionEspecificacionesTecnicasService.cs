using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Services;

public class DisenosVersionEspecificacionesTecnicasService : IDisenosVersionEspecificacionesTecnicasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(DisenosVersionEspecificacionesTecnicasService);
    private readonly IDisenosVersionEspecificacionesTecnicasRepository _repo;
    private readonly ErrorLogService _log;
    public DisenosVersionEspecificacionesTecnicasService(IDisenosVersionEspecificacionesTecnicasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<DisenosVersionEspecificacionesTecnicas>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<DisenosVersionEspecificacionesTecnicas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<DisenosVersionEspecificacionesTecnicas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<DisenosVersionEspecificacionesTecnicas>> GetByIdAsync(string COD_DISENO, int NUM_VERSION, string COD_ESPECIFICACION_TECNICA)
    {
        try { var d = await _repo.ShowbyidAsync(COD_DISENO, NUM_VERSION, COD_ESPECIFICACION_TECNICA);
              return d is null ? ApiResponse<DisenosVersionEspecificacionesTecnicas>.Fail("No encontrado.") : ApiResponse<DisenosVersionEspecificacionesTecnicas>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<DisenosVersionEspecificacionesTecnicas>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<DisenosVersionEspecificacionesTecnicas>>> SearchAsync(DisenosVersionEspecificacionesTecnicasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<DisenosVersionEspecificacionesTecnicas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<DisenosVersionEspecificacionesTecnicas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(DisenosVersionEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(DisenosVersionEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_DISENO, int NUM_VERSION, string COD_ESPECIFICACION_TECNICA)
    {
        try { var r = await _repo.DeleteAsync(COD_DISENO, NUM_VERSION, COD_ESPECIFICACION_TECNICA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}