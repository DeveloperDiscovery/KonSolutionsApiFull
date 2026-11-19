using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EstructuraRfidRepository : IEstructuraRfidRepository
{
    private readonly IDbConnectionFactory _db;
    public EstructuraRfidRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EstructuraRfid>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EstructuraRfid>(SP.SCHEMAEstandar.EstructuraRfid.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EstructuraRfid?> ShowbyidAsync(string COD_ESTRUCTURA_RFID)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EstructuraRfid>(SP.SCHEMAEstandar.EstructuraRfid.Showbyid, new { COD_ESTRUCTURA_RFID }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.EstructuraRfid.Showcombox);
    }

    public async Task<IEnumerable<EstructuraRfid>> SearchAsync(EstructuraRfidSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EstructuraRfid>(SP.SCHEMAEstandar.EstructuraRfid.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EstructuraRfidInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ESTRUCTURA_RFID", dto.COD_ESTRUCTURA_RFID);
        p.Add("DES_ESTRUCTURA_RFID", dto.DES_ESTRUCTURA_RFID);
        p.Add("VAL_LONGITUD", dto.VAL_LONGITUD);
        p.Add("NUM_ORDEN", dto.NUM_ORDEN);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.EstructuraRfid.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEstandar.EstructuraRfid.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.EstructuraRfid.Insert, p)
            };
        }
    }

    public async Task<WriteResult> UpdateAsync(EstructuraRfidUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ESTRUCTURA_RFID", dto.COD_ESTRUCTURA_RFID);
        p.Add("DES_ESTRUCTURA_RFID", dto.DES_ESTRUCTURA_RFID);
        p.Add("VAL_LONGITUD", dto.VAL_LONGITUD);
        p.Add("NUM_ORDEN", dto.NUM_ORDEN);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.EstructuraRfid.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEstandar.EstructuraRfid.Update,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.EstructuraRfid.Update, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(string COD_ESTRUCTURA_RFID)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ESTRUCTURA_RFID", COD_ESTRUCTURA_RFID);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.EstructuraRfid.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEstandar.EstructuraRfid.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.EstructuraRfid.Delete, p)
            };
        }
    }
}
