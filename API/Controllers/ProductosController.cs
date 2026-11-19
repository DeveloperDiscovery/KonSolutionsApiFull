using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/productos")]
[ExigePermiso("ESTANDAR.PRODUCTOS")]
public class ProductosController : BaseController
{
    private readonly IProductosService _service;
    public ProductosController(IProductosService service) => _service = service;

    // Los filtros van [FromQuery] y son OPCIONALES: una llamada sin nada sigue devolviendo
    // el maestro completo, como siempre. Hay otras pantallas que piden este endpoint sin
    // filtros (la Matriz de Especificaciones Tecnicas, sin ir mas lejos) y no se enteran.
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ProductosSearchDto? filtros)
    { var r = await _service.GetAllAsync(filtros); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_PRODUCTO}")]
    public async Task<IActionResult> GetById(string COD_PRODUCTO)
    { var r = await _service.GetByIdAsync(COD_PRODUCTO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    // ── Panel lateral de filtro por Especificaciones Tecnicas ────────────────────
    // El arbol: que especificaciones y que items tienen REGISTRADOS los productos de la
    // clasificacion vigente, con cuantos productos cada uno.
    [HttpGet("especificaciones-arbol")]
    public async Task<IActionResult> GetEspecificacionesArbol([FromQuery] ProductosSearchDto? filtros)
    { var r = await _service.GetEspecificacionesArbolAsync(filtros); return r.Success ? Ok(r) : BadRequest(r); }

    // Los productos que cumplen los checks. Sin checks devuelve lo mismo que GetAll, asi que
    // la pantalla puede usar SIEMPRE este endpoint y no tener dos caminos.
    [HttpGet("filtrar-especificaciones")]
    public async Task<IActionResult> FiltrarPorEspecificaciones([FromQuery] ProductosSearchDto dto)
    { var r = await _service.FiltrarPorEspecificacionesAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ProductosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ProductosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    // PUT y no POST: PROC_ESTANDAR_PRODUCTOS_INSERT rechaza una clave que ya existe
    // ("Ya existe un registro con esa clave"), asi que la modificacion necesita su propio
    // verbo y su propio procedimiento. Antes el dialogo mandaba todo por POST y modificar
    // un producto era imposible.
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProductosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_PRODUCTO}")]
    public async Task<IActionResult> Delete(string COD_PRODUCTO)
    { var r = await _service.DeleteAsync(COD_PRODUCTO); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos()
    { var r = await _service.GetLoadcombosAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("{COD_PRODUCTO}/actualizar-descripcion-detalle")]
    public async Task<IActionResult> ActualizarDescripcionDetalle(string COD_PRODUCTO)
    { var r = await _service.ActualizarDescripcionDetalleAsync(COD_PRODUCTO); return r.Success ? Ok(r) : BadRequest(r); }

}