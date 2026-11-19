using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Services;

/// <summary>Lee de la ruta de archivos de XML recibidos (la de Configuración general) un archivo que bajó el servicio, por ejemplo el PDF de un comprobante. El nombre que guarda el documento es relativo a esa
/// ruta («procesados/F001-123.pdf»); aquí se comprueba que no se salga de ella (nada de «..» ni rutas absolutas) antes de abrir nada.</summary>
public class XmlRecibidosArchivos
{
    private readonly IXmlRecibidosRepository _repo;
    private readonly IPathFilesRepository _paths;
    public XmlRecibidosArchivos(IXmlRecibidosRepository repo, IPathFilesRepository paths) { _repo = repo; _paths = paths; }

    public async Task<(RecibidoArchivo? Archivo, string? Error)> LeerAsync(string? relativo, long maxBytes)
    {
        if (string.IsNullOrWhiteSpace(relativo)) return (null, "El documento no trae un archivo guardado.");
        var rel = relativo.Trim().Replace('\\', '/');
        if (rel.StartsWith('/') || rel.Contains(':') || rel.Split('/').Any(x => x == ".."))
            return (null, "El nombre del archivo no es válido.");
        var cfg = await _repo.ConfigAsync();
        var clave = cfg?.COD_KEY_PATHFILE_COMPRAS_XML;
        if (string.IsNullOrWhiteSpace(clave)) return (null, "Falta la ruta de archivos de Compras en Configuración general, pestaña Compras.");
        var ruta = await _paths.ShowbyidAsync(clave.Trim());
        if (ruta is null || string.IsNullOrWhiteSpace(ruta.DES_PATH_FILE)) return (null, $"La ruta «{clave}» no existe en Rutas de archivos o no tiene carpeta.");
        var raiz = Path.GetFullPath(ruta.DES_PATH_FILE.Trim());
        var completa = Path.GetFullPath(Path.Combine(raiz, rel));
        var comparacion = OperatingSystem.IsWindows() ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
        if (!completa.StartsWith(raiz.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar, comparacion))
            return (null, "El nombre del archivo no es válido.");
        var info = new FileInfo(completa);
        if (!info.Exists) return (null, $"El archivo «{rel}» ya no está en la carpeta del servidor.");
        if (info.Length > maxBytes) return (null, $"El archivo pesa más de {maxBytes / 1024 / 1024} MB.");
        return (new RecibidoArchivo { NOMBRE = info.Name, CONTENIDO = await File.ReadAllBytesAsync(completa) }, null);
    }
}
