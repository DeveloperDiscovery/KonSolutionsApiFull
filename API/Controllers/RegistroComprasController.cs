using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Registro de Compras (Comercial/RegistroCompras): los comprobantes de pago de los proveedores (FINANZAS.COMPROBANTES_PAGO y _DETALLE).
/// Se asocian a una o más órdenes de compra (bienes) o de servicio (servicios) y se validan contra ellas: recepción, cantidades, precios, importes, duplicidad y condiciones.
/// La clase de operación sale del parámetro de la opción de menú («COD_CLASE_OPERACION = 'CMPRS'») y llega siempre como codClaseOperacion: no se escribe en el código.
///
/// Permisos (por verbo): GET consulta; POST crea, asocia órdenes, guarda líneas, vincula e importa XML; PUT modifica, valida, envía a aprobación, aprueba, contabiliza y anula; DELETE borra.
/// Los botones Validar, Aprobar, Contabilizar, Anular e Importar XML (script 40) se habilitan por rol aparte; el estado del comprobante decide además qué se puede hacer.</summary>
[Authorize]
[Route("api/v1/comercial/registro-compras")]
[ExigePermiso("COMERCIAL.REGISTRO_COMPRAS")]
public class RegistroComprasController : BaseController
{
    private readonly IRegistroComprasService _service;
    public RegistroComprasController(IRegistroComprasService service) => _service = service;

    // ───────── cabecera ─────────

