using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/tipos-movimiento")]
[ExigePermiso("LOGISTICA.TIPOS_MOVIMIENTO")]
public class TiposMovimientoController : BaseController
{
    private readonly ITiposMovimientoService _service;
    public TiposMovimientoController(ITiposMovimientoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codClaseMovimiento)
    { var r = await _service.GetAllAsync(codClaseMovimiento); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CLASE_MOVIMIENTO}/{COD_TIPO_MOVIMIENTO}")]
    public async Task<IActionResult> GetById(string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO)
    { var r = await _service.GetByIdAsync(COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] string codClaseMovimiento)
    { var r = await _service.GetComboboxAsync(codClaseMovimiento); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_LOGISTICA_TIPOS_MOVIMIENTO_LOADCOMBOS: TiposEntidades + TiposMotivos +
    /// TiposEstados + Estado para el diálogo en un solo viaje. codTipoEstado es opcional
    /// (se autorresuelve).</summary>
    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos([FromQuery] int codTipoEstado = 0)
    { var r = await _service.LoadcombosAsync(codTipoEstado); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TiposMovimientoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposMovimientoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    /// <summary>Modificacion. No existia: el controlador solo tenia POST, y el SP de alta
    /// rechaza una clave que ya existe. O sea que modificar un tipo de movimiento devolvia
    /// "Ya existe un registro con esa clave" y no habia forma de corregir ninguno desde la
    /// aplicacion. El SP de update estaba en la base desde siempre, sin nadie que lo llamara.</summary>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TiposMovimientoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_CLASE_MOVIMIENTO}/{COD_TIPO_MOVIMIENTO}")]
    public async Task<IActionResult> Delete(string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO)
    { var r = await _service.DeleteAsync(COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO); return r.Success ? Ok(r) : BadRequest(r); }

}