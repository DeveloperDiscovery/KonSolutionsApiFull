using System.Data;
using Dapper;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Realtime;

/// <summary>
/// Servicio en segundo plano que consulta ENTORNO.CONTROL_CAMBIOS cada cierto intervalo
/// y guarda las versiones en memoria (ControlCambiosStore).
/// Una sola consulta por intervalo sirve a TODOS los usuarios → eficiente.
/// </summary>
public class ControlCambiosBackgroundService : BackgroundService
{
    private readonly IServiceProvider _sp;
    private readonly ControlCambiosStore _store;
    private readonly ILogger<ControlCambiosBackgroundService> _log;

    // Intervalo de consulta a la BD (segundos). Ajustable.
    private readonly TimeSpan _intervalo = TimeSpan.FromSeconds(8);

    public ControlCambiosBackgroundService(
        IServiceProvider sp, ControlCambiosStore store, ILogger<ControlCambiosBackgroundService> log)
    {
        _sp = sp; _store = store; _log = log;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<IDbConnectionFactory>();
                using var conn = db.CreateConnection();

                var filas = await conn.QueryAsync<(string DES_TABLA, long NUM_VERSION)>(
                    "PROC_ENTORNO_CONTROL_CAMBIOS_VERSIONES_SHOWALL",
                    commandType: System.Data.CommandType.StoredProcedure);

                _store.Actualizar(filas.Select(f => (f.DES_TABLA, f.NUM_VERSION)));
            }
            catch (Exception ex)
            {
                _log.LogWarning(ex, "ControlCambios: no se pudo consultar CONTROL_CAMBIOS");
            }

            try { await Task.Delay(_intervalo, stoppingToken); }
            catch (TaskCanceledException) { break; }
        }
    }
}
