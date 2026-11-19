using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/costos/costos-productos")]
[ExigePermiso("COSTOS.COSTOS_PRODUCTOS")]
public class CostosProductosController : BaseController
{
    private readonly ICostosProductosService _service;
    public CostosProductosController(ICostosProductosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ALMACEN}/{COD_PROCESO}/{NUM_ORDEN_PRODUCCION}")]
    public async Task<IActionResult> GetById(string COD_ALMACEN, string COD_PROCESO, int NUM_ORDEN_PRODUCCION)
    { var r = await _service.GetByIdAsync(COD_ALMACEN, COD_PROCESO, NUM_ORDEN_PRODUCCION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] CostosProductosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] CostosProductosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CostosProductosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_ALMACEN}/{COD_PROCESO}/{NUM_ORDEN_PRODUCCION}")]
    public async Task<IActionResult> Delete(string COD_ALMACEN, string COD_PROCESO, int NUM_ORDEN_PRODUCCION)
    { var r = await _service.DeleteAsync(COD_ALMACEN, COD_PROCESO, NUM_ORDEN_PRODUCCION); return r.Success ? Ok(r) : BadRequest(r); }

}