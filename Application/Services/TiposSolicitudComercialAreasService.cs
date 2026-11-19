using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class TiposSolicitudComercialAreasService : ITiposSolicitudComercialAreasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase    = nameof(TiposSolicitudComercialAreasService);

    private readonly ITiposSolicitudComercialAreasRepository _repo;
    private readonly ErrorLogService _log;

    public TiposSolicitudComercialAreasService(ITiposSolicitudComercialAreasRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposSolicitudComercialAreas>>> GetAllAsync(string codTipoSolicitudComercial)
    {
        try { var d = (await _repo.ShowallAsync(codTipoSolicitudComercial)).ToList();
              return ApiResponse<IEnumerable<TiposSolicitudComercialAreas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposSolicitudComercialAreas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposSolicitudComercialAreas>> GetByIdAsync(string codTipoSolicitudComercial, int codArea)
    {
        try { var d = await _repo.ShowbyidAsync(codTipoSolicitudComercial, codArea);
              return d is null ? ApiResponse<TiposSolicitudComercialAreas>.Fail("No encontrado.") : ApiResponse<TiposSolicitudComercialAreas>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposSolicitudComercialAreas>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposSolicitudComercialAreas>>> GetComboboxAsync(string codTipoSolicitudComercial)
    {
        try { var d = (await _repo.ShowcomboxAsync(codTipoSolicitudComercial)).ToList();
              return ApiResponse<IEnumerable<TiposSolicitudComercialAreas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<TiposSolicitudComercialAreas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposSolicitudComercialAreas>>> SearchAsync(TiposSolicitudComercialAreasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<TiposSolicitudComercialAreas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TiposSolicitudComercialAreas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposSolicitudComercialAreasInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Área asignada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string codTipoSolicitudComercial, int codArea)
    {
        try { var r = await _repo.DeleteAsync(codTipoSolicitudComercial, codArea);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Área quitada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
