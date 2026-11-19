using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/entidades-direcciones")]
[ExigePermiso("COMERCIAL.ENTIDADES_DIRECCIONES")]
public class EntidadesDireccionesController : BaseController
{
    private readonly IEntidadesDireccionesService _service;
    public EntidadesDireccionesController(IEntidadesDireccionesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] decimal codEntidad)
    { var r = await _service.GetAllAsync(codEntidad); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ENTIDAD}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> GetById(decimal COD_ENTIDAD, int NUM_SECUENCIA)
    { var r = await _service.GetByIdAsync(COD_ENTIDAD, NUM_SECUENCIA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_COMERCIAL_ENTIDADES_DIRECCIONES_LOADCOMBOS: TiposDireccion + TiposZona
    /// (Rrhh) + TiposVia + Estado para el diálogo en un solo viaje. codTipoEstado es opcional
    /// (se autorresuelve).</summary>
    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos([FromQuery] int codTipoEstado = 0)
    { var r = await _service.LoadcombosAsync(codTipoEstado); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EntidadesDireccionesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EntidadesDireccionesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EntidadesDireccionesUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_ENTIDAD}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> Delete(decimal COD_ENTIDAD, int NUM_SECUENCIA)
    { var r = await _service.DeleteAsync(COD_ENTIDAD, NUM_SECUENCIA); return r.Success ? Ok(r) : BadRequest(r); }

}