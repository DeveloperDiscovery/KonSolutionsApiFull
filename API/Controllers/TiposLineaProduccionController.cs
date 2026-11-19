using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Produccion;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/produccion/tipos-linea-produccion")]
[ExigePermiso("PRODUCCION.TIPOS_LINEA_PRODUCCION")]
public class TiposLineaProduccionController : BaseController
{
    private readonly ITiposLineaProduccionService _service;
    public TiposLineaProduccionController(ITiposLineaProduccionService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TIPO_LINEA_PRODUCCION}")]
    public async Task<IActionResult> GetById(string COD_TIPO_LINEA_PRODUCCION)
    { var r = await _service.GetByIdAsync(COD_TIPO_LINEA_PRODUCCION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TiposLineaProduccionSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposLineaProduccionInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TiposLineaProduccionUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_TIPO_LINEA_PRODUCCION}")]
    public async Task<IActionResult> Delete(string COD_TIPO_LINEA_PRODUCCION)
    { var r = await _service.DeleteAsync(COD_TIPO_LINEA_PRODUCCION); return r.Success ? Ok(r) : BadRequest(r); }

}