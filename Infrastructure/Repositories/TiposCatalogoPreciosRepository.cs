using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposCatalogoPreciosRepository : ITiposCatalogoPreciosRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposCatalogoPreciosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposCatalogoPrecios>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposCatalogoPrecios>(SP.SCHEMAComercial.TiposCatalogoPrecios.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposCatalogoPrecios?> ShowbyidAsync(string COD_TIPO_CATALOGO_PRECIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposCatalogoPrecios>(SP.SCHEMAComercial.TiposCatalogoPrecios.Showbyid, new { COD_TIPO_CATALOGO_PRECIO = COD_TIPO_CATALOGO_PRECIO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.TiposCatalogoPrecios.Showcombox);
    }

    public async Task<IEnumerable<TiposCatalogoPrecios>> SearchAsync(TiposCatalogoPreciosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposCatalogoPrecios>(SP.SCHEMAComercial.TiposCatalogoPrecios.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposCatalogoPreciosInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CATALOGO_PRECIO", dto.COD_TIPO_CATALOGO_PRECIO);
        p.Add("DES_TIPO_CATALOGO_PRECIO", dto.DES_TIPO_CATALOGO_PRECIO);
        p.Add("FLG_CATALOGO_MATRIZ", dto.FLG_CATALOGO_MATRIZ ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposCatalogoPrecios.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposCatalogoPrecios.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposCatalogoPrecios.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposCatalogoPreciosUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CATALOGO_PRECIO", dto.COD_TIPO_CATALOGO_PRECIO);
        p.Add("DES_TIPO_CATALOGO_PRECIO", dto.DES_TIPO_CATALOGO_PRECIO);
        p.Add("FLG_CATALOGO_MATRIZ", dto.FLG_CATALOGO_MATRIZ ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposCatalogoPrecios.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposCatalogoPrecios.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposCatalogoPrecios.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_CATALOGO_PRECIO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposCatalogoPrecios.Delete, new { COD_TIPO_CATALOGO_PRECIO = COD_TIPO_CATALOGO_PRECIO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposCatalogoPrecios.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposCatalogoPrecios.Delete, new { COD_TIPO_CATALOGO_PRECIO = COD_TIPO_CATALOGO_PRECIO })
        };
    }
}

}