using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/finanzas/cajas-saldos")]
[ExigePermiso("FINANZAS.CAJAS_SALDOS")]
public class CajasSaldosController : BaseController
{
    private readonly ICajasSaldosService _service;
    public CajasSaldosController(ICajasSaldosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CAJA}/{FEC_CIERRE}/{COD_TURNO}")]
    public async Task<IActionResult> GetById(string COD_CAJA, DateTime FEC_CIERRE, string COD_TURNO)
    { var r = await _service.GetByIdAsync(COD_CAJA, FEC_CIERRE, COD_TURNO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] CajasSaldosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] CajasSaldosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CajasSaldosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_CAJA}/{FEC_CIERRE}/{COD_TURNO}")]
    public async Task<IActionResult> Delete(string COD_CAJA, DateTime FEC_CIERRE, string COD_TURNO)
    { var r = await _service.DeleteAsync(COD_CAJA, FEC_CIERRE, COD_TURNO); return r.Success ? Ok(r) : BadRequest(r); }

}