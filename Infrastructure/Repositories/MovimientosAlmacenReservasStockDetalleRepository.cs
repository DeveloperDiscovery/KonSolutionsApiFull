using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Enlace a nivel LINEA con una reserva. Reservas es el unico origen que enlaza en los dos niveles, y por eso se puede tomar una reserva entera o lineas sueltas.</summary>
public class MovimientosAlmacenReservasStockDetalleRepository : IMovimientosAlmacenReservasStockDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public MovimientosAlmacenReservasStockDetalleRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>El SHOWALL ya viene filtrado por movimiento: el SP lo recibe como parametro
    /// obligatorio. Por eso este modulo no necesita endpoints "por-cabecera" aparte.</summary>
    public async Task<IEnumerable<MovimientosAlmacenReservasStockDetalle>> ShowallAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_RESERVA_STOCK)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientosAlmacenReservasStockDetalle>(SP.SCHEMALogistica.MovimientosAlmacenReservasStockDetalle.Showall,
            new { NUM_MOVIMIENTO_ALMACEN, NUM_RESERVA_STOCK }, commandType: CommandType.StoredProcedure);
    }

    public async Task<MovimientosAlmacenReservasStockDetalle?> ShowbyidAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_RESERVA_STOCK, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<MovimientosAlmacenReservasStockDetalle>(SP.SCHEMALogistica.MovimientosAlmacenReservasStockDetalle.Showbyid,
            new { NUM_MOVIMIENTO_ALMACEN, NUM_RESERVA_STOCK, NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.MovimientosAlmacenReservasStockDetalle.Showcombox);
    }

    public async Task<IEnumerable<MovimientosAlmacenReservasStockDetalle>> SearchAsync(MovimientosAlmacenReservasStockDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientosAlmacenReservasStockDetalle>(SP.SCHEMALogistica.MovimientosAlmacenReservasStockDetalle.Search,
            new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(MovimientosAlmacenReservasStockDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("NUM_RESERVA_STOCK", dto.NUM_RESERVA_STOCK);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.MovimientosAlmacenReservasStockDetalle.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.MovimientosAlmacenReservasStockDetalle.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.MovimientosAlmacenReservasStockDetalle.Insert, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_RESERVA_STOCK, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", NUM_MOVIMIENTO_ALMACEN);
        p.Add("NUM_RESERVA_STOCK", NUM_RESERVA_STOCK);
        p.Add("NUM_SECUENCIA", NUM_SECUENCIA);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.MovimientosAlmacenReservasStockDetalle.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.MovimientosAlmacenReservasStockDetalle.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.MovimientosAlmacenReservasStockDetalle.Delete, p)
            };
        }
    }
}
