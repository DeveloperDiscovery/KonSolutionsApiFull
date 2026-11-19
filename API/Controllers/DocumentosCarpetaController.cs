using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;

namespace KonSolutionsApi.API.Controllers;

/// <summary>
/// Endpoint genérico de documentos por carpeta — reutilizable desde cualquier diálogo
/// (Solicitudes Comerciales, Entidades, etc.). codKeyPathfile es la clave de
/// ENTORNO.PATHFILES (PROC_ENTORNO_PATHFILES_SHOWBYID) que resuelve la ruta base;
/// identificador es la subcarpeta (N° de Solicitud, código de Entidad, etc.).
/// </summary>
[Authorize]
[ExigePermiso("ENTORNO.PATHFILES")]
[Route("api/v1/entorno/documentos-carpeta/{codKeyPathfile}/{identificador}")]
public class DocumentosCarpetaController : BaseController
{
    private readonly IDocumentosCarpetaService _service;
    public DocumentosCarpetaController(IDocumentosCarpetaService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> List(string codKeyPathfile, string identificador)
    {
        var r = await _service.ListAsync(codKeyPathfile, identificador);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpPost]
    [RequestSizeLimit(20_000_000)]
    public async Task<IActionResult> Upload(string codKeyPathfile, string identificador, IFormFile archivo)
    {
        var r = await _service.UploadAsync(codKeyPathfile, identificador, archivo);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpDelete("{nombreArchivo}")]
    public async Task<IActionResult> Delete(string codKeyPathfile, string identificador, string nombreArchivo)
    {
        var r = await _service.DeleteAsync(codKeyPathfile, identificador, nombreArchivo);
        return r.Success ? Ok(r) : BadRequest(r);
    }
}
