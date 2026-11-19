using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/etiquetas-busqueda-areas")]
[ExigePermiso("ESTANDAR.ETIQUETAS_BUSQUEDA_AREAS")]
public class EtiquetasBusquedaAreasController : BaseController
{
    private readonly IEtiquetasBusquedaAreasService _service;
    public EtiquetasBusquedaAreasController(IEtiquetasBusquedaAreasService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var r = await _service.GetAllAsync();
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpGet("{COD_ETIQUETA_BUSQUEDA}")]
    public async Task<IActionResult> GetById(string COD_ETIQUETA_BUSQUEDA)
    { var r = await _service.GetByIdAsync(COD_ETIQUETA_BUSQUEDA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EtiquetasBusquedaAreasInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_ETIQUETA_BUSQUEDA}")]
    public async Task<IActionResult> Delete(string COD_ETIQUETA_BUSQUEDA)
    { var r = await _service.DeleteAsync(COD_ETIQUETA_BUSQUEDA); return r.Success ? Ok(r) : BadRequest(r); }
}
