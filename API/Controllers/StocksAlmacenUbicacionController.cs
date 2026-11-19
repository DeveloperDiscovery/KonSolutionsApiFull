using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/stocks-almacen-ubicacion")]
[ExigePermiso("LOGISTICA.STOCKS_ALMACEN_UBICACION")]
public class StocksAlmacenUbicacionController : BaseController
{
    private readonly IStocksAlmacenUbicacionService _service;
    public StocksAlmacenUbicacionController(IStocksAlmacenUbicacionService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ALMACEN}/{COD_UBICACION_ALMACEN}/{COD_PRODUCTO}")]
    public async Task<IActionResult> GetById(string COD_ALMACEN, string COD_UBICACION_ALMACEN, string COD_PRODUCTO)
    { var r = await _service.GetByIdAsync(COD_ALMACEN, COD_UBICACION_ALMACEN, COD_PRODUCTO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("loadcombos")]
    public async Task<IActionResult> LoadCombos()
    { var r = await _service.LoadCombosAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    // Va por POST y no por GET: son veintitrés filtros, y varios son texto libre que en la
    // query string obligaría a escapar y recortar.
    [HttpPost("search-detail")]
    public async Task<IActionResult> SearchDetail([FromBody] StocksAlmacenUbicacionSearchDetailDto dto)
    { var r = await _service.SearchDetailAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // Lee una etiqueta (RFID o código de barras) y dice qué es y cómo está su stock. Va por POST: una etiqueta RFID
    // mide más de 100 caracteres. Rechazada: 400 con el mensaje del procedimiento.
    [HttpPost("etiqueta")]
    public async Task<IActionResult> Etiqueta([FromBody] StocksAlmacenUbicacionEtiquetaDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.EtiquetaAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // Kardex de una línea de stock: la clave completa viaja en la query string.
    [HttpGet("kardex")]
    public async Task<IActionResult> Kardex([FromQuery] StocksAlmacenUbicacionKardexDto dto)
    { var r = await _service.KardexAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] StocksAlmacenUbicacionSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] StocksAlmacenUbicacionInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] StocksAlmacenUbicacionUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_ALMACEN}/{COD_UBICACION_ALMACEN}/{COD_PRODUCTO}")]
    public async Task<IActionResult> Delete(string COD_ALMACEN, string COD_UBICACION_ALMACEN, string COD_PRODUCTO)
    { var r = await _service.DeleteAsync(COD_ALMACEN, COD_UBICACION_ALMACEN, COD_PRODUCTO); return r.Success ? Ok(r) : BadRequest(r); }

}