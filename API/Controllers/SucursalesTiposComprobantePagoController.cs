using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/sucursales-tipos-comprobante-pago")]
[ExigePermiso("ESTANDAR.SUCURSALES_TIPOS_COMPROBANTE_PAGO")]
public class SucursalesTiposComprobantePagoController : BaseController
{
    private readonly ISucursalesTiposComprobantePagoService _service;
    public SucursalesTiposComprobantePagoController(ISucursalesTiposComprobantePagoService service) => _service = service;

    // Tabla hija: los SP SHOWALL/SHOWCOMBOX reciben @COD_SUCURSAL obligatorio,
    // así que no existe un "listar todo" — siempre se lista dentro de una sucursal.
    [HttpGet("{COD_SUCURSAL}")]
    public async Task<IActionResult> GetAll(string COD_SUCURSAL)
    { var r = await _service.GetAllAsync(COD_SUCURSAL); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_SUCURSAL}/{COD_TIPO_COMPROBANTE_PAGO}")]
    public async Task<IActionResult> GetById(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO)
    { var r = await _service.GetByIdAsync(COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox/{COD_SUCURSAL}")]
    public async Task<IActionResult> GetCombobox(string COD_SUCURSAL)
    { var r = await _service.GetComboboxAsync(COD_SUCURSAL); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SucursalesTiposComprobantePagoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] SucursalesTiposComprobantePagoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_SUCURSAL}/{COD_TIPO_COMPROBANTE_PAGO}")]
    public async Task<IActionResult> Delete(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO)
    { var r = await _service.DeleteAsync(COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO); return r.Success ? Ok(r) : BadRequest(r); }

}