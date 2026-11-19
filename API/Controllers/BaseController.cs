using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace KonSolutionsApi.API.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    /// <summary>
    /// Código del usuario logueado, tomado del token JWT.
    /// Busca primero el claim corto "nameid" (como queda escrito en el JWT) y luego
    /// el largo ClaimTypes.NameIdentifier, porque según el remapeo de .NET puede
    /// aparecer con cualquiera de los dos nombres. Esto corrige que la auditoría
    /// grababa "SISTEMA" en vez del usuario real.
    /// </summary>
    protected string GetCodUsuario() =>
        User.FindFirst("nameid")?.Value
        ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value
        ?? "SISTEMA";

    /// <summary>
    /// Código del rol activo del usuario logueado, tomado del claim "rol" del token JWT.
    /// Es el rol con el que se inició sesión — si el usuario tiene varios roles y elige
    /// otro en el selector de la barra superior, el frontend debe mandar el rol elegido
    /// explícitamente en cada request que lo necesite (no se actualiza el JWT en caliente).
    /// </summary>
    protected string? GetCodRol() => User.FindFirst("rol")?.Value;

    /// <summary>
    /// Estación (IP) del cliente. Si la conexión es local (IPv6 ::1) la normaliza.
    /// </summary>
    protected string GetEstacion()
    {
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "LOCALHOST";
        return ip == "::1" ? "LOCALHOST" : ip;
    }
}
