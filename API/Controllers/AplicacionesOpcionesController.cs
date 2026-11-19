using System.Data;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/seguridad/aplicaciones-opciones")]
[ExigePermiso("SEGURIDAD.APLICACIONES_OPCIONES")]
public class AplicacionesOpcionesController : BaseController
{
    private readonly IAplicacionesOpcionesService _service;
    private readonly IDbConnectionFactory _db;
    public AplicacionesOpcionesController(IAplicacionesOpcionesService service, IDbConnectionFactory db)
    { _service = service; _db = db; }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("by-app")]
    public async Task<IActionResult> GetByApp([FromQuery] string codAplicacion, [FromQuery] string? tipoOpciones = null)
    {
        if (string.IsNullOrWhiteSpace(codAplicacion))
            return BadRequest(ApiResponse<object>.Fail("codAplicacion requerido."));
        try
        {
            using var conn = _db.CreateConnection();
            var data = await conn.QueryAsync<AplicacionesOpciones>(
                SP.SCHEMASeguridad.AplicacionesOpciones.Showall,
                new { COD_APLICACION = codAplicacion },
                commandType: CommandType.StoredProcedure);
            if (!string.IsNullOrWhiteSpace(tipoOpciones))
            {
                var tipos = tipoOpciones.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                data = data.Where(x => tipos.Contains(x.COD_TIPO_OPCION_APLICACION, StringComparer.OrdinalIgnoreCase));
            }
            return Ok(ApiResponse<IEnumerable<AplicacionesOpciones>>.Ok(data));
        }
        catch (Exception ex) { return BadRequest(ApiResponse<object>.Fail(ex.Message)); }
    }

    [HttpGet("{COD_APLICACION}/{COD_OPCION_APLICACION}")]
    public async Task<IActionResult> GetById(string COD_APLICACION, string COD_OPCION_APLICACION)
    { var r = await _service.GetByIdAsync(COD_APLICACION, COD_OPCION_APLICACION); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_SEGURIDAD_APLICACIONES_OPCIONES_LOADCOMBOS: Tipo de opción + Aplicación +
    /// Estados para AplicacionesOpcionesDialog.razor en un solo viaje. codTipoEstado es
    /// opcional (se autorresuelve).</summary>
    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos([FromQuery] int codTipoEstado = 0)
    { var r = await _service.LoadcombosAsync(codTipoEstado); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] AplicacionesOpcionesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [Authorize(Policy = "Administrador")]
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] AplicacionesOpcionesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [Authorize(Policy = "Administrador")]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] AplicacionesOpcionesUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [Authorize(Policy = "Administrador")]
    [HttpDelete("{COD_APLICACION}/{COD_OPCION_APLICACION}")]
    public async Task<IActionResult> Delete(string COD_APLICACION, string COD_OPCION_APLICACION)
    { var r = await _service.DeleteAsync(COD_APLICACION, COD_OPCION_APLICACION); return r.Success ? Ok(r) : BadRequest(r); }

    [Authorize(Policy = "Administrador")]
    [HttpPost("move")]
    public async Task<IActionResult> Move([FromBody] AplicacionesOpcionesMoveDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.MoveAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

}