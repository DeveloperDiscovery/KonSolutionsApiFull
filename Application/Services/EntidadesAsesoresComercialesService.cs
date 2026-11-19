using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class EntidadesAsesoresComercialesService : IEntidadesAsesoresComercialesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EntidadesAsesoresComercialesService);
    private readonly IEntidadesAsesoresComercialesRepository _repo;
    private readonly ErrorLogService _log;
    public EntidadesAsesoresComercialesService(IEntidadesAsesoresComercialesRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EntidadesAsesoresComerciales>>> GetAllAsync(decimal codEntidadCliente)
    {
        try { var d = (await _repo.ShowallAsync(codEntidadCliente)).ToList();
              return ApiResponse<IEnumerable<EntidadesAsesoresComerciales>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EntidadesAsesoresComerciales>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EntidadesAsesoresComerciales>> GetByIdAsync(decimal codEntidadCliente, decimal codEntidadAsesorComercial)
    {
        try { var d = await _repo.ShowbyidAsync(codEntidadCliente, codEntidadAsesorComercial);
              return d is null ? ApiResponse<EntidadesAsesoresComerciales>.Fail("No encontrado.") : ApiResponse<EntidadesAsesoresComerciales>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EntidadesAsesoresComerciales>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EntidadesAsesoresComerciales>>> SearchAsync(EntidadesAsesoresComercialesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<EntidadesAsesoresComerciales>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<EntidadesAsesoresComerciales>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EntidadesAsesoresComercialesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal codEntidadCliente, decimal codEntidadAsesorComercial)
    {
        try { var r = await _repo.DeleteAsync(codEntidadCliente, codEntidadAsesorComercial);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
