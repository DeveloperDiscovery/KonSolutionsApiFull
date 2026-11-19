using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Requerimientos de stock dentro del pedido comercial: qué se puede elegir, y
/// los dos niveles de vínculo una vez elegido.</summary>
[Authorize]
[Route("api/v1/comercial/pedidos-comerciales-requerimientos")]
[ExigePermiso("COMERCIAL.PEDIDOS_COMERCIALES")]
public class PedidosComercialesRequerimientosController : BaseController
{
    private readonly IPedidosComercialesRequerimientosService _service;
    public PedidosComercialesRequerimientosController(IPedidosComercialesRequerimientosService service) => _service = service;

    /// <summary>Requerimientos de compra registrados. El número de pedido no filtra: sirve
    /// para que el SP marque cuáles ya están vinculados.</summary>
    [HttpGet("registrados")]
    public async Task<IActionResult> GetRegistrados([FromQuery] decimal numPedidoComercial)
    { var r = await _service.GetRegistradosAsync(numPedidoComercial); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("registrados-detalle")]
    public async Task<IActionResult> GetRegistradosDetalle([FromQuery] string codClaseOperacion, [FromQuery] string codTipoOperacion, [FromQuery] decimal numPedidoComercial)
    { var r = await _service.GetRegistradosDetalleAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial); return r.Success ? Ok(r) : BadRequest(r); }

    // ── Vínculo de cabecera ──────────────────────────────────────────────────

    [HttpGet("cabecera")]
    public async Task<IActionResult> GetCabeceras([FromQuery] string codClaseOperacion, [FromQuery] string codTipoOperacion, [FromQuery] decimal numPedidoComercial)
    { var r = await _service.GetCabecerasAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("cabecera")]
    public async Task<IActionResult> InsertCabecera([FromBody] PedidoComercialRequerimientoStockDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertCabeceraAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("cabecera/{COD_CLASE_OPERACION}/{COD_TIPO_OPERACION}/{NUM_PEDIDO_COMERCIAL}/{NUM_REQUERIMIENTO_STOCK}")]
    public async Task<IActionResult> DeleteCabecera(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, decimal NUM_PEDIDO_COMERCIAL, int NUM_REQUERIMIENTO_STOCK)
    { var r = await _service.DeleteCabeceraAsync(COD_CLASE_OPERACION, COD_TIPO_OPERACION, NUM_PEDIDO_COMERCIAL, NUM_REQUERIMIENTO_STOCK);
      return r.Success ? Ok(r) : BadRequest(r); }

    // ── Vínculo de detalle ───────────────────────────────────────────────────

    [HttpGet("detalle")]
    public async Task<IActionResult> GetDetalles([FromQuery] string codClaseOperacion, [FromQuery] string codTipoOperacion, [FromQuery] decimal numPedidoComercial, [FromQuery] int numSecuencia, [FromQuery] int numRequerimientoStock)
    { var r = await _service.GetDetallesAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial, numSecuencia, numRequerimientoStock);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("detalle")]
    public async Task<IActionResult> InsertDetalle([FromBody] PedidoComercialDetalleRequerimientoStockDetalleDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertDetalleAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("detalle/{COD_CLASE_OPERACION}/{COD_TIPO_OPERACION}/{NUM_PEDIDO_COMERCIAL}/{NUM_SECUENCIA}/{NUM_REQUERIMIENTO_STOCK}/{NUM_SECUENCIA_REQUERIMIENTO_STOCK}")]
    public async Task<IActionResult> DeleteDetalle(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, decimal NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA, int NUM_REQUERIMIENTO_STOCK, int NUM_SECUENCIA_REQUERIMIENTO_STOCK)
    { var r = await _service.DeleteDetalleAsync(COD_CLASE_OPERACION, COD_TIPO_OPERACION, NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA, NUM_REQUERIMIENTO_STOCK, NUM_SECUENCIA_REQUERIMIENTO_STOCK);
      return r.Success ? Ok(r) : BadRequest(r); }
}
