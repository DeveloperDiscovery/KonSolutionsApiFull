using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/inventarios-almacenes-ubicaciones")]
[ExigePermiso("LOGISTICA.INVENTARIOS_ALMACENES_UBICACIONES")]
public class InventariosAlmacenesUbicacionesController : BaseController
{
    private readonly IInventariosAlmacenesUbicacionesService _service;
    public InventariosAlmacenesUbicacionesController(IInventariosAlmacenesUbicacionesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int numInventario, [FromQuery] string codAlmacen)
    { var r = await _service.GetAllAsync(numInventario, codAlmacen); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_INVENTARIO}/{COD_ALMACEN}/{COD_UBICACION_ALMACEN}")]
    public async Task<IActionResult> GetById(int NUM_INVENTARIO, string COD_ALMACEN, string COD_UBICACION_ALMACEN)
    { var r = await _service.GetByIdAsync(NUM_INVENTARIO, COD_ALMACEN, COD_UBICACION_ALMACEN); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] int numInventario, [FromQuery] string codAlmacen)
    { var r = await _service.GetComboboxAsync(numInventario, codAlmacen); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] InventariosAlmacenesUbicacionesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // El SP _INSERT es upsert (actualiza si ya existe, inserta si no) — no hay verbo PUT/Update.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] InventariosAlmacenesUbicacionesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{NUM_INVENTARIO}/{COD_ALMACEN}/{COD_UBICACION_ALMACEN}")]
    public async Task<IActionResult> Delete(int NUM_INVENTARIO, string COD_ALMACEN, string COD_UBICACION_ALMACEN)
    { var r = await _service.DeleteAsync(NUM_INVENTARIO, COD_ALMACEN, COD_UBICACION_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }
}
