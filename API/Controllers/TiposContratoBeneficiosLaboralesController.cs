using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/rrhh/tipos-contrato-beneficios-laborales")]
[ExigePermiso("RRHH.TIPOS_CONTRATO_BENEFICIOS_LABORALES")]
public class TiposContratoBeneficiosLaboralesController : BaseController
{
    private readonly ITiposContratoBeneficiosLaboralesService _service;
    public TiposContratoBeneficiosLaboralesController(ITiposContratoBeneficiosLaboralesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TIPO_CONTRATO}/{COD_BENEFICIO_LABORAL}")]
    public async Task<IActionResult> GetById(string COD_TIPO_CONTRATO, string COD_BENEFICIO_LABORAL)
    { var r = await _service.GetByIdAsync(COD_TIPO_CONTRATO, COD_BENEFICIO_LABORAL); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] TiposContratoBeneficiosLaboralesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TiposContratoBeneficiosLaboralesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_TIPO_CONTRATO}/{COD_BENEFICIO_LABORAL}")]
    public async Task<IActionResult> Delete(string COD_TIPO_CONTRATO, string COD_BENEFICIO_LABORAL)
    { var r = await _service.DeleteAsync(COD_TIPO_CONTRATO, COD_BENEFICIO_LABORAL); return r.Success ? Ok(r) : BadRequest(r); }

}