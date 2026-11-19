using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Historial de estados de un pedido comercial: una fila por cada cambio de
/// estado, con quién y cuándo lo hizo.</summary>
[Authorize]
[Route("api/v1/comercial/pedidos-comerciales-estados")]
[ExigePermiso("COMERCIAL.PEDIDOS_COMERCIALES")]
public class PedidosComercialesEstadosController : BaseController
{
    private readonly IPedidosComercialesEstadosService _service;
    public PedidosComercialesEstadosController(IPedidosComercialesEstadosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codClaseOperacion, [FromQuery] string codTipoOperacion, [FromQuery] int numPedidoComercial)
    { var r = await _service.GetAllAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial); return r.Success ? Ok(r) : BadRequest(r); }
}
