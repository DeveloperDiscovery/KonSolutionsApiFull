using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/rrhh/personal-asistencia")]
[ExigePermiso("RRHH.PERSONAL_ASISTENCIA")]
public class PersonalAsistenciaController : BaseController
{
    private readonly IPersonalAsistenciaService _service;
    public PersonalAsistenciaController(IPersonalAsistenciaService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_PLANILLA_REMUNERATIVA}/{COD_ENTIDAD_PERSONAL}")]
    public async Task<IActionResult> GetById(int NUM_PLANILLA_REMUNERATIVA, decimal COD_ENTIDAD_PERSONAL)
    { var r = await _service.GetByIdAsync(NUM_PLANILLA_REMUNERATIVA, COD_ENTIDAD_PERSONAL); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] PersonalAsistenciaSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] PersonalAsistenciaInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] PersonalAsistenciaUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_PLANILLA_REMUNERATIVA}/{COD_ENTIDAD_PERSONAL}")]
    public async Task<IActionResult> Delete(int NUM_PLANILLA_REMUNERATIVA, decimal COD_ENTIDAD_PERSONAL)
    { var r = await _service.DeleteAsync(NUM_PLANILLA_REMUNERATIVA, COD_ENTIDAD_PERSONAL); return r.Success ? Ok(r) : BadRequest(r); }

}