using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposOperacionProduccionRepository : ITiposOperacionProduccionRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposOperacionProduccionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposOperacionProduccion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposOperacionProduccion>(SP.SCHEMAPlaneamiento.TiposOperacionProduccion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposOperacionProduccion?> ShowbyidAsync(string COD_TIPO_OPERACION_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposOperacionProduccion>(SP.SCHEMAPlaneamiento.TiposOperacionProduccion.Showbyid, new { COD_TIPO_OPERACION_PRODUCCION = COD_TIPO_OPERACION_PRODUCCION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAPlaneamiento.TiposOperacionProduccion.Showcombox);
    }

    public async Task<IEnumerable<TiposOperacionProduccion>> SearchAsync(TiposOperacionProduccionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposOperacionProduccion>(SP.SCHEMAPlaneamiento.TiposOperacionProduccion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposOperacionProduccionInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_OPERACION_PRODUCCION", dto.COD_TIPO_OPERACION_PRODUCCION);
        p.Add("DES_TIPO_OPERACION_PRODUCCION", dto.DES_TIPO_OPERACION_PRODUCCION);
        p.Add("COD_CLASE_OPERACION_PRODUCCION", dto.COD_CLASE_OPERACION_PRODUCCION);
        p.Add("COD_CATEGORIA_OPERACION_PRODUCCION", dto.COD_CATEGORIA_OPERACION_PRODUCCION);
        p.Add("FLG_CONTROL_CALIDAD", dto.FLG_CONTROL_CALIDAD ?? false);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.TiposOperacionProduccion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.TiposOperacionProduccion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.TiposOperacionProduccion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposOperacionProduccionUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_OPERACION_PRODUCCION", dto.COD_TIPO_OPERACION_PRODUCCION);
        p.Add("DES_TIPO_OPERACION_PRODUCCION", dto.DES_TIPO_OPERACION_PRODUCCION);
        p.Add("COD_CLASE_OPERACION_PRODUCCION", dto.COD_CLASE_OPERACION_PRODUCCION);
        p.Add("COD_CATEGORIA_OPERACION_PRODUCCION", dto.COD_CATEGORIA_OPERACION_PRODUCCION);
        p.Add("FLG_CONTROL_CALIDAD", dto.FLG_CONTROL_CALIDAD ?? false);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.TiposOperacionProduccion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.TiposOperacionProduccion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.TiposOperacionProduccion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_OPERACION_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.TiposOperacionProduccion.Delete, new { COD_TIPO_OPERACION_PRODUCCION = COD_TIPO_OPERACION_PRODUCCION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.TiposOperacionProduccion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.TiposOperacionProduccion.Delete, new { COD_TIPO_OPERACION_PRODUCCION = COD_TIPO_OPERACION_PRODUCCION })
        };
    }
}

}