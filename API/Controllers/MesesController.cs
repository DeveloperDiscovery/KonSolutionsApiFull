using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/meses")]
[ExigePermiso("ESTANDAR.MESES")]
public class MesesController : BaseController
{
    private readonly IMesesService _service;
    public MesesController(IMesesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_MES}")]
    public async Task<IActionResult> GetById(int NUM_MES)
    { var r = await _service.GetByIdAsync(NUM_MES); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] MesesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{NUM_MES}")]
    public async Task<IActionResult> Delete(int NUM_MES)
    { var r = await _service.DeleteAsync(NUM_MES); return r.Success ? Ok(r) : BadRequest(r); }
}
