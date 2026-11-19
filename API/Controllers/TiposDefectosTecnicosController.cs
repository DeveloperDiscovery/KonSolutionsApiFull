using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Controlcalidad;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/controlcalidad/tipos-defectos-tecnicos")]
[ExigePermiso("CONTROLCALIDAD.TIPOS_DEFECTOS_TECNICOS")]
public class TiposDefectosTecnicosController : BaseController
{
    private readonly ITiposDefectosTecnicosService _service;
    public TiposDefectosTecnicosController(ITiposDefectosTecnicosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codClaseDefectoTecnico)
    { var r = await _service.GetAllAsync(codClaseDefectoTecnico); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CLASE_DEFECTO_TECNICO}/{COD_TIPO_DEFECTO_TECNICO}")]
    public async Task<IActionResult> GetById(string COD_CLASE_DEFECTO_TECNICO, string COD_TIPO_DEFECTO_TECNICO)
    { var r = await _service.GetByIdAsync(COD_CLASE_DEFECTO_TECNICO, COD_TIPO_DEFECTO_TECNICO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TiposDefectosTecnicosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposDefectosTecnicosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TiposDefectosTecnicosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_CLASE_DEFECTO_TECNICO}/{COD_TIPO_DEFECTO_TECNICO}")]
    public async Task<IActionResult> Delete(string COD_CLASE_DEFECTO_TECNICO, string COD_TIPO_DEFECTO_TECNICO)
    { var r = await _service.DeleteAsync(COD_CLASE_DEFECTO_TECNICO, COD_TIPO_DEFECTO_TECNICO); return r.Success ? Ok(r) : BadRequest(r); }

}