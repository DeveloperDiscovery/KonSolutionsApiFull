using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Tipos de comprobante de pago asociados a un tipo de movimiento.
///
/// El permiso declarado es LOGISTICA.TIPOS_MOVIMIENTO —el de la tabla padre— y no el de la
/// tabla propia: esto no es una opción de menú, se administra desde dentro del diálogo de
/// Tipos de Movimiento, y quien puede tocar el tipo puede tocar sus comprobantes. Declarar
/// LOGISTICA.TIPOS_MOVIMIENTO_TIPOS_COMPROBATE_PAGO dejaría todo denegado hasta registrar esa
/// fila en SEGURIDAD.APLICACIONES_OPCIONES.DES_OBJETO. Si más adelante querés permiso propio,
/// se registra el objeto y se cambia esta línea.</summary>
[Authorize]
[Route("api/v1/logistica/tipos-movimiento-tipos-comprobante-pago")]
[ExigePermiso("LOGISTICA.TIPOS_MOVIMIENTO")]
public class TiposMovimientoTiposComprobantePagoController : BaseController
{
    private readonly ITiposMovimientoTiposComprobantePagoService _service;
    public TiposMovimientoTiposComprobantePagoController(ITiposMovimientoTiposComprobantePagoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codClaseMovimiento, [FromQuery] string codTipoMovimiento)
    { var r = await _service.GetAllAsync(codClaseMovimiento, codTipoMovimiento); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CLASE_MOVIMIENTO}/{COD_TIPO_MOVIMIENTO}/{COD_TIPO_COMPROBANTE_PAGO}")]
    public async Task<IActionResult> GetById(string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO, string COD_TIPO_COMPROBANTE_PAGO)
    { var r = await _service.GetByIdAsync(COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO, COD_TIPO_COMPROBANTE_PAGO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] string codClaseMovimiento, [FromQuery] string codTipoMovimiento)
    { var r = await _service.GetComboboxAsync(codClaseMovimiento, codTipoMovimiento); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string codClaseMovimiento, [FromQuery] string codTipoMovimiento, [FromQuery] TiposMovimientoTiposComprobantePagoSearchDto dto)
    { var r = await _service.SearchAsync(codClaseMovimiento, codTipoMovimiento, dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposMovimientoTiposComprobantePagoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    /// <summary>El SP de alta rechaza una clave que ya existe, así que la modificación va por
    /// su propio verbo. Sin esto, cambiar el control de correlativo de una fila ya asociada
    /// devolvería "Ya existe un registro con esa clave".</summary>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TiposMovimientoTiposComprobantePagoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_CLASE_MOVIMIENTO}/{COD_TIPO_MOVIMIENTO}/{COD_TIPO_COMPROBANTE_PAGO}")]
    public async Task<IActionResult> Delete(string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO, string COD_TIPO_COMPROBANTE_PAGO)
    { var r = await _service.DeleteAsync(COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO, COD_TIPO_COMPROBANTE_PAGO); return r.Success ? Ok(r) : BadRequest(r); }
}
