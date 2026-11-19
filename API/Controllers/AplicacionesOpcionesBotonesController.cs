using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/seguridad/aplicaciones-opciones-botones")]
[ExigePermiso("SEGURIDAD.APLICACIONES_OPCIONES_BOTONES")]
public class AplicacionesOpcionesBotonesController : BaseController
{
    private readonly IAplicacionesOpcionesBotonesService _service;
    private readonly ILogger<AplicacionesOpcionesBotonesController> _log;
    public AplicacionesOpcionesBotonesController(IAplicacionesOpcionesBotonesService service, ILogger<AplicacionesOpcionesBotonesController> log)
    { _service = service; _log = log; }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codAplicacion, [FromQuery] string codOpcionAplicacion)
    {
        if (string.IsNullOrWhiteSpace(codAplicacion) || string.IsNullOrWhiteSpace(codOpcionAplicacion))
            return BadRequest(new { Success = false, Message = "codAplicacion y codOpcionAplicacion son requeridos." });
        var r = await _service.GetAllAsync(codAplicacion, codOpcionAplicacion);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpGet("{COD_APLICACION}/{COD_OPCION_APLICACION}/{COD_BOTON_OPCION}")]
    public async Task<IActionResult> GetById(string COD_APLICACION, string COD_OPCION_APLICACION, string COD_BOTON_OPCION)
    { var r = await _service.GetByIdAsync(COD_APLICACION, COD_OPCION_APLICACION, COD_BOTON_OPCION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_SEGURIDAD_APLICACIONES_OPCIONES_BOTONES_LOADCOMBOS: Botón estándar +
    /// Estados para AplicacionesOpcionesBotonesDialog.razor en un solo viaje. codTipoEstado
    /// es opcional (se autorresuelve).</summary>
    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos([FromQuery] int codTipoEstado = 0)
    { var r = await _service.LoadcombosAsync(codTipoEstado); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] AplicacionesOpcionesBotonesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] AplicacionesOpcionesBotonesInsertDto dto)
    {
        _log.LogInformation("[CTRL-INSERT] Recibido: APL={Apl} OPC={Opc} BTN={Btn} EST={Std} TIPO_EST={TE} EST={E}",
            dto.COD_APLICACION, dto.COD_OPCION_APLICACION, dto.COD_BOTON_OPCION,
            dto.COD_BOTON_OPCION_ESTANDAR, dto.COD_TIPO_ESTADO, dto.COD_ESTADO);

        if (!ModelState.IsValid)
        {
            var errores = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            _log.LogWarning("[CTRL-INSERT] ModelState INVALIDO: {Err}", errores);
            return BadRequest(ModelState);
        }

        var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
        _log.LogInformation("[CTRL-INSERT] Resultado: Success={Ok} Msg={Msg}", r.Success, r.Message);
        return r.Success ? StatusCode(201, r) : BadRequest(r);
    }

    [HttpDelete("{COD_APLICACION}/{COD_OPCION_APLICACION}/{COD_BOTON_OPCION}")]
    public async Task<IActionResult> Delete(string COD_APLICACION, string COD_OPCION_APLICACION, string COD_BOTON_OPCION)
    { var r = await _service.DeleteAsync(COD_APLICACION, COD_OPCION_APLICACION, COD_BOTON_OPCION); return r.Success ? Ok(r) : BadRequest(r); }

}