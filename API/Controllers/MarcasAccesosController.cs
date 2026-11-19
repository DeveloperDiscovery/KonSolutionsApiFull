using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Safety;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/safety/marcas-accesos")]
[ExigePermiso("SAFETY.MARCAS_ACCESOS")]
public class MarcasAccesosController : BaseController
{
    private readonly IMarcasAccesosService _service;
    public MarcasAccesosController(IMarcasAccesosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_READ_TAG}")]
    public async Task<IActionResult> GetById(int NUM_READ_TAG)
    { var r = await _service.GetByIdAsync(NUM_READ_TAG); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] MarcasAccesosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] MarcasAccesosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] MarcasAccesosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_READ_TAG}")]
    public async Task<IActionResult> Delete(int NUM_READ_TAG)
    { var r = await _service.DeleteAsync(NUM_READ_TAG); return r.Success ? Ok(r) : BadRequest(r); }

}