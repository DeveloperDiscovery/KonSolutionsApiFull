using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/seguridad/botones-estandar")]
[ExigePermiso("SEGURIDAD.BOTONES_ESTANDAR")]
public class BotonesEstandarController : BaseController
{
    private readonly IBotonesEstandarService _service;
    public BotonesEstandarController(IBotonesEstandarService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_BOTON_OPCION_ESTANDAR}")]
    public async Task<IActionResult> GetById(string COD_BOTON_OPCION_ESTANDAR)
    { var r = await _service.GetByIdAsync(COD_BOTON_OPCION_ESTANDAR); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] BotonesEstandarSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] BotonesEstandarInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] BotonesEstandarUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_BOTON_OPCION_ESTANDAR}")]
    public async Task<IActionResult> Delete(string COD_BOTON_OPCION_ESTANDAR)
    { var r = await _service.DeleteAsync(COD_BOTON_OPCION_ESTANDAR); return r.Success ? Ok(r) : BadRequest(r); }

}