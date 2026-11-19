using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/tipos-catalogo-precios")]
[ExigePermiso("COMERCIAL.TIPOS_CATALOGO_PRECIOS")]
public class TiposCatalogoPreciosController : BaseController
{
    private readonly ITiposCatalogoPreciosService _service;
    public TiposCatalogoPreciosController(ITiposCatalogoPreciosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TIPO_CATALOGO_PRECIO}")]
    public async Task<IActionResult> GetById(string COD_TIPO_CATALOGO_PRECIO)
    { var r = await _service.GetByIdAsync(COD_TIPO_CATALOGO_PRECIO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TiposCatalogoPreciosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposCatalogoPreciosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TiposCatalogoPreciosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_TIPO_CATALOGO_PRECIO}")]
    public async Task<IActionResult> Delete(string COD_TIPO_CATALOGO_PRECIO)
    { var r = await _service.DeleteAsync(COD_TIPO_CATALOGO_PRECIO); return r.Success ? Ok(r) : BadRequest(r); }

}