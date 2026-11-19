using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/estados-estandar")]
[ExigePermiso("ESTANDAR.ESTADOS_ESTANDAR")]
public class EstadosEstandarController : BaseController
{
    private readonly IEstadosEstandarService _service;
    public EstadosEstandarController(IEstadosEstandarService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_INDICE}/{COD_ESTADO}")]
    public async Task<IActionResult> GetById(int COD_INDICE, int COD_ESTADO)
    { var r = await _service.GetByIdAsync(COD_INDICE, COD_ESTADO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EstadosEstandarSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EstadosEstandarInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EstadosEstandarUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_INDICE}/{COD_ESTADO}")]
    public async Task<IActionResult> Delete(int COD_INDICE, int COD_ESTADO)
    { var r = await _service.DeleteAsync(COD_INDICE, COD_ESTADO); return r.Success ? Ok(r) : BadRequest(r); }

}