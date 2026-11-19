using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/rrhh/nueve-box-desempeno-potencial")]
[ExigePermiso("RRHH.NUEVE_BOX_DESEMPENO_POTENCIAL")]
public class NueveBoxDesempenoPotencialController : BaseController
{
    private readonly INueveBoxDesempenoPotencialService _service;
    public NueveBoxDesempenoPotencialController(INueveBoxDesempenoPotencialService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_DESEMPENO_9BOX}/{COD_POTENCIAL_9BOX}")]
    public async Task<IActionResult> GetById(string COD_DESEMPENO_9BOX, string COD_POTENCIAL_9BOX)
    { var r = await _service.GetByIdAsync(COD_DESEMPENO_9BOX, COD_POTENCIAL_9BOX); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] NueveBoxDesempenoPotencialSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] NueveBoxDesempenoPotencialInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] NueveBoxDesempenoPotencialUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_DESEMPENO_9BOX}/{COD_POTENCIAL_9BOX}")]
    public async Task<IActionResult> Delete(string COD_DESEMPENO_9BOX, string COD_POTENCIAL_9BOX)
    { var r = await _service.DeleteAsync(COD_DESEMPENO_9BOX, COD_POTENCIAL_9BOX); return r.Success ? Ok(r) : BadRequest(r); }

}