using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/entorno/entidades-configurar")]
[ExigePermiso("ENTORNO.ENTIDADES_CONFIGURAR")]
public class EntidadesConfigurarController : BaseController
{
    private readonly IEntidadesConfigurarService _service;
    public EntidadesConfigurarController(IEntidadesConfigurarService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    // Por query string (no por segmento de ruta): DES_TABLE viaja como "ESQUEMA.TABLA" (con
    // punto), y eso rompe la resolución de rutas atributo si se pasa como {DES_TABLE}/{DES_COLUMN}.
    [HttpGet("byid")]
    public async Task<IActionResult> GetById([FromQuery] string desTable, [FromQuery] string desColumn)
    { var r = await _service.GetByIdAsync(desTable, desColumn); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] string desTable)
    { var r = await _service.GetComboboxAsync(desTable); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EntidadesConfigurarSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EntidadesConfigurarInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EntidadesConfigurarUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string desTable, [FromQuery] string desColumn)
    { var r = await _service.DeleteAsync(desTable, desColumn); return r.Success ? Ok(r) : BadRequest(r); }
}
