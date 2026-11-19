using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

/// <summary>La guía de remisión electrónica: lecturas, el validador previo y la emisión (emitir, consultar y reintentar).
/// Las piezas sueltas de la emisión (crear, registrar XML, envío o respuesta) no se exponen: las llama la emisión misma,
/// de corrido, para que nadie deje una guía a medias.
///
/// PERMISOS. Hay dos puertas y cada una tiene su objeto:
///   · LOGISTICA.GUIAS_REMISION (la bandeja de guías): el listado y, pidiendo la guía por su número, la ficha,
///     el historial de envíos, el XML y la constancia (CDR). Quien tiene la bandeja puede recorrer todas las guías.
///   · LOGISTICA.MOVIMIENTOS_ALMACEN (la pestaña "Guía de remisión" del movimiento): lo mismo pero pidiendo la
///     guía por su movimiento (`por-movimiento/{n}`), más la revisión previa (`validar/{n}`). Quien ve el movimiento
///     ve la guía de ESE movimiento, no las demás: por eso estas rutas no cuelgan de la bandeja.</summary>
[Authorize]
[Route("api/v1/logistica/guias-remision")]
[ExigePermiso("LOGISTICA.GUIAS_REMISION")]
public class GuiasRemisionController : BaseController
{
    private readonly IGuiasRemisionService _service;
    private readonly IGuiasRemisionEmisionService _emision;
    public GuiasRemisionController(IGuiasRemisionService service, IGuiasRemisionEmisionService emision) { _service = service; _emision = emision; }

