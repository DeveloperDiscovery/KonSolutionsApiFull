using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Activos;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/activos/activos-valorizacion")]
[ExigePermiso("ACTIVOS.ACTIVOS_VALORIZACION")]
public class ActivosValorizacionController : BaseController
{
    private readonly IActivosValorizacionService _service;
    public ActivosValorizacionController(IActivosValorizacionService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ACTIVO}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> GetById(string COD_ACTIVO, int NUM_SECUENCIA)
    { var r = await _service.GetByIdAsync(COD_ACTIVO, NUM_SECUENCIA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ActivosValorizacionSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ActivosValorizacionInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ActivosValorizacionUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_ACTIVO}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> Delete(string COD_ACTIVO, int NUM_SECUENCIA)
    { var r = await _service.DeleteAsync(COD_ACTIVO, NUM_SECUENCIA); return r.Success ? Ok(r) : BadRequest(r); }

}