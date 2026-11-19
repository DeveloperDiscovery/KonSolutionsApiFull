using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Líneas de COMERCIAL.PEDIDOS_COMERCIALES_DETALLE. El detalle se lee siempre dentro
/// de su cabecera — igual criterio que RequerimientosStockDetalleController.</summary>
[Authorize]
[Route("api/v1/comercial/pedidos-comerciales-detalle")]
[ExigePermiso("COMERCIAL.PEDIDOS_COMERCIALES_DETALLE")]
public class PedidosComercialesDetalleController : BaseController
{
    private readonly IPedidosComercialesDetalleService _service;
    public PedidosComercialesDetalleController(IPedidosComercialesDetalleService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codClaseOperacion, [FromQuery] string codTipoOperacion, [FromQuery] decimal numPedidoComercial)
    { var r = await _service.GetAllAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("por-cabecera/{codClaseOperacion}/{codTipoOperacion}/{numPedidoComercial}")]
    public async Task<IActionResult> GetPorCabecera(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    { var r = await _service.GetAllAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{numPedidoComercial}/{numSecuencia}")]
    public async Task<IActionResult> GetById(decimal numPedidoComercial, int numSecuencia)
    { var r = await _service.GetByIdAsync(numPedidoComercial, numSecuencia); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] PedidosComercialesDetalleSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] PedidosComercialesDetalleInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      dto.COD_USUARIO_REGISTRO = GetCodUsuario();
      dto.COD_ESTACION_REGISTRO = GetEstacion();
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] PedidosComercialesDetalleUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      dto.COD_USUARIO_REGISTRO = GetCodUsuario();
      dto.COD_ESTACION_REGISTRO = GetEstacion();
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{codClaseOperacion}/{codTipoOperacion}/{numPedidoComercial}/{numSecuencia}")]
    public async Task<IActionResult> Delete(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, int numSecuencia)
    { var r = await _service.DeleteAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial, numSecuencia); return r.Success ? Ok(r) : BadRequest(r); }
}
