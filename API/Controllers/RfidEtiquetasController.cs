using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Rfid;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Consulta de etiquetas RFID de todas las emisiones: por código, TID, EPC, producto, lote, emisión o estado.
/// También recibe la constancia de que el EPC quedó grabado en el chip (la manda el equipo o el agente local).</summary>
[Authorize]
[Route("api/v1/rfid/etiquetas")]
[ExigePermiso("RFID.EMISIONES_ETIQUETAS")]
public class RfidEtiquetasController : BaseController
{
    private readonly IRfidEmisionesService _service;
    public RfidEtiquetasController(IRfidEmisionesService service) => _service = service;

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] RfidEtiquetaSearchDto dto)
    { var r = await _service.EtiquetasSearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>El equipo grabó el EPC y lo leyó de vuelta: si <c>COD_EPC_LEIDO</c> viene y no coincide, la grabación falló y no se deja constancia.</summary>
    [HttpPut("grabada")]
    [ExigePermiso("RFID.EMISIONES")]
    public async Task<IActionResult> Grabada([FromBody] RfidGrabadaDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.GrabadaAsync(dto, GetCodUsuario());
      return r.Success ? Ok(r) : BadRequest(r); }
}