    /// <summary>Lista los comprobantes de la clase (obligatoria) con los filtros opcionales.</summary>
    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] RegistroCompraSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_REGISTRO:decimal}")]
    public async Task<IActionResult> GetById(decimal NUM_REGISTRO)
    { var r = await _service.GetByIdAsync(NUM_REGISTRO); return r.Success ? Ok(r) : NotFound(r); }

    /// <summary>Tipos de operación (con lo que habilitan), tipos de comprobante, monedas, formas y condiciones de pago, incoterms y proyectos, en un solo viaje.</summary>
    [HttpGet("loadcombos")]
    public async Task<IActionResult> Loadcombos([FromQuery] string codClaseOperacion)
    { var r = await _service.LoadcombosAsync(codClaseOperacion); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] RegistroCompraInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] RegistroCompraUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_REGISTRO:decimal}")]
    public async Task<IActionResult> Delete(decimal NUM_REGISTRO)
    { var r = await _service.DeleteAsync(NUM_REGISTRO); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>El detalle de los valores totales del documento (Concepto / Tasa / Valor: base, impuestos, importe total…), que el estándar deja en FINANZAS.COMPROBANTES_PAGO_CALCULO al registrar las líneas.</summary>
    [HttpGet("{NUM_REGISTRO:decimal}/calculo")]
    public async Task<IActionResult> Calculo(decimal NUM_REGISTRO)
    { var r = await _service.CalculoAsync(NUM_REGISTRO); return r.Success ? Ok(r) : BadRequest(r); }

    // ───────── líneas ─────────

    [HttpGet("{NUM_REGISTRO:decimal}/lineas")]
    public async Task<IActionResult> Lineas(decimal NUM_REGISTRO)
    { var r = await _service.LineasAsync(NUM_REGISTRO); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Alta de una línea (sin NUM_SECUENCIA) o modificación de la que se indique.</summary>
    [HttpPost("{NUM_REGISTRO:decimal}/lineas")]
    public async Task<IActionResult> LineaGuardar(decimal NUM_REGISTRO, [FromBody] RegistroCompraLineaDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.LineaGuardarAsync(NUM_REGISTRO, dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{NUM_REGISTRO:decimal}/lineas/{NUM_SECUENCIA:int}")]
    public async Task<IActionResult> LineaDelete(decimal NUM_REGISTRO, int NUM_SECUENCIA)
    { var r = await _service.LineaDeleteAsync(NUM_REGISTRO, NUM_SECUENCIA); return r.Success ? Ok(r) : BadRequest(r); }

    // ───────── órdenes ─────────

    /// <summary>Las órdenes ya asociadas al comprobante.</summary>
    [HttpGet("{NUM_REGISTRO:decimal}/pedidos")]
    public async Task<IActionResult> Pedidos(decimal NUM_REGISTRO)
    { var r = await _service.PedidosAsync(NUM_REGISTRO); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Las órdenes que se pueden asociar: del mismo proveedor, aprobadas, y de bienes si el comprobante es de bienes o de servicios si es de servicios.</summary>
    [HttpGet("{NUM_REGISTRO:decimal}/pedidos/disponibles")]
    public async Task<IActionResult> PedidosDisponibles(decimal NUM_REGISTRO)
    { var r = await _service.PedidosDisponiblesAsync(NUM_REGISTRO); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Las líneas de las órdenes asociadas, con lo que falta facturar de cada una.</summary>
    [HttpGet("{NUM_REGISTRO:decimal}/pedidos/lineas")]
    public async Task<IActionResult> PedidosLineas(decimal NUM_REGISTRO)
    { var r = await _service.PedidosLineasAsync(NUM_REGISTRO); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("{NUM_REGISTRO:decimal}/pedidos")]
    public async Task<IActionResult> PedidoAsociar(decimal NUM_REGISTRO, [FromBody] RegistroCompraPedidoAsociarDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.PedidoAsociarAsync(NUM_REGISTRO, dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{NUM_REGISTRO:decimal}/pedidos/{codClaseOperacion}/{codTipoOperacion}/{numPedidoComercial:decimal}")]
    public async Task<IActionResult> PedidoDesasociar(decimal NUM_REGISTRO, string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    { var r = await _service.PedidoDesasociarAsync(NUM_REGISTRO, codClaseOperacion, codTipoOperacion, numPedidoComercial); return r.Success ? Ok(r) : BadRequest(r); }

    // ───────── notas de crédito y débito por ítem (tipos de operación con FLG_INCIDENCIA_PRODUCTO) ─────────

    /// <summary>Las facturas del mismo proveedor que la nota puede afectar.</summary>
    [HttpGet("{NUM_REGISTRO:decimal}/incidencias/origenes")]
    public async Task<IActionResult> IncidenciasOrigenes(decimal NUM_REGISTRO, [FromQuery] string? search)
    { var r = await _service.IncidenciasOrigenesAsync(NUM_REGISTRO, search); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Las líneas de la factura elegida con su saldo y lo que la nota ya aplica.</summary>
    [HttpGet("{NUM_REGISTRO:decimal}/incidencias/disponibles")]
    public async Task<IActionResult> IncidenciasDisponibles(decimal NUM_REGISTRO, [FromQuery] decimal origen)
    { var r = await _service.IncidenciasDisponiblesAsync(NUM_REGISTRO, origen); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Lo que la nota ya tiene asociado.</summary>
    [HttpGet("{NUM_REGISTRO:decimal}/incidencias")]
    public async Task<IActionResult> Incidencias(decimal NUM_REGISTRO)
    { var r = await _service.IncidenciasAsync(NUM_REGISTRO); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Reemplaza la asociación de la nota (línea de la nota ↔ línea de la factura, con cantidad e importe).</summary>
    [HttpPut("{NUM_REGISTRO:decimal}/incidencias")]
    public async Task<IActionResult> IncidenciasGuardar(decimal NUM_REGISTRO, [FromBody] RegistroCompraIncidenciasGuardarDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.IncidenciasGuardarAsync(NUM_REGISTRO, dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Anulación total: copia todas las líneas de la factura a la nota y las asocia completas.</summary>
    [HttpPost("{NUM_REGISTRO:decimal}/incidencias/anulacion-total")]
    public async Task<IActionResult> IncidenciasAnulacionTotal(decimal NUM_REGISTRO, [FromBody] RegistroCompraIncidenciaAnulacionDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.IncidenciasAnulacionTotalAsync(NUM_REGISTRO, dto.NUM_REGISTRO_ORIGEN!.Value, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    /// <summary>Adiciona al comprobante (nota) las líneas elegidas en la grilla de la factura y las asocia: devolución = cantidad devuelta; diferencia de precio = importe de la diferencia.</summary>
    [HttpPost("{NUM_REGISTRO:decimal}/incidencias/adicionar")]
    public async Task<IActionResult> IncidenciasAdicionar(decimal NUM_REGISTRO, [FromBody] RegistroCompraIncidenciaAdicionarDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.IncidenciasAdicionarAsync(NUM_REGISTRO, dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    // ---- Anticipos: facturas de anticipo (FLG_ANTICIPO) que se aplican por su monto total a la factura ----
    [HttpGet("{NUM_REGISTRO:decimal}/anticipos/disponibles")]
    public async Task<IActionResult> AnticiposDisponibles(decimal NUM_REGISTRO)
    { var r = await _service.AnticiposDisponiblesAsync(NUM_REGISTRO); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_REGISTRO:decimal}/anticipos")]
    public async Task<IActionResult> Anticipos(decimal NUM_REGISTRO)
    { var r = await _service.AnticiposAsync(NUM_REGISTRO); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("{NUM_REGISTRO:decimal}/anticipos/{NUM_REGISTRO_ANTICIPO:decimal}")]
    public async Task<IActionResult> AnticipoAsociar(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ANTICIPO)
    { var r = await _service.AnticipoAsociarAsync(NUM_REGISTRO, NUM_REGISTRO_ANTICIPO, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_REGISTRO:decimal}/anticipos/{NUM_REGISTRO_ANTICIPO:decimal}")]
    public async Task<IActionResult> AnticipoQuitar(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ANTICIPO)
    { var r = await _service.AnticipoQuitarAsync(NUM_REGISTRO, NUM_REGISTRO_ANTICIPO); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Guías de devolución (movimientos de almacén de devolución al proveedor) que se pueden adjuntar.</summary>
    [HttpGet("{NUM_REGISTRO:decimal}/incidencias/guias/disponibles")]
    public async Task<IActionResult> IncidenciasGuiasDisponibles(decimal NUM_REGISTRO)
    { var r = await _service.IncidenciasGuiasDisponiblesAsync(NUM_REGISTRO); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Guías de devolución ya adjuntas.</summary>
    [HttpGet("{NUM_REGISTRO:decimal}/incidencias/guias")]
    public async Task<IActionResult> IncidenciasGuias(decimal NUM_REGISTRO)
    { var r = await _service.IncidenciasGuiasAsync(NUM_REGISTRO); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Datos de una línea de la nota para editarla: la línea de la factura afectada y la cantidad del proveedor según la guía.</summary>
    [HttpGet("{NUM_REGISTRO:decimal}/incidencias/lineas/{NUM_SECUENCIA:int}/guia")]
    public async Task<IActionResult> IncidenciasLineaGuia(decimal NUM_REGISTRO, int NUM_SECUENCIA)
    { var r = await _service.IncidenciasLineaGuiaAsync(NUM_REGISTRO, NUM_SECUENCIA); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("{NUM_REGISTRO:decimal}/incidencias/guias/{NUM_MOVIMIENTO_ALMACEN:decimal}")]
    public async Task<IActionResult> IncidenciasGuiaAsociar(decimal NUM_REGISTRO, decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.IncidenciasGuiaAsociarAsync(NUM_REGISTRO, NUM_MOVIMIENTO_ALMACEN, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_REGISTRO:decimal}/incidencias/guias/{NUM_MOVIMIENTO_ALMACEN:decimal}")]
    public async Task<IActionResult> IncidenciasGuiaQuitar(decimal NUM_REGISTRO, decimal NUM_MOVIMIENTO_ALMACEN)
    { var r = await _service.IncidenciasGuiaQuitarAsync(NUM_REGISTRO, NUM_MOVIMIENTO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Vincula solas las líneas que no tienen orden, por producto (o por el código del proveedor), a la línea de las órdenes asociadas con más pendiente.</summary>
    [HttpPost("{NUM_REGISTRO:decimal}/autovincular")]
    public async Task<IActionResult> Autovincular(decimal NUM_REGISTRO)
    { var r = await _service.AutovincularAsync(NUM_REGISTRO); return r.Success ? Ok(r) : BadRequest(r); }

    // ───────── conciliación y flujo ─────────

    /// <summary>Lo que encontró la última validación, paso por paso.</summary>
    [HttpGet("{NUM_REGISTRO:decimal}/conciliacion")]
    public async Task<IActionResult> Conciliacion(decimal NUM_REGISTRO)
    { var r = await _service.ConciliacionAsync(NUM_REGISTRO); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Valida el comprobante contra sus órdenes y devuelve el estado en que quedó y los hallazgos.</summary>
    [HttpPut("{NUM_REGISTRO:decimal}/validar")]
    public async Task<IActionResult> Validar(decimal NUM_REGISTRO)
    { var r = await _service.ValidarAsync(NUM_REGISTRO, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPut("{NUM_REGISTRO:decimal}/enviar-aprobacion")]
    public async Task<IActionResult> EnviarAprobacion(decimal NUM_REGISTRO, [FromBody] RegistroCompraJustificacionDto dto)
    { var r = await _service.EnviarAprobacionAsync(NUM_REGISTRO, dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPut("{NUM_REGISTRO:decimal}/aprobar")]
    public async Task<IActionResult> Aprobar(decimal NUM_REGISTRO, [FromBody] RegistroCompraJustificacionDto dto)
    { var r = await _service.AprobarAsync(NUM_REGISTRO, dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Deja el comprobante contabilizado y su deuda en el cronograma. No genera el asiento contable.</summary>
    [HttpPut("{NUM_REGISTRO:decimal}/contabilizar")]
    public async Task<IActionResult> Contabilizar(decimal NUM_REGISTRO)
    { var r = await _service.ContabilizarAsync(NUM_REGISTRO, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPut("{NUM_REGISTRO:decimal}/anular")]
    public async Task<IActionResult> Anular(decimal NUM_REGISTRO, [FromBody] RegistroCompraJustificacionDto dto)
    { var r = await _service.AnularAsync(NUM_REGISTRO, dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    // ───────── PDF del comprobante ─────────

    /// <summary>Guarda el PDF del comprobante (multipart, campo «archivo»; máximo 20 MB). Reemplaza el anterior. El nombre se graba con la cabecera.</summary>
    [HttpPost("{NUM_REGISTRO:decimal}/pdf")]
    [RequestSizeLimit(20_000_000)]
    public async Task<IActionResult> PdfSubir(decimal NUM_REGISTRO, IFormFile archivo)
    {
        if (archivo is null) return BadRequest(ApiResponse<bool>.Fail("No se recibió ningún archivo."));
        await using var s = archivo.OpenReadStream();
        var r = await _service.PdfGuardarAsync(NUM_REGISTRO, archivo.FileName, s, archivo.Length);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    /// <summary>El PDF guardado, listo para mostrarse (application/pdf).</summary>
    [HttpGet("{NUM_REGISTRO:decimal}/pdf")]
    public async Task<IActionResult> PdfVer(decimal NUM_REGISTRO)
    {
        var r = await _service.PdfLeerAsync(NUM_REGISTRO);
        return r.Success && r.Data is not null ? File(r.Data.CONTENIDO, "application/pdf") : NotFound(r);
    }

    [HttpDelete("{NUM_REGISTRO:decimal}/pdf")]
    public async Task<IActionResult> PdfQuitar(decimal NUM_REGISTRO)
    { var r = await _service.PdfQuitarAsync(NUM_REGISTRO); return r.Success ? Ok(r) : BadRequest(r); }

    // ───────── XML ─────────

    /// <summary>El XML original con el que se importó el comprobante.</summary>
    [HttpGet("{NUM_REGISTRO:decimal}/xml")]
    public async Task<IActionResult> Xml(decimal NUM_REGISTRO)
    { var r = await _service.XmlAsync(NUM_REGISTRO); return r.Success ? Ok(r) : NotFound(r); }

    /// <summary>Importa un XML (factura o boleta electrónica) que llega en el cuerpo.</summary>
    [HttpPost("importar-xml")]
    public async Task<IActionResult> ImportarXml([FromBody] RegistroCompraImportarXmlDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.ImportarXmlAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    /// <summary>Importa los XML de la carpeta configurada en Configuración general, pestaña Compras: los que entran pasan a «procesados» y los que no, a «errores».</summary>
    [HttpPost("importar-carpeta")]
    public async Task<IActionResult> ImportarCarpeta([FromBody] RegistroCompraImportarCarpetaDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.ImportarCarpetaAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }
}
