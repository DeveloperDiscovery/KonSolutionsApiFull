using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/productos-especificaciones-tecnicas")]
[ExigePermiso("ESTANDAR.PRODUCTOS_ESPECIFICACIONES_TECNICAS")]
public class ProductosEspecificacionesTecnicasController : BaseController
{
    private readonly IProductosEspecificacionesTecnicasService _service;
    public ProductosEspecificacionesTecnicasController(IProductosEspecificacionesTecnicasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codProducto)
    { var r = await _service.GetAllAsync(codProducto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_PRODUCTO}/{COD_ESPECIFICACION_TECNICA}")]
    public async Task<IActionResult> GetById(string COD_PRODUCTO, string COD_ESPECIFICACION_TECNICA)
    { var r = await _service.GetByIdAsync(COD_PRODUCTO, COD_ESPECIFICACION_TECNICA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ProductosEspecificacionesTecnicasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ProductosEspecificacionesTecnicasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_PRODUCTO}/{COD_ESPECIFICACION_TECNICA}")]
    public async Task<IActionResult> Delete(string COD_PRODUCTO, string COD_ESPECIFICACION_TECNICA)
    { var r = await _service.DeleteAsync(COD_PRODUCTO, COD_ESPECIFICACION_TECNICA); return r.Success ? Ok(r) : BadRequest(r); }
}
