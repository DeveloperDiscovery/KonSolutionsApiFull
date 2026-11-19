using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/planeamiento/programacion-lineas-produccion")]
[ExigePermiso("PLANEAMIENTO.PROGRAMACION_LINEAS_PRODUCCION")]
public class ProgramacionLineasProduccionController : BaseController
{
    private readonly IProgramacionLineasProduccionService _service;
    public ProgramacionLineasProduccionController(IProgramacionLineasProduccionService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{FEC_PROGRAMACION}/{COD_ENTIDAD}")]
    public async Task<IActionResult> GetById(DateTime FEC_PROGRAMACION, decimal COD_ENTIDAD)
    { var r = await _service.GetByIdAsync(FEC_PROGRAMACION, COD_ENTIDAD); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ProgramacionLineasProduccionSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ProgramacionLineasProduccionInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProgramacionLineasProduccionUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{FEC_PROGRAMACION}/{COD_ENTIDAD}")]
    public async Task<IActionResult> Delete(DateTime FEC_PROGRAMACION, decimal COD_ENTIDAD)
    { var r = await _service.DeleteAsync(FEC_PROGRAMACION, COD_ENTIDAD); return r.Success ? Ok(r) : BadRequest(r); }

}