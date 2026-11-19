using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;
namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/tablas-tipos-estado")]
[ExigePermiso("ESTANDAR.TABLAS_TIPOS_ESTADO")]
public class TablasTiposEstadoController : BaseController
{
    private readonly ITablasTiposEstadoService _service;
    public TablasTiposEstadoController(ITablasTiposEstadoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{DES_TABLA}")]
    public async Task<IActionResult> GetById(string DES_TABLA)
    { var r = await _service.GetByIdAsync(DES_TABLA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TablasTiposEstadoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TablasTiposEstadoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState); var r = await _service.InsertAsync(dto); return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{DES_TABLA}")]
    public async Task<IActionResult> Delete(string DES_TABLA)
    { var r = await _service.DeleteAsync(DES_TABLA); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_ENTORNO_TABLAS_TIPOS_ESTADO_TABLASDISPONIBLES: tablas del sistema que aún
    /// no están configuradas — para el combo de "Nuevo" (no tiene sentido reconfigurar una ya existente).</summary>
    [HttpGet("tablas-disponibles")]
    public async Task<IActionResult> GetTablasDisponibles()
    { var r = await _service.TablasDisponiblesAsync(); return r.Success ? Ok(r) : BadRequest(r); }
}
