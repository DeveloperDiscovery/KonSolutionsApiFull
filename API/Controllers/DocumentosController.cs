using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace KonSolutionsApi.API.Controllers;

/// <summary>
/// Proxy de documentos (PDFs de comprobantes, etc.) guardados en rutas UNC del servidor
/// de archivos. El navegador no puede cargar rutas UNC en un iframe, y el servidor de
/// archivos no tiene HTTPS — en vez de eso, la API (que sí corre en HTTPS) lee el archivo
/// del recurso de red y lo devuelve al cliente, evitando el bloqueo de "Mixed Content".
/// </summary>
[Authorize]
[SinControlPermisos("Devuelve un archivo ya subido, a partir de su ruta. No administra ninguna tabla. "
                    + "ATENCIÓN: no distingue de qué módulo es el documento, así que un usuario "
                    + "autenticado puede pedir cualquiera cuya ruta conozca. Acotarlo exige validar "
                    + "el permiso del módulo dueño del archivo, que es un cambio de diseño aparte.")]
[Route("api/v1/entorno/documentos")]
public class DocumentosController : ControllerBase
{
    private static readonly FileExtensionContentTypeProvider _tipos = new();

    [HttpGet("ver")]
    public IActionResult Ver([FromQuery] string ruta)
    {
        if (string.IsNullOrWhiteSpace(ruta) || !ruta.StartsWith(@"\\"))
            return BadRequest("Ruta inválida.");

        if (!_tipos.TryGetContentType(ruta, out var contentType))
            contentType = "application/octet-stream";

        // File.Exists() en rutas UNC puede dar falso negativo por timing/latencia de red —
        // en vez de precondicionar con Exists, se intenta leer directo. Se captura CUALQUIER
        // excepción (no solo FileNotFound/DirectoryNotFound) y se devuelve el mensaje real:
        // en rutas de red, .NET suele lanzar IOException ("no se pudo encontrar la red") o
        // UnauthorizedAccessException en vez de FileNotFoundException, y esas quedaban sin
        // capturar antes — el cliente necesita ver el mensaje real para diagnosticar esto,
        // en vez de un 404 genérico que oculta la causa.
        try
        {
            var bytes = System.IO.File.ReadAllBytes(ruta);
            return File(bytes, contentType);
        }
        catch (Exception ex)
        {
            return NotFound($"No se pudo leer el archivo: [{ex.GetType().Name}] {ex.Message}");
        }
    }
}
