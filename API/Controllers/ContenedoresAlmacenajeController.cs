using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/contenedores-almacenaje")]
[ExigePermiso("LOGISTICA.CONTENEDORES_ALMACENAJE")]
public class ContenedoresAlmacenajeController : BaseController
{
    private readonly IContenedoresAlmacenajeService _service;
    public ContenedoresAlmacenajeController(IContenedoresAlmacenajeService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ContenedoresAlmacenajeSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_LOGISTICA_CONTENEDORES_ALMACENAJE_LOADCOMBOS: Tipos de ubicación +
    /// Sucursales + Almacenes para ContenedoresAlmacenajeDialog.razor en un solo viaje.</summary>
    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos()
    { var r = await _service.LoadcombosAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CONTENEDOR}")]
    public async Task<IActionResult> GetById(string COD_CONTENEDOR)
    { var r = await _service.GetByIdAsync(COD_CONTENEDOR); return r.Success ? Ok(r) : NotFound(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ContenedoresAlmacenajeInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_CONTENEDOR}")]
    public async Task<IActionResult> Delete(string COD_CONTENEDOR)
    { var r = await _service.DeleteAsync(COD_CONTENEDOR); return r.Success ? Ok(r) : BadRequest(r); }
}
