using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/costos/costo-sub-elementos-centro-costo")]
[ExigePermiso("COSTOS.COSTO_SUB_ELEMENTOS_CENTRO_COSTO")]
public class CostoSubElementosCentroCostoController : BaseController
{
    private readonly ICostoSubElementosCentroCostoService _service;
    public CostoSubElementosCentroCostoController(ICostoSubElementosCentroCostoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_PERIODO_CONTABLE}/{COD_ELEMENTO_COSTO}/{COD_SUB_ELEMENTO_COSTO}")]
    public async Task<IActionResult> GetById(int NUM_PERIODO_CONTABLE, string COD_ELEMENTO_COSTO, string COD_SUB_ELEMENTO_COSTO)
    { var r = await _service.GetByIdAsync(NUM_PERIODO_CONTABLE, COD_ELEMENTO_COSTO, COD_SUB_ELEMENTO_COSTO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] CostoSubElementosCentroCostoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] CostoSubElementosCentroCostoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CostoSubElementosCentroCostoUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_PERIODO_CONTABLE}/{COD_ELEMENTO_COSTO}/{COD_SUB_ELEMENTO_COSTO}")]
    public async Task<IActionResult> Delete(int NUM_PERIODO_CONTABLE, string COD_ELEMENTO_COSTO, string COD_SUB_ELEMENTO_COSTO)
    { var r = await _service.DeleteAsync(NUM_PERIODO_CONTABLE, COD_ELEMENTO_COSTO, COD_SUB_ELEMENTO_COSTO); return r.Success ? Ok(r) : BadRequest(r); }

}