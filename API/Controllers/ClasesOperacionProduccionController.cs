using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/planeamiento/clases-operacion-produccion")]
[ExigePermiso("PLANEAMIENTO.CLASES_OPERACION_PRODUCCION")]
public class ClasesOperacionProduccionController : BaseController
{
    private readonly IClasesOperacionProduccionService _service;
    public ClasesOperacionProduccionController(IClasesOperacionProduccionService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CLASE_OPERACION_PRODUCCION}")]
    public async Task<IActionResult> GetById(string COD_CLASE_OPERACION_PRODUCCION)
    { var r = await _service.GetByIdAsync(COD_CLASE_OPERACION_PRODUCCION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ClasesOperacionProduccionSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ClasesOperacionProduccionInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ClasesOperacionProduccionUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_CLASE_OPERACION_PRODUCCION}")]
    public async Task<IActionResult> Delete(string COD_CLASE_OPERACION_PRODUCCION)
    { var r = await _service.DeleteAsync(COD_CLASE_OPERACION_PRODUCCION); return r.Success ? Ok(r) : BadRequest(r); }

}