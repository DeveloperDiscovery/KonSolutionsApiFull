using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/estructura-rfid")]
[ExigePermiso("ESTANDAR.ESTRUCTURA_RFID")]
public class EstructuraRfidController : BaseController
{
    private readonly IEstructuraRfidService _service;
    public EstructuraRfidController(IEstructuraRfidService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ESTRUCTURA_RFID}")]
    public async Task<IActionResult> GetById(string COD_ESTRUCTURA_RFID)
    { var r = await _service.GetByIdAsync(COD_ESTRUCTURA_RFID); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EstructuraRfidSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EstructuraRfidInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EstructuraRfidUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_ESTRUCTURA_RFID}")]
    public async Task<IActionResult> Delete(string COD_ESTRUCTURA_RFID)
    { var r = await _service.DeleteAsync(COD_ESTRUCTURA_RFID); return r.Success ? Ok(r) : BadRequest(r); }
}
