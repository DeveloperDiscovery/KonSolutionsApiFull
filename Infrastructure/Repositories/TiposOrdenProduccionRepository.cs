using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposOrdenProduccionRepository : ITiposOrdenProduccionRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposOrdenProduccionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposOrdenProduccion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposOrdenProduccion>(SP.SCHEMAPlaneamiento.TiposOrdenProduccion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposOrdenProduccion?> ShowbyidAsync(string COD_TIPO_ORDEN_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposOrdenProduccion>(SP.SCHEMAPlaneamiento.TiposOrdenProduccion.Showbyid, new { COD_TIPO_ORDEN_PRODUCCION = COD_TIPO_ORDEN_PRODUCCION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAPlaneamiento.TiposOrdenProduccion.Showcombox);
    }

    public async Task<IEnumerable<TiposOrdenProduccion>> SearchAsync(TiposOrdenProduccionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposOrdenProduccion>(SP.SCHEMAPlaneamiento.TiposOrdenProduccion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposOrdenProduccionInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_ORDEN_PRODUCCION", dto.COD_TIPO_ORDEN_PRODUCCION);
        p.Add("DES_TIPO_ORDEN_PRODUCCION", dto.DES_TIPO_ORDEN_PRODUCCION);
        p.Add("COD_TIPO_ENTIDAD", dto.COD_TIPO_ENTIDAD);
        p.Add("FLG_ORDEN_PROPIA", dto.FLG_ORDEN_PROPIA ?? false);
        p.Add("FLG_SERVICIO_PRODUCCION", dto.FLG_SERVICIO_PRODUCCION ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.TiposOrdenProduccion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.TiposOrdenProduccion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.TiposOrdenProduccion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposOrdenProduccionUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_ORDEN_PRODUCCION", dto.COD_TIPO_ORDEN_PRODUCCION);
        p.Add("DES_TIPO_ORDEN_PRODUCCION", dto.DES_TIPO_ORDEN_PRODUCCION);
        p.Add("COD_TIPO_ENTIDAD", dto.COD_TIPO_ENTIDAD);
        p.Add("FLG_ORDEN_PROPIA", dto.FLG_ORDEN_PROPIA ?? false);
        p.Add("FLG_SERVICIO_PRODUCCION", dto.FLG_SERVICIO_PRODUCCION ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.TiposOrdenProduccion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.TiposOrdenProduccion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.TiposOrdenProduccion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_ORDEN_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.TiposOrdenProduccion.Delete, new { COD_TIPO_ORDEN_PRODUCCION = COD_TIPO_ORDEN_PRODUCCION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.TiposOrdenProduccion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.TiposOrdenProduccion.Delete, new { COD_TIPO_ORDEN_PRODUCCION = COD_TIPO_ORDEN_PRODUCCION })
        };
    }
}

}