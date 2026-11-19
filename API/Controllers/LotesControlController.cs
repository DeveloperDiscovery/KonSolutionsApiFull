using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/lotes-control")]
[ExigePermiso("LOGISTICA.LOTES_CONTROL")]
public class LotesControlController : BaseController
{
    private readonly ILotesControlService _service;
    public LotesControlController(ILotesControlService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_LOTE_CONTROL}")]
    public async Task<IActionResult> GetById(string NUM_LOTE_CONTROL)
    { var r = await _service.GetByIdAsync(NUM_LOTE_CONTROL); return r.Success ? Ok(r) : NotFound(r); }

    /// <summary>codEntidad opcional: con entidad, sólo los lotes de esa entidad; sin ella,
    /// todos. Lo resuelve el SP, que ya recibe el parámetro con DEFAULT NULL.</summary>
    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] decimal? codEntidad = null)
    { var r = await _service.GetComboboxAsync(codEntidad); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] LotesControlSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] LotesControlInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] LotesControlUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_LOTE_CONTROL}")]
    public async Task<IActionResult> Delete(string NUM_LOTE_CONTROL)
    { var r = await _service.DeleteAsync(NUM_LOTE_CONTROL); return r.Success ? Ok(r) : BadRequest(r); }

}