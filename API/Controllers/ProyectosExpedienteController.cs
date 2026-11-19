using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.API.Filters;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.API.Controllers;

/// <summary>El expediente del proyecto: sus listados por proyecto y las acciones (mover fechas del Gantt, avance, confirmación, gastos, archivos).
/// El permiso es el del proyecto (PROYECTOS.PROYECTOS); el portafolio y «mis tareas» tienen el suyo.</summary>
[Authorize]
[Route("api/v1/proyectos/expediente")]
[ExigePermiso("PROYECTOS.PROYECTOS")]
public class ProyectosExpedienteController : BaseController
{
    private readonly IProyectosExpedienteService _service;
    public ProyectosExpedienteController(IProyectosExpedienteService service) => _service = service;

    private IActionResult Resp<T>(ApiResponse<T> r) => r.Success ? Ok(r) : BadRequest(r);

    [HttpGet("{num:int}/resumen")] public async Task<IActionResult> Resumen(int num) => Resp(await _service.ResumenAsync(num));
    [HttpGet("{num:int}/planificacion")] public async Task<IActionResult> Planificacion(int num) => Resp(await _service.PlanificacionAsync(num));
    [HttpGet("{num:int}/recursos")] public async Task<IActionResult> Recursos(int num) => Resp(await _service.RecursosAsync(num));
    [HttpGet("{num:int}/costos")] public async Task<IActionResult> Costos(int num) => Resp(await _service.CostosAsync(num));
    [HttpGet("{num:int}/riesgos")] public async Task<IActionResult> Riesgos(int num) => Resp(await _service.RiesgosAsync(num));
    [HttpGet("{num:int}/documentos")] public async Task<IActionResult> Documentos(int num) => Resp(await _service.DocumentosAsync(num));
    [HttpGet("{num:int}/materiales")] public async Task<IActionResult> Materiales(int num) => Resp(await _service.MaterialesAsync(num));
    [HttpGet("{num:int}/tipos-entidad")] public async Task<IActionResult> TiposEntidad(int num) => Resp(await _service.TiposEntidadAsync(num));
    [HttpGet("{num:int}/entidades-elegibles")] public async Task<IActionResult> EntidadesElegibles(int num) => Resp(await _service.EntidadesElegiblesAsync(num));

    [HttpPut("{num:int}/tipos-entidad")]
    public async Task<IActionResult> GuardarTiposEntidad(int num, [FromBody] ExpedienteTiposEntidadDto dto)
        => Resp(await _service.GuardarTiposEntidadAsync(num, dto, GetCodUsuario(), GetEstacion()));

    /// <summary>(PUT) Horas, costo por hora y utilidad (solo personal propio) de una asignacion.</summary>
    [HttpPut("{num:int}/recursos/costo")]
    public async Task<IActionResult> RecursoCosto(int num, [FromBody] ExpedienteRecursoCostoDto dto) => Resp(await _service.RecursoCostoGuardarAsync(num, dto, GetCodUsuario(), GetEstacion()));

    /// <summary>Codigos de las entidades que son personal propio (tienen un tipo de entidad marcado como personal propio).</summary>
    [HttpGet("{num:int}/entidades-propias")]
    public async Task<IActionResult> EntidadesPropias(int num) => Resp(await _service.EntidadesPropiasAsync());

    [HttpPut("{num:int}/recalcular")]
    public async Task<IActionResult> Recalcular(int num, [FromQuery] DateTime? fecReal = null)
    {
        if (fecReal is { } f && f.Date > DateTime.Today) return BadRequest(ApiResponse<bool>.Fail("La fecha real no puede ser futura."));
        return Resp(await _service.RecalcularAsync(num, GetCodUsuario(), GetEstacion(), fecReal));
    }

    /// <summary>(PUT: mover fechas, avance y confirmación modifican el proyecto.) Arrastrar en el Gantt: cambia las fechas programadas de una fase, tarea o hito y devuelve lo que cambió.</summary>
    [HttpDelete("{num:int}/recursos/{codTarea:int}/{codEntidad:decimal}/{codRol}")]
    public async Task<IActionResult> QuitarRecurso(int num, int codTarea, decimal codEntidad, string codRol)
    {
        var r = await _service.QuitarRecursoAsync(num, codTarea, codEntidad, codRol);
        // la mano de obra de la asignacion que se quita deja de contar en el costo real del proyecto
        if (r.Success) await _service.RecalcularAsync(num, GetCodUsuario(), GetEstacion());
        return Resp(r);
    }

