using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

// Sin opción propia en SEGURIDAD.APLICACIONES_OPCIONES — mismo permiso que el módulo padre
// InventariosAlmacenesPuntoToma (ver comentario en InventariosAlmacenesPuntoTomaConteoController).
[Authorize]
[Route("api/v1/logistica/inventarios-almacenes-punto-toma-equipos-asignados")]
[ExigePermiso("LOGISTICA.INVENTARIOS_ALMACENES_PUNTO_TOMA")]
public class InventariosAlmacenesPuntoTomaEquiposAsignadosController : BaseController
{
    private readonly IInventariosAlmacenesPuntoTomaEquiposAsignadosService _service;
    public InventariosAlmacenesPuntoTomaEquiposAsignadosController(IInventariosAlmacenesPuntoTomaEquiposAsignadosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int numInventario, [FromQuery] string codAlmacen, [FromQuery] byte numPuntoTomaInventario)
    { var r = await _service.GetAllAsync(numInventario, codAlmacen, numPuntoTomaInventario); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_INVENTARIO}/{COD_ALMACEN}/{NUM_PUNTO_TOMA_INVENTARIO}/{COD_PRODUCTO_EQUIPO}")]
    public async Task<IActionResult> GetById(int NUM_INVENTARIO, string COD_ALMACEN, byte NUM_PUNTO_TOMA_INVENTARIO, string COD_PRODUCTO_EQUIPO)
    { var r = await _service.GetByIdAsync(NUM_INVENTARIO, COD_ALMACEN, NUM_PUNTO_TOMA_INVENTARIO, COD_PRODUCTO_EQUIPO); return r.Success ? Ok(r) : NotFound(r); }

    // OJO: pese al nombre "combobox", este SP devuelve todas las columnas (igual que GetAll).
    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] int numInventario, [FromQuery] string codAlmacen, [FromQuery] byte numPuntoTomaInventario)
    { var r = await _service.GetComboboxAsync(numInventario, codAlmacen, numPuntoTomaInventario); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] InventariosAlmacenesPuntoTomaEquiposAsignadosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] InventariosAlmacenesPuntoTomaEquiposAsignadosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] InventariosAlmacenesPuntoTomaEquiposAsignadosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_INVENTARIO}/{COD_ALMACEN}/{NUM_PUNTO_TOMA_INVENTARIO}/{COD_PRODUCTO_EQUIPO}")]
    public async Task<IActionResult> Delete(int NUM_INVENTARIO, string COD_ALMACEN, byte NUM_PUNTO_TOMA_INVENTARIO, string COD_PRODUCTO_EQUIPO)
    { var r = await _service.DeleteAsync(NUM_INVENTARIO, COD_ALMACEN, NUM_PUNTO_TOMA_INVENTARIO, COD_PRODUCTO_EQUIPO); return r.Success ? Ok(r) : BadRequest(r); }
}
