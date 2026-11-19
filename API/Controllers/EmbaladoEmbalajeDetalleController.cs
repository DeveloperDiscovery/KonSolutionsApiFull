using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/embalado-embalaje-detalle")]
[ExigePermiso("LOGISTICA.EMBALADO_EMBALAJE_DETALLE")]
public class EmbaladoEmbalajeDetalleController : BaseController
{
    private readonly IEmbaladoEmbalajeDetalleService _service;
    public EmbaladoEmbalajeDetalleController(IEmbaladoEmbalajeDetalleService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_EMBALADO}/{NUM_SECUENCIA_EMBALAJE}")]
    public async Task<IActionResult> GetById(decimal NUM_EMBALADO, int NUM_SECUENCIA_EMBALAJE)
    { var r = await _service.GetByIdAsync(NUM_EMBALADO, NUM_SECUENCIA_EMBALAJE); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EmbaladoEmbalajeDetalleSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EmbaladoEmbalajeDetalleInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EmbaladoEmbalajeDetalleUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_EMBALADO}/{NUM_SECUENCIA_EMBALAJE}")]
    public async Task<IActionResult> Delete(decimal NUM_EMBALADO, int NUM_SECUENCIA_EMBALAJE)
    { var r = await _service.DeleteAsync(NUM_EMBALADO, NUM_SECUENCIA_EMBALAJE); return r.Success ? Ok(r) : BadRequest(r); }

}