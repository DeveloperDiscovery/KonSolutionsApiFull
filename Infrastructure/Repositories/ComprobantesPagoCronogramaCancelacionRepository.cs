using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ComprobantesPagoCronogramaCancelacionRepository : IComprobantesPagoCronogramaCancelacionRepository
{
    private readonly IDbConnectionFactory _db;
    public ComprobantesPagoCronogramaCancelacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ComprobantesPagoCronogramaCancelacion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComprobantesPagoCronogramaCancelacion>(SP.SCHEMAFinanzas.ComprobantesPagoCronogramaCancelacion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ComprobantesPagoCronogramaCancelacion?> ShowbyidAsync(decimal NUM_REGISTRO, DateTime FEC_VENCIMIENTO, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ComprobantesPagoCronogramaCancelacion>(SP.SCHEMAFinanzas.ComprobantesPagoCronogramaCancelacion.Showbyid, new { NUM_REGISTRO = NUM_REGISTRO, FEC_VENCIMIENTO = FEC_VENCIMIENTO, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAFinanzas.ComprobantesPagoCronogramaCancelacion.Showcombox);
    }

    public async Task<IEnumerable<ComprobantesPagoCronogramaCancelacion>> SearchAsync(ComprobantesPagoCronogramaCancelacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComprobantesPagoCronogramaCancelacion>(SP.SCHEMAFinanzas.ComprobantesPagoCronogramaCancelacion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ComprobantesPagoCronogramaCancelacionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", dto.NUM_REGISTRO);
        p.Add("FEC_VENCIMIENTO", dto.FEC_VENCIMIENTO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_FORMA_PAGO", dto.COD_FORMA_PAGO);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("VAL_CANCELACION", dto.VAL_CANCELACION);
        p.Add("FEC_CANCELACION", dto.FEC_CANCELACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ComprobantesPagoCronogramaCancelacion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ComprobantesPagoCronogramaCancelacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ComprobantesPagoCronogramaCancelacion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ComprobantesPagoCronogramaCancelacionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", dto.NUM_REGISTRO);
        p.Add("FEC_VENCIMIENTO", dto.FEC_VENCIMIENTO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_FORMA_PAGO", dto.COD_FORMA_PAGO);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("VAL_CANCELACION", dto.VAL_CANCELACION);
        p.Add("FEC_CANCELACION", dto.FEC_CANCELACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ComprobantesPagoCronogramaCancelacion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ComprobantesPagoCronogramaCancelacion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ComprobantesPagoCronogramaCancelacion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal NUM_REGISTRO, DateTime FEC_VENCIMIENTO, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ComprobantesPagoCronogramaCancelacion.Delete, new { NUM_REGISTRO = NUM_REGISTRO, FEC_VENCIMIENTO = FEC_VENCIMIENTO, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ComprobantesPagoCronogramaCancelacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ComprobantesPagoCronogramaCancelacion.Delete, new { NUM_REGISTRO = NUM_REGISTRO, FEC_VENCIMIENTO = FEC_VENCIMIENTO, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}