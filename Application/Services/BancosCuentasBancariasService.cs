using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Services;

public class BancosCuentasBancariasService : IBancosCuentasBancariasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(BancosCuentasBancariasService);
    private readonly IBancosCuentasBancariasRepository _repo;
    private readonly ErrorLogService _log;
    public BancosCuentasBancariasService(IBancosCuentasBancariasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<BancosCuentasBancarias>>> GetAllAsync(string codBanco)
    {
        try { var d = (await _repo.ShowallAsync(codBanco)).ToList();
              return ApiResponse<IEnumerable<BancosCuentasBancarias>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<BancosCuentasBancarias>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<BancosCuentasBancarias>> GetByIdAsync(string COD_BANCO, string NUM_CUENTA_BANCARIA)
    {
        try { var d = await _repo.ShowbyidAsync(COD_BANCO, NUM_CUENTA_BANCARIA);
              return d is null ? ApiResponse<BancosCuentasBancarias>.Fail("No encontrado.") : ApiResponse<BancosCuentasBancarias>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<BancosCuentasBancarias>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<BancosCuentasBancarias>>> SearchAsync(BancosCuentasBancariasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<BancosCuentasBancarias>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<BancosCuentasBancarias>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(BancosCuentasBancariasInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(BancosCuentasBancariasUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_BANCO, string NUM_CUENTA_BANCARIA)
    {
        try { var r = await _repo.DeleteAsync(COD_BANCO, NUM_CUENTA_BANCARIA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}