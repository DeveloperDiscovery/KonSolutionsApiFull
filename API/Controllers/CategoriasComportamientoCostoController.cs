using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/costos/categorias-comportamiento-costo")]
[ExigePermiso("COSTOS.CATEGORIAS_COMPORTAMIENTO_COSTO")]
public class CategoriasComportamientoCostoController : BaseController
{
    private readonly ICategoriasComportamientoCostoService _service;
    public CategoriasComportamientoCostoController(ICategoriasComportamientoCostoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CATEGORIA_COMPORTAMIENTO}")]
    public async Task<IActionResult> GetById(string COD_CATEGORIA_COMPORTAMIENTO)
    { var r = await _service.GetByIdAsync(COD_CATEGORIA_COMPORTAMIENTO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] CategoriasComportamientoCostoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] CategoriasComportamientoCostoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CategoriasComportamientoCostoUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_CATEGORIA_COMPORTAMIENTO}")]
    public async Task<IActionResult> Delete(string COD_CATEGORIA_COMPORTAMIENTO)
    { var r = await _service.DeleteAsync(COD_CATEGORIA_COMPORTAMIENTO); return r.Success ? Ok(r) : BadRequest(r); }

}