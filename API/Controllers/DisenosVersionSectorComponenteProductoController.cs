using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/ingenieria/disenos-version-sector-componente-producto")]
[ExigePermiso("INGENIERIA.DISENOS_VERSION_SECTOR_COMPONENTE_PRODUCTO")]
public class DisenosVersionSectorComponenteProductoController : BaseController
{
    private readonly IDisenosVersionSectorComponenteProductoService _service;
    public DisenosVersionSectorComponenteProductoController(IDisenosVersionSectorComponenteProductoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_DISENO}/{NUM_VERSION}/{COD_SECTOR_PRODUCTO}")]
    public async Task<IActionResult> GetById(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO)
    { var r = await _service.GetByIdAsync(COD_DISENO, NUM_VERSION, COD_SECTOR_PRODUCTO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] DisenosVersionSectorComponenteProductoSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] DisenosVersionSectorComponenteProductoInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] DisenosVersionSectorComponenteProductoUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_DISENO}/{NUM_VERSION}/{COD_SECTOR_PRODUCTO}")]
    public async Task<IActionResult> Delete(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO)
    { var r = await _service.DeleteAsync(COD_DISENO, NUM_VERSION, COD_SECTOR_PRODUCTO); return r.Success ? Ok(r) : BadRequest(r); }

}