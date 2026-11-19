using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposImpuestoRepository : ITiposImpuestoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposImpuestoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposImpuesto>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposImpuesto>(SP.SCHEMAComercial.TiposImpuesto.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposImpuesto?> ShowbyidAsync(string COD_TIPO_IMPUESTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposImpuesto>(SP.SCHEMAComercial.TiposImpuesto.Showbyid, new { COD_TIPO_IMPUESTO = COD_TIPO_IMPUESTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.TiposImpuesto.Showcombox);
    }

    public async Task<IEnumerable<TiposImpuesto>> SearchAsync(TiposImpuestoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposImpuesto>(SP.SCHEMAComercial.TiposImpuesto.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposImpuestoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_IMPUESTO", dto.COD_TIPO_IMPUESTO);
        p.Add("DES_TIPO_IMPUESTO", dto.DES_TIPO_IMPUESTO);
        p.Add("COD_TIPO_IMPUESTO_SUNAT", dto.COD_TIPO_IMPUESTO_SUNAT);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposImpuesto.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposImpuesto.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposImpuesto.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposImpuestoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_IMPUESTO", dto.COD_TIPO_IMPUESTO);
        p.Add("DES_TIPO_IMPUESTO", dto.DES_TIPO_IMPUESTO);
        p.Add("COD_TIPO_IMPUESTO_SUNAT", dto.COD_TIPO_IMPUESTO_SUNAT);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposImpuesto.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposImpuesto.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposImpuesto.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_IMPUESTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposImpuesto.Delete, new { COD_TIPO_IMPUESTO = COD_TIPO_IMPUESTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposImpuesto.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposImpuesto.Delete, new { COD_TIPO_IMPUESTO = COD_TIPO_IMPUESTO })
        };
    }
}

}