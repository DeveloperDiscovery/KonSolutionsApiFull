using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Developer;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/developer/tablas-codigos")]
[ExigePermiso("DEVELOPER.TABLAS_CODIGOS")]
public class TablasCodigosController : BaseController
{
    private readonly ITablasCodigosService _service;
    public TablasCodigosController(ITablasCodigosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NAME_SCHEMA}/{NAME_TABLA}")]
    public async Task<IActionResult> GetById(string NAME_SCHEMA, string NAME_TABLA)
    { var r = await _service.GetByIdAsync(NAME_SCHEMA, NAME_TABLA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TablasCodigosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TablasCodigosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TablasCodigosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NAME_SCHEMA}/{NAME_TABLA}")]
    public async Task<IActionResult> Delete(string NAME_SCHEMA, string NAME_TABLA)
    { var r = await _service.DeleteAsync(NAME_SCHEMA, NAME_TABLA); return r.Success ? Ok(r) : BadRequest(r); }

}