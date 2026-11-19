using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;

namespace KonSolutionsApi.Application.Services;

/// <summary>Lo que necesita la pestaña RFID de Configuración general: los productos que pueden ser la estación donde corre el agente y los archivos que hay en la carpeta
/// de las plantillas (la ruta se elige antes, de ENTORNO.PATHFILES).</summary>
public class RfidConfiguracionService : IRfidConfiguracionService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(RfidConfiguracionService);
    private const int MaxArchivos = 1000;
    private static readonly char[] InvalidosEnNombre = { '\\', '/', ':', '*', '?', '"', '<', '>', '|', ',' };

    private readonly IRfidEquiposRepository _equipos;
    private readonly IPathFilesRepository _pathFiles;
    private readonly ErrorLogService _log;
    public RfidConfiguracionService(IRfidEquiposRepository equipos, IPathFilesRepository pathFiles, ErrorLogService log)
    { _equipos = equipos; _pathFiles = pathFiles; _log = log; }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> EstacionesAsync(string? codGrupoProducto, string? codFamiliaProducto, string? codSubFamiliaProducto)
    {
        try { var d = (await _equipos.EstacionesComboAsync(codGrupoProducto, codFamiliaProducto, codSubFamiliaProducto)).ToList();
              return ApiResponse<IEnumerable<ComboboxItem>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(EstacionesAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> ArchivosPlantillaAsync(string codKeyPathfile)
    {
        try
        {
            var pf = await _pathFiles.ShowbyidAsync(codKeyPathfile?.Trim() ?? "");
            if (pf is null || string.IsNullOrWhiteSpace(pf.DES_PATH_FILE))
                return ApiResponse<IEnumerable<ComboboxItem>>.Fail($"No se encontró la ruta '{codKeyPathfile}' en ENTORNO.PATHFILES, o no tiene servidor (la ruta completa se arma como \\\\IP + carpeta).");
            var d = ArchivosDe(pf.DES_PATH_FILE).Select(n => new ComboboxItem { Id = n, Texto = n }).ToList();
            return ApiResponse<IEnumerable<ComboboxItem>>.Ok(d, total: d.Count);
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ArchivosPlantillaAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    /// <summary>Los nombres de los archivos de una carpeta (sin subcarpetas), por nombre, hasta 1000. Salen solo los que se pueden guardar como plantilla (<see cref="NombreValido"/>).</summary>
    public static List<string> ArchivosDe(string carpeta)
    {
        if (!Directory.Exists(carpeta))
            throw new DirectoryNotFoundException($"La carpeta '{carpeta}' no existe o el servidor de la API no puede llegar a ella.");
        return Directory.EnumerateFiles(carpeta).Select(f => Path.GetFileName(f)).Where(NombreValido)
                        .OrderBy(n => n, StringComparer.OrdinalIgnoreCase).Take(MaxArchivos).ToList();
    }

    /// <summary>La misma regla que dbo.FNC_RFID_EQUIPOS: solo el nombre del archivo, sin carpetas ni \ / : * ? " &lt; &gt; | , ni dos puntos seguidos. Los ocultos (con punto delante) no cuentan.</summary>
    public static bool NombreValido(string? nombre)
        => !string.IsNullOrWhiteSpace(nombre) && !nombre.StartsWith('.') && nombre.IndexOfAny(InvalidosEnNombre) < 0 && !nombre.Contains("..");
}
