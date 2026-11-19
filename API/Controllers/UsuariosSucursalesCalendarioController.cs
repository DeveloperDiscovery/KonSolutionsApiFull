using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/seguridad/usuarios-sucursales-calendario")]
[ExigePermiso("SEGURIDAD.USUARIOS_SUCURSALES_CALENDARIO")]
public class UsuariosSucursalesCalendarioController : BaseController
{
    private readonly IUsuariosSucursalesCalendarioService _service;
    public UsuariosSucursalesCalendarioController(IUsuariosSucursalesCalendarioService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_USUARIO}/{COD_SUCURSAL}/{FEC_INICIAL}")]
    public async Task<IActionResult> GetById(string COD_USUARIO, string COD_SUCURSAL, DateTime FEC_INICIAL)
    { var r = await _service.GetByIdAsync(COD_USUARIO, COD_SUCURSAL, FEC_INICIAL); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] UsuariosSucursalesCalendarioSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] UsuariosSucursalesCalendarioInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UsuariosSucursalesCalendarioUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_USUARIO}/{COD_SUCURSAL}/{FEC_INICIAL}")]
    public async Task<IActionResult> Delete(string COD_USUARIO, string COD_SUCURSAL, DateTime FEC_INICIAL)
    { var r = await _service.DeleteAsync(COD_USUARIO, COD_SUCURSAL, FEC_INICIAL); return r.Success ? Ok(r) : BadRequest(r); }

}