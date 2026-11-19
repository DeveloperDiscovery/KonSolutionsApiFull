using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

// ── Maestro ──────────────────────────────────────────────────────────────────
[Authorize]
[Route("api/v1/logistica/flujos-aprobacion")]
[ExigePermiso("LOGISTICA.FLUJOS_APROBACION")]
public class FlujosAprobacionController : BaseController
{
    private readonly IFlujosAprobacionService _service;
    public FlujosAprobacionController(IFlujosAprobacionService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] FlujosAprobacionSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // Va después de las rutas literales (combobox/search) para que no las capture.
    [HttpGet("{NUM_FLUJO_APROBACION}")]
    public async Task<IActionResult> GetById(short NUM_FLUJO_APROBACION)
    { var r = await _service.GetByIdAsync(NUM_FLUJO_APROBACION); return r.Success ? Ok(r) : NotFound(r); }

    // POST = crear, PUT = modificar: el SP de alta es estricto con la clave repetida.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] FlujosAprobacionInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] FlujosAprobacionUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_FLUJO_APROBACION}")]
    public async Task<IActionResult> Delete(short NUM_FLUJO_APROBACION)
    { var r = await _service.DeleteAsync(NUM_FLUJO_APROBACION); return r.Success ? Ok(r) : BadRequest(r); }
}


// ── Detalle: Área / Cargo ────────────────────────────────────────────────────
[Authorize]
[Route("api/v1/logistica/flujos-aprobacion-area-cargo")]
[ExigePermiso("LOGISTICA.FLUJOS_APROBACION")]
public class FlujosAprobacionAreaCargoController : BaseController
{
    private readonly IFlujosAprobacionAreaCargoService _service;
    public FlujosAprobacionAreaCargoController(IFlujosAprobacionAreaCargoService service) => _service = service;

    // Todos los niveles del flujo, de todas sus áreas: es lo que consume la grilla editable,
    // que elige el área dentro de cada fila en vez de filtrar por arriba.
    [HttpGet("{NUM_FLUJO_APROBACION}")]
    public async Task<IActionResult> GetTodos(short NUM_FLUJO_APROBACION)
    { var r = await _service.GetAllAsync(NUM_FLUJO_APROBACION); return r.Success ? Ok(r) : BadRequest(r); }

    // Acotado a un área (se conserva: el SP lo sigue soportando).
    [HttpGet("{NUM_FLUJO_APROBACION}/{COD_AREA}")]
    public async Task<IActionResult> GetAll(short NUM_FLUJO_APROBACION, int COD_AREA)
    { var r = await _service.GetAllAsync(NUM_FLUJO_APROBACION, COD_AREA); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_FLUJO_APROBACION}/{COD_AREA}/{COD_CARGO}")]
    public async Task<IActionResult> GetById(short NUM_FLUJO_APROBACION, int COD_AREA, int COD_CARGO)
    { var r = await _service.GetByIdAsync(NUM_FLUJO_APROBACION, COD_AREA, COD_CARGO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] FlujosAprobacionAreaCargoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] FlujosAprobacionAreaCargoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] FlujosAprobacionAreaCargoUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_FLUJO_APROBACION}/{COD_AREA}/{COD_CARGO}")]
    public async Task<IActionResult> Delete(short NUM_FLUJO_APROBACION, int COD_AREA, int COD_CARGO)
    { var r = await _service.DeleteAsync(NUM_FLUJO_APROBACION, COD_AREA, COD_CARGO); return r.Success ? Ok(r) : BadRequest(r); }

    // Copiar los niveles de este flujo a otro.
    [HttpPost("copy")]
    public async Task<IActionResult> Copy([FromBody] FlujosAprobacionAreaCargoCopyDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.CopyAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }
}


// ── Detalle: Tipos de Operación ──────────────────────────────────────────────
[Authorize]
[Route("api/v1/logistica/flujos-aprobacion-tipos-operacion")]
[ExigePermiso("LOGISTICA.FLUJOS_APROBACION")]
public class FlujosAprobacionTiposOperacionController : BaseController
{
    private readonly IFlujosAprobacionTiposOperacionService _service;
    public FlujosAprobacionTiposOperacionController(IFlujosAprobacionTiposOperacionService service) => _service = service;

    // Todos los tipos del flujo, de todas sus clases: lo que consume la grilla editable.
    [HttpGet("{NUM_FLUJO_APROBACION}")]
    public async Task<IActionResult> GetTodos(short NUM_FLUJO_APROBACION)
    { var r = await _service.GetAllAsync(NUM_FLUJO_APROBACION); return r.Success ? Ok(r) : BadRequest(r); }

    // Acotado a una clase (se conserva: el SP lo sigue soportando).
    [HttpGet("{NUM_FLUJO_APROBACION}/{COD_CLASE_OPERACION}")]
    public async Task<IActionResult> GetAll(short NUM_FLUJO_APROBACION, string COD_CLASE_OPERACION)
    { var r = await _service.GetAllAsync(NUM_FLUJO_APROBACION, COD_CLASE_OPERACION); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_FLUJO_APROBACION}/{COD_CLASE_OPERACION}/{COD_TIPO_OPERACION}")]
    public async Task<IActionResult> GetById(short NUM_FLUJO_APROBACION, string COD_CLASE_OPERACION, string COD_TIPO_OPERACION)
    { var r = await _service.GetByIdAsync(NUM_FLUJO_APROBACION, COD_CLASE_OPERACION, COD_TIPO_OPERACION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] FlujosAprobacionTiposOperacionSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // Tabla de ASIGNACIÓN: el SP de alta es upsert, no hay PUT.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] FlujosAprobacionTiposOperacionInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{NUM_FLUJO_APROBACION}/{COD_CLASE_OPERACION}/{COD_TIPO_OPERACION}")]
    public async Task<IActionResult> Delete(short NUM_FLUJO_APROBACION, string COD_CLASE_OPERACION, string COD_TIPO_OPERACION)
    { var r = await _service.DeleteAsync(NUM_FLUJO_APROBACION, COD_CLASE_OPERACION, COD_TIPO_OPERACION); return r.Success ? Ok(r) : BadRequest(r); }
}
