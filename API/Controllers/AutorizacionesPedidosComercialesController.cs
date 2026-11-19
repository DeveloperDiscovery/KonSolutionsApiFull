using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/autorizaciones-pedidos-comerciales")]
[ExigePermiso("COMERCIAL.AUTORIZACIONES_PEDIDOS_COMERCIALES")]
public class AutorizacionesPedidosComercialesController : BaseController
{
    private readonly IAutorizacionesPedidosComercialesService _service;
    public AutorizacionesPedidosComercialesController(IAutorizacionesPedidosComercialesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CLASE_OPERACION}/{COD_TIPO_OPERACION}/{COD_ENTIDAD_AUTORIZADA}")]
    public async Task<IActionResult> GetById(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, decimal COD_ENTIDAD_AUTORIZADA)
    { var r = await _service.GetByIdAsync(COD_CLASE_OPERACION, COD_TIPO_OPERACION, COD_ENTIDAD_AUTORIZADA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] AutorizacionesPedidosComercialesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] AutorizacionesPedidosComercialesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] AutorizacionesPedidosComercialesUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_CLASE_OPERACION}/{COD_TIPO_OPERACION}/{COD_ENTIDAD_AUTORIZADA}")]
    public async Task<IActionResult> Delete(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, decimal COD_ENTIDAD_AUTORIZADA)
    { var r = await _service.DeleteAsync(COD_CLASE_OPERACION, COD_TIPO_OPERACION, COD_ENTIDAD_AUTORIZADA); return r.Success ? Ok(r) : BadRequest(r); }

}