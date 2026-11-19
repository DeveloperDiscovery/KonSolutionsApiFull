using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/entidades-productos-tipos-precios")]
[ExigePermiso("COMERCIAL.ENTIDADES_PRODUCTOS_TIPOS_PRECIOS")]
public class EntidadesProductosTiposPreciosController : BaseController
{
    private readonly IEntidadesProductosTiposPreciosService _service;
    public EntidadesProductosTiposPreciosController(IEntidadesProductosTiposPreciosService service) => _service = service;

    /// <summary>Los tipos de precio de un par (entidad, producto) — la subgrilla.</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] decimal codEntidad, [FromQuery] string codProducto, [FromQuery] int? codTipoPrecio = null)
    { var r = await _service.GetAllAsync(codEntidad, codProducto, codTipoPrecio); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ENTIDAD}/{COD_PRODUCTO}/{COD_TIPO_PRECIO}/{COD_MONEDA}")]
    public async Task<IActionResult> GetById(decimal COD_ENTIDAD, string COD_PRODUCTO, int COD_TIPO_PRECIO, string COD_MONEDA)
    { var r = await _service.GetByIdAsync(COD_ENTIDAD, COD_PRODUCTO, COD_TIPO_PRECIO, COD_MONEDA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EntidadesProductosTiposPreciosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EntidadesProductosTiposPreciosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EntidadesProductosTiposPreciosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    // La PK son cuatro columnas: la moneda faltaba y por eso esta ruta no borraba nada.
    [HttpDelete("{COD_ENTIDAD}/{COD_PRODUCTO}/{COD_TIPO_PRECIO}/{COD_MONEDA}")]
    public async Task<IActionResult> Delete(decimal COD_ENTIDAD, string COD_PRODUCTO, int COD_TIPO_PRECIO, string COD_MONEDA)
    { var r = await _service.DeleteAsync(COD_ENTIDAD, COD_PRODUCTO, COD_TIPO_PRECIO, COD_MONEDA); return r.Success ? Ok(r) : BadRequest(r); }

}