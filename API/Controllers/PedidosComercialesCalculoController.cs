using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/pedidos-comerciales-calculo")]
[ExigePermiso("COMERCIAL.PEDIDOS_COMERCIALES_CALCULO")]
public class PedidosComercialesCalculoController : BaseController
{
    private readonly IPedidosComercialesCalculoService _service;
    public PedidosComercialesCalculoController(IPedidosComercialesCalculoService service) => _service = service;

    // El SHOWALL es por cabecera: PROC_..._CALCULO_SHOWALL exige clase, tipo y número de
    // pedido. Se exponen las dos formas por consistencia con PedidosComercialesDetalle:
    // query para llamadas sueltas, ruta para las pantallas.
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codClaseOperacion, [FromQuery] string codTipoOperacion, [FromQuery] decimal numPedidoComercial)
    { var r = await _service.GetAllAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("por-cabecera/{codClaseOperacion}/{codTipoOperacion}/{numPedidoComercial}")]
    public async Task<IActionResult> GetPorCabecera(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    { var r = await _service.GetAllAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_PEDIDO_COMERCIAL}/{COD_TIPO_VALOR_CALCULO}")]
    public async Task<IActionResult> GetById(int NUM_PEDIDO_COMERCIAL, string COD_TIPO_VALOR_CALCULO)
    { var r = await _service.GetByIdAsync(NUM_PEDIDO_COMERCIAL, COD_TIPO_VALOR_CALCULO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] PedidosComercialesCalculoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] PedidosComercialesCalculoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] PedidosComercialesCalculoUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Dispara el recálculo. Es POST y no GET a propósito: PROC_..._CALCULA borra y
    /// reescribe PEDIDOS_COMERCIALES_CALCULO y actualiza VAL_TOTAL de la cabecera, así que
    /// escribe. Un GET que modifica datos lo reintenta cualquier proxy o botón de recargar.</summary>
    [HttpPost("calcular/{codClaseOperacion}/{codTipoOperacion}/{numPedidoComercial}")]
    public async Task<IActionResult> Calcular(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    { var r = await _service.CalcularAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Convierte el pedido elegido en el comparativo en un pedido de la clase/tipo
    /// "sincronizada" (ej. Proforma → Orden de Compra). Escribe — crea un pedido nuevo —, por
    /// eso POST y no GET.</summary>
    [HttpPost("sincronizar/{codClaseOperacion}/{codTipoOperacion}/{numPedidoComercial}")]
    public async Task<IActionResult> Sincronizar(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    { var r = await _service.SincronizarAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Revierte la conversión hecha por Sincronizar. Recibe la clave de la ORDEN
    /// DE COMPRA (el pedido que creó Sincronizar), no la de la proforma.</summary>
    [HttpPost("revertir/{codClaseOperacion}/{codTipoOperacion}/{numPedidoComercial}")]
    public async Task<IActionResult> Revertir(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    { var r = await _service.RevertirAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Misma reversión, pero recibe la clave de la PROFORMA.</summary>
    [HttpPost("revertir-proforma/{codClaseOperacion}/{codTipoOperacion}/{numPedidoComercial}")]
    public async Task<IActionResult> RevertirProforma(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    { var r = await _service.RevertirProformaAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_PEDIDO_COMERCIAL}/{COD_TIPO_VALOR_CALCULO}")]
    public async Task<IActionResult> Delete(int NUM_PEDIDO_COMERCIAL, string COD_TIPO_VALOR_CALCULO)
    { var r = await _service.DeleteAsync(NUM_PEDIDO_COMERCIAL, COD_TIPO_VALOR_CALCULO); return r.Success ? Ok(r) : BadRequest(r); }

}