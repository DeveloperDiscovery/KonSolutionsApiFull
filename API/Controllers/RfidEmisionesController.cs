using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Rfid;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Emisión de etiquetas RFID (RFID.EMISIONES). Una emisión nace de un ingreso por compra o del stock y reserva etiquetas
/// PENDIENTES; cuando un chip entrega su TID (<c>POST {id}/enrolar</c>) recibe el código <c>TID + correlativo de 15 dígitos</c>
/// (queda en ESTANDAR.OBJETOS_RFID) y el EPC que hay que grabar. Esos mismos endpoints son el contrato del agente local de los equipos.
///
/// Permisos: POST crea (emisión, enrolamiento), PUT modifica (equipo, grabación, anulaciones) y DELETE borra.</summary>
[Authorize]
[Route("api/v1/rfid/emisiones")]
[ExigePermiso("RFID.EMISIONES")]
public class RfidEmisionesController : BaseController
{
    private readonly IRfidEmisionesService _service;
    public RfidEmisionesController(IRfidEmisionesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_EMISION:int}")]
    public async Task<IActionResult> GetById(int NUM_EMISION)
    { var r = await _service.GetByIdAsync(NUM_EMISION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] RfidEmisionSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_EMISION:int}/detalle")]
    public async Task<IActionResult> Detalle(int NUM_EMISION)
    { var r = await _service.DetalleAsync(NUM_EMISION); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Las etiquetas de la emisión. Con <c>COD_ESTADO_ETIQUETA=P&amp;TOP=n</c> es "las siguientes n pendientes" (lo que pide el agente).</summary>
    [HttpGet("{NUM_EMISION:int}/etiquetas")]
    public async Task<IActionResult> Etiquetas(int NUM_EMISION, [FromQuery] int? NUM_ITEM, [FromQuery] string? COD_ESTADO_ETIQUETA, [FromQuery] int? TOP)
    { var r = await _service.EtiquetasAsync(NUM_EMISION, NUM_ITEM, COD_ESTADO_ETIQUETA, TOP); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] RfidEmisionInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] RfidEmisionUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPut("{NUM_EMISION:int}/anular")]
    public async Task<IActionResult> Anular(int NUM_EMISION, [FromBody] RfidAnularDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.AnularAsync(NUM_EMISION, dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_EMISION:int}")]
    public async Task<IActionResult> Delete(int NUM_EMISION)
    { var r = await _service.DeleteAsync(NUM_EMISION); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Un chip entrega su TID: se le asigna la siguiente etiqueta pendiente (o la elegida) y se devuelve el código y el EPC a grabar.
    /// Si el mismo TID llega otra vez a la misma emisión (un reintento) devuelve lo ya enrolado, marcado con FLG_YA_ENROLADA.</summary>
    [HttpPost("{NUM_EMISION:int}/enrolar")]
    public async Task<IActionResult> Enrolar(int NUM_EMISION, [FromBody] RfidEnrolarDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.EnrolarAsync(NUM_EMISION, dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Anula UNA etiqueta (se dañó o falló al grabar). Si ya tiene lecturas no se puede.</summary>
    [HttpPut("{NUM_EMISION:int}/etiquetas/{NUM_ITEM:int}/{NUM_ETIQUETA:int}/anular")]
    public async Task<IActionResult> AnularEtiqueta(int NUM_EMISION, int NUM_ITEM, int NUM_ETIQUETA, [FromBody] RfidEtiquetaAnularDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.AnularEtiquetaAsync(NUM_EMISION, NUM_ITEM, NUM_ETIQUETA, dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    // ───────── de dónde salen las etiquetas ─────────

    [HttpGet("origen/compras")]
    public async Task<IActionResult> OrigenCompras([FromQuery] RfidOrigenCompraSearchDto dto)
    { var r = await _service.OrigenComprasAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("origen/compras/{NUM_MOVIMIENTO_ALMACEN:decimal}/lineas")]
    public async Task<IActionResult> OrigenComprasLineas(decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.OrigenComprasLineasAsync(NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("origen/stock")]
    public async Task<IActionResult> OrigenStock([FromQuery] RfidOrigenStockSearchDto dto)
    { if (string.IsNullOrWhiteSpace(dto.COD_ALMACEN)) return BadRequest(new { Success = false, Message = "Indica el almacén." });
      var r = await _service.OrigenStockAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }
}
