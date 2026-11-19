using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/entidades-productos")]
[ExigePermiso("COMERCIAL.ENTIDADES_PRODUCTOS")]
public class EntidadesProductosController : BaseController
{
    private readonly IEntidadesProductosService _service;
    public EntidadesProductosController(IEntidadesProductosService service) => _service = service;

    /// <summary>Sirve a las dos pestañas: con codEntidad devuelve los productos de ese
    /// proveedor, con codProducto los proveedores de ese producto. Sin ninguno, todo.</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] decimal? codEntidad = null, [FromQuery] string? codProducto = null)
    { var r = await _service.GetAllAsync(codEntidad, codProducto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ENTIDAD}/{COD_PRODUCTO}")]
    public async Task<IActionResult> GetById(decimal COD_ENTIDAD, string COD_PRODUCTO)
    { var r = await _service.GetByIdAsync(COD_ENTIDAD, COD_PRODUCTO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EntidadesProductosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EntidadesProductosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EntidadesProductosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_ENTIDAD}/{COD_PRODUCTO}")]
    public async Task<IActionResult> Delete(decimal COD_ENTIDAD, string COD_PRODUCTO)
    { var r = await _service.DeleteAsync(COD_ENTIDAD, COD_PRODUCTO); return r.Success ? Ok(r) : BadRequest(r); }

}