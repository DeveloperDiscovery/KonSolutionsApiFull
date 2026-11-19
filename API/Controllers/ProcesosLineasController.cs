using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Produccion;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/produccion/procesos-lineas")]
[ExigePermiso("PRODUCCION.PROCESOS_LINEAS")]
public class ProcesosLineasController : BaseController
{
    private readonly IProcesosLineasService _service;
    public ProcesosLineasController(IProcesosLineasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_PROCESO}/{COD_LINEA_PRODUCCION}")]
    public async Task<IActionResult> GetById(string COD_PROCESO, string COD_LINEA_PRODUCCION)
    { var r = await _service.GetByIdAsync(COD_PROCESO, COD_LINEA_PRODUCCION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ProcesosLineasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ProcesosLineasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProcesosLineasUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_PROCESO}/{COD_LINEA_PRODUCCION}")]
    public async Task<IActionResult> Delete(string COD_PROCESO, string COD_LINEA_PRODUCCION)
    { var r = await _service.DeleteAsync(COD_PROCESO, COD_LINEA_PRODUCCION); return r.Success ? Ok(r) : BadRequest(r); }

}