using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EstructuraCodebarRepository : IEstructuraCodebarRepository
{
    private readonly IDbConnectionFactory _db;
    public EstructuraCodebarRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EstructuraCodebar>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EstructuraCodebar>(SP.SCHEMAEstandar.EstructuraCodebar.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EstructuraCodebar?> ShowbyidAsync(string COD_ESTRUCTURA_CODEBAR)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EstructuraCodebar>(SP.SCHEMAEstandar.EstructuraCodebar.Showbyid, new { COD_ESTRUCTURA_CODEBAR }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.EstructuraCodebar.Showcombox);
    }

    public async Task<WriteResult> InsertAsync(EstructuraCodebarInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ESTRUCTURA_CODEBAR", dto.COD_ESTRUCTURA_CODEBAR);
        p.Add("DES_ESTRUCTURA_CODEBAR", dto.DES_ESTRUCTURA_CODEBAR);
        p.Add("VAL_LONGITUD", dto.VAL_LONGITUD);
        p.Add("NUM_ORDEN", dto.NUM_ORDEN);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.EstructuraCodebar.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEstandar.EstructuraCodebar.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.EstructuraCodebar.Insert, p)
            };
        }
    }

    public async Task<WriteResult> UpdateAsync(EstructuraCodebarUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ESTRUCTURA_CODEBAR", dto.COD_ESTRUCTURA_CODEBAR);
        p.Add("DES_ESTRUCTURA_CODEBAR", dto.DES_ESTRUCTURA_CODEBAR);
        p.Add("VAL_LONGITUD", dto.VAL_LONGITUD);
        p.Add("NUM_ORDEN", dto.NUM_ORDEN);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.EstructuraCodebar.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEstandar.EstructuraCodebar.Update,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.EstructuraCodebar.Update, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(string COD_ESTRUCTURA_CODEBAR)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ESTRUCTURA_CODEBAR", COD_ESTRUCTURA_CODEBAR);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.EstructuraCodebar.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEstandar.EstructuraCodebar.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.EstructuraCodebar.Delete, p)
            };
        }
    }
}
