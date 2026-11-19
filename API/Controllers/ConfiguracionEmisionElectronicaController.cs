using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

/// <summary>La configuración de emisión electrónica (SUNAT). Es una sola fila: no hay alta ni baja.
/// Nunca devuelve un secreto: dice qué hay cargado (ver ConfiguracionEmisionElectronica.TIENE_*).</summary>
[Authorize]
[Route("api/v1/estandar/configuracion-emision-electronica")]
[ExigePermiso("ESTANDAR.CONFIGURACION_EMISION_ELECTRONICA")]
public class ConfiguracionEmisionElectronicaController : BaseController
{
    private readonly IConfiguracionEmisionElectronicaService _service;
    public ConfiguracionEmisionElectronicaController(IConfiguracionEmisionElectronicaService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Get()
    { var r = await _service.GetAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ConfiguracionEmisionElectronicaUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }
}
