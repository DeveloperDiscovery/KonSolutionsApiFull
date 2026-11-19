using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/rrhh/ranking-academico")]
[ExigePermiso("RRHH.RANKING_ACADEMICO")]
public class RankingAcademicoController : BaseController
{
    private readonly IRankingAcademicoService _service;
    public RankingAcademicoController(IRankingAcademicoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_RANKING_ACADEMICO}")]
    public async Task<IActionResult> GetById(string COD_RANKING_ACADEMICO)
    { var r = await _service.GetByIdAsync(COD_RANKING_ACADEMICO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] RankingAcademicoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] RankingAcademicoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] RankingAcademicoUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_RANKING_ACADEMICO}")]
    public async Task<IActionResult> Delete(string COD_RANKING_ACADEMICO)
    { var r = await _service.DeleteAsync(COD_RANKING_ACADEMICO); return r.Success ? Ok(r) : BadRequest(r); }

}