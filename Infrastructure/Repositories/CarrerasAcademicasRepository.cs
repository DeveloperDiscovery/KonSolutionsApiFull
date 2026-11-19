using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CarrerasAcademicasRepository : ICarrerasAcademicasRepository
{
    private readonly IDbConnectionFactory _db;
    public CarrerasAcademicasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CarrerasAcademicas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CarrerasAcademicas>(SP.SCHEMARrhh.CarrerasAcademicas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CarrerasAcademicas?> ShowbyidAsync(string COD_CARRERA_ACADEMICA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CarrerasAcademicas>(SP.SCHEMARrhh.CarrerasAcademicas.Showbyid, new { COD_CARRERA_ACADEMICA = COD_CARRERA_ACADEMICA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.CarrerasAcademicas.Showcombox);
    }

    public async Task<IEnumerable<CarrerasAcademicas>> SearchAsync(CarrerasAcademicasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CarrerasAcademicas>(SP.SCHEMARrhh.CarrerasAcademicas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CarrerasAcademicasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CARRERA_ACADEMICA", dto.COD_CARRERA_ACADEMICA);
        p.Add("DES_CARRERA_ACADEMICA", dto.DES_CARRERA_ACADEMICA);
        p.Add("COD_TIPO_CARRERA_ACADEMICA", dto.COD_TIPO_CARRERA_ACADEMICA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.CarrerasAcademicas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.CarrerasAcademicas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.CarrerasAcademicas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CarrerasAcademicasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CARRERA_ACADEMICA", dto.COD_CARRERA_ACADEMICA);
        p.Add("DES_CARRERA_ACADEMICA", dto.DES_CARRERA_ACADEMICA);
        p.Add("COD_TIPO_CARRERA_ACADEMICA", dto.COD_TIPO_CARRERA_ACADEMICA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.CarrerasAcademicas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.CarrerasAcademicas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.CarrerasAcademicas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CARRERA_ACADEMICA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.CarrerasAcademicas.Delete, new { COD_CARRERA_ACADEMICA = COD_CARRERA_ACADEMICA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.CarrerasAcademicas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.CarrerasAcademicas.Delete, new { COD_CARRERA_ACADEMICA = COD_CARRERA_ACADEMICA })
        };
    }
}

}