using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/configuracion-usuario")]
[ExigePermiso("ESTANDAR.CONFIGURACION_USUARIO")]
public class ConfiguracionUsuarioController : BaseController
{
    private readonly IConfiguracionUsuarioService _service;
    public ConfiguracionUsuarioController(IConfiguracionUsuarioService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Configuración del usuario actualmente autenticado.</summary>
    [HttpGet("mi-configuracion")]
    public async Task<IActionResult> GetMiConfiguracion()
    { var r = await _service.GetByIdAsync(GetCodUsuario()); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("{COD_USUARIO}")]
    public async Task<IActionResult> GetById(string COD_USUARIO)
    { var r = await _service.GetByIdAsync(COD_USUARIO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ConfiguracionUsuarioSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Guarda la configuración del usuario actualmente autenticado (ignora COD_USUARIO recibido).</summary>
    [HttpPost]
    public async Task<IActionResult> Save([FromBody] ConfiguracionUsuarioSaveDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        dto.COD_USUARIO = GetCodUsuario();
        var r = await _service.SaveAsync(dto, GetCodUsuario(), GetEstacion());
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpDelete("{COD_USUARIO}")]
    public async Task<IActionResult> Delete(string COD_USUARIO)
    { var r = await _service.DeleteAsync(COD_USUARIO); return r.Success ? Ok(r) : BadRequest(r); }
}
