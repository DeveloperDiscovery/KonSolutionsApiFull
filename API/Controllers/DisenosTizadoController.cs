using System.Text;
using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Moldes (piezas del patron) y tizados 2D de una version de diseno (script SQL 115). Permisos: los de la version
/// (INGENIERIA.DISENOS_VERSION): ver = GET, grabar = POST.</summary>
[Authorize]
[Route("api/v1/ingenieria/disenos-tizado")]
[ExigePermiso("INGENIERIA.DISENOS_VERSION")]
public class DisenosTizadoController : BaseController
{
    private readonly IDisenosTizadoService _service;
    public DisenosTizadoController(IDisenosTizadoService service) => _service = service;

    [HttpGet("moldes/{codDiseno}/{numVersion:int}")]
    public async Task<IActionResult> Moldes(string codDiseno, int numVersion)
    { var r = await _service.MoldesAsync(codDiseno, numVersion); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("moldes/guardar")]
    public async Task<IActionResult> GuardarMoldes([FromBody] DisenosMoldesGuardarDto dto)
    { var r = await _service.GuardarMoldesAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("moldes/eliminar")]
    public async Task<IActionResult> EliminarMolde([FromBody] DisenosMoldeClaveDto dto)
    { var r = await _service.EliminarMoldeAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("moldes/importar-dxf")]
    [RequestSizeLimit(8_000_000)]
    public async Task<IActionResult> ImportarDxf([FromBody] DisenosMoldeImportarDto dto)
    { var r = await _service.ImportarDxfAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("calcular")]
    public async Task<IActionResult> Calcular([FromBody] DisenosTizadoCalcularDto dto, CancellationToken ct)
    { var r = await _service.CalcularAsync(dto, ct); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("guardar")]
    public async Task<IActionResult> Guardar([FromBody] DisenosTizadoCalcularDto dto, CancellationToken ct)
    { var r = await _service.GuardarTizadoAsync(dto, GetCodUsuario(), GetEstacion(), ct); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("tizados/{codDiseno}/{numVersion:int}")]
    public async Task<IActionResult> Tizados(string codDiseno, int numVersion)
    { var r = await _service.TizadosAsync(codDiseno, numVersion); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("tizados/{codDiseno}/{numVersion:int}/{numSecuencia:int}")]
    public async Task<IActionResult> Tizado(string codDiseno, int numVersion, int numSecuencia)
    { var r = await _service.TizadoAsync(codDiseno, numVersion, numSecuencia); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("tizados/{codDiseno}/{numVersion:int}/{numSecuencia:int}/dxf")]
    public async Task<IActionResult> TizadoDxf(string codDiseno, int numVersion, int numSecuencia)
    {
        var r = await _service.TizadoDxfAsync(codDiseno, numVersion, numSecuencia);
        if (!r.Success) return BadRequest(r);
        return File(Encoding.ASCII.GetBytes(r.Data ?? ""), "application/dxf", $"tizado_{codDiseno}_v{numVersion}_{numSecuencia}.dxf");
    }

    [HttpPost("tizados/eliminar")]
    public async Task<IActionResult> EliminarTizado([FromBody] DisenosMoldeClaveDto dto)
    { var r = await _service.EliminarTizadoAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("tizados/ajustar")]
    public async Task<IActionResult> AjustarTizado([FromBody] DisenosTizadoAjusteDto dto)
    { var r = await _service.AjustarTizadoAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("gradacion/{codDiseno}/{numVersion:int}")]
    public async Task<IActionResult> Reglas(string codDiseno, int numVersion)
    { var r = await _service.ReglasGradacionAsync(codDiseno, numVersion); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("gradacion/previsualizar")]
    public async Task<IActionResult> PrevisualizarGradacion([FromBody] DisenosGradacionDto dto)
    { var r = await _service.PrevisualizarGradacionAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("gradacion/generar")]
    public async Task<IActionResult> GenerarGradacion([FromBody] DisenosGradacionDto dto)
    { var r = await _service.GenerarGradacionAsync(dto, GetCodUsuario(), GetEstacion()); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("gradacion/eliminar")]
    public async Task<IActionResult> EliminarReglaGradacion([FromBody] DisenosGradacionClaveDto dto)
    { var r = await _service.EliminarReglaGradacionAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }
}
