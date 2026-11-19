using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/entidades")]
[ExigePermiso("ESTANDAR.ENTIDADES")]
public class EntidadesController : BaseController
{
    private readonly IEntidadesService _service;
    public EntidadesController(IEntidadesService service) => _service = service;

    // codTipoEntidad opcional: cuando viene, filtra por ESTANDAR.ENTIDADES_TIPOS_ENTIDADES
    // (SP _MODULESHOWALL) en vez del listado genérico — usado por pantallas reutilizadas
    // con un tipo fijo (ej. "CLI" = Clientes, ver DES_PARAMETRO en APLICACIONES_OPCIONES).
    // desTiposEntidad opcional: filtro multi-selección de chips en la grilla genérica
    // (ej. 'CLI_PRV_'), independiente de codTipoEntidad (que resuelve pantallas reutilizadas
    // con un único tipo fijo, ver comentario abajo).
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? codTipoEntidad = null, [FromQuery] string? desTiposEntidad = null)
    {
        var r = string.IsNullOrWhiteSpace(codTipoEntidad)
            ? await _service.GetAllAsync(desTiposEntidad)
            : await _service.GetAllPorTipoAsync(codTipoEntidad);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    // Encadenado: entidades de un tipo que además dependen de otra entidad elegida (ej.
    // "Entidad conductora" dependiente de la "Entidad propietaria" en
    // TransporteDocumentoDialog.razor). El frontend ya llamaba a esta ruta
    // (ApiRoutes.Estandar.EntidadesDependientes) pero la acción nunca se había creado, así
    // que el pedido caía en GetById({COD_ENTIDAD}) con "dependientes" como valor y tiraba 400.
    [HttpGet("dependientes")]
    public async Task<IActionResult> GetDependientes([FromQuery] string codTipoEntidad, [FromQuery] decimal codEntidadDependencia)
    { var r = await _service.GetAllPorTipoDependienteAsync(codTipoEntidad, codEntidadDependencia); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_ENTIDAD}")]
    public async Task<IActionResult> GetById(decimal COD_ENTIDAD)
    { var r = await _service.GetByIdAsync(COD_ENTIDAD); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] EntidadesSearchDto dto)
    {
        var r = string.IsNullOrWhiteSpace(dto.CodTipoEntidad)
            ? await _service.SearchAsync(dto)
            : await _service.SearchPorTipoAsync(dto.CodTipoEntidad, dto.Search);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    // PROC_ESTANDAR_ENTIDADES_PARADEPENDER — solo Persona Jurídica, para el combo
    // "Entidad dependencia" del diálogo (habilitado solo en Persona Natural).
    [HttpGet("para-depender")]
    public async Task<IActionResult> GetParaDepender()
    { var r = await _service.GetParaDependerAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EntidadesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpDelete("{COD_ENTIDAD}")]
    public async Task<IActionResult> Delete(decimal COD_ENTIDAD)
    { var r = await _service.DeleteAsync(COD_ENTIDAD); return r.Success ? Ok(r) : BadRequest(r); }

    // Experimento: logos de TODAS las entidades en un endpoint aparte del GetAll principal
    // (que no trae IMG_LOGO) — la grilla carga solo texto y este pedido llega después, en
    // paralelo, sin bloquear el primer render.
    [HttpGet("images")]
    public async Task<IActionResult> GetImages()
    { var r = await _service.GetImagesAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>PROC_ESTANDAR_ENTIDADES_LOADCOMBOS: tipos de persona/documento de identidad,
    /// países, estados, tipos de medio de contacto y de dirección para el diálogo de
    /// Entidades en un solo viaje. codTipoEstado es opcional (se autorresuelve).</summary>
    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos([FromQuery] int codTipoEstado = 0)
    { var r = await _service.LoadcombosAsync(codTipoEstado); return r.Success ? Ok(r) : BadRequest(r); }

}