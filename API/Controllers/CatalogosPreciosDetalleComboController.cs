using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/catalogos-precios-detalle-combo")]
[ExigePermiso("COMERCIAL.CATALOGOS_PRECIOS_DETALLE_COMBO")]
public class CatalogosPreciosDetalleComboController : BaseController
{
    private readonly ICatalogosPreciosDetalleComboService _service;
    public CatalogosPreciosDetalleComboController(ICatalogosPreciosDetalleComboService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_CATALOGO_PRECIOS}/{COD_PRODUCTO}/{COD_PRODUCTO_COMBO}")]
    public async Task<IActionResult> GetById(int NUM_CATALOGO_PRECIOS, string COD_PRODUCTO, string COD_PRODUCTO_COMBO)
    { var r = await _service.GetByIdAsync(NUM_CATALOGO_PRECIOS, COD_PRODUCTO, COD_PRODUCTO_COMBO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] CatalogosPreciosDetalleComboSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] CatalogosPreciosDetalleComboInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CatalogosPreciosDetalleComboUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_CATALOGO_PRECIOS}/{COD_PRODUCTO}/{COD_PRODUCTO_COMBO}")]
    public async Task<IActionResult> Delete(int NUM_CATALOGO_PRECIOS, string COD_PRODUCTO, string COD_PRODUCTO_COMBO)
    { var r = await _service.DeleteAsync(NUM_CATALOGO_PRECIOS, COD_PRODUCTO, COD_PRODUCTO_COMBO); return r.Success ? Ok(r) : BadRequest(r); }

}