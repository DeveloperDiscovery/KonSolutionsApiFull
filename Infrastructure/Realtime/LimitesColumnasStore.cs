using System.Collections.Concurrent;

namespace KonSolutionsApi.Infrastructure.Realtime;

/// <summary>
/// Representa los metadatos de tipo/tamaño de una columna de la BD,
/// tal como los devuelve PROC_ENTORNO_DATABASE_TIPOS_DATO_SWHOWALL.
/// </summary>
public class LimiteColumna
{
    /// <summary>Schema de la tabla (ej. SEGURIDAD).</summary>
    public string TABLE_SCHEMA { get; set; } = "";
    /// <summary>Nombre de la tabla (ej. USUARIOS).</summary>
    public string TABLE_NAME { get; set; } = "";
    /// <summary>Nombre de la columna (ej. COD_USUARIO).</summary>
    public string COLUMN_NAME { get; set; } = "";
    /// <summary>Tipo de dato SQL (varchar, int, decimal, etc.).</summary>
    public string DATA_TYPE { get; set; } = "";
    /// <summary>"NO" = columna obligatoria (NOT NULL); "YES" = acepta NULL.</summary>
    public string IS_NULLABLE { get; set; } = "YES";
    /// <summary>Longitud máxima para texto (null si no aplica).</summary>
    public int? CHARACTER_MAXIMUM_LENGTH { get; set; }
    /// <summary>Dígitos totales para numéricos (null si no aplica).</summary>
    public int? NUMERIC_PRECISION { get; set; }
    /// <summary>Dígitos decimales para numéricos (null si no aplica).</summary>
    public int? NUMERIC_SCALE { get; set; }
}

/// <summary>
/// Almacén en memoria de los límites de columnas.
/// Se llena una sola vez (la primera petición) y se reutiliza, para no
/// ejecutar el SP en cada llamada. Singleton compartido por todos los usuarios.
/// </summary>
public class LimitesColumnasStore
{
    private List<LimiteColumna>? _cache;
    private readonly object _lock = new();

    /// <summary>True si ya se cargaron los límites en memoria.</summary>
    public bool Cargado => _cache is not null;

    /// <summary>Guarda en memoria la lista de límites obtenida del SP.</summary>
    public void Set(List<LimiteColumna> datos)
    {
        lock (_lock) { _cache = datos; }
    }

    /// <summary>Devuelve los límites cacheados (vacío si aún no se cargaron).</summary>
    public List<LimiteColumna> Get()
    {
        lock (_lock) { return _cache ?? new List<LimiteColumna>(); }
    }
}
