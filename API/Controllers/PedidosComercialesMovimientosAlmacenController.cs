using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/pedidos-comerciales-movimientos-almacen")]
[ExigePermiso("COMERCIAL.PEDIDOS_COMERCIALES_MOVIMIENTOS_ALMACEN")]
public class PedidosComercialesMovimientosAlmacenController : BaseController
{
    private readonly IPedidosComercialesMovimientosAlmacenService _service;
    public PedidosComercialesMovimientosAlmacenController(IPedidosComercialesMovimientosAlmacenService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_PEDIDO_COMERCIAL}/{NUM_SECUENCIA_PEDIDO}/{NUM_MOVIMIENTO_ALMACEN}")]
    public async Task<IActionResult> GetById(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA_PEDIDO, decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.GetByIdAsync(NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA_PEDIDO, NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] PedidosComercialesMovimientosAlmacenSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] PedidosComercialesMovimientosAlmacenInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{NUM_PEDIDO_COMERCIAL}/{NUM_SECUENCIA_PEDIDO}/{NUM_MOVIMIENTO_ALMACEN}")]
    public async Task<IActionResult> Delete(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA_PEDIDO, decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.DeleteAsync(NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA_PEDIDO, NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

}