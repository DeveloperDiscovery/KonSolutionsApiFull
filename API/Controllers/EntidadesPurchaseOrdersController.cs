using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Purchase orders del cliente.
///
/// Todo va por query string y no por segmentos de ruta: NUM_PURCHASE_ORDER es texto libre de
/// hasta 30 caracteres y puede traer barras o puntos, que romperían el ruteo. Además evita la
/// ambigüedad entre rutas de la misma cantidad de segmentos, que ya nos costó un 400 en Pedidos
/// Comerciales.</summary>
[Authorize]
[Route("api/v1/comercial/entidades-purchase-orders")]
[ExigePermiso("COMERCIAL.ENTIDADES_PURCHASE_ORDERS")]
public class EntidadesPurchaseOrdersController : BaseController
{
    private readonly IEntidadesPurchaseOrdersService _service;
    public EntidadesPurchaseOrdersController(IEntidadesPurchaseOrdersService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] decimal codEntidad)
    {
        if (codEntidad <= 0) return BadRequest("Indique el cliente.");
        var r = await _service.GetAllAsync(codEntidad);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] decimal codEntidad)
    { var r = await _service.GetComboboxAsync(codEntidad); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EntidadesPurchaseOrdersSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("byid")]
    public async Task<IActionResult> GetById([FromQuery] decimal codEntidad, [FromQuery] string numPurchaseOrder)
    { var r = await _service.GetByIdAsync(codEntidad, numPurchaseOrder); return r.Success ? Ok(r) : NotFound(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EntidadesPurchaseOrdersInsertDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
        return r.Success ? StatusCode(201, r) : BadRequest(r);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EntidadesPurchaseOrdersUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] decimal codEntidad, [FromQuery] string numPurchaseOrder)
    { var r = await _service.DeleteAsync(codEntidad, numPurchaseOrder); return r.Success ? Ok(r) : BadRequest(r); }
}


/// <summary>Especificaciones de despacho de una purchase order (el embalaje).</summary>
[Authorize]
[Route("api/v1/comercial/entidades-purchase-orders-despacho")]
[ExigePermiso("COMERCIAL.ENTIDADES_PURCHASE_ORDERS")]
public class EntidadesPurchaseOrdersDespachoController : BaseController
{
    private readonly IEntidadesPurchaseOrdersDespachoService _service;
    public EntidadesPurchaseOrdersDespachoController(IEntidadesPurchaseOrdersDespachoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] decimal codEntidad, [FromQuery] string numPurchaseOrder)
    {
        if (codEntidad <= 0 || string.IsNullOrWhiteSpace(numPurchaseOrder))
            return BadRequest("Indique el cliente y la purchase order.");
        var r = await _service.GetAllAsync(codEntidad, numPurchaseOrder);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EntidadesPurchaseOrdersDespachoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("byid")]
    public async Task<IActionResult> GetById([FromQuery] decimal codEntidad, [FromQuery] string numPurchaseOrder,
                                             [FromQuery] int numSecuencia)
    { var r = await _service.GetByIdAsync(codEntidad, numPurchaseOrder, numSecuencia); return r.Success ? Ok(r) : NotFound(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EntidadesPurchaseOrdersDespachoInsertDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
        return r.Success ? StatusCode(201, r) : BadRequest(r);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EntidadesPurchaseOrdersDespachoUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] decimal codEntidad, [FromQuery] string numPurchaseOrder,
                                            [FromQuery] int numSecuencia)
    { var r = await _service.DeleteAsync(codEntidad, numPurchaseOrder, numSecuencia); return r.Success ? Ok(r) : BadRequest(r); }
}


/// <summary>Especificaciones técnicas de un despacho.</summary>
[Authorize]
[Route("api/v1/comercial/entidades-purchase-orders-especificaciones-tecnicas")]
[ExigePermiso("COMERCIAL.ENTIDADES_PURCHASE_ORDERS")]
public class EntidadesPurchaseOrdersEspecificacionTecnicaController : BaseController
{
    private readonly IEntidadesPurchaseOrdersEspecificacionTecnicaService _service;
    public EntidadesPurchaseOrdersEspecificacionTecnicaController(
        IEntidadesPurchaseOrdersEspecificacionTecnicaService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] decimal codEntidad, [FromQuery] string numPurchaseOrder,
                                            [FromQuery] int numSecuencia)
    {
        if (codEntidad <= 0 || string.IsNullOrWhiteSpace(numPurchaseOrder))
            return BadRequest("Indique el cliente y la purchase order.");
        var r = await _service.GetAllAsync(codEntidad, numPurchaseOrder, numSecuencia);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EntidadesPurchaseOrdersEspecificacionTecnicaSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("byid")]
    public async Task<IActionResult> GetById([FromQuery] decimal codEntidad, [FromQuery] string numPurchaseOrder,
                                             [FromQuery] int numSecuencia, [FromQuery] string codEspecificacionTecnica)
    {
        var r = await _service.GetByIdAsync(codEntidad, numPurchaseOrder, numSecuencia, codEspecificacionTecnica);
        return r.Success ? Ok(r) : NotFound(r);
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EntidadesPurchaseOrdersEspecificacionTecnicaInsertDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
        return r.Success ? StatusCode(201, r) : BadRequest(r);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EntidadesPurchaseOrdersEspecificacionTecnicaUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] decimal codEntidad, [FromQuery] string numPurchaseOrder,
                                            [FromQuery] int numSecuencia, [FromQuery] string codEspecificacionTecnica)
    {
        var r = await _service.DeleteAsync(codEntidad, numPurchaseOrder, numSecuencia, codEspecificacionTecnica);
        return r.Success ? Ok(r) : BadRequest(r);
    }
}
