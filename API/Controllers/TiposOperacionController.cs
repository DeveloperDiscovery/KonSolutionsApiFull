using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/tipos-operacion")]
[ExigePermiso("ESTANDAR.TIPOS_OPERACION")]
public class TiposOperacionController : BaseController
{
    private readonly ITiposOperacionService _service;
    public TiposOperacionController(ITiposOperacionService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codClaseOperacion)
    { var r = await _service.GetAllAsync(codClaseOperacion); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CLASE_OPERACION}/{COD_TIPO_OPERACION}")]
    public async Task<IActionResult> GetById(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION)
    { var r = await _service.GetByIdAsync(COD_CLASE_OPERACION, COD_TIPO_OPERACION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string codClaseOperacion, [FromQuery] TiposOperacionSearchDto dto)
    { var r = await _service.SearchAsync(codClaseOperacion, dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposOperacionInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TiposOperacionUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_CLASE_OPERACION}/{COD_TIPO_OPERACION}")]
    public async Task<IActionResult> Delete(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION)
    { var r = await _service.DeleteAsync(COD_CLASE_OPERACION, COD_TIPO_OPERACION); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_ESTANDAR_TIPOS_OPERACION_LOADCOMBOS: tipos de entidad, tipos de pedido
    /// comercial, tipos de precio, condiciones de pago, clases de operación y estados para
    /// el diálogo de Tipos de Operación en un solo viaje.</summary>
    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos([FromQuery] int codTipoEstadoClasesOperacion = 0)
    { var r = await _service.LoadcombosAsync(codTipoEstadoClasesOperacion); return r.Success ? Ok(r) : BadRequest(r); }

}