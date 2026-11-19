using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/estructura-producto")]
[ExigePermiso("ESTANDAR.ESTRUCTURA_PRODUCTO")]
public class EstructuraProductoController : BaseController
{
    private readonly IEstructuraProductoService _service;
    public EstructuraProductoController(IEstructuraProductoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{codGrupoProducto}")]
    public async Task<IActionResult> GetById(string codGrupoProducto)
    { var r = await _service.GetByIdAsync(codGrupoProducto); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EstructuraProductoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EstructuraProductoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EstructuraProductoUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{codGrupoProducto}")]
    public async Task<IActionResult> Delete(string codGrupoProducto)
    { var r = await _service.DeleteAsync(codGrupoProducto); return r.Success ? Ok(r) : BadRequest(r); }
}
