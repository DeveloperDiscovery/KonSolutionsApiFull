using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/ingenieria/disenos-memorias")]
[ExigePermiso("INGENIERIA.DISENOS_MEMORIAS")]
public class DisenosMemoriasController : BaseController
{
    private readonly IDisenosMemoriasService _service;
    public DisenosMemoriasController(IDisenosMemoriasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codDiseno)
    {
        if (string.IsNullOrWhiteSpace(codDiseno))
            return BadRequest(new { Success = false, Message = "codDiseno es requerido." });
        var r = await _service.GetAllAsync(codDiseno);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpGet("{COD_DISENO}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> GetById(string COD_DISENO, short NUM_SECUENCIA)
    { var r = await _service.GetByIdAsync(COD_DISENO, NUM_SECUENCIA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] DisenosMemoriasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] DisenosMemoriasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_DISENO}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> Delete(string COD_DISENO, short NUM_SECUENCIA)
    { var r = await _service.DeleteAsync(COD_DISENO, NUM_SECUENCIA); return r.Success ? Ok(r) : BadRequest(r); }
}

[Authorize]
[Route("api/v1/ingenieria/disenos-versiones-memorias")]
public class DisenosVersionesMemoriasController : BaseController
{
    private readonly IDisenosVersionesMemoriasService _service;
    public DisenosVersionesMemoriasController(IDisenosVersionesMemoriasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codDiseno, [FromQuery] short numVersion)
    {
        if (string.IsNullOrWhiteSpace(codDiseno))
            return BadRequest(new { Success = false, Message = "codDiseno es requerido." });
        var r = await _service.GetAllAsync(codDiseno, numVersion);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpGet("{COD_DISENO}/{NUM_VERSION}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> GetById(string COD_DISENO, short NUM_VERSION, short NUM_SECUENCIA)
    { var r = await _service.GetByIdAsync(COD_DISENO, NUM_VERSION, NUM_SECUENCIA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] DisenosVersionesMemoriasSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] DisenosVersionesMemoriasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_DISENO}/{NUM_VERSION}/{NUM_SECUENCIA}")]
    public async Task<IActionResult> Delete(string COD_DISENO, short NUM_VERSION, short NUM_SECUENCIA)
    { var r = await _service.DeleteAsync(COD_DISENO, NUM_VERSION, NUM_SECUENCIA); return r.Success ? Ok(r) : BadRequest(r); }
}
