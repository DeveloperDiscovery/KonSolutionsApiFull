using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Networking;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class LiquidacionesComisionVentaDetalleRepository : ILiquidacionesComisionVentaDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public LiquidacionesComisionVentaDetalleRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<LiquidacionesComisionVentaDetalle>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<LiquidacionesComisionVentaDetalle>(SP.SCHEMANetworking.LiquidacionesComisionVentaDetalle.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<LiquidacionesComisionVentaDetalle?> ShowbyidAsync(int NUM_LIQUIDACION_COMISION_VENTA, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<LiquidacionesComisionVentaDetalle>(SP.SCHEMANetworking.LiquidacionesComisionVentaDetalle.Showbyid, new { NUM_LIQUIDACION_COMISION_VENTA = NUM_LIQUIDACION_COMISION_VENTA, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANetworking.LiquidacionesComisionVentaDetalle.Showcombox);
    }

    public async Task<IEnumerable<LiquidacionesComisionVentaDetalle>> SearchAsync(LiquidacionesComisionVentaDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<LiquidacionesComisionVentaDetalle>(SP.SCHEMANetworking.LiquidacionesComisionVentaDetalle.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(LiquidacionesComisionVentaDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_LIQUIDACION_COMISION_VENTA", dto.NUM_LIQUIDACION_COMISION_VENTA);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("NUM_REGISTRO_COMPROBANTE_PAGO", dto.NUM_REGISTRO_COMPROBANTE_PAGO);
        p.Add("NUM_SECUENCIA_COMPROBANTE_PAGO", dto.NUM_SECUENCIA_COMPROBANTE_PAGO);
        p.Add("FEC_VENTA", dto.FEC_VENTA);
        p.Add("FEC_ENTREGA", dto.FEC_ENTREGA);
        p.Add("COD_CATEGORIA_COMISION", dto.COD_CATEGORIA_COMISION);
        p.Add("COD_TIPO_CATEGORIA_COMISION", dto.COD_TIPO_CATEGORIA_COMISION);
        p.Add("COD_ENTIDAD_ASESOR_COMERCIAL", dto.COD_ENTIDAD_ASESOR_COMERCIAL);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("TAS_COMISION_VENTA", dto.TAS_COMISION_VENTA);
        p.Add("TAS_COMISON_REFERENCIADA", dto.TAS_COMISON_REFERENCIADA);
        p.Add("VAL_COMISION_VENTA", dto.VAL_COMISION_VENTA);
        p.Add("VAL_COMISON_REFERENCIADA", dto.VAL_COMISON_REFERENCIADA);
        p.Add("VAL_COMISION", dto.VAL_COMISION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.LiquidacionesComisionVentaDetalle.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.LiquidacionesComisionVentaDetalle.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.LiquidacionesComisionVentaDetalle.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(LiquidacionesComisionVentaDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_LIQUIDACION_COMISION_VENTA", dto.NUM_LIQUIDACION_COMISION_VENTA);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("NUM_REGISTRO_COMPROBANTE_PAGO", dto.NUM_REGISTRO_COMPROBANTE_PAGO);
        p.Add("NUM_SECUENCIA_COMPROBANTE_PAGO", dto.NUM_SECUENCIA_COMPROBANTE_PAGO);
        p.Add("FEC_VENTA", dto.FEC_VENTA);
        p.Add("FEC_ENTREGA", dto.FEC_ENTREGA);
        p.Add("COD_CATEGORIA_COMISION", dto.COD_CATEGORIA_COMISION);
        p.Add("COD_TIPO_CATEGORIA_COMISION", dto.COD_TIPO_CATEGORIA_COMISION);
        p.Add("COD_ENTIDAD_ASESOR_COMERCIAL", dto.COD_ENTIDAD_ASESOR_COMERCIAL);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("TAS_COMISION_VENTA", dto.TAS_COMISION_VENTA);
        p.Add("TAS_COMISON_REFERENCIADA", dto.TAS_COMISON_REFERENCIADA);
        p.Add("VAL_COMISION_VENTA", dto.VAL_COMISION_VENTA);
        p.Add("VAL_COMISON_REFERENCIADA", dto.VAL_COMISON_REFERENCIADA);
        p.Add("VAL_COMISION", dto.VAL_COMISION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.LiquidacionesComisionVentaDetalle.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.LiquidacionesComisionVentaDetalle.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.LiquidacionesComisionVentaDetalle.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_LIQUIDACION_COMISION_VENTA, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.LiquidacionesComisionVentaDetalle.Delete, new { NUM_LIQUIDACION_COMISION_VENTA = NUM_LIQUIDACION_COMISION_VENTA, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.LiquidacionesComisionVentaDetalle.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.LiquidacionesComisionVentaDetalle.Delete, new { NUM_LIQUIDACION_COMISION_VENTA = NUM_LIQUIDACION_COMISION_VENTA, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}