using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/entidades-purcharse-orders")]
[ExigePermiso("COMERCIAL.ENTIDADES_PURCHARSE_ORDERS")]
public class EntidadesPurcharseOrdersController : BaseController
{
    private readonly IEntidadesPurcharseOrdersService _service;
    public EntidadesPurcharseOrdersController(IEntidadesPurcharseOrdersService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ENTIDAD}/{NUM_PURCHASE_ORDER}")]
    public async Task<IActionResult> GetById(decimal COD_ENTIDAD, string NUM_PURCHASE_ORDER)
    { var r = await _service.GetByIdAsync(COD_ENTIDAD, NUM_PURCHASE_ORDER); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EntidadesPurcharseOrdersSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EntidadesPurcharseOrdersInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EntidadesPurcharseOrdersUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_ENTIDAD}/{NUM_PURCHASE_ORDER}")]
    public async Task<IActionResult> Delete(decimal COD_ENTIDAD, string NUM_PURCHASE_ORDER)
    { var r = await _service.DeleteAsync(COD_ENTIDAD, NUM_PURCHASE_ORDER); return r.Success ? Ok(r) : BadRequest(r); }

}