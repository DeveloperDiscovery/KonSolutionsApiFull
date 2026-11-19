using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/rrhh/encuestas-areas")]
[ExigePermiso("RRHH.ENCUESTAS_AREAS")]
public class EncuestasAreasController : BaseController
{
    private readonly IEncuestasAreasService _service;
    public EncuestasAreasController(IEncuestasAreasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_ENCUESTA}/{COD_AREA}")]
    public async Task<IActionResult> GetById(int NUM_ENCUESTA, int COD_AREA)
    { var r = await _service.GetByIdAsync(NUM_ENCUESTA, COD_AREA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EncuestasAreasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EncuestasAreasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{NUM_ENCUESTA}/{COD_AREA}")]
    public async Task<IActionResult> Delete(int NUM_ENCUESTA, int COD_AREA)
    { var r = await _service.DeleteAsync(NUM_ENCUESTA, COD_AREA); return r.Success ? Ok(r) : BadRequest(r); }

}