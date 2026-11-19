using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Sunat;

namespace KonSolutionsApi.Application.Services;

public class CubsoSunatService : ICubsoSunatService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(CubsoSunatService);
    private readonly ICubsoSunatRepository _repo;
    private readonly ErrorLogService _log;
    public CubsoSunatService(ICubsoSunatRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<CubsoSunat>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<CubsoSunat>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<CubsoSunat>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<CubsoSunat>> GetByIdAsync(string COD_CUBSO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_CUBSO);
              return d is null ? ApiResponse<CubsoSunat>.Fail("No encontrado.") : ApiResponse<CubsoSunat>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<CubsoSunat>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<CubsoSunat>>> SearchAsync(CubsoSunatSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<CubsoSunat>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<CubsoSunat>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(CubsoSunatInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.Ok(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(CubsoSunatUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.Ok(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CUBSO)
    {
        try { var r = await _repo.DeleteAsync(COD_CUBSO);
              return ApiResponse<WriteResult>.Ok(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UploadJsonAsync(string jsonArray)
    {
        try { var r = await _repo.UploadJsonAsync(jsonArray);
              return ApiResponse<WriteResult>.Ok(r, "Códigos CUBSO cargados."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UploadJsonAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}