using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/entorno/ejecutar-sentencias")]
[ExigePermiso("ENTORNO.EJECUTAR_SENTENCIAS")]
public class EjecutarSentenciasController : BaseController
{
    private readonly IEjecutarSentenciasService _service;
    public EjecutarSentenciasController(IEjecutarSentenciasService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Execute([FromBody] EjecutarSentenciaDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.ExecuteAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }
}