    [HttpPut("{num:int}/mover")]
    public async Task<IActionResult> Mover(int num, [FromBody] ExpedienteMoverDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState); return Resp(await _service.MoverAsync(num, dto, GetCodUsuario(), GetEstacion())); }

    // ───────── eventos del proyecto (retraso o no, desde/hasta, responsable) ─────────
    [HttpGet("{num:int}/eventos")]
    public async Task<IActionResult> Eventos(int num) => Resp(await _service.EventosAsync(num));

    [HttpPost("{num:int}/eventos")]
    public async Task<IActionResult> EventoCrear(int num, [FromBody] ExpedienteEventoDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState); return Resp(await _service.EventoGuardarAsync(num, dto, true, GetCodUsuario(), GetEstacion())); }

    [HttpPut("{num:int}/eventos")]
    public async Task<IActionResult> EventoModificar(int num, [FromBody] ExpedienteEventoDto dto)
    { if (!ModelState.IsValid || dto.NumSecuencia is null) return BadRequest(ModelState); return Resp(await _service.EventoGuardarAsync(num, dto, false, GetCodUsuario(), GetEstacion())); }

    [HttpDelete("{num:int}/eventos/{secuencia:int}")]
    public async Task<IActionResult> EventoQuitar(int num, int secuencia) => Resp(await _service.EventoQuitarAsync(num, secuencia));

    /// <summary>Iconos de los estados que usan las fases y tareas del proyecto (se muestran delante del estado).</summary>
    [HttpGet("{num:int}/estados-iconos")]
    public async Task<IActionResult> EstadosIconos(int num) => Resp(await _service.EstadosIconosAsync(num));

    /// <summary>Memorias del proyecto (descripciones detalladas), con su evento, fecha y usuario.</summary>
    [HttpGet("{num:int}/memorias")]
    public async Task<IActionResult> Memorias(int num) => Resp(await _service.MemoriasAsync(num));

    // ───────── documentos de compromiso y aceptacion ─────────

    [HttpGet("{num:int}/compromisos")]
    public async Task<IActionResult> Compromisos(int num) => Resp(await _service.CompromisosAsync(num));

    [HttpGet("compromisos-tipos")]
    public async Task<IActionResult> CompromisoTipos() => Resp(await _service.CompromisoTiposAsync());

    [HttpGet("{num:int}/compromisos/{secuencia:int}/personas")]
    public async Task<IActionResult> CompromisoPersonas(int num, int secuencia) => Resp(await _service.CompromisoPersonasAsync(num, secuencia));

    [HttpGet("{num:int}/compromisos/{secuencia:int}/decisiones")]
    public async Task<IActionResult> CompromisoDecisiones(int num, int secuencia) => Resp(await _service.CompromisoDecisionesAsync(num, secuencia));

    [HttpPost("{num:int}/compromisos")]
    public async Task<IActionResult> CompromisoCrear(int num, [FromBody] ExpedienteCompromisoDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState); dto.NumSecuencia = null; return Resp(await _service.CompromisoGuardarAsync(num, dto, GetCodUsuario(), GetEstacion())); }

    [HttpPut("{num:int}/compromisos")]
    public async Task<IActionResult> CompromisoModificar(int num, [FromBody] ExpedienteCompromisoDto dto)
    { if (!ModelState.IsValid || dto.NumSecuencia is null or 0) return BadRequest(ModelState); return Resp(await _service.CompromisoGuardarAsync(num, dto, GetCodUsuario(), GetEstacion())); }

    /// <summary>Un aprobador aprueba, observa o rechaza el documento (observar y rechazar piden comentario).</summary>
    [HttpPut("{num:int}/compromisos/{secuencia:int}/decision")]
    public async Task<IActionResult> CompromisoDecidir(int num, int secuencia, [FromBody] ExpedienteCompromisoDecidirDto dto)
        => Resp(await _service.CompromisoDecidirAsync(num, secuencia, dto, GetCodUsuario(), GetEstacion()));

    [HttpDelete("{num:int}/compromisos/{secuencia:int}")]
    public async Task<IActionResult> CompromisoQuitar(int num, int secuencia) => Resp(await _service.CompromisoQuitarAsync(num, secuencia));

    /// <summary>Organigrama del proyecto: los roles con gente asignada y su jerarquia (rol padre mas cercano presente).</summary>
    [HttpGet("{num:int}/organigrama")]
    public async Task<IActionResult> Organigrama(int num) => Resp(await _service.OrganigramaAsync(num));

    /// <summary>Cargo y area de una entidad (responsable o sponsor), para mostrarlos al elegirla.</summary>
    [HttpGet("entidad-cargo/{codEntidad}")]
    public async Task<IActionResult> CargoArea(string codEntidad) => Resp(await _service.CargoAreaAsync(codEntidad));

    /// <summary>Historial de cambios de estado del proyecto (envio a aprobacion, aprobado, rechazado, observado...).</summary>
    [HttpGet("{num:int}/estados-historial")]
    public async Task<IActionResult> EstadosHistorial(int num) => Resp(await _service.EstadosHistorialAsync(num));

    /// <summary>Aplica un boton de dialogo al proyecto: el destino sale del flujo de la opcion Gestion de Proyectos.</summary>
    [HttpPut("{num:int}/estado")]
    public async Task<IActionResult> ProyectoEstado(int num, [FromBody] ExpedienteEstadoDto dto)
        => Resp(await _service.EstadoAplicarAsync("P", num, 0, dto, GetCodUsuario(), GetEstacion()));

    /// <summary>Cambia el estado de una fase con un boton de dialogo: el destino sale del flujo de la opcion Fase.</summary>
    [HttpPut("{num:int}/fases/{codFase:int}/estado")]
    public async Task<IActionResult> FaseEstado(int num, int codFase, [FromBody] ExpedienteEstadoDto dto)
        => Resp(await _service.EstadoAplicarAsync("F", num, codFase, dto, GetCodUsuario(), GetEstacion()));

    [HttpPut("{num:int}/tareas/{codTarea:int}/estado")]
    public async Task<IActionResult> TareaEstado(int num, int codTarea, [FromBody] ExpedienteEstadoDto dto)
        => Resp(await _service.EstadoAplicarAsync("T", num, codTarea, dto, GetCodUsuario(), GetEstacion()));

    [HttpPut("{num:int}/tareas/{codTarea:int}/avance")]
    public async Task<IActionResult> Avance(int num, int codTarea, [FromBody] ExpedienteAvanceDto dto)
        => Resp(await _service.AvanceAsync(num, codTarea, dto, GetCodUsuario(), GetEstacion()));

    [HttpPut("{num:int}/tareas/{codTarea:int}/confirmar")]
    public async Task<IActionResult> Confirmar(int num, int codTarea, [FromBody] ExpedienteConfirmarDto dto)
        => Resp(await _service.ConfirmarAsync(num, codTarea, dto, GetCodUsuario(), GetEstacion()));

    [HttpPost("{num:int}/gastos")]
    public async Task<IActionResult> AsignarGasto(int num, [FromBody] ExpedienteGastoDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState); return Resp(await _service.AsignarGastoAsync(num, dto, GetCodUsuario(), GetEstacion())); }

    // ───────── archivos ─────────

    [HttpPost("{num:int}/archivos")]
    [RequestSizeLimit(52_000_000)]
    public async Task<IActionResult> ArchivoSubir(int num, IFormFile archivo, [FromForm] int? codFase, [FromForm] int? codTarea)
    {
        if (archivo is null) return BadRequest(ApiResponse<int>.Fail("No se recibió ningún archivo."));
        await using var s = archivo.OpenReadStream();
        return Resp(await _service.ArchivoSubirAsync(num, codFase, codTarea, archivo.FileName, s, archivo.Length, GetCodUsuario(), GetEstacion()));
    }

    [HttpGet("{num:int}/archivos/{secuencia:int}")]
    public async Task<IActionResult> ArchivoVer(int num, int secuencia)
    {
        var r = await _service.ArchivoLeerAsync(num, secuencia);
        if (!r.Success || r.Data is null) return NotFound(r);
        new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider().TryGetContentType(r.Data.NOMBRE, out var tipo);
        Response.Headers["X-Nombre-Archivo"] = Uri.EscapeDataString(r.Data.NOMBRE);
        return File(r.Data.CONTENIDO, tipo ?? "application/octet-stream");
    }

    [HttpDelete("{num:int}/archivos/{secuencia:int}")]
    public async Task<IActionResult> ArchivoQuitar(int num, int secuencia) => Resp(await _service.ArchivoQuitarAsync(num, secuencia));

    // ───────── portafolio y mis tareas (cada uno con su permiso) ─────────

    [HttpGet("portafolio")]
    [ExigePermiso("PROYECTOS.PORTAFOLIO")]
    public async Task<IActionResult> Portafolio([FromQuery] string? estado, [FromQuery] string? tipo, [FromQuery] string? search)
        => Resp(await _service.PortafolioAsync(estado, tipo, search));

    [HttpGet("mis-tareas")]
    [ExigePermiso("PROYECTOS.MIS_TAREAS")]
    public async Task<IActionResult> MisTareas([FromQuery] decimal entidad, [FromQuery] bool soloAbiertas = true)
        => Resp(await _service.MisTareasAsync(entidad, soloAbiertas));

    /// <summary>«Mis tareas»: avance y confirmación con el permiso de esa pantalla.</summary>
    [HttpPut("mis-tareas/{num:int}/{codTarea:int}/avance")]
    [ExigePermiso("PROYECTOS.MIS_TAREAS")]
    public async Task<IActionResult> MisTareasAvance(int num, int codTarea, [FromBody] ExpedienteAvanceDto dto)
        => Resp(await _service.AvanceAsync(num, codTarea, dto, GetCodUsuario(), GetEstacion()));

    [HttpPut("mis-tareas/{num:int}/{codTarea:int}/confirmar")]
    [ExigePermiso("PROYECTOS.MIS_TAREAS")]
    public async Task<IActionResult> MisTareasConfirmar(int num, int codTarea, [FromBody] ExpedienteConfirmarDto dto)
        => Resp(await _service.ConfirmarAsync(num, codTarea, dto, GetCodUsuario(), GetEstacion()));
}
