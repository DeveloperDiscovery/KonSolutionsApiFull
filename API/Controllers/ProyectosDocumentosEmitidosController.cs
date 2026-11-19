using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.API.Filters;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Documentos emitidos de un proyecto (actas, contrato, cronograma a aprobar): se crean desde una plantilla, se editan, se envian a aprobar y se emiten con su PDF congelado.
/// El permiso es el del proyecto (PROYECTOS.PROYECTOS).</summary>
[Authorize]
[Route("api/v1/proyectos/expediente")]
[ExigePermiso("PROYECTOS.PROYECTOS")]
public class ProyectosDocumentosEmitidosController : BaseController
{
    private readonly IProyectosDocumentosEmitidosService _service;
    public ProyectosDocumentosEmitidosController(IProyectosDocumentosEmitidosService service) => _service = service;

    private IActionResult Resp<T>(ApiResponse<T> r) => r.Success ? Ok(r) : BadRequest(r);

    [HttpGet("documentos-emitidos/plantillas")]
    public async Task<IActionResult> Plantillas() => Resp(await _service.PlantillasAsync());

    [HttpGet("{num:int}/documentos-emitidos")]
    public async Task<IActionResult> Listar(int num) => Resp(await _service.ListarAsync(num));

    [HttpGet("{num:int}/documentos-emitidos/{secuencia:int}")]
    public async Task<IActionResult> Obtener(int num, int secuencia) => Resp(await _service.ObtenerAsync(num, secuencia));

    [HttpPost("{num:int}/documentos-emitidos")]
    public async Task<IActionResult> Nuevo(int num, [FromBody] DocumentoEmitidoNuevoDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState); return Resp(await _service.NuevoAsync(num, dto, GetCodUsuario(), GetEstacion())); }

    [HttpPut("{num:int}/documentos-emitidos/{secuencia:int}")]
    public async Task<IActionResult> Guardar(int num, int secuencia, [FromBody] DocumentoEmitidoGuardarDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState); return Resp(await _service.GuardarAsync(num, secuencia, dto, GetCodUsuario(), GetEstacion())); }

    [HttpDelete("{num:int}/documentos-emitidos/{secuencia:int}")]
    public async Task<IActionResult> Eliminar(int num, int secuencia) => Resp(await _service.EliminarAsync(num, secuencia));

    [HttpPost("{num:int}/documentos-emitidos/{secuencia:int}/enviar")]
    public async Task<IActionResult> Enviar(int num, int secuencia, [FromBody] DocumentoEmitidoEnviarDto dto)
        => Resp(await _service.EnviarAsync(num, secuencia, dto, GetCodRol(), GetCodUsuario(), GetEstacion()));

    [HttpPost("{num:int}/documentos-emitidos/{secuencia:int}/emitir")]
    public async Task<IActionResult> Emitir(int num, int secuencia, [FromBody] DocumentoEmitidoEmitirDto dto)
        => Resp(await _service.EmitirAsync(num, secuencia, dto, GetCodRol(), GetCodUsuario(), GetEstacion()));

    [HttpPost("{num:int}/documentos-emitidos/{secuencia:int}/nueva-version")]
    public async Task<IActionResult> NuevaVersion(int num, int secuencia) => Resp(await _service.NuevaVersionAsync(num, secuencia, GetCodUsuario(), GetEstacion()));

    [HttpPost("{num:int}/documentos-emitidos/{secuencia:int}/anular")]
    public async Task<IActionResult> Anular(int num, int secuencia, [FromBody] DocumentoEmitidoAnularDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState); return Resp(await _service.AnularAsync(num, secuencia, dto, GetCodUsuario(), GetEstacion())); }

    /// <summary>El texto con los datos del proyecto ya puestos (vista previa del texto mientras se edita).</summary>
    [HttpPost("{num:int}/documentos-emitidos/texto")]
    public async Task<IActionResult> Texto(int num, [FromBody] DocumentoEmitidoTextoDto dto) => Resp(await _service.TextoResueltoAsync(num, dto));

    /// <summary>El PDF: el congelado si el documento esta emitido; si no, una vista previa con el texto actual.</summary>
    [HttpGet("{num:int}/documentos-emitidos/{secuencia:int}/pdf")]
    public async Task<IActionResult> Pdf(int num, int secuencia)
    {
        var r = await _service.PdfAsync(num, secuencia, GetCodRol(), GetCodUsuario(), GetEstacion());
        if (!r.Success || r.Data is null) return BadRequest(r);
        Response.Headers["X-Nombre-Archivo"] = Uri.EscapeDataString(r.Data.NOMBRE);
        return File(r.Data.CONTENIDO, "application/pdf");
    }
}
