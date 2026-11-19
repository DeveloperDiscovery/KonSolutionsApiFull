using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Endpoint que expone el treeview de opciones de aplicaciones.</summary>
[Authorize]
[Route("api/v1/seguridad/aplicaciones-opciones-treeview")]
[ExigePermiso("SEGURIDAD.APLICACIONES_OPCINOES_TREVIEW")]
public class AplicacionesOpcionesTreeviewController : BaseController
{
    private readonly IAplicacionesOpcionesTreeviewService _service;
    public AplicacionesOpcionesTreeviewController(IAplicacionesOpcionesTreeviewService service)
        => _service = service;

    /// <summary>Retorna todos los nodos planos para armar el treeview en el cliente.</summary>
    [HttpGet]
    public async Task<IActionResult> GetTreeview()
    {
        var r = await _service.GetTreeviewAsync();
        return r.Success ? Ok(r) : BadRequest(r);
    }

    // Experimento: íconos de TODO el árbol en un endpoint aparte del texto principal —
    // se pide en paralelo, sin bloquear el primer render del treeview.
    [HttpGet("logos")]
    public async Task<IActionResult> GetLogos()
    {
        var r = await _service.GetLogosAsync();
        return r.Success ? Ok(r) : BadRequest(r);
    }
}
