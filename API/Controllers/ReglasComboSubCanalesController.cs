using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/reglas-combo-sub-canales")]
[ExigePermiso("COMERCIAL.REGLAS_COMBO_SUB_CANALES")]
public class ReglasComboSubCanalesController : BaseController
{
    private readonly IReglasComboSubCanalesService _service;
    public ReglasComboSubCanalesController(IReglasComboSubCanalesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_UNIDAD_COMERCIAL}/{NUM_CANAL_VENTA}/{NUM_SUB_CANAL_VENTA}")]
    public async Task<IActionResult> GetById(int NUM_UNIDAD_COMERCIAL, int NUM_CANAL_VENTA, int NUM_SUB_CANAL_VENTA)
    { var r = await _service.GetByIdAsync(NUM_UNIDAD_COMERCIAL, NUM_CANAL_VENTA, NUM_SUB_CANAL_VENTA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ReglasComboSubCanalesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ReglasComboSubCanalesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ReglasComboSubCanalesUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_UNIDAD_COMERCIAL}/{NUM_CANAL_VENTA}/{NUM_SUB_CANAL_VENTA}")]
    public async Task<IActionResult> Delete(int NUM_UNIDAD_COMERCIAL, int NUM_CANAL_VENTA, int NUM_SUB_CANAL_VENTA)
    { var r = await _service.DeleteAsync(NUM_UNIDAD_COMERCIAL, NUM_CANAL_VENTA, NUM_SUB_CANAL_VENTA); return r.Success ? Ok(r) : BadRequest(r); }

}