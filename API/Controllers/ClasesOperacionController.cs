using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/clases-operacion")]
[ExigePermiso("ESTANDAR.CLASES_OPERACION")]
public class ClasesOperacionController : BaseController
{
    private readonly IClasesOperacionService _service;
    public ClasesOperacionController(IClasesOperacionService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CLASE_OPERACION}")]
    public async Task<IActionResult> GetById(string COD_CLASE_OPERACION)
    { var r = await _service.GetByIdAsync(COD_CLASE_OPERACION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_ESTANDAR_CLASES_OPERACION_LOADCOMBOS (SP ya existente en BD, antes
    /// huérfano): TiposEntidades + TiposEstados + TiposMotivos + Estado para el diálogo en un
    /// solo viaje. codTipoEstado es opcional (se autorresuelve).</summary>
    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos([FromQuery] int codTipoEstado = 0)
    { var r = await _service.LoadcombosAsync(codTipoEstado); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ClasesOperacionSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ClasesOperacionInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ClasesOperacionUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_CLASE_OPERACION}")]
    public async Task<IActionResult> Delete(string COD_CLASE_OPERACION)
    { var r = await _service.DeleteAsync(COD_CLASE_OPERACION); return r.Success ? Ok(r) : BadRequest(r); }

}