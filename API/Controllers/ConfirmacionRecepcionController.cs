using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Confirmación de recepción de transferencias: el almacén destino recibe (o rechaza)
/// las salidas que le enviaron.
///
/// Va con el permiso de movimientos de almacén: confirmar crea el ingreso en el destino y
/// rechazar crea el ingreso por anulación en el origen, los dos son movimientos.
///
/// El rol llega en la consulta y en el cuerpo, y no se toma del token: el token trae el rol con
/// el que se inició sesión, y el usuario puede haber elegido otro en la barra superior. El SP
/// valida que el usuario del token pertenezca a ese rol.</summary>
[Authorize]
[Route("api/v1/logistica/confirmacion-recepcion")]
[ExigePermiso("LOGISTICA.MOVIMIENTOS_ALMACEN")]
public class ConfirmacionRecepcionController : BaseController
{
    private readonly IConfirmacionRecepcionService _service;
    public ConfirmacionRecepcionController(IConfirmacionRecepcionService service) => _service = service;

    /// <summary>Las salidas por recibir en los almacenes del rol (o en uno de ellos).</summary>
    [HttpGet("pendientes")]
    public async Task<IActionResult> Pendientes([FromQuery] string? COD_ROL_USUARIO, [FromQuery] string? COD_ALMACEN)
    { var r = await _service.PendientesAsync(COD_ROL_USUARIO, GetCodUsuario(), COD_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Las líneas de una salida.</summary>
    [HttpGet("{NUM_MOVIMIENTO_ALMACEN}/detalle")]
    public async Task<IActionResult> Detalle(decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.DetalleAsync(NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>El estado RECHAZADO de la salida, su tipo de motivo y los motivos.</summary>
    [HttpGet("{NUM_MOVIMIENTO_ALMACEN}/motivos-rechazo")]
    public async Task<IActionResult> MotivosRechazo(decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.MotivosRechazoAsync(NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Por línea de la salida: si su producto se lee por RFID, lo enviado y lo etiquetado en la salida, más el requisito
    /// de lectura (N / O / R) del tipo de ingreso que se generará.</summary>
    [HttpGet("{NUM_MOVIMIENTO_ALMACEN}/lectura")]
    public async Task<IActionResult> LecturaContexto(decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.LecturaContextoAsync(NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Las etiquetas que leyó la salida (con NUM_SECUENCIA, solo las de esa línea): lo que pasa al ingreso si se confirma sin leer.</summary>
    [HttpGet("{NUM_MOVIMIENTO_ALMACEN}/etiquetas")]
    public async Task<IActionResult> Etiquetas(decimal NUM_MOVIMIENTO_ALMACEN, [FromQuery] int? NUM_SECUENCIA)
    { var r = await _service.EtiquetasAsync(NUM_MOVIMIENTO_ALMACEN, NUM_SECUENCIA); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Revisa una etiqueta contra la salida SIN grabar (el ingreso todavía no existe). Rechazada: 400 con el número
    /// del error en data.errorNumber (50708 falta el peso; 50706 ya ingresó, se puede forzar; 50731 no es de la salida;
    /// 50732 sobra).</summary>
    [HttpPost("lectura")]
    public async Task<IActionResult> Leer([FromBody] RecepcionLecturaDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.LeerAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Confirma: crea el ingreso en el almacén destino y pasa la salida y el ingreso a CONFIRMADO.</summary>
    [HttpPost("confirmar")]
    public async Task<IActionResult> Confirmar([FromBody] RecepcionConfirmarDto dto)
    { var r = await _service.ConfirmarAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Rechaza: devuelve el stock al origen con un ingreso por anulación de salida y pasa la salida y ese
    /// ingreso a RECHAZADO, con el motivo.</summary>
    [HttpPost("rechazar")]
    public async Task<IActionResult> Rechazar([FromBody] RecepcionRechazarDto dto)
    { var r = await _service.RechazarAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }
}
