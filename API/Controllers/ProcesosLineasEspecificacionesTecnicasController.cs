using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Activos;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/activos/procesos-lineas-especificaciones-tecnicas")]
[ExigePermiso("ACTIVOS.PROCESOS_LINEAS_ESPECIFICACIONES_TECNICAS")]
public class ProcesosLineasEspecificacionesTecnicasController : BaseController
{
    private readonly IProcesosLineasEspecificacionesTecnicasService _service;
    public ProcesosLineasEspecificacionesTecnicasController(IProcesosLineasEspecificacionesTecnicasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_PROCESO}/{COD_LINEA_PRODUCCION}/{COD_ESPECIFICACION_TECNICA}")]
    public async Task<IActionResult> GetById(string COD_PROCESO, string COD_LINEA_PRODUCCION, string COD_ESPECIFICACION_TECNICA)
    { var r = await _service.GetByIdAsync(COD_PROCESO, COD_LINEA_PRODUCCION, COD_ESPECIFICACION_TECNICA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ProcesosLineasEspecificacionesTecnicasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ProcesosLineasEspecificacionesTecnicasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProcesosLineasEspecificacionesTecnicasUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_PROCESO}/{COD_LINEA_PRODUCCION}/{COD_ESPECIFICACION_TECNICA}")]
    public async Task<IActionResult> Delete(string COD_PROCESO, string COD_LINEA_PRODUCCION, string COD_ESPECIFICACION_TECNICA)
    { var r = await _service.DeleteAsync(COD_PROCESO, COD_LINEA_PRODUCCION, COD_ESPECIFICACION_TECNICA); return r.Success ? Ok(r) : BadRequest(r); }

}