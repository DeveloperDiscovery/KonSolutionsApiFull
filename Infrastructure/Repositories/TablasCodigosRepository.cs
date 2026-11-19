using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Developer;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TablasCodigosRepository : ITablasCodigosRepository
{
    private readonly IDbConnectionFactory _db;
    public TablasCodigosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TablasCodigos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TablasCodigos>(SP.SCHEMADeveloper.TablasCodigos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TablasCodigos?> ShowbyidAsync(string NAME_SCHEMA, string NAME_TABLA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TablasCodigos>(SP.SCHEMADeveloper.TablasCodigos.Showbyid, new { NAME_SCHEMA = NAME_SCHEMA, NAME_TABLA = NAME_TABLA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMADeveloper.TablasCodigos.Showcombox);
    }

    public async Task<IEnumerable<TablasCodigos>> SearchAsync(TablasCodigosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TablasCodigos>(SP.SCHEMADeveloper.TablasCodigos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TablasCodigosInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NAME_SCHEMA", dto.NAME_SCHEMA);
        p.Add("NAME_TABLA", dto.NAME_TABLA);
        p.Add("FIELD_CORRELATIVO", dto.FIELD_CORRELATIVO);
        p.Add("FIELD_PARENT", dto.FIELD_PARENT);
        p.Add("FIELD_WHERE", dto.FIELD_WHERE);
        p.Add("IS_NUMERIC", dto.IS_NUMERIC);
        p.Add("FRACCION_SEGMENTO", dto.FRACCION_SEGMENTO);
        p.Add("LONGITUD_MAXIMA", dto.LONGITUD_MAXIMA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMADeveloper.TablasCodigos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMADeveloper.TablasCodigos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMADeveloper.TablasCodigos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TablasCodigosUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NAME_SCHEMA", dto.NAME_SCHEMA);
        p.Add("NAME_TABLA", dto.NAME_TABLA);
        p.Add("FIELD_CORRELATIVO", dto.FIELD_CORRELATIVO);
        p.Add("FIELD_PARENT", dto.FIELD_PARENT);
        p.Add("FIELD_WHERE", dto.FIELD_WHERE);
        p.Add("IS_NUMERIC", dto.IS_NUMERIC);
        p.Add("FRACCION_SEGMENTO", dto.FRACCION_SEGMENTO);
        p.Add("LONGITUD_MAXIMA", dto.LONGITUD_MAXIMA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMADeveloper.TablasCodigos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMADeveloper.TablasCodigos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMADeveloper.TablasCodigos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string NAME_SCHEMA, string NAME_TABLA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMADeveloper.TablasCodigos.Delete, new { NAME_SCHEMA = NAME_SCHEMA, NAME_TABLA = NAME_TABLA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMADeveloper.TablasCodigos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMADeveloper.TablasCodigos.Delete, new { NAME_SCHEMA = NAME_SCHEMA, NAME_TABLA = NAME_TABLA })
        };
    }
}

}