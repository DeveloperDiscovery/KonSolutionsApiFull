using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/guias-remision-series")]
[ExigePermiso("LOGISTICA.GUIAS_REMISION_SERIES")]
public class GuiasRemisionSeriesController : BaseController
{
    private readonly IGuiasRemisionSeriesService _service;
    public GuiasRemisionSeriesController(IGuiasRemisionSeriesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_SERIE}")]
    public async Task<IActionResult> GetById(string COD_SERIE)
    { var r = await _service.GetByIdAsync(COD_SERIE); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] GuiaRemisionSerieInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] GuiaRemisionSerieUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_SERIE}")]
    public async Task<IActionResult> Delete(string COD_SERIE)
    { var r = await _service.DeleteAsync(COD_SERIE); return r.Success ? Ok(r) : BadRequest(r); }
}
