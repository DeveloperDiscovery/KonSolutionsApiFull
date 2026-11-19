using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposLineaProduccionRepository : ITiposLineaProduccionRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposLineaProduccionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposLineaProduccion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposLineaProduccion>(SP.SCHEMAProduccion.TiposLineaProduccion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposLineaProduccion?> ShowbyidAsync(string COD_TIPO_LINEA_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposLineaProduccion>(SP.SCHEMAProduccion.TiposLineaProduccion.Showbyid, new { COD_TIPO_LINEA_PRODUCCION = COD_TIPO_LINEA_PRODUCCION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProduccion.TiposLineaProduccion.Showcombox);
    }

    public async Task<IEnumerable<TiposLineaProduccion>> SearchAsync(TiposLineaProduccionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposLineaProduccion>(SP.SCHEMAProduccion.TiposLineaProduccion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposLineaProduccionInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_LINEA_PRODUCCION", dto.COD_TIPO_LINEA_PRODUCCION);
        p.Add("DES_TIPO_LINEA_PRODUCCION", dto.DES_TIPO_LINEA_PRODUCCION);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION ?? false);
        p.Add("FLG_PLANTA_PROPIA", dto.FLG_PLANTA_PROPIA ?? false);
        p.Add("IMG_PICTURE", dto.IMG_PICTURE, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.TiposLineaProduccion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.TiposLineaProduccion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.TiposLineaProduccion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposLineaProduccionUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_LINEA_PRODUCCION", dto.COD_TIPO_LINEA_PRODUCCION);
        p.Add("DES_TIPO_LINEA_PRODUCCION", dto.DES_TIPO_LINEA_PRODUCCION);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION ?? false);
        p.Add("FLG_PLANTA_PROPIA", dto.FLG_PLANTA_PROPIA ?? false);
        p.Add("IMG_PICTURE", dto.IMG_PICTURE, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.TiposLineaProduccion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.TiposLineaProduccion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.TiposLineaProduccion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_LINEA_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.TiposLineaProduccion.Delete, new { COD_TIPO_LINEA_PRODUCCION = COD_TIPO_LINEA_PRODUCCION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.TiposLineaProduccion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.TiposLineaProduccion.Delete, new { COD_TIPO_LINEA_PRODUCCION = COD_TIPO_LINEA_PRODUCCION })
        };
    }
}

}