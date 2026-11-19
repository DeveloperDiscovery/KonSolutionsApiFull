using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class OrdenProduccionItemsRepository : IOrdenProduccionItemsRepository
{
    private readonly IDbConnectionFactory _db;
    public OrdenProduccionItemsRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<OrdenProduccionItems>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<OrdenProduccionItems>(SP.SCHEMAPlaneamiento.OrdenProduccionItems.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<OrdenProduccionItems?> ShowbyidAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<OrdenProduccionItems>(SP.SCHEMAPlaneamiento.OrdenProduccionItems.Showbyid, new { COD_PROCESO = COD_PROCESO, NUM_ORDEN_PRODUCCION = NUM_ORDEN_PRODUCCION, NUM_ORDEN_PRODUCCION_ITEM = NUM_ORDEN_PRODUCCION_ITEM }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAPlaneamiento.OrdenProduccionItems.Showcombox);
    }

    /// <summary>Los items de una orden. Igual que en OrdenProduccion, la clave es
    /// NUM_ORDEN_PRODUCCION_ITEM y no arranca con COD_.</summary>
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxPorOrdenAsync(string codProceso, int numOrdenProduccion)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAPlaneamiento.OrdenProduccionItems.Showcombox,
            "NUM_ORDEN_PRODUCCION_ITEM", "DES_PRODUCTO",
            new { COD_PROCESO = codProceso, NUM_ORDEN_PRODUCCION = numOrdenProduccion });
    }

    public async Task<IEnumerable<OrdenProduccionItems>> SearchAsync(OrdenProduccionItemsSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<OrdenProduccionItems>(SP.SCHEMAPlaneamiento.OrdenProduccionItems.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(OrdenProduccionItemsInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_RUTA_PRODUCCION", dto.COD_RUTA_PRODUCCION);
        p.Add("CAN_PRODUCIR", dto.CAN_PRODUCIR);
        p.Add("CAN_PRODUCIDA", dto.CAN_PRODUCIDA);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("CAN_PRODUCIR_SECUNDARIA", dto.CAN_PRODUCIR_SECUNDARIA);
        p.Add("CAN_PRODUCIDA_SECUNDARIA", dto.CAN_PRODUCIDA_SECUNDARIA);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIA", dto.COD_UNIDAD_MEDIDA_SECUNDARIA);
        p.Add("FLG_REPOSICION", dto.FLG_REPOSICION ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.OrdenProduccionItems.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.OrdenProduccionItems.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.OrdenProduccionItems.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(OrdenProduccionItemsUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_RUTA_PRODUCCION", dto.COD_RUTA_PRODUCCION);
        p.Add("CAN_PRODUCIR", dto.CAN_PRODUCIR);
        p.Add("CAN_PRODUCIDA", dto.CAN_PRODUCIDA);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("CAN_PRODUCIR_SECUNDARIA", dto.CAN_PRODUCIR_SECUNDARIA);
        p.Add("CAN_PRODUCIDA_SECUNDARIA", dto.CAN_PRODUCIDA_SECUNDARIA);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIA", dto.COD_UNIDAD_MEDIDA_SECUNDARIA);
        p.Add("FLG_REPOSICION", dto.FLG_REPOSICION ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.OrdenProduccionItems.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.OrdenProduccionItems.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.OrdenProduccionItems.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.OrdenProduccionItems.Delete, new { COD_PROCESO = COD_PROCESO, NUM_ORDEN_PRODUCCION = NUM_ORDEN_PRODUCCION, NUM_ORDEN_PRODUCCION_ITEM = NUM_ORDEN_PRODUCCION_ITEM }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.OrdenProduccionItems.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.OrdenProduccionItems.Delete, new { COD_PROCESO = COD_PROCESO, NUM_ORDEN_PRODUCCION = NUM_ORDEN_PRODUCCION, NUM_ORDEN_PRODUCCION_ITEM = NUM_ORDEN_PRODUCCION_ITEM })
        };
    }
}

}