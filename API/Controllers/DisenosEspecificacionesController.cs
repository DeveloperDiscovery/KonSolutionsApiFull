using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Especificaciones tecnicas de una version de diseno: las que pide la configuracion de su sub familia / familia / grupo, con su valor
/// (script SQL 113). Permisos: igual que la estructura y la ruta, los de la version (INGENIERIA.DISENOS_VERSION): ver = GET, grabar = POST.</summary>
[Authorize]
[Route("api/v1/ingenieria/disenos-especificaciones")]
[ExigePermiso("INGENIERIA.DISENOS_VERSION")]
public class DisenosEspecificacionesController : BaseController
{
    private readonly IDisenosEspecificacionesService _service;
    public DisenosEspecificacionesController(IDisenosEspecificacionesService service) => _service = service;

    [HttpGet("{codDiseno}/{numVersion:int}")]
    public async Task<IActionResult> Panel(string codDiseno, int numVersion)
    { var r = await _service.PanelAsync(codDiseno, numVersion); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("guardar")]
    public async Task<IActionResult> Guardar([FromBody] DisenosEspecificacionesGuardarDto dto)
    { var r = await _service.GuardarAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }
}
