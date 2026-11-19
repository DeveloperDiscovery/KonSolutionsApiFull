using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Administracion;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/administracion/areas")]
[ExigePermiso("ADMINISTRACION.AREAS")]
public class AreasController : BaseController
{
    private readonly IAreasService _service;
    public AreasController(IAreasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_AREA}")]
    public async Task<IActionResult> GetById(int COD_AREA)
    { var r = await _service.GetByIdAsync(COD_AREA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_ADMINISTRACION_AREAS_LOADCOMBOS: sucursales + centros de costo + estados
    /// para el diálogo de Áreas en un solo viaje. codTipoEstado es opcional (se autorresuelve).</summary>
    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos([FromQuery] int codTipoEstado = 0)
    { var r = await _service.LoadcombosAsync(codTipoEstado); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] AreasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_ADMINISTRACION_AREAS_CARGOS_TREEVIEW: cargos activos del área — combo
    /// "Cargo" al asignar cargos a un tipo de solicitud comercial.</summary>
    [HttpGet("{codArea}/cargos")]
    public async Task<IActionResult> GetCargosByArea(int codArea)
    { var r = await _service.GetCargosByAreaAsync(codArea); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] AreasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] AreasUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_AREA}")]
    public async Task<IActionResult> Delete(int COD_AREA)
    { var r = await _service.DeleteAsync(COD_AREA); return r.Success ? Ok(r) : BadRequest(r); }

}