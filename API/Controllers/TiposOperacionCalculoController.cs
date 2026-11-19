using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/tipos-operacion-calculo")]
[ExigePermiso("COMERCIAL.TIPOS_OPERACION_CALCULO")]
public class TiposOperacionCalculoController : BaseController
{
    private readonly ITiposOperacionCalculoService _service;
    public TiposOperacionCalculoController(ITiposOperacionCalculoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codClaseOperacion, [FromQuery] string codTipoOperacion)
    { var r = await _service.GetAllAsync(codClaseOperacion, codTipoOperacion); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CLASE_OPERACION}/{COD_TIPO_OPERACION}/{COD_TIPO_VALOR_CALCULO}")]
    public async Task<IActionResult> GetById(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_VALOR_CALCULO)
    { var r = await _service.GetByIdAsync(COD_CLASE_OPERACION, COD_TIPO_OPERACION, COD_TIPO_VALOR_CALCULO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string codClaseOperacion, [FromQuery] string codTipoOperacion, [FromQuery] TiposOperacionCalculoSearchDto dto)
    { var r = await _service.SearchAsync(codClaseOperacion, codTipoOperacion, dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposOperacionCalculoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TiposOperacionCalculoUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_CLASE_OPERACION}/{COD_TIPO_OPERACION}/{COD_TIPO_VALOR_CALCULO}")]
    public async Task<IActionResult> Delete(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_VALOR_CALCULO)
    { var r = await _service.DeleteAsync(COD_CLASE_OPERACION, COD_TIPO_OPERACION, COD_TIPO_VALOR_CALCULO); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("copy")]
    public async Task<IActionResult> Copy([FromBody] TiposOperacionCalculoCopyDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.CopyAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

}