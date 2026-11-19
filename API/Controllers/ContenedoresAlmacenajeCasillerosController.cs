using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/contenedores-almacenaje-casilleros")]
[ExigePermiso("LOGISTICA.CONTENEDORES_ALMACENAJE_CASILLEROS")]
public class ContenedoresAlmacenajeCasillerosController : BaseController
{
    private readonly IContenedoresAlmacenajeCasillerosService _service;
    public ContenedoresAlmacenajeCasillerosController(IContenedoresAlmacenajeCasillerosService service) => _service = service;

    // Ruta antes que "{COD_CONTENEDOR}" para que "search"/"combobox"/"por-ubicacion" no se
    // tomen por un código.
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ContenedoresAlmacenajeCasillerosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>El camino inverso: no se conoce el contenedor, se conoce la ubicación (viene
    /// de una reserva o de un stock) y hace falta el rack y sus polígonos para dibujarla.</summary>
    [HttpGet("por-ubicacion/{COD_ALMACEN}/{COD_UBICACION_ALMACEN}")]
    public async Task<IActionResult> BuscarPorUbicacion(string COD_ALMACEN, string COD_UBICACION_ALMACEN)
    { var r = await _service.BuscarPorUbicacionAsync(COD_ALMACEN, COD_UBICACION_ALMACEN); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox/{COD_CONTENEDOR}")]
    public async Task<IActionResult> GetCombobox(string COD_CONTENEDOR)
    { var r = await _service.GetComboboxAsync(COD_CONTENEDOR); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CONTENEDOR}")]
    public async Task<IActionResult> GetAll(string COD_CONTENEDOR)
    { var r = await _service.GetAllAsync(COD_CONTENEDOR); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CONTENEDOR}/{COD_CASILLERO}")]
    public async Task<IActionResult> GetById(string COD_CONTENEDOR, string COD_CASILLERO)
    { var r = await _service.GetByIdAsync(COD_CONTENEDOR, COD_CASILLERO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ContenedoresAlmacenajeCasillerosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    /// <summary>Genera la matriz completa de casilleros del contenedor (_CREATETALL).
    /// Reemplaza lo que hubiera: no es un alta incremental.</summary>
    [HttpPost("generar")]
    public async Task<IActionResult> Generar([FromBody] ContenedoresAlmacenajeCasillerosGenerarDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.GenerarAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_CONTENEDOR}/{COD_CASILLERO}")]
    public async Task<IActionResult> Delete(string COD_CONTENEDOR, string COD_CASILLERO)
    { var r = await _service.DeleteAsync(COD_CONTENEDOR, COD_CASILLERO); return r.Success ? Ok(r) : BadRequest(r); }
}
