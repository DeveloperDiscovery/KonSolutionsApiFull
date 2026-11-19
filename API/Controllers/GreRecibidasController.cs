using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.API.Filters;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Las guías de remisión que los proveedores mandaron por correo y bajó el servicio de XML (LOGISTICA.GRE_RECIBIDAS), vistas desde Movimientos de Almacén («Traer guía recibida»).
/// Permisos por verbo, los de Movimientos de Almacén: GET consulta, PUT asigna producto y descarta, POST genera el movimiento. Aquí también se puede ver el estado del servicio y correrlo ahora.</summary>
[Authorize]
[Route("api/v1/logistica/gre-recibidas")]
[ExigePermiso("LOGISTICA.MOVIMIENTOS_ALMACEN")]
public class GreRecibidasController : BaseController
{
    private readonly IGreRecibidasService _service;
    private readonly IXmlRecibidosService _servicioXml;
    public GreRecibidasController(IGreRecibidasService service, IXmlRecibidosService servicioXml) { _service = service; _servicioXml = servicioXml; }

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] GreRecibidaSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_GRE:decimal}")]
    public async Task<IActionResult> GetById(decimal NUM_GRE)
    { var r = await _service.GetByIdAsync(NUM_GRE); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("{NUM_GRE:decimal}/lineas")]
    public async Task<IActionResult> Lineas(decimal NUM_GRE)
    { var r = await _service.LineasAsync(NUM_GRE); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_GRE:decimal}/xml")]
    public async Task<IActionResult> Xml(decimal NUM_GRE)
    { var r = await _service.XmlAsync(NUM_GRE); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("{NUM_GRE:decimal}/pdf")]
    public async Task<IActionResult> Pdf(decimal NUM_GRE)
    { var r = await _service.PdfAsync(NUM_GRE); return r.Success && r.Data is not null ? File(r.Data.CONTENIDO, "application/pdf") : NotFound(r); }

    [HttpPut("{NUM_GRE:decimal}/lineas/{NUM_ITEM:int}/producto")]
    public async Task<IActionResult> AsignarProducto(decimal NUM_GRE, int NUM_ITEM, [FromBody] RecibidoAsignarProductoDto dto)
    { var r = await _service.AsignarProductoAsync(NUM_GRE, NUM_ITEM, dto ?? new(), GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPut("{NUM_GRE:decimal}/descartar")]
    public async Task<IActionResult> Descartar(decimal NUM_GRE, [FromBody] RecibidoDescartarDto dto)
    { var r = await _service.DescartarAsync(NUM_GRE, dto ?? new(), GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Genera el ingreso a almacén con la guía y su detalle (todas las líneas necesitan ya el producto de la empresa) y deja la guía como usada.</summary>
    [HttpPost("{NUM_GRE:decimal}/generar-movimiento")]
    public async Task<IActionResult> GenerarMovimiento(decimal NUM_GRE, [FromBody] GreRecibidaGenerarDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var r = await _service.GenerarMovimientoAsync(NUM_GRE, dto ?? new(), GetCodUsuario(), GetEstacion());
        return r.Success ? StatusCode(201, r) : BadRequest(r);
    }

    /// <summary>Si la cuenta de Microsoft 365 está configurada, si la carga automática está activa y cómo salió la última corrida.</summary>
    [HttpGet("servicio/estado")]
    public async Task<IActionResult> EstadoServicio()
    { return Ok(ApiResponse<XmlRecibidosEstadoDto>.Ok(await _servicioXml.EstadoAsync())); }

    /// <summary>Corre ahora el servicio que baja los XML y PDF del correo (aunque la carga automática esté apagada).</summary>
    [HttpPost("servicio/ejecutar")]
    public async Task<IActionResult> EjecutarServicio(CancellationToken ct)
    {
        var c = await _servicioXml.EjecutarAsync(true, ct);
        return c.Mensaje is null || c.Correos > 0 ? Ok(ApiResponse<XmlRecibidosCiclo>.Ok(c, c.Mensaje ?? "Listo.")) : BadRequest(ApiResponse<XmlRecibidosCiclo>.Fail(c.Mensaje));
    }
}
