using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Contabilidad;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/contabilidad/centros-costo")]
[ExigePermiso("CONTABILIDAD.CENTROS_COSTO")]
public class CentrosCostoController : BaseController
{
    private readonly ICentrosCostoService _service;
    public CentrosCostoController(ICentrosCostoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CENTRO_COSTO}")]
    public async Task<IActionResult> GetById(string COD_CENTRO_COSTO)
    { var r = await _service.GetByIdAsync(COD_CENTRO_COSTO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] CentrosCostoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] CentrosCostoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_CENTRO_COSTO}")]
    public async Task<IActionResult> Delete(string COD_CENTRO_COSTO)
    { var r = await _service.DeleteAsync(COD_CENTRO_COSTO); return r.Success ? Ok(r) : BadRequest(r); }

}