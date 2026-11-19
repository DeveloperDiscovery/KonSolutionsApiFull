using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/ingenieria/disenos-version")]
[ExigePermiso("INGENIERIA.DISENOS_VERSION")]
public class DisenosVersionController : BaseController
{
    private readonly IDisenosVersionService _service;
    public DisenosVersionController(IDisenosVersionService service) => _service = service;

    // codDiseno es obligatorio en los tres listados: los SP lo exigen como parámetro.
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codDiseno)
    { var r = await _service.GetAllAsync(codDiseno); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_DISENO}/{NUM_VERSION:int}")]
    public async Task<IActionResult> GetById(string COD_DISENO, int NUM_VERSION)
    { var r = await _service.GetByIdAsync(COD_DISENO, NUM_VERSION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] string codDiseno)
    { var r = await _service.GetComboboxAsync(codDiseno); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string codDiseno, [FromQuery] DisenosVersionSearchDto dto)
    { var r = await _service.SearchAsync(codDiseno, dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] DisenosVersionInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_DISENO}/{NUM_VERSION:int}")]
    public async Task<IActionResult> Delete(string COD_DISENO, int NUM_VERSION)
    { var r = await _service.DeleteAsync(COD_DISENO, NUM_VERSION); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos()
    { var r = await _service.GetLoadcombosAsync(); return r.Success ? Ok(r) : BadRequest(r); }
}
