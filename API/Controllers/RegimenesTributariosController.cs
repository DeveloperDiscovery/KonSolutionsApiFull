using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Sunat;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/sunat/regimenes-tributarios")]
[ExigePermiso("SUNAT.REGIMENES_TRIBUTARIOS")]
public class RegimenesTributariosController : BaseController
{
    private readonly IRegimenesTributariosService _service;
    public RegimenesTributariosController(IRegimenesTributariosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_REGIMEN_TRIBUTARIO}")]
    public async Task<IActionResult> GetById(int COD_REGIMEN_TRIBUTARIO)
    { var r = await _service.GetByIdAsync(COD_REGIMEN_TRIBUTARIO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] RegimenesTributariosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] RegimenesTributariosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] RegimenesTributariosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_REGIMEN_TRIBUTARIO}")]
    public async Task<IActionResult> Delete(int COD_REGIMEN_TRIBUTARIO)
    { var r = await _service.DeleteAsync(COD_REGIMEN_TRIBUTARIO); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_SUNAT_REGIMENES_TRIBUTARIOS_LOADCOMBOS: tipos de comprobante de pago,
    /// monedas, tipos de impuesto y tipos de valor de cálculo para el diálogo de Regímenes
    /// Tributarios en un solo viaje.</summary>
    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos()
    { var r = await _service.LoadcombosAsync(); return r.Success ? Ok(r) : BadRequest(r); }

}