using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Contabilidad;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/contabilidad/asientos-contables")]
[ExigePermiso("CONTABILIDAD.ASIENTOS_CONTABLES")]
public class AsientosContablesController : BaseController
{
    private readonly IAsientosContablesService _service;
    public AsientosContablesController(IAsientosContablesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ASIENTO_CONTABLE}")]
    public async Task<IActionResult> GetById(string COD_ASIENTO_CONTABLE)
    { var r = await _service.GetByIdAsync(COD_ASIENTO_CONTABLE); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] AsientosContablesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] AsientosContablesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] AsientosContablesUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_ASIENTO_CONTABLE}")]
    public async Task<IActionResult> Delete(string COD_ASIENTO_CONTABLE)
    { var r = await _service.DeleteAsync(COD_ASIENTO_CONTABLE); return r.Success ? Ok(r) : BadRequest(r); }

}