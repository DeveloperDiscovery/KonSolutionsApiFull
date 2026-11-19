using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class IncotermsFasesRepository : IIncotermsFasesRepository
{
    private readonly IDbConnectionFactory _db;
    public IncotermsFasesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<IncotermsFases>> ShowallAsync(string codIncoterm)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<IncotermsFases>(SP.SCHEMAComercial.IncotermsFases.Showall, new { COD_INCOTERM = codIncoterm }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IncotermsFases?> ShowbyidAsync(string COD_INCOTERM, string COD_FASE_INCOTERM)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<IncotermsFases>(SP.SCHEMAComercial.IncotermsFases.Showbyid, new { COD_INCOTERM = COD_INCOTERM, COD_FASE_INCOTERM = COD_FASE_INCOTERM }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.IncotermsFases.Showcombox);
    }

    public async Task<IEnumerable<IncotermsFases>> SearchAsync(IncotermsFasesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<IncotermsFases>(SP.SCHEMAComercial.IncotermsFases.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(IncotermsFasesInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_INCOTERM", dto.COD_INCOTERM);
        p.Add("COD_FASE_INCOTERM", dto.COD_FASE_INCOTERM);
        p.Add("FLG_VENDEDOR", dto.FLG_VENDEDOR ?? false);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.IncotermsFases.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.IncotermsFases.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.IncotermsFases.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(IncotermsFasesUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_INCOTERM", dto.COD_INCOTERM);
        p.Add("COD_FASE_INCOTERM", dto.COD_FASE_INCOTERM);
        p.Add("FLG_VENDEDOR", dto.FLG_VENDEDOR ?? false);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.IncotermsFases.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.IncotermsFases.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.IncotermsFases.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_INCOTERM, string COD_FASE_INCOTERM)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.IncotermsFases.Delete, new { COD_INCOTERM = COD_INCOTERM, COD_FASE_INCOTERM = COD_FASE_INCOTERM }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.IncotermsFases.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.IncotermsFases.Delete, new { COD_INCOTERM = COD_INCOTERM, COD_FASE_INCOTERM = COD_FASE_INCOTERM })
        };
    }
}

}