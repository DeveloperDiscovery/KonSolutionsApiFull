using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Equipos RFID, de solo lectura. Un equipo -impresora, antena de escritura, lector- es un PRODUCTO (ESTANDAR.PRODUCTOS) y sus datos son las
/// especificaciones técnicas de su ficha: se registran en Productos y Ficha técnica. Qué productos son equipos y qué especificación guarda cada dato se
/// dice en Configuración general &gt; RFID. Van con el permiso de las emisiones (quien emite necesita verlos; el agente también).</summary>
[Authorize]
[Route("api/v1/rfid/equipos")]
[ExigePermiso("RFID.EMISIONES")]
public class RfidEquiposController : BaseController
{
    private readonly IRfidEquiposService _service;
    public RfidEquiposController(IRfidEquiposService service) => _service = service;

    /// <summary>Todos los equipos con los datos de su ficha técnica (marca, modelo, protocolo, IP, puertos, tipo, potencia, estación, plantilla).</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_PRODUCTO}")]
    public async Task<IActionResult> GetById(string COD_PRODUCTO)
    { var r = await _service.GetByIdAsync(COD_PRODUCTO); return r.Success ? Ok(r) : NotFound(r); }

    /// <summary>Los equipos para elegir uno al emitir.</summary>
    [HttpGet("combo")]
    public async Task<IActionResult> Combo()
    { var r = await _service.ComboAsync(); return r.Success ? Ok(r) : BadRequest(r); }
}
