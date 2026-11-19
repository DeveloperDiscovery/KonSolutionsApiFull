using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/tipos-operacion-tipos-comprobante-pago")]
[ExigePermiso("COMERCIAL.TIPOS_OPERACION_TIPOS_COMPROBANTE_PAGO")]
public class TiposOperacionTiposComprobantePagoController : BaseController
{
    private readonly ITiposOperacionTiposComprobantePagoService _service;
    public TiposOperacionTiposComprobantePagoController(ITiposOperacionTiposComprobantePagoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codClaseOperacion, [FromQuery] string codTipoOperacion)
    { var r = await _service.GetAllAsync(codClaseOperacion, codTipoOperacion); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CLASE_OPERACION}/{COD_TIPO_OPERACION}/{COD_TIPO_COMPROBANTE_PAGO}")]
    public async Task<IActionResult> GetById(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_COMPROBANTE_PAGO)
    { var r = await _service.GetByIdAsync(COD_CLASE_OPERACION, COD_TIPO_OPERACION, COD_TIPO_COMPROBANTE_PAGO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string codClaseOperacion, [FromQuery] string codTipoOperacion, [FromQuery] TiposOperacionTiposComprobantePagoSearchDto dto)
    { var r = await _service.SearchAsync(codClaseOperacion, codTipoOperacion, dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposOperacionTiposComprobantePagoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_CLASE_OPERACION}/{COD_TIPO_OPERACION}/{COD_TIPO_COMPROBANTE_PAGO}")]
    public async Task<IActionResult> Delete(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_COMPROBANTE_PAGO)
    { var r = await _service.DeleteAsync(COD_CLASE_OPERACION, COD_TIPO_OPERACION, COD_TIPO_COMPROBANTE_PAGO); return r.Success ? Ok(r) : BadRequest(r); }

}