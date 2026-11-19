using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class NotificacionesFlujoRepository : INotificacionesFlujoRepository
{
    private readonly IDbConnectionFactory _db;
    public NotificacionesFlujoRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>
    /// Trae las notificaciones pendientes (COD_ESTADO = 0) del usuario y, en un segundo
    /// resultset, el resumen por COD_TIPO_NOTIFICACION (conteo + ícono) usado para pintar
    /// el grupo de íconos del AppBar sin repetir el ícono por cada notificación.
    /// </summary>
    public async Task<NotificacionesFlujoResult> UsuarioPendienteAsync(string codUsuario)
    {
        using var conn = _db.CreateConnection();
        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMAEntorno.NotificacionesFlujo.UsuarioPendiente,
            new { COD_USUARIO = codUsuario },
            commandType: CommandType.StoredProcedure);

        var notificaciones = (await multi.ReadAsync<NotificacionFlujoItem>()).ToList();
        var resumen        = (await multi.ReadAsync<NotificacionResumenTipo>()).ToList();

        return new NotificacionesFlujoResult { Notificaciones = notificaciones, Resumen = resumen };
    }

    /// <summary>Listado del módulo de consulta. recepcionada=true → Recibidas
    /// (como aprobador); false → Emitidas (como notificador).</summary>
    public async Task<IEnumerable<NotificacionFlujoDetalle>> ShowallAsync(string codUsuario, bool recepcionada)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<NotificacionFlujoDetalle>(
            SP.SCHEMAEntorno.NotificacionesFlujo.Showall,
            new { COD_USUARIO = codUsuario, FLG_RECEPCIONADA = recepcionada },
            commandType: CommandType.StoredProcedure);
    }
}
