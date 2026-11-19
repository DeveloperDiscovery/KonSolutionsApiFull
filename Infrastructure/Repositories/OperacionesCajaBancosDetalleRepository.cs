using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class OperacionesCajaBancosDetalleRepository : IOperacionesCajaBancosDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public OperacionesCajaBancosDetalleRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<OperacionesCajaBancosDetalle>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<OperacionesCajaBancosDetalle>(SP.SCHEMAFinanzas.OperacionesCajaBancosDetalle.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<OperacionesCajaBancosDetalle?> ShowbyidAsync(decimal NUM_OPERACION, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<OperacionesCajaBancosDetalle>(SP.SCHEMAFinanzas.OperacionesCajaBancosDetalle.Showbyid, new { NUM_OPERACION = NUM_OPERACION, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAFinanzas.OperacionesCajaBancosDetalle.Showcombox);
    }

    public async Task<IEnumerable<OperacionesCajaBancosDetalle>> SearchAsync(OperacionesCajaBancosDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<OperacionesCajaBancosDetalle>(SP.SCHEMAFinanzas.OperacionesCajaBancosDetalle.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(OperacionesCajaBancosDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_OPERACION", dto.NUM_OPERACION);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("NUM_REGISTRO_COMPROBANTES", dto.NUM_REGISTRO_COMPROBANTES);
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("NUM_OPERACION_BANCARIA", dto.NUM_OPERACION_BANCARIA);
        p.Add("COD_FORMA_PAGO", dto.COD_FORMA_PAGO);
        p.Add("VAL_OPERACION", dto.VAL_OPERACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.OperacionesCajaBancosDetalle.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.OperacionesCajaBancosDetalle.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.OperacionesCajaBancosDetalle.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(OperacionesCajaBancosDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_OPERACION", dto.NUM_OPERACION);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("NUM_REGISTRO_COMPROBANTES", dto.NUM_REGISTRO_COMPROBANTES);
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("NUM_OPERACION_BANCARIA", dto.NUM_OPERACION_BANCARIA);
        p.Add("COD_FORMA_PAGO", dto.COD_FORMA_PAGO);
        p.Add("VAL_OPERACION", dto.VAL_OPERACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.OperacionesCajaBancosDetalle.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.OperacionesCajaBancosDetalle.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.OperacionesCajaBancosDetalle.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal NUM_OPERACION, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.OperacionesCajaBancosDetalle.Delete, new { NUM_OPERACION = NUM_OPERACION, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.OperacionesCajaBancosDetalle.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.OperacionesCajaBancosDetalle.Delete, new { NUM_OPERACION = NUM_OPERACION, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}