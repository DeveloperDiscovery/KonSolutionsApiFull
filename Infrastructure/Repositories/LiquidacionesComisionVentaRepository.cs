using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Networking;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class LiquidacionesComisionVentaRepository : ILiquidacionesComisionVentaRepository
{
    private readonly IDbConnectionFactory _db;
    public LiquidacionesComisionVentaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<LiquidacionesComisionVenta>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<LiquidacionesComisionVenta>(SP.SCHEMANetworking.LiquidacionesComisionVenta.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<LiquidacionesComisionVenta?> ShowbyidAsync(int NUM_LIQUIDACION_COMISION_VENTA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<LiquidacionesComisionVenta>(SP.SCHEMANetworking.LiquidacionesComisionVenta.Showbyid, new { NUM_LIQUIDACION_COMISION_VENTA = NUM_LIQUIDACION_COMISION_VENTA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANetworking.LiquidacionesComisionVenta.Showcombox, "NUM_LIQUIDACION_COMISION_VENTA", "DES_OBESRVACION");
    }

    public async Task<IEnumerable<LiquidacionesComisionVenta>> SearchAsync(LiquidacionesComisionVentaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<LiquidacionesComisionVenta>(SP.SCHEMANetworking.LiquidacionesComisionVenta.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(LiquidacionesComisionVentaInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_LIQUIDACION_COMISION_VENTA", dto.NUM_LIQUIDACION_COMISION_VENTA);
        p.Add("NUM_PERIODO_LIQUIDACION", dto.NUM_PERIODO_LIQUIDACION);
        p.Add("NUM_GRUPO_NETWORKING", dto.NUM_GRUPO_NETWORKING);
        p.Add("COD_ENTIDAD_ASESOR_COMERCIAL", dto.COD_ENTIDAD_ASESOR_COMERCIAL);
        p.Add("FEC_COMISION_VENTA", dto.FEC_COMISION_VENTA);
        p.Add("VAL_COMISION", dto.VAL_COMISION);
        p.Add("DES_OBESRVACION", dto.DES_OBESRVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.LiquidacionesComisionVenta.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.LiquidacionesComisionVenta.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.LiquidacionesComisionVenta.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(LiquidacionesComisionVentaUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_LIQUIDACION_COMISION_VENTA", dto.NUM_LIQUIDACION_COMISION_VENTA);
        p.Add("NUM_PERIODO_LIQUIDACION", dto.NUM_PERIODO_LIQUIDACION);
        p.Add("NUM_GRUPO_NETWORKING", dto.NUM_GRUPO_NETWORKING);
        p.Add("COD_ENTIDAD_ASESOR_COMERCIAL", dto.COD_ENTIDAD_ASESOR_COMERCIAL);
        p.Add("FEC_COMISION_VENTA", dto.FEC_COMISION_VENTA);
        p.Add("VAL_COMISION", dto.VAL_COMISION);
        p.Add("DES_OBESRVACION", dto.DES_OBESRVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.LiquidacionesComisionVenta.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.LiquidacionesComisionVenta.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.LiquidacionesComisionVenta.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_LIQUIDACION_COMISION_VENTA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.LiquidacionesComisionVenta.Delete, new { NUM_LIQUIDACION_COMISION_VENTA = NUM_LIQUIDACION_COMISION_VENTA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.LiquidacionesComisionVenta.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.LiquidacionesComisionVenta.Delete, new { NUM_LIQUIDACION_COMISION_VENTA = NUM_LIQUIDACION_COMISION_VENTA })
        };
    }
}

}