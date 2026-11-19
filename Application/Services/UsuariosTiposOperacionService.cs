using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Services;

public class UsuariosTiposOperacionService : IUsuariosTiposOperacionService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(UsuariosTiposOperacionService);
    private readonly IUsuariosTiposOperacionRepository _repo;
    private readonly ErrorLogService _log;
    public UsuariosTiposOperacionService(IUsuariosTiposOperacionRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<UsuariosTiposOperacion>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<UsuariosTiposOperacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<UsuariosTiposOperacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<UsuariosTiposOperacion>> GetByIdAsync(string COD_USUARIO, string COD_CLASE_OPERACION, string COD_TIPO_OPERACION)
    {
        try { var d = await _repo.ShowbyidAsync(COD_USUARIO, COD_CLASE_OPERACION, COD_TIPO_OPERACION);
              return d is null ? ApiResponse<UsuariosTiposOperacion>.Fail("No encontrado.") : ApiResponse<UsuariosTiposOperacion>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<UsuariosTiposOperacion>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<UsuariosTiposOperacion>>> SearchAsync(UsuariosTiposOperacionSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<UsuariosTiposOperacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<UsuariosTiposOperacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(UsuariosTiposOperacionInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_USUARIO, string COD_CLASE_OPERACION, string COD_TIPO_OPERACION)
    {
        try { var r = await _repo.DeleteAsync(COD_USUARIO, COD_CLASE_OPERACION, COD_TIPO_OPERACION);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}