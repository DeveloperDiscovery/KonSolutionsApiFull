using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/seguridad/perfiles-usuario")]
[ExigePermiso("SEGURIDAD.PERFILES_USUARIO")]
public class PerfilesUsuarioController : BaseController
{
    private readonly IPerfilesUsuarioService _service;
    public PerfilesUsuarioController(IPerfilesUsuarioService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_PERFIL_USUARIO}")]
    public async Task<IActionResult> GetById(string COD_PERFIL_USUARIO)
    { var r = await _service.GetByIdAsync(COD_PERFIL_USUARIO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_SEGURIDAD_PERFILES_USUARIO_LOADCOMBOS: complejidades de contraseña +
    /// estados para PerfilDialog.razor en un solo viaje. codTipoEstado es opcional (se
    /// autorresuelve).</summary>
    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos([FromQuery] int codTipoEstado = 0)
    { var r = await _service.LoadcombosAsync(codTipoEstado); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] PerfilesUsuarioSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [Authorize(Policy = "Administrador")]
    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] PerfilesUsuarioInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] PerfilesUsuarioUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [Authorize(Policy = "Administrador")]
    [HttpDelete("{COD_PERFIL_USUARIO}")]
    public async Task<IActionResult> Delete(string COD_PERFIL_USUARIO)
    { var r = await _service.DeleteAsync(COD_PERFIL_USUARIO); return r.Success ? Ok(r) : BadRequest(r); }

}