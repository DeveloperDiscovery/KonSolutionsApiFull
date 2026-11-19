using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/nomina/tipos-planilla-periodos")]
[ExigePermiso("NOMINA.TIPOS_PLANILLA_PERIODOS")]
public class TiposPlanillaPeriodosController : BaseController
{
    private readonly ITiposPlanillaPeriodosService _service;
    public TiposPlanillaPeriodosController(ITiposPlanillaPeriodosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TIPO_PLANILLA}/{COD_TIPO_PERIODO}")]
    public async Task<IActionResult> GetById(string COD_TIPO_PLANILLA, string COD_TIPO_PERIODO)
    { var r = await _service.GetByIdAsync(COD_TIPO_PLANILLA, COD_TIPO_PERIODO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TiposPlanillaPeriodosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposPlanillaPeriodosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_TIPO_PLANILLA}/{COD_TIPO_PERIODO}")]
    public async Task<IActionResult> Delete(string COD_TIPO_PLANILLA, string COD_TIPO_PERIODO)
    { var r = await _service.DeleteAsync(COD_TIPO_PLANILLA, COD_TIPO_PERIODO); return r.Success ? Ok(r) : BadRequest(r); }

}