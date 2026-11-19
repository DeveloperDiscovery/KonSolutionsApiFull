using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class GruposPlaneamientosPedidosDetalleRepository : IGruposPlaneamientosPedidosDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public GruposPlaneamientosPedidosDetalleRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<GruposPlaneamientosPedidosDetalle>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GruposPlaneamientosPedidosDetalle>(SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidosDetalle.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<GruposPlaneamientosPedidosDetalle?> ShowbyidAsync(int COD_GRUPO_PLANEAMIENTO, int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<GruposPlaneamientosPedidosDetalle>(SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidosDetalle.Showbyid, new { COD_GRUPO_PLANEAMIENTO = COD_GRUPO_PLANEAMIENTO, NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidosDetalle.Showcombox);
    }

    public async Task<IEnumerable<GruposPlaneamientosPedidosDetalle>> SearchAsync(GruposPlaneamientosPedidosDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GruposPlaneamientosPedidosDetalle>(SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidosDetalle.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(GruposPlaneamientosPedidosDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_GRUPO_PLANEAMIENTO", dto.COD_GRUPO_PLANEAMIENTO);
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_DISENO", dto.COD_DISENO);
        p.Add("NUM_VERSION", dto.NUM_VERSION);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidosDetalle.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidosDetalle.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidosDetalle.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(GruposPlaneamientosPedidosDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_GRUPO_PLANEAMIENTO", dto.COD_GRUPO_PLANEAMIENTO);
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_DISENO", dto.COD_DISENO);
        p.Add("NUM_VERSION", dto.NUM_VERSION);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidosDetalle.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidosDetalle.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidosDetalle.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_GRUPO_PLANEAMIENTO, int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidosDetalle.Delete, new { COD_GRUPO_PLANEAMIENTO = COD_GRUPO_PLANEAMIENTO, NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidosDetalle.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidosDetalle.Delete, new { COD_GRUPO_PLANEAMIENTO = COD_GRUPO_PLANEAMIENTO, NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}