using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/familias-producto")]
[ExigePermiso("ESTANDAR.FAMILIAS_PRODUCTO")]
public class FamiliasProductoController : BaseController
{
    private readonly IFamiliasProductoService _service;
    public FamiliasProductoController(IFamiliasProductoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codGrupoProducto)
    { var r = await _service.GetAllAsync(codGrupoProducto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_GRUPO_PRODUCTO}/{COD_FAMILIA_PRODUCTO}")]
    public async Task<IActionResult> GetById(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO)
    { var r = await _service.GetByIdAsync(COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] FamiliasProductoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] FamiliasProductoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_GRUPO_PRODUCTO}/{COD_FAMILIA_PRODUCTO}")]
    public async Task<IActionResult> Delete(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO)
    { var r = await _service.DeleteAsync(COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos()
    { var r = await _service.GetLoadcombosAsync(); return r.Success ? Ok(r) : BadRequest(r); }

}