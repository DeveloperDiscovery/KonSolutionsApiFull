using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ReglasComboRepository : IReglasComboRepository
{
    private readonly IDbConnectionFactory _db;
    public ReglasComboRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ReglasCombo>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ReglasCombo>(SP.SCHEMAComercial.ReglasCombo.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ReglasCombo?> ShowbyidAsync(string COD_REGLA_COMBOS)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ReglasCombo>(SP.SCHEMAComercial.ReglasCombo.Showbyid, new { COD_REGLA_COMBOS = COD_REGLA_COMBOS }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.ReglasCombo.Showcombox);
    }

    public async Task<IEnumerable<ReglasCombo>> SearchAsync(ReglasComboSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ReglasCombo>(SP.SCHEMAComercial.ReglasCombo.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ReglasComboInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_REGLA_COMBOS", dto.COD_REGLA_COMBOS);
        p.Add("DES_REGLA_COMBOS", dto.DES_REGLA_COMBOS);
        p.Add("FLG_FIJO", dto.FLG_FIJO ?? false);
        p.Add("FLG_VERSUS", dto.FLG_VERSUS ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.ReglasCombo.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.ReglasCombo.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.ReglasCombo.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ReglasComboUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_REGLA_COMBOS", dto.COD_REGLA_COMBOS);
        p.Add("DES_REGLA_COMBOS", dto.DES_REGLA_COMBOS);
        p.Add("FLG_FIJO", dto.FLG_FIJO ?? false);
        p.Add("FLG_VERSUS", dto.FLG_VERSUS ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.ReglasCombo.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.ReglasCombo.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.ReglasCombo.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_REGLA_COMBOS)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.ReglasCombo.Delete, new { COD_REGLA_COMBOS = COD_REGLA_COMBOS }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.ReglasCombo.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.ReglasCombo.Delete, new { COD_REGLA_COMBOS = COD_REGLA_COMBOS })
        };
    }
}

}