using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ComprobantesPagoCronogramaRepository : IComprobantesPagoCronogramaRepository
{
    private readonly IDbConnectionFactory _db;
    public ComprobantesPagoCronogramaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ComprobantesPagoCronograma>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComprobantesPagoCronograma>(SP.SCHEMAFinanzas.ComprobantesPagoCronograma.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ComprobantesPagoCronograma?> ShowbyidAsync(decimal NUM_REGISTRO, DateTime FEC_VENCIMIENTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ComprobantesPagoCronograma>(SP.SCHEMAFinanzas.ComprobantesPagoCronograma.Showbyid, new { NUM_REGISTRO = NUM_REGISTRO, FEC_VENCIMIENTO = FEC_VENCIMIENTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAFinanzas.ComprobantesPagoCronograma.Showcombox, "NUM_REGISTRO", "DES_OBSERVACION");
    }

    public async Task<IEnumerable<ComprobantesPagoCronograma>> SearchAsync(ComprobantesPagoCronogramaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComprobantesPagoCronograma>(SP.SCHEMAFinanzas.ComprobantesPagoCronograma.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ComprobantesPagoCronogramaInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", dto.NUM_REGISTRO);
        p.Add("FEC_VENCIMIENTO", dto.FEC_VENCIMIENTO);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("VAL_PENDIENTE", dto.VAL_PENDIENTE);
        p.Add("VAL_PAGADO", dto.VAL_PAGADO);
        p.Add("COD_TIPO_ESTADO_PAGO", dto.COD_TIPO_ESTADO_PAGO);
        p.Add("COD_ESTADO_PAGO", dto.COD_ESTADO_PAGO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ComprobantesPagoCronograma.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ComprobantesPagoCronograma.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ComprobantesPagoCronograma.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ComprobantesPagoCronogramaUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", dto.NUM_REGISTRO);
        p.Add("FEC_VENCIMIENTO", dto.FEC_VENCIMIENTO);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("VAL_PENDIENTE", dto.VAL_PENDIENTE);
        p.Add("VAL_PAGADO", dto.VAL_PAGADO);
        p.Add("COD_TIPO_ESTADO_PAGO", dto.COD_TIPO_ESTADO_PAGO);
        p.Add("COD_ESTADO_PAGO", dto.COD_ESTADO_PAGO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ComprobantesPagoCronograma.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ComprobantesPagoCronograma.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ComprobantesPagoCronograma.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal NUM_REGISTRO, DateTime FEC_VENCIMIENTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ComprobantesPagoCronograma.Delete, new { NUM_REGISTRO = NUM_REGISTRO, FEC_VENCIMIENTO = FEC_VENCIMIENTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ComprobantesPagoCronograma.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ComprobantesPagoCronograma.Delete, new { NUM_REGISTRO = NUM_REGISTRO, FEC_VENCIMIENTO = FEC_VENCIMIENTO })
        };
    }
}

}