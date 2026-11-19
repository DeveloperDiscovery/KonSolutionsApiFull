using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SolicitudesComercialesClienteRepository : ISolicitudesComercialesClienteRepository
{
    private readonly IDbConnectionFactory _db;
    public SolicitudesComercialesClienteRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<SolicitudesComerciales>> ShowallAsync(decimal codEntidadCliente)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SolicitudesComerciales>(SP.SCHEMAComercial.SolicitudesComercialesCliente.Showall,
            new { COD_ENTIDAD_CLIENTE = codEntidadCliente }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<SolicitudesComerciales>> SearchAsync(decimal codEntidadCliente, string? search)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SolicitudesComerciales>(SP.SCHEMAComercial.SolicitudesComercialesCliente.Search,
            new { COD_ENTIDAD_CLIENTE = codEntidadCliente, SEARCH = search }, commandType: CommandType.StoredProcedure);
    }
}
