using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/anos")]
[ExigePermiso("ESTANDAR.ANOS")]
public class AnosController : BaseController
{
    private readonly IAnosService _service;
    public AnosController(IAnosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_ANO}")]
    public async Task<IActionResult> GetById(int NUM_ANO)
    { var r = await _service.GetByIdAsync(NUM_ANO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] AnosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{NUM_ANO}")]
    public async Task<IActionResult> Delete(int NUM_ANO)
    { var r = await _service.DeleteAsync(NUM_ANO); return r.Success ? Ok(r) : BadRequest(r); }
}
