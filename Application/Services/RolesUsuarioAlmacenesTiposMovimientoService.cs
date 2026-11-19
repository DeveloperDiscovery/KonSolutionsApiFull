using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Services;

public class RolesUsuarioAlmacenesTiposMovimientoService : IRolesUsuarioAlmacenesTiposMovimientoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(RolesUsuarioAlmacenesTiposMovimientoService);
    private readonly IRolesUsuarioAlmacenesTiposMovimientoRepository _repo;
    private readonly ErrorLogService _log;
    public RolesUsuarioAlmacenesTiposMovimientoService(IRolesUsuarioAlmacenesTiposMovimientoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<RolesUsuarioAlmacenesTiposMovimiento>>> GetAllAsync(string COD_ROL_USUARIO, string COD_ALMACEN)
    {
        try { var d = (await _repo.ShowallAsync(COD_ROL_USUARIO, COD_ALMACEN)).ToList();
              return ApiResponse<IEnumerable<RolesUsuarioAlmacenesTiposMovimiento>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<RolesUsuarioAlmacenesTiposMovimiento>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<RolesUsuarioAlmacenesTiposMovimiento>>> GetDisponiblesAsync(string COD_ROL_USUARIO, string COD_ALMACEN)
    {
        try { var d = (await _repo.DisponiblesAsync(COD_ROL_USUARIO, COD_ALMACEN)).ToList();
              return ApiResponse<IEnumerable<RolesUsuarioAlmacenesTiposMovimiento>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetDisponiblesAsync));
              return ApiResponse<IEnumerable<RolesUsuarioAlmacenesTiposMovimiento>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RolesUsuarioAlmacenesTiposMovimiento>> GetByIdAsync(string COD_ROL_USUARIO, string COD_ALMACEN, string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ROL_USUARIO, COD_ALMACEN, COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO);
              return d is null ? ApiResponse<RolesUsuarioAlmacenesTiposMovimiento>.Fail("No encontrado.") : ApiResponse<RolesUsuarioAlmacenesTiposMovimiento>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<RolesUsuarioAlmacenesTiposMovimiento>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string COD_ROL_USUARIO, string COD_ALMACEN)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(COD_ROL_USUARIO, COD_ALMACEN)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<RolesUsuarioAlmacenesTiposMovimiento>>> SearchAsync(RolesUsuarioAlmacenesTiposMovimientoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<RolesUsuarioAlmacenesTiposMovimiento>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<RolesUsuarioAlmacenesTiposMovimiento>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(RolesUsuarioAlmacenesTiposMovimientoInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Tipo de movimiento asignado al rol."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ROL_USUARIO, string COD_ALMACEN, string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO)
    {
        try { var r = await _repo.DeleteAsync(COD_ROL_USUARIO, COD_ALMACEN, COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Tipo de movimiento quitado del rol."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    /// <summary>Lote de pares clase+tipo. Un fallo no corta el resto; el fallido se reporta como
    /// "clase/tipo" para que se pueda identificar en la grilla.</summary>
    public async Task<ApiResponse<RolesUsuarioAlmacenesTiposMovimientoLoteResult>> AsignarAsync(RolesUsuarioAlmacenesTiposMovimientoLoteDto dto, string codUsuario, string codEstacion)
    {
        var res = new RolesUsuarioAlmacenesTiposMovimientoLoteResult();
        foreach (var t in Unicos(dto))
        {
            try
            {
                await _repo.InsertAsync(new RolesUsuarioAlmacenesTiposMovimientoInsertDto
                {
                    COD_ROL_USUARIO = dto.COD_ROL_USUARIO,
                    COD_ALMACEN = dto.COD_ALMACEN,
                    COD_CLASE_MOVIMIENTO = t.COD_CLASE_MOVIMIENTO,
                    COD_TIPO_MOVIMIENTO = t.COD_TIPO_MOVIMIENTO
                }, codUsuario, codEstacion);
                res.Procesados++;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, Programa, Clase, nameof(AsignarAsync));
                res.Fallidos.Add($"{t.COD_CLASE_MOVIMIENTO}/{t.COD_TIPO_MOVIMIENTO}");
                res.DesError ??= ex.Message;
            }
        }
        return ApiResponse<RolesUsuarioAlmacenesTiposMovimientoLoteResult>.Ok(res);
    }

    public async Task<ApiResponse<RolesUsuarioAlmacenesTiposMovimientoLoteResult>> DesasignarAsync(RolesUsuarioAlmacenesTiposMovimientoLoteDto dto)
    {
        var res = new RolesUsuarioAlmacenesTiposMovimientoLoteResult();
        foreach (var t in Unicos(dto))
        {
            try { await _repo.DeleteAsync(dto.COD_ROL_USUARIO, dto.COD_ALMACEN, t.COD_CLASE_MOVIMIENTO, t.COD_TIPO_MOVIMIENTO); res.Procesados++; }
            catch (Exception ex)
            {
                _log.LogError(ex, Programa, Clase, nameof(DesasignarAsync));
                res.Fallidos.Add($"{t.COD_CLASE_MOVIMIENTO}/{t.COD_TIPO_MOVIMIENTO}");
                res.DesError ??= ex.Message;
            }
        }
        return ApiResponse<RolesUsuarioAlmacenesTiposMovimientoLoteResult>.Ok(res);
    }

    /// <summary>Distinct por el par completo: Distinct() sobre los objetos no serviría porque
    /// son referencias distintas aunque traigan los mismos códigos.</summary>
    private static IEnumerable<RolesUsuarioAlmacenesTiposMovimientoClave> Unicos(RolesUsuarioAlmacenesTiposMovimientoLoteDto dto)
        => dto.Tipos.GroupBy(t => $"{t.COD_CLASE_MOVIMIENTO}|{t.COD_TIPO_MOVIMIENTO}").Select(g => g.First());
}
