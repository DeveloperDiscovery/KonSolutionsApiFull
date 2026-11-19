using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/rrhh/cargos-implementos")]
[ExigePermiso("RRHH.CARGOS_IMPLEMENTOS")]
public class CargosImplementosController : BaseController
{
    private readonly ICargosImplementosService _service;
    public CargosImplementosController(ICargosImplementosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CARGO}/{COD_PRODUCTO_IMPLEMENTO}")]
    public async Task<IActionResult> GetById(int COD_CARGO, string COD_PRODUCTO_IMPLEMENTO)
    { var r = await _service.GetByIdAsync(COD_CARGO, COD_PRODUCTO_IMPLEMENTO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] CargosImplementosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] CargosImplementosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CargosImplementosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_CARGO}/{COD_PRODUCTO_IMPLEMENTO}")]
    public async Task<IActionResult> Delete(int COD_CARGO, string COD_PRODUCTO_IMPLEMENTO)
    { var r = await _service.DeleteAsync(COD_CARGO, COD_PRODUCTO_IMPLEMENTO); return r.Success ? Ok(r) : BadRequest(r); }

}