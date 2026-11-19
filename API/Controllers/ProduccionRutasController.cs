using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/ingenieria/produccion-rutas")]
[ExigePermiso("INGENIERIA.PRODUCCION_RUTAS")]
public class ProduccionRutasController : BaseController
{
    private readonly IProduccionRutasService _service;
    public ProduccionRutasController(IProduccionRutasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_RUTA_PRODUCCION}")]
    public async Task<IActionResult> GetById(int COD_RUTA_PRODUCCION)
    { var r = await _service.GetByIdAsync(COD_RUTA_PRODUCCION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ProduccionRutasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ProduccionRutasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_RUTA_PRODUCCION}")]
    public async Task<IActionResult> Delete(int COD_RUTA_PRODUCCION)
    { var r = await _service.DeleteAsync(COD_RUTA_PRODUCCION); return r.Success ? Ok(r) : BadRequest(r); }

}