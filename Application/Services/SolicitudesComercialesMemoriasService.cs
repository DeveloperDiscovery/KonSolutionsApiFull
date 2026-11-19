using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class SolicitudesComercialesMemoriasService : ISolicitudesComercialesMemoriasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(SolicitudesComercialesMemoriasService);
    private readonly ISolicitudesComercialesMemoriasRepository _repo;
    private readonly ErrorLogService _log;
    public SolicitudesComercialesMemoriasService(ISolicitudesComercialesMemoriasRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<SolicitudesComercialesMemorias>>> GetAllAsync(int numSolicitudComercial)
    {
        try { var d = (await _repo.ShowallAsync(numSolicitudComercial)).ToList();
              return ApiResponse<IEnumerable<SolicitudesComercialesMemorias>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<SolicitudesComercialesMemorias>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<SolicitudesComercialesMemorias>> GetByIdAsync(int numSolicitudComercial, short numSecuencia)
    {
        try { var d = await _repo.ShowbyidAsync(numSolicitudComercial, numSecuencia);
              return d is null ? ApiResponse<SolicitudesComercialesMemorias>.Fail("No encontrado.") : ApiResponse<SolicitudesComercialesMemorias>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<SolicitudesComercialesMemorias>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<SolicitudesComercialesMemorias>>> SearchAsync(SolicitudesComercialesMemoriasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<SolicitudesComercialesMemorias>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<SolicitudesComercialesMemorias>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(SolicitudesComercialesMemoriasInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Memoria guardada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int numSolicitudComercial, short numSecuencia)
    {
        try { var r = await _repo.DeleteAsync(numSolicitudComercial, numSecuencia);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Memoria eliminada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
