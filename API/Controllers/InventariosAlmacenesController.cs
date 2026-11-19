using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/inventarios-almacenes")]
[ExigePermiso("LOGISTICA.INVENTARIOS_ALMACENES")]
public class InventariosAlmacenesController : BaseController
{
    private readonly IInventariosAlmacenesService _service;
    public InventariosAlmacenesController(IInventariosAlmacenesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? numInventario = null)
    { var r = await _service.GetAllAsync(numInventario); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_INVENTARIO}/{COD_ALMACEN}")]
    public async Task<IActionResult> GetById(int NUM_INVENTARIO, string COD_ALMACEN)
    { var r = await _service.GetByIdAsync(NUM_INVENTARIO, COD_ALMACEN); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] InventariosAlmacenesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] InventariosAlmacenesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] InventariosAlmacenesUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_INVENTARIO}/{COD_ALMACEN}")]
    public async Task<IActionResult> Delete(int NUM_INVENTARIO, string COD_ALMACEN)
    { var r = await _service.DeleteAsync(NUM_INVENTARIO, COD_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

}