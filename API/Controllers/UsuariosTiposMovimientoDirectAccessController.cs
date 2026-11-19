using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Accesos directos de tipo de movimiento, uno por usuario. El permiso declarado es
/// LOGISTICA.MOVIMIENTOS_ALMACEN, el del documento donde viven estos botones: no es una
/// opción de menú propia, se administra desde dentro del documento, y quien puede abrir un
/// movimiento puede marcar o quitar sus propios atajos.</summary>
[Authorize]
[Route("api/v1/logistica/usuarios-tipos-movimiento-direct-access")]
[ExigePermiso("LOGISTICA.MOVIMIENTOS_ALMACEN")]
public class UsuariosTiposMovimientoDirectAccessController : BaseController
{
    private readonly IUsuariosTiposMovimientoDirectAccessService _service;
    public UsuariosTiposMovimientoDirectAccessController(IUsuariosTiposMovimientoDirectAccessService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codClaseMovimiento)
    { var r = await _service.GetAllAsync(GetCodUsuario(), codClaseMovimiento); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CLASE_MOVIMIENTO}/{COD_TIPO_MOVIMIENTO}")]
    public async Task<IActionResult> GetById(string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO)
    { var r = await _service.GetByIdAsync(GetCodUsuario(), COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] string codClaseMovimiento)
    { var r = await _service.GetComboboxAsync(GetCodUsuario(), codClaseMovimiento); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string codClaseMovimiento, [FromQuery] UsuariosTiposMovimientoDirectAccessSearchDto dto)
    { var r = await _service.SearchAsync(GetCodUsuario(), codClaseMovimiento, dto); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>El usuario del acceso directo es SIEMPRE el que está logueado, nunca el que
    /// venga en el cuerpo: si no, cualquiera podría marcar atajos a nombre de otro con sólo
    /// cambiar el COD_USUARIO del POST.</summary>
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] UsuariosTiposMovimientoDirectAccessInsertDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        dto.COD_USUARIO = GetCodUsuario();
        var r = await _service.InsertAsync(dto);
        return r.Success ? StatusCode(201, r) : BadRequest(r);
    }

    [HttpDelete("{COD_CLASE_MOVIMIENTO}/{COD_TIPO_MOVIMIENTO}")]
    public async Task<IActionResult> Delete(string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO)
    { var r = await _service.DeleteAsync(GetCodUsuario(), COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO); return r.Success ? Ok(r) : BadRequest(r); }
}
