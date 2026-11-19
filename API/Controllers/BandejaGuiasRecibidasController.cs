using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.API.Filters;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Bandeja de guías recibidas (Logística): la consulta de las guías de remisión que los proveedores y transportistas mandaron por correo (o que alguien dejó en la carpeta) y que cargó el servicio de XML (LOGISTICA.GRE_RECIBIDAS).
/// Es de CONSULTA: lista, ficha, líneas, XML original y PDF; y descartar / reactivar un comprobante pendiente. Generar el ingreso a almacén se sigue haciendo desde Movimientos de Almacén.
/// Tiene su PROPIO permiso (LOGISTICA.GUIAS_RECIBIDAS: GET consulta, PUT descartar) para poder darle la bandeja a quien no usa Movimientos de Almacén; usa los mismos servicios que el controlador de «Traer guía recibida».</summary>
[Authorize]
[Route("api/v1/logistica/guias-recibidas")]
[ExigePermiso("LOGISTICA.GUIAS_RECIBIDAS")]
public class BandejaGuiasRecibidasController : BaseController
{
    private readonly IGreRecibidasService _service;
    public BandejaGuiasRecibidasController(IGreRecibidasService service) => _service = service;

    /// <summary>Lista con filtros opcionales: texto (serie, número, RUC, remitente o descripción de una línea), RUC del remitente, tipo de guía (09 remitente, 31 transportista), estado (PEN, USA, DES) y fechas de emisión.</summary>
    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] GreRecibidaSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_GRE:decimal}")]
    public async Task<IActionResult> GetById(decimal NUM_GRE)
    { var r = await _service.GetByIdAsync(NUM_GRE); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("{NUM_GRE:decimal}/lineas")]
    public async Task<IActionResult> Lineas(decimal NUM_GRE)
    { var r = await _service.LineasAsync(NUM_GRE); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>El XML original, tal como llegó.</summary>
    [HttpGet("{NUM_GRE:decimal}/xml")]
    public async Task<IActionResult> Xml(decimal NUM_GRE)
    { var r = await _service.XmlAsync(NUM_GRE); return r.Success ? Ok(r) : NotFound(r); }

    /// <summary>El PDF que vino junto al XML, listo para mostrarse.</summary>
    [HttpGet("{NUM_GRE:decimal}/pdf")]
    public async Task<IActionResult> Pdf(decimal NUM_GRE)
    { var r = await _service.PdfAsync(NUM_GRE); return r.Success && r.Data is not null ? File(r.Data.CONTENIDO, "application/pdf") : NotFound(r); }

    /// <summary>Descarta una guía pendiente (FLG_DESCARTAR = true) o la devuelve a pendiente (false). Una ya usada en un movimiento de almacén no se toca.</summary>
    [HttpPut("{NUM_GRE:decimal}/descartar")]
    public async Task<IActionResult> Descartar(decimal NUM_GRE, [FromBody] RecibidoDescartarDto dto)
    { var r = await _service.DescartarAsync(NUM_GRE, dto ?? new(), GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>«Completar datos»: registra los proveedores (tipo PRV), asigna los productos por el código del proveedor y registra los lotes de TODOS los guías pendientes.</summary>
    [HttpPost("completar-datos")]
    public async Task<IActionResult> CompletarDatos()
    { var r = await _service.CompletarDatosAsync(null, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Lo mismo, solo para el documento indicado (debe estar pendiente).</summary>
    [HttpPost("{NUM_GRE:decimal}/completar-datos")]
    public async Task<IActionResult> CompletarDatosUno(decimal NUM_GRE)
    { var r = await _service.CompletarDatosAsync(NUM_GRE, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>«Validar con SUNAT» de las guías sin resultado: revisión propia (SUNAT no consulta guías de terceros).</summary>
    [HttpPost("validar-sunat")]
    public async Task<IActionResult> ValidarSunat()
    { var r = await _service.ValidarSunatAsync(null); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Lo mismo, solo para la guía indicada (aunque ya tenga resultado).</summary>
    [HttpPost("{NUM_GRE:decimal}/validar-sunat")]
    public async Task<IActionResult> ValidarSunatUno(decimal NUM_GRE)
    { var r = await _service.ValidarSunatAsync(NUM_GRE); return r.Success ? Ok(r) : BadRequest(r); }
}
