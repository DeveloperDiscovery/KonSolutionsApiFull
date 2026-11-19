using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/entidades-usuarios-sugeridos")]
[ExigePermiso("ESTANDAR.ENTIDADES_USUARIOS_COMERCIALES")]
public class EntidadesUsuariosSugeridosController : BaseController
{
    private readonly IEntidadesUsuariosSugeridosService _service;
    public EntidadesUsuariosSugeridosController(IEntidadesUsuariosSugeridosService service) => _service = service;

    [HttpGet("comerciales")]
    public async Task<IActionResult> GetComerciales()
    { var r = await _service.GetComercialesAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("dependientes")]
    public async Task<IActionResult> GetDependientes()
    { var r = await _service.GetDependientesAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_ESTANDAR_ENTIDADES_DEPENDIENTES_SHOWCOMBOX: los dependientes de UNA
    /// entidad. Lo usa el combo de Vendedor del Pedido Comercial, donde esa entidad es el
    /// proveedor del pedido.</summary>
    [HttpGet("dependientes/{codEntidad}")]
    public async Task<IActionResult> GetDependientesPorEntidad(decimal codEntidad)
    { var r = await _service.GetDependientesPorEntidadAsync(codEntidad); return r.Success ? Ok(r) : BadRequest(r); }
}
