using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposRutaProduccionRepository : ITiposRutaProduccionRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposRutaProduccionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposRutaProduccion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposRutaProduccion>(SP.SCHEMAIngenieria.TiposRutaProduccion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposRutaProduccion?> ShowbyidAsync(string COD_TIPO_RUTA_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposRutaProduccion>(SP.SCHEMAIngenieria.TiposRutaProduccion.Showbyid, new { COD_TIPO_RUTA_PRODUCCION = COD_TIPO_RUTA_PRODUCCION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.TiposRutaProduccion.Showcombox);
    }

    public async Task<IEnumerable<TiposRutaProduccion>> SearchAsync(TiposRutaProduccionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposRutaProduccion>(SP.SCHEMAIngenieria.TiposRutaProduccion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposRutaProduccionInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_RUTA_PRODUCCION", dto.COD_TIPO_RUTA_PRODUCCION);
        p.Add("DES_TIPO_RUTA_PRODUCCION", dto.DES_TIPO_RUTA_PRODUCCION);
        p.Add("FLG_DESARROLLO", dto.FLG_DESARROLLO ?? false);
        p.Add("FLG_COTIZACION", dto.FLG_COTIZACION ?? false);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION ?? false);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.TiposRutaProduccion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.TiposRutaProduccion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.TiposRutaProduccion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposRutaProduccionUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_RUTA_PRODUCCION", dto.COD_TIPO_RUTA_PRODUCCION);
        p.Add("DES_TIPO_RUTA_PRODUCCION", dto.DES_TIPO_RUTA_PRODUCCION);
        p.Add("FLG_DESARROLLO", dto.FLG_DESARROLLO ?? false);
        p.Add("FLG_COTIZACION", dto.FLG_COTIZACION ?? false);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION ?? false);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.TiposRutaProduccion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.TiposRutaProduccion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.TiposRutaProduccion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_RUTA_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.TiposRutaProduccion.Delete, new { COD_TIPO_RUTA_PRODUCCION = COD_TIPO_RUTA_PRODUCCION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.TiposRutaProduccion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.TiposRutaProduccion.Delete, new { COD_TIPO_RUTA_PRODUCCION = COD_TIPO_RUTA_PRODUCCION })
        };
    }
}

}