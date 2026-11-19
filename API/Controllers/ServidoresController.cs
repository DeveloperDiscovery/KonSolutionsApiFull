using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/entorno/servidores")]
[ExigePermiso("ENTORNO.SERVIDORES")]
public class ServidoresController : BaseController
{
    private readonly IServidoresService _service;
    public ServidoresController(IServidoresService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_KEY_SERVIDOR}")]
    public async Task<IActionResult> GetById(string COD_KEY_SERVIDOR)
    { var r = await _service.GetByIdAsync(COD_KEY_SERVIDOR); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ServidoresSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ServidoresInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_KEY_SERVIDOR}")]
    public async Task<IActionResult> Delete(string COD_KEY_SERVIDOR)
    { var r = await _service.DeleteAsync(COD_KEY_SERVIDOR); return r.Success ? Ok(r) : BadRequest(r); }

}
