using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Vínculos pedido↔requerimiento ya grabados, para la subgrilla de trazabilidad del
/// detalle del pedido. Solo lectura: el alta/baja del vínculo la maneja el flujo de
/// incorporar/desvincular requerimientos, no esta pantalla.</summary>
[Authorize]
[Route("api/v1/comercial/pedidos-comerciales-requerimientos-stock-link")]
[ExigePermiso("COMERCIAL.PEDIDOS_COMERCIALES")]
public class PedidosComercialesRequerimientosStockLinkController : BaseController
{
    private readonly IPedidosComercialesRequerimientosStockLinkService _service;
    public PedidosComercialesRequerimientosStockLinkController(IPedidosComercialesRequerimientosStockLinkService service) => _service = service;

    /// <summary>Requerimientos vinculados a la CABECERA del pedido (uno o varios).</summary>
    [HttpGet("cabecera")]
    public async Task<IActionResult> GetCabecera([FromQuery] string codClaseOperacion, [FromQuery] string codTipoOperacion, [FromQuery] decimal numPedidoComercial)
    { var r = await _service.GetCabeceraAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>El detalle del vínculo de UNA línea del pedido: de qué requerimientos salió
    /// y con cuánta cantidad.
    ///
    /// numRequerimientoStock es OPCIONAL desde el script 61. Sin él vienen todos los de la
    /// línea en una sola consulta; con él, sólo ese. La subgrilla lo omite: antes tenía que
    /// averiguar la lista consultando la tabla de cabecera, que nadie escribía, y por eso
    /// mostraba siempre "esta línea no viene de ningún requerimiento".</summary>
    [HttpGet("detalle")]
    public async Task<IActionResult> GetDetalle([FromQuery] string codClaseOperacion, [FromQuery] string codTipoOperacion,
        [FromQuery] decimal numPedidoComercial, [FromQuery] int numSecuencia, [FromQuery] int? numRequerimientoStock = null)
    { var r = await _service.GetDetalleAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial, numSecuencia, numRequerimientoStock); return r.Success ? Ok(r) : BadRequest(r); }
}
