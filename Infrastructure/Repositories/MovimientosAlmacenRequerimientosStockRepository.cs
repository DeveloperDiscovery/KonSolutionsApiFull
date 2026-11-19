using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Enlace entre un movimiento y los requerimientos de stock que atiende. Solo a nivel documento: este origen no tiene enlace por linea.</summary>
public class MovimientosAlmacenRequerimientosStockRepository : IMovimientosAlmacenRequerimientosStockRepository
{
    private readonly IDbConnectionFactory _db;
    public MovimientosAlmacenRequerimientosStockRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>El SHOWALL ya viene filtrado por movimiento: el SP lo recibe como parametro
    /// obligatorio. Por eso este modulo no necesita endpoints "por-cabecera" aparte.</summary>
    public async Task<IEnumerable<MovimientosAlmacenRequerimientosStock>> ShowallAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientosAlmacenRequerimientosStock>(SP.SCHEMALogistica.MovimientosAlmacenRequerimientosStock.Showall,
            new { NUM_MOVIMIENTO_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    public async Task<MovimientosAlmacenRequerimientosStock?> ShowbyidAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_REQUERIMIENTO_STOCK)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<MovimientosAlmacenRequerimientosStock>(SP.SCHEMALogistica.MovimientosAlmacenRequerimientosStock.Showbyid,
            new { NUM_MOVIMIENTO_ALMACEN, NUM_REQUERIMIENTO_STOCK }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.MovimientosAlmacenRequerimientosStock.Showcombox);
    }

    public async Task<IEnumerable<MovimientosAlmacenRequerimientosStock>> SearchAsync(MovimientosAlmacenRequerimientosStockSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientosAlmacenRequerimientosStock>(SP.SCHEMALogistica.MovimientosAlmacenRequerimientosStock.Search,
            new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(MovimientosAlmacenRequerimientosStockInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("NUM_REQUERIMIENTO_STOCK", dto.NUM_REQUERIMIENTO_STOCK);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.MovimientosAlmacenRequerimientosStock.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.MovimientosAlmacenRequerimientosStock.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.MovimientosAlmacenRequerimientosStock.Insert, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_REQUERIMIENTO_STOCK)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", NUM_MOVIMIENTO_ALMACEN);
        p.Add("NUM_REQUERIMIENTO_STOCK", NUM_REQUERIMIENTO_STOCK);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.MovimientosAlmacenRequerimientosStock.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.MovimientosAlmacenRequerimientosStock.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.MovimientosAlmacenRequerimientosStock.Delete, p)
            };
        }
    }
}
