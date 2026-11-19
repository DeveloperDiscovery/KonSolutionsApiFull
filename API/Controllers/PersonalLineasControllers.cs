using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Personal de produccion por categoria ocupacional (script SQL 124). Permisos: los de PRODUCCION.PERSONAL_OPERACION.</summary>
[Authorize]
[Route("api/v1/produccion/personal-operacion")]
[ExigePermiso("PRODUCCION.PERSONAL_OPERACION")]
public class PersonalOperacionController : BaseController
{
    private readonly IPersonalLineasService _service;
    public PersonalOperacionController(IPersonalLineasService service) => _service = service;

    private IActionResult Res<T>(KonSolutionsApi.Common.ApiResponse<T> r) => r.Success ? Ok(r) : BadRequest(r);

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] string? buscar) => Res(await _service.PersonalListarAsync(buscar));

    [HttpGet("{codEntidad:int}")]
    public async Task<IActionResult> Obtener(int codEntidad) => Res(await _service.PersonalAsync(codEntidad));

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] PersonalOperacionGuardarDto dto)
    { var r = await _service.PersonalGuardarAsync(dto, true, GetCodUsuario(), GetEstacion()); return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Modificar([FromBody] PersonalOperacionGuardarDto dto) => Res(await _service.PersonalGuardarAsync(dto, false, GetCodUsuario(), GetEstacion()));

    [HttpDelete("{codEntidad:int}")]
    public async Task<IActionResult> Eliminar(int codEntidad) => Res(await _service.PersonalEliminarAsync(codEntidad));

    /// <summary>Personas activas que todavia no tienen categoria (para el alta).</summary>
    [HttpGet("disponibles")]
    public async Task<IActionResult> Disponibles() => Res(await _service.PersonalDisponiblesAsync());

    /// <summary>Personal activo por categoria y proceso.</summary>
    [HttpGet("resumen")]
    public async Task<IActionResult> Resumen() => Res(await _service.PersonalResumenAsync());

    [HttpGet("combos")]
    public async Task<IActionResult> Combos() => Res(await _service.PersonalCombosAsync());
}

/// <summary>Armado de lineas de produccion: puestos, reparto de operaciones y balance (script SQL 125). Permisos: los de INGENIERIA.LINEAS_ARMADOS.</summary>
[Authorize]
[Route("api/v1/ingenieria/lineas-armados")]
[ExigePermiso("INGENIERIA.LINEAS_ARMADOS")]
public class LineasArmadosController : BaseController
{
    private readonly IPersonalLineasService _service;
    public LineasArmadosController(IPersonalLineasService service) => _service = service;

