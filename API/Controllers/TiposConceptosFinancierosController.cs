using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/finanzas/tipos-conceptos-financieros")]
[ExigePermiso("FINANZAS.TIPOS_CONCEPTOS_FINANCIEROS")]
public class TiposConceptosFinancierosController : BaseController
{
    private readonly ITiposConceptosFinancierosService _service;
    public TiposConceptosFinancierosController(ITiposConceptosFinancierosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TIPO_CONCEPTO_FINANCIERO}")]
    public async Task<IActionResult> GetById(string COD_TIPO_CONCEPTO_FINANCIERO)
    { var r = await _service.GetByIdAsync(COD_TIPO_CONCEPTO_FINANCIERO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TiposConceptosFinancierosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposConceptosFinancierosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TiposConceptosFinancierosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_TIPO_CONCEPTO_FINANCIERO}")]
    public async Task<IActionResult> Delete(string COD_TIPO_CONCEPTO_FINANCIERO)
    { var r = await _service.DeleteAsync(COD_TIPO_CONCEPTO_FINANCIERO); return r.Success ? Ok(r) : BadRequest(r); }

}