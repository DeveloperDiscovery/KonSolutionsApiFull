using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Enlace a nivel LINEA con una orden de compra.</summary>
public class MovimientosAlmacenPedidosComercialesDetalleRepository : IMovimientosAlmacenPedidosComercialesDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public MovimientosAlmacenPedidosComercialesDetalleRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>El SHOWALL ya viene filtrado por movimiento: el SP lo recibe como parametro
    /// obligatorio. Por eso este modulo no necesita endpoints "por-cabecera" aparte.</summary>
    public async Task<IEnumerable<MovimientosAlmacenPedidosComercialesDetalle>> ShowallAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_PEDIDO_COMERCIAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientosAlmacenPedidosComercialesDetalle>(SP.SCHEMALogistica.MovimientosAlmacenPedidosComercialesDetalle.Showall,
            new { NUM_MOVIMIENTO_ALMACEN, NUM_PEDIDO_COMERCIAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<MovimientosAlmacenPedidosComercialesDetalle?> ShowbyidAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<MovimientosAlmacenPedidosComercialesDetalle>(SP.SCHEMALogistica.MovimientosAlmacenPedidosComercialesDetalle.Showbyid,
            new { NUM_MOVIMIENTO_ALMACEN, NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.MovimientosAlmacenPedidosComercialesDetalle.Showcombox);
    }

    public async Task<IEnumerable<MovimientosAlmacenPedidosComercialesDetalle>> SearchAsync(MovimientosAlmacenPedidosComercialesDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientosAlmacenPedidosComercialesDetalle>(SP.SCHEMALogistica.MovimientosAlmacenPedidosComercialesDetalle.Search,
            new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(MovimientosAlmacenPedidosComercialesDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.MovimientosAlmacenPedidosComercialesDetalle.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.MovimientosAlmacenPedidosComercialesDetalle.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.MovimientosAlmacenPedidosComercialesDetalle.Insert, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", NUM_MOVIMIENTO_ALMACEN);
        p.Add("NUM_PEDIDO_COMERCIAL", NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_SECUENCIA", NUM_SECUENCIA);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.MovimientosAlmacenPedidosComercialesDetalle.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.MovimientosAlmacenPedidosComercialesDetalle.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.MovimientosAlmacenPedidosComercialesDetalle.Delete, p)
            };
        }
    }
}
