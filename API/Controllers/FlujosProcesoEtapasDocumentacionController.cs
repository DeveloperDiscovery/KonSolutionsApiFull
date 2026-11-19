using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/entorno/flujos-proceso-etapas-documentacion")]
[ExigePermiso("ENTORNO.FLUJOS_PROCESO_ETAPAS_DOCUMENTACION")]
public class FlujosProcesoEtapasDocumentacionController : BaseController
{
    private readonly IFlujosProcesoEtapasDocumentacionService _service;
    public FlujosProcesoEtapasDocumentacionController(IFlujosProcesoEtapasDocumentacionService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_FLUJO_PROCESO}/{NUM_ETAPA}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> GetById(int NUM_FLUJO_PROCESO, int NUM_ETAPA, int NUM_SECUENCIA)
    { var r = await _service.GetByIdAsync(NUM_FLUJO_PROCESO, NUM_ETAPA, NUM_SECUENCIA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] FlujosProcesoEtapasDocumentacionSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] FlujosProcesoEtapasDocumentacionInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] FlujosProcesoEtapasDocumentacionUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_FLUJO_PROCESO}/{NUM_ETAPA}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> Delete(int NUM_FLUJO_PROCESO, int NUM_ETAPA, int NUM_SECUENCIA)
    { var r = await _service.DeleteAsync(NUM_FLUJO_PROCESO, NUM_ETAPA, NUM_SECUENCIA); return r.Success ? Ok(r) : BadRequest(r); }

}