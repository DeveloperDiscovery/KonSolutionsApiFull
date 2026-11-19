using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposDesarrolloRepository : ITiposDesarrolloRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposDesarrolloRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposDesarrollo>> ShowallAsync(string codClaseDesarrollo)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposDesarrollo>(SP.SCHEMAIngenieria.TiposDesarrollo.Showall, new { COD_CLASE_DESARROLLO = codClaseDesarrollo }, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposDesarrollo?> ShowbyidAsync(string COD_CLASE_DESARROLLO, string COD_TIPO_DESARROLLO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposDesarrollo>(SP.SCHEMAIngenieria.TiposDesarrollo.Showbyid, new { COD_CLASE_DESARROLLO = COD_CLASE_DESARROLLO, COD_TIPO_DESARROLLO = COD_TIPO_DESARROLLO }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>SHOWCOMBOX devuelve las mismas columnas COD_/DES_ que SHOWALL (no Id/Texto)
    /// y además exige la clase, igual que SHOWALL.</summary>
    public async Task<IEnumerable<TiposDesarrollo>> ShowcomboxAsync(string codClaseDesarrollo)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposDesarrollo>(SP.SCHEMAIngenieria.TiposDesarrollo.Showcombox, new { COD_CLASE_DESARROLLO = codClaseDesarrollo }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<TiposDesarrollo>> SearchAsync(string codClaseDesarrollo, TiposDesarrolloSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposDesarrollo>(SP.SCHEMAIngenieria.TiposDesarrollo.Search,
            new { COD_CLASE_DESARROLLO = codClaseDesarrollo, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposDesarrolloInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_DESARROLLO", dto.COD_CLASE_DESARROLLO);
        p.Add("COD_TIPO_DESARROLLO", dto.COD_TIPO_DESARROLLO);
        p.Add("DES_TIPO_DESARROLLO", dto.DES_TIPO_DESARROLLO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.TiposDesarrollo.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.TiposDesarrollo.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.TiposDesarrollo.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CLASE_DESARROLLO, string COD_TIPO_DESARROLLO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.TiposDesarrollo.Delete, new { COD_CLASE_DESARROLLO = COD_CLASE_DESARROLLO, COD_TIPO_DESARROLLO = COD_TIPO_DESARROLLO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.TiposDesarrollo.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.TiposDesarrollo.Delete, new { COD_CLASE_DESARROLLO = COD_CLASE_DESARROLLO, COD_TIPO_DESARROLLO = COD_TIPO_DESARROLLO })
        };
    }
}
}
