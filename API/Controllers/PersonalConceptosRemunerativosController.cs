using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/rrhh/personal-conceptos-remunerativos")]
[ExigePermiso("RRHH.PERSONAL_CONCEPTOS_REMUNERATIVOS")]
public class PersonalConceptosRemunerativosController : BaseController
{
    private readonly IPersonalConceptosRemunerativosService _service;
    public PersonalConceptosRemunerativosController(IPersonalConceptosRemunerativosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ENTIDAD_PERSONAL}/{COD_CONCEPTO_REMUNERATIVO}")]
    public async Task<IActionResult> GetById(decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO)
    { var r = await _service.GetByIdAsync(COD_ENTIDAD_PERSONAL, COD_CONCEPTO_REMUNERATIVO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] PersonalConceptosRemunerativosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] PersonalConceptosRemunerativosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] PersonalConceptosRemunerativosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_ENTIDAD_PERSONAL}/{COD_CONCEPTO_REMUNERATIVO}")]
    public async Task<IActionResult> Delete(decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO)
    { var r = await _service.DeleteAsync(COD_ENTIDAD_PERSONAL, COD_CONCEPTO_REMUNERATIVO); return r.Success ? Ok(r) : BadRequest(r); }

}