using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Produccion;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/produccion/clases-proceso")]
[ExigePermiso("PRODUCCION.CLASES_PROCESO")]
public class ClasesProcesoController : BaseController
{
    private readonly IClasesProcesoService _service;
    public ClasesProcesoController(IClasesProcesoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CLASE_PROCESO}")]
    public async Task<IActionResult> GetById(string COD_CLASE_PROCESO)
    { var r = await _service.GetByIdAsync(COD_CLASE_PROCESO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ClasesProcesoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ClasesProcesoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ClasesProcesoUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_CLASE_PROCESO}")]
    public async Task<IActionResult> Delete(string COD_CLASE_PROCESO)
    { var r = await _service.DeleteAsync(COD_CLASE_PROCESO); return r.Success ? Ok(r) : BadRequest(r); }

}