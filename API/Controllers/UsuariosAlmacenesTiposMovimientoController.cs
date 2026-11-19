using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/seguridad/usuarios-almacenes-tipos-movimiento")]
[ExigePermiso("SEGURIDAD.USUARIOS_ALMACENES_TIPOS_MOVIMIENTO")]
public class UsuariosAlmacenesTiposMovimientoController : BaseController
{
    private readonly IUsuariosAlmacenesTiposMovimientoService _service;
    public UsuariosAlmacenesTiposMovimientoController(IUsuariosAlmacenesTiposMovimientoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_USUARIO}/{COD_ALMACEN}/{COD_CLASE_MOVIMIENTO}")]
    public async Task<IActionResult> GetById(string COD_USUARIO, string COD_ALMACEN, string COD_CLASE_MOVIMIENTO)
    { var r = await _service.GetByIdAsync(COD_USUARIO, COD_ALMACEN, COD_CLASE_MOVIMIENTO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] UsuariosAlmacenesTiposMovimientoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] UsuariosAlmacenesTiposMovimientoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_USUARIO}/{COD_ALMACEN}/{COD_CLASE_MOVIMIENTO}")]
    public async Task<IActionResult> Delete(string COD_USUARIO, string COD_ALMACEN, string COD_CLASE_MOVIMIENTO)
    { var r = await _service.DeleteAsync(COD_USUARIO, COD_ALMACEN, COD_CLASE_MOVIMIENTO); return r.Success ? Ok(r) : BadRequest(r); }

}