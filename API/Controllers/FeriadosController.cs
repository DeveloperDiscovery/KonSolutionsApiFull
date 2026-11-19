using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Feriados (independientes de sábados y domingos). La clave va en la ruta como yyyy-MM-dd.</summary>
[Authorize]
[Route("api/v1/estandar/feriados")]
[ExigePermiso("ESTANDAR.FERIADOS")]
public class FeriadosController : BaseController
{
    private readonly IFeriadosService _service;
    public FeriadosController(IFeriadosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{fecha:datetime}")]
    public async Task<IActionResult> GetById(DateTime fecha) { var r = await _service.GetByIdAsync(fecha); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] FeriadosSearchDto dto) { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] FeriadosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState); var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] FeriadosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState); var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{fecha:datetime}")]
    public async Task<IActionResult> Delete(DateTime fecha) { var r = await _service.DeleteAsync(fecha); return r.Success ? Ok(r) : BadRequest(r); }
}
