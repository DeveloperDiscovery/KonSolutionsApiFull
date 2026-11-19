using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/planeamiento/grupos-requerimiento-pedidos")]
[ExigePermiso("PLANEAMIENTO.GRUPOS_REQUERIMIENTO_PEDIDOS")]
public class GruposRequerimientoPedidosController : BaseController
{
    private readonly IGruposRequerimientoPedidosService _service;
    public GruposRequerimientoPedidosController(IGruposRequerimientoPedidosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_GRUPO_PLANEAMIENTO}/{COD_GRUPO_REQUERIMIENTO}/{NUM_PEDIDO_COMERCIAL}")]
    public async Task<IActionResult> GetById(int COD_GRUPO_PLANEAMIENTO, int COD_GRUPO_REQUERIMIENTO, int NUM_PEDIDO_COMERCIAL)
    { var r = await _service.GetByIdAsync(COD_GRUPO_PLANEAMIENTO, COD_GRUPO_REQUERIMIENTO, NUM_PEDIDO_COMERCIAL); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] GruposRequerimientoPedidosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] GruposRequerimientoPedidosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] GruposRequerimientoPedidosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_GRUPO_PLANEAMIENTO}/{COD_GRUPO_REQUERIMIENTO}/{NUM_PEDIDO_COMERCIAL}")]
    public async Task<IActionResult> Delete(int COD_GRUPO_PLANEAMIENTO, int COD_GRUPO_REQUERIMIENTO, int NUM_PEDIDO_COMERCIAL)
    { var r = await _service.DeleteAsync(COD_GRUPO_PLANEAMIENTO, COD_GRUPO_REQUERIMIENTO, NUM_PEDIDO_COMERCIAL); return r.Success ? Ok(r) : BadRequest(r); }

}