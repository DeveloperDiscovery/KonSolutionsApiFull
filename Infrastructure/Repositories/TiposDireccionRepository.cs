using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposDireccionRepository : ITiposDireccionRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposDireccionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposDireccion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposDireccion>(SP.SCHEMAComercial.TiposDireccion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposDireccion?> ShowbyidAsync(int COD_TIPO_DIRECCION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposDireccion>(SP.SCHEMAComercial.TiposDireccion.Showbyid, new { COD_TIPO_DIRECCION = COD_TIPO_DIRECCION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.TiposDireccion.Showcombox);
    }

    public async Task<IEnumerable<TiposDireccion>> SearchAsync(TiposDireccionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposDireccion>(SP.SCHEMAComercial.TiposDireccion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposDireccionInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_DIRECCION", dto.COD_TIPO_DIRECCION);
        p.Add("DES_TIPO_DIRECCION", dto.DES_TIPO_DIRECCION);
        p.Add("FLG_COBRANZA", dto.FLG_COBRANZA ?? false);
        p.Add("FLG_ENTREGA", dto.FLG_ENTREGA ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposDireccion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposDireccion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposDireccion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposDireccionUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_DIRECCION", dto.COD_TIPO_DIRECCION);
        p.Add("DES_TIPO_DIRECCION", dto.DES_TIPO_DIRECCION);
        p.Add("FLG_COBRANZA", dto.FLG_COBRANZA ?? false);
        p.Add("FLG_ENTREGA", dto.FLG_ENTREGA ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposDireccion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposDireccion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposDireccion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_TIPO_DIRECCION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposDireccion.Delete, new { COD_TIPO_DIRECCION = COD_TIPO_DIRECCION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposDireccion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposDireccion.Delete, new { COD_TIPO_DIRECCION = COD_TIPO_DIRECCION })
        };
    }
}

}