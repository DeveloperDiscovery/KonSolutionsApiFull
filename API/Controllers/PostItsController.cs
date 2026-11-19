using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.API.Controllers;

// Sin política de permisos por rol/opción: cualquier usuario logueado administra sus
// propios post-its (filtrados por COD_USUARIO en el frontend).
[Authorize]
[Route("api/v1/entorno/postits")]
[ExigePermiso("ENTORNO.POSTITS")]
public class PostItsController : BaseController
{
    private readonly IPostItsService _service;
    public PostItsController(IPostItsService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{NUM_POSTIT}")]
    public async Task<IActionResult> GetById(long NUM_POSTIT)
    { var r = await _service.GetByIdAsync(NUM_POSTIT); return r.Success ? Ok(r) : NotFound(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] PostItsInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{NUM_POSTIT}")]
    public async Task<IActionResult> Delete(long NUM_POSTIT)
    { var r = await _service.DeleteAsync(NUM_POSTIT); return r.Success ? Ok(r) : BadRequest(r); }
}
