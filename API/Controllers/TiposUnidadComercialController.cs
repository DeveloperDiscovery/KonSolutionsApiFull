using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/tipos-unidad-comercial")]
[ExigePermiso("COMERCIAL.TIPOS_UNIDAD_COMERCIAL")]
public class TiposUnidadComercialController : BaseController
{
    private readonly ITiposUnidadComercialService _service;
    public TiposUnidadComercialController(ITiposUnidadComercialService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TIPO_UNIDAD_COMERCIAL}")]
    public async Task<IActionResult> GetById(string COD_TIPO_UNIDAD_COMERCIAL)
    { var r = await _service.GetByIdAsync(COD_TIPO_UNIDAD_COMERCIAL); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TiposUnidadComercialSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposUnidadComercialInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TiposUnidadComercialUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_TIPO_UNIDAD_COMERCIAL}")]
    public async Task<IActionResult> Delete(string COD_TIPO_UNIDAD_COMERCIAL)
    { var r = await _service.DeleteAsync(COD_TIPO_UNIDAD_COMERCIAL); return r.Success ? Ok(r) : BadRequest(r); }

}