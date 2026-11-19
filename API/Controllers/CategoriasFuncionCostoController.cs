using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/costos/categorias-funcion-costo")]
[ExigePermiso("COSTOS.CATEGORIAS_FUNCION_COSTO")]
public class CategoriasFuncionCostoController : BaseController
{
    private readonly ICategoriasFuncionCostoService _service;
    public CategoriasFuncionCostoController(ICategoriasFuncionCostoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CATEGORIA_FUNCION}")]
    public async Task<IActionResult> GetById(string COD_CATEGORIA_FUNCION)
    { var r = await _service.GetByIdAsync(COD_CATEGORIA_FUNCION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] CategoriasFuncionCostoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] CategoriasFuncionCostoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CategoriasFuncionCostoUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_CATEGORIA_FUNCION}")]
    public async Task<IActionResult> Delete(string COD_CATEGORIA_FUNCION)
    { var r = await _service.DeleteAsync(COD_CATEGORIA_FUNCION); return r.Success ? Ok(r) : BadRequest(r); }

}