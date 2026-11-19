using KonSolutionsApi.Application.Interfaces;

namespace KonSolutionsApi.Infrastructure.XmlRecibidos;

/// <summary>Corre el servicio de XML recibidos cada tantos minutos (los de la configuración general; 15 si están vacíos) mientras la carga automática esté activa. Si está apagada, o falta la cuenta de Microsoft 365,
/// solo se asoma a la configuración una vez por minuto. Una falla no lo detiene: se anota y se vuelve a intentar en la siguiente.</summary>
public class XmlRecibidosBackgroundService : BackgroundService
{
    private readonly IServiceProvider _sp;
    private readonly ILogger<XmlRecibidosBackgroundService> _log;
    public static TimeSpan EsperaInicial = TimeSpan.FromSeconds(30);
    public static TimeSpan UnMinuto = TimeSpan.FromMinutes(1);

    public XmlRecibidosBackgroundService(IServiceProvider sp, ILogger<XmlRecibidosBackgroundService> log) { _sp = sp; _log = log; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try { await Task.Delay(EsperaInicial, stoppingToken); } catch (OperationCanceledException) { return; }
        var avisoCuenta = false;
        while (!stoppingToken.IsCancellationRequested)
        {
            var espera = UnMinuto;
            try
            {
                using var scope = _sp.CreateScope();
                var graph = scope.ServiceProvider.GetRequiredService<IGraphCorreo>();
                if (!graph.Configurado)
                {
                    if (!avisoCuenta) { _log.LogInformation("XmlRecibidos: falta la cuenta de Microsoft 365 en appsettings (sección XmlRecibidos); el servicio no corre."); avisoCuenta = true; }
                }
                else
                {
                    avisoCuenta = false;
                    var repo = scope.ServiceProvider.GetRequiredService<IXmlRecibidosRepository>();
                    var cfg = await repo.ConfigAsync();
                    if (cfg?.FLG_XML_RECIBIDOS_ACTIVO == true)
                    {
                        var svc = scope.ServiceProvider.GetRequiredService<IXmlRecibidosService>();
                        var c = await svc.EjecutarAsync(false, stoppingToken);
                        if (c.Mensaje is not null) _log.LogWarning("XmlRecibidos: {Mensaje}", c.Mensaje);
                        else if (c.Correos > 0) _log.LogInformation("XmlRecibidos: {Correos} correos, {Procesados} procesados, {Errores} con error, {Xml} XML cargados.", c.Correos, c.CorreosProcesados, c.CorreosConError, c.XmlCargados);
                        espera = TimeSpan.FromMinutes(Math.Clamp(cfg.CAN_MINUTOS_XML_RECIBIDOS ?? 15, 1, 1440));
                    }
                }
            }
            catch (OperationCanceledException) { break; }
            catch (Exception ex) { _log.LogWarning(ex, "XmlRecibidos: la corrida programada falló"); }
            try { await Task.Delay(espera, stoppingToken); } catch (OperationCanceledException) { break; }
        }
    }
}
