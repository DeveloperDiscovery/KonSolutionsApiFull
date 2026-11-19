using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;
using KonSolutionsApi.Domain.Models.Reportes;

namespace KonSolutionsApi.Application.Services;

/// <summary>Documentos emitidos de un proyecto: se crean desde una plantilla, se editan, se envian a aprobar (compromiso) y se emiten con su PDF congelado en los archivos del proyecto.
/// El PDF lo genera el reporteador con el diseño de la plantilla y el texto del documento.</summary>
public class ProyectosDocumentosEmitidosService : IProyectosDocumentosEmitidosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ProyectosDocumentosEmitidosService);
    private readonly IProyectosDocumentosEmitidosRepository _repo;
    private readonly IPlantillasDocumentoRepository _plantillas;
    private readonly IReportesService _reportes;
    private readonly IProyectosExpedienteService _expediente;
    private readonly ErrorLogService _log;

    public ProyectosDocumentosEmitidosService(IProyectosDocumentosEmitidosRepository repo, IPlantillasDocumentoRepository plantillas, IReportesService reportes, IProyectosExpedienteService expediente, ErrorLogService log)
    { _repo = repo; _plantillas = plantillas; _reportes = reportes; _expediente = expediente; _log = log; }

    private async Task<ApiResponse<T>> Corre<T>(Func<Task<T>> accion, string metodo)
    {
        try { return ApiResponse<T>.Ok(await accion()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, metodo); return ApiResponse<T>.Fail(ex.Message); }
    }

    public Task<ApiResponse<IEnumerable<DocumentoEmitido>>> ListarAsync(int num) => Corre(() => _repo.ListarAsync(num), nameof(ListarAsync));

    public async Task<ApiResponse<DocumentoEmitido>> ObtenerAsync(int num, int secuencia)
    {
        try
        {
            var d = await _repo.ObtenerAsync(num, secuencia);
            return d is null ? ApiResponse<DocumentoEmitido>.Fail("El documento no existe.") : ApiResponse<DocumentoEmitido>.Ok(d);
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ObtenerAsync)); return ApiResponse<DocumentoEmitido>.Fail(ex.Message); }
    }

    public Task<ApiResponse<int>> NuevoAsync(int num, DocumentoEmitidoNuevoDto dto, string codUsuario, string codEstacion) => Corre(() => _repo.NuevoAsync(num, dto, codUsuario, codEstacion), nameof(NuevoAsync));

    public Task<ApiResponse<bool>> GuardarAsync(int num, int secuencia, DocumentoEmitidoGuardarDto dto, string codUsuario, string codEstacion)
        => Corre(async () => { await _repo.GuardarAsync(num, secuencia, dto, codUsuario, codEstacion); return true; }, nameof(GuardarAsync));

    public Task<ApiResponse<int>> NuevaVersionAsync(int num, int secuencia, string codUsuario, string codEstacion) => Corre(() => _repo.NuevaVersionAsync(num, secuencia, codUsuario, codEstacion), nameof(NuevaVersionAsync));

    public Task<ApiResponse<bool>> AnularAsync(int num, int secuencia, DocumentoEmitidoAnularDto dto, string codUsuario, string codEstacion)
        => Corre(async () => { await _repo.AnularAsync(num, secuencia, dto.Motivo, codUsuario, codEstacion); return true; }, nameof(AnularAsync));

    public Task<ApiResponse<bool>> EliminarAsync(int num, int secuencia) => Corre(async () => { await _repo.EliminarAsync(num, secuencia); return true; }, nameof(EliminarAsync));

    public Task<ApiResponse<IEnumerable<ComboboxItem>>> PlantillasAsync() => Corre(() => _plantillas.ComboAsync(), nameof(PlantillasAsync));

    public Task<ApiResponse<string?>> TextoResueltoAsync(int num, DocumentoEmitidoTextoDto dto) => Corre(() => _repo.TextoResueltoAsync(num, dto.Contenido ?? ""), nameof(TextoResueltoAsync));

    /// <summary>El PDF del documento con su texto actual, hecho por el reporteador. Falla con un mensaje claro si la plantilla no tiene diseño.</summary>
    private async Task<(ReporteArchivo? Archivo, string? Error)> GenerarPdfAsync(DocumentoEmitido d, string? codRol, string codUsuario, string codEstacion)
    {
        if (d.NUM_DISENO is null) return (null, "La plantilla de este documento no tiene un diseño del reporteador: asignale uno en Plantillas de documento.");
        var dto = new ReporteGenerarDto
        {
            NUM_DISENO = d.NUM_DISENO, COD_FORMATO = "PDF",
            Valores = new Dictionary<string, string?> { ["NUM_PROYECTO"] = d.NUM_PROYECTO.ToString(), ["NUM_DOCUMENTO"] = d.NUM_SECUENCIA.ToString() }
        };
        var r = await _reportes.GenerarAsync(dto, codUsuario, codRol, codEstacion);
        if (!r.Success || r.Data is null) return (null, string.IsNullOrWhiteSpace(r.Message) ? "No se pudo generar el PDF." : r.Message);
        return (r.Data, null);
    }

    private static string NombrePdf(DocumentoEmitido d, string sufijo)
    {
        var baseNombre = new string((d.DES_TITULO ?? "documento").Select(c => char.IsLetterOrDigit(c) ? c : '_').ToArray()).Trim('_');
        if (baseNombre.Length > 60) baseNombre = baseNombre[..60];
        return $"{baseNombre}_v{d.NUM_VERSION}{sufijo}.pdf";
    }

    /// <summary>Guarda el PDF generado como archivo del proyecto (el mismo repositorio de archivos del expediente) y devuelve su secuencia.</summary>
    private async Task<(int? Secuencia, string? Error)> GuardarArchivoAsync(DocumentoEmitido d, ReporteArchivo pdf, string sufijo, string codUsuario, string codEstacion)
    {
        await using var ms = new MemoryStream(pdf.CONTENIDO);
        var r = await _expediente.ArchivoSubirAsync(d.NUM_PROYECTO, d.COD_FASE, null, NombrePdf(d, sufijo), ms, ms.Length, codUsuario, codEstacion);
        return r.Success ? (r.Data, null) : (null, string.IsNullOrWhiteSpace(r.Message) ? "No se pudo guardar el PDF en los archivos del proyecto." : r.Message);
    }

    public async Task<ApiResponse<int>> EnviarAsync(int num, int secuencia, DocumentoEmitidoEnviarDto dto, string? codRol, string codUsuario, string codEstacion)
    {
        int? sec = null;
        try
        {
            var d = await _repo.ObtenerAsync(num, secuencia);
            if (d is null) return ApiResponse<int>.Fail("El documento no existe.");
            if (d.COD_ESTADO_DOCUMENTO is not ("BOR" or "OBS")) return ApiResponse<int>.Fail("Solo se envia a aprobar un documento en borrador u observado.");
            // el PDF borrador que veran los aprobadores en la pestaña Compromisos
            var (pdf, err) = await GenerarPdfAsync(d, codRol, codUsuario, codEstacion);
            if (pdf is null) return ApiResponse<int>.Fail(err!);
            var (s, errArchivo) = await GuardarArchivoAsync(d, pdf, "_borrador", codUsuario, codEstacion);
            if (s is null) return ApiResponse<int>.Fail(errArchivo!);
            sec = s;
            var compromiso = await _repo.EnviarAsync(num, secuencia, dto, sec, codUsuario, codEstacion);
            return ApiResponse<int>.Ok(compromiso, "Documento enviado a aprobar.");
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(EnviarAsync));
            if (sec is not null) { try { await _expediente.ArchivoQuitarAsync(num, sec.Value); } catch { /* ya se informa el error principal */ } }
            return ApiResponse<int>.Fail(ex.Message);
        }
    }

    public async Task<ApiResponse<bool>> EmitirAsync(int num, int secuencia, DocumentoEmitidoEmitirDto dto, string? codRol, string codUsuario, string codEstacion)
    {
        int? sec = null;
        try
        {
            var d = await _repo.ObtenerAsync(num, secuencia);
            if (d is null) return ApiResponse<bool>.Fail("El documento no existe.");
            // se valida antes de generar y guardar el PDF (el SP vuelve a validar)
            if (!(d.COD_ESTADO_DOCUMENTO == "APR" || (d.COD_ESTADO_DOCUMENTO == "BOR" && !d.FLG_REQUIERE_APROBACION)))
                return ApiResponse<bool>.Fail("Solo se emite un documento aprobado (o un borrador de una plantilla que no pide aprobacion).");
            var (pdf, err) = await GenerarPdfAsync(d, codRol, codUsuario, codEstacion);
            if (pdf is null) return ApiResponse<bool>.Fail(err!);
            var (s, errArchivo) = await GuardarArchivoAsync(d, pdf, "", codUsuario, codEstacion);
            if (s is null) return ApiResponse<bool>.Fail(errArchivo!);
            sec = s;
            await _repo.EmitirAsync(num, secuencia, sec.Value, dto.CodEntidadEmisor, codUsuario, codEstacion);
            return ApiResponse<bool>.Ok(true, "Documento emitido.");
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(EmitirAsync));
            if (sec is not null) { try { await _expediente.ArchivoQuitarAsync(num, sec.Value); } catch { /* ya se informa el error principal */ } }
            return ApiResponse<bool>.Fail(ex.Message);
        }
    }

    public async Task<ApiResponse<ProyectoArchivoContenido>> PdfAsync(int num, int secuencia, string? codRol, string codUsuario, string codEstacion)
    {
        try
        {
            var d = await _repo.ObtenerAsync(num, secuencia);
            if (d is null) return ApiResponse<ProyectoArchivoContenido>.Fail("El documento no existe.");
            // emitido: el PDF congelado
            if (d.COD_ESTADO_DOCUMENTO == "EMI" && d.NUM_SECUENCIA_ARCHIVO is not null)
                return await _expediente.ArchivoLeerAsync(num, d.NUM_SECUENCIA_ARCHIVO.Value);
            var (pdf, err) = await GenerarPdfAsync(d, codRol, codUsuario, codEstacion);
            if (pdf is null) return ApiResponse<ProyectoArchivoContenido>.Fail(err!);
            return ApiResponse<ProyectoArchivoContenido>.Ok(new ProyectoArchivoContenido { NOMBRE = NombrePdf(d, d.COD_ESTADO_DOCUMENTO == "EMI" ? "" : "_vista_previa"), CONTENIDO = pdf.CONTENIDO });
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(PdfAsync)); return ApiResponse<ProyectoArchivoContenido>.Fail(ex.Message); }
    }
}
