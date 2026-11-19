using System.Linq;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using KonSolutionsApi.Common;

namespace KonSolutionsApi.API.Middleware;

/// <summary>Último recurso: lo que llega acá es una excepción que NADIE atrapó.
///
/// Que un endpoint termine en este middleware ya es información: los servicios envuelven
/// todo en try/catch y devuelven Fail(ex.Message), que sale como 400 CON el motivo. Un 500
/// con el texto genérico significa que reventó fuera de ese try — en el binding de
/// parámetros, en un filtro, o al serializar la respuesta.
///
/// EL MENSAJE GENÉRICO ES A PROPÓSITO: un 500 no debe contarle al mundo la estructura
/// interna. Pero cuando hay que diagnosticar, tener que ir a buscar el archivo de log
/// cuesta una vuelta entera. Por eso existe el interruptor de abajo.</summary>
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ErrorLogService _log;
    private readonly IConfiguration _config;

    public GlobalExceptionMiddleware(RequestDelegate next, ErrorLogService log, IConfiguration config)
    { _next = next; _log = log; _config = config; }

    public async Task InvokeAsync(HttpContext context)
    {
        try { await _next(context); }
        catch (Exception ex)
        {
            _log.LogError(ex, "KonSolutionsApi", nameof(GlobalExceptionMiddleware), "InvokeAsync", $"Path={context.Request.Path}");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            // ErrorLog:DetalleEnRespuesta — APAGADO por defecto. Encendido, el 500 viaja con
            // el tipo de excepción, el mensaje y las primeras líneas del stack, así que el
            // motivo real se lee en la pantalla y en la consola del navegador.
            //
            // Es para diagnosticar. En producción va apagado: el detalle de una excepción
            // dice más de lo que conviene contar.
            var conDetalle = _config.GetValue<bool>("ErrorLog:DetalleEnRespuesta");

            var mensaje = conDetalle
                ? $"{ex.GetType().Name}: {ex.Message}"
                : "Error interno del servidor. Revise el log.";

            var cuerpo = conDetalle
                ? (object)new
                {
                    success = false,
                    message = mensaje,
                    data    = (object?)null,
                    total   = 0,
                    // Sólo las primeras líneas: el stack completo no entra en una celda de
                    // la grilla y lo que importa está arriba.
                    detalle = new
                    {
                        tipo       = ex.GetType().FullName,
                        mensaje    = ex.Message,
                        interna    = ex.InnerException?.Message,
                        path       = context.Request.Path.Value,
                        query      = context.Request.QueryString.Value,
                        stack      = PrimerasLineas(ex.StackTrace, 8)
                    }
                }
                : ApiResponse<object>.Fail(mensaje);

            await context.Response.WriteAsync(JsonSerializer.Serialize(cuerpo,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }
    }

    private static string PrimerasLineas(string? stack, int cuantas)
    {
        if (string.IsNullOrWhiteSpace(stack)) return "";
        var lineas = stack.Split('\n');
        return string.Join("\n", lineas.Take(cuantas)).Trim();
    }
}
