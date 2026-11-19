using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Networking;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/networking/categorias-comision-historico")]
[ExigePermiso("NETWORKING.CATEGORIAS_COMISION_HISTORICO")]
public class CategoriasComisionHistoricoController : BaseController
{
    private readonly ICategoriasComisionHistoricoService _service;
    public CategoriasComisionHistoricoController(ICategoriasComisionHistoricoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CATEGORIA_COMISION}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> GetById(string COD_CATEGORIA_COMISION, int NUM_SECUENCIA)
    { var r = await _service.GetByIdAsync(COD_CATEGORIA_COMISION, NUM_SECUENCIA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] CategoriasComisionHistoricoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] CategoriasComisionHistoricoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CategoriasComisionHistoricoUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_CATEGORIA_COMISION}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> Delete(string COD_CATEGORIA_COMISION, int NUM_SECUENCIA)
    { var r = await _service.DeleteAsync(COD_CATEGORIA_COMISION, NUM_SECUENCIA); return r.Success ? Ok(r) : BadRequest(r); }

}