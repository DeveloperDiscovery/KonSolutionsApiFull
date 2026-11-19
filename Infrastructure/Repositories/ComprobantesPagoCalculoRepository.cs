using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ComprobantesPagoCalculoRepository : IComprobantesPagoCalculoRepository
{
    private readonly IDbConnectionFactory _db;
    public ComprobantesPagoCalculoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ComprobantesPagoCalculo>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComprobantesPagoCalculo>(SP.SCHEMAFinanzas.ComprobantesPagoCalculo.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ComprobantesPagoCalculo?> ShowbyidAsync(decimal NUM_REGISTRO, string COD_TIPO_VALOR_CALCULO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ComprobantesPagoCalculo>(SP.SCHEMAFinanzas.ComprobantesPagoCalculo.Showbyid, new { NUM_REGISTRO = NUM_REGISTRO, COD_TIPO_VALOR_CALCULO = COD_TIPO_VALOR_CALCULO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAFinanzas.ComprobantesPagoCalculo.Showcombox);
    }

    public async Task<IEnumerable<ComprobantesPagoCalculo>> SearchAsync(ComprobantesPagoCalculoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComprobantesPagoCalculo>(SP.SCHEMAFinanzas.ComprobantesPagoCalculo.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ComprobantesPagoCalculoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", dto.NUM_REGISTRO);
        p.Add("COD_TIPO_VALOR_CALCULO", dto.COD_TIPO_VALOR_CALCULO);
        p.Add("COD_TIPO_IMPUESTO", dto.COD_TIPO_IMPUESTO);
        p.Add("TAS_IMPUESTO", dto.TAS_IMPUESTO);
        p.Add("VAL_CALCULO", dto.VAL_CALCULO);
        p.Add("NUM_REGISTRO_DESTINO", dto.NUM_REGISTRO_DESTINO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ComprobantesPagoCalculo.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ComprobantesPagoCalculo.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ComprobantesPagoCalculo.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ComprobantesPagoCalculoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", dto.NUM_REGISTRO);
        p.Add("COD_TIPO_VALOR_CALCULO", dto.COD_TIPO_VALOR_CALCULO);
        p.Add("COD_TIPO_IMPUESTO", dto.COD_TIPO_IMPUESTO);
        p.Add("TAS_IMPUESTO", dto.TAS_IMPUESTO);
        p.Add("VAL_CALCULO", dto.VAL_CALCULO);
        p.Add("NUM_REGISTRO_DESTINO", dto.NUM_REGISTRO_DESTINO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ComprobantesPagoCalculo.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ComprobantesPagoCalculo.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ComprobantesPagoCalculo.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal NUM_REGISTRO, string COD_TIPO_VALOR_CALCULO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ComprobantesPagoCalculo.Delete, new { NUM_REGISTRO = NUM_REGISTRO, COD_TIPO_VALOR_CALCULO = COD_TIPO_VALOR_CALCULO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ComprobantesPagoCalculo.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ComprobantesPagoCalculo.Delete, new { NUM_REGISTRO = NUM_REGISTRO, COD_TIPO_VALOR_CALCULO = COD_TIPO_VALOR_CALCULO })
        };
    }
}

}