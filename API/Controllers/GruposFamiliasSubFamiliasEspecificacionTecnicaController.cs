using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/ingenieria/grupos-familias-sub-familias-especificacion-tecnica")]
[ExigePermiso("INGENIERIA.GRUPOS_FAMILIAS_SUB_FAMILIAS_ESPECIFICACION_TECNICA")]
public class GruposFamiliasSubFamiliasEspecificacionTecnicaController : BaseController
{
    private readonly IGruposFamiliasSubFamiliasEspecificacionTecnicaService _service;
    public GruposFamiliasSubFamiliasEspecificacionTecnicaController(IGruposFamiliasSubFamiliasEspecificacionTecnicaService service) => _service = service;

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] GruposFamiliasSubFamiliasEspecificacionTecnicaSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("treelist")]
    public async Task<IActionResult> Treelist([FromQuery] GruposFamiliasSubFamiliasEspecificacionTecnicaSearchDto dto)
    { var r = await _service.GetTreelistAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_ESPECIFICACION_TECNICA_LOADCOMBOS:
    /// Tipos de Registro de Datos + Grupos de Especificación Técnica + estados para
    /// GruposEspecificacionTecnicaDialog.razor en un solo viaje. codTipoEstado es opcional
    /// (se autorresuelve).</summary>
    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos([FromQuery] int codTipoEstado = 0)
    { var r = await _service.LoadcombosAsync(codTipoEstado); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_GRUPO_PRODUCTO}/{COD_FAMILIA_PRODUCTO}/{COD_SUB_FAMILIA_PRODUCTO}/{COD_ESPECIFICACION_TECNICA}")]
    public async Task<IActionResult> GetById(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO, string COD_ESPECIFICACION_TECNICA)
    { var r = await _service.GetByIdAsync(COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO, COD_SUB_FAMILIA_PRODUCTO, COD_ESPECIFICACION_TECNICA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] GruposFamiliasSubFamiliasEspecificacionTecnicaInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_GRUPO_PRODUCTO}/{COD_FAMILIA_PRODUCTO}/{COD_SUB_FAMILIA_PRODUCTO}/{COD_ESPECIFICACION_TECNICA}")]
    public async Task<IActionResult> Delete(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO, string COD_ESPECIFICACION_TECNICA)
    { var r = await _service.DeleteAsync(COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO, COD_SUB_FAMILIA_PRODUCTO, COD_ESPECIFICACION_TECNICA); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("copiar")]
    public async Task<IActionResult> Copiar([FromBody] GruposFamiliasSubFamiliasEspecificacionTecnicaCopiarDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.CopiarAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }
}
