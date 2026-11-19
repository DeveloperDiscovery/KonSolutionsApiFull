using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;

namespace KonSolutionsApi.Application.Services;

public class SubProcesosService : ISubProcesosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(SubProcesosService);
    private readonly ISubProcesosRepository _repo;
    private readonly ErrorLogService _log;
    public SubProcesosService(ISubProcesosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<SubProcesos>>> GetAllAsync(string COD_PROCESO)
    {
        try { var d = (await _repo.ShowallAsync(COD_PROCESO)).ToList();
              return ApiResponse<IEnumerable<SubProcesos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<SubProcesos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<SubProcesos>> GetByIdAsync(string COD_PROCESO, string COD_SUB_PROCESO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_PROCESO, COD_SUB_PROCESO);
              return d is null ? ApiResponse<SubProcesos>.Fail("No encontrado.") : ApiResponse<SubProcesos>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<SubProcesos>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string COD_PROCESO)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(COD_PROCESO)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<SubProcesos>>> SearchAsync(SubProcesosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<SubProcesos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<SubProcesos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(SubProcesosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.Ok(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(SubProcesosUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.Ok(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PROCESO, string COD_SUB_PROCESO)
    {
        try { var r = await _repo.DeleteAsync(COD_PROCESO, COD_SUB_PROCESO);
              return ApiResponse<WriteResult>.Ok(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}