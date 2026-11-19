using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EntidadesUsuariosSugeridosRepository : IEntidadesUsuariosSugeridosRepository
{
    private readonly IDbConnectionFactory _db;
    public EntidadesUsuariosSugeridosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EntidadUsuarioSugerido>> ComercialesAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadUsuarioSugerido>(SP.SCHEMAEstandar.EntidadesUsuariosSugeridos.Comerciales, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<EntidadUsuarioSugerido>> DependientesAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadUsuarioSugerido>(SP.SCHEMAEstandar.EntidadesUsuariosSugeridos.Dependientes, commandType: CommandType.StoredProcedure);
    }

    /// <summary>PROC_ESTANDAR_ENTIDADES_DEPENDIENTES_SHOWCOMBOX: las personas que dependen de
    /// UNA entidad puntual (COD_ENTIDAD_DEPENDENCIA = @COD_ENTIDAD). Alimenta el combo de
    /// Vendedor del Pedido Comercial, donde la entidad es el proveedor del pedido.
    /// Devuelve la misma forma que DependientesAsync, por eso comparte EntidadUsuarioSugerido.</summary>
    public async Task<IEnumerable<EntidadUsuarioSugerido>> DependientesPorEntidadAsync(decimal codEntidad)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadUsuarioSugerido>(
            SP.SCHEMAEstandar.EntidadesUsuariosSugeridos.DependientesShowcombox,
            new { COD_ENTIDAD = codEntidad }, commandType: CommandType.StoredProcedure);
    }
}
