using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class AlmacenesTiposMovimientoService : IAlmacenesTiposMovimientoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(AlmacenesTiposMovimientoService);
    private readonly IAlmacenesTiposMovimientoRepository _repo;
    private readonly ErrorLogService _log;
    public AlmacenesTiposMovimientoService(IAlmacenesTiposMovimientoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<AlmacenesTiposMovimiento>>> GetAllAsync(string COD_ALMACEN, string COD_CLASE_MOVIMIENTO)
    {
        try { var d = (await _repo.ShowallAsync(COD_ALMACEN, COD_CLASE_MOVIMIENTO)).ToList();
              return ApiResponse<IEnumerable<AlmacenesTiposMovimiento>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<AlmacenesTiposMovimiento>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<AlmacenesTiposMovimiento>> GetByIdAsync(string COD_ALMACEN, string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ALMACEN, COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO);
              return d is null ? ApiResponse<AlmacenesTiposMovimiento>.Fail("No encontrado.") : ApiResponse<AlmacenesTiposMovimiento>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<AlmacenesTiposMovimiento>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string COD_ALMACEN, string COD_CLASE_MOVIMIENTO)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(COD_ALMACEN, COD_CLASE_MOVIMIENTO)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<AlmacenesTiposMovimiento>>> SearchAsync(AlmacenesTiposMovimientoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<AlmacenesTiposMovimiento>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<AlmacenesTiposMovimiento>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(AlmacenesTiposMovimientoInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Tipo de movimiento asignado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ALMACEN, string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO)
    {
        try { var r = await _repo.DeleteAsync(COD_ALMACEN, COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Tipo de movimiento desasignado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    /// <summary>Asignación en bloque.
    ///
    /// Cada tipo se procesa por separado y un fallo NO corta el lote: si el tipo 7 de 20 da
    /// error, los otros 19 igual quedan asignados y la respuesta dice cuál falló. Abortar al
    /// primer error dejaría el almacén a medio configurar y sin rastro de dónde se cortó.</summary>
    public async Task<ApiResponse<AlmacenesTiposMovimientoLoteResult>> AsignarAsync(AlmacenesTiposMovimientoLoteDto dto, string codUsuario, string codEstacion)
    {
        var res = new AlmacenesTiposMovimientoLoteResult();
        foreach (var tipo in dto.Tipos.Distinct())
        {
            try
            {
                await _repo.InsertAsync(new AlmacenesTiposMovimientoInsertDto
                {
                    COD_ALMACEN = dto.COD_ALMACEN,
                    COD_CLASE_MOVIMIENTO = dto.COD_CLASE_MOVIMIENTO,
                    COD_TIPO_MOVIMIENTO = tipo
                }, codUsuario, codEstacion);
                res.Procesados++;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, Programa, Clase, nameof(AsignarAsync));
                res.Fallidos.Add(tipo);
                res.DesError ??= ex.Message;
            }
        }
        return ApiResponse<AlmacenesTiposMovimientoLoteResult>.Ok(res);
    }

    /// <summary>Baja en bloque. Mismo criterio que la asignación, y acá importa todavía más:
    /// el SP DELETE rechaza el tipo que ya esté referenciado desde
    /// SEGURIDAD.USUARIOS_ALMACENES_TIPOS_MOVIMIENTO, así que es normal que algunos no se
    /// puedan quitar y el usuario necesita saber cuáles.</summary>
    public async Task<ApiResponse<AlmacenesTiposMovimientoLoteResult>> DesasignarAsync(AlmacenesTiposMovimientoLoteDto dto)
    {
        var res = new AlmacenesTiposMovimientoLoteResult();
        foreach (var tipo in dto.Tipos.Distinct())
        {
            try
            {
                await _repo.DeleteAsync(dto.COD_ALMACEN, dto.COD_CLASE_MOVIMIENTO, tipo);
                res.Procesados++;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, Programa, Clase, nameof(DesasignarAsync));
                res.Fallidos.Add(tipo);
                res.DesError ??= ex.Message;
            }
        }
        return ApiResponse<AlmacenesTiposMovimientoLoteResult>.Ok(res);
    }
}
