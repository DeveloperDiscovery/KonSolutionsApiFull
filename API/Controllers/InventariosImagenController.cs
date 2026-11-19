using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/inventarios-imagen")]
[ExigePermiso("LOGISTICA.INVENTARIOS_IMAGEN")]
public class InventariosImagenController : BaseController
{
    private readonly IInventariosImagenService _service;
    public InventariosImagenController(IInventariosImagenService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_INVENTARIO}/{COD_ALMACEN}/{COD_UBICACION_ALMACEN}")]
    public async Task<IActionResult> GetById(int NUM_INVENTARIO, string COD_ALMACEN, string COD_UBICACION_ALMACEN)
    { var r = await _service.GetByIdAsync(NUM_INVENTARIO, COD_ALMACEN, COD_UBICACION_ALMACEN); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] InventariosImagenSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] InventariosImagenInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] InventariosImagenUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_INVENTARIO}/{COD_ALMACEN}/{COD_UBICACION_ALMACEN}")]
    public async Task<IActionResult> Delete(int NUM_INVENTARIO, string COD_ALMACEN, string COD_UBICACION_ALMACEN)
    { var r = await _service.DeleteAsync(NUM_INVENTARIO, COD_ALMACEN, COD_UBICACION_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

}