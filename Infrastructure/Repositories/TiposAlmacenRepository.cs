using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposAlmacenRepository : ITiposAlmacenRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposAlmacenRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposAlmacen>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposAlmacen>(SP.SCHEMALogistica.TiposAlmacen.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposAlmacen?> ShowbyidAsync(string COD_TIPO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposAlmacen>(SP.SCHEMALogistica.TiposAlmacen.Showbyid, new { COD_TIPO_ALMACEN = COD_TIPO_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    // El SP _SHOWCOMBOX devuelve las columnas de la tabla (COD_/DES_TIPO_ALMACEN...), no
    // las columnas "Id"/"Texto" que espera ComboboxItem — mapear directo a ComboboxItem
    // daba una lista de items vacíos. Se lee como TiposAlmacen y se proyecta acá.
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.TiposAlmacen.Showcombox);
    }

    public async Task<IEnumerable<TiposAlmacen>> SearchAsync(TiposAlmacenSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposAlmacen>(SP.SCHEMALogistica.TiposAlmacen.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposAlmacenInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_ALMACEN", dto.COD_TIPO_ALMACEN);
        p.Add("DES_TIPO_ALMACEN", dto.DES_TIPO_ALMACEN);
        p.Add("FLG_REGULAR", dto.FLG_REGULAR ?? false);
        p.Add("FLG_TRANSITO", dto.FLG_TRANSITO ?? false);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION ?? false);
        p.Add("FLG_PRODUCTO_TERMINADO", dto.FLG_PRODUCTO_TERMINADO ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposAlmacen.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposAlmacen.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposAlmacen.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo juego de parámetros que el alta: el SP _UPDATE se genera
    /// con el mismo contrato que el _INSERT justamente para que la API no tenga que armar dos
    /// mapeos distintos ni mantener dos DTO divergentes.</summary>
    public async Task<WriteResult> UpdateAsync(TiposAlmacenUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_ALMACEN", dto.COD_TIPO_ALMACEN);
        p.Add("DES_TIPO_ALMACEN", dto.DES_TIPO_ALMACEN);
        p.Add("FLG_REGULAR", dto.FLG_REGULAR ?? false);
        p.Add("FLG_TRANSITO", dto.FLG_TRANSITO ?? false);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION ?? false);
        p.Add("FLG_PRODUCTO_TERMINADO", dto.FLG_PRODUCTO_TERMINADO ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposAlmacen.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposAlmacen.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposAlmacen.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposAlmacen.Delete, new { COD_TIPO_ALMACEN = COD_TIPO_ALMACEN }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposAlmacen.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposAlmacen.Delete, new { COD_TIPO_ALMACEN = COD_TIPO_ALMACEN })
        };
    }
}

}