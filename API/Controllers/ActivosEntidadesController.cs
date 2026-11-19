using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Activos;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/activos/activos-entidades")]
[ExigePermiso("ACTIVOS.ACTIVOS_ENTIDADES")]
public class ActivosEntidadesController : BaseController
{
    private readonly IActivosEntidadesService _service;
    public ActivosEntidadesController(IActivosEntidadesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ACTIVO}/{COD_ENTIDAD}/{FEC_ASIGNACION_INICIO}")]
    public async Task<IActionResult> GetById(string COD_ACTIVO, decimal COD_ENTIDAD, DateTime FEC_ASIGNACION_INICIO)
    { var r = await _service.GetByIdAsync(COD_ACTIVO, COD_ENTIDAD, FEC_ASIGNACION_INICIO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ActivosEntidadesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ActivosEntidadesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ActivosEntidadesUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_ACTIVO}/{COD_ENTIDAD}/{FEC_ASIGNACION_INICIO}")]
    public async Task<IActionResult> Delete(string COD_ACTIVO, decimal COD_ENTIDAD, DateTime FEC_ASIGNACION_INICIO)
    { var r = await _service.DeleteAsync(COD_ACTIVO, COD_ENTIDAD, FEC_ASIGNACION_INICIO); return r.Success ? Ok(r) : BadRequest(r); }

}