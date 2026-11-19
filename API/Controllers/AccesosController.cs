using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Safety;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/safety/accesos")]
[ExigePermiso("SAFETY.ACCESOS")]
public class AccesosController : BaseController
{
    private readonly IAccesosService _service;
    public AccesosController(IAccesosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_SUCURSAL}/{COD_ZONIFICACION}/{COD_ENTIDAD}")]
    public async Task<IActionResult> GetById(string COD_SUCURSAL, string COD_ZONIFICACION, decimal COD_ENTIDAD)
    { var r = await _service.GetByIdAsync(COD_SUCURSAL, COD_ZONIFICACION, COD_ENTIDAD); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] AccesosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] AccesosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] AccesosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_SUCURSAL}/{COD_ZONIFICACION}/{COD_ENTIDAD}")]
    public async Task<IActionResult> Delete(string COD_SUCURSAL, string COD_ZONIFICACION, decimal COD_ENTIDAD)
    { var r = await _service.DeleteAsync(COD_SUCURSAL, COD_ZONIFICACION, COD_ENTIDAD); return r.Success ? Ok(r) : BadRequest(r); }

}