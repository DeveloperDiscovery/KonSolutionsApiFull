using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class IncotermsRepository : IIncotermsRepository
{
    private readonly IDbConnectionFactory _db;
    public IncotermsRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Incoterms>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Incoterms>(SP.SCHEMAComercial.Incoterms.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Incoterms?> ShowbyidAsync(string COD_INCOTERM)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Incoterms>(SP.SCHEMAComercial.Incoterms.Showbyid, new { COD_INCOTERM = COD_INCOTERM }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.Incoterms.Showcombox);
    }

    public async Task<IEnumerable<Incoterms>> SearchAsync(IncotermsSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Incoterms>(SP.SCHEMAComercial.Incoterms.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(IncotermsInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_INCOTERM", dto.COD_INCOTERM);
        p.Add("DES_INCOTERM", dto.DES_INCOTERM);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.Incoterms.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.Incoterms.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.Incoterms.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(IncotermsUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_INCOTERM", dto.COD_INCOTERM);
        p.Add("DES_INCOTERM", dto.DES_INCOTERM);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.Incoterms.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.Incoterms.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.Incoterms.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_INCOTERM)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.Incoterms.Delete, new { COD_INCOTERM = COD_INCOTERM }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.Incoterms.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.Incoterms.Delete, new { COD_INCOTERM = COD_INCOTERM })
        };
    }
}

}