using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/logistica/productos-almacenes")]
[ExigePermiso("LOGISTICA.PRODUCTOS_ALMACENES")]
public class ProductosAlmacenesController : BaseController
{
    private readonly IProductosAlmacenesService _service;
    public ProductosAlmacenesController(IProductosAlmacenesService service) => _service = service;

    // Los tres SP de consulta exigen @COD_PRODUCTO: esta tabla siempre se mira desde un
    // producto (la pantalla es una subgrilla de la ficha del producto), nunca "todos".
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string codProducto)
    { var r = await _service.GetAllAsync(codProducto); return r.Success ? Ok(r) : BadRequest(r); }

    // La vuelta: todos los productos configurados en un almacén (pestaña "Por almacén").
    // Va a un SP propio; el SHOWALL normal no admite entrar por este lado de la clave.
    [HttpGet("por-almacen")]
    public async Task<IActionResult> GetAllPorAlmacen([FromQuery] string codAlmacen)
    { var r = await _service.GetAllPorAlmacenAsync(codAlmacen); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Todo lo que tiene cantidad pendiente de pedir — pestaña "A Requerir". Desde
    /// la versión del 04/09 el SP ya no filtra por producto: devuelve el pendiente completo.</summary>
    [HttpGet("requerir")]
    public async Task<IActionResult> GetAllRequerir()
    { var r = await _service.GetAllRequerirAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_PRODUCTO}/{COD_ALMACEN}")]
    public async Task<IActionResult> GetById(string COD_PRODUCTO, string COD_ALMACEN)
    { var r = await _service.GetByIdAsync(COD_PRODUCTO, COD_ALMACEN); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] string codProducto)
    { var r = await _service.GetComboboxAsync(codProducto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string codProducto, [FromQuery] ProductosAlmacenesSearchDto dto)
    { var r = await _service.SearchAsync(codProducto, dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ProductosAlmacenesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProductosAlmacenesUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_PRODUCTO}/{COD_ALMACEN}")]
    public async Task<IActionResult> Delete(string COD_PRODUCTO, string COD_ALMACEN)
    { var r = await _service.DeleteAsync(COD_PRODUCTO, COD_ALMACEN); return r.Success ? Ok(r) : BadRequest(r); }

    // ── Procesos de reposición ────────────────────────────────────────────────
    // Los tres son botones configurados en BD (ver AccionesBotones): "Borrar/Resetear
    // requerimiento", "Generar requerimiento de compra" y "Generar stock de seguridad".
    // Van por POST aunque no reciban cuerpo: modifican datos, no son consultas.

    /// <summary>Pone en 0 la cantidad a requerir de lo que todavía no tiene requerimiento.
    /// Sin parámetros (los dos null) el SP no toca nada — es el propio SP el que decide, no
    /// se emula acá un "borrar todo" que él no contempla.</summary>
    [HttpPost("reset")]
    public async Task<IActionResult> Reset([FromQuery] string? codAlmacen, [FromQuery] string? codProducto)
    { var r = await _service.ResetAsync(codAlmacen, codProducto); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Genera los requerimientos de compra con TODO lo que tenga cantidad pendiente:
    /// el SP no recibe listas, decide por su cuenta. El usuario y la estación salen del JWT,
    /// no del request — nadie genera requerimientos a nombre de otro.</summary>
    [HttpPost("requerimiento")]
    public async Task<IActionResult> Requerimiento()
    { var r = await _service.RequerimientoAsync(GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Recalcula la cantidad a requerir contra el stock real (stock de seguridad),
    /// acotado a los almacenes y productos seleccionados. Listas de códigos separados por "_".</summary>
    [HttpPost("cantidades")]
    public async Task<IActionResult> Cantidades([FromQuery] string? lstAlmacenes, [FromQuery] string? lstProductos)
    { var r = await _service.CantidadesAsync(lstAlmacenes ?? "", lstProductos ?? ""); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Botón "Revertir": anula el requerimiento generado. El usuario y la estación
    /// salen del JWT — la anulación queda a nombre de quien la ejecuta, no de quien la pida
    /// en el request.</summary>
    [HttpPost("reversion")]
    public async Task<IActionResult> Reversion([FromQuery] int numRequerimientoStock)
    { var r = await _service.ReversionAsync(numRequerimientoStock, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }
}
