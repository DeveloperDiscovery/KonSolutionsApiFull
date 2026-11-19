using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/configuraciones-productos")]
[ExigePermiso("ESTANDAR.CONFIGURACIONES_PRODUCTOS")]
public class ConfiguracionesProductosController : BaseController
{
    private readonly IConfiguracionesProductosService _service;
    public ConfiguracionesProductosController(IConfiguracionesProductosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codTipoConfiguracion)
    { var r = await _service.GetAllAsync(codTipoConfiguracion); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{codTipoConfiguracion}/{numSecuencia}")]
    public async Task<IActionResult> GetById(string codTipoConfiguracion, short numSecuencia)
    { var r = await _service.GetByIdAsync(codTipoConfiguracion, numSecuencia); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] string codTipoConfiguracion)
    { var r = await _service.GetComboboxAsync(codTipoConfiguracion); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string codTipoConfiguracion, [FromQuery] string? search)
    { var r = await _service.SearchAsync(codTipoConfiguracion, search); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ConfiguracionesProductosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ConfiguracionesProductosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{codTipoConfiguracion}/{numSecuencia}")]
    public async Task<IActionResult> Delete(string codTipoConfiguracion, short numSecuencia)
    { var r = await _service.DeleteAsync(codTipoConfiguracion, numSecuencia); return r.Success ? Ok(r) : BadRequest(r); }

}
