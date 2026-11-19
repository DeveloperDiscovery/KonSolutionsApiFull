using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Application.Services;
using KonSolutionsApi.Common;

namespace KonSolutionsApi.API.Controllers;

/// <summary>
/// Ficha técnica de los botones de acción (Clase / Método / SP), para el menú contextual
/// de AccionesBotones.razor.
///
/// EL FILTRO DE PERFIL VIVE ACÁ, NO EN EL CLIENTE
/// ----------------------------------------------
/// El frontend es Blazor WASM: corre en el navegador del usuario y se puede editar. Ocultar
/// el clic derecho allá sirve para que no moleste a quien no lo necesita, pero NO es una
/// barrera. Como este endpoint expone nombres de stored procedures y estructura interna, la
/// autorización real tiene que estar del lado del servidor, que es lo que hace esta clase.
///
/// POR QUÉ SE CONSULTA EL PERFIL Y NO SE LEE EL TOKEN
/// --------------------------------------------------
/// El JWT que emite AuthController trae los claims flgSeguridad y flgAdministrador, pero NO
/// flgAuditoria ni flgDeveloper, que son dos de los tres perfiles habilitados. Se podrían
/// agregar al login, pero eso obligaría a que todos vuelvan a iniciar sesión para que los
/// tokens ya emitidos los tengan. Consultando el perfil (que sí viaja en el claim "perfil")
/// funciona con los tokens actuales y desde el primer día. Es una consulta más por clic
/// derecho, sobre una acción de diagnóstico manual y poco frecuente.
/// </summary>
[Authorize]
[Route("api/v1/diagnostico")]
public class DiagnosticoController : BaseController
{
    private readonly IAccionTecnicaService _accion;
    private readonly IPerfilesUsuarioService _perfiles;

    public DiagnosticoController(IAccionTecnicaService accion, IPerfilesUsuarioService perfiles)
    {
        _accion = accion;
        _perfiles = perfiles;
    }

    /// <summary>
    /// GET api/v1/diagnostico/accion?formulario=Logistica/Inventarios.razor&amp;verbo=PUT
    /// </summary>
    [HttpGet("accion")]
    public async Task<IActionResult> Accion([FromQuery] string? formulario,
                                            [FromQuery] string? verbo,
                                            [FromQuery] string? boton)
    {
        if (!await EsPerfilTecnicoAsync())
            return Forbid();

        return Ok(ApiResponse<object>.Ok(_accion.Resolver(formulario, verbo, boton)));
    }

    /// <summary>
    /// Habilitado para los perfiles de Seguridad, Auditoría y Developer.
    /// Se pregunta por los FLAGS del perfil y no por códigos fijos ("SGRD", "AUDTR",
    /// "DVLPR"): así sigue funcionando si mañana crean otro perfil de auditoría con otro
    /// código, que es exactamente para lo que están esos flags.
    /// Ante cualquier falla devuelve false — si no se puede comprobar el perfil, no se
    /// expone la información.
    /// </summary>
    private async Task<bool> EsPerfilTecnicoAsync()
    {
        try
        {
            var codPerfil = User.FindFirst("perfil")?.Value;
            if (string.IsNullOrWhiteSpace(codPerfil)) return false;

            var r = await _perfiles.GetByIdAsync(codPerfil);
            var p = r?.Data;
            if (p is null) return false;

            return (p.FLG_SEGURIDAD ?? false)
                || (p.FLG_AUDITORIA ?? false)
                || (p.FLG_DEVELOPER ?? false);
        }
        catch
        {
            return false;
        }
    }
}
