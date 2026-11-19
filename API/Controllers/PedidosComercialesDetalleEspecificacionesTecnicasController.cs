using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/ingenieria/pedidos-comerciales-detalle-especificaciones-tecnicas")]
[ExigePermiso("INGENIERIA.PEDIDOS_COMERCIALES_DETALLE_ESPECIFICACIONES_TECNICAS")]
public class PedidosComercialesDetalleEspecificacionesTecnicasController : BaseController
{
    private readonly IPedidosComercialesDetalleEspecificacionesTecnicasService _service;
    public PedidosComercialesDetalleEspecificacionesTecnicasController(IPedidosComercialesDetalleEspecificacionesTecnicasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_PEDIDO_COMERCIAL}/{NUM_SECUENCIA}/{COD_ESPECIFICACION_TECNICA}")]
    public async Task<IActionResult> GetById(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA)
    { var r = await _service.GetByIdAsync(NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA, COD_ESPECIFICACION_TECNICA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] PedidosComercialesDetalleEspecificacionesTecnicasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] PedidosComercialesDetalleEspecificacionesTecnicasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] PedidosComercialesDetalleEspecificacionesTecnicasUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_PEDIDO_COMERCIAL}/{NUM_SECUENCIA}/{COD_ESPECIFICACION_TECNICA}")]
    public async Task<IActionResult> Delete(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA)
    { var r = await _service.DeleteAsync(NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA, COD_ESPECIFICACION_TECNICA); return r.Success ? Ok(r) : BadRequest(r); }

}