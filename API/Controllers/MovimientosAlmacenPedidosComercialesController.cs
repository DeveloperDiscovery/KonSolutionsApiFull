using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/movimientos-almacen-pedidos-comerciales")]
[ExigePermiso("LOGISTICA.MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES")]
public class MovimientosAlmacenPedidosComercialesController : BaseController
{
    private readonly IMovimientosAlmacenPedidosComercialesService _service;
    public MovimientosAlmacenPedidosComercialesController(IMovimientosAlmacenPedidosComercialesService service) => _service = service;

    // El movimiento es obligatorio: el SP lo exige y no tiene valor por defecto.
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.GetAllAsync(NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_MOVIMIENTO_ALMACEN}/{NUM_PEDIDO_COMERCIAL}")]
    public async Task<IActionResult> GetById(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_PEDIDO_COMERCIAL)
    { var r = await _service.GetByIdAsync(NUM_MOVIMIENTO_ALMACEN, NUM_PEDIDO_COMERCIAL); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] MovimientosAlmacenPedidosComercialesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] MovimientosAlmacenPedidosComercialesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    // Sin PUT, y no es un olvido: en una tabla de enlace la fila ENTERA es la clave, asi que
    // no hay nada que modificar. Su SP de alta ademas hace upsert, con lo cual volver a
    // asignar algo ya asignado es inofensivo.

    [HttpDelete("{NUM_MOVIMIENTO_ALMACEN}/{NUM_PEDIDO_COMERCIAL}")]
    public async Task<IActionResult> Delete(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_PEDIDO_COMERCIAL)
    { var r = await _service.DeleteAsync(NUM_MOVIMIENTO_ALMACEN, NUM_PEDIDO_COMERCIAL); return r.Success ? Ok(r) : BadRequest(r); }
}
