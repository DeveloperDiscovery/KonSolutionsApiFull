using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SubCanalesVentaRepository : ISubCanalesVentaRepository
{
    private readonly IDbConnectionFactory _db;
    public SubCanalesVentaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<SubCanalesVenta>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SubCanalesVenta>(SP.SCHEMAComercial.SubCanalesVenta.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<SubCanalesVenta?> ShowbyidAsync(int NUM_UNIDAD_NEGOCIO, int NUM_CANAL_VENTA, int NUM_SUB_CANAL_VENTA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<SubCanalesVenta>(SP.SCHEMAComercial.SubCanalesVenta.Showbyid, new { NUM_UNIDAD_NEGOCIO = NUM_UNIDAD_NEGOCIO, NUM_CANAL_VENTA = NUM_CANAL_VENTA, NUM_SUB_CANAL_VENTA = NUM_SUB_CANAL_VENTA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.SubCanalesVenta.Showcombox, "NUM_UNIDAD_NEGOCIO", "DES_CANAL_VENTA");
    }

    public async Task<IEnumerable<SubCanalesVenta>> SearchAsync(SubCanalesVentaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SubCanalesVenta>(SP.SCHEMAComercial.SubCanalesVenta.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(SubCanalesVentaInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_UNIDAD_NEGOCIO", dto.NUM_UNIDAD_NEGOCIO);
        p.Add("NUM_CANAL_VENTA", dto.NUM_CANAL_VENTA);
        p.Add("NUM_SUB_CANAL_VENTA", dto.NUM_SUB_CANAL_VENTA);
        p.Add("FLG_PRODUCTO", dto.FLG_PRODUCTO ?? false);
        p.Add("FLG_COMBO", dto.FLG_COMBO ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.SubCanalesVenta.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.SubCanalesVenta.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.SubCanalesVenta.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(SubCanalesVentaUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_UNIDAD_NEGOCIO", dto.NUM_UNIDAD_NEGOCIO);
        p.Add("NUM_CANAL_VENTA", dto.NUM_CANAL_VENTA);
        p.Add("NUM_SUB_CANAL_VENTA", dto.NUM_SUB_CANAL_VENTA);
        p.Add("FLG_PRODUCTO", dto.FLG_PRODUCTO ?? false);
        p.Add("FLG_COMBO", dto.FLG_COMBO ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.SubCanalesVenta.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.SubCanalesVenta.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.SubCanalesVenta.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_UNIDAD_NEGOCIO, int NUM_CANAL_VENTA, int NUM_SUB_CANAL_VENTA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.SubCanalesVenta.Delete, new { NUM_UNIDAD_NEGOCIO = NUM_UNIDAD_NEGOCIO, NUM_CANAL_VENTA = NUM_CANAL_VENTA, NUM_SUB_CANAL_VENTA = NUM_SUB_CANAL_VENTA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.SubCanalesVenta.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.SubCanalesVenta.Delete, new { NUM_UNIDAD_NEGOCIO = NUM_UNIDAD_NEGOCIO, NUM_CANAL_VENTA = NUM_CANAL_VENTA, NUM_SUB_CANAL_VENTA = NUM_SUB_CANAL_VENTA })
        };
    }
}

}