    // ── La bandeja: por el número de la guía ─────────────────────────────────────────────────

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GuiaRemisionFiltroDto filtro)
    { var r = await _service.GetAllAsync(filtro); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_GUIA_REMISION:int}")]
    public async Task<IActionResult> GetById(int NUM_GUIA_REMISION)
    { var r = await _service.GetByIdAsync(NUM_GUIA_REMISION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("{NUM_GUIA_REMISION:int}/envios")]
    public async Task<IActionResult> GetEnvios(int NUM_GUIA_REMISION)
    { var r = await _service.GetEnviosAsync(NUM_GUIA_REMISION); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_GUIA_REMISION:int}/xml")]
    public async Task<IActionResult> GetXml(int NUM_GUIA_REMISION)
    { var r = await _service.GetXmlAsync(NUM_GUIA_REMISION);
      return r.Success ? File(r.Data!.Contenido!, "application/xml", $"{r.Data.NUM_GUIA}.xml") : NotFound(r); }

    [HttpGet("{NUM_GUIA_REMISION:int}/cdr")]
    public async Task<IActionResult> GetCdr(int NUM_GUIA_REMISION)
    { var r = await _service.GetCdrAsync(NUM_GUIA_REMISION);
      return r.Success ? File(r.Data!.Contenido!, "application/zip", $"R-{r.Data.NUM_GUIA}.zip") : NotFound(r); }

    // ── La pestaña del movimiento: por el número del movimiento ──────────────────────────────

    [HttpGet("por-movimiento/{NUM_MOVIMIENTO_ALMACEN}")]
    [ExigePermiso("LOGISTICA.MOVIMIENTOS_ALMACEN")]
    public async Task<IActionResult> GetByMovimiento(decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.GetByMovimientoAsync(NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("por-movimiento/{NUM_MOVIMIENTO_ALMACEN}/envios")]
    [ExigePermiso("LOGISTICA.MOVIMIENTOS_ALMACEN")]
    public async Task<IActionResult> GetEnviosPorMovimiento(decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.GetEnviosPorMovimientoAsync(NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("por-movimiento/{NUM_MOVIMIENTO_ALMACEN}/xml")]
    [ExigePermiso("LOGISTICA.MOVIMIENTOS_ALMACEN")]
    public async Task<IActionResult> GetXmlPorMovimiento(decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.GetXmlPorMovimientoAsync(NUM_MOVIMIENTO_ALMACEN);
      return r.Success ? File(r.Data!.Contenido!, "application/xml", $"{r.Data.NUM_GUIA}.xml") : NotFound(r); }

    [HttpGet("por-movimiento/{NUM_MOVIMIENTO_ALMACEN}/cdr")]
    [ExigePermiso("LOGISTICA.MOVIMIENTOS_ALMACEN")]
    public async Task<IActionResult> GetCdrPorMovimiento(decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.GetCdrPorMovimientoAsync(NUM_MOVIMIENTO_ALMACEN);
      return r.Success ? File(r.Data!.Contenido!, "application/zip", $"R-{r.Data.NUM_GUIA}.zip") : NotFound(r); }

    [HttpGet("validar/{NUM_MOVIMIENTO_ALMACEN}")]
    [ExigePermiso("LOGISTICA.MOVIMIENTOS_ALMACEN")]
    public async Task<IActionResult> Validar(decimal NUM_MOVIMIENTO_ALMACEN, [FromQuery] bool m1l = false)
    { var r = await _service.ValidarAsync(NUM_MOVIMIENTO_ALMACEN, m1l); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>El chequeo de catálogos: lo que les falta (códigos SUNAT, ubigeos, RUC) a los registros que usan las salidas. Vive en la pantalla de
    /// Configuración de emisión electrónica, por eso va por su objeto.</summary>
    [HttpGet("catalogos-pendientes")]
    [ExigePermiso("ESTANDAR.CONFIGURACION_EMISION_ELECTRONICA")]
    public async Task<IActionResult> GetCatalogosPendientes()
    { var r = await _service.GetCatalogosPendientesAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>¿Se puede modificar el movimiento, o su guía ya está ENVIADA o ACEPTADA? Siempre contesta 200: «no tiene guía» es «no está bloqueado».</summary>
    [HttpGet("por-movimiento/{NUM_MOVIMIENTO_ALMACEN}/bloqueo")]
    [ExigePermiso("LOGISTICA.MOVIMIENTOS_ALMACEN")]
    public async Task<IActionResult> GetBloqueoPorMovimiento(decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.GetBloqueoPorMovimientoAsync(NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    // ── La emisión ───────────────────────────────────────────────────────────────────────────
    // Una respuesta de SUNAT —aceptada, rechazada o en proceso— es un resultado y vuelve con 200; solo el no haber podido emitir es un 400.
    // Se pasa el token de la petición: si el usuario cierra la pantalla mientras se espera a SUNAT, la guía queda ENVIADA y se consulta después.

    [HttpPost("emitir")]
    [ExigePermiso("LOGISTICA.MOVIMIENTOS_ALMACEN")]
    public async Task<IActionResult> Emitir([FromBody] GuiaRemisionEmitirDto dto, CancellationToken ct)
    { var r = await _emision.EmitirAsync(dto, GetCodUsuario(), GetEstacion(), ct); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("por-movimiento/{NUM_MOVIMIENTO_ALMACEN}/consultar")]
    [ExigePermiso("LOGISTICA.MOVIMIENTOS_ALMACEN")]
    public async Task<IActionResult> ConsultarPorMovimiento(decimal NUM_MOVIMIENTO_ALMACEN, CancellationToken ct)
    { var r = await _emision.ConsultarPorMovimientoAsync(NUM_MOVIMIENTO_ALMACEN, GetCodUsuario(), GetEstacion(), ct); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("por-movimiento/{NUM_MOVIMIENTO_ALMACEN}/reintentar")]
    [ExigePermiso("LOGISTICA.MOVIMIENTOS_ALMACEN")]
    public async Task<IActionResult> ReintentarPorMovimiento(decimal NUM_MOVIMIENTO_ALMACEN, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] GuiaRemisionReintentarEmisionDto? dto, CancellationToken ct)
    { var r = await _emision.ReintentarPorMovimientoAsync(NUM_MOVIMIENTO_ALMACEN, dto?.OPCIONES, GetCodUsuario(), GetEstacion(), ct); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("por-movimiento/{NUM_MOVIMIENTO_ALMACEN}/opciones")]
    [ExigePermiso("LOGISTICA.MOVIMIENTOS_ALMACEN")]
    public async Task<IActionResult> GetOpcionesPorMovimiento(decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _emision.GetOpcionesPorMovimientoAsync(NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : NotFound(r); }

    [HttpPost("{NUM_GUIA_REMISION:int}/consultar")]
    public async Task<IActionResult> Consultar(int NUM_GUIA_REMISION, CancellationToken ct)
    { var r = await _emision.ConsultarAsync(NUM_GUIA_REMISION, GetCodUsuario(), GetEstacion(), ct); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("{NUM_GUIA_REMISION:int}/reintentar")]
    public async Task<IActionResult> Reintentar(int NUM_GUIA_REMISION, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] GuiaRemisionReintentarEmisionDto? dto, CancellationToken ct)
    { var r = await _emision.ReintentarAsync(NUM_GUIA_REMISION, dto?.OPCIONES, GetCodUsuario(), GetEstacion(), ct); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_GUIA_REMISION:int}/opciones")]
    public async Task<IActionResult> GetOpciones(int NUM_GUIA_REMISION)
    { var r = await _emision.GetOpcionesAsync(NUM_GUIA_REMISION); return r.Success ? Ok(r) : NotFound(r); }
}
