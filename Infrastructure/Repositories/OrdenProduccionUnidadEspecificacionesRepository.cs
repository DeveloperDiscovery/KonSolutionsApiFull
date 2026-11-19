using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class OrdenProduccionUnidadEspecificacionesRepository : IOrdenProduccionUnidadEspecificacionesRepository
{
    private readonly IDbConnectionFactory _db;
    public OrdenProduccionUnidadEspecificacionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<OrdenProduccionUnidadEspecificaciones>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<OrdenProduccionUnidadEspecificaciones>(SP.SCHEMAProduccion.OrdenProduccionUnidadEspecificaciones.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<OrdenProduccionUnidadEspecificaciones?> ShowbyidAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<OrdenProduccionUnidadEspecificaciones>(SP.SCHEMAProduccion.OrdenProduccionUnidadEspecificaciones.Showbyid, new { COD_PROCESO = COD_PROCESO, NUM_ORDEN_PRODUCCION = NUM_ORDEN_PRODUCCION, NUM_ORDEN_PRODUCCION_ITEM = NUM_ORDEN_PRODUCCION_ITEM }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProduccion.OrdenProduccionUnidadEspecificaciones.Showcombox);
    }

    public async Task<IEnumerable<OrdenProduccionUnidadEspecificaciones>> SearchAsync(OrdenProduccionUnidadEspecificacionesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<OrdenProduccionUnidadEspecificaciones>(SP.SCHEMAProduccion.OrdenProduccionUnidadEspecificaciones.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(OrdenProduccionUnidadEspecificacionesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_ITEM_TECNICO", dto.COD_ITEM_TECNICO);
        p.Add("VAL_ESPACIFICACION_TENICA", dto.VAL_ESPACIFICACION_TENICA);
        p.Add("COD_CLASE_DEFECTO_TECNICO", dto.COD_CLASE_DEFECTO_TECNICO);
        p.Add("COD_TIPO_DEFECTO_TECNICO", dto.COD_TIPO_DEFECTO_TECNICO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.OrdenProduccionUnidadEspecificaciones.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.OrdenProduccionUnidadEspecificaciones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.OrdenProduccionUnidadEspecificaciones.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(OrdenProduccionUnidadEspecificacionesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_ITEM_TECNICO", dto.COD_ITEM_TECNICO);
        p.Add("VAL_ESPACIFICACION_TENICA", dto.VAL_ESPACIFICACION_TENICA);
        p.Add("COD_CLASE_DEFECTO_TECNICO", dto.COD_CLASE_DEFECTO_TECNICO);
        p.Add("COD_TIPO_DEFECTO_TECNICO", dto.COD_TIPO_DEFECTO_TECNICO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.OrdenProduccionUnidadEspecificaciones.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.OrdenProduccionUnidadEspecificaciones.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.OrdenProduccionUnidadEspecificaciones.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.OrdenProduccionUnidadEspecificaciones.Delete, new { COD_PROCESO = COD_PROCESO, NUM_ORDEN_PRODUCCION = NUM_ORDEN_PRODUCCION, NUM_ORDEN_PRODUCCION_ITEM = NUM_ORDEN_PRODUCCION_ITEM }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.OrdenProduccionUnidadEspecificaciones.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.OrdenProduccionUnidadEspecificaciones.Delete, new { COD_PROCESO = COD_PROCESO, NUM_ORDEN_PRODUCCION = NUM_ORDEN_PRODUCCION, NUM_ORDEN_PRODUCCION_ITEM = NUM_ORDEN_PRODUCCION_ITEM })
        };
    }
}

}