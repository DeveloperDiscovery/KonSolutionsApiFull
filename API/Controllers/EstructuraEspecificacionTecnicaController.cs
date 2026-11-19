using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/ingenieria/estructura-especificacion-tecnica")]
[ExigePermiso("INGENIERIA.ESTRUCTURA_ESPECIFICACION_TECNICA")]
public class EstructuraEspecificacionTecnicaController : BaseController
{
    private readonly IEstructuraEspecificacionTecnicaService _service;
    public EstructuraEspecificacionTecnicaController(IEstructuraEspecificacionTecnicaService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_ESTRUCTURA_ESPECIFICACION_TECNICA}")]
    public async Task<IActionResult> GetById(int NUM_ESTRUCTURA_ESPECIFICACION_TECNICA)
    { var r = await _service.GetByIdAsync(NUM_ESTRUCTURA_ESPECIFICACION_TECNICA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EstructuraEspecificacionTecnicaSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EstructuraEspecificacionTecnicaInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{NUM_ESTRUCTURA_ESPECIFICACION_TECNICA}")]
    public async Task<IActionResult> Delete(int NUM_ESTRUCTURA_ESPECIFICACION_TECNICA)
    { var r = await _service.DeleteAsync(NUM_ESTRUCTURA_ESPECIFICACION_TECNICA); return r.Success ? Ok(r) : BadRequest(r); }

}