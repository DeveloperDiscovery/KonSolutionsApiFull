namespace KonSolutionsApi.Domain.Models.Entorno;

/// <summary>Archivo físico dentro de una carpeta de documentos genérica: la ruta base se
/// resuelve por una clave de ENTORNO.PATHFILES (PROC_ENTORNO_PATHFILES_SHOWBYID) y dentro de
/// ella se crea (si no existe) una subcarpeta con un identificador (N° de Solicitud, código de
/// Entidad, etc.) — reutilizable desde cualquier diálogo que necesite adjuntar documentos.
/// No hay tabla de metadatos: se lista directamente el contenido del directorio.</summary>
public class DocumentoCarpetaDto
{
    public string   NombreArchivo      { get; set; } = string.Empty;
    public long     TamanoBytes        { get; set; }
    public DateTime FechaModificacion  { get; set; }
    public string   Extension          { get; set; } = string.Empty;

    // Ruta UNC completa del archivo — se usa para pedir el preview al proxy genérico
    // GET /api/v1/entorno/documentos/ver?ruta=... (mismo mecanismo que NotificacionAprobacionDialog).
    public string   RutaCompleta       { get; set; } = string.Empty;
}
