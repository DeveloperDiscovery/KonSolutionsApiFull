using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Comparativo de compras: qué pedidos atienden los mismos requerimientos y
/// cómo se comparan sus precios y fechas.</summary>
[Authorize]
[Route("api/v1/comercial/pedidos-comerciales-comparativo")]
[ExigePermiso("COMERCIAL.PEDIDOS_COMERCIALES")]
public class PedidosComercialesComparativoController : BaseController
{
    private readonly IPedidosComercialesComparativoService _service;
    public PedidosComercialesComparativoController(IPedidosComercialesComparativoService service) => _service = service;

    /// <summary>Los requerimientos elegibles: sólo los que ya tienen pedidos.</summary>
    [HttpGet("requerimientos")]
    public async Task<IActionResult> GetRequerimientos()
    { var r = await _service.GetRequerimientosConPedidosAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>El comparativo. Los requerimientos van separados por guion bajo: 1_2_3.
    /// codClaseOperacion es el mismo que fija la opción de menú (Proformas/Órdenes de
    /// Compra/etc.) — evita comparar pedidos de una clase de operación contra otra.
    /// codMoneda es la moneda a la que se convierten los importes para poder comparar
    /// pedidos que están en monedas distintas.</summary>
    [HttpGet]
    public async Task<IActionResult> GetComparativo([FromQuery] string codClaseOperacion, [FromQuery] string codMoneda, [FromQuery] string requerimientos)
    { var r = await _service.GetComparativoAsync(codClaseOperacion, codMoneda, requerimientos); return r.Success ? Ok(r) : BadRequest(r); }
}
