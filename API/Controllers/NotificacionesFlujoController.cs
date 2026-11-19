using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/entorno/notificaciones-flujo")]
[ExigePermiso("ENTORNO.NOTIFICACIONES_FLUJO")]
public class NotificacionesFlujoController : BaseController
{
    private readonly INotificacionesFlujoService _service;
    public NotificacionesFlujoController(INotificacionesFlujoService service) => _service = service;

    /// <summary>Notificaciones pendientes + resumen por tipo del usuario logueado — para el grupo de íconos del AppBar.</summary>
    [HttpGet("pendientes")]
    public async Task<IActionResult> Pendientes()
    { var r = await _service.UsuarioPendienteAsync(GetCodUsuario()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Listado de consulta. recepcionada=true → Recibidas; false → Emitidas.
    /// Sin codUsuario usa el del token (cada quien ve las suyas).</summary>
    [HttpGet("showall")]
    public async Task<IActionResult> Showall([FromQuery] bool recepcionada, [FromQuery] string? codUsuario = null)
    {
        var cod = string.IsNullOrWhiteSpace(codUsuario) ? GetCodUsuario() : codUsuario;
        var r = await _service.ShowallAsync(cod, recepcionada);
        return r.Success ? Ok(r) : BadRequest(r);
    }
}
