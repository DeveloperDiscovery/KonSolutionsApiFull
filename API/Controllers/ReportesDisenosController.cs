using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Reportes;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Reporteador: los diseños (listado, ficha o libre por bandas), sus plantillas propias de Word y Excel, y la vista previa.
/// Pantalla Reportes/ReportesDisenos.razor.
///
/// La vista previa genera el reporte por diseño (sin botón): queda protegida por el permiso de esta pantalla.</summary>
[Authorize]
[Route("api/v1/reportes/disenos")]
[ExigePermiso("REPORTES.DISENOS")]
public class ReportesDisenosController : BaseController
{
    private readonly IReportesService _service;
    public ReportesDisenosController(IReportesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] int? NUM_FUENTE)
    { var r = await _service.DisenosAsync(NUM_FUENTE); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_DISENO:int}")]
    public async Task<IActionResult> Uno(int NUM_DISENO)
    { var r = await _service.DisenoAsync(NUM_DISENO); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Las fuentes, para elegir la del diseño (con el permiso de esta pantalla).</summary>
    [HttpGet("fuentes")]
    public async Task<IActionResult> Fuentes()
    { var r = await _service.FuentesAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Una fuente con sus resultados y columnas, para armar el diseño.</summary>
    [HttpGet("fuentes/{NUM_FUENTE:int}")]
    public async Task<IActionResult> Fuente(int NUM_FUENTE)
    { var r = await _service.FuenteAsync(NUM_FUENTE); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Grabar([FromBody] ReporteDisenoDetalle dto)
    { var r = await _service.DisenoGrabarAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_DISENO:int}")]
    public async Task<IActionResult> Eliminar(int NUM_DISENO)
    { var r = await _service.DisenoEliminarAsync(NUM_DISENO); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Convertir a bandas: crea un diseño libre nuevo con el lienzo armado desde este listado o ficha (el
    /// original queda igual). Devuelve el número del diseño nuevo.</summary>
    [HttpPost("{NUM_DISENO:int}/convertir")]
    public async Task<IActionResult> Convertir(int NUM_DISENO)
    { var r = await _service.DisenoConvertirAsync(NUM_DISENO, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    // ── Fórmulas del diseño libre ────────────────────────────────────────────────
    /// <summary>Comprueba una fórmula o condición del editor: errores con línea y columna, qué tipo de valor da y, si se
    /// pide, su valor con la primera fila (ejecuta el SP de la fuente con los valores por defecto).</summary>
    [HttpPost("formulas/comprobar")]
    public async Task<IActionResult> ComprobarFormula([FromBody] ReporteFormulaComprobarDto dto)
    { var r = await _service.FormulaComprobarAsync(dto, GetCodUsuario(), GetCodRol(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Las funciones del lenguaje de fórmulas, con su ayuda y un ejemplo.</summary>
    [HttpGet("formulas/funciones")]
    public IActionResult FuncionesFormula() => Ok(_service.FormulaFunciones());

    /// <summary>La plantilla base (DOC o XLS): el diseño con marcas en vez de datos, para editarla y subirla.</summary>
    [HttpGet("{NUM_DISENO:int}/plantilla-base/{formato}")]
    public async Task<IActionResult> PlantillaBase(int NUM_DISENO, string formato)
    { var r = await _service.PlantillaBaseAsync(NUM_DISENO, formato, GetCodUsuario()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>La plantilla propia que está guardada.</summary>
    [HttpGet("{NUM_DISENO:int}/plantilla/{formato}")]
    public async Task<IActionResult> Plantilla(int NUM_DISENO, string formato)
    { var r = await _service.PlantillaDescargarAsync(NUM_DISENO, formato); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Sube la plantilla propia. Devuelve las marcas que no existen en la fuente como avisos.</summary>
    [HttpPost("{NUM_DISENO:int}/plantilla/{formato}")]
    [RequestSizeLimit(8 * 1024 * 1024)]
    public async Task<IActionResult> SubirPlantilla(int NUM_DISENO, string formato, [FromBody] ReportePlantillaDto dto)
    { var r = await _service.PlantillaSubirAsync(NUM_DISENO, formato, dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Quita la plantilla propia: el formato vuelve a salir del diseño.</summary>
    [HttpDelete("{NUM_DISENO:int}/plantilla/{formato}")]
    public async Task<IActionResult> QuitarPlantilla(int NUM_DISENO, string formato)
    { var r = await _service.PlantillaQuitarAsync(NUM_DISENO, formato, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    // ── Vista previa ─────────────────────────────────────────────────────────────
    [HttpGet("{NUM_DISENO:int}/dialogo")]
    public async Task<IActionResult> Dialogo(int NUM_DISENO)
    { var r = await _service.DialogoAsync(NUM_DISENO, null, null, null, null, GetCodUsuario()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("{NUM_DISENO:int}/combo")]
    public async Task<IActionResult> Combo(int NUM_DISENO, [FromBody] ReporteComboDto dto)
    {
        dto.NUM_DISENO = NUM_DISENO;
        dto.COD_APLICACION = dto.COD_OPCION_APLICACION = dto.COD_BOTON_OPCION = null;
        var r = await _service.ComboAsync(dto, GetCodUsuario(), GetCodRol(), GetEstacion());
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpPost("{NUM_DISENO:int}/generar")]
    public async Task<IActionResult> Generar(int NUM_DISENO, [FromBody] ReporteGenerarDto dto)
    {
        dto.NUM_DISENO = NUM_DISENO;
        dto.COD_APLICACION = dto.COD_OPCION_APLICACION = dto.COD_BOTON_OPCION = null;
        var r = await _service.GenerarAsync(dto, GetCodUsuario(), GetCodRol(), GetEstacion());
        return r.Success ? Ok(r) : BadRequest(r);
    }
}
