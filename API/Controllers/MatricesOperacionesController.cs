using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Matrices de operaciones (script SQL 130): arboles modelo, ligados opcionalmente a una sub familia. Su arbol se ve y se edita con las
/// rutas con tiempos (una matriz es una ruta: ahi hace falta tambien el permiso de INGENIERIA.RUTAS_TIEMPOS). Permisos: los de INGENIERIA.MATRICES_OPERACIONES (script 131).</summary>
[Authorize]
[Route("api/v1/ingenieria/matrices")]
[ExigePermiso("INGENIERIA.MATRICES_OPERACIONES")]
public class MatricesOperacionesController : BaseController
{
    private readonly IMatricesOperacionesService _service;
    public MatricesOperacionesController(IMatricesOperacionesService service) => _service = service;

    private IActionResult Res<T>(KonSolutionsApi.Common.ApiResponse<T> r) => r.Success ? Ok(r) : BadRequest(r);

    [HttpGet]
    public async Task<IActionResult> Matrices() => Res(await _service.MatricesAsync());

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] MatrizGuardarDto dto) => Res(await _service.MatrizCrearAsync(dto, GetCodUsuario(), GetEstacion()));

    [HttpPut]
    public async Task<IActionResult> Actualizar([FromBody] MatrizGuardarDto dto) => Res(await _service.MatrizActualizarAsync(dto, GetCodUsuario(), GetEstacion()));

    [HttpDelete("{cod}")]
    public async Task<IActionResult> Eliminar(string cod) => Res(await _service.MatrizEliminarAsync(cod));

    [HttpPost("duplicar")]
    public async Task<IActionResult> Duplicar([FromBody] MatrizDuplicarDto dto) => Res(await _service.MatrizDuplicarAsync(dto, GetCodUsuario(), GetEstacion()));
}

/// <summary>Asignar una matriz a una version de diseno (copia independiente y editable), guardar la ruta de una version como matriz y
/// separar las rutas que comparten varias versiones (script SQL 130). Permisos: los de INGENIERIA.DISENOS_VERSION.</summary>
[Authorize]
[Route("api/v1/ingenieria/disenos-matrices")]
[ExigePermiso("INGENIERIA.DISENOS_VERSION")]
public class DisenosMatricesController : BaseController
{
    private readonly IMatricesOperacionesService _service;
    public DisenosMatricesController(IMatricesOperacionesService service) => _service = service;

    private IActionResult Res<T>(KonSolutionsApi.Common.ApiResponse<T> r) => r.Success ? Ok(r) : BadRequest(r);

    [HttpGet("{codDiseno}/{numVersion:int}/matrices")]
    public async Task<IActionResult> MatricesParaVersion(string codDiseno, int numVersion) => Res(await _service.MatricesParaVersionAsync(codDiseno, numVersion));

    [HttpGet("{codDiseno}/{numVersion:int}/ruta")]
    public async Task<IActionResult> RutaOperaciones(string codDiseno, int numVersion) => Res(await _service.RutaOperacionesInfoAsync(codDiseno, numVersion));

    [HttpPost("asignar")]
    public async Task<IActionResult> Asignar([FromBody] MatrizAsignarDto dto) => Res(await _service.MatrizAsignarAsync(dto, GetCodUsuario(), GetEstacion()));

    [HttpPost("guardar-como-matriz")]
    public async Task<IActionResult> GuardarComoMatriz([FromBody] GuardarComoMatrizDto dto) => Res(await _service.GuardarComoMatrizAsync(dto, GetCodUsuario(), GetEstacion()));

    [HttpPost("{codDiseno}/{numVersion:int}/independizar")]
    public async Task<IActionResult> Independizar(string codDiseno, int numVersion) => Res(await _service.RutaIndependizarAsync(codDiseno, numVersion, GetCodUsuario(), GetEstacion()));

    [HttpGet("rutas-compartidas")]
    public async Task<IActionResult> RutasCompartidas() => Res(await _service.RutasCompartidasAsync());
}
