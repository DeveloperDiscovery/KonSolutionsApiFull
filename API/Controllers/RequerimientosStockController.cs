using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/requerimientos-stock")]
[ExigePermiso("LOGISTICA.REQUERIMIENTOS_STOCK")]
// 67/68: piloto del guardián de flujo. "00030"/"030001" son COD_APLICACION/
// COD_OPCION_APLICACION de "Requerimientos de Almacén" -las mismas con las que ya
// se armó "Configuración de Flujos de los Estados" para este módulo. Falta
// configurar la dependencia REQUIERE REGISTRADO sobre el botón MODIFICAR en esa
// pantalla para que esto empiece a bloquear algo: hoy, sin esa fila, el SP de
// validación devuelve válido siempre -es exactamente el estado en el que quedó
// la configuración real, según lo que confirmamos con el diagnóstico.
[ExigeFlujo("00030", "030001")]
public class RequerimientosStockController : BaseController
{
    private readonly IRequerimientosStockService _service;
    public RequerimientosStockController(IRequerimientosStockService service) => _service = service;

    // El filtro por Tipo de Requerimiento va [FromQuery] y es OPCIONAL: una llamada sin nada
    // sigue devolviendo el listado completo, como siempre. El combo de "Requerimiento de
    // origen" de Reservas pide este mismo endpoint sin filtrar y no se entera.
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] RequerimientosStockSearchDto? filtros)
    { var r = await _service.GetAllAsync(filtros); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_REQUERIMIENTO_STOCK}")]
    public async Task<IActionResult> GetById(int NUM_REQUERIMIENTO_STOCK)
    { var r = await _service.GetByIdAsync(NUM_REQUERIMIENTO_STOCK); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] RequerimientosStockSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_LOGISTICA_REQUERIMIENTOS_STOCK_LOADCOMBOS: Tipos + Centros de costo +
    /// Entidades + Lotes + Productos para RequerimientoDocumento.razor en un solo viaje.</summary>
    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos()
    { var r = await _service.LoadcombosAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] RequerimientosStockInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] RequerimientosStockUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_REQUERIMIENTO_STOCK}")]
    public async Task<IActionResult> Delete(int NUM_REQUERIMIENTO_STOCK)
    { var r = await _service.DeleteAsync(NUM_REQUERIMIENTO_STOCK); return r.Success ? Ok(r) : BadRequest(r); }

    // Botones del documento (COD_BOTON_OPCION "04"/"05" en SEGURIDAD.APLICACIONES_OPCIONES_BOTONES).
    // Rutas alineadas con los helpers que ApiRoutes.Logistica ya tenía declarados —
    // RequerimientosStockGeneraReservas/RevierteReservas— sin usar hasta ahora.
    [HttpPost("{numRequerimientoStock}/genera-reservas")]
    public async Task<IActionResult> GeneraReservas(int numRequerimientoStock)
    { var r = await _service.MakereservaAsync(numRequerimientoStock, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("{numRequerimientoStock}/revierte-reservas")]
    public async Task<IActionResult> RevierteReservas(int numRequerimientoStock)
    { var r = await _service.ReversereservaAsync(numRequerimientoStock, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

}