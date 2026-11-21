using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.API.Filters;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Formulacion;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Componentes de formulacion: las categorias de la paleta del constructor de formulas (script SQL 145). Permisos: FORMULACION.COMPONENTES (script 146).</summary>
[Authorize]
[Route("api/v1/formulacion/componentes")]
[ExigePermiso("FORMULACION.COMPONENTES")]
public class FormulacionComponentesController : BaseController
{
    private readonly IFormulacionService _service;
    public FormulacionComponentesController(IFormulacionService service) => _service = service;
    private IActionResult Res<T>(ApiResponse<T> r) => r.Success ? Ok(r) : BadRequest(r);

    [HttpGet]
    public async Task<IActionResult> Listar() => Res(await _service.ComponentesAsync());

    [HttpGet("{cod}")]
    public async Task<IActionResult> Obtener(string cod) { var r = await _service.ComponenteAsync(cod); return r.Success ? Ok(r) : NotFound(r); }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] FormulacionComponenteGuardarDto dto) { var r = await _service.ComponenteCrearAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Actualizar([FromBody] FormulacionComponenteGuardarDto dto) => Res(await _service.ComponenteActualizarAsync(dto, GetCodUsuario(), GetEstacion()));

    [HttpDelete("{cod}")]
    public async Task<IActionResult> Eliminar(string cod) => Res(await _service.ComponenteEliminarAsync(cod));
}

/// <summary>Elementos de formulacion: las fichas de cada componente. Permisos: FORMULACION.ELEMENTOS (script 146).</summary>
[Authorize]
[Route("api/v1/formulacion/elementos")]
[ExigePermiso("FORMULACION.ELEMENTOS")]
public class FormulacionElementosController : BaseController
{
    private readonly IFormulacionService _service;
    public FormulacionElementosController(IFormulacionService service) => _service = service;
    private IActionResult Res<T>(ApiResponse<T> r) => r.Success ? Ok(r) : BadRequest(r);

    /// <summary>Todos los elementos, o los de un componente (?componente=CONSM).</summary>
    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] string? componente) => Res(await _service.ElementosAsync(componente));

    [HttpGet("{componente}/{elemento}")]
    public async Task<IActionResult> Obtener(string componente, string elemento) { var r = await _service.ElementoAsync(componente, elemento); return r.Success ? Ok(r) : NotFound(r); }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] FormulacionElementoGuardarDto dto) { var r = await _service.ElementoCrearAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Actualizar([FromBody] FormulacionElementoGuardarDto dto) => Res(await _service.ElementoActualizarAsync(dto, GetCodUsuario(), GetEstacion()));

    [HttpDelete("{componente}/{elemento}")]
    public async Task<IActionResult> Eliminar(string componente, string elemento) => Res(await _service.ElementoEliminarAsync(componente, elemento));
}

/// <summary>Formulas de consumo: cabecera y fichas en orden, armadas con el constructor (script SQL 145). Permisos: FORMULACION.FORMULAS (script 146).
/// Validar y consultar la paleta no cambian nada, pero van por POST/GET segun lleven cuerpo o no: la paleta pide el permiso de ver y validar el de crear.</summary>
[Authorize]
[Route("api/v1/formulacion/formulas")]
[ExigePermiso("FORMULACION.FORMULAS")]
public class FormulacionFormulasController : BaseController
{
    private readonly IFormulacionService _service;
    public FormulacionFormulasController(IFormulacionService service) => _service = service;
    private IActionResult Res<T>(ApiResponse<T> r) => r.Success ? Ok(r) : BadRequest(r);

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] string? search) => Res(await _service.FormulasAsync(search));

    [HttpGet("{num:int}")]
    public async Task<IActionResult> Obtener(int num) { var r = await _service.FormulaAsync(num); return r.Success ? Ok(r) : NotFound(r); }

    /// <summary>Componentes habilitados con sus fichas. Las de especificaciones tecnicas se filtran por las sub familias que se indiquen.</summary>
    [HttpGet("paleta")]
    public async Task<IActionResult> Paleta([FromQuery] PaletaConsultaDto dto) => Res(await _service.PaletaAsync(dto));

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] FormulaGuardarDto dto) { dto.NUM_FORMULACION = null; var r = await _service.FormulaGuardarAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Actualizar([FromBody] FormulaGuardarDto dto) => Res(await _service.FormulaGuardarAsync(dto, GetCodUsuario(), GetEstacion()));

    [HttpDelete("{num:int}")]
    public async Task<IActionResult> Eliminar(int num) => Res(await _service.FormulaEliminarAsync(num));

    [HttpPost("{num:int}/copiar")]
    public async Task<IActionResult> Copiar(int num) { var r = await _service.FormulaCopiarAsync(num, GetCodUsuario(), GetEstacion()); return r.Success ? StatusCode(201, r) : BadRequest(r); }

    /// <summary>Revisa la forma de una formula sin grabarla. Devuelve la lista de problemas (vacia si es valida).</summary>
    [HttpPost("validar")]
    public async Task<IActionResult> Validar([FromBody] FormulaValidarDto dto) => Res(await _service.FormulaValidarAsync(dto));
}
