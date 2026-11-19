using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Base de los cuatro catalogos de operaciones (script SQL 119). Cada uno declara su ruta y su permiso.</summary>
public abstract class CatalogoOperacionControllerBase : BaseController
{
    private readonly IOperacionesTiemposService _service;
    protected CatalogoOperacionControllerBase(IOperacionesTiemposService service) => _service = service;
    protected abstract string Catalogo { get; }

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] bool soloActivos = false)
    { var r = await _service.CatalogoListarAsync(Catalogo, soloActivos); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CatalogoOperacionItem dto)
    { var r = await _service.CatalogoGuardarAsync(Catalogo, dto, true, GetCodUsuario(), GetEstacion()); return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Modificar([FromBody] CatalogoOperacionItem dto)
    { var r = await _service.CatalogoGuardarAsync(Catalogo, dto, false, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{cod}")]
    public async Task<IActionResult> Eliminar(string cod)
    { var r = await _service.CatalogoEliminarAsync(Catalogo, cod); return r.Success ? Ok(r) : BadRequest(r); }
}

[Authorize]
[Route("api/v1/ingenieria/categorias-ocupacionales")]
[ExigePermiso("INGENIERIA.CATEGORIAS_OCUPACIONALES")]
public class CategoriasOcupacionalesController : CatalogoOperacionControllerBase
{
    public CategoriasOcupacionalesController(IOperacionesTiemposService s) : base(s) { }
    protected override string Catalogo => "categorias";
}

[Authorize]
[Route("api/v1/ingenieria/tipos-maquina")]
[ExigePermiso("INGENIERIA.TIPOS_MAQUINA")]
public class TiposMaquinaController : CatalogoOperacionControllerBase
{
    public TiposMaquinaController(IOperacionesTiemposService s) : base(s) { }
    protected override string Catalogo => "maquinas";
}

[Authorize]
[Route("api/v1/ingenieria/metodos-tiempo")]
[ExigePermiso("INGENIERIA.METODOS_TIEMPO")]
public class MetodosTiempoController : CatalogoOperacionControllerBase
{
    public MetodosTiempoController(IOperacionesTiemposService s) : base(s) { }
    protected override string Catalogo => "metodos";
}

[Authorize]
[Route("api/v1/produccion/tipos-distribucion-linea")]
[ExigePermiso("PRODUCCION.TIPOS_DISTRIBUCION_LINEA")]
public class TiposDistribucionLineaController : CatalogoOperacionControllerBase
{
    public TiposDistribucionLineaController(IOperacionesTiemposService s) : base(s) { }
    protected override string Catalogo => "distribuciones";
}

/// <summary>Catalogo de operaciones, su composicion en arbol y sus tiempos estandar (scripts SQL 120 y 121).
/// Permisos: los de INGENIERIA.OPERACIONES (ver = GET, crear = POST, modificar = PUT, borrar = DELETE).</summary>
[Authorize]
[Route("api/v1/ingenieria/operaciones")]
[ExigePermiso("INGENIERIA.OPERACIONES")]
public class OperacionesController : BaseController
{
    private readonly IOperacionesTiemposService _service;
    public OperacionesController(IOperacionesTiemposService service) => _service = service;

    private IActionResult Res<T>(KonSolutionsApi.Common.ApiResponse<T> r) => r.Success ? Ok(r) : BadRequest(r);

    [HttpGet]
    public async Task<IActionResult> Listar() => Res(await _service.OperacionesAsync());

    /// <summary>Los catalogos activos para los combos de las pantallas (categorias, maquinas, metodos, distribuciones, tipos, procesos, subprocesos, unidades).</summary>
    [HttpGet("combos")]
    public async Task<IActionResult> Combos() => Res(await _service.CombosAsync());

    [HttpGet("siguiente-codigo")]
    public async Task<IActionResult> SiguienteCodigo() => Res(await _service.SiguienteCodigoAsync());

    [HttpGet("{cod}")]
    public async Task<IActionResult> Detalle(string cod) { var r = await _service.OperacionAsync(cod); return r.Success ? Ok(r) : NotFound(r); }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] OperacionGuardarDto dto)
    { var r = await _service.OperacionGuardarAsync(dto, true, GetCodUsuario(), GetEstacion()); return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Modificar([FromBody] OperacionGuardarDto dto) => Res(await _service.OperacionGuardarAsync(dto, false, GetCodUsuario(), GetEstacion()));

    [HttpDelete("{cod}")]
    public async Task<IActionResult> Eliminar(string cod) => Res(await _service.OperacionEliminarAsync(cod));

    [HttpGet("{cod}/componentes")]
    public async Task<IActionResult> Componentes(string cod) => Res(await _service.ComponentesAsync(cod));

    [HttpPut("{cod}/componentes")]
    public async Task<IActionResult> GuardarComponentes(string cod, [FromBody] OperacionComponentesGuardarDto dto)
    { dto.COD_OPERACION_PADRE = cod; return Res(await _service.ComponentesGuardarAsync(dto, GetCodUsuario(), GetEstacion())); }

    [HttpGet("{cod}/arbol")]
    public async Task<IActionResult> Arbol(string cod, [FromQuery] DateTime? fecha) => Res(await _service.ArbolAsync(cod, fecha));

    [HttpGet("{cod}/usada-en")]
    public async Task<IActionResult> UsadaEn(string cod) => Res(await _service.UsadaEnAsync(cod));

    [HttpGet("{cod}/usada-en-rutas")]
    public async Task<IActionResult> UsadaEnRutas(string cod) => Res(await _service.UsadaEnRutasAsync(cod));

    [HttpGet("{cod}/tiempos")]
    public async Task<IActionResult> Tiempos(string cod) => Res(await _service.TiemposAsync(cod));

    [HttpPost("tiempos")]
    public async Task<IActionResult> GuardarTiempo([FromBody] OperacionTiempoGuardarDto dto) => Res(await _service.TiempoGuardarAsync(dto, GetCodUsuario(), GetEstacion()));

    [HttpPost("tiempos/aprobar")]
    public async Task<IActionResult> AprobarTiempo([FromBody] OperacionTiempoClaveDto dto) => Res(await _service.TiempoAprobarAsync(dto, GetCodUsuario(), GetEstacion()));

    [HttpPost("tiempos/eliminar")]
    public async Task<IActionResult> EliminarTiempo([FromBody] OperacionTiempoClaveDto dto) => Res(await _service.TiempoEliminarAsync(dto));

    [HttpGet("{cod}/estudios")]
    public async Task<IActionResult> Estudios(string cod) => Res(await _service.EstudiosAsync(cod));

    [HttpGet("estudios/{num:int}/elementos")]
    public async Task<IActionResult> EstudioElementos(int num) => Res(await _service.EstudioElementosAsync(num));

    [HttpPost("estudios")]
    public async Task<IActionResult> GuardarEstudio([FromBody] EstudioTiempoGuardarDto dto) => Res(await _service.EstudioGuardarAsync(dto, GetCodUsuario(), GetEstacion()));

    [HttpDelete("estudios/{num:int}")]
    public async Task<IActionResult> EliminarEstudio(int num) => Res(await _service.EstudioEliminarAsync(num));

    [HttpGet("{cod}/sam-por-categoria")]
    public async Task<IActionResult> SamPorCategoria(string cod, [FromQuery] DateTime? fecha) => Res(await _service.SamPorCategoriaOperacionAsync(cod, fecha));

    [HttpGet("{cod}/sam-por-clase")]
    public async Task<IActionResult> SamPorClase(string cod, [FromQuery] DateTime? fecha) => Res(await _service.SamPorClaseOperacionAsync(cod, fecha));
}

/// <summary>La ruta de produccion con tiempos estandar, sumas y precedencias (script SQL 122). Permisos: los de INGENIERIA.RUTAS_TIEMPOS.</summary>
[Authorize]
[Route("api/v1/ingenieria/rutas-tiempos")]
[ExigePermiso("INGENIERIA.RUTAS_TIEMPOS")]
public class RutasTiemposController : BaseController
{
    private readonly IOperacionesTiemposService _service;
    public RutasTiemposController(IOperacionesTiemposService service) => _service = service;

    private IActionResult Res<T>(KonSolutionsApi.Common.ApiResponse<T> r) => r.Success ? Ok(r) : BadRequest(r);

    [HttpGet]
    public async Task<IActionResult> Rutas() => Res(await _service.RutasAsync());

    [HttpGet("{ruta}/arbol")]
    public async Task<IActionResult> Arbol(string ruta, [FromQuery] DateTime? fecha) => Res(await _service.RutaArbolAsync(ruta, fecha));

    [HttpPost("procesos")]
    public async Task<IActionResult> AgregarProceso([FromBody] RutaProcesoAgregarDto dto) => Res(await _service.RutaProcesoAgregarAsync(dto, GetCodUsuario(), GetEstacion()));

    [HttpPost("subprocesos")]
    public async Task<IActionResult> AgregarSubproceso([FromBody] RutaSubprocesoAgregarDto dto) => Res(await _service.RutaSubprocesoAgregarAsync(dto, GetCodUsuario(), GetEstacion()));

    [HttpPost("operaciones")]
    public async Task<IActionResult> AgregarOperacion([FromBody] RutaOperacionAgregarDto dto) => Res(await _service.RutaOperacionAgregarAsync(dto, GetCodUsuario(), GetEstacion()));

    [HttpPost("quitar")]
    public async Task<IActionResult> Quitar([FromBody] RutaFilaClaveDto dto) => Res(await _service.RutaQuitarAsync(dto));

    [HttpPost("editar")]
    public async Task<IActionResult> Editar([FromBody] RutaOperacionEditarDto dto) => Res(await _service.RutaOperacionEditarAsync(dto, GetCodUsuario(), GetEstacion()));

    [HttpPost("mover")]
    public async Task<IActionResult> Mover([FromBody] RutaMoverDto dto) => Res(await _service.RutaMoverAsync(dto));

    [HttpPost("tiempo-propio")]
    public async Task<IActionResult> TiempoPropio([FromBody] RutaFilaClaveDto dto) => Res(await _service.RutaTiempoPropioAsync(dto, GetCodUsuario(), GetEstacion()));

    [HttpPost("recalcular")]
    public async Task<IActionResult> Recalcular([FromBody] RutaRecalcularDto dto) => Res(await _service.RutaRecalcularAsync(dto, GetCodUsuario(), GetEstacion()));

    [HttpGet("{ruta}/sam-por-categoria")]
    public async Task<IActionResult> SamPorCategoria(string ruta) => Res(await _service.RutaSamPorCategoriaAsync(ruta));

    [HttpGet("{ruta}/sam-por-clase")]
    public async Task<IActionResult> SamPorClase(string ruta) => Res(await _service.RutaSamPorClaseAsync(ruta));

    [HttpGet("{ruta}/precedencias")]
    public async Task<IActionResult> Precedencias(string ruta) => Res(await _service.PrecedenciasAsync(ruta));

    [HttpPost("precedencias/generar")]
    public async Task<IActionResult> GenerarPrecedencias([FromBody] RutaPrecedenciasGenerarDto dto) => Res(await _service.PrecedenciasGenerarAsync(dto, GetCodUsuario(), GetEstacion()));

    [HttpPut("precedencias")]
    public async Task<IActionResult> GuardarPrecedencias([FromBody] RutaPrecedenciasGuardarDto dto) => Res(await _service.PrecedenciasGuardarAsync(dto, GetCodUsuario(), GetEstacion()));
}
