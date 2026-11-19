using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PedidosComercialesMovimientosAlmacenRepository : IPedidosComercialesMovimientosAlmacenRepository
{
    private readonly IDbConnectionFactory _db;
    public PedidosComercialesMovimientosAlmacenRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PedidosComercialesMovimientosAlmacen>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidosComercialesMovimientosAlmacen>(SP.SCHEMAComercial.PedidosComercialesMovimientosAlmacen.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PedidosComercialesMovimientosAlmacen?> ShowbyidAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA_PEDIDO, decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PedidosComercialesMovimientosAlmacen>(SP.SCHEMAComercial.PedidosComercialesMovimientosAlmacen.Showbyid, new { NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA_PEDIDO = NUM_SECUENCIA_PEDIDO, NUM_MOVIMIENTO_ALMACEN = NUM_MOVIMIENTO_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.PedidosComercialesMovimientosAlmacen.Showcombox);
    }

    public async Task<IEnumerable<PedidosComercialesMovimientosAlmacen>> SearchAsync(PedidosComercialesMovimientosAlmacenSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidosComercialesMovimientosAlmacen>(SP.SCHEMAComercial.PedidosComercialesMovimientosAlmacen.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PedidosComercialesMovimientosAlmacenInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_SECUENCIA_PEDIDO", dto.NUM_SECUENCIA_PEDIDO);
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("NUM_SECUENCIA_MOVIMIENTO", dto.NUM_SECUENCIA_MOVIMIENTO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.PedidosComercialesMovimientosAlmacen.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.PedidosComercialesMovimientosAlmacen.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosComercialesMovimientosAlmacen.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA_PEDIDO, decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.PedidosComercialesMovimientosAlmacen.Delete, new { NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA_PEDIDO = NUM_SECUENCIA_PEDIDO, NUM_MOVIMIENTO_ALMACEN = NUM_MOVIMIENTO_ALMACEN }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.PedidosComercialesMovimientosAlmacen.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosComercialesMovimientosAlmacen.Delete, new { NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA_PEDIDO = NUM_SECUENCIA_PEDIDO, NUM_MOVIMIENTO_ALMACEN = NUM_MOVIMIENTO_ALMACEN })
        };
    }
}

}