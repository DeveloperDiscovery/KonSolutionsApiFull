using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/seguridad/usuarios-tipos-operacion")]
[ExigePermiso("SEGURIDAD.USUARIOS_TIPOS_OPERACION")]
public class UsuariosTiposOperacionController : BaseController
{
    private readonly IUsuariosTiposOperacionService _service;
    public UsuariosTiposOperacionController(IUsuariosTiposOperacionService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_USUARIO}/{COD_CLASE_OPERACION}/{COD_TIPO_OPERACION}")]
    public async Task<IActionResult> GetById(string COD_USUARIO, string COD_CLASE_OPERACION, string COD_TIPO_OPERACION)
    { var r = await _service.GetByIdAsync(COD_USUARIO, COD_CLASE_OPERACION, COD_TIPO_OPERACION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] UsuariosTiposOperacionSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] UsuariosTiposOperacionInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_USUARIO}/{COD_CLASE_OPERACION}/{COD_TIPO_OPERACION}")]
    public async Task<IActionResult> Delete(string COD_USUARIO, string COD_CLASE_OPERACION, string COD_TIPO_OPERACION)
    { var r = await _service.DeleteAsync(COD_USUARIO, COD_CLASE_OPERACION, COD_TIPO_OPERACION); return r.Success ? Ok(r) : BadRequest(r); }

}