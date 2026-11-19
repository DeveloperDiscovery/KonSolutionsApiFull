using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class EntidadesUnidadesComercialesService : IEntidadesUnidadesComercialesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EntidadesUnidadesComercialesService);
    private readonly IEntidadesUnidadesComercialesRepository _repo;
    private readonly ErrorLogService _log;
    public EntidadesUnidadesComercialesService(IEntidadesUnidadesComercialesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EntidadesUnidadesComerciales>>> GetAllAsync(decimal codEntidad)
    {
        try { var d = (await _repo.ShowallAsync(codEntidad)).ToList();
              return ApiResponse<IEnumerable<EntidadesUnidadesComerciales>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EntidadesUnidadesComerciales>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EntidadesUnidadesComerciales>> GetByIdAsync(decimal codEntidad, byte codUnidadComercial)
    {
        try { var d = await _repo.ShowbyidAsync(codEntidad, codUnidadComercial);
              return d is null ? ApiResponse<EntidadesUnidadesComerciales>.Fail("No encontrado.") : ApiResponse<EntidadesUnidadesComerciales>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EntidadesUnidadesComerciales>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EntidadesUnidadesComercialesInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Unidad comercial guardada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesUnidadesComercialesUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Unidad comercial actualizada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal codEntidad, byte codUnidadComercial)
    {
        try { var r = await _repo.DeleteAsync(codEntidad, codUnidadComercial);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Unidad comercial eliminada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
