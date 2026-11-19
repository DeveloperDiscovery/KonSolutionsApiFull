using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ComprobantesPagoRepository : IComprobantesPagoRepository
{
    private readonly IDbConnectionFactory _db;
    public ComprobantesPagoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ComprobantesPago>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComprobantesPago>(SP.SCHEMAFinanzas.ComprobantesPago.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ComprobantesPago?> ShowbyidAsync(decimal NUM_REGISTRO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ComprobantesPago>(SP.SCHEMAFinanzas.ComprobantesPago.Showbyid, new { NUM_REGISTRO = NUM_REGISTRO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAFinanzas.ComprobantesPago.Showcombox, "NUM_REGISTRO", "");
    }

    public async Task<IEnumerable<ComprobantesPago>> SearchAsync(ComprobantesPagoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComprobantesPago>(SP.SCHEMAFinanzas.ComprobantesPago.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ComprobantesPagoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", dto.NUM_REGISTRO);
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("NUM_SECUENCIA_DIRECCION", dto.NUM_SECUENCIA_DIRECCION);
        p.Add("COD_TIPO_COMPROBANTE_PAGO", dto.COD_TIPO_COMPROBANTE_PAGO);
        p.Add("NUM_COMPROBANTE_PAGO", dto.NUM_COMPROBANTE_PAGO);
        p.Add("FEC_COMPROBANTE_PAGO", dto.FEC_COMPROBANTE_PAGO);
        p.Add("FEC_VENCIMIENTO", dto.FEC_VENCIMIENTO);
        p.Add("FEC_RECEPCION", dto.FEC_RECEPCION);
        p.Add("FEC_DEVOLUCION", dto.FEC_DEVOLUCION);
        p.Add("FEC_EMBARQUE", dto.FEC_EMBARQUE);
        p.Add("FEC_COMERCIAL", dto.FEC_COMERCIAL);
        p.Add("FEC_REGISTRO_CAJA", dto.FEC_REGISTRO_CAJA);
        p.Add("COD_CAJA", dto.COD_CAJA);
        p.Add("NUM_UNIDAD_COMERCIAL", dto.NUM_UNIDAD_COMERCIAL);
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("NUM_LOTE_CONTROL", dto.NUM_LOTE_CONTROL);
        p.Add("COD_FORMA_PAGO", dto.COD_FORMA_PAGO);
        p.Add("COD_CONDICION_PAGO", dto.COD_CONDICION_PAGO);
        p.Add("COD_TIPO_DESCUENTO", dto.COD_TIPO_DESCUENTO);
        p.Add("TAS_DESCUENTO", dto.TAS_DESCUENTO);
        p.Add("COD_INCOTERM", dto.COD_INCOTERM);
        p.Add("COD_CENTRO_COSTO_SOLICITANTE", dto.COD_CENTRO_COSTO_SOLICITANTE);
        p.Add("COD_CENTRO_COSTO_DESTINO", dto.COD_CENTRO_COSTO_DESTINO);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("VAL_TOTAL", dto.VAL_TOTAL);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_ENTIDAD_VENDEDOR", dto.COD_ENTIDAD_VENDEDOR);
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("COD_ASIENTO_CONTABLE", dto.COD_ASIENTO_CONTABLE);
        p.Add("NUM_ASIENTO_CONTABLE", dto.NUM_ASIENTO_CONTABLE);
        p.Add("COD_TIPO_ESTADO_PAGO", dto.COD_TIPO_ESTADO_PAGO);
        p.Add("COD_ESTADO_PAGO", dto.COD_ESTADO_PAGO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ComprobantesPago.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ComprobantesPago.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ComprobantesPago.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ComprobantesPagoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", dto.NUM_REGISTRO);
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("NUM_SECUENCIA_DIRECCION", dto.NUM_SECUENCIA_DIRECCION);
        p.Add("COD_TIPO_COMPROBANTE_PAGO", dto.COD_TIPO_COMPROBANTE_PAGO);
        p.Add("NUM_COMPROBANTE_PAGO", dto.NUM_COMPROBANTE_PAGO);
        p.Add("FEC_COMPROBANTE_PAGO", dto.FEC_COMPROBANTE_PAGO);
        p.Add("FEC_VENCIMIENTO", dto.FEC_VENCIMIENTO);
        p.Add("FEC_RECEPCION", dto.FEC_RECEPCION);
        p.Add("FEC_DEVOLUCION", dto.FEC_DEVOLUCION);
        p.Add("FEC_EMBARQUE", dto.FEC_EMBARQUE);
        p.Add("FEC_COMERCIAL", dto.FEC_COMERCIAL);
        p.Add("FEC_REGISTRO_CAJA", dto.FEC_REGISTRO_CAJA);
        p.Add("COD_CAJA", dto.COD_CAJA);
        p.Add("NUM_UNIDAD_COMERCIAL", dto.NUM_UNIDAD_COMERCIAL);
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("NUM_LOTE_CONTROL", dto.NUM_LOTE_CONTROL);
        p.Add("COD_FORMA_PAGO", dto.COD_FORMA_PAGO);
        p.Add("COD_CONDICION_PAGO", dto.COD_CONDICION_PAGO);
        p.Add("COD_TIPO_DESCUENTO", dto.COD_TIPO_DESCUENTO);
        p.Add("TAS_DESCUENTO", dto.TAS_DESCUENTO);
        p.Add("COD_INCOTERM", dto.COD_INCOTERM);
        p.Add("COD_CENTRO_COSTO_SOLICITANTE", dto.COD_CENTRO_COSTO_SOLICITANTE);
        p.Add("COD_CENTRO_COSTO_DESTINO", dto.COD_CENTRO_COSTO_DESTINO);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("VAL_TOTAL", dto.VAL_TOTAL);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_ENTIDAD_VENDEDOR", dto.COD_ENTIDAD_VENDEDOR);
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("COD_ASIENTO_CONTABLE", dto.COD_ASIENTO_CONTABLE);
        p.Add("NUM_ASIENTO_CONTABLE", dto.NUM_ASIENTO_CONTABLE);
        p.Add("COD_TIPO_ESTADO_PAGO", dto.COD_TIPO_ESTADO_PAGO);
        p.Add("COD_ESTADO_PAGO", dto.COD_ESTADO_PAGO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ComprobantesPago.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ComprobantesPago.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ComprobantesPago.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal NUM_REGISTRO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ComprobantesPago.Delete, new { NUM_REGISTRO = NUM_REGISTRO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ComprobantesPago.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ComprobantesPago.Delete, new { NUM_REGISTRO = NUM_REGISTRO })
        };
    }
}

}