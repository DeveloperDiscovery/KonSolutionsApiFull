using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Transportes asignados a un movimiento. Es la unica de las cinco tablas hijas que tiene datos propios —acarreo y observacion—, por eso lleva UPDATE.</summary>
public class MovimientosAlmacenTransporteRepository : IMovimientosAlmacenTransporteRepository
{
    private readonly IDbConnectionFactory _db;
    public MovimientosAlmacenTransporteRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>El SHOWALL ya viene filtrado por movimiento: el SP lo recibe como parametro
    /// obligatorio. Por eso este modulo no necesita endpoints "por-cabecera" aparte.</summary>
    public async Task<IEnumerable<MovimientosAlmacenTransporte>> ShowallAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientosAlmacenTransporte>(SP.SCHEMALogistica.MovimientosAlmacenTransporte.Showall,
            new { NUM_MOVIMIENTO_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    public async Task<MovimientosAlmacenTransporte?> ShowbyidAsync(decimal NUM_MOVIMIENTO_ALMACEN, string COD_TRANSPORTE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<MovimientosAlmacenTransporte>(SP.SCHEMALogistica.MovimientosAlmacenTransporte.Showbyid,
            new { NUM_MOVIMIENTO_ALMACEN, COD_TRANSPORTE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.MovimientosAlmacenTransporte.Showcombox);
    }

    public async Task<IEnumerable<MovimientosAlmacenTransporte>> SearchAsync(MovimientosAlmacenTransporteSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientosAlmacenTransporte>(SP.SCHEMALogistica.MovimientosAlmacenTransporte.Search,
            new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(MovimientosAlmacenTransporteInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("COD_TRANSPORTE", dto.COD_TRANSPORTE);
        p.Add("FLG_ACARREO", dto.FLG_ACARREO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.MovimientosAlmacenTransporte.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.MovimientosAlmacenTransporte.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.MovimientosAlmacenTransporte.Insert, p)
            };
        }
    }

    public async Task<WriteResult> UpdateAsync(MovimientosAlmacenTransporteUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("COD_TRANSPORTE", dto.COD_TRANSPORTE);
        p.Add("FLG_ACARREO", dto.FLG_ACARREO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.MovimientosAlmacenTransporte.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.MovimientosAlmacenTransporte.Update,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.MovimientosAlmacenTransporte.Update, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, string COD_TRANSPORTE)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", NUM_MOVIMIENTO_ALMACEN);
        p.Add("COD_TRANSPORTE", COD_TRANSPORTE);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.MovimientosAlmacenTransporte.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.MovimientosAlmacenTransporte.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.MovimientosAlmacenTransporte.Delete, p)
            };
        }
    }
}
