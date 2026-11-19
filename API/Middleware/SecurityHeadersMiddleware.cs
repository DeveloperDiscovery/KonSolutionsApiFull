namespace KonSolutionsApi.API.Middleware;

/// <summary>
/// Agrega headers de seguridad HTTP a todas las respuestas.
/// Previene clickjacking, sniffing de contenido, XSS, y limita información del servidor.
/// </summary>
public class SecurityHeadersMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var h = context.Response.Headers;
        h.XContentTypeOptions = "nosniff";
        h.XFrameOptions = "DENY";
        // X-XSS-Protection eliminado: obsoleto en navegadores modernos y puede crear falsa seguridad
        h["Content-Security-Policy"]   = "default-src 'self'; frame-ancestors 'none'; object-src 'none';";
        h["Strict-Transport-Security"] = "max-age=63072000; includeSubDomains; preload";
        h["Referrer-Policy"]           = "strict-origin-when-cross-origin";
        h["X-Permitted-Cross-Domain-Policies"] = "none";
        h["Permissions-Policy"]        = "geolocation=(), microphone=(), camera=()";
        // Eliminar headers que exponen la tecnología del servidor
        context.Response.Headers.Remove("Server");
        context.Response.Headers.Remove("X-Powered-By");
        await _next(context);
    }
}
