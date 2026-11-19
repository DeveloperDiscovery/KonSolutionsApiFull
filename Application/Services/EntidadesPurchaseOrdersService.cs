using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

// ── Cabecera ────────────────────────────────────────────────────────────────
public class EntidadesPurchaseOrdersService : IEntidadesPurchaseOrdersService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EntidadesPurchaseOrdersService);
    private readonly IEntidadesPurchaseOrdersRepository _repo;
    private readonly ErrorLogService _log;
    public EntidadesPurchaseOrdersService(IEntidadesPurchaseOrdersRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EntidadesPurchaseOrders>>> GetAllAsync(decimal codEntidad)
    {
        try { var d = (await _repo.ShowallAsync(codEntidad)).ToList();
              return ApiResponse<IEnumerable<EntidadesPurchaseOrders>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EntidadesPurchaseOrders>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EntidadesPurchaseOrders>> GetByIdAsync(decimal codEntidad, string numPurchaseOrder)
    {
        try { var d = await _repo.ShowbyidAsync(codEntidad, numPurchaseOrder);
              return d is null ? ApiResponse<EntidadesPurchaseOrders>.Fail("No encontrado.")
                               : ApiResponse<EntidadesPurchaseOrders>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EntidadesPurchaseOrders>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(decimal codEntidad)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(codEntidad)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    /// <summary>El SEARCH del SP mezcla el filtro por cliente con el de texto usando OR sin
    /// paréntesis, así que puede traer órdenes de OTROS clientes (ver LEEME). Hasta que se
    /// corrija en la base, se descarta acá lo que no sea del cliente pedido: es preferible
    /// devolver de menos que mostrarle a alguien la orden de otro.</summary>
    public async Task<ApiResponse<IEnumerable<EntidadesPurchaseOrders>>> SearchAsync(EntidadesPurchaseOrdersSearchDto dto)
    {
        try
        {
            var d = (await _repo.SearchAsync(dto))
                    .Where(x => x.COD_ENTIDAD == dto.COD_ENTIDAD)
                    .ToList();
            return ApiResponse<IEnumerable<EntidadesPurchaseOrders>>.Ok(d, total: d.Count);
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<EntidadesPurchaseOrders>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EntidadesPurchaseOrdersInsertDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.NUM_PURCHASE_ORDER))
                return ApiResponse<WriteResult>.Fail("Indique el número de purchase order.");

            var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
            return ApiResponse<WriteResult>.FromWriteResult(r, "Purchase order registrada.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesPurchaseOrdersUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Purchase order actualizada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal codEntidad, string numPurchaseOrder)
    {
        try { var r = await _repo.DeleteAsync(codEntidad, numPurchaseOrder);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Purchase order eliminada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}


// ── Despacho ────────────────────────────────────────────────────────────────
public class EntidadesPurchaseOrdersDespachoService : IEntidadesPurchaseOrdersDespachoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EntidadesPurchaseOrdersDespachoService);
    private readonly IEntidadesPurchaseOrdersDespachoRepository _repo;
    private readonly ErrorLogService _log;
    public EntidadesPurchaseOrdersDespachoService(IEntidadesPurchaseOrdersDespachoRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EntidadesPurchaseOrdersDespacho>>> GetAllAsync(decimal codEntidad, string numPurchaseOrder)
    {
        try { var d = (await _repo.ShowallAsync(codEntidad, numPurchaseOrder)).ToList();
              return ApiResponse<IEnumerable<EntidadesPurchaseOrdersDespacho>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EntidadesPurchaseOrdersDespacho>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EntidadesPurchaseOrdersDespacho>> GetByIdAsync(decimal codEntidad, string numPurchaseOrder, int numSecuencia)
    {
        try { var d = await _repo.ShowbyidAsync(codEntidad, numPurchaseOrder, numSecuencia);
              return d is null ? ApiResponse<EntidadesPurchaseOrdersDespacho>.Fail("No encontrado.")
                               : ApiResponse<EntidadesPurchaseOrdersDespacho>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EntidadesPurchaseOrdersDespacho>.Fail(ex.Message); }
    }

    /// <summary>Mismo recorte que en la cabecera: el SEARCH del SP puede traer despachos de otras
    /// órdenes por el OR sin paréntesis.</summary>
    public async Task<ApiResponse<IEnumerable<EntidadesPurchaseOrdersDespacho>>> SearchAsync(EntidadesPurchaseOrdersDespachoSearchDto dto)
    {
        try
        {
            var d = (await _repo.SearchAsync(dto))
                    .Where(x => x.COD_ENTIDAD == dto.COD_ENTIDAD && x.NUM_PURCHASE_ORDER == dto.NUM_PURCHASE_ORDER)
                    .ToList();
            return ApiResponse<IEnumerable<EntidadesPurchaseOrdersDespacho>>.Ok(d, total: d.Count);
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<EntidadesPurchaseOrdersDespacho>>.Fail(ex.Message); }
    }

    /// <summary>La cantidad de embalaje no puede ser cero ni negativa: es cuántas unidades entran
    /// en el empaque, y un cero haría que cualquier cálculo de embalaje divida por nada.</summary>
    private static string? Validar(decimal? canEmbalaje)
        => canEmbalaje is not null && canEmbalaje <= 0
            ? "La cantidad de embalaje tiene que ser mayor que cero."
            : null;

    public async Task<ApiResponse<WriteResult>> InsertAsync(EntidadesPurchaseOrdersDespachoInsertDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            var error = Validar(dto.CAN_EMBALAJE);
            if (error is not null) return ApiResponse<WriteResult>.Fail(error);

            // El SP no numera: si la secuencia llega en cero o repetida, se resuelve acá contra lo
            // que ya está grabado. No cierra la ventana entre dos altas simultáneas —eso sólo se
            // arregla en el SP— pero cubre el caso normal y el de un llamador que no la mande.
            if (dto.NUM_SECUENCIA <= 0)
            {
                var existentes = (await _repo.ShowallAsync(dto.COD_ENTIDAD, dto.NUM_PURCHASE_ORDER)).ToList();
                dto.NUM_SECUENCIA = existentes.Count == 0 ? 1 : existentes.Max(x => x.NUM_SECUENCIA ?? 0) + 1;
            }

            var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
            return ApiResponse<WriteResult>.FromWriteResult(r, "Despacho agregado.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesPurchaseOrdersDespachoUpdateDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            var error = Validar(dto.CAN_EMBALAJE);
            if (error is not null) return ApiResponse<WriteResult>.Fail(error);

            var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
            return ApiResponse<WriteResult>.FromWriteResult(r, "Despacho actualizado.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal codEntidad, string numPurchaseOrder, int numSecuencia)
    {
        try { var r = await _repo.DeleteAsync(codEntidad, numPurchaseOrder, numSecuencia);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Despacho eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}


// ── Especificación técnica ──────────────────────────────────────────────────
public class EntidadesPurchaseOrdersEspecificacionTecnicaService : IEntidadesPurchaseOrdersEspecificacionTecnicaService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EntidadesPurchaseOrdersEspecificacionTecnicaService);
    private readonly IEntidadesPurchaseOrdersEspecificacionTecnicaRepository _repo;
    private readonly ErrorLogService _log;
    public EntidadesPurchaseOrdersEspecificacionTecnicaService(
        IEntidadesPurchaseOrdersEspecificacionTecnicaRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EntidadesPurchaseOrdersEspecificacionTecnica>>> GetAllAsync(
        decimal codEntidad, string numPurchaseOrder, int numSecuencia)
    {
        try { var d = (await _repo.ShowallAsync(codEntidad, numPurchaseOrder, numSecuencia)).ToList();
              return ApiResponse<IEnumerable<EntidadesPurchaseOrdersEspecificacionTecnica>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EntidadesPurchaseOrdersEspecificacionTecnica>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EntidadesPurchaseOrdersEspecificacionTecnica>> GetByIdAsync(
        decimal codEntidad, string numPurchaseOrder, int numSecuencia, string codEspecificacion)
    {
        try { var d = await _repo.ShowbyidAsync(codEntidad, numPurchaseOrder, numSecuencia, codEspecificacion);
              return d is null ? ApiResponse<EntidadesPurchaseOrdersEspecificacionTecnica>.Fail("No encontrado.")
                               : ApiResponse<EntidadesPurchaseOrdersEspecificacionTecnica>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EntidadesPurchaseOrdersEspecificacionTecnica>.Fail(ex.Message); }
    }

    /// <summary>Mismo recorte que en los otros dos niveles: el OR sin paréntesis del SP.</summary>
    public async Task<ApiResponse<IEnumerable<EntidadesPurchaseOrdersEspecificacionTecnica>>> SearchAsync(
        EntidadesPurchaseOrdersEspecificacionTecnicaSearchDto dto)
    {
        try
        {
            var d = (await _repo.SearchAsync(dto))
                    .Where(x => x.COD_ENTIDAD == dto.COD_ENTIDAD
                             && x.NUM_PURCHASE_ORDER == dto.NUM_PURCHASE_ORDER
                             && (x.NUM_SECUENCIA ?? 0) == dto.NUM_SECUENCIA)
                    .ToList();
            return ApiResponse<IEnumerable<EntidadesPurchaseOrdersEspecificacionTecnica>>.Ok(d, total: d.Count);
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<EntidadesPurchaseOrdersEspecificacionTecnica>>.Fail(ex.Message); }
    }

    /// <summary>Una sola especificación puede ser la de cabecera del despacho. Se comprueba acá
    /// porque la tabla no lo impide y dos "header" dejarían el documento sin saber cuál imprimir.</summary>
    private async Task<string?> ValidarHeaderAsync(
        decimal codEntidad, string numPurchaseOrder, int numSecuencia, string codEspecificacionPropia, bool esHeader)
    {
        if (!esHeader) return null;

        var otras = (await _repo.ShowallAsync(codEntidad, numPurchaseOrder, numSecuencia))
                    .Where(x => x.COD_ESPECIFICACION_TECNICA != codEspecificacionPropia);

        return otras.Any(x => x.FLG_HEADER == true)
            ? "Este despacho ya tiene una especificación marcada como cabecera. Quítasela a esa antes de marcar otra."
            : null;
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(
        EntidadesPurchaseOrdersEspecificacionTecnicaInsertDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.COD_ESPECIFICACION_TECNICA))
                return ApiResponse<WriteResult>.Fail("Elija la especificación técnica.");

            var error = await ValidarHeaderAsync(dto.COD_ENTIDAD, dto.NUM_PURCHASE_ORDER, dto.NUM_SECUENCIA,
                                                 dto.COD_ESPECIFICACION_TECNICA, dto.FLG_HEADER ?? false);
            if (error is not null) return ApiResponse<WriteResult>.Fail(error);

            var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
            return ApiResponse<WriteResult>.FromWriteResult(r, "Especificación agregada.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(
        EntidadesPurchaseOrdersEspecificacionTecnicaUpdateDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            var error = await ValidarHeaderAsync(dto.COD_ENTIDAD, dto.NUM_PURCHASE_ORDER, dto.NUM_SECUENCIA,
                                                 dto.COD_ESPECIFICACION_TECNICA, dto.FLG_HEADER ?? false);
            if (error is not null) return ApiResponse<WriteResult>.Fail(error);

            var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
            return ApiResponse<WriteResult>.FromWriteResult(r, "Especificación actualizada.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(
        decimal codEntidad, string numPurchaseOrder, int numSecuencia, string codEspecificacion)
    {
        try { var r = await _repo.DeleteAsync(codEntidad, numPurchaseOrder, numSecuencia, codEspecificacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Especificación eliminada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
