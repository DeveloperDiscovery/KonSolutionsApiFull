using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/tipos-pedido-comercial")]
[ExigePermiso("COMERCIAL.TIPOS_PEDIDO_COMERCIAL")]
public class TiposPedidoComercialController : BaseController
{
    private readonly ITiposPedidoComercialService _service;
    public TiposPedidoComercialController(ITiposPedidoComercialService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TIPO_PEDIDO_COMERCIAL}")]
    public async Task<IActionResult> GetById(int COD_TIPO_PEDIDO_COMERCIAL)
    { var r = await _service.GetByIdAsync(COD_TIPO_PEDIDO_COMERCIAL); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TiposPedidoComercialSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_COMERCIAL_TIPOS_PEDIDO_COMERCIAL_LOADCOMBOS: tipos de entidad + tipos de
    /// comprobante de pago para el diálogo de Tipos de Pedido Comercial en un solo viaje.</summary>
    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos()
    { var r = await _service.LoadcombosAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposPedidoComercialInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TiposPedidoComercialUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_TIPO_PEDIDO_COMERCIAL}")]
    public async Task<IActionResult> Delete(int COD_TIPO_PEDIDO_COMERCIAL)
    { var r = await _service.DeleteAsync(COD_TIPO_PEDIDO_COMERCIAL); return r.Success ? Ok(r) : BadRequest(r); }

}