using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/estados")]
[ExigePermiso("ESTANDAR.ESTADOS_FROM_ESTADOS_ESTANDAR")]
public class EstadosController : BaseController
{
    private readonly IEstadosService _service;
    public EstadosController(IEstadosService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("por-tipo/{COD_TIPO_ESTADO}")]
    public async Task<IActionResult> GetByTipo(int COD_TIPO_ESTADO)
    { var r = await _service.GetByTipoAsync(COD_TIPO_ESTADO); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("import-estandar/{COD_TIPO_ESTADO}/{COD_ESTADO_ESTANDAR}")]
    public async Task<IActionResult> ImportFromEstandar(int COD_TIPO_ESTADO, int COD_ESTADO_ESTANDAR)
    {
        var r = await _service.ImportFromEstandarAsync(COD_TIPO_ESTADO, COD_ESTADO_ESTANDAR, GetCodUsuario(), GetEstacion());
        return r.Success ? StatusCode(201, r) : BadRequest(r);
    }

    [HttpGet("{COD_TIPO_ESTADO}/{COD_ESTADO}")]
    public async Task<IActionResult> GetById(int COD_TIPO_ESTADO, int COD_ESTADO)
    { var r = await _service.GetByIdAsync(COD_TIPO_ESTADO, COD_ESTADO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EstadosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EstadosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_TIPO_ESTADO}/{COD_ESTADO}")]
    public async Task<IActionResult> Delete(int COD_TIPO_ESTADO, int COD_ESTADO)
    { var r = await _service.DeleteAsync(COD_TIPO_ESTADO, COD_ESTADO); return r.Success ? Ok(r) : BadRequest(r); }

}