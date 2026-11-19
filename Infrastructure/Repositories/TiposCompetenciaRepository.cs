using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposCompetenciaRepository : ITiposCompetenciaRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposCompetenciaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposCompetencia>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposCompetencia>(SP.SCHEMARrhh.TiposCompetencia.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposCompetencia?> ShowbyidAsync(string COD_TIPO_COMPETENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposCompetencia>(SP.SCHEMARrhh.TiposCompetencia.Showbyid, new { COD_TIPO_COMPETENCIA = COD_TIPO_COMPETENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.TiposCompetencia.Showcombox);
    }

    public async Task<IEnumerable<TiposCompetencia>> SearchAsync(TiposCompetenciaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposCompetencia>(SP.SCHEMARrhh.TiposCompetencia.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposCompetenciaInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_COMPETENCIA", dto.COD_TIPO_COMPETENCIA);
        p.Add("DES_TIPO_COMPETENCIA", dto.DES_TIPO_COMPETENCIA);
        p.Add("NUM_NIVEL", dto.NUM_NIVEL);
        p.Add("NUM_NIVEL_MINIMO", dto.NUM_NIVEL_MINIMO);
        p.Add("NUM_NIVEL_MAXIMO", dto.NUM_NIVEL_MAXIMO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposCompetencia.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposCompetencia.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposCompetencia.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposCompetenciaUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_COMPETENCIA", dto.COD_TIPO_COMPETENCIA);
        p.Add("DES_TIPO_COMPETENCIA", dto.DES_TIPO_COMPETENCIA);
        p.Add("NUM_NIVEL", dto.NUM_NIVEL);
        p.Add("NUM_NIVEL_MINIMO", dto.NUM_NIVEL_MINIMO);
        p.Add("NUM_NIVEL_MAXIMO", dto.NUM_NIVEL_MAXIMO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposCompetencia.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposCompetencia.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposCompetencia.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_COMPETENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposCompetencia.Delete, new { COD_TIPO_COMPETENCIA = COD_TIPO_COMPETENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposCompetencia.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposCompetencia.Delete, new { COD_TIPO_COMPETENCIA = COD_TIPO_COMPETENCIA })
        };
    }
}

}