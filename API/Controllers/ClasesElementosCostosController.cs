using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/costos/clases-elementos-costos")]
[ExigePermiso("COSTOS.CLASES_ELEMENTOS_COSTOS")]
public class ClasesElementosCostosController : BaseController
{
    private readonly IClasesElementosCostosService _service;
    public ClasesElementosCostosController(IClasesElementosCostosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CLASE_COSTO}/{COD_ELEMENTO_COSTO}")]
    public async Task<IActionResult> GetById(string COD_CLASE_COSTO, string COD_ELEMENTO_COSTO)
    { var r = await _service.GetByIdAsync(COD_CLASE_COSTO, COD_ELEMENTO_COSTO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ClasesElementosCostosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ClasesElementosCostosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ClasesElementosCostosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_CLASE_COSTO}/{COD_ELEMENTO_COSTO}")]
    public async Task<IActionResult> Delete(string COD_CLASE_COSTO, string COD_ELEMENTO_COSTO)
    { var r = await _service.DeleteAsync(COD_CLASE_COSTO, COD_ELEMENTO_COSTO); return r.Success ? Ok(r) : BadRequest(r); }

}