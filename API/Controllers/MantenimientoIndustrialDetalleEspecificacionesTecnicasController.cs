using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Mantenimiento;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/mantenimiento/mantenimiento-industrial-detalle-especificaciones-tecnicas")]
[ExigePermiso("MANTENIMIENTO.MANTENIMIENTO_INDUSTRIAL_DETALLE_ESPECIFICACIONES_TECNICAS")]
public class MantenimientoIndustrialDetalleEspecificacionesTecnicasController : BaseController
{
    private readonly IMantenimientoIndustrialDetalleEspecificacionesTecnicasService _service;
    public MantenimientoIndustrialDetalleEspecificacionesTecnicasController(IMantenimientoIndustrialDetalleEspecificacionesTecnicasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_MANTENIMIENTO_INDUSTRIAL}/{NUM_SECUENCIA}/{COD_ESPECIFICACION_TECNICA}")]
    public async Task<IActionResult> GetById(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA)
    { var r = await _service.GetByIdAsync(NUM_MANTENIMIENTO_INDUSTRIAL, NUM_SECUENCIA, COD_ESPECIFICACION_TECNICA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] MantenimientoIndustrialDetalleEspecificacionesTecnicasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] MantenimientoIndustrialDetalleEspecificacionesTecnicasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] MantenimientoIndustrialDetalleEspecificacionesTecnicasUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_MANTENIMIENTO_INDUSTRIAL}/{NUM_SECUENCIA}/{COD_ESPECIFICACION_TECNICA}")]
    public async Task<IActionResult> Delete(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA)
    { var r = await _service.DeleteAsync(NUM_MANTENIMIENTO_INDUSTRIAL, NUM_SECUENCIA, COD_ESPECIFICACION_TECNICA); return r.Success ? Ok(r) : BadRequest(r); }

}