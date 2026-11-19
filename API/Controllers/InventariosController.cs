using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/inventarios")]
[ExigePermiso("LOGISTICA.INVENTARIOS")]
public class InventariosController : BaseController
{
    private readonly IInventariosService _service;
    public InventariosController(IInventariosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_INVENTARIO}")]
    public async Task<IActionResult> GetById(short NUM_INVENTARIO)
    { var r = await _service.GetByIdAsync(NUM_INVENTARIO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] InventariosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] InventariosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] InventariosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_INVENTARIO}")]
    public async Task<IActionResult> Delete(short NUM_INVENTARIO)
    { var r = await _service.DeleteAsync(NUM_INVENTARIO); return r.Success ? Ok(r) : BadRequest(r); }


    // ── Procesos de inventario (botones 04 a 08 de la opción) ───────────────
    // Van por POST y no por GET: los cinco escriben.
    // El usuario y la estación salen del JWT y del contexto de la conexión, no de la
    // llamada: el cliente no debe poder decir "esto lo hizo otro".

    /// <summary>Botón 04 · Generar Requerimiento de Equipos para Inventario.</summary>
    [HttpPost("{NUM_INVENTARIO}/requerimiento")]
    public async Task<IActionResult> RequerimientoMake(short NUM_INVENTARIO)
    { var r = await _service.RequerimientoMakeAsync(NUM_INVENTARIO, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Botón 05 · Revertir Requerimiento.</summary>
    [HttpPost("{NUM_INVENTARIO}/requerimiento-revierte")]
    public async Task<IActionResult> RequerimientoRevierte(short NUM_INVENTARIO)
    { var r = await _service.RequerimientoRevierteAsync(NUM_INVENTARIO, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Botón 06 · Generar Imagen de Inventario.
    /// El SP correspondiente todavía no existe en la base: ver el LEEME.</summary>
    [HttpPost("{NUM_INVENTARIO}/imagen")]
    public async Task<IActionResult> Imagen(short NUM_INVENTARIO)
    { var r = await _service.ImagenAsync(NUM_INVENTARIO, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Botón 07 · Cerrar Inventario.</summary>
    [HttpPost("{NUM_INVENTARIO}/cerrar")]
    public async Task<IActionResult> Cerrar(short NUM_INVENTARIO)
    { var r = await _service.CerrarAsync(NUM_INVENTARIO, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Botón 08 · Abrir Inventario.</summary>
    [HttpPost("{NUM_INVENTARIO}/abrir")]
    public async Task<IActionResult> Abrir(short NUM_INVENTARIO)
    { var r = await _service.AbrirAsync(NUM_INVENTARIO, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

}
