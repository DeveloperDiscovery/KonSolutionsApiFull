using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/entorno/tipos-lectura")]
[ExigePermiso("ENTORNO.TIPOS_LECTURA")]
public class TiposLecturaController : BaseController
{
    private readonly ITiposLecturaService _service;
    public TiposLecturaController(ITiposLecturaService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TIPO_LECTURA}")]
    public async Task<IActionResult> GetById(byte COD_TIPO_LECTURA)
    { var r = await _service.GetByIdAsync(COD_TIPO_LECTURA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposLecturaInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TiposLecturaUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_TIPO_LECTURA}")]
    public async Task<IActionResult> Delete(byte COD_TIPO_LECTURA)
    { var r = await _service.DeleteAsync(COD_TIPO_LECTURA); return r.Success ? Ok(r) : BadRequest(r); }
}
