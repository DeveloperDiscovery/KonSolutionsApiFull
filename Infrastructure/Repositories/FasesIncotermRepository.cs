using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class FasesIncotermRepository : IFasesIncotermRepository
{
    private readonly IDbConnectionFactory _db;
    public FasesIncotermRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<FasesIncoterm>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<FasesIncoterm>(SP.SCHEMAComercial.FasesIncoterm.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<FasesIncoterm?> ShowbyidAsync(string COD_FASE_INCOTERM)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FasesIncoterm>(SP.SCHEMAComercial.FasesIncoterm.Showbyid, new { COD_FASE_INCOTERM = COD_FASE_INCOTERM }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.FasesIncoterm.Showcombox);
    }

    public async Task<IEnumerable<FasesIncoterm>> SearchAsync(FasesIncotermSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<FasesIncoterm>(SP.SCHEMAComercial.FasesIncoterm.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(FasesIncotermInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_FASE_INCOTERM", dto.COD_FASE_INCOTERM);
        p.Add("DES_FASE_INCOTERM", dto.DES_FASE_INCOTERM);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.FasesIncoterm.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.FasesIncoterm.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.FasesIncoterm.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(FasesIncotermUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_FASE_INCOTERM", dto.COD_FASE_INCOTERM);
        p.Add("DES_FASE_INCOTERM", dto.DES_FASE_INCOTERM);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.FasesIncoterm.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.FasesIncoterm.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.FasesIncoterm.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_FASE_INCOTERM)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.FasesIncoterm.Delete, new { COD_FASE_INCOTERM = COD_FASE_INCOTERM }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.FasesIncoterm.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.FasesIncoterm.Delete, new { COD_FASE_INCOTERM = COD_FASE_INCOTERM })
        };
    }
}

}