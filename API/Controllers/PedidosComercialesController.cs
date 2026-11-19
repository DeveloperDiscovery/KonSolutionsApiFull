using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Fase 1: solo cabecera de COMERCIAL.PEDIDOS_COMERCIALES. codClaseOperacion/
/// codTipoOperacion son obligatorios en GetAll/Search — la pantalla reutilizable (Proformas,
/// Órdenes de Compra, Pedidos de Venta) siempre los resuelve desde el Parámetro/selección
/// antes de listar, igual que Entidades con codTipoEntidad.</summary>
[Authorize]
[Route("api/v1/comercial/pedidos-comerciales")]
[ExigePermiso("COMERCIAL.PEDIDOS_COMERCIALES")]
public class PedidosComercialesController : BaseController
{
    private readonly IPedidosComercialesService _service;
    public PedidosComercialesController(IPedidosComercialesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codClaseOperacion, [FromQuery] string codTipoOperacion)
    { var r = await _service.GetAllAsync(codClaseOperacion, codTipoOperacion); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Porcentaje de atención (lo atendido sobre lo pedido) de cada pedido de la clase de operación; la grilla de Órdenes de Compra lo muestra como barra.
    /// codTipoOperacion es opcional.</summary>
    [HttpGet("atencion")]
    public async Task<IActionResult> Atencion([FromQuery] string codClaseOperacion, [FromQuery] string? codTipoOperacion)
    { var r = await _service.AtencionAsync(codClaseOperacion, codTipoOperacion); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] PedidosComercialesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{codClaseOperacion}/{codTipoOperacion}/{numPedidoComercial}")]
    public async Task<IActionResult> GetById(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    { var r = await _service.GetByIdAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial); return r.Success ? Ok(r) : NotFound(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] PedidosComercialesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] PedidosComercialesUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{codClaseOperacion}/{codTipoOperacion}/{numPedidoComercial}")]
    public async Task<IActionResult> Delete(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    { var r = await _service.DeleteAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_COMERCIAL_PEDIDOS_COMERCIALES_LOADCOMBOS: tipos de operación, tipos de
    /// documento por operación, países, incoterms, monedas, formas y condiciones de pago,
    /// tipos de descuento (con su tasa) y proyectos, en un solo viaje.</summary>
    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos([FromQuery] string codClaseOperacion)
    { var r = await _service.LoadcombosAsync(codClaseOperacion); return r.Success ? Ok(r) : BadRequest(r); }
}
