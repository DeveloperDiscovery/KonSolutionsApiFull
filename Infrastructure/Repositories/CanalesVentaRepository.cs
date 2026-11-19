using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CanalesVentaRepository : ICanalesVentaRepository
{
    private readonly IDbConnectionFactory _db;
    public CanalesVentaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CanalesVenta>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CanalesVenta>(SP.SCHEMAComercial.CanalesVenta.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CanalesVenta?> ShowbyidAsync(int NUM_UNIDAD_NEGOCIO, int NUM_CANAL_VENTA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CanalesVenta>(SP.SCHEMAComercial.CanalesVenta.Showbyid, new { NUM_UNIDAD_NEGOCIO = NUM_UNIDAD_NEGOCIO, NUM_CANAL_VENTA = NUM_CANAL_VENTA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.CanalesVenta.Showcombox, "NUM_UNIDAD_NEGOCIO", "DES_CANAL_VENTA");
    }

    public async Task<IEnumerable<CanalesVenta>> SearchAsync(CanalesVentaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CanalesVenta>(SP.SCHEMAComercial.CanalesVenta.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CanalesVentaInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_UNIDAD_NEGOCIO", dto.NUM_UNIDAD_NEGOCIO);
        p.Add("NUM_CANAL_VENTA", dto.NUM_CANAL_VENTA);
        p.Add("DES_CANAL_VENTA", dto.DES_CANAL_VENTA);
        p.Add("FLG_PRODUCTO", dto.FLG_PRODUCTO ?? false);
        p.Add("FLG_COMBO", dto.FLG_COMBO ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.CanalesVenta.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.CanalesVenta.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.CanalesVenta.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CanalesVentaUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_UNIDAD_NEGOCIO", dto.NUM_UNIDAD_NEGOCIO);
        p.Add("NUM_CANAL_VENTA", dto.NUM_CANAL_VENTA);
        p.Add("DES_CANAL_VENTA", dto.DES_CANAL_VENTA);
        p.Add("FLG_PRODUCTO", dto.FLG_PRODUCTO ?? false);
        p.Add("FLG_COMBO", dto.FLG_COMBO ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.CanalesVenta.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.CanalesVenta.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.CanalesVenta.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_UNIDAD_NEGOCIO, int NUM_CANAL_VENTA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.CanalesVenta.Delete, new { NUM_UNIDAD_NEGOCIO = NUM_UNIDAD_NEGOCIO, NUM_CANAL_VENTA = NUM_CANAL_VENTA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.CanalesVenta.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.CanalesVenta.Delete, new { NUM_UNIDAD_NEGOCIO = NUM_UNIDAD_NEGOCIO, NUM_CANAL_VENTA = NUM_CANAL_VENTA })
        };
    }
}

}