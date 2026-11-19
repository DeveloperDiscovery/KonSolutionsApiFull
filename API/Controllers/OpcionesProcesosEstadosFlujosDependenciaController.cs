using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/opciones-procesos-estados-flujos-dependencia")]
[ExigePermiso("ESTANDAR.OPCIONES_PROCESOS_ESTADOS")]
public class OpcionesProcesosEstadosFlujosDependenciaController : BaseController
{
    private readonly IOpcionesProcesosEstadosFlujosDependenciaService _service;
    public OpcionesProcesosEstadosFlujosDependenciaController(IOpcionesProcesosEstadosFlujosDependenciaService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string codAplicacion, [FromQuery] string codOpcionAplicacion,
        [FromQuery] short codTipoEstado, [FromQuery] string codBotonOpcion)
    {
        var r = await _service.GetAllAsync(codAplicacion, codOpcionAplicacion, codTipoEstado, codBotonOpcion);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] OpcionesProcesosEstadosFlujosDependenciaInsertDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
        return r.Success ? StatusCode(201, r) : BadRequest(r);
    }

    [HttpDelete("{COD_APLICACION}/{COD_OPCION_APLICACION}/{COD_TIPO_ESTADO}/{COD_BOTON_OPCION}/{COD_ESTADO}")]
    public async Task<IActionResult> Delete(
        string COD_APLICACION, string COD_OPCION_APLICACION, short COD_TIPO_ESTADO, string COD_BOTON_OPCION, short COD_ESTADO)
    {
        var r = await _service.DeleteAsync(COD_APLICACION, COD_OPCION_APLICACION, COD_TIPO_ESTADO, COD_BOTON_OPCION, COD_ESTADO);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    // OJO: este endpoint hereda [ExigePermiso("ESTANDAR.OPCIONES_PROCESOS_ESTADOS")] de la
    // clase — ese permiso es el de administrar ESTA tabla de configuración. Si lo vas a llamar
    // desde OTRAS pantallas (ej. antes de ejecutar "Anular" en Requerimientos de Producción)
    // para validar la transición de estado, esos usuarios probablemente NO tengan ese permiso
    // y el endpoint les va a devolver 403 aunque sí puedan ejecutar el botón. Si pasa eso, avisá
    // y lo saco a un controller aparte solo con [Authorize].
    [HttpGet("validar")]
    public async Task<IActionResult> Validar(
        [FromQuery] string codAplicacion, [FromQuery] string codOpcionAplicacion,
        [FromQuery] string codBotonOpcion, [FromQuery] short codTipoEstado, [FromQuery] short codEstadoActual)
    {
        var r = await _service.ValidarAsync(codAplicacion, codOpcionAplicacion, codBotonOpcion, codTipoEstado, codEstadoActual);
        return r.Success ? Ok(r) : BadRequest(r);
    }
}
