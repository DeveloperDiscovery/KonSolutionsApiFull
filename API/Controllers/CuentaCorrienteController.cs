using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/finanzas/cuenta-corriente")]
[ExigePermiso("FINANZAS.CUENTA_CORRIENTE")]
public class CuentaCorrienteController : BaseController
{
    private readonly ICuentaCorrienteService _service;
    public CuentaCorrienteController(ICuentaCorrienteService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ENTIDAD}/{COD_TIPO_ENTIDAD}/{COD_MONEDA}")]
    public async Task<IActionResult> GetById(decimal COD_ENTIDAD, string COD_TIPO_ENTIDAD, string COD_MONEDA)
    { var r = await _service.GetByIdAsync(COD_ENTIDAD, COD_TIPO_ENTIDAD, COD_MONEDA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] CuentaCorrienteSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] CuentaCorrienteInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CuentaCorrienteUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_ENTIDAD}/{COD_TIPO_ENTIDAD}/{COD_MONEDA}")]
    public async Task<IActionResult> Delete(decimal COD_ENTIDAD, string COD_TIPO_ENTIDAD, string COD_MONEDA)
    { var r = await _service.DeleteAsync(COD_ENTIDAD, COD_TIPO_ENTIDAD, COD_MONEDA); return r.Success ? Ok(r) : BadRequest(r); }

}