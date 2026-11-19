using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Enlace entre un movimiento y las reservas de stock que atiende, a nivel documento.</summary>
public class MovimientosAlmacenReservasStockRepository : IMovimientosAlmacenReservasStockRepository
{
    private readonly IDbConnectionFactory _db;
    public MovimientosAlmacenReservasStockRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>El SHOWALL ya viene filtrado por movimiento: el SP lo recibe como parametro
    /// obligatorio. Por eso este modulo no necesita endpoints "por-cabecera" aparte.</summary>
    public async Task<IEnumerable<MovimientosAlmacenReservasStock>> ShowallAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientosAlmacenReservasStock>(SP.SCHEMALogistica.MovimientosAlmacenReservasStock.Showall,
            new { NUM_MOVIMIENTO_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    public async Task<MovimientosAlmacenReservasStock?> ShowbyidAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_RESERVA_STOCK)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<MovimientosAlmacenReservasStock>(SP.SCHEMALogistica.MovimientosAlmacenReservasStock.Showbyid,
            new { NUM_MOVIMIENTO_ALMACEN, NUM_RESERVA_STOCK }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.MovimientosAlmacenReservasStock.Showcombox);
    }

    public async Task<IEnumerable<MovimientosAlmacenReservasStock>> SearchAsync(MovimientosAlmacenReservasStockSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientosAlmacenReservasStock>(SP.SCHEMALogistica.MovimientosAlmacenReservasStock.Search,
            new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(MovimientosAlmacenReservasStockInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("NUM_RESERVA_STOCK", dto.NUM_RESERVA_STOCK);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.MovimientosAlmacenReservasStock.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.MovimientosAlmacenReservasStock.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.MovimientosAlmacenReservasStock.Insert, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_RESERVA_STOCK)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", NUM_MOVIMIENTO_ALMACEN);
        p.Add("NUM_RESERVA_STOCK", NUM_RESERVA_STOCK);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.MovimientosAlmacenReservasStock.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.MovimientosAlmacenReservasStock.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.MovimientosAlmacenReservasStock.Delete, p)
            };
        }
    }
}
