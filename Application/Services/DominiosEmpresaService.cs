using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Services;

public class DominiosEmpresaService : IDominiosEmpresaService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(DominiosEmpresaService);
    private readonly IDominiosEmpresaRepository _repo;
    private readonly ErrorLogService _log;
    public DominiosEmpresaService(IDominiosEmpresaRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<DominiosEmpresa>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<DominiosEmpresa>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<DominiosEmpresa>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<DominiosEmpresa>> GetByIdAsync(byte COD_DOMINIO_EMPRESA)
    {
        try { var d = await _repo.ShowbyidAsync(COD_DOMINIO_EMPRESA);
              return d is null ? ApiResponse<DominiosEmpresa>.Fail("No encontrado.") : ApiResponse<DominiosEmpresa>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<DominiosEmpresa>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<DominiosEmpresa>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<DominiosEmpresa>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<DominiosEmpresa>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(DominiosEmpresaInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(byte COD_DOMINIO_EMPRESA)
    {
        try { var r = await _repo.DeleteAsync(COD_DOMINIO_EMPRESA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
