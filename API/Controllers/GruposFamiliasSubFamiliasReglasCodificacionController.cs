using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/ingenieria/grupos-familias-sub-familias-reglas-codificacion")]
[ExigePermiso("INGENIERIA.GRUPOS_FAMILIAS_SUB_FAMILIAS_REGLAS_CODIFICACION")]
public class GruposFamiliasSubFamiliasReglasCodificacionController : BaseController
{
    private readonly IGruposFamiliasSubFamiliasReglasCodificacionService _service;
    public GruposFamiliasSubFamiliasReglasCodificacionController(IGruposFamiliasSubFamiliasReglasCodificacionService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_GRUPO_PRODUCTO}/{COD_FAMILIA_PRODUCTO}/{COD_SUB_FAMILIA_PRODUCTO}")]
    public async Task<IActionResult> GetById(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO)
    { var r = await _service.GetByIdAsync(COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO, COD_SUB_FAMILIA_PRODUCTO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] GruposFamiliasSubFamiliasReglasCodificacionSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] GruposFamiliasSubFamiliasReglasCodificacionInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] GruposFamiliasSubFamiliasReglasCodificacionUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_GRUPO_PRODUCTO}/{COD_FAMILIA_PRODUCTO}/{COD_SUB_FAMILIA_PRODUCTO}")]
    public async Task<IActionResult> Delete(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO)
    { var r = await _service.DeleteAsync(COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO, COD_SUB_FAMILIA_PRODUCTO); return r.Success ? Ok(r) : BadRequest(r); }

}