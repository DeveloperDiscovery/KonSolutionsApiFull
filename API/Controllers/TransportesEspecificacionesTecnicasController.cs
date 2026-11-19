using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/transportes-especificaciones-tecnicas")]
[ExigePermiso("LOGISTICA.TRANSPORTES_ESPECIFICACIONES_TECNICAS")]
public class TransportesEspecificacionesTecnicasController : BaseController
{
    private readonly ITransportesEspecificacionesTecnicasService _service;
    public TransportesEspecificacionesTecnicasController(ITransportesEspecificacionesTecnicasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string COD_TRANSPORTE)
    { var r = await _service.GetAllAsync(COD_TRANSPORTE); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TRANSPORTE}/{COD_ESPECIFICACION_TECNICA}")]
    public async Task<IActionResult> GetById(string COD_TRANSPORTE, string COD_ESPECIFICACION_TECNICA)
    { var r = await _service.GetByIdAsync(COD_TRANSPORTE, COD_ESPECIFICACION_TECNICA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] string COD_TRANSPORTE)
    { var r = await _service.GetComboboxAsync(COD_TRANSPORTE); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TransportesEspecificacionesTecnicasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TransportesEspecificacionesTecnicasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TransportesEspecificacionesTecnicasUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_TRANSPORTE}/{COD_ESPECIFICACION_TECNICA}")]
    public async Task<IActionResult> Delete(string COD_TRANSPORTE, string COD_ESPECIFICACION_TECNICA)
    { var r = await _service.DeleteAsync(COD_TRANSPORTE, COD_ESPECIFICACION_TECNICA); return r.Success ? Ok(r) : BadRequest(r); }

}