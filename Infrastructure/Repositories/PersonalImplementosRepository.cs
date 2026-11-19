using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PersonalImplementosRepository : IPersonalImplementosRepository
{
    private readonly IDbConnectionFactory _db;
    public PersonalImplementosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PersonalImplementos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalImplementos>(SP.SCHEMARrhh.PersonalImplementos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PersonalImplementos?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, string COD_PRODUCTO_IMPLEMENTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PersonalImplementos>(SP.SCHEMARrhh.PersonalImplementos.Showbyid, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_PRODUCTO_IMPLEMENTO = COD_PRODUCTO_IMPLEMENTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.PersonalImplementos.Showcombox);
    }

    public async Task<IEnumerable<PersonalImplementos>> SearchAsync(PersonalImplementosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalImplementos>(SP.SCHEMARrhh.PersonalImplementos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PersonalImplementosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_PRODUCTO_IMPLEMENTO", dto.COD_PRODUCTO_IMPLEMENTO);
        p.Add("COD_TIPO_IMPLEMENTO_LABORAL", dto.COD_TIPO_IMPLEMENTO_LABORAL);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_ITEM_TECNICO", dto.COD_ITEM_TECNICO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalImplementos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalImplementos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalImplementos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PersonalImplementosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_PRODUCTO_IMPLEMENTO", dto.COD_PRODUCTO_IMPLEMENTO);
        p.Add("COD_TIPO_IMPLEMENTO_LABORAL", dto.COD_TIPO_IMPLEMENTO_LABORAL);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_ITEM_TECNICO", dto.COD_ITEM_TECNICO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalImplementos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalImplementos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalImplementos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, string COD_PRODUCTO_IMPLEMENTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalImplementos.Delete, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_PRODUCTO_IMPLEMENTO = COD_PRODUCTO_IMPLEMENTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalImplementos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalImplementos.Delete, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_PRODUCTO_IMPLEMENTO = COD_PRODUCTO_IMPLEMENTO })
        };
    }
}

}