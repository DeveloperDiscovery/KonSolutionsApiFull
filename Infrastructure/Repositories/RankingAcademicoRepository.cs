using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class RankingAcademicoRepository : IRankingAcademicoRepository
{
    private readonly IDbConnectionFactory _db;
    public RankingAcademicoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<RankingAcademico>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RankingAcademico>(SP.SCHEMARrhh.RankingAcademico.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<RankingAcademico?> ShowbyidAsync(string COD_RANKING_ACADEMICO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RankingAcademico>(SP.SCHEMARrhh.RankingAcademico.Showbyid, new { COD_RANKING_ACADEMICO = COD_RANKING_ACADEMICO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.RankingAcademico.Showcombox);
    }

    public async Task<IEnumerable<RankingAcademico>> SearchAsync(RankingAcademicoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RankingAcademico>(SP.SCHEMARrhh.RankingAcademico.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(RankingAcademicoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_RANKING_ACADEMICO", dto.COD_RANKING_ACADEMICO);
        p.Add("DES_RANKING_ACADEMICO", dto.DES_RANKING_ACADEMICO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.RankingAcademico.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.RankingAcademico.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.RankingAcademico.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(RankingAcademicoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_RANKING_ACADEMICO", dto.COD_RANKING_ACADEMICO);
        p.Add("DES_RANKING_ACADEMICO", dto.DES_RANKING_ACADEMICO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.RankingAcademico.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.RankingAcademico.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.RankingAcademico.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_RANKING_ACADEMICO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.RankingAcademico.Delete, new { COD_RANKING_ACADEMICO = COD_RANKING_ACADEMICO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.RankingAcademico.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.RankingAcademico.Delete, new { COD_RANKING_ACADEMICO = COD_RANKING_ACADEMICO })
        };
    }
}

}