using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposCarreraAcademicaRepository : ITiposCarreraAcademicaRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposCarreraAcademicaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposCarreraAcademica>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposCarreraAcademica>(SP.SCHEMARrhh.TiposCarreraAcademica.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposCarreraAcademica?> ShowbyidAsync(string COD_TIPO_CARRERA_ACADEMICA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposCarreraAcademica>(SP.SCHEMARrhh.TiposCarreraAcademica.Showbyid, new { COD_TIPO_CARRERA_ACADEMICA = COD_TIPO_CARRERA_ACADEMICA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.TiposCarreraAcademica.Showcombox);
    }

    public async Task<IEnumerable<TiposCarreraAcademica>> SearchAsync(TiposCarreraAcademicaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposCarreraAcademica>(SP.SCHEMARrhh.TiposCarreraAcademica.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposCarreraAcademicaInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CARRERA_ACADEMICA", dto.COD_TIPO_CARRERA_ACADEMICA);
        p.Add("DES_TIPO_CARRERA_ACADEMICA", dto.DES_TIPO_CARRERA_ACADEMICA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposCarreraAcademica.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposCarreraAcademica.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposCarreraAcademica.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposCarreraAcademicaUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CARRERA_ACADEMICA", dto.COD_TIPO_CARRERA_ACADEMICA);
        p.Add("DES_TIPO_CARRERA_ACADEMICA", dto.DES_TIPO_CARRERA_ACADEMICA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposCarreraAcademica.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposCarreraAcademica.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposCarreraAcademica.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_CARRERA_ACADEMICA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposCarreraAcademica.Delete, new { COD_TIPO_CARRERA_ACADEMICA = COD_TIPO_CARRERA_ACADEMICA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposCarreraAcademica.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposCarreraAcademica.Delete, new { COD_TIPO_CARRERA_ACADEMICA = COD_TIPO_CARRERA_ACADEMICA })
        };
    }
}

}