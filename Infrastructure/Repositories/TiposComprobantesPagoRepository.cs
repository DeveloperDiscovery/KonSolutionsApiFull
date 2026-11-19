using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposComprobantesPagoRepository : ITiposComprobantesPagoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposComprobantesPagoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposComprobantesPago>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposComprobantesPago>(SP.SCHEMAEstandar.TiposComprobantesPago.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposComprobantesPago?> ShowbyidAsync(string COD_TIPO_COMPROBANTE_PAGO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposComprobantesPago>(SP.SCHEMAEstandar.TiposComprobantesPago.Showbyid, new { COD_TIPO_COMPROBANTE_PAGO = COD_TIPO_COMPROBANTE_PAGO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.TiposComprobantesPago.Showcombox);
    }

    public async Task<IEnumerable<TiposComprobantesPago>> SearchAsync(TiposComprobantesPagoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposComprobantesPago>(SP.SCHEMAEstandar.TiposComprobantesPago.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposComprobantesPagoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_COMPROBANTE_PAGO", dto.COD_TIPO_COMPROBANTE_PAGO);
        p.Add("DES_TIPO_COMPROBANTE_PAGO", dto.DES_TIPO_COMPROBANTE_PAGO);
        p.Add("COD_PREFIJO", dto.COD_PREFIJO);
        p.Add("COD_PREFIJO_SUNAT", dto.COD_PREFIJO_SUNAT);
        p.Add("COD_COMPROBANTE_PAGO_SUNAT", dto.COD_COMPROBANTE_PAGO_SUNAT);
        p.Add("FLG_EMISION", dto.FLG_EMISION ?? false);
        p.Add("FLG_POSITIVO", dto.FLG_POSITIVO ?? false);
        p.Add("FLG_ANTICIPO", dto.FLG_ANTICIPO ?? false);
        p.Add("FLG_PREFIJO_COMPROBANTE_ORIGEN", dto.FLG_PREFIJO_COMPROBANTE_ORIGEN ?? false);
        p.Add("FLG_DOCUMENTO_APLICABLE", dto.FLG_DOCUMENTO_APLICABLE ?? false);
        p.Add("FLG_CONTROL_INTERNO", dto.FLG_CONTROL_INTERNO ?? false);
        p.Add("VAL_LONGITUD_SERIE", dto.VAL_LONGITUD_SERIE);
        p.Add("VAL_LONGITUD_NUMERO", dto.VAL_LONGITUD_NUMERO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposComprobantesPago.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposComprobantesPago.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposComprobantesPago.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposComprobantesPagoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_COMPROBANTE_PAGO", dto.COD_TIPO_COMPROBANTE_PAGO);
        p.Add("DES_TIPO_COMPROBANTE_PAGO", dto.DES_TIPO_COMPROBANTE_PAGO);
        p.Add("COD_PREFIJO", dto.COD_PREFIJO);
        p.Add("COD_PREFIJO_SUNAT", dto.COD_PREFIJO_SUNAT);
        p.Add("COD_COMPROBANTE_PAGO_SUNAT", dto.COD_COMPROBANTE_PAGO_SUNAT);
        p.Add("FLG_EMISION", dto.FLG_EMISION ?? false);
        p.Add("FLG_POSITIVO", dto.FLG_POSITIVO ?? false);
        p.Add("FLG_ANTICIPO", dto.FLG_ANTICIPO ?? false);
        p.Add("FLG_PREFIJO_COMPROBANTE_ORIGEN", dto.FLG_PREFIJO_COMPROBANTE_ORIGEN ?? false);
        p.Add("FLG_DOCUMENTO_APLICABLE", dto.FLG_DOCUMENTO_APLICABLE ?? false);
        p.Add("FLG_CONTROL_INTERNO", dto.FLG_CONTROL_INTERNO ?? false);
        p.Add("VAL_LONGITUD_SERIE", dto.VAL_LONGITUD_SERIE);
        p.Add("VAL_LONGITUD_NUMERO", dto.VAL_LONGITUD_NUMERO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposComprobantesPago.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposComprobantesPago.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposComprobantesPago.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_COMPROBANTE_PAGO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposComprobantesPago.Delete, new { COD_TIPO_COMPROBANTE_PAGO = COD_TIPO_COMPROBANTE_PAGO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposComprobantesPago.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposComprobantesPago.Delete, new { COD_TIPO_COMPROBANTE_PAGO = COD_TIPO_COMPROBANTE_PAGO })
        };
    }
}

}