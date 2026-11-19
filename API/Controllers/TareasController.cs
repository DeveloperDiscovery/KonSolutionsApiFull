using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Soporte;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/soporte/tareas")]
[ExigePermiso("SOPORTE.TAREAS")]
public class TareasController : BaseController
{
    private readonly ITareasService _service;
    public TareasController(ITareasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TAREA}")]
    public async Task<IActionResult> GetById(int COD_TAREA)
    { var r = await _service.GetByIdAsync(COD_TAREA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TareasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT de este módulo ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TareasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TareasUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_TAREA}")]
    public async Task<IActionResult> Delete(int COD_TAREA)
    { var r = await _service.DeleteAsync(COD_TAREA); return r.Success ? Ok(r) : BadRequest(r); }

}