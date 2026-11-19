using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/costos/costos-entidades-tarifado")]
[ExigePermiso("COSTOS.COSTOS_ENTIDADES_TARIFADO")]
public class CostosEntidadesTarifadoController : BaseController
{
    private readonly ICostosEntidadesTarifadoService _service;
    public CostosEntidadesTarifadoController(ICostosEntidadesTarifadoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_PERIODO_CONTABLE}/{COD_PROCESO}/{COD_PRODUCTO}")]
    public async Task<IActionResult> GetById(int NUM_PERIODO_CONTABLE, string COD_PROCESO, string COD_PRODUCTO)
    { var r = await _service.GetByIdAsync(NUM_PERIODO_CONTABLE, COD_PROCESO, COD_PRODUCTO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] CostosEntidadesTarifadoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] CostosEntidadesTarifadoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CostosEntidadesTarifadoUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_PERIODO_CONTABLE}/{COD_PROCESO}/{COD_PRODUCTO}")]
    public async Task<IActionResult> Delete(int NUM_PERIODO_CONTABLE, string COD_PROCESO, string COD_PRODUCTO)
    { var r = await _service.DeleteAsync(NUM_PERIODO_CONTABLE, COD_PROCESO, COD_PRODUCTO); return r.Success ? Ok(r) : BadRequest(r); }

}