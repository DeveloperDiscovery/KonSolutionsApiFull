using System.Collections.Concurrent;

namespace KonSolutionsApi.Infrastructure.Realtime;

/// <summary>Una línea del trace: un SP ejecutado, con su duración real.</summary>
public class SpTraceEntry
{
    public DateTime FEC_HORA      { get; set; }
    public string   DES_SP        { get; set; } = string.Empty;
    public string   DES_EXEC      { get; set; } = string.Empty;
    public long     VAL_DURACION_MS { get; set; }
}

/// <summary>
/// Buffer circular en memoria con los últimos N stored procedures ejecutados
/// (nombre, línea EXEC completa y duración real medida en SpTracingCommand).
/// Lo alimenta ErrorLogService.LogSpTrace y lo consume el Dashboard de Monitoreo
/// (pestaña "Trace de Procesos") vía polling. Singleton compartido por toda la API.
/// </summary>
public class SpTraceBuffer
{
    private readonly ConcurrentQueue<SpTraceEntry> _cola = new();
    private const int Capacidad = 500;

    public void Add(string execLine, long duracionMs)
    {
        // Nombre del SP: lo que va entre "EXEC " y el primer espacio/param.
        var sp = execLine;
        if (sp.StartsWith("EXEC ", StringComparison.OrdinalIgnoreCase))
            sp = sp.Substring(5);
        var corte = sp.IndexOf(' ');
        if (corte > 0) sp = sp.Substring(0, corte);

        _cola.Enqueue(new SpTraceEntry
        {
            FEC_HORA        = DateTime.Now,
            DES_SP          = sp.Trim(),
            DES_EXEC        = execLine,
            VAL_DURACION_MS = duracionMs
        });

        while (_cola.Count > Capacidad && _cola.TryDequeue(out _)) { }
    }

    /// <summary>Devuelve las entradas más recientes primero (hasta 'top').</summary>
    public List<SpTraceEntry> GetRecent(int top = 200)
        => _cola.Reverse().Take(top).ToList();
}
