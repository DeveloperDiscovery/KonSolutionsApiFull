using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Services;

public class ProyectosParametrosService : IProyectosParametrosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ProyectosParametrosService);
    private readonly IProyectosParametrosRepository _repo;
    private readonly ErrorLogService _log;
    public ProyectosParametrosService(IProyectosParametrosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<ProyectosParametros>> GetAsync()
    {
        try { return ApiResponse<ProyectosParametros>.Ok(await _repo.ShowAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAsync)); return ApiResponse<ProyectosParametros>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> GuardarAsync(ProyectosParametrosGuardarDto dto, string codUsuario, string codEstacion)
    {
        try { return ApiResponse<WriteResult>.FromWriteResult(await _repo.GuardarAsync(dto, codUsuario, codEstacion), "Parámetros guardados."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GuardarAsync)); return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}

/// <summary>Carpeta de archivos de los proyectos: la ruta configurada en Proyectos > Parámetros y, dentro, una subcarpeta por proyecto con su número a 10 dígitos.</summary>
public static class ProyectosCarpetas
{
    public static string Nombre(int num) => num.ToString("D10");

    /// <summary>La carpeta raíz configurada, o el motivo por el que no se puede usar.</summary>
    public static async Task<(string? Raiz, string? Error)> RaizAsync(IProyectosParametrosRepository parametros, IPathFilesRepository paths)
    {
        var p = await parametros.ShowAsync();
        var clave = p.COD_KEY_PATHFILE_PROYECTOS;
        if (string.IsNullOrWhiteSpace(clave)) return (null, "No hay una ruta de archivos para proyectos: elígela en Proyectos > Parámetros de proyectos.");
        var ruta = await paths.ShowbyidAsync(clave);
        if (ruta is null || string.IsNullOrWhiteSpace(ruta.DES_PATH_FILE)) return (null, $"La ruta «{clave}» no existe en Rutas de archivos o no tiene carpeta.");
        var carpeta = ruta.DES_PATH_FILE!.Trim();
        if (!Directory.Exists(carpeta)) return (null, $"La carpeta «{carpeta}» no existe o la API no puede verla.");
        return (carpeta, null);
    }

    /// <summary>Crea la subcarpeta del proyecto (0000000001). Devuelve null si quedó creada, o el aviso de por qué no.</summary>
    public static async Task<string?> CrearAsync(IProyectosParametrosRepository parametros, IPathFilesRepository paths, int num)
    {
        try
        {
            var (raiz, error) = await RaizAsync(parametros, paths);
            if (raiz is null) return error;
            Directory.CreateDirectory(Path.Combine(raiz, Nombre(num)));
            return null;
        }
        catch (Exception ex) { return $"No se pudo crear la carpeta del proyecto: {ex.Message}"; }
    }
}
