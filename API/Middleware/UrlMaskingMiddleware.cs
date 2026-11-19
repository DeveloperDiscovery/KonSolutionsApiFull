namespace KonSolutionsApi.API.Middleware;

/// <summary>
/// Enmascaramiento de URLs: traduce rutas públicas cortas a las rutas internas reales.
/// DEBE estar primero en el pipeline, antes de UseRouting/MapControllers.
///
/// Mapeo público → interno:
///   /api/v1/s/  →  /api/v1/seguridad/
///   /api/v1/e/  →  /api/v1/estandar/
///   /api/v1/c/  →  /api/v1/costos/
///   /api/v1/l/  →  /api/v1/logistica/
///   /api/v1/m/  →  /api/v1/comercial/
///   /api/v1/a/  →  /api/v1/activos/
///   /api/v1/r/  →  /api/v1/rrhh/
///   /api/v1/n/  →  /api/v1/entorno/
///   /api/v1/d/  →  /api/v1/developer/
/// </summary>
public class UrlMaskingMiddleware
{
    private readonly RequestDelegate _next;

    // Orden importa: rutas más específicas primero
    private static readonly (string Masked, string Real)[] _mappings =
    {
        ("/api/v1/s/",  "/api/v1/seguridad/"),
        ("/api/v1/e/",  "/api/v1/estandar/"),
        ("/api/v1/c/",  "/api/v1/costos/"),
        ("/api/v1/l/",  "/api/v1/logistica/"),
        ("/api/v1/m/",  "/api/v1/comercial/"),
        ("/api/v1/a/",  "/api/v1/activos/"),
        ("/api/v1/r/",  "/api/v1/rrhh/"),
        ("/api/v1/n/",  "/api/v1/entorno/"),
        ("/api/v1/d/",  "/api/v1/developer/"),
    };

    public UrlMaskingMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var originalPath = context.Request.Path.Value ?? "";

        foreach (var (masked, real) in _mappings)
        {
            if (originalPath.StartsWith(masked, StringComparison.OrdinalIgnoreCase))
            {
                var newPath = real + originalPath[masked.Length..];
                context.Request.Path = new PathString(newPath);
                break;
            }
        }

        await _next(context);
    }
}
