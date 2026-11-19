using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/tipos-almacen")]
[ExigePermiso("LOGISTICA.TIPOS_ALMACEN")]
public class TiposAlmacenController : BaseController
{
    private readonly ITiposAlmacenService _service;
    public TiposAlmacenController(ITiposAlmacenService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TIPO_ALMACEN}")]
    public async Task<IActionResult> GetById(string COD_TIPO_ALMACEN)
    { var r = await _service.GetByIdAsync(COD_TIPO_ALMACEN); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TiposAlmacenSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. Es el primer módulo con los verbos separados: el SP
    // _INSERT ya no hace upsert, así que un POST sobre una clave existente es un error y no
    // una modificación encubierta.
    //
    // Para el guardián de permisos esto cambia algo concreto: al haber un verbo por
    // intención, ya no necesita consultar la base para saber si exigir FLG_ACCION_POST o
    // FLG_ACCION_PUT. Este módulo deja de pagar esa lectura.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposAlmacenInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TiposAlmacenUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_TIPO_ALMACEN}")]
    public async Task<IActionResult> Delete(string COD_TIPO_ALMACEN)
    { var r = await _service.DeleteAsync(COD_TIPO_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

}