using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Catalogo de plantillas de documento de proyecto (acta de constitucion, de entrega, de cierre, contrato, cronograma a aprobar).</summary>
[Authorize]
[Route("api/v1/proyectos/plantillas-documento")]
[ExigePermiso("PROYECTOS.PLANTILLAS_DOCUMENTO")]
public class PlantillasDocumentoController : BaseController
{
    private readonly IPlantillasDocumentoService _service;
    public PlantillasDocumentoController(IPlantillasDocumentoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_PLANTILLA_DOCUMENTO}")]
    public async Task<IActionResult> GetById(string COD_PLANTILLA_DOCUMENTO) { var r = await _service.GetByIdAsync(COD_PLANTILLA_DOCUMENTO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] PlantillaDocumentoSearchDto dto) { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> Combo() { var r = await _service.ComboAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] PlantillaDocumentoDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState); var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] PlantillaDocumentoDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState); var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_PLANTILLA_DOCUMENTO}")]
    public async Task<IActionResult> Delete(string COD_PLANTILLA_DOCUMENTO) { var r = await _service.DeleteAsync(COD_PLANTILLA_DOCUMENTO); return r.Success ? Ok(r) : BadRequest(r); }
}
