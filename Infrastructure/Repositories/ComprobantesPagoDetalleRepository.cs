using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ComprobantesPagoDetalleRepository : IComprobantesPagoDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public ComprobantesPagoDetalleRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ComprobantesPagoDetalle>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComprobantesPagoDetalle>(SP.SCHEMAFinanzas.ComprobantesPagoDetalle.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ComprobantesPagoDetalle?> ShowbyidAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ComprobantesPagoDetalle>(SP.SCHEMAFinanzas.ComprobantesPagoDetalle.Showbyid, new { NUM_REGISTRO = NUM_REGISTRO, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAFinanzas.ComprobantesPagoDetalle.Showcombox, "NUM_REGISTRO", "DES_OBSERVACION");
    }

    public async Task<IEnumerable<ComprobantesPagoDetalle>> SearchAsync(ComprobantesPagoDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComprobantesPagoDetalle>(SP.SCHEMAFinanzas.ComprobantesPagoDetalle.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ComprobantesPagoDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", dto.NUM_REGISTRO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("CAN_PRODUCTO_SECUDARIO", dto.CAN_PRODUCTO_SECUDARIO);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIO", dto.COD_UNIDAD_MEDIDA_SECUNDARIO);
        p.Add("COD_TIPO_DESCUENTO", dto.COD_TIPO_DESCUENTO);
        p.Add("TAS_DESCUENTO", dto.TAS_DESCUENTO);
        p.Add("PRE_UNITARIO", dto.PRE_UNITARIO);
        p.Add("VAL_DESCUENTO", dto.VAL_DESCUENTO);
        p.Add("VAL_TOTAL", dto.VAL_TOTAL);
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_SECUENCIA_PEDIDO_COMERCIAL", dto.NUM_SECUENCIA_PEDIDO_COMERCIAL);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ComprobantesPagoDetalle.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ComprobantesPagoDetalle.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ComprobantesPagoDetalle.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ComprobantesPagoDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", dto.NUM_REGISTRO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("CAN_PRODUCTO_SECUDARIO", dto.CAN_PRODUCTO_SECUDARIO);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIO", dto.COD_UNIDAD_MEDIDA_SECUNDARIO);
        p.Add("COD_TIPO_DESCUENTO", dto.COD_TIPO_DESCUENTO);
        p.Add("TAS_DESCUENTO", dto.TAS_DESCUENTO);
        p.Add("PRE_UNITARIO", dto.PRE_UNITARIO);
        p.Add("VAL_DESCUENTO", dto.VAL_DESCUENTO);
        p.Add("VAL_TOTAL", dto.VAL_TOTAL);
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_SECUENCIA_PEDIDO_COMERCIAL", dto.NUM_SECUENCIA_PEDIDO_COMERCIAL);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ComprobantesPagoDetalle.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ComprobantesPagoDetalle.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ComprobantesPagoDetalle.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ComprobantesPagoDetalle.Delete, new { NUM_REGISTRO = NUM_REGISTRO, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ComprobantesPagoDetalle.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ComprobantesPagoDetalle.Delete, new { NUM_REGISTRO = NUM_REGISTRO, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}