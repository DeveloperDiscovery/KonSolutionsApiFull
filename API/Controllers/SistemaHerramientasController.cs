using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Sistema;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/sistema/herramientas")]
public class SistemaHerramientasController : BaseController
{
    private readonly ISistemaHerramientasService _service;
    public SistemaHerramientasController(ISistemaHerramientasService service) => _service = service;

    /// <summary>Lista de tablas para el combo de DES_OBJETO. Es metadato del esquema, no
    /// administra ninguna tabla, así que no lleva [ExigePermiso].</summary>
    [HttpGet("tablas")]
    public async Task<IActionResult> GetTablas()
    { var r = await _service.GetTablasAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("generar-sps-text")]
    public async Task<IActionResult> GenerarSpsText([FromBody] GenerarSpsTextRequest dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var r = await _service.GenerarSpsTextAsync(dto, GetCodUsuario(), GetEstacion());
        return r.Success ? Ok(r) : BadRequest(r);
    }
}
