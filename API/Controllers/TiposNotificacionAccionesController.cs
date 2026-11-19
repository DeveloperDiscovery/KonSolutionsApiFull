using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/entorno/tipos-notificacion-acciones")]
[ExigePermiso("ENTORNO.TIPOS_NOTIFICACION_ACCIONES")]
public class TiposNotificacionAccionesController : BaseController
{
    private readonly ITiposNotificacionAccionesService _service;
    public TiposNotificacionAccionesController(ITiposNotificacionAccionesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codTipoNotificacion)
    { var r = await _service.GetAllAsync(codTipoNotificacion); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{codTipoNotificacion}/{numSecuencia}")]
    public async Task<IActionResult> GetById(string codTipoNotificacion, int numSecuencia)
    { var r = await _service.GetByIdAsync(codTipoNotificacion, numSecuencia); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] string codTipoNotificacion)
    { var r = await _service.GetComboboxAsync(codTipoNotificacion); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TiposNotificacionAccionesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposNotificacionAccionesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{codTipoNotificacion}/{numSecuencia}")]
    public async Task<IActionResult> Delete(string codTipoNotificacion, int numSecuencia)
    { var r = await _service.DeleteAsync(codTipoNotificacion, numSecuencia); return r.Success ? Ok(r) : BadRequest(r); }
}
