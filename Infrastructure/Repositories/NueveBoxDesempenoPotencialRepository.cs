using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class NueveBoxDesempenoPotencialRepository : INueveBoxDesempenoPotencialRepository
{
    private readonly IDbConnectionFactory _db;
    public NueveBoxDesempenoPotencialRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<NueveBoxDesempenoPotencial>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<NueveBoxDesempenoPotencial>(SP.SCHEMARrhh.NueveBoxDesempenoPotencial.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<NueveBoxDesempenoPotencial?> ShowbyidAsync(string COD_DESEMPENO_9BOX, string COD_POTENCIAL_9BOX)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<NueveBoxDesempenoPotencial>(SP.SCHEMARrhh.NueveBoxDesempenoPotencial.Showbyid, new { COD_DESEMPENO_9BOX = COD_DESEMPENO_9BOX, COD_POTENCIAL_9BOX = COD_POTENCIAL_9BOX }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.NueveBoxDesempenoPotencial.Showcombox);
    }

    public async Task<IEnumerable<NueveBoxDesempenoPotencial>> SearchAsync(NueveBoxDesempenoPotencialSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<NueveBoxDesempenoPotencial>(SP.SCHEMARrhh.NueveBoxDesempenoPotencial.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(NueveBoxDesempenoPotencialInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_DESEMPENO_9BOX", dto.COD_DESEMPENO_9BOX);
        p.Add("COD_POTENCIAL_9BOX", dto.COD_POTENCIAL_9BOX);
        p.Add("DES_EVALUACION", dto.DES_EVALUACION);
        p.Add("NUM_EVALUCION", dto.NUM_EVALUCION);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.NueveBoxDesempenoPotencial.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.NueveBoxDesempenoPotencial.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.NueveBoxDesempenoPotencial.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(NueveBoxDesempenoPotencialUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_DESEMPENO_9BOX", dto.COD_DESEMPENO_9BOX);
        p.Add("COD_POTENCIAL_9BOX", dto.COD_POTENCIAL_9BOX);
        p.Add("DES_EVALUACION", dto.DES_EVALUACION);
        p.Add("NUM_EVALUCION", dto.NUM_EVALUCION);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.NueveBoxDesempenoPotencial.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.NueveBoxDesempenoPotencial.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.NueveBoxDesempenoPotencial.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_DESEMPENO_9BOX, string COD_POTENCIAL_9BOX)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.NueveBoxDesempenoPotencial.Delete, new { COD_DESEMPENO_9BOX = COD_DESEMPENO_9BOX, COD_POTENCIAL_9BOX = COD_POTENCIAL_9BOX }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.NueveBoxDesempenoPotencial.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.NueveBoxDesempenoPotencial.Delete, new { COD_DESEMPENO_9BOX = COD_DESEMPENO_9BOX, COD_POTENCIAL_9BOX = COD_POTENCIAL_9BOX })
        };
    }
}

}