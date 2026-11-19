using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/ingenieria/tipos-elemento-formula")]
[ExigePermiso("INGENIERIA.TIPOS_ELEMENTO_FORMULA")]
public class TiposElementoFormulaController : BaseController
{
    private readonly ITiposElementoFormulaService _service;
    public TiposElementoFormulaController(ITiposElementoFormulaService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CLASE_ELEMENTO_FORMULA}/{COD_TIPO_ELEMENTO_FORMULA}")]
    public async Task<IActionResult> GetById(int COD_CLASE_ELEMENTO_FORMULA, int COD_TIPO_ELEMENTO_FORMULA)
    { var r = await _service.GetByIdAsync(COD_CLASE_ELEMENTO_FORMULA, COD_TIPO_ELEMENTO_FORMULA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TiposElementoFormulaSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposElementoFormulaInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TiposElementoFormulaUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_CLASE_ELEMENTO_FORMULA}/{COD_TIPO_ELEMENTO_FORMULA}")]
    public async Task<IActionResult> Delete(int COD_CLASE_ELEMENTO_FORMULA, int COD_TIPO_ELEMENTO_FORMULA)
    { var r = await _service.DeleteAsync(COD_CLASE_ELEMENTO_FORMULA, COD_TIPO_ELEMENTO_FORMULA); return r.Success ? Ok(r) : BadRequest(r); }

}