using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Combos de la pestaña RFID de Configuración general (estación donde corre el agente y archivo de la plantilla). Van con el permiso de Configuración general,
/// como el resto de esa pantalla, no con el de las emisiones.</summary>
[Authorize]
[Route("api/v1/estandar/configuracion-general/rfid")]
[ExigePermiso("ESTANDAR.CONFIGURACION_GENERAL")]
public class RfidConfiguracionController : BaseController
{
    private readonly IRfidConfiguracionService _service;
    public RfidConfiguracionController(IRfidConfiguracionService service) => _service = service;

    /// <summary>Los productos que pueden ser la estación: los del grupo y, si se indican, la familia y la sub familia. Sin grupo, ninguno.</summary>
    [HttpGet("estaciones")]
    public async Task<IActionResult> Estaciones([FromQuery] string? codGrupoProducto, [FromQuery] string? codFamiliaProducto, [FromQuery] string? codSubFamiliaProducto)
    { var r = await _service.EstacionesAsync(codGrupoProducto, codFamiliaProducto, codSubFamiliaProducto); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Los archivos que hay en la carpeta de la ruta (ENTORNO.PATHFILES) para elegir la plantilla de impresión.</summary>
    [HttpGet("plantillas/{codKeyPathfile}")]
    public async Task<IActionResult> Plantillas(string codKeyPathfile)
    { var r = await _service.ArchivosPlantillaAsync(codKeyPathfile); return r.Success ? Ok(r) : BadRequest(r); }
}
