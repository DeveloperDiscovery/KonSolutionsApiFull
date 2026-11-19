using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/opciones-procesos-estados")]
[ExigePermiso("ESTANDAR.OPCIONES_PROCESOS_ESTADOS")]
public class OpcionesProcesosEstadosController : BaseController
{
    private readonly IOpcionesProcesosEstadosService _service;
    public OpcionesProcesosEstadosController(IOpcionesProcesosEstadosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codAplicacion, [FromQuery] string codOpcionAplicacion)
    { var r = await _service.GetAllAsync(codAplicacion, codOpcionAplicacion); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_APLICACION}/{COD_OPCION_APLICACION}/{COD_TIPO_ESTADO}")]
    public async Task<IActionResult> GetById(string COD_APLICACION, string COD_OPCION_APLICACION, short COD_TIPO_ESTADO)
    { var r = await _service.GetByIdAsync(COD_APLICACION, COD_OPCION_APLICACION, COD_TIPO_ESTADO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] string codAplicacion, [FromQuery] string codOpcionAplicacion)
    { var r = await _service.GetComboboxAsync(codAplicacion, codOpcionAplicacion); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] OpcionesProcesosEstadosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // El SP _INSERT es upsert (actualiza si ya existe, inserta si no) — no hay verbo PUT/Update.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] OpcionesProcesosEstadosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_APLICACION}/{COD_OPCION_APLICACION}/{COD_TIPO_ESTADO}")]
    public async Task<IActionResult> Delete(string COD_APLICACION, string COD_OPCION_APLICACION, short COD_TIPO_ESTADO)
    { var r = await _service.DeleteAsync(COD_APLICACION, COD_OPCION_APLICACION, COD_TIPO_ESTADO); return r.Success ? Ok(r) : BadRequest(r); }
}
