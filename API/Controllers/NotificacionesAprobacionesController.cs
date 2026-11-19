using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[SinControlPermisos("Ficha y acciones de una notificación de aprobación dirigida al propio usuario. Los SP reciben el usuario del token: "
                    + "la ficha solo se entrega a quien participa de la notificación y la acción solo la ejecuta el aprobador vigente.")]
[Route("api/v1/entorno/notificaciones-aprobaciones")]
public class NotificacionesAprobacionesController : BaseController
{
    private readonly INotificacionesAprobacionesService _service;
    public NotificacionesAprobacionesController(INotificacionesAprobacionesService service) => _service = service;

    /// <summary>Ficha completa de una notificación de aprobación (cabecera + documentos + ejecución).</summary>
    [HttpGet("{numNotificacion:int}")]
    public async Task<IActionResult> GetById(int numNotificacion)
    {
        var r = await _service.GetByIdAsync(numNotificacion, GetCodUsuario());
        return r.Success ? Ok(r) : NotFound(r);
    }

    /// <summary>
    /// Aprobar, rechazar u observar. El cuerpo solo trae la justificación: el servidor comprueba que seas el aprobador vigente
    /// y ejecuta el comando guardado de esa acción (aprobar, rechazar u observar en la ruta). Nada de SQL viaja desde el cliente.
    /// </summary>
    [HttpPost("{numNotificacion:int}/{accion}")]
    public async Task<IActionResult> Accion(int numNotificacion, string accion, [FromBody] NotificacionAprobacionAccionDto? dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var r = await _service.AccionAsync(numNotificacion, accion, dto ?? new NotificacionAprobacionAccionDto(), GetCodUsuario(), GetEstacion());
        return r.Success ? Ok(r) : BadRequest(r);
    }
}
