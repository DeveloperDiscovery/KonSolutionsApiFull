using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposPersonalRepository : ITiposPersonalRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposPersonalRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposPersonal>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposPersonal>(SP.SCHEMARrhh.TiposPersonal.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposPersonal?> ShowbyidAsync(string COD_TIPO_PERSONAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposPersonal>(SP.SCHEMARrhh.TiposPersonal.Showbyid, new { COD_TIPO_PERSONAL = COD_TIPO_PERSONAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.TiposPersonal.Showcombox);
    }

    public async Task<IEnumerable<TiposPersonal>> SearchAsync(TiposPersonalSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposPersonal>(SP.SCHEMARrhh.TiposPersonal.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposPersonalInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PERSONAL", dto.COD_TIPO_PERSONAL);
        p.Add("DES_TIPO_PERSONAL", dto.DES_TIPO_PERSONAL);
        p.Add("COD_PREFIJO_ID", dto.COD_PREFIJO_ID);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposPersonal.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposPersonal.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposPersonal.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposPersonalUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PERSONAL", dto.COD_TIPO_PERSONAL);
        p.Add("DES_TIPO_PERSONAL", dto.DES_TIPO_PERSONAL);
        p.Add("COD_PREFIJO_ID", dto.COD_PREFIJO_ID);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposPersonal.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposPersonal.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposPersonal.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_PERSONAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposPersonal.Delete, new { COD_TIPO_PERSONAL = COD_TIPO_PERSONAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposPersonal.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposPersonal.Delete, new { COD_TIPO_PERSONAL = COD_TIPO_PERSONAL })
        };
    }
}

}