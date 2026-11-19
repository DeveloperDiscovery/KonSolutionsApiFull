using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ClasesDesarrolloRepository : IClasesDesarrolloRepository
{
    private readonly IDbConnectionFactory _db;
    public ClasesDesarrolloRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ClasesDesarrollo>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ClasesDesarrollo>(SP.SCHEMAIngenieria.ClasesDesarrollo.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ClasesDesarrollo?> ShowbyidAsync(string COD_CLASE_DESARROLLO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ClasesDesarrollo>(SP.SCHEMAIngenieria.ClasesDesarrollo.Showbyid, new { COD_CLASE_DESARROLLO = COD_CLASE_DESARROLLO }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>SHOWCOMBOX devuelve las mismas columnas COD_/DES_ que SHOWALL (no Id/Texto),
    /// así que se materializa como ClasesDesarrollo y el cliente arma el combo.</summary>
    public async Task<IEnumerable<ClasesDesarrollo>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ClasesDesarrollo>(SP.SCHEMAIngenieria.ClasesDesarrollo.Showcombox, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ClasesDesarrolloInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_DESARROLLO", dto.COD_CLASE_DESARROLLO);
        p.Add("DES_CLASE_DESARROLLO", dto.DES_CLASE_DESARROLLO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.ClasesDesarrollo.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.ClasesDesarrollo.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.ClasesDesarrollo.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CLASE_DESARROLLO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.ClasesDesarrollo.Delete, new { COD_CLASE_DESARROLLO = COD_CLASE_DESARROLLO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.ClasesDesarrollo.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.ClasesDesarrollo.Delete, new { COD_CLASE_DESARROLLO = COD_CLASE_DESARROLLO })
        };
    }
}
}
