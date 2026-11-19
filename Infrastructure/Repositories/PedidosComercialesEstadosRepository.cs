using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PedidosComercialesEstadosRepository : IPedidosComercialesEstadosRepository
{
    private readonly IDbConnectionFactory _db;
    public PedidosComercialesEstadosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PedidosComercialesEstados>> ShowallAsync(string codClaseOperacion, string codTipoOperacion, int numPedidoComercial)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidosComercialesEstados>(
            SP.SCHEMAComercial.PedidosComercialesEstados.Showall,
            new { COD_CLASE_OPERACION = codClaseOperacion, COD_TIPO_OPERACION = codTipoOperacion, NUM_PEDIDO_COMERCIAL = numPedidoComercial },
            commandType: CommandType.StoredProcedure);
    }
}
