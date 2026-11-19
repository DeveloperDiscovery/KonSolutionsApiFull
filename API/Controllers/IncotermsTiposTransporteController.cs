using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/incoterms-tipos-transporte")]
[ExigePermiso("COMERCIAL.INCOTERMS_TIPOS_TRANSPORTE")]
public class IncotermsTiposTransporteController : BaseController
{
    private readonly IIncotermsTiposTransporteService _service;
    public IncotermsTiposTransporteController(IIncotermsTiposTransporteService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_INCOTERM}/{COD_TIPO_TRANSPORTE}")]
    public async Task<IActionResult> GetById(string COD_INCOTERM, string COD_TIPO_TRANSPORTE)
    { var r = await _service.GetByIdAsync(COD_INCOTERM, COD_TIPO_TRANSPORTE); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] IncotermsTiposTransporteSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] IncotermsTiposTransporteInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_INCOTERM}/{COD_TIPO_TRANSPORTE}")]
    public async Task<IActionResult> Delete(string COD_INCOTERM, string COD_TIPO_TRANSPORTE)
    { var r = await _service.DeleteAsync(COD_INCOTERM, COD_TIPO_TRANSPORTE); return r.Success ? Ok(r) : BadRequest(r); }

}