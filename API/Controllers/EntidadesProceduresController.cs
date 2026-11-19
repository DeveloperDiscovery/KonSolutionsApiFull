using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Soporte del generador de sentencias SQL en "Entidades a Configurar" (campo
/// DES_COMMANDSQL): lista de SP candidatos y sus parámetros, para armar el EXEC a mano sin
/// tener que consultar SSMS. Usa el mismo permiso que ENTORNO.ENTIDADES_CONFIGURAR — no es un
/// módulo propio, es una herramienta de esa pantalla.</summary>
[Authorize]
[Route("api/v1/entorno/entidades-procedures")]
[ExigePermiso("ENTORNO.ENTIDADES_CONFIGURAR")]
public class EntidadesProceduresController : BaseController
{
    private readonly IEntidadesProceduresService _service;
    public EntidadesProceduresController(IEntidadesProceduresService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("parameters")]
    public async Task<IActionResult> GetParameters([FromQuery] string name)
    { var r = await _service.GetParametersAsync(name); return r.Success ? Ok(r) : BadRequest(r); }
}
