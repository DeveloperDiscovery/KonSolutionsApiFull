using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/rrhh/encuestas-preguntas-respuestas")]
[ExigePermiso("RRHH.ENCUESTAS_PREGUNTAS_RESPUESTAS")]
public class EncuestasPreguntasRespuestasController : BaseController
{
    private readonly IEncuestasPreguntasRespuestasService _service;
    public EncuestasPreguntasRespuestasController(IEncuestasPreguntasRespuestasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_ENCUESTA}/{NUM_PREGUNTA}/{NUM_RESPUESTA}")]
    public async Task<IActionResult> GetById(int NUM_ENCUESTA, int NUM_PREGUNTA, int NUM_RESPUESTA)
    { var r = await _service.GetByIdAsync(NUM_ENCUESTA, NUM_PREGUNTA, NUM_RESPUESTA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EncuestasPreguntasRespuestasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EncuestasPreguntasRespuestasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EncuestasPreguntasRespuestasUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_ENCUESTA}/{NUM_PREGUNTA}/{NUM_RESPUESTA}")]
    public async Task<IActionResult> Delete(int NUM_ENCUESTA, int NUM_PREGUNTA, int NUM_RESPUESTA)
    { var r = await _service.DeleteAsync(NUM_ENCUESTA, NUM_PREGUNTA, NUM_RESPUESTA); return r.Success ? Ok(r) : BadRequest(r); }

}