using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/ingenieria/especificaciones-items-tecnicos")]
[ExigePermiso("INGENIERIA.ESPECIFICACIONES_TECNICAS")]
public class EspecificacionesItemsTecnicosController : BaseController
{
    private readonly IEspecificacionesItemsTecnicosService _service;
    public EspecificacionesItemsTecnicosController(IEspecificacionesItemsTecnicosService service) => _service = service;

    [HttpGet("treelist")]
    public async Task<IActionResult> GetTreelist([FromQuery] decimal codEntidad)
    {
        var r = await _service.GetTreelistAsync(codEntidad);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpGet("treelist-por-especificacion")]
    public async Task<IActionResult> GetTreelistPorEspecificacion([FromQuery] string? codEspecificacionTecnica)
    {
        var r = await _service.GetTreelistPorEspecificacionAsync(codEspecificacionTecnica);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpGet("combo-dependientes")]
    public async Task<IActionResult> GetComboDependientes(
        [FromQuery] string codEspecificacionTecnica,
        [FromQuery] string? codEspecificacionTecnicaDependiente,
        [FromQuery] int? codItemTecnicoDependiente,
        [FromQuery] decimal? codEntidadDependiente)
    {
        var r = await _service.GetComboDependientesAsync(codEspecificacionTecnica, codEspecificacionTecnicaDependiente, codItemTecnicoDependiente, codEntidadDependiente);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EspecificacionesItemsTecnicosUpdateDto dto)
    {
        var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpDelete("{codEspecificacionTecnica}/{codItemTecnico}")]
    public async Task<IActionResult> Delete(string codEspecificacionTecnica, string codItemTecnico)
    {
        var r = await _service.DeleteAsync(codEspecificacionTecnica, codItemTecnico);
        return r.Success ? Ok(r) : BadRequest(r);
    }
}
