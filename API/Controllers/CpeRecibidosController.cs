using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.API.Filters;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Los comprobantes que los proveedores mandaron por correo y bajó el servicio de XML (FINANZAS.CPE_RECIBIDOS), vistos desde el Registro de Compras («Traer de XML recibidos»).
/// Permisos por verbo, los del Registro de Compras: GET consulta, PUT asigna producto y descarta, POST genera el registro.</summary>
[Authorize]
[Route("api/v1/finanzas/cpe-recibidos")]
[ExigePermiso("COMERCIAL.REGISTRO_COMPRAS")]
public class CpeRecibidosController : BaseController
{
    private readonly ICpeRecibidosService _service;
    public CpeRecibidosController(ICpeRecibidosService service) => _service = service;

    /// <summary>Lista con filtros opcionales: texto (serie, número, RUC, razón social o descripción de una línea), RUC, tipo, estado (PEN, USA, DES) y fechas de emisión.</summary>
    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] CpeRecibidoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_CPE:decimal}")]
    public async Task<IActionResult> GetById(decimal NUM_CPE)
    { var r = await _service.GetByIdAsync(NUM_CPE); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("{NUM_CPE:decimal}/lineas")]
    public async Task<IActionResult> Lineas(decimal NUM_CPE)
    { var r = await _service.LineasAsync(NUM_CPE); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>El XML original, tal como llegó.</summary>
    [HttpGet("{NUM_CPE:decimal}/xml")]
    public async Task<IActionResult> Xml(decimal NUM_CPE)
    { var r = await _service.XmlAsync(NUM_CPE); return r.Success ? Ok(r) : NotFound(r); }

    /// <summary>El tipo de operación que le toca según su XML: el de la clase (por omisión CMPRS) cuyo código SUNAT es el del XML (InvoiceTypeCode listID) y que es de bienes o de servicios según la unidad ZZ de las líneas.
    /// Si hay uno solo viene en COD_TIPO_OPERACION; si no, DES_MOTIVO dice por qué y hay que elegirlo.</summary>
    [HttpGet("{NUM_CPE:decimal}/tipo-operacion")]
    public async Task<IActionResult> TipoOperacion(decimal NUM_CPE, [FromQuery] string? codClaseOperacion)
    { var r = await _service.TipoOperacionAsync(NUM_CPE, codClaseOperacion); return r.Success ? Ok(r) : NotFound(r); }

    /// <summary>El PDF que vino en el mismo correo, listo para mostrarse.</summary>
    [HttpGet("{NUM_CPE:decimal}/pdf")]
    public async Task<IActionResult> Pdf(decimal NUM_CPE)
    { var r = await _service.PdfAsync(NUM_CPE); return r.Success && r.Data is not null ? File(r.Data.CONTENIDO, "application/pdf") : NotFound(r); }

    /// <summary>El producto de la empresa de una línea (vacío lo quita). Solo mientras el comprobante está pendiente.</summary>
    [HttpPut("{NUM_CPE:decimal}/lineas/{NUM_ITEM:int}/producto")]
    public async Task<IActionResult> AsignarProducto(decimal NUM_CPE, int NUM_ITEM, [FromBody] RecibidoAsignarProductoDto dto)
    { var r = await _service.AsignarProductoAsync(NUM_CPE, NUM_ITEM, dto ?? new(), GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPut("{NUM_CPE:decimal}/descartar")]
    public async Task<IActionResult> Descartar(decimal NUM_CPE, [FromBody] RecibidoDescartarDto dto)
    { var r = await _service.DescartarAsync(NUM_CPE, dto ?? new(), GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Genera el registro de compras con el comprobante y su detalle, asocia la orden de compra si el XML la cita, deja el comprobante como usado y pasa el PDF al registro.
    /// El tipo de operación puede ir vacío: entonces sale del XML (ver tipo-operacion).</summary>
    [HttpPost("{NUM_CPE:decimal}/generar-registro")]
    public async Task<IActionResult> GenerarRegistro(decimal NUM_CPE, [FromBody] CpeRecibidoGenerarDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var r = await _service.GenerarRegistroAsync(NUM_CPE, dto ?? new(), GetCodUsuario(), GetEstacion());
        return r.Success ? StatusCode(201, r) : BadRequest(r);
    }
}
