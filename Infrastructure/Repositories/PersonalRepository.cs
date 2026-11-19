using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PersonalRepository : IPersonalRepository
{
    private readonly IDbConnectionFactory _db;
    public PersonalRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Personal>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Personal>(SP.SCHEMARrhh.Personal.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Personal?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Personal>(SP.SCHEMARrhh.Personal.Showbyid, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.Personal.Showcombox);
    }

    public async Task<IEnumerable<Personal>> SearchAsync(PersonalSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Personal>(SP.SCHEMARrhh.Personal.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PersonalInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_PERSONAL", dto.COD_PERSONAL);
        p.Add("FEC_NACIMIENTO", dto.FEC_NACIMIENTO);
        p.Add("COD_TIPO_PERSONAL", dto.COD_TIPO_PERSONAL);
        p.Add("COD_SEXO", dto.COD_SEXO);
        p.Add("COD_AREA", dto.COD_AREA);
        p.Add("COD_CARGO", dto.COD_CARGO);
        p.Add("COD_ESTADO_CIVIL", dto.COD_ESTADO_CIVIL);
        p.Add("FEC_INGRESO", dto.FEC_INGRESO);
        p.Add("FEC_CESE", dto.FEC_CESE);
        p.Add("COD_CENTRO_COSTO", dto.COD_CENTRO_COSTO);
        p.Add("COD_AFP", dto.COD_AFP);
        p.Add("COD_GRADO_CONFIDENCIABILIDAD", dto.COD_GRADO_CONFIDENCIABILIDAD);
        p.Add("COD_CATEGORIA_OPERACION_PRODUCCION", dto.COD_CATEGORIA_OPERACION_PRODUCCION);
        p.Add("COD_MONEDA_REMUNERACION", dto.COD_MONEDA_REMUNERACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.Personal.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.Personal.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.Personal.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PersonalUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_PERSONAL", dto.COD_PERSONAL);
        p.Add("FEC_NACIMIENTO", dto.FEC_NACIMIENTO);
        p.Add("COD_TIPO_PERSONAL", dto.COD_TIPO_PERSONAL);
        p.Add("COD_SEXO", dto.COD_SEXO);
        p.Add("COD_AREA", dto.COD_AREA);
        p.Add("COD_CARGO", dto.COD_CARGO);
        p.Add("COD_ESTADO_CIVIL", dto.COD_ESTADO_CIVIL);
        p.Add("FEC_INGRESO", dto.FEC_INGRESO);
        p.Add("FEC_CESE", dto.FEC_CESE);
        p.Add("COD_CENTRO_COSTO", dto.COD_CENTRO_COSTO);
        p.Add("COD_AFP", dto.COD_AFP);
        p.Add("COD_GRADO_CONFIDENCIABILIDAD", dto.COD_GRADO_CONFIDENCIABILIDAD);
        p.Add("COD_CATEGORIA_OPERACION_PRODUCCION", dto.COD_CATEGORIA_OPERACION_PRODUCCION);
        p.Add("COD_MONEDA_REMUNERACION", dto.COD_MONEDA_REMUNERACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.Personal.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.Personal.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.Personal.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.Personal.Delete, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.Personal.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.Personal.Delete, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL })
        };
    }
}

}