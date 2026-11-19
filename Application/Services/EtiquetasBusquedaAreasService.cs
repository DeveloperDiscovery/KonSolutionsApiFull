using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class EtiquetasBusquedaAreasService : IEtiquetasBusquedaAreasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EtiquetasBusquedaAreasService);
    private readonly IEtiquetasBusquedaAreasRepository _repo;
    private readonly ErrorLogService _log;
    public EtiquetasBusquedaAreasService(IEtiquetasBusquedaAreasRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EtiquetasBusquedaAreas>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<EtiquetasBusquedaAreas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EtiquetasBusquedaAreas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EtiquetasBusquedaAreas>> GetByIdAsync(string codEtiquetaBusqueda)
    {
        try { var d = await _repo.ShowbyidAsync(codEtiquetaBusqueda);
              return d is null ? ApiResponse<EtiquetasBusquedaAreas>.Fail("No encontrado.") : ApiResponse<EtiquetasBusquedaAreas>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EtiquetasBusquedaAreas>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EtiquetasBusquedaAreasInsertDto dto, string codUsuario, string codEstacion)
    {
        try { dto.COD_USUARIO_REGISTRO = codUsuario; dto.COD_ESTACION_REGISTRO = codEstacion;
              var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string codEtiquetaBusqueda)
    {
        try { var r = await _repo.DeleteAsync(codEtiquetaBusqueda);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
