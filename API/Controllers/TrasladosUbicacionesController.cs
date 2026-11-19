using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Traslados entre ubicaciones de un mismo almacén (de un rack a otro).
///
/// Va con el permiso de movimientos de almacén: un traslado ES un par de movimientos (salida e
/// ingreso), y quien no puede grabar movimientos no debería poder hacerlo por acá.</summary>
[Authorize]
[Route("api/v1/logistica/traslados-ubicaciones")]
[ExigePermiso("LOGISTICA.MOVIMIENTOS_ALMACEN")]
public class TrasladosUbicacionesController : BaseController
{
    private readonly ITrasladosUbicacionesService _service;
    public TrasladosUbicacionesController(ITrasladosUbicacionesService service) => _service = service;

    /// <summary>Las salidas con alcance "Entre ubicaciones del almacén" de la clase.</summary>
    [HttpGet("tipos")]
    public async Task<IActionResult> Tipos([FromQuery] string? COD_CLASE_MOVIMIENTO)
    { var r = await _service.TiposAsync(COD_CLASE_MOVIMIENTO); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Racks, casilleros y ubicaciones sueltas del almacén, con cuántos productos tienen.</summary>
    [HttpGet("mapa")]
    public async Task<IActionResult> Mapa([FromQuery] string? COD_ALMACEN)
    { var r = await _service.MapaAsync(COD_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>El stock de una ubicación, con lo reservado y lo disponible.</summary>
    [HttpGet("stock")]
    public async Task<IActionResult> Stock([FromQuery] string? COD_ALMACEN, [FromQuery] string? COD_UBICACION_ALMACEN)
    { var r = await _service.StockAsync(COD_ALMACEN, COD_UBICACION_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Graba el traslado: la salida, su ingreso y todas las líneas, en una transacción.</summary>
    [HttpPost("registra")]
    public async Task<IActionResult> Registra([FromBody] TrasladoUbicacionesRegistraDto dto)
    { var r = await _service.RegistraAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Los traslados del almacén, una fila por línea, del más reciente al más antiguo.</summary>
    [HttpGet("historial")]
    public async Task<IActionResult> Historial([FromQuery] string? COD_ALMACEN, [FromQuery] DateTime? FEC_INICIAL, [FromQuery] DateTime? FEC_FINAL)
    { var r = await _service.HistorialAsync(COD_ALMACEN, FEC_INICIAL, FEC_FINAL); return r.Success ? Ok(r) : BadRequest(r); }
}
