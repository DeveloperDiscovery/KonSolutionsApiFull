using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Rfid;

namespace KonSolutionsApi.API.Controllers;

/// <summary>La cola de impresión de etiquetas RFID. El navegador no puede hablar con la impresora de la planta: el botón "Imprimir" de Emisión de TAG deja un TRABAJO
/// aquí y el servicio del agente (<c>agente-rfid servicio</c>, en la PC de la planta) lo toma, imprime las etiquetas de la emisión y va informando.
///
/// Permisos (los mismos de Emisión de TAG, RFID.EMISIONES): GET consulta; POST pide imprimir (la web) y reclama el siguiente trabajo (el servicio); PUT cancela (la web) e informa el avance
/// y cierra el trabajo (el servicio). Así el usuario de servicio del agente no necesita ningún permiso nuevo.</summary>
[Authorize]
[Route("api/v1/rfid/impresion")]
[ExigePermiso("RFID.EMISIONES")]
public class RfidImpresionController : BaseController
{
    private readonly IRfidImpresionService _service;
    public RfidImpresionController(IRfidImpresionService service) => _service = service;

    /// <summary>Los trabajos de una emisión (o de todas), los más recientes primero. <c>FLG_SOLO_ACTIVOS=true</c> deja solo los pendientes y en proceso.</summary>
    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] RfidTrabajoImpresionSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_TRABAJO:int}")]
    public async Task<IActionResult> GetById(int NUM_TRABAJO)
    { var r = await _service.GetByIdAsync(NUM_TRABAJO); return r.Success ? Ok(r) : NotFound(r); }

    /// <summary>La web pide imprimir las etiquetas que faltan de una emisión (o las primeras n) en una impresora.</summary>
    [HttpPost]
    public async Task<IActionResult> Solicitar([FromBody] RfidImprimirDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.SolicitarAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    /// <summary>Cancela un trabajo: pendiente = en el acto; en proceso = el servicio se detiene al terminar la etiqueta que tiene en la impresora.</summary>
    [HttpPut("{NUM_TRABAJO:int}/cancelar")]
    public async Task<IActionResult> Cancelar(int NUM_TRABAJO)
    { var r = await _service.CancelarAsync(NUM_TRABAJO, GetCodUsuario()); return r.Success ? Ok(r) : BadRequest(r); }

    // ───────── lo que usa el servicio del agente ─────────

    /// <summary>El servicio de una impresora toma su siguiente trabajo (queda "en proceso"). Sin trabajos: éxito con Data nulo.</summary>
    [HttpPost("reclamar")]
    public async Task<IActionResult> Reclamar([FromBody] RfidTrabajoReclamarDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.ReclamarAsync(dto, GetCodUsuario()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Cuántas lleva (acumulado) y latido. La respuesta dice si piden cancelar.</summary>
    [HttpPut("{NUM_TRABAJO:int}/progreso")]
    public async Task<IActionResult> Progreso(int NUM_TRABAJO, [FromBody] RfidTrabajoProgresoDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.ProgresoAsync(NUM_TRABAJO, dto, GetCodUsuario()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>El servicio cierra el trabajo: T terminado, F con fallos o C cancelado.</summary>
    [HttpPut("{NUM_TRABAJO:int}/terminar")]
    public async Task<IActionResult> Terminar(int NUM_TRABAJO, [FromBody] RfidTrabajoTerminarDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.TerminarAsync(NUM_TRABAJO, dto, GetCodUsuario()); return r.Success ? Ok(r) : BadRequest(r); }
}
