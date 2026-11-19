using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/almacenes")]
[ExigePermiso("LOGISTICA.ALMACENES")]
public class AlmacenesController : BaseController
{
    private readonly IAlmacenesService _service;
    public AlmacenesController(IAlmacenesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ALMACEN}")]
    public async Task<IActionResult> GetById(string COD_ALMACEN)
    { var r = await _service.GetByIdAsync(COD_ALMACEN); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] AlmacenesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_LOGISTICA_ALMACENES_LOADCOMBOS: Tipos de almacén + Sucursales + Procesos
    /// para AlmacenesDialog.razor en un solo viaje.</summary>
    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos()
    { var r = await _service.LoadcombosAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] AlmacenesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_ALMACEN}")]
    public async Task<IActionResult> Delete(string COD_ALMACEN)
    { var r = await _service.DeleteAsync(COD_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

}