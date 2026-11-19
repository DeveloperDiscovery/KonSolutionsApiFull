using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class OrdenProduccionUnidadRepository : IOrdenProduccionUnidadRepository
{
    private readonly IDbConnectionFactory _db;
    public OrdenProduccionUnidadRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<OrdenProduccionUnidad>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<OrdenProduccionUnidad>(SP.SCHEMAPlaneamiento.OrdenProduccionUnidad.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<OrdenProduccionUnidad?> ShowbyidAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<OrdenProduccionUnidad>(SP.SCHEMAPlaneamiento.OrdenProduccionUnidad.Showbyid, new { COD_PROCESO = COD_PROCESO, NUM_ORDEN_PRODUCCION = NUM_ORDEN_PRODUCCION, NUM_ORDEN_PRODUCCION_ITEM = NUM_ORDEN_PRODUCCION_ITEM }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAPlaneamiento.OrdenProduccionUnidad.Showcombox);
    }

    /// <summary>Las unidades mínimas de un item. La clave es NUM_ORDEN_PRODUCCION_UNIDAD y el
    /// SP no trae una descripción propia de la unidad, así que el texto es el alias del
    /// proceso: alcanza para distinguir una fila de otra en el combo.</summary>
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxPorItemAsync(string codProceso, int numOrdenProduccion, int numOrdenProduccionItem)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAPlaneamiento.OrdenProduccionUnidad.Showcombox,
            "NUM_ORDEN_PRODUCCION_UNIDAD", "DES_NOMBRE_ORDEN_PRODUCCION_UNIDAD",
            new { COD_PROCESO = codProceso, NUM_ORDEN_PRODUCCION = numOrdenProduccion, NUM_ORDEN_PRODUCCION_ITEM = numOrdenProduccionItem });
    }

    public async Task<IEnumerable<OrdenProduccionUnidad>> SearchAsync(OrdenProduccionUnidadSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<OrdenProduccionUnidad>(SP.SCHEMAPlaneamiento.OrdenProduccionUnidad.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(OrdenProduccionUnidadInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
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
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.OrdenProduccionUnidad.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.OrdenProduccionUnidad.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.OrdenProduccionUnidad.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(OrdenProduccionUnidadUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
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
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.OrdenProduccionUnidad.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.OrdenProduccionUnidad.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.OrdenProduccionUnidad.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.OrdenProduccionUnidad.Delete, new { COD_PROCESO = COD_PROCESO, NUM_ORDEN_PRODUCCION = NUM_ORDEN_PRODUCCION, NUM_ORDEN_PRODUCCION_ITEM = NUM_ORDEN_PRODUCCION_ITEM }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.OrdenProduccionUnidad.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.OrdenProduccionUnidad.Delete, new { COD_PROCESO = COD_PROCESO, NUM_ORDEN_PRODUCCION = NUM_ORDEN_PRODUCCION, NUM_ORDEN_PRODUCCION_ITEM = NUM_ORDEN_PRODUCCION_ITEM })
        };
    }
}

}