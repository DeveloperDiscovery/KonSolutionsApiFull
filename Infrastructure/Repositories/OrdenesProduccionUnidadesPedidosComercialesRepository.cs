using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class OrdenesProduccionUnidadesPedidosComercialesRepository : IOrdenesProduccionUnidadesPedidosComercialesRepository
{
    private readonly IDbConnectionFactory _db;
    public OrdenesProduccionUnidadesPedidosComercialesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<OrdenesProduccionUnidadesPedidosComerciales>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<OrdenesProduccionUnidadesPedidosComerciales>(SP.SCHEMAProduccion.OrdenesProduccionUnidadesPedidosComerciales.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<OrdenesProduccionUnidadesPedidosComerciales?> ShowbyidAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<OrdenesProduccionUnidadesPedidosComerciales>(SP.SCHEMAProduccion.OrdenesProduccionUnidadesPedidosComerciales.Showbyid, new { COD_PROCESO = COD_PROCESO, NUM_ORDEN_PRODUCCION = NUM_ORDEN_PRODUCCION, NUM_ORDEN_PRODUCCION_ITEM = NUM_ORDEN_PRODUCCION_ITEM }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProduccion.OrdenesProduccionUnidadesPedidosComerciales.Showcombox);
    }

    public async Task<IEnumerable<OrdenesProduccionUnidadesPedidosComerciales>> SearchAsync(OrdenesProduccionUnidadesPedidosComercialesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<OrdenesProduccionUnidadesPedidosComerciales>(SP.SCHEMAProduccion.OrdenesProduccionUnidadesPedidosComerciales.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(OrdenesProduccionUnidadesPedidosComercialesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("CAN_PRODUCIR", dto.CAN_PRODUCIR);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.OrdenesProduccionUnidadesPedidosComerciales.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.OrdenesProduccionUnidadesPedidosComerciales.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.OrdenesProduccionUnidadesPedidosComerciales.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(OrdenesProduccionUnidadesPedidosComercialesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("CAN_PRODUCIR", dto.CAN_PRODUCIR);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.OrdenesProduccionUnidadesPedidosComerciales.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.OrdenesProduccionUnidadesPedidosComerciales.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.OrdenesProduccionUnidadesPedidosComerciales.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.OrdenesProduccionUnidadesPedidosComerciales.Delete, new { COD_PROCESO = COD_PROCESO, NUM_ORDEN_PRODUCCION = NUM_ORDEN_PRODUCCION, NUM_ORDEN_PRODUCCION_ITEM = NUM_ORDEN_PRODUCCION_ITEM }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.OrdenesProduccionUnidadesPedidosComerciales.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.OrdenesProduccionUnidadesPedidosComerciales.Delete, new { COD_PROCESO = COD_PROCESO, NUM_ORDEN_PRODUCCION = NUM_ORDEN_PRODUCCION, NUM_ORDEN_PRODUCCION_ITEM = NUM_ORDEN_PRODUCCION_ITEM })
        };
    }
}

}