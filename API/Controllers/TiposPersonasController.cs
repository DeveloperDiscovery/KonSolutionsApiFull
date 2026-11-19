using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/tipos-personas")]
[ExigePermiso("ESTANDAR.TIPOS_PERSONAS")]
public class TiposPersonasController : BaseController
{
    private readonly ITiposPersonasService _service;
    public TiposPersonasController(ITiposPersonasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TIPO_PERSONA}")]
    public async Task<IActionResult> GetById(byte COD_TIPO_PERSONA)
    { var r = await _service.GetByIdAsync(COD_TIPO_PERSONA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposPersonasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_TIPO_PERSONA}")]
    public async Task<IActionResult> Delete(byte COD_TIPO_PERSONA)
    { var r = await _service.DeleteAsync(COD_TIPO_PERSONA); return r.Success ? Ok(r) : BadRequest(r); }

}
