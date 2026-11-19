using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/entorno/tipos-notificacion")]
[ExigePermiso("ENTORNO.TIPOS_NOTIFICACION")]
public class TiposNotificacionController : BaseController
{
    private readonly ITiposNotificacionService _service;
    public TiposNotificacionController(ITiposNotificacionService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TIPO_NOTIFICACION}")]
    public async Task<IActionResult> GetById(string COD_TIPO_NOTIFICACION)
    { var r = await _service.GetByIdAsync(COD_TIPO_NOTIFICACION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TiposNotificacionSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposNotificacionInsertDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
        return r.Success ? StatusCode(201, r) : BadRequest(r);
    }

    [HttpDelete("{COD_TIPO_NOTIFICACION}")]
    public async Task<IActionResult> Delete(string COD_TIPO_NOTIFICACION)
    { var r = await _service.DeleteAsync(COD_TIPO_NOTIFICACION); return r.Success ? Ok(r) : BadRequest(r); }
}
