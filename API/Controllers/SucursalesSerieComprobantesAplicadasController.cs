using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/sucursales-serie-comprobantes-aplicadas")]
[ExigePermiso("ESTANDAR.SUCURSALES_SERIE_COMPROBANTES_APLICADAS")]
public class SucursalesSerieComprobantesAplicadasController : BaseController
{
    private readonly ISucursalesSerieComprobantesAplicadasService _service;
    public SucursalesSerieComprobantesAplicadasController(ISucursalesSerieComprobantesAplicadasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_SUCURSAL}/{COD_TIPO_COMPROBANTE_PAGO}/{COD_TIPO_COMPROBANTE_PAGO_APLICADO}")]
    public async Task<IActionResult> GetById(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO, string COD_TIPO_COMPROBANTE_PAGO_APLICADO)
    { var r = await _service.GetByIdAsync(COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO, COD_TIPO_COMPROBANTE_PAGO_APLICADO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SucursalesSerieComprobantesAplicadasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] SucursalesSerieComprobantesAplicadasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] SucursalesSerieComprobantesAplicadasUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_SUCURSAL}/{COD_TIPO_COMPROBANTE_PAGO}/{COD_TIPO_COMPROBANTE_PAGO_APLICADO}")]
    public async Task<IActionResult> Delete(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO, string COD_TIPO_COMPROBANTE_PAGO_APLICADO)
    { var r = await _service.DeleteAsync(COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO, COD_TIPO_COMPROBANTE_PAGO_APLICADO); return r.Success ? Ok(r) : BadRequest(r); }

}