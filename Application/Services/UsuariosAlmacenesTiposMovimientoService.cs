using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Services;

public class UsuariosAlmacenesTiposMovimientoService : IUsuariosAlmacenesTiposMovimientoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(UsuariosAlmacenesTiposMovimientoService);
    private readonly IUsuariosAlmacenesTiposMovimientoRepository _repo;
    private readonly ErrorLogService _log;
    public UsuariosAlmacenesTiposMovimientoService(IUsuariosAlmacenesTiposMovimientoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<UsuariosAlmacenesTiposMovimiento>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<UsuariosAlmacenesTiposMovimiento>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<UsuariosAlmacenesTiposMovimiento>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<UsuariosAlmacenesTiposMovimiento>> GetByIdAsync(string COD_USUARIO, string COD_ALMACEN, string COD_CLASE_MOVIMIENTO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_USUARIO, COD_ALMACEN, COD_CLASE_MOVIMIENTO);
              return d is null ? ApiResponse<UsuariosAlmacenesTiposMovimiento>.Fail("No encontrado.") : ApiResponse<UsuariosAlmacenesTiposMovimiento>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<UsuariosAlmacenesTiposMovimiento>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<UsuariosAlmacenesTiposMovimiento>>> SearchAsync(UsuariosAlmacenesTiposMovimientoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<UsuariosAlmacenesTiposMovimiento>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<UsuariosAlmacenesTiposMovimiento>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(UsuariosAlmacenesTiposMovimientoInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_USUARIO, string COD_ALMACEN, string COD_CLASE_MOVIMIENTO)
    {
        try { var r = await _repo.DeleteAsync(COD_USUARIO, COD_ALMACEN, COD_CLASE_MOVIMIENTO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}