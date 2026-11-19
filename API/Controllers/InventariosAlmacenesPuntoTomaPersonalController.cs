using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

// Sin opción propia en SEGURIDAD.APLICACIONES_OPCIONES — mismo permiso que el módulo padre
// InventariosAlmacenesPuntoToma (ver comentario en InventariosAlmacenesPuntoTomaConteoController).
// Sin PUT: tabla de asignación pura (Asignar/Quitar), ver comentario en el Domain Model.
[Authorize]
[Route("api/v1/logistica/inventarios-almacenes-punto-toma-personal")]
[ExigePermiso("LOGISTICA.INVENTARIOS_ALMACENES_PUNTO_TOMA")]
public class InventariosAlmacenesPuntoTomaPersonalController : BaseController
{
    private readonly IInventariosAlmacenesPuntoTomaPersonalService _service;
    public InventariosAlmacenesPuntoTomaPersonalController(IInventariosAlmacenesPuntoTomaPersonalService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int numInventario, [FromQuery] string codAlmacen, [FromQuery] byte numPuntoTomaInventario)
    { var r = await _service.GetAllAsync(numInventario, codAlmacen, numPuntoTomaInventario); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_INVENTARIO}/{COD_ALMACEN}/{NUM_PUNTO_TOMA_INVENTARIO}/{COD_ENTIDAD_ASIGNADA}")]
    public async Task<IActionResult> GetById(int NUM_INVENTARIO, string COD_ALMACEN, byte NUM_PUNTO_TOMA_INVENTARIO, decimal COD_ENTIDAD_ASIGNADA)
    { var r = await _service.GetByIdAsync(NUM_INVENTARIO, COD_ALMACEN, NUM_PUNTO_TOMA_INVENTARIO, COD_ENTIDAD_ASIGNADA); return r.Success ? Ok(r) : NotFound(r); }

    // OJO: pese al nombre "combobox", este SP devuelve todas las columnas (igual que GetAll).
    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] int numInventario, [FromQuery] string codAlmacen, [FromQuery] byte numPuntoTomaInventario)
    { var r = await _service.GetComboboxAsync(numInventario, codAlmacen, numPuntoTomaInventario); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] InventariosAlmacenesPuntoTomaPersonalSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = Asignar (el SP hace upsert internamente).
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] InventariosAlmacenesPuntoTomaPersonalInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{NUM_INVENTARIO}/{COD_ALMACEN}/{NUM_PUNTO_TOMA_INVENTARIO}/{COD_ENTIDAD_ASIGNADA}")]
    public async Task<IActionResult> Delete(int NUM_INVENTARIO, string COD_ALMACEN, byte NUM_PUNTO_TOMA_INVENTARIO, decimal COD_ENTIDAD_ASIGNADA)
    { var r = await _service.DeleteAsync(NUM_INVENTARIO, COD_ALMACEN, NUM_PUNTO_TOMA_INVENTARIO, COD_ENTIDAD_ASIGNADA); return r.Success ? Ok(r) : BadRequest(r); }
}
