using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PedidosComercialesComparativoRepository : IPedidosComercialesComparativoRepository
{
    private readonly IDbConnectionFactory _db;
    public PedidosComercialesComparativoRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>Requerimientos que ya tienen al menos un pedido apuntándolos.</summary>
    public async Task<IEnumerable<RequerimientoConPedidos>> RequerimientosConPedidosAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RequerimientoConPedidos>(
            SP.SCHEMAComercial.PedidosComercialesComparativo.RequerimientosConPedidos,
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>El SP devuelve dos resultsets —cabeceras y líneas— y se leen con
    /// QueryMultiple en un solo viaje. El orden importa: primero pedidos, después
    /// líneas, igual que en el procedimiento.</summary>
    public async Task<ComparativoResultado> ComparativoAsync(string codClaseOperacion, string codMoneda, string requerimientos)
    {
        using var conn = _db.CreateConnection();
        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMAComercial.PedidosComercialesComparativo.Comparativo,
            new { COD_CLASE_OPERACION = codClaseOperacion, COD_MONEDA = codMoneda, DES_REQUERIMIENTOS = requerimientos },
            commandType: CommandType.StoredProcedure);

        return new ComparativoResultado
        {
            Pedidos = (await multi.ReadAsync<ComparativoPedidoCabecera>()).ToList(),
            Lineas  = (await multi.ReadAsync<ComparativoPedidoLinea>()).ToList()
        };
    }
}
