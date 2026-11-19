using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/comercial/pedidos-comerciales-especificaciones-tecnicas-base")]
[ExigePermiso("COMERCIAL.PEDIDOS_COMERCIALES_ESPECIFICACIONES_TECNICAS_BASE")]
public class PedidosComercialesEspecificacionesTecnicasBaseController : BaseController
{
    private readonly IPedidosComercialesEspecificacionesTecnicasBaseService _service;
    public PedidosComercialesEspecificacionesTecnicasBaseController(IPedidosComercialesEspecificacionesTecnicasBaseService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_PEDIDO_COMERCIAL}/{COD_TIPO_ENTIDAD}/{COD_GRUPO_PRODUCTO}")]
    public async Task<IActionResult> GetById(int NUM_PEDIDO_COMERCIAL, string COD_TIPO_ENTIDAD, string COD_GRUPO_PRODUCTO)
    { var r = await _service.GetByIdAsync(NUM_PEDIDO_COMERCIAL, COD_TIPO_ENTIDAD, COD_GRUPO_PRODUCTO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] PedidosComercialesEspecificacionesTecnicasBaseSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] PedidosComercialesEspecificacionesTecnicasBaseInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{NUM_PEDIDO_COMERCIAL}/{COD_TIPO_ENTIDAD}/{COD_GRUPO_PRODUCTO}")]
    public async Task<IActionResult> Delete(int NUM_PEDIDO_COMERCIAL, string COD_TIPO_ENTIDAD, string COD_GRUPO_PRODUCTO)
    { var r = await _service.DeleteAsync(NUM_PEDIDO_COMERCIAL, COD_TIPO_ENTIDAD, COD_GRUPO_PRODUCTO); return r.Success ? Ok(r) : BadRequest(r); }

}