using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SolicitudesComercialesNotificacionesRepository : ISolicitudesComercialesNotificacionesRepository
{
    private readonly IDbConnectionFactory _db;
    public SolicitudesComercialesNotificacionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<SolicitudesComercialesNotificaciones>> ShowallAsync(int numSolicitudComercial)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SolicitudesComercialesNotificaciones>(
            SP.SCHEMAComercial.SolicitudesComercialesNotificaciones.Showall,
            new { NUM_SOLICITUD_COMERCIAL = numSolicitudComercial }, commandType: CommandType.StoredProcedure);
    }

    public async Task<SolicitudesComercialesNotificaciones?> ShowbyidAsync(int numSolicitudComercial, int numNotificacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<SolicitudesComercialesNotificaciones>(
            SP.SCHEMAComercial.SolicitudesComercialesNotificaciones.Showbyid,
            new { NUM_SOLICITUD_COMERCIAL = numSolicitudComercial, NUM_NOTIFICACION = numNotificacion },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<SolicitudesComercialesNotificaciones>> SearchAsync(SolicitudesComercialesNotificacionesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SolicitudesComercialesNotificaciones>(
            SP.SCHEMAComercial.SolicitudesComercialesNotificaciones.Search,
            new { NUM_SOLICITUD_COMERCIAL = dto.NUM_SOLICITUD_COMERCIAL, SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }
}
