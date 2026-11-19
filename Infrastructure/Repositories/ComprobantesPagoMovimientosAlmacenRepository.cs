using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ComprobantesPagoMovimientosAlmacenRepository : IComprobantesPagoMovimientosAlmacenRepository
{
    private readonly IDbConnectionFactory _db;
    public ComprobantesPagoMovimientosAlmacenRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ComprobantesPagoMovimientosAlmacen>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComprobantesPagoMovimientosAlmacen>(SP.SCHEMAFinanzas.ComprobantesPagoMovimientosAlmacen.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ComprobantesPagoMovimientosAlmacen?> ShowbyidAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA_COMPROBANTE, decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ComprobantesPagoMovimientosAlmacen>(SP.SCHEMAFinanzas.ComprobantesPagoMovimientosAlmacen.Showbyid, new { NUM_REGISTRO = NUM_REGISTRO, NUM_SECUENCIA_COMPROBANTE = NUM_SECUENCIA_COMPROBANTE, NUM_MOVIMIENTO_ALMACEN = NUM_MOVIMIENTO_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAFinanzas.ComprobantesPagoMovimientosAlmacen.Showcombox);
    }

    public async Task<IEnumerable<ComprobantesPagoMovimientosAlmacen>> SearchAsync(ComprobantesPagoMovimientosAlmacenSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComprobantesPagoMovimientosAlmacen>(SP.SCHEMAFinanzas.ComprobantesPagoMovimientosAlmacen.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ComprobantesPagoMovimientosAlmacenInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", dto.NUM_REGISTRO);
        p.Add("NUM_SECUENCIA_COMPROBANTE", dto.NUM_SECUENCIA_COMPROBANTE);
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("NUM_SECUENCIA_MOVIMIENTO", dto.NUM_SECUENCIA_MOVIMIENTO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ComprobantesPagoMovimientosAlmacen.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ComprobantesPagoMovimientosAlmacen.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ComprobantesPagoMovimientosAlmacen.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA_COMPROBANTE, decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ComprobantesPagoMovimientosAlmacen.Delete, new { NUM_REGISTRO = NUM_REGISTRO, NUM_SECUENCIA_COMPROBANTE = NUM_SECUENCIA_COMPROBANTE, NUM_MOVIMIENTO_ALMACEN = NUM_MOVIMIENTO_ALMACEN }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ComprobantesPagoMovimientosAlmacen.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ComprobantesPagoMovimientosAlmacen.Delete, new { NUM_REGISTRO = NUM_REGISTRO, NUM_SECUENCIA_COMPROBANTE = NUM_SECUENCIA_COMPROBANTE, NUM_MOVIMIENTO_ALMACEN = NUM_MOVIMIENTO_ALMACEN })
        };
    }
}

}