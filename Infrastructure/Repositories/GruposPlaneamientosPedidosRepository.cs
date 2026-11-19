using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class GruposPlaneamientosPedidosRepository : IGruposPlaneamientosPedidosRepository
{
    private readonly IDbConnectionFactory _db;
    public GruposPlaneamientosPedidosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<GruposPlaneamientosPedidos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GruposPlaneamientosPedidos>(SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<GruposPlaneamientosPedidos?> ShowbyidAsync(int COD_GRUPO_PLANEAMIENTO, int NUM_PEDIDO_COMERCIAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<GruposPlaneamientosPedidos>(SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidos.Showbyid, new { COD_GRUPO_PLANEAMIENTO = COD_GRUPO_PLANEAMIENTO, NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidos.Showcombox);
    }

    public async Task<IEnumerable<GruposPlaneamientosPedidos>> SearchAsync(GruposPlaneamientosPedidosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GruposPlaneamientosPedidos>(SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(GruposPlaneamientosPedidosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_GRUPO_PLANEAMIENTO", dto.COD_GRUPO_PLANEAMIENTO);
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidos.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_GRUPO_PLANEAMIENTO, int NUM_PEDIDO_COMERCIAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidos.Delete, new { COD_GRUPO_PLANEAMIENTO = COD_GRUPO_PLANEAMIENTO, NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.GruposPlaneamientosPedidos.Delete, new { COD_GRUPO_PLANEAMIENTO = COD_GRUPO_PLANEAMIENTO, NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL })
        };
    }
}

}