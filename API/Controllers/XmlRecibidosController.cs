using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.API.Filters;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Bandeja de XML recibidos: el servicio que baja de Microsoft 365 los XML y PDF de los comprobantes y las guías de remisión de los proveedores.
/// Corre solo cada tantos minutos (configuración general, pestaña Compras > XML recibidos); estos dos puntos sirven para ver cómo va y para correrlo ahora.</summary>
[Authorize]
[Route("api/v1/finanzas/xml-recibidos")]
[ExigePermiso("COMERCIAL.REGISTRO_COMPRAS")]
public class XmlRecibidosController : BaseController
{
    private readonly IXmlRecibidosService _service;
    public XmlRecibidosController(IXmlRecibidosService service) => _service = service;

    /// <summary>Si la cuenta de Microsoft 365 está configurada, si la carga automática está activa, si hay una corrida en curso y cómo salió la última.</summary>
    [HttpGet("estado")]
    public async Task<IActionResult> Estado()
    { return Ok(ApiResponse<XmlRecibidosEstadoDto>.Ok(await _service.EstadoAsync())); }

    /// <summary>Corre el servicio ahora, aunque la carga automática esté apagada. Si ya hay una corrida en curso, lo dice y no hace otra.</summary>
    [HttpPost("ejecutar")]
    public async Task<IActionResult> Ejecutar(CancellationToken ct)
    {
        var c = await _service.EjecutarAsync(true, ct);
        return c.Mensaje is null || c.Correos > 0 ? Ok(ApiResponse<XmlRecibidosCiclo>.Ok(c, c.Mensaje ?? "Listo.")) : BadRequest(ApiResponse<XmlRecibidosCiclo>.Fail(c.Mensaje));
    }
}
