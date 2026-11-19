using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.Application.Services;

public class CostosCentroCostosProcesosService : ICostosCentroCostosProcesosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(CostosCentroCostosProcesosService);
    private readonly ICostosCentroCostosProcesosRepository _repo;
    private readonly ErrorLogService _log;
    public CostosCentroCostosProcesosService(ICostosCentroCostosProcesosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<CostosCentroCostosProcesos>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<CostosCentroCostosProcesos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<CostosCentroCostosProcesos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<CostosCentroCostosProcesos>> GetByIdAsync(string COD_CENTRO_COSTO, string COD_PROCESO, string COD_SUB_PROCESO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_CENTRO_COSTO, COD_PROCESO, COD_SUB_PROCESO);
              return d is null ? ApiResponse<CostosCentroCostosProcesos>.Fail("No encontrado.") : ApiResponse<CostosCentroCostosProcesos>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<CostosCentroCostosProcesos>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<CostosCentroCostosProcesos>>> SearchAsync(CostosCentroCostosProcesosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<CostosCentroCostosProcesos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<CostosCentroCostosProcesos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(CostosCentroCostosProcesosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(CostosCentroCostosProcesosUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CENTRO_COSTO, string COD_PROCESO, string COD_SUB_PROCESO)
    {
        try { var r = await _repo.DeleteAsync(COD_CENTRO_COSTO, COD_PROCESO, COD_SUB_PROCESO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}