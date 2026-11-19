using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/tipos-producto")]
[ExigePermiso("ESTANDAR.TIPOS_PRODUCTO")]
public class TiposProductoController : BaseController
{
    private readonly ITiposProductoService _service;
    public TiposProductoController(ITiposProductoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TIPO_PRODUCTO}")]
    public async Task<IActionResult> GetById(string COD_TIPO_PRODUCTO)
    { var r = await _service.GetByIdAsync(COD_TIPO_PRODUCTO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TiposProductoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposProductoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_TIPO_PRODUCTO}")]
    public async Task<IActionResult> Delete(string COD_TIPO_PRODUCTO)
    { var r = await _service.DeleteAsync(COD_TIPO_PRODUCTO); return r.Success ? Ok(r) : BadRequest(r); }

}