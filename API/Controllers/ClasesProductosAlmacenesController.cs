using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/costos/clases-productos-almacenes")]
[ExigePermiso("COSTOS.CLASES_PRODUCTOS_ALMACENES")]
public class ClasesProductosAlmacenesController : BaseController
{
    private readonly IClasesProductosAlmacenesService _service;
    public ClasesProductosAlmacenesController(IClasesProductosAlmacenesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CLASE_PRODUCTO}/{COD_ALMACEN}")]
    public async Task<IActionResult> GetById(string COD_CLASE_PRODUCTO, string COD_ALMACEN)
    { var r = await _service.GetByIdAsync(COD_CLASE_PRODUCTO, COD_ALMACEN); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ClasesProductosAlmacenesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ClasesProductosAlmacenesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_CLASE_PRODUCTO}/{COD_ALMACEN}")]
    public async Task<IActionResult> Delete(string COD_CLASE_PRODUCTO, string COD_ALMACEN)
    { var r = await _service.DeleteAsync(COD_CLASE_PRODUCTO, COD_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

}