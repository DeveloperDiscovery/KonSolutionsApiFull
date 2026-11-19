using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/tipos-operacion-calculo-afectados")]
[ExigePermiso("COMERCIAL.TIPOS_OPERACION_CALCULO_AFECTADOS")]
public class TiposOperacionCalculoAfectadosController : BaseController
{
    private readonly ITiposOperacionCalculoAfectadosService _service;
    public TiposOperacionCalculoAfectadosController(ITiposOperacionCalculoAfectadosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codClaseOperacion, [FromQuery] string codTipoOperacion, [FromQuery] string codTipoValorCalculo)
    { var r = await _service.GetAllAsync(codClaseOperacion, codTipoOperacion, codTipoValorCalculo); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CLASE_OPERACION}/{COD_TIPO_OPERACION}/{COD_TIPO_VALOR_CALCULO}/{COD_TIPO_VALOR_CALCULO_AFECTADO}")]
    public async Task<IActionResult> GetById(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_VALOR_CALCULO, string COD_TIPO_VALOR_CALCULO_AFECTADO)
    { var r = await _service.GetByIdAsync(COD_CLASE_OPERACION, COD_TIPO_OPERACION, COD_TIPO_VALOR_CALCULO, COD_TIPO_VALOR_CALCULO_AFECTADO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string codClaseOperacion, [FromQuery] string codTipoOperacion, [FromQuery] string codTipoValorCalculo, [FromQuery] TiposOperacionCalculoAfectadosSearchDto dto)
    { var r = await _service.SearchAsync(codClaseOperacion, codTipoOperacion, codTipoValorCalculo, dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposOperacionCalculoAfectadosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_CLASE_OPERACION}/{COD_TIPO_OPERACION}/{COD_TIPO_VALOR_CALCULO}/{COD_TIPO_VALOR_CALCULO_AFECTADO}")]
    public async Task<IActionResult> Delete(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_VALOR_CALCULO, string COD_TIPO_VALOR_CALCULO_AFECTADO)
    { var r = await _service.DeleteAsync(COD_CLASE_OPERACION, COD_TIPO_OPERACION, COD_TIPO_VALOR_CALCULO, COD_TIPO_VALOR_CALCULO_AFECTADO); return r.Success ? Ok(r) : BadRequest(r); }
}
