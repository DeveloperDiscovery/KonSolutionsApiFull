using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CompetenciasRepository : ICompetenciasRepository
{
    private readonly IDbConnectionFactory _db;
    public CompetenciasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Competencias>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Competencias>(SP.SCHEMARrhh.Competencias.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Competencias?> ShowbyidAsync(string COD_COMPETENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Competencias>(SP.SCHEMARrhh.Competencias.Showbyid, new { COD_COMPETENCIA = COD_COMPETENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.Competencias.Showcombox);
    }

    public async Task<IEnumerable<Competencias>> SearchAsync(CompetenciasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Competencias>(SP.SCHEMARrhh.Competencias.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CompetenciasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_COMPETENCIA", dto.COD_COMPETENCIA);
        p.Add("DES_COMPETENCIA", dto.DES_COMPETENCIA);
        p.Add("TAS_PESO_EVALUACION", dto.TAS_PESO_EVALUACION);
        p.Add("COD_TIPO_COMPETENCIA", dto.COD_TIPO_COMPETENCIA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.Competencias.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.Competencias.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.Competencias.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CompetenciasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_COMPETENCIA", dto.COD_COMPETENCIA);
        p.Add("DES_COMPETENCIA", dto.DES_COMPETENCIA);
        p.Add("TAS_PESO_EVALUACION", dto.TAS_PESO_EVALUACION);
        p.Add("COD_TIPO_COMPETENCIA", dto.COD_TIPO_COMPETENCIA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.Competencias.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.Competencias.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.Competencias.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_COMPETENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.Competencias.Delete, new { COD_COMPETENCIA = COD_COMPETENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.Competencias.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.Competencias.Delete, new { COD_COMPETENCIA = COD_COMPETENCIA })
        };
    }
}

}