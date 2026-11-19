using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/movimientos-almacen-detalle")]
[ExigePermiso("LOGISTICA.MOVIMIENTOS_ALMACEN_DETALLE")]
public class MovimientosAlmacenDetalleController : BaseController
{
    private readonly IMovimientosAlmacenDetalleService _service;
    public MovimientosAlmacenDetalleController(IMovimientosAlmacenDetalleService service) => _service = service;

    // El movimiento es obligatorio: el SP lo exige y no tiene valor por defecto. Sin el,
    // esto devolvia "expects parameter '@NUM_MOVIMIENTO_ALMACEN', which was not supplied".
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.GetAllAsync(NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_MOVIMIENTO_ALMACEN}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> GetById(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_SECUENCIA)
    { var r = await _service.GetByIdAsync(NUM_MOVIMIENTO_ALMACEN, NUM_SECUENCIA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] MovimientosAlmacenDetalleSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // Alta manual del detalle. Van acá, con el permiso del detalle, y no en Stocks: quien carga
    // un movimiento no necesariamente tiene permiso sobre la consulta de stock.
    [HttpGet("stock-disponible")]
    public async Task<IActionResult> StockDisponible([FromQuery] decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.StockDisponibleAsync(NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("combos-linea")]
    public async Task<IActionResult> CombosLinea([FromQuery] decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.CombosLineaAsync(NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] MovimientosAlmacenDetalleInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] MovimientosAlmacenDetalleUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    // ── Lecturas de etiquetas (RFID / codigo de barras) ───────────────────────────────────────
    // Van aca, con el permiso del detalle, igual que stock-disponible y combos-linea: leer es cargar
    // lineas al movimiento, y no hay una opcion de menu aparte para las etiquetas.

    /// <summary>Las etiquetas RFID leidas de un movimiento (con NUM_SECUENCIA, solo las de esa linea).</summary>
    [HttpGet("etiquetas")]
    public async Task<IActionResult> Etiquetas([FromQuery] decimal NUM_MOVIMIENTO_ALMACEN, [FromQuery] int? NUM_SECUENCIA, [FromQuery] string? Search)
    { var r = await _service.EtiquetasAsync(NUM_MOVIMIENTO_ALMACEN, NUM_SECUENCIA, Search); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Una lectura del lector (RFID o codigo de barras). Rechazada: 400 con el numero del error en
    /// data.errorNumber (50708 = falta el peso; 50706 / 50707 = ya ingreso / ya salio, se puede forzar).</summary>
    [HttpPost("lecturas")]
    public async Task<IActionResult> Leer([FromBody] MovimientosAlmacenLecturaDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.LeerAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    /// <summary>Lo que falta leer o no coincide (vacio = en orden o el tipo no exige lectura).</summary>
    [HttpGet("lecturas-pendientes")]
    public async Task<IActionResult> LecturasPendientes([FromQuery] decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.LecturasPendientesAsync(NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Cambia el peso de una etiqueta ya leida.</summary>
    [HttpPut("etiquetas")]
    public async Task<IActionResult> EtiquetaUpdate([FromBody] MovimientosAlmacenEtiquetaUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.EtiquetaUpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Quita una etiqueta leida. La etiqueta va en la query: es un texto largo y puede traer cualquier caracter.</summary>
    [HttpDelete("etiquetas")]
    public async Task<IActionResult> EtiquetaDelete([FromQuery] decimal NUM_MOVIMIENTO_ALMACEN, [FromQuery] int NUM_SECUENCIA, [FromQuery] string COD_STICKER_RFID)
    { var r = await _service.EtiquetaDeleteAsync(NUM_MOVIMIENTO_ALMACEN, NUM_SECUENCIA, COD_STICKER_RFID, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_MOVIMIENTO_ALMACEN}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> Delete(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_SECUENCIA)
    { var r = await _service.DeleteAsync(NUM_MOVIMIENTO_ALMACEN, NUM_SECUENCIA); return r.Success ? Ok(r) : BadRequest(r); }

}