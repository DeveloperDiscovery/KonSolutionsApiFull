using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class OperacionesCajaBancosDetalleCalculoRepository : IOperacionesCajaBancosDetalleCalculoRepository
{
    private readonly IDbConnectionFactory _db;
    public OperacionesCajaBancosDetalleCalculoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<OperacionesCajaBancosDetalleCalculo>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<OperacionesCajaBancosDetalleCalculo>(SP.SCHEMAFinanzas.OperacionesCajaBancosDetalleCalculo.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<OperacionesCajaBancosDetalleCalculo?> ShowbyidAsync(decimal NUM_OPERACION, int NUM_SECUENCIA, string COD_TIPO_VALOR_CALCULO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<OperacionesCajaBancosDetalleCalculo>(SP.SCHEMAFinanzas.OperacionesCajaBancosDetalleCalculo.Showbyid, new { NUM_OPERACION = NUM_OPERACION, NUM_SECUENCIA = NUM_SECUENCIA, COD_TIPO_VALOR_CALCULO = COD_TIPO_VALOR_CALCULO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAFinanzas.OperacionesCajaBancosDetalleCalculo.Showcombox);
    }

    public async Task<IEnumerable<OperacionesCajaBancosDetalleCalculo>> SearchAsync(OperacionesCajaBancosDetalleCalculoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<OperacionesCajaBancosDetalleCalculo>(SP.SCHEMAFinanzas.OperacionesCajaBancosDetalleCalculo.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(OperacionesCajaBancosDetalleCalculoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_OPERACION", dto.NUM_OPERACION);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_TIPO_VALOR_CALCULO", dto.COD_TIPO_VALOR_CALCULO);
        p.Add("COD_TIPO_IMPUESTO", dto.COD_TIPO_IMPUESTO);
        p.Add("TAS_IMPUESTO", dto.TAS_IMPUESTO);
        p.Add("VAL_CALCULO", dto.VAL_CALCULO);
        p.Add("NUM_OPERACION_DESTINO", dto.NUM_OPERACION_DESTINO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.OperacionesCajaBancosDetalleCalculo.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.OperacionesCajaBancosDetalleCalculo.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.OperacionesCajaBancosDetalleCalculo.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(OperacionesCajaBancosDetalleCalculoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_OPERACION", dto.NUM_OPERACION);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_TIPO_VALOR_CALCULO", dto.COD_TIPO_VALOR_CALCULO);
        p.Add("COD_TIPO_IMPUESTO", dto.COD_TIPO_IMPUESTO);
        p.Add("TAS_IMPUESTO", dto.TAS_IMPUESTO);
        p.Add("VAL_CALCULO", dto.VAL_CALCULO);
        p.Add("NUM_OPERACION_DESTINO", dto.NUM_OPERACION_DESTINO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.OperacionesCajaBancosDetalleCalculo.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.OperacionesCajaBancosDetalleCalculo.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.OperacionesCajaBancosDetalleCalculo.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal NUM_OPERACION, int NUM_SECUENCIA, string COD_TIPO_VALOR_CALCULO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.OperacionesCajaBancosDetalleCalculo.Delete, new { NUM_OPERACION = NUM_OPERACION, NUM_SECUENCIA = NUM_SECUENCIA, COD_TIPO_VALOR_CALCULO = COD_TIPO_VALOR_CALCULO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.OperacionesCajaBancosDetalleCalculo.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.OperacionesCajaBancosDetalleCalculo.Delete, new { NUM_OPERACION = NUM_OPERACION, NUM_SECUENCIA = NUM_SECUENCIA, COD_TIPO_VALOR_CALCULO = COD_TIPO_VALOR_CALCULO })
        };
    }
}

}