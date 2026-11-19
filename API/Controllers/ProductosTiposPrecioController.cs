using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/productos-tipos-precio")]
[ExigePermiso("COMERCIAL.PRODUCTOS_TIPOS_PRECIO")]
public class ProductosTiposPrecioController : BaseController
{
    private readonly IProductosTiposPrecioService _service;
    public ProductosTiposPrecioController(IProductosTiposPrecioService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_PRODUCTO}/{COD_TIPO_PRECIO}/{COD_MONEDA}")]
    public async Task<IActionResult> GetById(string COD_PRODUCTO, int COD_TIPO_PRECIO, string COD_MONEDA)
    { var r = await _service.GetByIdAsync(COD_PRODUCTO, COD_TIPO_PRECIO, COD_MONEDA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ProductosTiposPrecioSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ProductosTiposPrecioInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProductosTiposPrecioUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_PRODUCTO}/{COD_TIPO_PRECIO}/{COD_MONEDA}")]
    public async Task<IActionResult> Delete(string COD_PRODUCTO, int COD_TIPO_PRECIO, string COD_MONEDA)
    { var r = await _service.DeleteAsync(COD_PRODUCTO, COD_TIPO_PRECIO, COD_MONEDA); return r.Success ? Ok(r) : BadRequest(r); }

}