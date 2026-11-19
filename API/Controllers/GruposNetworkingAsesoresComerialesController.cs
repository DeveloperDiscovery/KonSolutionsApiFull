using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Networking;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/networking/grupos-networking-asesores-comeriales")]
[ExigePermiso("NETWORKING.GRUPOS_NETWORKING_ASESORES_COMERIALES")]
public class GruposNetworkingAsesoresComerialesController : BaseController
{
    private readonly IGruposNetworkingAsesoresComerialesService _service;
    public GruposNetworkingAsesoresComerialesController(IGruposNetworkingAsesoresComerialesService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_GRUPO_NETWORKING}/{COD_ENTIDAD_ASESOR_COMERCIAL}")]
    public async Task<IActionResult> GetById(int NUM_GRUPO_NETWORKING, decimal COD_ENTIDAD_ASESOR_COMERCIAL)
    { var r = await _service.GetByIdAsync(NUM_GRUPO_NETWORKING, COD_ENTIDAD_ASESOR_COMERCIAL); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] GruposNetworkingAsesoresComerialesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] GruposNetworkingAsesoresComerialesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] GruposNetworkingAsesoresComerialesUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{NUM_GRUPO_NETWORKING}/{COD_ENTIDAD_ASESOR_COMERCIAL}")]
    public async Task<IActionResult> Delete(int NUM_GRUPO_NETWORKING, decimal COD_ENTIDAD_ASESOR_COMERCIAL)
    { var r = await _service.DeleteAsync(NUM_GRUPO_NETWORKING, COD_ENTIDAD_ASESOR_COMERCIAL); return r.Success ? Ok(r) : BadRequest(r); }

}