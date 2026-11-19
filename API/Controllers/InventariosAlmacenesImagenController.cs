using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Tabla SIN CRUD manual — la puebla un proceso todavía no implementado (toma de
/// imagen de stock). Se expone el CRUD completo por consistencia arquitectónica; el
/// frontend solo consume GetAll (grilla de solo lectura).</summary>
[Authorize]
[Route("api/v1/logistica/inventarios-almacenes-imagen")]
[ExigePermiso("LOGISTICA.INVENTARIOS_ALMACENES_IMAGEN")]
public class InventariosAlmacenesImagenController : BaseController
{
    private readonly IInventariosAlmacenesImagenService _service;
    public InventariosAlmacenesImagenController(IInventariosAlmacenesImagenService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int numInventario, [FromQuery] string codAlmacen)
    { var r = await _service.GetAllAsync(numInventario, codAlmacen); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_INVENTARIO}/{COD_ALMACEN}/{COD_UBICACION_ALMACEN}/{COD_PRODUCTO}/{NUM_LOTE_CONTROL}/{COD_PROCESO}/{NUM_ORDEN_PRODUCCION}/{NUM_ORDEN_PRODUCCION_ITEM}/{NUM_ORDEN_PRODUCCION_UNIDAD}")]
    public async Task<IActionResult> GetById(int NUM_INVENTARIO, string COD_ALMACEN, string COD_UBICACION_ALMACEN, string COD_PRODUCTO, string NUM_LOTE_CONTROL, string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM, int NUM_ORDEN_PRODUCCION_UNIDAD)
    { var r = await _service.GetByIdAsync(NUM_INVENTARIO, COD_ALMACEN, COD_UBICACION_ALMACEN, COD_PRODUCTO, NUM_LOTE_CONTROL, COD_PROCESO, NUM_ORDEN_PRODUCCION, NUM_ORDEN_PRODUCCION_ITEM, NUM_ORDEN_PRODUCCION_UNIDAD);
      return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] int numInventario, [FromQuery] string codAlmacen)
    { var r = await _service.GetComboboxAsync(numInventario, codAlmacen); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] InventariosAlmacenesImagenSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] InventariosAlmacenesImagenInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] InventariosAlmacenesImagenUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_INVENTARIO}/{COD_ALMACEN}/{COD_UBICACION_ALMACEN}/{COD_PRODUCTO}/{NUM_LOTE_CONTROL}/{COD_PROCESO}/{NUM_ORDEN_PRODUCCION}/{NUM_ORDEN_PRODUCCION_ITEM}/{NUM_ORDEN_PRODUCCION_UNIDAD}")]
    public async Task<IActionResult> Delete(int NUM_INVENTARIO, string COD_ALMACEN, string COD_UBICACION_ALMACEN, string COD_PRODUCTO, string NUM_LOTE_CONTROL, string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM, int NUM_ORDEN_PRODUCCION_UNIDAD)
    { var r = await _service.DeleteAsync(NUM_INVENTARIO, COD_ALMACEN, COD_UBICACION_ALMACEN, COD_PRODUCTO, NUM_LOTE_CONTROL, COD_PROCESO, NUM_ORDEN_PRODUCCION, NUM_ORDEN_PRODUCCION_ITEM, NUM_ORDEN_PRODUCCION_UNIDAD);
      return r.Success ? Ok(r) : BadRequest(r); }
}
