using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class EntidadesTiposMedioContactoService : IEntidadesTiposMedioContactoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EntidadesTiposMedioContactoService);
    private readonly IEntidadesTiposMedioContactoRepository _repo;
    private readonly ErrorLogService _log;
    public EntidadesTiposMedioContactoService(IEntidadesTiposMedioContactoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EntidadesTiposMedioContacto>>> GetAllAsync(decimal COD_ENTIDAD)
    {
        try { var d = (await _repo.ShowallAsync(COD_ENTIDAD)).ToList();
              return ApiResponse<IEnumerable<EntidadesTiposMedioContacto>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EntidadesTiposMedioContacto>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EntidadesTiposMedioContacto>> GetByIdAsync(decimal COD_ENTIDAD, short NUM_SECUENCIA)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ENTIDAD, NUM_SECUENCIA);
              return d is null ? ApiResponse<EntidadesTiposMedioContacto>.Fail("No encontrado.") : ApiResponse<EntidadesTiposMedioContacto>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EntidadesTiposMedioContacto>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EntidadesTiposMedioContacto>>> SearchAsync(EntidadesTiposMedioContactoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<EntidadesTiposMedioContacto>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<EntidadesTiposMedioContacto>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EntidadesTiposMedioContactoInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD, short NUM_SECUENCIA)
    {
        try { var r = await _repo.DeleteAsync(COD_ENTIDAD, NUM_SECUENCIA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}
