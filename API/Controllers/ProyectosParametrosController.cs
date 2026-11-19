using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Parámetros del módulo de Proyectos (hoy, la ruta de archivos). GET lee, PUT graba.</summary>
[Authorize]
[Route("api/v1/proyectos/parametros")]
[ExigePermiso("PROYECTOS.PROYECTOS_PARAMETROS")]
public class ProyectosParametrosController : BaseController
{
    private readonly IProyectosParametrosService _service;
    private readonly IProyectosExpedienteService _expediente;
    public ProyectosParametrosController(IProyectosParametrosService service, IProyectosExpedienteService expediente) { _service = service; _expediente = expediente; }

    [HttpGet]
    public async Task<IActionResult> Get() { var r = await _service.GetAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Guardar([FromBody] ProyectosParametrosGuardarDto dto)
    { var r = await _service.GuardarAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Tipos de entidad y cuales cuentan como personal propio de la empresa (para pedir utilidad en los recursos).</summary>
    [HttpGet("tipos-propios")]
    public async Task<IActionResult> TiposPropios() { var r = await _expediente.TiposPropiosAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPut("tipos-propios")]
    public async Task<IActionResult> TiposPropiosGuardar([FromBody] List<string> tipos)
    { var r = await _expediente.TiposPropiosGuardarAsync(tipos ?? new List<string>(), GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }
}
