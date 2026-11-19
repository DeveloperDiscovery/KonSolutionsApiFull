using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class OrdenProduccionRepository : IOrdenProduccionRepository
{
    private readonly IDbConnectionFactory _db;
    public OrdenProduccionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<OrdenProduccion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<OrdenProduccion>(SP.SCHEMAPlaneamiento.OrdenProduccion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<OrdenProduccion?> ShowbyidAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<OrdenProduccion>(SP.SCHEMAPlaneamiento.OrdenProduccion.Showbyid, new { COD_PROCESO = COD_PROCESO, NUM_ORDEN_PRODUCCION = NUM_ORDEN_PRODUCCION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAPlaneamiento.OrdenProduccion.Showcombox);
    }

    /// <summary>Las órdenes de un proceso. El SP identifica la fila por NUM_ORDEN_PRODUCCION,
    /// no por COD_ — por eso va con las columnas explícitas y no por convención.</summary>
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxPorProcesoAsync(string codProceso)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAPlaneamiento.OrdenProduccion.Showcombox,
            "NUM_ORDEN_PRODUCCION", "DES_ORDEN_PRODUCCION", new { COD_PROCESO = codProceso });
    }

    public async Task<IEnumerable<OrdenProduccion>> SearchAsync(OrdenProduccionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<OrdenProduccion>(SP.SCHEMAPlaneamiento.OrdenProduccion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(OrdenProduccionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("DES_ORDEN_PRODUCCION", dto.DES_ORDEN_PRODUCCION);
        p.Add("COD_TIPO_ORDEN_PRODUCCION", dto.COD_TIPO_ORDEN_PRODUCCION);
        p.Add("COD_ENTIDAD_PROPIETARIA", dto.COD_ENTIDAD_PROPIETARIA);
        p.Add("COD_RUTA_PRODUCCION", dto.COD_RUTA_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_PROPIETARIA", dto.NUM_ORDEN_PRODUCCION_PROPIETARIA);
        p.Add("FEC_ORDEN_PRODUCCION", dto.FEC_ORDEN_PRODUCCION);
        p.Add("FEC_INICIO_PRODUCCION", dto.FEC_INICIO_PRODUCCION);
        p.Add("FEC_ENTREGA_PRODUCCION", dto.FEC_ENTREGA_PRODUCCION);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_ACTIVO_MAQUINA", dto.COD_ACTIVO_MAQUINA);
        p.Add("COD_LINEA_PRODUCCION", dto.COD_LINEA_PRODUCCION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.OrdenProduccion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.OrdenProduccion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.OrdenProduccion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(OrdenProduccionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("DES_ORDEN_PRODUCCION", dto.DES_ORDEN_PRODUCCION);
        p.Add("COD_TIPO_ORDEN_PRODUCCION", dto.COD_TIPO_ORDEN_PRODUCCION);
        p.Add("COD_ENTIDAD_PROPIETARIA", dto.COD_ENTIDAD_PROPIETARIA);
        p.Add("COD_RUTA_PRODUCCION", dto.COD_RUTA_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_PROPIETARIA", dto.NUM_ORDEN_PRODUCCION_PROPIETARIA);
        p.Add("FEC_ORDEN_PRODUCCION", dto.FEC_ORDEN_PRODUCCION);
        p.Add("FEC_INICIO_PRODUCCION", dto.FEC_INICIO_PRODUCCION);
        p.Add("FEC_ENTREGA_PRODUCCION", dto.FEC_ENTREGA_PRODUCCION);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_ACTIVO_MAQUINA", dto.COD_ACTIVO_MAQUINA);
        p.Add("COD_LINEA_PRODUCCION", dto.COD_LINEA_PRODUCCION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.OrdenProduccion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.OrdenProduccion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.OrdenProduccion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.OrdenProduccion.Delete, new { COD_PROCESO = COD_PROCESO, NUM_ORDEN_PRODUCCION = NUM_ORDEN_PRODUCCION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.OrdenProduccion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.OrdenProduccion.Delete, new { COD_PROCESO = COD_PROCESO, NUM_ORDEN_PRODUCCION = NUM_ORDEN_PRODUCCION })
        };
    }
}

}