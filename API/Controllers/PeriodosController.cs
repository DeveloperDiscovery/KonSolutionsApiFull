using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/periodos")]
[ExigePermiso("ESTANDAR.PERIODOS")]
public class PeriodosController : BaseController
{
    private readonly IPeriodosService _service;
    public PeriodosController(IPeriodosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codTipoPeriodo)
    { var r = await _service.GetAllAsync(codTipoPeriodo); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{numPeriodo}")]
    public async Task<IActionResult> GetById([FromQuery] string codTipoPeriodo, int numPeriodo)
    { var r = await _service.GetByIdAsync(codTipoPeriodo, numPeriodo); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] string codTipoPeriodo)
    { var r = await _service.GetComboboxAsync(codTipoPeriodo); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] PeriodosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] PeriodosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] PeriodosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{numPeriodo}")]
    public async Task<IActionResult> Delete([FromQuery] string codTipoPeriodo, int numPeriodo)
    { var r = await _service.DeleteAsync(codTipoPeriodo, numPeriodo); return r.Success ? Ok(r) : BadRequest(r); }

}
