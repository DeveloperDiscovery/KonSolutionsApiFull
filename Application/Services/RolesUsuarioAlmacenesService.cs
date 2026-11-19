using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Services;

public class RolesUsuarioAlmacenesService : IRolesUsuarioAlmacenesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(RolesUsuarioAlmacenesService);
    private readonly IRolesUsuarioAlmacenesRepository _repo;
    private readonly ErrorLogService _log;
    public RolesUsuarioAlmacenesService(IRolesUsuarioAlmacenesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<RolesUsuarioAlmacenes>>> GetAllAsync(string COD_ROL_USUARIO)
    {
        try { var d = (await _repo.ShowallAsync(COD_ROL_USUARIO)).ToList();
              return ApiResponse<IEnumerable<RolesUsuarioAlmacenes>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<RolesUsuarioAlmacenes>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<RolesUsuarioAlmacenes>>> GetDisponiblesAsync(string COD_ROL_USUARIO)
    {
        try { var d = (await _repo.DisponiblesAsync(COD_ROL_USUARIO)).ToList();
              return ApiResponse<IEnumerable<RolesUsuarioAlmacenes>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetDisponiblesAsync));
              return ApiResponse<IEnumerable<RolesUsuarioAlmacenes>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RolesUsuarioAlmacenes>> GetByIdAsync(string COD_ROL_USUARIO, string COD_ALMACEN)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ROL_USUARIO, COD_ALMACEN);
              return d is null ? ApiResponse<RolesUsuarioAlmacenes>.Fail("No encontrado.") : ApiResponse<RolesUsuarioAlmacenes>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<RolesUsuarioAlmacenes>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string COD_ROL_USUARIO)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(COD_ROL_USUARIO)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<RolesUsuarioAlmacenes>>> SearchAsync(RolesUsuarioAlmacenesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<RolesUsuarioAlmacenes>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<RolesUsuarioAlmacenes>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(RolesUsuarioAlmacenesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Almacén asignado al rol."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ROL_USUARIO, string COD_ALMACEN)
    {
        try { var r = await _repo.DeleteAsync(COD_ROL_USUARIO, COD_ALMACEN);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Almacén quitado del rol."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    /// <summary>Asignación en bloque.
    ///
    /// Cada almacén se procesa por separado y un fallo NO corta el lote: si el 7 de 20 da
    /// error, los otros 19 igual quedan asignados y la respuesta dice cuál falló. Abortar al
    /// primer error dejaría el rol a medio configurar y sin rastro de dónde se cortó.</summary>
    public async Task<ApiResponse<RolesUsuarioAlmacenesLoteResult>> AsignarAsync(RolesUsuarioAlmacenesLoteDto dto, string codUsuario, string codEstacion)
    {
        var res = new RolesUsuarioAlmacenesLoteResult();
        foreach (var alm in dto.Almacenes.Distinct())
        {
            try
            {
                await _repo.InsertAsync(new RolesUsuarioAlmacenesInsertDto
                {
                    COD_ROL_USUARIO = dto.COD_ROL_USUARIO,
                    COD_ALMACEN = alm
                }, codUsuario, codEstacion);
                res.Procesados++;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, Programa, Clase, nameof(AsignarAsync));
                res.Fallidos.Add(alm);
                res.DesError ??= ex.Message;
            }
        }
        return ApiResponse<RolesUsuarioAlmacenesLoteResult>.Ok(res);
    }

    /// <summary>Baja en bloque. Mismo criterio: quitar un almacén puede ser rechazado si el rol
    /// todavía tiene tipos de movimiento colgando de él, y el usuario necesita saber cuáles
    /// quedaron.</summary>
    public async Task<ApiResponse<RolesUsuarioAlmacenesLoteResult>> DesasignarAsync(RolesUsuarioAlmacenesLoteDto dto)
    {
        var res = new RolesUsuarioAlmacenesLoteResult();
        foreach (var alm in dto.Almacenes.Distinct())
        {
            try { await _repo.DeleteAsync(dto.COD_ROL_USUARIO, alm); res.Procesados++; }
            catch (Exception ex)
            {
                _log.LogError(ex, Programa, Clase, nameof(DesasignarAsync));
                res.Fallidos.Add(alm);
                res.DesError ??= ex.Message;
            }
        }
        return ApiResponse<RolesUsuarioAlmacenesLoteResult>.Ok(res);
    }
}
