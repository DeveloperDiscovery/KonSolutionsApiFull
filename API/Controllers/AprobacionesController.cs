using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Aprobaciones;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/aprobaciones/aprobaciones")]
[ExigePermiso("APROBACIONES.APROBACIONES")]
public class AprobacionesController : BaseController
{
    private readonly IAprobacionesService _service;
    public AprobacionesController(IAprobacionesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_APROBACION}")]
    public async Task<IActionResult> GetById(int NUM_APROBACION)
    { var r = await _service.GetByIdAsync(NUM_APROBACION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] AprobacionesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT de este módulo ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] AprobacionesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] AprobacionesUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_APROBACION}")]
    public async Task<IActionResult> Delete(int NUM_APROBACION)
    { var r = await _service.DeleteAsync(NUM_APROBACION); return r.Success ? Ok(r) : BadRequest(r); }

}