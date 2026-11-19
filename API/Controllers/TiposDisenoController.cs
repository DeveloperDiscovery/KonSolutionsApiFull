using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/ingenieria/tipos-diseno")]
[ExigePermiso("INGENIERIA.TIPOS_DISENO")]
public class TiposDisenoController : BaseController
{
    private readonly ITiposDisenoService _service;
    public TiposDisenoController(ITiposDisenoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TIPO_DISENO}")]
    public async Task<IActionResult> GetById(string COD_TIPO_DISENO)
    { var r = await _service.GetByIdAsync(COD_TIPO_DISENO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TiposDisenoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposDisenoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_TIPO_DISENO}")]
    public async Task<IActionResult> Delete(string COD_TIPO_DISENO)
    { var r = await _service.DeleteAsync(COD_TIPO_DISENO); return r.Success ? Ok(r) : BadRequest(r); }

}