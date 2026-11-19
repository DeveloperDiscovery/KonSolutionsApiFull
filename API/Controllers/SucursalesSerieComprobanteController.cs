using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/sucursales-serie-comprobante")]
[ExigePermiso("ESTANDAR.SUCURSALES_SERIE_COMPROBANTE")]
public class SucursalesSerieComprobanteController : BaseController
{
    private readonly ISucursalesSerieComprobanteService _service;
    public SucursalesSerieComprobanteController(ISucursalesSerieComprobanteService service) => _service = service;

    /// <summary>Las series de un tipo de comprobante en una sucursal. Los dos códigos van en
    /// la ruta porque el SP los exige: una serie no existe fuera de su par.</summary>
    [HttpGet("{COD_SUCURSAL}/{COD_TIPO_COMPROBANTE_PAGO}")]
    public async Task<IActionResult> GetAll(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO)
    { var r = await _service.GetAllAsync(COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_SUCURSAL}/{COD_TIPO_COMPROBANTE_PAGO}/{NUM_SERIE_COMPROBANTE}")]
    public async Task<IActionResult> GetById(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO, int NUM_SERIE_COMPROBANTE)
    { var r = await _service.GetByIdAsync(COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO, NUM_SERIE_COMPROBANTE); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox/{COD_SUCURSAL}/{COD_TIPO_COMPROBANTE_PAGO}")]
    public async Task<IActionResult> GetCombobox(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO)
    { var r = await _service.GetComboboxAsync(COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string codSucursal, [FromQuery] string codTipoComprobantePago, [FromQuery] SucursalesSerieComprobanteSearchDto dto)
    { var r = await _service.SearchAsync(codSucursal, codTipoComprobantePago, dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] SucursalesSerieComprobanteInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] SucursalesSerieComprobanteUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_SUCURSAL}/{COD_TIPO_COMPROBANTE_PAGO}/{NUM_SERIE_COMPROBANTE}")]
    public async Task<IActionResult> Delete(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO, int NUM_SERIE_COMPROBANTE)
    { var r = await _service.DeleteAsync(COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO, NUM_SERIE_COMPROBANTE); return r.Success ? Ok(r) : BadRequest(r); }

}