using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Estructura de una version de diseno (sectores, componentes, insumos y subensambles de hasta 10 niveles),
/// bloqueo/desbloqueo de la version y copia de versiones o segmentos. Script SQL 107.
/// Permisos: ver la estructura = GET de la version; todo lo que la cambia = POST/PUT/DELETE de la misma tabla.</summary>
[Authorize]
[Route("api/v1/ingenieria/disenos-estructura")]
[ExigePermiso("INGENIERIA.DISENOS_VERSION")]
public class DisenosEstructuraController : BaseController
{
    private readonly IDisenosEstructuraService _service;
    public DisenosEstructuraController(IDisenosEstructuraService service) => _service = service;

    [HttpGet("{codDiseno}/{numVersion:int}")]
    public async Task<IActionResult> Estructura(string codDiseno, int numVersion, [FromQuery] int maxNivel = 10)
    { var r = await _service.EstructuraAsync(codDiseno, numVersion, maxNivel); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("sector")]
    public async Task<IActionResult> SectorGuardar([FromBody] DisenosEstructuraSectorDto dto)
    { var r = await _service.SectorGuardarAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("sector/{codDiseno}/{numVersion:int}/{codSector}")]
    public async Task<IActionResult> SectorEliminar(string codDiseno, int numVersion, string codSector)
    { var r = await _service.SectorEliminarAsync(codDiseno, numVersion, codSector); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("componente")]
    public async Task<IActionResult> ComponenteAgregar([FromBody] DisenosEstructuraComponenteDto dto)
    { var r = await _service.ComponenteAgregarAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("componente/quitar")]
    public async Task<IActionResult> ComponenteQuitar([FromBody] DisenosEstructuraComponenteDto dto)
    { var r = await _service.ComponenteQuitarAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("componente/cambiar")]
    public async Task<IActionResult> ComponenteCambiar([FromBody] DisenosEstructuraComponenteCambiarDto dto)
    { var r = await _service.ComponenteCambiarAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("insumo")]
    public async Task<IActionResult> InsumoGuardar([FromBody] DisenosEstructuraInsumoDto dto)
    { var r = await _service.InsumoGuardarAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("insumo/eliminar")]
    public async Task<IActionResult> InsumoEliminar([FromBody] DisenosEstructuraInsumoClaveDto dto)
    { var r = await _service.InsumoEliminarAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // ---------- formulas de consumo en las lineas (script 147) ----------
    /// <summary>Formulas ACTIVAS que se pueden asignar a la linea (las de la sub familia del insumo y las genericas).</summary>
    [HttpGet("formula/disponibles")]
    public async Task<IActionResult> FormulaDisponibles([FromQuery] DisenosEstructuraLineaClaveDto linea)
    { var r = await _service.FormulaDisponiblesAsync(linea); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>La formula asignada a la linea con sus fichas y el ultimo CAN_TOTAL.</summary>
    [HttpGet("formula")]
    public async Task<IActionResult> FormulaLinea([FromQuery] DisenosEstructuraLineaClaveDto linea)
    { var r = await _service.FormulaLineaAsync(linea); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("formula/asignar")]
    public async Task<IActionResult> FormulaAsignar([FromBody] DisenosEstructuraFormulaAsignarDto dto)
    { var r = await _service.FormulaAsignarAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Edita la copia de la formula de la linea (fichas, constantes, orden) y la recalcula.</summary>
    [HttpPost("formula/guardar")]
    public async Task<IActionResult> FormulaGuardar([FromBody] DisenosEstructuraFormulaGuardarDto dto)
    { var r = await _service.FormulaGuardarAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("formula/quitar")]
    public async Task<IActionResult> FormulaQuitar([FromBody] DisenosEstructuraLineaClaveDto linea)
    { var r = await _service.FormulaQuitarAsync(linea, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("formula/calcular")]
    public async Task<IActionResult> FormulaCalcular([FromBody] DisenosEstructuraFormulaCalcularDto dto)
    { var r = await _service.FormulaCalcularAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("formula/calcular-version")]
    public async Task<IActionResult> FormulaCalcularVersion([FromBody] DisenosVersionClaveDto dto, [FromQuery] bool guardar = true)
    { var r = await _service.FormulaCalcularVersionAsync(dto.COD_DISENO, dto.NUM_VERSION, guardar, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Las formulas de las lineas de la version (y de sus subensambles) para la ficha del diagrama.</summary>
    [HttpGet("formula/diagrama/{codDiseno}/{numVersion:int}")]
    public async Task<IActionResult> FormulaDiagrama(string codDiseno, int numVersion)
    { var r = await _service.FormulaDiagramaAsync(codDiseno, numVersion); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Tipos de material de la estructura (01 Materia prima ... 05 Herramientas y utillaje), solo los habilitados.</summary>
    [HttpGet("tipos-material")]
    public async Task<IActionResult> TiposMaterial()
    { var r = await _service.TiposMaterialAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Indicadores del arbol: insumos pivote, formula asignada y tizado (de la version y de sus subensambles).</summary>
    [HttpGet("indicadores/{codDiseno}/{numVersion:int}")]
    public async Task<IActionResult> Indicadores(string codDiseno, int numVersion)
    { var r = await _service.IndicadoresAsync(codDiseno, numVersion); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("bloquear")]
    public async Task<IActionResult> Bloquear([FromBody] DisenosVersionClaveDto dto)
    { var r = await _service.BloquearAsync(dto.COD_DISENO, dto.NUM_VERSION, GetCodUsuario()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("desbloquear")]
    public async Task<IActionResult> Desbloquear([FromBody] DisenosVersionClaveDto dto)
    { var r = await _service.DesbloquearAsync(dto.COD_DISENO, dto.NUM_VERSION, GetCodUsuario()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("copiar-version")]
    public async Task<IActionResult> CopiarVersion([FromBody] DisenosVersionCopiarDto dto)
    { var r = await _service.CopiarVersionAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("copiar-segmento")]
    public async Task<IActionResult> CopiarSegmento([FromBody] DisenosSegmentoCopiarDto dto)
    { var r = await _service.CopiarSegmentoAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    // ---------- ruta de produccion de la version (script 112) ----------
    [HttpGet("ruta-catalogos")]
    public async Task<IActionResult> RutaCatalogos()
    { var r = await _service.RutaCatalogosAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("ruta/crear")]
    public async Task<IActionResult> RutaCrear([FromBody] DisenosRutaCrearDto dto)
    { var r = await _service.RutaCrearAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("ruta/asignar")]
    public async Task<IActionResult> RutaAsignar([FromBody] DisenosRutaAsignarDto dto)
    { var r = await _service.RutaAsignarAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("ruta/copiar")]
    public async Task<IActionResult> RutaCopiar([FromBody] DisenosRutaCopiarDto dto)
    { var r = await _service.RutaCopiarAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

}
