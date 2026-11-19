using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/entorno/diccionario-etiquetas")]
[ExigePermiso("ENTORNO.DICCIONARIO_ETIQUETAS")]
public class DiccionarioEtiquetasController : BaseController
{
    private readonly IDiccionarioEtiquetasService _service;
    public DiccionarioEtiquetasController(IDiccionarioEtiquetasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{codEtiqueta}")]
    public async Task<IActionResult> GetById(string codEtiqueta)
    { var r = await _service.GetByIdAsync(codEtiqueta); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] DiccionarioEtiquetasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{codEtiqueta}")]
    public async Task<IActionResult> Delete(string codEtiqueta)
    { var r = await _service.DeleteAsync(codEtiqueta); return r.Success ? Ok(r) : BadRequest(r); }
}
