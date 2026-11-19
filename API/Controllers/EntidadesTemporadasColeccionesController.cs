using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/entidades-temporadas-colecciones")]
[ExigePermiso("COMERCIAL.ENTIDADES_TEMPORADAS_COLECCIONES")]
public class EntidadesTemporadasColeccionesController : BaseController
{
    private readonly IEntidadesTemporadasColeccionesService _service;
    public EntidadesTemporadasColeccionesController(IEntidadesTemporadasColeccionesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] decimal codEntidad, [FromQuery] int numTemporada)
    { var r = await _service.GetAllAsync(codEntidad, numTemporada); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ENTIDAD}/{NUM_TEMPORADA}/{NUM_COLECCION}")]
    public async Task<IActionResult> GetById(decimal COD_ENTIDAD, int NUM_TEMPORADA, int NUM_COLECCION)
    { var r = await _service.GetByIdAsync(COD_ENTIDAD, NUM_TEMPORADA, NUM_COLECCION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] decimal codEntidad, [FromQuery] int numTemporada)
    { var r = await _service.GetComboboxAsync(codEntidad, numTemporada); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EntidadesTemporadasColeccionesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EntidadesTemporadasColeccionesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_ENTIDAD}/{NUM_TEMPORADA}/{NUM_COLECCION}")]
    public async Task<IActionResult> Delete(decimal COD_ENTIDAD, int NUM_TEMPORADA, int NUM_COLECCION)
    { var r = await _service.DeleteAsync(COD_ENTIDAD, NUM_TEMPORADA, NUM_COLECCION); return r.Success ? Ok(r) : BadRequest(r); }
}
