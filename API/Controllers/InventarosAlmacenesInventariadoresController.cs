using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/inventaros-almacenes-inventariadores")]
[ExigePermiso("LOGISTICA.INVENTAROS_ALMACENES_INVENTARIADORES")]
public class InventarosAlmacenesInventariadoresController : BaseController
{
    private readonly IInventarosAlmacenesInventariadoresService _service;
    public InventarosAlmacenesInventariadoresController(IInventarosAlmacenesInventariadoresService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_INVENTARIO}/{COD_ALMACEN}/{COD_ENTIDAD_INVENTARIADOR}")]
    public async Task<IActionResult> GetById(int NUM_INVENTARIO, string COD_ALMACEN, decimal COD_ENTIDAD_INVENTARIADOR)
    { var r = await _service.GetByIdAsync(NUM_INVENTARIO, COD_ALMACEN, COD_ENTIDAD_INVENTARIADOR); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] InventarosAlmacenesInventariadoresSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] InventarosAlmacenesInventariadoresInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] InventarosAlmacenesInventariadoresUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_INVENTARIO}/{COD_ALMACEN}/{COD_ENTIDAD_INVENTARIADOR}")]
    public async Task<IActionResult> Delete(int NUM_INVENTARIO, string COD_ALMACEN, decimal COD_ENTIDAD_INVENTARIADOR)
    { var r = await _service.DeleteAsync(NUM_INVENTARIO, COD_ALMACEN, COD_ENTIDAD_INVENTARIADOR); return r.Success ? Ok(r) : BadRequest(r); }

}