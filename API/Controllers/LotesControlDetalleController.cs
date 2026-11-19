using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/lotes-control-detalle")]
[ExigePermiso("LOGISTICA.LOTES_CONTROL_DETALLE")]
public class LotesControlDetalleController : BaseController
{
    private readonly ILotesControlDetalleService _service;
    public LotesControlDetalleController(ILotesControlDetalleService service) => _service = service;

    // El detalle se lee siempre dentro de su lote: el procedimiento lo exige.
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string NUM_LOTE_CONTROL)
    { var r = await _service.GetAllAsync(NUM_LOTE_CONTROL); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("por-cabecera/{NUM_LOTE_CONTROL}")]
    public async Task<IActionResult> GetPorCabecera(string NUM_LOTE_CONTROL)
    { var r = await _service.GetAllAsync(NUM_LOTE_CONTROL); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_LOTE_CONTROL}/{COD_PRODUCTO}")]
    public async Task<IActionResult> GetById(string NUM_LOTE_CONTROL, string COD_PRODUCTO)
    { var r = await _service.GetByIdAsync(NUM_LOTE_CONTROL, COD_PRODUCTO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] LotesControlDetalleSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] LotesControlDetalleInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] LotesControlDetalleUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_LOTE_CONTROL}/{COD_PRODUCTO}")]
    public async Task<IActionResult> Delete(string NUM_LOTE_CONTROL, string COD_PRODUCTO)
    { var r = await _service.DeleteAsync(NUM_LOTE_CONTROL, COD_PRODUCTO); return r.Success ? Ok(r) : BadRequest(r); }

}