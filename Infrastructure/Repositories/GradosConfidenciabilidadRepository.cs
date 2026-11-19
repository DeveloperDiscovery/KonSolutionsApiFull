using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class GradosConfidenciabilidadRepository : IGradosConfidenciabilidadRepository
{
    private readonly IDbConnectionFactory _db;
    public GradosConfidenciabilidadRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<GradosConfidenciabilidad>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GradosConfidenciabilidad>(SP.SCHEMARrhh.GradosConfidenciabilidad.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<GradosConfidenciabilidad?> ShowbyidAsync(int COD_GRADO_CONFIDENCIABILIDAD)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<GradosConfidenciabilidad>(SP.SCHEMARrhh.GradosConfidenciabilidad.Showbyid, new { COD_GRADO_CONFIDENCIABILIDAD = COD_GRADO_CONFIDENCIABILIDAD }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.GradosConfidenciabilidad.Showcombox);
    }

    public async Task<IEnumerable<GradosConfidenciabilidad>> SearchAsync(GradosConfidenciabilidadSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GradosConfidenciabilidad>(SP.SCHEMARrhh.GradosConfidenciabilidad.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(GradosConfidenciabilidadInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_GRADO_CONFIDENCIABILIDAD", dto.COD_GRADO_CONFIDENCIABILIDAD);
        p.Add("DES_GRADO_CONFIDENCIABILIDAD", dto.DES_GRADO_CONFIDENCIABILIDAD);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.GradosConfidenciabilidad.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.GradosConfidenciabilidad.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.GradosConfidenciabilidad.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(GradosConfidenciabilidadUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_GRADO_CONFIDENCIABILIDAD", dto.COD_GRADO_CONFIDENCIABILIDAD);
        p.Add("DES_GRADO_CONFIDENCIABILIDAD", dto.DES_GRADO_CONFIDENCIABILIDAD);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.GradosConfidenciabilidad.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.GradosConfidenciabilidad.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.GradosConfidenciabilidad.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_GRADO_CONFIDENCIABILIDAD)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.GradosConfidenciabilidad.Delete, new { COD_GRADO_CONFIDENCIABILIDAD = COD_GRADO_CONFIDENCIABILIDAD }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.GradosConfidenciabilidad.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.GradosConfidenciabilidad.Delete, new { COD_GRADO_CONFIDENCIABILIDAD = COD_GRADO_CONFIDENCIABILIDAD })
        };
    }
}

}