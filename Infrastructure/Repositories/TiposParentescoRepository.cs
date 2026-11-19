using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposParentescoRepository : ITiposParentescoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposParentescoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposParentesco>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposParentesco>(SP.SCHEMARrhh.TiposParentesco.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposParentesco?> ShowbyidAsync(string COD_TIPO_PARENTESCO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposParentesco>(SP.SCHEMARrhh.TiposParentesco.Showbyid, new { COD_TIPO_PARENTESCO = COD_TIPO_PARENTESCO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.TiposParentesco.Showcombox);
    }

    public async Task<IEnumerable<TiposParentesco>> SearchAsync(TiposParentescoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposParentesco>(SP.SCHEMARrhh.TiposParentesco.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposParentescoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PARENTESCO", dto.COD_TIPO_PARENTESCO);
        p.Add("DES_TIPO_PARENTESCO", dto.DES_TIPO_PARENTESCO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposParentesco.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposParentesco.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposParentesco.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposParentescoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PARENTESCO", dto.COD_TIPO_PARENTESCO);
        p.Add("DES_TIPO_PARENTESCO", dto.DES_TIPO_PARENTESCO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposParentesco.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposParentesco.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposParentesco.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_PARENTESCO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposParentesco.Delete, new { COD_TIPO_PARENTESCO = COD_TIPO_PARENTESCO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposParentesco.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposParentesco.Delete, new { COD_TIPO_PARENTESCO = COD_TIPO_PARENTESCO })
        };
    }
}

}