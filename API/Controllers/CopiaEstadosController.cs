using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Copia de estados. SIMULAR = true (por defecto) solo valida y devuelve el detalle; con false graba.</summary>
[Authorize]
[Route("api/v1/estandar/copia-estados")]
public class CopiaEstadosController : BaseController
{
    private readonly ICopiaEstadosService _service;
    public CopiaEstadosController(ICopiaEstadosService service) => _service = service;

    /// <summary>Copia la configuracion de estados de una opcion / tipo de estado a otra.</summary>
    [HttpPost("configuracion")]
    [ExigePermiso("ESTANDAR.OPCIONES_PROCESOS_ESTADOS")]
    public async Task<IActionResult> CopiarConfiguracion([FromBody] CopiaConfiguracionEstadosDto dto)
    { var r = await _service.CopiarConfiguracionAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Copia los estados de un tipo de estado a otro, con la opcion de eliminar antes los del destino.</summary>
    [HttpPost("tipo")]
    [ExigePermiso("ESTANDAR.ESTADOS_FROM_ESTADOS_ESTANDAR")]
    public async Task<IActionResult> CopiarTipo([FromBody] CopiaEstadosTipoDto dto)
    { var r = await _service.CopiarEstadosTipoAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }
}
