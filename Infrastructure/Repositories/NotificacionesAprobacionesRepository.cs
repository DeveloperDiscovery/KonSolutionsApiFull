using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class NotificacionesAprobacionesRepository : INotificacionesAprobacionesRepository
{
    private readonly IDbConnectionFactory _db;
    public NotificacionesAprobacionesRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>
    /// Ficha completa de una notificación de aprobación: cabecera, documentos vinculados
    /// (para el visor de PDF) y el historial/acciones disponibles de ejecución.
    /// </summary>
    public async Task<NotificacionAprobacionResult> ShowbyidAsync(int numNotificacion, string codUsuario)
    {
        using var conn = _db.CreateConnection();
        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMAEntorno.NotificacionesAprobaciones.Showbyid,
            new { NUM_NOTIFICACION = numNotificacion, COD_USUARIO = codUsuario },
            commandType: CommandType.StoredProcedure);

        var ficha      = await multi.ReadFirstOrDefaultAsync<NotificacionAprobacionFicha>();
        var documentos = (await multi.ReadAsync<NotificacionAprobacionDocumento>()).ToList();
        var historial  = (await multi.ReadAsync<NotificacionAprobacionHistorial>()).ToList();
        var ejecucion  = (await multi.ReadAsync<NotificacionAprobacionEjecucion>()).ToList();

        return new NotificacionAprobacionResult
        {
            Ficha = ficha, Documentos = documentos, Historial = historial, Ejecucion = ejecucion
        };
    }

    /// <summary>
    /// Aprobar, rechazar u observar. El procedimiento hace todo en una transacción: comprueba que el usuario sea el aprobador
    /// vigente, ejecuta el comando que la notificación dejó guardado para esa acción y deja el estado. El texto SQL nunca sale de la base.
    /// </summary>
    public async Task<(WriteResult Resultado, NotificacionAprobacionAccionResult? Accion)> AccionAsync(int numNotificacion, string codAccion, string codUsuario, string codEstacion, string? desObservacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_NOTIFICACION", numNotificacion);
        p.Add("COD_ACCION", codAccion);
        p.Add("COD_USUARIO", codUsuario);
        p.Add("COD_ESTACION", codEstacion);
        p.Add("DES_OBSERVACION", string.IsNullOrWhiteSpace(desObservacion) ? null : desObservacion.Trim());
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<NotificacionAprobacionAccionResult>(SP.SCHEMAEntorno.NotificacionesAprobaciones.Accion, p, commandType: CommandType.StoredProcedure);
            return (new WriteResult { Estado = "OK", AffectedRows = 1 }, r);
        }
        catch (SqlException ex)
        {
            var sentencia = SqlSentenceHelper.Build(SP.SCHEMAEntorno.NotificacionesAprobaciones.Accion, new { NUM_NOTIFICACION = numNotificacion, COD_ACCION = codAccion, COD_USUARIO = codUsuario });
            return (new WriteResult { Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number, ErrorProcedure = SP.SCHEMAEntorno.NotificacionesAprobaciones.Accion, SqlSentence = sentencia }, null);
        }
    }
}
