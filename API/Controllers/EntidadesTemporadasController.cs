using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/entidades-temporadas")]
[ExigePermiso("COMERCIAL.ENTIDADES_TEMPORADAS")]
public class EntidadesTemporadasController : BaseController
{
    private readonly IEntidadesTemporadasService _service;
    public EntidadesTemporadasController(IEntidadesTemporadasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] decimal codEntidad)
    { var r = await _service.GetAllAsync(codEntidad); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ENTIDAD}/{NUM_TEMPORADA}")]
    public async Task<IActionResult> GetById(decimal COD_ENTIDAD, int NUM_TEMPORADA)
    { var r = await _service.GetByIdAsync(COD_ENTIDAD, NUM_TEMPORADA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] decimal codEntidad)
    { var r = await _service.GetComboboxAsync(codEntidad); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EntidadesTemporadasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_COMERCIAL_ENTIDADES_TEMPORADAS_LOADCOMBOS: años + meses para el diálogo de
    /// Temporadas de Entidad en un solo viaje.</summary>
    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos()
    { var r = await _service.LoadcombosAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EntidadesTemporadasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_ENTIDAD}/{NUM_TEMPORADA}")]
    public async Task<IActionResult> Delete(decimal COD_ENTIDAD, int NUM_TEMPORADA)
    { var r = await _service.DeleteAsync(COD_ENTIDAD, NUM_TEMPORADA); return r.Success ? Ok(r) : BadRequest(r); }

}