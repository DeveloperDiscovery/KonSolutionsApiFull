using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Contabilidad;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/contabilidad/tipos-cuenta-contable")]
[ExigePermiso("CONTABILIDAD.TIPOS_CUENTA_CONTABLE")]
public class TiposCuentaContableController : BaseController
{
    private readonly ITiposCuentaContableService _service;
    public TiposCuentaContableController(ITiposCuentaContableService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TIPO_CUENTA_CONTABLE}")]
    public async Task<IActionResult> GetById(string COD_TIPO_CUENTA_CONTABLE)
    { var r = await _service.GetByIdAsync(COD_TIPO_CUENTA_CONTABLE); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TiposCuentaContableSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposCuentaContableInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TiposCuentaContableUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_TIPO_CUENTA_CONTABLE}")]
    public async Task<IActionResult> Delete(string COD_TIPO_CUENTA_CONTABLE)
    { var r = await _service.DeleteAsync(COD_TIPO_CUENTA_CONTABLE); return r.Success ? Ok(r) : BadRequest(r); }

}