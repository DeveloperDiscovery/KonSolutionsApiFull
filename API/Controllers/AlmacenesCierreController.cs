using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Cierre de almacenes por fecha (LOGISTICA.ALMACENES_CIERRE). Mientras un cierre está vigente, los SP de movimientos,
/// de detalle y de stock rechazan con el error 50590 cualquier operación del almacén cuya fecha caiga dentro del periodo
/// (o hecha hoy, si hoy cae dentro). Reabrir deja rastro (quién, cuándo, por qué).
///
/// POST cierra (uno o varios almacenes a la vez), PUT modifica, PUT {id}/reabrir reabre y DELETE borra un cierre ya reabierto.</summary>
[Authorize]
[Route("api/v1/logistica/almacenes-cierre")]
[ExigePermiso("LOGISTICA.ALMACENES_CIERRE")]
public class AlmacenesCierreController : BaseController
{
    private readonly IAlmacenesCierreService _service;
    public AlmacenesCierreController(IAlmacenesCierreService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_CIERRE_ALMACEN:int}")]
    public async Task<IActionResult> GetById(int NUM_CIERRE_ALMACEN)
    { var r = await _service.GetByIdAsync(NUM_CIERRE_ALMACEN); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] AlmacenesCierreSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>¿El almacén admite operar en esa fecha (y hoy)? Es solo consulta y la usan las pantallas de movimientos para
    /// avisar antes de intentar: por eso va con el permiso de movimientos y no con el de los cierres.</summary>
    [HttpGet("consulta")]
    [ExigePermiso("LOGISTICA.MOVIMIENTOS_ALMACEN")]
    public async Task<IActionResult> Consultar([FromQuery] string COD_ALMACEN, [FromQuery] DateTime? FEC_MOVIMENTO)
    { var r = await _service.ConsultarAsync(COD_ALMACEN, FEC_MOVIMENTO); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] AlmacenesCierreInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] AlmacenesCierreUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPut("{NUM_CIERRE_ALMACEN:int}/reabrir")]
    public async Task<IActionResult> Reabrir(int NUM_CIERRE_ALMACEN, [FromBody] AlmacenesCierreReabrirDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.ReabrirAsync(NUM_CIERRE_ALMACEN, dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_CIERRE_ALMACEN:int}")]
    public async Task<IActionResult> Delete(int NUM_CIERRE_ALMACEN)
    { var r = await _service.DeleteAsync(NUM_CIERRE_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }
}
