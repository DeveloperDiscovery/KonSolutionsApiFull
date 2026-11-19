using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class MonedasRepository : IMonedasRepository
{
    private readonly IDbConnectionFactory _db;
    public MonedasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Monedas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Monedas>(SP.SCHEMAEstandar.Monedas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Monedas?> ShowbyidAsync(string COD_MONEDA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Monedas>(SP.SCHEMAEstandar.Monedas.Showbyid, new { COD_MONEDA = COD_MONEDA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.Monedas.Showcombox);
    }

    public async Task<IEnumerable<Monedas>> SearchAsync(MonedasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Monedas>(SP.SCHEMAEstandar.Monedas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(MonedasInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("DES_MONEDA", dto.DES_MONEDA);
        p.Add("COD_MONEDA_SUNAT", dto.COD_MONEDA_SUNAT);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Monedas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Monedas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Monedas.Insert, p)
        };
    }
}

    public async Task<WriteResult> UpdateAsync(string COD_MONEDA, MonedasUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_MONEDA", COD_MONEDA);
        p.Add("DES_MONEDA", dto.DES_MONEDA);
        p.Add("COD_MONEDA_SUNAT", dto.COD_MONEDA_SUNAT);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Monedas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Monedas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Monedas.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_MONEDA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Monedas.Delete, new { COD_MONEDA = COD_MONEDA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Monedas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Monedas.Delete, new { COD_MONEDA = COD_MONEDA })
        };
    }
}

}