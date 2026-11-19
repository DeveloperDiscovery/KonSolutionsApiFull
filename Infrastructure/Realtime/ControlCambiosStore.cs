using System.Collections.Concurrent;

namespace KonSolutionsApi.Infrastructure.Realtime;

/// <summary>
/// Almacén en memoria de las versiones de cambio por tabla.
/// Lo llena el ControlCambiosBackgroundService y lo leen los endpoints.
/// Singleton: una sola copia compartida para todos los usuarios.
/// </summary>
public class ControlCambiosStore
{
    private readonly ConcurrentDictionary<string, long> _versiones = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>Última versión consultada de la BD (sirve como "sello" global).</summary>
    public long SelloGlobal { get; private set; }

    public void Actualizar(IEnumerable<(string tabla, long version)> datos)
    {
        long maxVersion = SelloGlobal;
        foreach (var (tabla, version) in datos)
        {
            _versiones[tabla] = version;
            if (version > maxVersion) maxVersion = version;
        }
        SelloGlobal = maxVersion;
    }

    /// <summary>Versión actual de una tabla (0 si no se conoce).</summary>
    public long VersionDe(string tabla)
        => _versiones.TryGetValue(tabla, out var v) ? v : 0;

    /// <summary>Snapshot de todas las versiones conocidas.</summary>
    public Dictionary<string, long> Todas()
        => new(_versiones, StringComparer.OrdinalIgnoreCase);
}
