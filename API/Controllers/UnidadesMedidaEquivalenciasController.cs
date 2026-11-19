using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/unidades-medida-equivalencias")]
[ExigePermiso("ESTANDAR.UNIDADES_MEDIDA_EQUIVALENCIAS")]
public class UnidadesMedidaEquivalenciasController : BaseController
{
    private readonly IUnidadesMedidaEquivalenciasService _service;
    public UnidadesMedidaEquivalenciasController(IUnidadesMedidaEquivalenciasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_UNIDAD_MEDIDA}/{COD_UNIDAD_MEDIDA_EQUIVALENTE}")]
    public async Task<IActionResult> GetById(string COD_UNIDAD_MEDIDA, string COD_UNIDAD_MEDIDA_EQUIVALENTE)
    { var r = await _service.GetByIdAsync(COD_UNIDAD_MEDIDA, COD_UNIDAD_MEDIDA_EQUIVALENTE); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] UnidadesMedidaEquivalenciasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] UnidadesMedidaEquivalenciasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UnidadesMedidaEquivalenciasUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("por-tipo/{COD_TIPO_UNIDAD_MEDIDA}")]
    public async Task<IActionResult> GetByTipo(string COD_TIPO_UNIDAD_MEDIDA)
    { var r = await _service.GetByTipoAsync(COD_TIPO_UNIDAD_MEDIDA); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("upsert")]
    public async Task<IActionResult> Upsert([FromBody] UnidadesMedidaEquivalenciasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpsertAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_UNIDAD_MEDIDA}/{COD_UNIDAD_MEDIDA_EQUIVALENTE}")]
    public async Task<IActionResult> Delete(string COD_UNIDAD_MEDIDA, string COD_UNIDAD_MEDIDA_EQUIVALENTE)
    { var r = await _service.DeleteAsync(COD_UNIDAD_MEDIDA, COD_UNIDAD_MEDIDA_EQUIVALENTE); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("cargar-estandar/{COD_TIPO_UNIDAD_MEDIDA}")]
    public async Task<IActionResult> CargarEstandar(string COD_TIPO_UNIDAD_MEDIDA)
    { var r = await _service.CargarEstandarAsync(COD_TIPO_UNIDAD_MEDIDA); return r.Success ? Ok(r) : BadRequest(r); }

}