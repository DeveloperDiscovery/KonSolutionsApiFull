using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ComprobantesPagoAplicadosRepository : IComprobantesPagoAplicadosRepository
{
    private readonly IDbConnectionFactory _db;
    public ComprobantesPagoAplicadosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ComprobantesPagoAplicados>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComprobantesPagoAplicados>(SP.SCHEMAFinanzas.ComprobantesPagoAplicados.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ComprobantesPagoAplicados?> ShowbyidAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA, decimal NUM_REGISTRO_ASOCIADO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ComprobantesPagoAplicados>(SP.SCHEMAFinanzas.ComprobantesPagoAplicados.Showbyid, new { NUM_REGISTRO = NUM_REGISTRO, NUM_SECUENCIA = NUM_SECUENCIA, NUM_REGISTRO_ASOCIADO = NUM_REGISTRO_ASOCIADO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAFinanzas.ComprobantesPagoAplicados.Showcombox);
    }

    public async Task<IEnumerable<ComprobantesPagoAplicados>> SearchAsync(ComprobantesPagoAplicadosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComprobantesPagoAplicados>(SP.SCHEMAFinanzas.ComprobantesPagoAplicados.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ComprobantesPagoAplicadosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", dto.NUM_REGISTRO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("NUM_REGISTRO_ASOCIADO", dto.NUM_REGISTRO_ASOCIADO);
        p.Add("NUM_SECUENCIA_ASOCIADO", dto.NUM_SECUENCIA_ASOCIADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ComprobantesPagoAplicados.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ComprobantesPagoAplicados.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ComprobantesPagoAplicados.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA, decimal NUM_REGISTRO_ASOCIADO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ComprobantesPagoAplicados.Delete, new { NUM_REGISTRO = NUM_REGISTRO, NUM_SECUENCIA = NUM_SECUENCIA, NUM_REGISTRO_ASOCIADO = NUM_REGISTRO_ASOCIADO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ComprobantesPagoAplicados.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ComprobantesPagoAplicados.Delete, new { NUM_REGISTRO = NUM_REGISTRO, NUM_SECUENCIA = NUM_SECUENCIA, NUM_REGISTRO_ASOCIADO = NUM_REGISTRO_ASOCIADO })
        };
    }
}

}