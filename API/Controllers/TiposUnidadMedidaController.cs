using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/tipos-unidad-medida")]
[ExigePermiso("ESTANDAR.TIPOS_UNIDAD_MEDIDA")]
public class TiposUnidadMedidaController : BaseController
{
    private readonly ITiposUnidadMedidaService _service;
    public TiposUnidadMedidaController(ITiposUnidadMedidaService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TIPO_UNIDAD_MEDIDA}")]
    public async Task<IActionResult> GetById(string COD_TIPO_UNIDAD_MEDIDA)
    { var r = await _service.GetByIdAsync(COD_TIPO_UNIDAD_MEDIDA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TiposUnidadMedidaSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposUnidadMedidaInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut("{COD_TIPO_UNIDAD_MEDIDA}")]
    public async Task<IActionResult> Update(string COD_TIPO_UNIDAD_MEDIDA, [FromBody] TiposUnidadMedidaInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(COD_TIPO_UNIDAD_MEDIDA, dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_TIPO_UNIDAD_MEDIDA}")]
    public async Task<IActionResult> Delete(string COD_TIPO_UNIDAD_MEDIDA)
    { var r = await _service.DeleteAsync(COD_TIPO_UNIDAD_MEDIDA); return r.Success ? Ok(r) : BadRequest(r); }

}