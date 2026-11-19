using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/entidades-tipos-medio-contacto")]
[ExigePermiso("COMERCIAL.ENTIDADES_TIPOS_MEDIO_CONTACTO")]
public class EntidadesTiposMedioContactoController : BaseController
{
    private readonly IEntidadesTiposMedioContactoService _service;
    public EntidadesTiposMedioContactoController(IEntidadesTiposMedioContactoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] decimal codEntidad)
    { var r = await _service.GetAllAsync(codEntidad); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ENTIDAD}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> GetById(decimal COD_ENTIDAD, short NUM_SECUENCIA)
    { var r = await _service.GetByIdAsync(COD_ENTIDAD, NUM_SECUENCIA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EntidadesTiposMedioContactoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EntidadesTiposMedioContactoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_ENTIDAD}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> Delete(decimal COD_ENTIDAD, short NUM_SECUENCIA)
    { var r = await _service.DeleteAsync(COD_ENTIDAD, NUM_SECUENCIA); return r.Success ? Ok(r) : BadRequest(r); }

}
