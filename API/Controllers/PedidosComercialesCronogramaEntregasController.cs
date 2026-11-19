using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

/// <summary>
/// Cronograma de entregas de un pedido comercial.
///
/// El permiso es el del pedido y no uno propio: la pestaña vive dentro de Órdenes de Compra
/// y Proformas, así que quien puede ver el documento puede ver su cronograma. Si más adelante
/// se le da opción de menú propia, hay que darle su propio ExigePermiso.
/// </summary>
[Authorize]
[Route("api/v1/logistica/pedidos-comerciales-cronograma-entregas")]
[ExigePermiso("COMERCIAL.PEDIDOS_COMERCIALES")]
public class PedidosComercialesCronogramaEntregasController : BaseController
{
    private readonly IPedidosComercialesCronogramaEntregasService _service;
    public PedidosComercialesCronogramaEntregasController(IPedidosComercialesCronogramaEntregasService service)
        => _service = service;

    /// <summary>Cronograma completo del pedido, de todas sus líneas.</summary>
    [HttpGet("por-pedido")]
    public async Task<IActionResult> PorPedido([FromQuery] string codTipoOperacion,
                                               [FromQuery] string codClaseOperacion,
                                               [FromQuery] decimal numPedidoComercial)
    {
        var r = await _service.GetPorPedidoAsync(codTipoOperacion, codClaseOperacion, numPedidoComercial);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] PedidoComercialCronogramaEntregaDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] PedidoComercialCronogramaEntregaDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>La clave son cinco campos, así que van por query y no en la ruta: una fecha
    /// dentro de un segmento de URL es fuente segura de problemas de formato.</summary>
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string codTipoOperacion,
                                            [FromQuery] string codClaseOperacion,
                                            [FromQuery] decimal numPedidoComercial,
                                            [FromQuery] int numSecuencia,
                                            [FromQuery] DateTime fecEntrega)
    {
        var r = await _service.DeleteAsync(codTipoOperacion, codClaseOperacion, numPedidoComercial, numSecuencia, fecEntrega);
        return r.Success ? Ok(r) : BadRequest(r);
    }
}
