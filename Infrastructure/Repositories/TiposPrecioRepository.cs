using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposPrecioRepository : ITiposPrecioRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposPrecioRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposPrecio>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposPrecio>(SP.SCHEMAComercial.TiposPrecio.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposPrecio?> ShowbyidAsync(int COD_TIPO_PRECIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposPrecio>(SP.SCHEMAComercial.TiposPrecio.Showbyid, new { COD_TIPO_PRECIO = COD_TIPO_PRECIO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.TiposPrecio.Showcombox);
    }

    public async Task<IEnumerable<TiposPrecio>> SearchAsync(TiposPrecioSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposPrecio>(SP.SCHEMAComercial.TiposPrecio.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposPrecioInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PRECIO", dto.COD_TIPO_PRECIO);
        p.Add("DES_TIPO_PRECIO", dto.DES_TIPO_PRECIO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposPrecio.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposPrecio.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposPrecio.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposPrecioUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PRECIO", dto.COD_TIPO_PRECIO);
        p.Add("DES_TIPO_PRECIO", dto.DES_TIPO_PRECIO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposPrecio.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposPrecio.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposPrecio.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_TIPO_PRECIO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposPrecio.Delete, new { COD_TIPO_PRECIO = COD_TIPO_PRECIO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposPrecio.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposPrecio.Delete, new { COD_TIPO_PRECIO = COD_TIPO_PRECIO })
        };
    }
}

}