using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/entorno/banco-colores")]
[ExigePermiso("ENTORNO.BANCO_COLORES")]
public class BancoColoresController : BaseController
{
    private readonly IBancoColoresService _service;
    public BancoColoresController(IBancoColoresService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_BANCO_COLOR:int}")]
    public async Task<IActionResult> GetById(int COD_BANCO_COLOR)
    { var r = await _service.GetByIdAsync(COD_BANCO_COLOR); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] BancoColoresSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Upsert: COD_BANCO_COLOR = 0 → alta nueva (IDENTITY); &gt; 0 → edita el existente.</summary>
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] BancoColoresSaveDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_BANCO_COLOR:int}")]
    public async Task<IActionResult> Delete(int COD_BANCO_COLOR)
    { var r = await _service.DeleteAsync(COD_BANCO_COLOR); return r.Success ? Ok(r) : BadRequest(r); }
}
