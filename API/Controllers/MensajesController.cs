using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Developer;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/developer/mensajes")]
[ExigePermiso("DEVELOPER.MENSAJES")]
public class MensajesController : BaseController
{
    private readonly IMensajesService _service;
    public MensajesController(IMensajesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_KEY_MENSAJE}")]
    public async Task<IActionResult> GetById(string COD_KEY_MENSAJE)
    { var r = await _service.GetByIdAsync(COD_KEY_MENSAJE); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] MensajesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] MensajesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] MensajesUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_KEY_MENSAJE}")]
    public async Task<IActionResult> Delete(string COD_KEY_MENSAJE)
    { var r = await _service.DeleteAsync(COD_KEY_MENSAJE); return r.Success ? Ok(r) : BadRequest(r); }

}