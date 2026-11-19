using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Ubicaciones de un almacén. Al ser tabla hija, TODAS las lecturas exigen
/// COD_ALMACEN — no existe un "listar todo" porque el SP SHOWALL lo recibe obligatorio.</summary>
[Authorize]
[Route("api/v1/logistica/almacenes-ubicaciones-almacen")]
[ExigePermiso("LOGISTICA.ALMACENES_UBICACIONES_ALMACEN")]
public class AlmacenesUbicacionesAlmacenController : BaseController
{
    private readonly IAlmacenesUbicacionesAlmacenService _service;
    public AlmacenesUbicacionesAlmacenController(IAlmacenesUbicacionesAlmacenService service) => _service = service;

    [HttpGet("{COD_ALMACEN}")]
    public async Task<IActionResult> GetAll(string COD_ALMACEN)
    { var r = await _service.GetAllAsync(COD_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ALMACEN}/{COD_UBICACION_ALMACEN}")]
    public async Task<IActionResult> GetById(string COD_ALMACEN, string COD_UBICACION_ALMACEN)
    { var r = await _service.GetByIdAsync(COD_ALMACEN, COD_UBICACION_ALMACEN); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox/{COD_ALMACEN}")]
    public async Task<IActionResult> GetCombobox(string COD_ALMACEN)
    { var r = await _service.GetComboboxAsync(COD_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] AlmacenesUbicacionesAlmacenSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] AlmacenesUbicacionesAlmacenInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_ALMACEN}/{COD_UBICACION_ALMACEN}")]
    public async Task<IActionResult> Delete(string COD_ALMACEN, string COD_UBICACION_ALMACEN)
    { var r = await _service.DeleteAsync(COD_ALMACEN, COD_UBICACION_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }
}
