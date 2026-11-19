using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ComprobantesPagoAsociadosRepository : IComprobantesPagoAsociadosRepository
{
    private readonly IDbConnectionFactory _db;
    public ComprobantesPagoAsociadosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ComprobantesPagoAsociados>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComprobantesPagoAsociados>(SP.SCHEMAFinanzas.ComprobantesPagoAsociados.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ComprobantesPagoAsociados?> ShowbyidAsync(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ASOCIADO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ComprobantesPagoAsociados>(SP.SCHEMAFinanzas.ComprobantesPagoAsociados.Showbyid, new { NUM_REGISTRO = NUM_REGISTRO, NUM_REGISTRO_ASOCIADO = NUM_REGISTRO_ASOCIADO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAFinanzas.ComprobantesPagoAsociados.Showcombox);
    }

    public async Task<IEnumerable<ComprobantesPagoAsociados>> SearchAsync(ComprobantesPagoAsociadosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComprobantesPagoAsociados>(SP.SCHEMAFinanzas.ComprobantesPagoAsociados.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ComprobantesPagoAsociadosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", dto.NUM_REGISTRO);
        p.Add("NUM_REGISTRO_ASOCIADO", dto.NUM_REGISTRO_ASOCIADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ComprobantesPagoAsociados.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ComprobantesPagoAsociados.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ComprobantesPagoAsociados.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ASOCIADO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ComprobantesPagoAsociados.Delete, new { NUM_REGISTRO = NUM_REGISTRO, NUM_REGISTRO_ASOCIADO = NUM_REGISTRO_ASOCIADO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ComprobantesPagoAsociados.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ComprobantesPagoAsociados.Delete, new { NUM_REGISTRO = NUM_REGISTRO, NUM_REGISTRO_ASOCIADO = NUM_REGISTRO_ASOCIADO })
        };
    }
}

}