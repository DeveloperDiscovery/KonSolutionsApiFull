using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/opciones-procesos-estados-flujos")]
[ExigePermiso("ESTANDAR.OPCIONES_PROCESOS_ESTADOS")]
public class OpcionesProcesosEstadosFlujosController : BaseController
{
    private readonly IOpcionesProcesosEstadosFlujosService _service;
    public OpcionesProcesosEstadosFlujosController(IOpcionesProcesosEstadosFlujosService service) => _service = service;

    [HttpGet("treelist")]
    public async Task<IActionResult> GetTreelist(
        [FromQuery] string codAplicacion, [FromQuery] string codOpcionAplicacion, [FromQuery] short codTipoEstado)
    {
        var r = await _service.GetTreelistAsync(codAplicacion, codOpcionAplicacion, codTipoEstado);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string codAplicacion, [FromQuery] string codOpcionAplicacion, [FromQuery] short codTipoEstado)
    {
        var r = await _service.GetAllAsync(codAplicacion, codOpcionAplicacion, codTipoEstado);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] OpcionesProcesosEstadosFlujosInsertDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
        return r.Success ? StatusCode(201, r) : BadRequest(r);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] OpcionesProcesosEstadosFlujosUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpDelete("{COD_APLICACION}/{COD_OPCION_APLICACION}/{COD_TIPO_ESTADO}/{COD_BOTON_OPCION}")]
    public async Task<IActionResult> Delete(string COD_APLICACION, string COD_OPCION_APLICACION, short COD_TIPO_ESTADO, string COD_BOTON_OPCION)
    {
        var r = await _service.DeleteAsync(COD_APLICACION, COD_OPCION_APLICACION, COD_TIPO_ESTADO, COD_BOTON_OPCION);
        return r.Success ? Ok(r) : BadRequest(r);
    }
}
