using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TitulosAcademicosRepository : ITitulosAcademicosRepository
{
    private readonly IDbConnectionFactory _db;
    public TitulosAcademicosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TitulosAcademicos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TitulosAcademicos>(SP.SCHEMARrhh.TitulosAcademicos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TitulosAcademicos?> ShowbyidAsync(string COD_TITULO_ACADEMICO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TitulosAcademicos>(SP.SCHEMARrhh.TitulosAcademicos.Showbyid, new { COD_TITULO_ACADEMICO = COD_TITULO_ACADEMICO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.TitulosAcademicos.Showcombox);
    }

    public async Task<IEnumerable<TitulosAcademicos>> SearchAsync(TitulosAcademicosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TitulosAcademicos>(SP.SCHEMARrhh.TitulosAcademicos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TitulosAcademicosInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TITULO_ACADEMICO", dto.COD_TITULO_ACADEMICO);
        p.Add("DES_TITULO_ACADEMICO", dto.DES_TITULO_ACADEMICO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TitulosAcademicos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TitulosAcademicos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TitulosAcademicos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TitulosAcademicosUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TITULO_ACADEMICO", dto.COD_TITULO_ACADEMICO);
        p.Add("DES_TITULO_ACADEMICO", dto.DES_TITULO_ACADEMICO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TitulosAcademicos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TitulosAcademicos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TitulosAcademicos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TITULO_ACADEMICO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TitulosAcademicos.Delete, new { COD_TITULO_ACADEMICO = COD_TITULO_ACADEMICO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TitulosAcademicos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TitulosAcademicos.Delete, new { COD_TITULO_ACADEMICO = COD_TITULO_ACADEMICO })
        };
    }
}

}