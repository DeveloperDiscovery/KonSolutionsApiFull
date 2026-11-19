using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EncuestasAreasRepository : IEncuestasAreasRepository
{
    private readonly IDbConnectionFactory _db;
    public EncuestasAreasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EncuestasAreas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EncuestasAreas>(SP.SCHEMARrhh.EncuestasAreas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EncuestasAreas?> ShowbyidAsync(int NUM_ENCUESTA, int COD_AREA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EncuestasAreas>(SP.SCHEMARrhh.EncuestasAreas.Showbyid, new { NUM_ENCUESTA = NUM_ENCUESTA, COD_AREA = COD_AREA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.EncuestasAreas.Showcombox);
    }

    public async Task<IEnumerable<EncuestasAreas>> SearchAsync(EncuestasAreasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EncuestasAreas>(SP.SCHEMARrhh.EncuestasAreas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EncuestasAreasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_ENCUESTA", dto.NUM_ENCUESTA);
        p.Add("COD_AREA", dto.COD_AREA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EncuestasAreas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EncuestasAreas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EncuestasAreas.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_ENCUESTA, int COD_AREA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EncuestasAreas.Delete, new { NUM_ENCUESTA = NUM_ENCUESTA, COD_AREA = COD_AREA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EncuestasAreas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EncuestasAreas.Delete, new { NUM_ENCUESTA = NUM_ENCUESTA, COD_AREA = COD_AREA })
        };
    }
}

}