using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/incoterms-fases")]
[ExigePermiso("COMERCIAL.INCOTERMS_FASES")]
public class IncotermsFasesController : BaseController
{
    private readonly IIncotermsFasesService _service;
    public IncotermsFasesController(IIncotermsFasesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codIncoterm)
    { var r = await _service.GetAllAsync(codIncoterm); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_INCOTERM}/{COD_FASE_INCOTERM}")]
    public async Task<IActionResult> GetById(string COD_INCOTERM, string COD_FASE_INCOTERM)
    { var r = await _service.GetByIdAsync(COD_INCOTERM, COD_FASE_INCOTERM); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] IncotermsFasesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] IncotermsFasesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] IncotermsFasesUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_INCOTERM}/{COD_FASE_INCOTERM}")]
    public async Task<IActionResult> Delete(string COD_INCOTERM, string COD_FASE_INCOTERM)
    { var r = await _service.DeleteAsync(COD_INCOTERM, COD_FASE_INCOTERM); return r.Success ? Ok(r) : BadRequest(r); }

}