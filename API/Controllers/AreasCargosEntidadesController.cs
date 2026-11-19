using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Administracion;

namespace KonSolutionsApi.API.Controllers;

/// <summary>Tabla puente ADMINISTRACION.AREAS_CARGOS_ENTIDADES: entidades (personas)
/// asignadas a un cargo dentro de un área (PK compuesta COD_AREA+COD_CARGO+COD_ENTIDAD).</summary>
[Authorize]
[Route("api/v1/administracion/areas-cargos-entidades")]
[ExigePermiso("ADMINISTRACION.AREAS_CARGOS_ENTIDADES")]
public class AreasCargosEntidadesController : BaseController
{
    private readonly IAreasCargosEntidadesService _service;
    public AreasCargosEntidadesController(IAreasCargosEntidadesService service) => _service = service;

    [HttpGet("{codArea}/{codCargo}")]
    public async Task<IActionResult> GetAll(int codArea, int codCargo)
    { var r = await _service.GetAllAsync(codArea, codCargo); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Las personas de un centro de costo, alcanzadas por su área. La usan los combos
    /// de Entidad requiriente de Requerimientos de Almacén.
    ///
    /// La ruta va con prefijo literal ("por-centro-costo/...") y no suelta: el centro de costo
    /// es una cadena y, sin el prefijo, chocaría con GetAll("{codArea}/{codCargo}").</summary>
    [HttpGet("por-centro-costo/{codCentroCosto}")]
    public async Task<IActionResult> GetComboboxPorCentroCosto(string codCentroCosto)
    { var r = await _service.GetComboboxPorCentroCostoAsync(codCentroCosto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{codArea}/{codCargo}/{codEntidad}")]
    public async Task<IActionResult> GetById(int codArea, int codCargo, decimal codEntidad)
    { var r = await _service.GetByIdAsync(codArea, codCargo, codEntidad); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] AreasCargosEntidadesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] AreasCargosEntidadesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{codArea}/{codCargo}/{codEntidad}")]
    public async Task<IActionResult> Delete(int codArea, int codCargo, decimal codEntidad)
    { var r = await _service.DeleteAsync(codArea, codCargo, codEntidad); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("treeview")]
    public async Task<IActionResult> GetTreeview()
    { var r = await _service.GetTreeviewAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("entidades-disponibles")]
    public async Task<IActionResult> GetEntidadesDisponibles()
    { var r = await _service.GetEntidadesDisponiblesAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>Combo de personas asignables como "asesor comercial" (u otro rol equivalente)
    /// para una Etiqueta de Búsqueda puntual (ej. "EJECUTIVO_VENTAS_GENERAL"):
    /// PROC_ADMINISTRACION_AREAS_CARGOS_ENTIDADES resuelve el Área — y los niveles jerárquicos
    /// habilitados — enlazados a esa etiqueta internamente (join contra ESTANDAR.
    /// ETIQUETAS_BUSQUEDA_AREAS) y devuelve las personas asignadas a sus cargos.</summary>
    [HttpGet("asesores-por-etiqueta")]
    public async Task<IActionResult> GetAsesoresPorEtiqueta([FromQuery] string codEtiquetaBusqueda)
    {
        if (string.IsNullOrWhiteSpace(codEtiquetaBusqueda))
            return BadRequest(new { Success = false, Message = "codEtiquetaBusqueda es requerido." });

        var r = await _service.GetPorEtiquetaAsync(codEtiquetaBusqueda);
        if (!r.Success) return BadRequest(r);

        // Una misma persona puede tener varios cargos dentro del Área enlazada a la opción
        // (varias filas con el mismo COD_ENTIDAD, una por cargo) — se agrupa para no repetirla
        // en el combo, quedándose con el primer nombre no vacío del grupo.
        var items = (r.Data ?? Enumerable.Empty<AreaCargoEntidadPorOpcion>())
            .GroupBy(n => n.COD_ENTIDAD)
            .Select(g => new ComboboxItem
            {
                Id = g.Key.ToString(System.Globalization.CultureInfo.InvariantCulture),
                Texto = g.Select(n => n.DES_NOMBRE_COMPLETO).FirstOrDefault(t => !string.IsNullOrWhiteSpace(t)) ?? ""
            })
            .Where(i => !string.IsNullOrWhiteSpace(i.Texto))
            .OrderBy(i => i.Texto)
            .ToList();
        return Ok(ApiResponse<IEnumerable<ComboboxItem>>.Ok(items));
    }
}
