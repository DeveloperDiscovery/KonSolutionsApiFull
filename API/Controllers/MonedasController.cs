using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/monedas")]
[ExigePermiso("ESTANDAR.MONEDAS")]
public class MonedasController : BaseController
{
    private readonly IMonedasService _service;
    public MonedasController(IMonedasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_MONEDA}")]
    public async Task<IActionResult> GetById(string COD_MONEDA)
    { var r = await _service.GetByIdAsync(COD_MONEDA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] MonedasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] MonedasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut("{COD_MONEDA}")]
    public async Task<IActionResult> Update(string COD_MONEDA, [FromBody] MonedasUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(COD_MONEDA, dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_MONEDA}")]
    public async Task<IActionResult> Delete(string COD_MONEDA)
    { var r = await _service.DeleteAsync(COD_MONEDA); return r.Success ? Ok(r) : BadRequest(r); }

}