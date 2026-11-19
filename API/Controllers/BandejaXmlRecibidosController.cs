using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.API.Filters;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Bandeja de XML recibidos (Comercial): la consulta de los comprobantes que los proveedores mandaron por correo (o que alguien dejó en la carpeta) y que cargó el servicio de XML (FINANZAS.CPE_RECIBIDOS).
/// Es de CONSULTA: lista, ficha, líneas, XML original y PDF; y descartar / reactivar un comprobante pendiente. Generar el registro de compras se sigue haciendo desde el Registro de Compras.
/// Tiene su PROPIO permiso (COMERCIAL.XML_RECIBIDOS: GET consulta, PUT descartar, POST completar datos) para poder darle la bandeja a quien no usa el Registro de Compras; usa los mismos servicios que el controlador de «Traer de XML recibidos».</summary>
[Authorize]
[Route("api/v1/comercial/xml-recibidos")]
[ExigePermiso("COMERCIAL.XML_RECIBIDOS")]
public class BandejaXmlRecibidosController : BaseController
{
    private readonly ICpeRecibidosService _service;
    public BandejaXmlRecibidosController(ICpeRecibidosService service) => _service = service;

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

    /// <summary>El PDF que vino junto al XML, listo para mostrarse.</summary>
    [HttpGet("{NUM_CPE:decimal}/pdf")]
    public async Task<IActionResult> Pdf(decimal NUM_CPE)
    { var r = await _service.PdfAsync(NUM_CPE); return r.Success && r.Data is not null ? File(r.Data.CONTENIDO, "application/pdf") : NotFound(r); }

    /// <summary>Descarta un comprobante pendiente (FLG_DESCARTAR = true) o lo devuelve a pendiente (false). Uno ya usado en un registro no se toca.</summary>
    [HttpPut("{NUM_CPE:decimal}/descartar")]
    public async Task<IActionResult> Descartar(decimal NUM_CPE, [FromBody] RecibidoDescartarDto dto)
    { var r = await _service.DescartarAsync(NUM_CPE, dto ?? new(), GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>«Completar datos»: registra los proveedores (tipo PRV), asigna los productos por el código del proveedor y registra los lotes de TODOS los comprobantes pendientes.</summary>
    [HttpPost("completar-datos")]
    public async Task<IActionResult> CompletarDatos()
    { var r = await _service.CompletarDatosAsync(null, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Lo mismo, solo para el documento indicado (debe estar pendiente).</summary>
    [HttpPost("{NUM_CPE:decimal}/completar-datos")]
    public async Task<IActionResult> CompletarDatosUno(decimal NUM_CPE)
    { var r = await _service.CompletarDatosAsync(NUM_CPE, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>«Validar con SUNAT» de TODOS los comprobantes que aún no tienen resultado o cuya consulta falló (hasta 50), con la Clave SOL de Configuración general.</summary>
    [HttpPost("validar-sunat")]
    public async Task<IActionResult> ValidarSunat()
    { var r = await _service.ValidarSunatAsync(null); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Lo mismo, solo para el comprobante indicado (aunque ya tenga resultado).</summary>
    [HttpPost("{NUM_CPE:decimal}/validar-sunat")]
    public async Task<IActionResult> ValidarSunatUno(decimal NUM_CPE)
    { var r = await _service.ValidarSunatAsync(NUM_CPE); return r.Success ? Ok(r) : BadRequest(r); }
}
