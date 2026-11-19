using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/pedidos-especificaciones-despacho-")]
[ExigePermiso("COMERCIAL.PEDIDOS_ESPECIFICACIONES_DESPACHO_")]
public class PedidosEspecificacionesDespachoController : BaseController
{
    private readonly IPedidosEspecificacionesDespachoService _service;
    public PedidosEspecificacionesDespachoController(IPedidosEspecificacionesDespachoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_PEDIDO_COMERCIAL}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> GetById(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA)
    { var r = await _service.GetByIdAsync(NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] PedidosEspecificacionesDespachoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] PedidosEspecificacionesDespachoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{NUM_PEDIDO_COMERCIAL}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> Delete(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA)
    { var r = await _service.DeleteAsync(NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA); return r.Success ? Ok(r) : BadRequest(r); }

}