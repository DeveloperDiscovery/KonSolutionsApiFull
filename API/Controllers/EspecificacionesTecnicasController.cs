using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/ingenieria/especificaciones-tecnicas")]
[ExigePermiso("INGENIERIA.ESPECIFICACIONES_TECNICAS")]
public class EspecificacionesTecnicasController : BaseController
{
    private readonly IEspecificacionesTecnicasService _service;
    public EspecificacionesTecnicasController(IEspecificacionesTecnicasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ESPECIFICACION_TECNICA}")]
    public async Task<IActionResult> GetById(string COD_ESPECIFICACION_TECNICA)
    { var r = await _service.GetByIdAsync(COD_ESPECIFICACION_TECNICA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EspecificacionesTecnicasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EspecificacionesTecnicasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_ESPECIFICACION_TECNICA}")]
    public async Task<IActionResult> Delete(string COD_ESPECIFICACION_TECNICA)
    { var r = await _service.DeleteAsync(COD_ESPECIFICACION_TECNICA); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos()
    { var r = await _service.GetLoadcombosAsync(); return r.Success ? Ok(r) : BadRequest(r); }

}