    private IActionResult Res<T>(KonSolutionsApi.Common.ApiResponse<T> r) => r.Success ? Ok(r) : BadRequest(r);

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] string? buscar) => Res(await _service.ArmadosAsync(buscar));

    [HttpGet("combos")]
    public async Task<IActionResult> Combos() => Res(await _service.ArmadoCombosAsync());

    [HttpGet("siguiente-codigo")]
    public async Task<IActionResult> SiguienteCodigo() => Res(await _service.ArmadoSiguienteCodigoAsync());

    [HttpGet("{cod}")]
    public async Task<IActionResult> Obtener(string cod) => Res(await _service.ArmadoAsync(cod));

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] ArmadoGuardarDto dto)
    { var r = await _service.ArmadoGuardarAsync(dto, true, GetCodUsuario(), GetEstacion()); return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Modificar([FromBody] ArmadoGuardarDto dto) => Res(await _service.ArmadoGuardarAsync(dto, false, GetCodUsuario(), GetEstacion()));

    [HttpDelete("{cod}")]
    public async Task<IActionResult> Eliminar(string cod) => Res(await _service.ArmadoEliminarAsync(cod));

    [HttpGet("{cod}/puestos")]
    public async Task<IActionResult> Puestos(string cod) => Res(await _service.ArmadoPuestosAsync(cod));

    /// <summary>Reemplaza los puestos y el reparto de operaciones del armado (todo o nada).</summary>
    [HttpPut("{cod}/puestos")]
    public async Task<IActionResult> GuardarPuestos(string cod, [FromBody] ArmadoPuestosGuardarDto dto)
    { dto.COD_ARMADO = cod; return Res(await _service.ArmadoPuestosGuardarAsync(dto, GetCodUsuario(), GetEstacion())); }

    [HttpGet("{cod}/hojas")]
    public async Task<IActionResult> Hojas(string cod) => Res(await _service.ArmadoHojasAsync(cod));

    [HttpGet("{cod}/avisos")]
    public async Task<IActionResult> Avisos(string cod) => Res(await _service.ArmadoAvisosAsync(cod));

    [HttpGet("{cod}/operarios-por-categoria")]
    public async Task<IActionResult> OperariosPorCategoria(string cod) => Res(await _service.ArmadoOperariosPorCategoriaAsync(cod));

    /// <summary>Arma el balance por takt y reemplaza el reparto actual.</summary>
    [HttpPost("{cod}/sugerir")]
    public async Task<IActionResult> Sugerir(string cod) => Res(await _service.ArmadoSugerirAsync(cod, GetCodUsuario(), GetEstacion()));

    // ===== O7: personal del armado =====
    [HttpGet("{cod}/personal")]
    public async Task<IActionResult> Personal(string cod) => Res(await _service.ArmadoPersonalAsignadoAsync(cod));

    [HttpGet("{cod}/personal-disponible")]
    public async Task<IActionResult> PersonalDisponible(string cod) => Res(await _service.ArmadoPersonalDisponibleAsync(cod));

    [HttpGet("{cod}/cobertura")]
    public async Task<IActionResult> Cobertura(string cod) => Res(await _service.ArmadoCoberturaAsync(cod));

    /// <summary>Reemplaza el personal asignado a los puestos (todo o nada).</summary>
    [HttpPut("{cod}/personal")]
    public async Task<IActionResult> GuardarPersonal(string cod, [FromBody] ArmadoPersonalGuardarDto dto)
    { dto.COD_ARMADO = cod; return Res(await _service.ArmadoPersonalGuardarAsync(dto, GetCodUsuario(), GetEstacion())); }

    /// <summary>Asigna el personal disponible a los puestos y reemplaza el actual.</summary>
    [HttpPost("{cod}/personal/sugerir")]
    public async Task<IActionResult> SugerirPersonal(string cod) => Res(await _service.ArmadoPersonalSugerirAsync(cod, GetCodUsuario(), GetEstacion()));

    /// <summary>El balance y el personal que pediria cada disposicion (no guarda nada).</summary>
    [HttpGet("{cod}/comparar")]
    public async Task<IActionResult> Comparar(string cod) => Res(await _service.ArmadoCompararAsync(cod));
}

/// <summary>Migracion asistida de las operaciones de las rutas existentes al catalogo (script SQL 128). Permisos: los de INGENIERIA.OPERACIONES_MIGRACION.</summary>
[Authorize]
[Route("api/v1/ingenieria/operaciones-migracion")]
[ExigePermiso("INGENIERIA.OPERACIONES_MIGRACION")]
public class OperacionesMigracionController : BaseController
{
    private readonly IOperacionesMigracionService _service;
    public OperacionesMigracionController(IOperacionesMigracionService service) => _service = service;

    private IActionResult Res<T>(KonSolutionsApi.Common.ApiResponse<T> r) => r.Success ? Ok(r) : BadRequest(r);

    [HttpGet("resumen")]
    public async Task<IActionResult> Resumen() => Res(await _service.ResumenAsync());

    [HttpGet("propuesta")]
    public async Task<IActionResult> Propuesta([FromQuery] string? ruta) => Res(await _service.PropuestaAsync(ruta));

    /// <summary>Migra los grupos elegidos (todo o nada).</summary>
    [HttpPost]
    public async Task<IActionResult> Aplicar([FromBody] MigracionAplicarDto dto) => Res(await _service.AplicarAsync(dto, GetCodUsuario(), GetEstacion()));

    /// <summary>Deshace lo migrado (todo, o solo una ruta).</summary>
    [HttpDelete]
    public async Task<IActionResult> Deshacer([FromQuery] string? ruta) => Res(await _service.DeshacerAsync(ruta, GetCodUsuario(), GetEstacion()));
}

