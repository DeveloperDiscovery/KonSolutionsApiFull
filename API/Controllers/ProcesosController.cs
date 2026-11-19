using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Produccion;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/produccion/procesos")]
[ExigePermiso("PRODUCCION.PROCESOS")]
public class ProcesosController : BaseController
{
    private readonly IProcesosService _service;
    public ProcesosController(IProcesosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_PROCESO}")]
    public async Task<IActionResult> GetById(string COD_PROCESO)
    { var r = await _service.GetByIdAsync(COD_PROCESO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ProcesosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_PRODUCCION_PROCESOS_LOADCOMBOS: clases de proceso + tipos de merma para el
    /// diálogo de Procesos en un solo viaje.</summary>
    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos()
    { var r = await _service.LoadcombosAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ProcesosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProcesosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_PROCESO}")]
    public async Task<IActionResult> Delete(string COD_PROCESO)
    { var r = await _service.DeleteAsync(COD_PROCESO); return r.Success ? Ok(r) : BadRequest(r); }

}