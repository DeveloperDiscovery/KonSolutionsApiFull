using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/motivos")]
[ExigePermiso("ESTANDAR.MOTIVOS")]
public class MotivosController : BaseController
{
    private readonly IMotivosService _service;
    public MotivosController(IMotivosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("por-tipo/{COD_TIPO_MOTIVO}")]
    public async Task<IActionResult> GetByTipo(int COD_TIPO_MOTIVO)
    { var r = await _service.GetByTipoAsync(COD_TIPO_MOTIVO); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_TIPO_MOTIVO}/{COD_MOTIVO}")]
    public async Task<IActionResult> GetById(int COD_TIPO_MOTIVO, int COD_MOTIVO)
    { var r = await _service.GetByIdAsync(COD_TIPO_MOTIVO, COD_MOTIVO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] MotivosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] MotivosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] MotivosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_TIPO_MOTIVO}/{COD_MOTIVO}")]
    public async Task<IActionResult> Delete(int COD_TIPO_MOTIVO, int COD_MOTIVO)
    { var r = await _service.DeleteAsync(COD_TIPO_MOTIVO, COD_MOTIVO); return r.Success ? Ok(r) : BadRequest(r); }

}