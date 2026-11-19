using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Cadena de aprobación de cada opción de menú.
///
/// La clave lleva COD_OPCION_APLICACION, que es texto libre de hasta 30 caracteres y en varias
/// opciones incluye puntos. Por eso todo va por query string y no por segmentos de ruta: un
/// código con punto o barra rompería el ruteo, y además evita la ambigüedad entre rutas de dos
/// segmentos que ya nos costó un 400 en Pedidos Comerciales.</summary>
[Authorize]
[Route("api/v1/entorno/aplicaciones-opciones-aprobaciones")]
[ExigePermiso("ENTORNO.APLICACIONES_OPCIONES_APROBACIONES")]
public class AplicacionesOpcionesAprobacionesController : BaseController
{
    private readonly IAplicacionesOpcionesAprobacionesService _service;
    public AplicacionesOpcionesAprobacionesController(IAplicacionesOpcionesAprobacionesService service)
        => _service = service;

    /// <summary>Niveles de una opción.</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codAplicacion, [FromQuery] string codOpcionAplicacion)
    {
        if (string.IsNullOrWhiteSpace(codAplicacion) || string.IsNullOrWhiteSpace(codOpcionAplicacion))
            return BadRequest("Indique la aplicación y la opción.");
        var r = await _service.GetAllAsync(codAplicacion, codOpcionAplicacion);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    /// <summary>Cuántos niveles tiene cada opción configurada. Es lo que le permite a la grilla
    /// de opciones marcar cuáles ya tienen flujo sin pedir el detalle de cada una.</summary>
    [HttpGet("resumen")]
    public async Task<IActionResult> GetResumen()
    { var r = await _service.GetResumenAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] AplicacionesOpcionesAprobacionesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("byid")]
    public async Task<IActionResult> GetById([FromQuery] string codAplicacion, [FromQuery] string codOpcionAplicacion,
                                             [FromQuery] int numSecuencia)
    {
        var r = await _service.GetByIdAsync(codAplicacion, codOpcionAplicacion, numSecuencia);
        return r.Success ? Ok(r) : NotFound(r);
    }

    // POST = alta (el SP numera la secuencia), PUT = modificación.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] AplicacionesOpcionesAprobacionesInsertDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
        return r.Success ? StatusCode(201, r) : BadRequest(r);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] AplicacionesOpcionesAprobacionesUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string codAplicacion, [FromQuery] string codOpcionAplicacion,
                                            [FromQuery] int numSecuencia)
    {
        var r = await _service.DeleteAsync(codAplicacion, codOpcionAplicacion, numSecuencia);
        return r.Success ? Ok(r) : BadRequest(r);
    }
}
