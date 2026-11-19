using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Produccion;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/produccion/sub-procesos")]
[ExigePermiso("PRODUCCION.SUB_PROCESOS")]
public class SubProcesosController : BaseController
{
    private readonly ISubProcesosService _service;
    public SubProcesosController(ISubProcesosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string COD_PROCESO)
    { var r = await _service.GetAllAsync(COD_PROCESO); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_PROCESO}/{COD_SUB_PROCESO}")]
    public async Task<IActionResult> GetById(string COD_PROCESO, string COD_SUB_PROCESO)
    { var r = await _service.GetByIdAsync(COD_PROCESO, COD_SUB_PROCESO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] string COD_PROCESO)
    { var r = await _service.GetComboboxAsync(COD_PROCESO); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SubProcesosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] SubProcesosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] SubProcesosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_PROCESO}/{COD_SUB_PROCESO}")]
    public async Task<IActionResult> Delete(string COD_PROCESO, string COD_SUB_PROCESO)
    { var r = await _service.DeleteAsync(COD_PROCESO, COD_SUB_PROCESO); return r.Success ? Ok(r) : BadRequest(r); }

}