using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SucursalesSerieComprobantesAplicadasRepository : ISucursalesSerieComprobantesAplicadasRepository
{
    private readonly IDbConnectionFactory _db;
    public SucursalesSerieComprobantesAplicadasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<SucursalesSerieComprobantesAplicadas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SucursalesSerieComprobantesAplicadas>(SP.SCHEMAEstandar.SucursalesSerieComprobantesAplicadas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<SucursalesSerieComprobantesAplicadas?> ShowbyidAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO, string COD_TIPO_COMPROBANTE_PAGO_APLICADO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<SucursalesSerieComprobantesAplicadas>(SP.SCHEMAEstandar.SucursalesSerieComprobantesAplicadas.Showbyid, new { COD_SUCURSAL = COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO = COD_TIPO_COMPROBANTE_PAGO, COD_TIPO_COMPROBANTE_PAGO_APLICADO = COD_TIPO_COMPROBANTE_PAGO_APLICADO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.SucursalesSerieComprobantesAplicadas.Showcombox);
    }

    public async Task<IEnumerable<SucursalesSerieComprobantesAplicadas>> SearchAsync(SucursalesSerieComprobantesAplicadasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SucursalesSerieComprobantesAplicadas>(SP.SCHEMAEstandar.SucursalesSerieComprobantesAplicadas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(SucursalesSerieComprobantesAplicadasInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_TIPO_COMPROBANTE_PAGO", dto.COD_TIPO_COMPROBANTE_PAGO);
        p.Add("COD_TIPO_COMPROBANTE_PAGO_APLICADO", dto.COD_TIPO_COMPROBANTE_PAGO_APLICADO);
        p.Add("NUM_SERIE_COMPROBANTE", dto.NUM_SERIE_COMPROBANTE);
        p.Add("NUM_ULTIMO_CORRELATIVO", dto.NUM_ULTIMO_CORRELATIVO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.SucursalesSerieComprobantesAplicadas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.SucursalesSerieComprobantesAplicadas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.SucursalesSerieComprobantesAplicadas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(SucursalesSerieComprobantesAplicadasUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_TIPO_COMPROBANTE_PAGO", dto.COD_TIPO_COMPROBANTE_PAGO);
        p.Add("COD_TIPO_COMPROBANTE_PAGO_APLICADO", dto.COD_TIPO_COMPROBANTE_PAGO_APLICADO);
        p.Add("NUM_SERIE_COMPROBANTE", dto.NUM_SERIE_COMPROBANTE);
        p.Add("NUM_ULTIMO_CORRELATIVO", dto.NUM_ULTIMO_CORRELATIVO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.SucursalesSerieComprobantesAplicadas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.SucursalesSerieComprobantesAplicadas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.SucursalesSerieComprobantesAplicadas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO, string COD_TIPO_COMPROBANTE_PAGO_APLICADO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.SucursalesSerieComprobantesAplicadas.Delete, new { COD_SUCURSAL = COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO = COD_TIPO_COMPROBANTE_PAGO, COD_TIPO_COMPROBANTE_PAGO_APLICADO = COD_TIPO_COMPROBANTE_PAGO_APLICADO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.SucursalesSerieComprobantesAplicadas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.SucursalesSerieComprobantesAplicadas.Delete, new { COD_SUCURSAL = COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO = COD_TIPO_COMPROBANTE_PAGO, COD_TIPO_COMPROBANTE_PAGO_APLICADO = COD_TIPO_COMPROBANTE_PAGO_APLICADO })
        };
    }
}

}