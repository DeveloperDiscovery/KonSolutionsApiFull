using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Catálogo de tipos de ubicación. Sin endpoint de búsqueda: la tabla no tiene
/// SP _SEARCH y el listado completo es corto, así que se filtra en el cliente.</summary>
[Authorize]
[Route("api/v1/logistica/tipos-ubicacion")]
[ExigePermiso("LOGISTICA.TIPOS_UBICACION")]
public class TiposUbicacionController : BaseController
{
    private readonly ITiposUbicacionService _service;
    public TiposUbicacionController(ITiposUbicacionService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TIPO_UBICACION}")]
    public async Task<IActionResult> GetById(string COD_TIPO_UBICACION)
    { var r = await _service.GetByIdAsync(COD_TIPO_UBICACION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposUbicacionInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_TIPO_UBICACION}")]
    public async Task<IActionResult> Delete(string COD_TIPO_UBICACION)
    { var r = await _service.DeleteAsync(COD_TIPO_UBICACION); return r.Success ? Ok(r) : BadRequest(r); }
}
