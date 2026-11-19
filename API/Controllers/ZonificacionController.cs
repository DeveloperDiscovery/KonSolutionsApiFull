using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Zonificacion;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/zonificacion/zonificacion")]
[ExigePermiso("ZONIFICACION.ZONIFICACION")]
public class ZonificacionController : BaseController
{
    private readonly IZonificacionService _service;
    public ZonificacionController(IZonificacionService service) => _service = service;

    // Tabla hija: los SP SHOWALL/SHOWCOMBOX reciben @COD_SUCURSAL obligatorio,
    // así que no existe un "listar todo" — siempre se lista dentro de una sucursal.
    [HttpGet("{COD_SUCURSAL}")]
    public async Task<IActionResult> GetAll(string COD_SUCURSAL)
    { var r = await _service.GetAllAsync(COD_SUCURSAL); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_SUCURSAL}/{COD_ZONIFICACION}")]
    public async Task<IActionResult> GetById(string COD_SUCURSAL, string COD_ZONIFICACION)
    { var r = await _service.GetByIdAsync(COD_SUCURSAL, COD_ZONIFICACION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox/{COD_SUCURSAL}")]
    public async Task<IActionResult> GetCombobox(string COD_SUCURSAL)
    { var r = await _service.GetComboboxAsync(COD_SUCURSAL); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_ZONIFICACION_ZONIFICACION_LOADCOMBOS: tipos de zonificación + zonas de la
    /// sucursal (combo "Zona padre") + estados para ZonificacionDialog.razor en un solo viaje.
    /// codTipoEstado es opcional (se autorresuelve).</summary>
    [HttpGet("loadcombos/{COD_SUCURSAL}")]
    public async Task<IActionResult> GetLoadcombos(string COD_SUCURSAL, [FromQuery] int codTipoEstado = 0)
    { var r = await _service.LoadcombosAsync(COD_SUCURSAL, codTipoEstado); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ZonificacionSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ZonificacionInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_SUCURSAL}/{COD_ZONIFICACION}")]
    public async Task<IActionResult> Delete(string COD_SUCURSAL, string COD_ZONIFICACION)
    { var r = await _service.DeleteAsync(COD_SUCURSAL, COD_ZONIFICACION); return r.Success ? Ok(r) : BadRequest(r); }

}