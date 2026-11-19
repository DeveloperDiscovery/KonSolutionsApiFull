using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PersonalFormacionAcademicaRepository : IPersonalFormacionAcademicaRepository
{
    private readonly IDbConnectionFactory _db;
    public PersonalFormacionAcademicaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PersonalFormacionAcademica>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalFormacionAcademica>(SP.SCHEMARrhh.PersonalFormacionAcademica.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PersonalFormacionAcademica?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_ENTIDAD_ACADEMICA, string COD_CARRERA_ACADEMICA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PersonalFormacionAcademica>(SP.SCHEMARrhh.PersonalFormacionAcademica.Showbyid, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_ENTIDAD_ACADEMICA = COD_ENTIDAD_ACADEMICA, COD_CARRERA_ACADEMICA = COD_CARRERA_ACADEMICA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.PersonalFormacionAcademica.Showcombox);
    }

    public async Task<IEnumerable<PersonalFormacionAcademica>> SearchAsync(PersonalFormacionAcademicaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalFormacionAcademica>(SP.SCHEMARrhh.PersonalFormacionAcademica.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PersonalFormacionAcademicaInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_ENTIDAD_ACADEMICA", dto.COD_ENTIDAD_ACADEMICA);
        p.Add("COD_CARRERA_ACADEMICA", dto.COD_CARRERA_ACADEMICA);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_TERMINO", dto.FEC_TERMINO);
        p.Add("COD_TITULO_ACADEMICO", dto.COD_TITULO_ACADEMICO);
        p.Add("COD_RANKING_ACADEMICO", dto.COD_RANKING_ACADEMICO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalFormacionAcademica.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalFormacionAcademica.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalFormacionAcademica.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PersonalFormacionAcademicaUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_ENTIDAD_ACADEMICA", dto.COD_ENTIDAD_ACADEMICA);
        p.Add("COD_CARRERA_ACADEMICA", dto.COD_CARRERA_ACADEMICA);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_TERMINO", dto.FEC_TERMINO);
        p.Add("COD_TITULO_ACADEMICO", dto.COD_TITULO_ACADEMICO);
        p.Add("COD_RANKING_ACADEMICO", dto.COD_RANKING_ACADEMICO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalFormacionAcademica.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalFormacionAcademica.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalFormacionAcademica.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_ENTIDAD_ACADEMICA, string COD_CARRERA_ACADEMICA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalFormacionAcademica.Delete, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_ENTIDAD_ACADEMICA = COD_ENTIDAD_ACADEMICA, COD_CARRERA_ACADEMICA = COD_CARRERA_ACADEMICA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalFormacionAcademica.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalFormacionAcademica.Delete, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_ENTIDAD_ACADEMICA = COD_ENTIDAD_ACADEMICA, COD_CARRERA_ACADEMICA = COD_CARRERA_ACADEMICA })
        };
    }
}

}