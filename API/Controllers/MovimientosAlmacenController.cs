using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/movimientos-almacen")]
[ExigePermiso("LOGISTICA.MOVIMIENTOS_ALMACEN")]
public class MovimientosAlmacenController : BaseController
{
    private readonly IMovimientosAlmacenService _service;
    private readonly IMovimientosAlmacenOrdenesCompraService _ordenes;
    private readonly IMovimientosAlmacenRequerimientosVistaService _requerimientos;
    public MovimientosAlmacenController(IMovimientosAlmacenService service,
                                        IMovimientosAlmacenOrdenesCompraService ordenes,
                                        IMovimientosAlmacenRequerimientosVistaService requerimientos)
    { _service = service; _ordenes = ordenes; _requerimientos = requerimientos; }

    // ── Pestaña "Requerimientos" del movimiento ──────────────────────────────────────
    // Mismo criterio que las de órdenes de compra: cuelgan del movimiento para viajar con
    // SU permiso, y son de sólo lectura.

    /// <summary>Sin número de movimiento: el SP no filtra por movimiento. Igual va bajo la
    /// ruta del movimiento, porque es la pantalla que la usa y el permiso es ése.</summary>
    [HttpGet("requerimientos/disponibles")]
    public async Task<IActionResult> RequerimientosDisponibles()
    { var r = await _requerimientos.DisponiblesAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_MOVIMIENTO_ALMACEN}/requerimientos/asignados")]
    public async Task<IActionResult> RequerimientosAsignados(decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _requerimientos.AsignadosAsync(NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_MOVIMIENTO_ALMACEN}/requerimientos/asignados/detalle")]
    public async Task<IActionResult> RequerimientosAsignadosDetalle(decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _requerimientos.AsignadosDetalleAsync(NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Cobertura de UN requerimiento contra el stock del almacén: lo disponible por
    /// línea (Lineas) y el stock a despachar con su reserva de origen (Stock). criterio: MAYOR
    /// (por defecto), MENOR o LOTE.
    ///
    /// Por POST aunque devuelva datos: el SP reescribe la tabla de stock seleccionado del par
    /// movimiento + requerimiento en cada llamada, y un GET no debe escribir. Al ser POST con
    /// sub-ruta, el permiso que se pide es el de escritura sobre el movimiento, que es lo
    /// correcto: quien sólo puede mirar no debe regenerar la selección.</summary>
    [HttpPost("{NUM_MOVIMIENTO_ALMACEN}/requerimientos/disponibles/{NUM_REQUERIMIENTO_STOCK}/cobertura")]
    public async Task<IActionResult> RequerimientosCobertura(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_REQUERIMIENTO_STOCK, [FromQuery] string? criterio = null)
    { var r = await _requerimientos.CoberturaAsync(NUM_MOVIMIENTO_ALMACEN, NUM_REQUERIMIENTO_STOCK, criterio); return r.Success ? Ok(r) : BadRequest(r); }

    // ── Escritura de la pestaña Requerimientos ───────────────────────────────────
    //
    // En ESTE controlador, con el permiso del movimiento, por el mismo motivo que las de
    // órdenes de compra: quien puede despachar contra un movimiento no necesariamente
    // tiene permiso sobre el módulo de requerimientos, y pedírselo le daría un 403 con los
    // botones a la vista.

    /// <summary>Asigna al movimiento las líneas marcadas.
    ///
    /// Sin clase ni tipo de operación, a diferencia de órdenes de compra: el número de
    /// requerimiento es único en toda la base. Sí lleva criterio, que ahí no existe: acá
    /// decide de qué ubicación sale la mercadería.</summary>
    [HttpPost("{NUM_MOVIMIENTO_ALMACEN}/requerimientos/asignar")]
    public async Task<IActionResult> RequerimientosAsignar(decimal NUM_MOVIMIENTO_ALMACEN, [FromBody] RequerimientoLineasSeleccionadasDto dto)
    {
        var r = await _requerimientos.AsignarAsync(NUM_MOVIMIENTO_ALMACEN, dto?.Lineas ?? "",
                    dto?.COD_CRITERIO ?? "MAYOR", GetCodUsuario(), GetEstacion());
        return r.Success ? Ok(r) : BadRequest(r);
    }

    /// <summary>Quita del movimiento las líneas marcadas. Por POST y no por DELETE porque
    /// lleva cuerpo: DELETE con cuerpo funciona, pero hay intermediarios que lo descartan sin
    /// avisar y el resultado sería un borrado que "a veces" no borra.</summary>
    [HttpPost("{NUM_MOVIMIENTO_ALMACEN}/requerimientos/quitar")]
    public async Task<IActionResult> RequerimientosQuitar(decimal NUM_MOVIMIENTO_ALMACEN, [FromBody] RequerimientoLineasSeleccionadasDto dto)
    {
        var r = await _requerimientos.QuitarAsync(NUM_MOVIMIENTO_ALMACEN, dto?.Lineas ?? "",
                    GetCodUsuario(), GetEstacion());
        return r.Success ? Ok(r) : BadRequest(r);
    }

    // ── Pestaña "Órdenes de Compra" del movimiento ───────────────────────────────────
    //
    // VIVEN ACÁ Y NO EN MovimientosAlmacenPedidosComercialesController A PROPÓSITO.
    // Ese controlador exige el permiso LOGISTICA.MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES;
    // estas cuatro consultas las hace la pantalla del movimiento, y quien graba un
    // movimiento no tiene por qué tener ese otro permiso. Colgadas de ahí, un usuario de
    // almacén recibiría 403 y las cuatro grillas se verían vacías sin ningún aviso — es
    // exactamente lo que ya nos pasó con el formato del número de comprobante.
    // Son de sólo lectura y del movimiento, así que el permiso correcto es el del movimiento.

    [HttpGet("{NUM_MOVIMIENTO_ALMACEN}/ordenes-compra/disponibles")]
    public async Task<IActionResult> OrdenesDisponibles(decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _ordenes.DisponiblesAsync(NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Detalle de las órdenes disponibles que el usuario tenga seleccionadas.
    /// La lista va por query y no por ruta: son varios números y pueden ser muchos.</summary>
    [HttpGet("{NUM_MOVIMIENTO_ALMACEN}/ordenes-compra/disponibles/detalle")]
    public async Task<IActionResult> OrdenesDisponiblesDetalle(decimal NUM_MOVIMIENTO_ALMACEN,
        [FromQuery] string codClaseOperacion, [FromQuery] string codTipoOperacion, [FromQuery] string? pedidos)
    {
        var r = await _ordenes.DisponiblesDetalleAsync(NUM_MOVIMIENTO_ALMACEN,
                    codClaseOperacion ?? "", codTipoOperacion ?? "", pedidos ?? "");
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpGet("{NUM_MOVIMIENTO_ALMACEN}/ordenes-compra/asignados")]
    public async Task<IActionResult> OrdenesAsignados(decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _ordenes.AsignadosAsync(NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_MOVIMIENTO_ALMACEN}/ordenes-compra/asignados/detalle")]
    public async Task<IActionResult> OrdenesAsignadosDetalle(decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _ordenes.AsignadosDetalleAsync(NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    // ── Escritura de la pestaña Órdenes de Compra ────────────────────────────────
    //
    // Van en ESTE controlador, con el permiso del movimiento, por el mismo motivo que las
    // cuatro consultas de arriba: quien tiene permiso para recibir mercadería en un
    // movimiento no necesariamente lo tiene sobre el módulo de pedidos comerciales, y
    // pedírselo le daría un 403 con los botones a la vista.

    /// <summary>Asigna al movimiento las líneas marcadas.
    ///
    /// La lista va en el cuerpo y no en la ruta: son varios pedidos con varias secuencias
    /// cada uno, y una recepción grande haría una URL más larga de lo que conviene.</summary>
    [HttpPost("{NUM_MOVIMIENTO_ALMACEN}/ordenes-compra/asignar")]
    public async Task<IActionResult> OrdenesAsignar(decimal NUM_MOVIMIENTO_ALMACEN, [FromBody] LineasSeleccionadasDto dto)
    {
        var r = await _ordenes.AsignarAsync(NUM_MOVIMIENTO_ALMACEN, dto?.COD_CLASE_OPERACION ?? "",
                    dto?.COD_TIPO_OPERACION ?? "", dto?.Lineas ?? "", GetCodUsuario(), GetEstacion());
        return r.Success ? Ok(r) : BadRequest(r);
    }

    /// <summary>Quita del movimiento las líneas marcadas. Va por POST y no por DELETE porque
    /// lleva cuerpo: DELETE con cuerpo funciona, pero hay intermediarios que lo descartan sin
    /// avisar y el resultado sería un borrado que "a veces" no borra.</summary>
    [HttpPost("{NUM_MOVIMIENTO_ALMACEN}/ordenes-compra/quitar")]
    public async Task<IActionResult> OrdenesQuitar(decimal NUM_MOVIMIENTO_ALMACEN, [FromBody] LineasSeleccionadasDto dto)
    {
        var r = await _ordenes.QuitarAsync(NUM_MOVIMIENTO_ALMACEN, dto?.COD_CLASE_OPERACION ?? "",
                    dto?.COD_TIPO_OPERACION ?? "", dto?.Lineas ?? "", GetCodUsuario(), GetEstacion());
        return r.Success ? Ok(r) : BadRequest(r);
    }

    /// <summary>Graba el lote y la ubicación elegidos para UNA línea. Se llama al cerrar el
    /// diálogo y al cambiar la ubicación, ANTES de asignar: el SP de asignación los lee de la
    /// línea del pedido, así que si no se grabaron primero, el movimiento entra sin ellos.</summary>
    [HttpPut("ordenes-compra/lote-ubicacion")]
    public async Task<IActionResult> OrdenesLoteUbicacion([FromBody] LoteUbicacionLineaDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var r = await _ordenes.GuardarLoteUbicacionAsync(dto);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    // El usuario sale del token, no de la pantalla: el SP lista sólo los almacenes de sus roles,
    // y si viajara como parámetro cualquiera podría pedir los de otro.
    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(GetCodUsuario()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_MOVIMIENTO_ALMACEN}")]
    public async Task<IActionResult> GetById(decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.GetByIdAsync(NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] MovimientosAlmacenSearchDto dto)
    { var r = await _service.SearchAsync(dto, GetCodUsuario()); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] MovimientosAlmacenInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] MovimientosAlmacenUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_MOVIMIENTO_ALMACEN}")]
    public async Task<IActionResult> Delete(decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.DeleteAsync(NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }


    /// <summary>Los ocho combos de la cabecera en un viaje. Sin esto la pantalla haria ocho
    /// peticiones cada vez que se abre un movimiento, contra las seis conexiones que abre el
    /// navegador por servidor.</summary>
    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos([FromQuery] string? codClaseMovimiento)
    { var r = await _service.GetLoadcombosAsync(codClaseMovimiento); return r.Success ? Ok(r) : BadRequest(r); }

    // AGREGAR AL DETALLE. Cada uno llama a un procedimiento que escribe el enlace y las
    // lineas en la MISMA transaccion: hacerlo en dos llamadas dejaria el movimiento
    // enlazado al documento pero sin lineas, y eso descuadra el stock en silencio.
    //
    // Devuelven cuantas lineas entraron. Cero NO es un error: significa que ya estaba todo
    // agregado o que no queda cantidad pendiente.

    /// <summary>Número de comprobante AL IMPRIMIR. Es POST y no GET porque consume un
    /// correlativo: cambia el estado de la base, aunque se lea como si fuera una consulta.
    ///
    /// Idempotente: si el movimiento ya tiene número devuelve el mismo, sin tomar otro. Una
    /// reimpresión no puede quemar un número nuevo.</summary>
    [HttpPost("{NUM_MOVIMIENTO_ALMACEN}/numero-impresion")]
    public async Task<IActionResult> NumeroImpresion(decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.NumeroImpresionAsync(NUM_MOVIMIENTO_ALMACEN, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Botón TRANSFERIR del documento y de la grilla: pasa el movimiento a su estado
    /// "enviado" (PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_TRANSFERIR). Si el estado actual lo
    /// permite lo decide antes la pantalla, con el flujo de estados del botón.</summary>
    [HttpPost("{NUM_MOVIMIENTO_ALMACEN}/transferir")]
    public async Task<IActionResult> Transferir(decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.TransferirAsync(NUM_MOVIMIENTO_ALMACEN, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("{NUM_MOVIMIENTO_ALMACEN}/agregar-desde-reserva")]
    public async Task<IActionResult> AgregarDesdeReserva(decimal NUM_MOVIMIENTO_ALMACEN, [FromBody] MovimientosAlmacenAgregarDto dto)
    { var r = await _service.AgregarDesdeReservaAsync(NUM_MOVIMIENTO_ALMACEN, dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("{NUM_MOVIMIENTO_ALMACEN}/agregar-desde-requerimiento")]
    public async Task<IActionResult> AgregarDesdeRequerimiento(decimal NUM_MOVIMIENTO_ALMACEN, [FromBody] MovimientosAlmacenAgregarDto dto)
    { var r = await _service.AgregarDesdeRequerimientoAsync(NUM_MOVIMIENTO_ALMACEN, dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("{NUM_MOVIMIENTO_ALMACEN}/agregar-desde-pedido-comercial")]
    public async Task<IActionResult> AgregarDesdePedidoComercial(decimal NUM_MOVIMIENTO_ALMACEN, [FromBody] MovimientosAlmacenAgregarDto dto)
    { var r = await _service.AgregarDesdePedidoComercialAsync(NUM_MOVIMIENTO_ALMACEN, dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    // ---- Devolucion de compra: guias del mismo proveedor, items y cantidades a devolver ----
    [HttpGet("{NUM_MOVIMIENTO_ALMACEN}/devolucion-compra/guias-disponibles")]
    public async Task<IActionResult> DevolucionGuiasDisponibles(decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.DevolucionGuiasDisponiblesAsync(NUM_MOVIMIENTO_ALMACEN);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_MOVIMIENTO_ALMACEN}/devolucion-compra/guias")]
    public async Task<IActionResult> DevolucionGuias(decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.DevolucionGuiasAsync(NUM_MOVIMIENTO_ALMACEN);
      return r.Success ? Ok(r) : BadRequest(r); }

    /// <param name="origenes">Numeros de movimiento de las guias separados por coma; sin valor trae los items ya asociados.</param>
    [HttpGet("{NUM_MOVIMIENTO_ALMACEN}/devolucion-compra/lineas")]
    public async Task<IActionResult> DevolucionLineas(decimal NUM_MOVIMIENTO_ALMACEN, [FromQuery] string? origenes)
    { var r = await _service.DevolucionLineasAsync(NUM_MOVIMIENTO_ALMACEN, string.IsNullOrWhiteSpace(origenes) ? null : origenes);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("{NUM_MOVIMIENTO_ALMACEN}/devolucion-compra/adicionar")]
    public async Task<IActionResult> DevolucionAdicionar(decimal NUM_MOVIMIENTO_ALMACEN, [FromBody] MovimientoDevolucionAdicionarDto dto)
    { var r = await _service.DevolucionAdicionarAsync(NUM_MOVIMIENTO_ALMACEN, dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_MOVIMIENTO_ALMACEN}/devolucion-compra/guias/{NUM_MOVIMIENTO_ALMACEN_ORIGEN}")]
    public async Task<IActionResult> DevolucionQuitarGuia(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_MOVIMIENTO_ALMACEN_ORIGEN)
    { var r = await _service.DevolucionQuitarGuiaAsync(NUM_MOVIMIENTO_ALMACEN, NUM_MOVIMIENTO_ALMACEN_ORIGEN);
      return r.Success ? Ok(r) : BadRequest(r); }
}