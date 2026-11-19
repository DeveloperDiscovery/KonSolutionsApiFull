using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PersonalAsistenciaRepository : IPersonalAsistenciaRepository
{
    private readonly IDbConnectionFactory _db;
    public PersonalAsistenciaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PersonalAsistencia>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalAsistencia>(SP.SCHEMARrhh.PersonalAsistencia.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PersonalAsistencia?> ShowbyidAsync(int NUM_PLANILLA_REMUNERATIVA, decimal COD_ENTIDAD_PERSONAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PersonalAsistencia>(SP.SCHEMARrhh.PersonalAsistencia.Showbyid, new { NUM_PLANILLA_REMUNERATIVA = NUM_PLANILLA_REMUNERATIVA, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.PersonalAsistencia.Showcombox);
    }

    public async Task<IEnumerable<PersonalAsistencia>> SearchAsync(PersonalAsistenciaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalAsistencia>(SP.SCHEMARrhh.PersonalAsistencia.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PersonalAsistenciaInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PLANILLA_REMUNERATIVA", dto.NUM_PLANILLA_REMUNERATIVA);
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_CONCEPTO_REMUNERATIVO", dto.COD_CONCEPTO_REMUNERATIVO);
        p.Add("COD_UNIDAD_MEDIDA_TIEMPO", dto.COD_UNIDAD_MEDIDA_TIEMPO);
        p.Add("CAN_TIEMPO", dto.CAN_TIEMPO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalAsistencia.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalAsistencia.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalAsistencia.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PersonalAsistenciaUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PLANILLA_REMUNERATIVA", dto.NUM_PLANILLA_REMUNERATIVA);
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_CONCEPTO_REMUNERATIVO", dto.COD_CONCEPTO_REMUNERATIVO);
        p.Add("COD_UNIDAD_MEDIDA_TIEMPO", dto.COD_UNIDAD_MEDIDA_TIEMPO);
        p.Add("CAN_TIEMPO", dto.CAN_TIEMPO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalAsistencia.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalAsistencia.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalAsistencia.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PLANILLA_REMUNERATIVA, decimal COD_ENTIDAD_PERSONAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalAsistencia.Delete, new { NUM_PLANILLA_REMUNERATIVA = NUM_PLANILLA_REMUNERATIVA, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalAsistencia.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalAsistencia.Delete, new { NUM_PLANILLA_REMUNERATIVA = NUM_PLANILLA_REMUNERATIVA, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL })
        };
    }
}

}