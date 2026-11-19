using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Mapa de trazabilidad de requerimientos. Sólo lectura.
///
/// Va con el permiso de requerimientos de stock: el mapa parte de un requerimiento, y quien no
/// puede ver requerimientos no debería poder recorrerlos por acá.</summary>
[Authorize]
[Route("api/v1/logistica/trazabilidad")]
[ExigePermiso("LOGISTICA.REQUERIMIENTOS_STOCK")]
public class TrazabilidadController : BaseController
{
    private readonly ITrazabilidadService _service;
    public TrazabilidadController(ITrazabilidadService service) => _service = service;

    /// <summary>El documento de partida. tipo: REQ, RES, PRF (proforma), OC (orden de compra) o MOV.
    /// Proformas y órdenes de compra se numeran por tipo de operación, así que un número puede
    /// devolver varios documentos.</summary>
    [HttpGet("buscar")]
    public async Task<IActionResult> Buscar([FromQuery] string? tipo, [FromQuery] string? numero)
    { var r = await _service.BuscarAsync(tipo, numero); return r.Success ? Ok(r) : NotFound(r); }

    /// <summary>Las franjas del mapa, en orden: requerimientos, reservas, una por cada clase de
    /// operación y almacenes.</summary>
    [HttpGet("franjas")]
    public async Task<IActionResult> Franjas()
    { var r = await _service.FranjasAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Un nodo: tipo (REQ, RES, PC, MOV, ALM) y clave.</summary>
    [HttpGet("nodo")]
    public async Task<IActionResult> Nodo([FromQuery] string? tipo, [FromQuery] string? clave)
    { var r = await _service.NodoAsync(tipo, clave); return r.Success ? Ok(r) : NotFound(r); }

    /// <summary>Los vecinos de un nodo. sentido: A (atrás), D (adelante) o vacío para los dos.</summary>
    [HttpGet("vecinos")]
    public async Task<IActionResult> Vecinos([FromQuery] string? tipo, [FromQuery] string? clave, [FromQuery] string? sentido)
    { var r = await _service.VecinosAsync(tipo, clave, sentido); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>La entidad del nodo para el panel: campos y líneas.</summary>
    [HttpGet("entidad")]
    public async Task<IActionResult> Entidad([FromQuery] string? tipo, [FromQuery] string? clave)
    { var r = await _service.EntidadAsync(tipo, clave); return r.Success ? Ok(r) : BadRequest(r); }
}
