using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/almacenes-tipos-movimiento")]
[ExigePermiso("LOGISTICA.ALMACENES_TIPOS_MOVIMIENTO")]
public class AlmacenesTiposMovimientoController : BaseController
{
    private readonly IAlmacenesTiposMovimientoService _service;
    public AlmacenesTiposMovimientoController(IAlmacenesTiposMovimientoService service) => _service = service;

    // Rutas literales antes que las de parámetros, para que "asignar" no se tome por un código.
    [HttpPost("asignar")]
    public async Task<IActionResult> Asignar([FromBody] AlmacenesTiposMovimientoLoteDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.AsignarAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("desasignar")]
    public async Task<IActionResult> Desasignar([FromBody] AlmacenesTiposMovimientoLoteDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.DesasignarAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] AlmacenesTiposMovimientoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("combobox/{COD_ALMACEN}/{COD_CLASE_MOVIMIENTO}")]
    public async Task<IActionResult> GetCombobox(string COD_ALMACEN, string COD_CLASE_MOVIMIENTO)
    { var r = await _service.GetComboboxAsync(COD_ALMACEN, COD_CLASE_MOVIMIENTO); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ALMACEN}/{COD_CLASE_MOVIMIENTO}")]
    public async Task<IActionResult> GetAll(string COD_ALMACEN, string COD_CLASE_MOVIMIENTO)
    { var r = await _service.GetAllAsync(COD_ALMACEN, COD_CLASE_MOVIMIENTO); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ALMACEN}/{COD_CLASE_MOVIMIENTO}/{COD_TIPO_MOVIMIENTO}")]
    public async Task<IActionResult> GetById(string COD_ALMACEN, string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO)
    { var r = await _service.GetByIdAsync(COD_ALMACEN, COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] AlmacenesTiposMovimientoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_ALMACEN}/{COD_CLASE_MOVIMIENTO}/{COD_TIPO_MOVIMIENTO}")]
    public async Task<IActionResult> Delete(string COD_ALMACEN, string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO)
    { var r = await _service.DeleteAsync(COD_ALMACEN, COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO); return r.Success ? Ok(r) : BadRequest(r); }
}
