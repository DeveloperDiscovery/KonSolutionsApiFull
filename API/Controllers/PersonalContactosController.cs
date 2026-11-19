using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/rrhh/personal-contactos")]
[ExigePermiso("RRHH.PERSONAL_CONTACTOS")]
public class PersonalContactosController : BaseController
{
    private readonly IPersonalContactosService _service;
    public PersonalContactosController(IPersonalContactosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ENTIDAD_PERSONAL}/{COD_ENTIDAD_CONTACTO}")]
    public async Task<IActionResult> GetById(decimal COD_ENTIDAD_PERSONAL, decimal COD_ENTIDAD_CONTACTO)
    { var r = await _service.GetByIdAsync(COD_ENTIDAD_PERSONAL, COD_ENTIDAD_CONTACTO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] PersonalContactosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] PersonalContactosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] PersonalContactosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_ENTIDAD_PERSONAL}/{COD_ENTIDAD_CONTACTO}")]
    public async Task<IActionResult> Delete(decimal COD_ENTIDAD_PERSONAL, decimal COD_ENTIDAD_CONTACTO)
    { var r = await _service.DeleteAsync(COD_ENTIDAD_PERSONAL, COD_ENTIDAD_CONTACTO); return r.Success ? Ok(r) : BadRequest(r); }

}