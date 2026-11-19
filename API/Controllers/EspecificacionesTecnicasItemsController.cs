using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/ingenieria/especificaciones-tecnicas-items")]
[ExigePermiso("INGENIERIA.ESPECIFICACIONES_ITEMS_TECNICOS")]
public class EspecificacionesTecnicasItemsController : BaseController
{
    private readonly IEspecificacionesTecnicasItemsService _service;
    public EspecificacionesTecnicasItemsController(IEspecificacionesTecnicasItemsService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ESPECIFICACION_TECNICA}/{COD_ITEM_TECNICO}")]
    public async Task<IActionResult> GetById(string COD_ESPECIFICACION_TECNICA, int COD_ITEM_TECNICO)
    { var r = await _service.GetByIdAsync(COD_ESPECIFICACION_TECNICA, COD_ITEM_TECNICO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EspecificacionesTecnicasItemsSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EspecificacionesTecnicasItemsInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EspecificacionesTecnicasItemsUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_ESPECIFICACION_TECNICA}/{COD_ITEM_TECNICO}")]
    public async Task<IActionResult> Delete(string COD_ESPECIFICACION_TECNICA, int COD_ITEM_TECNICO)
    { var r = await _service.DeleteAsync(COD_ESPECIFICACION_TECNICA, COD_ITEM_TECNICO); return r.Success ? Ok(r) : BadRequest(r); }

}