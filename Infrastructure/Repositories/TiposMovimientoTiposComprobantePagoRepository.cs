using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposMovimientoTiposComprobantePagoRepository : ITiposMovimientoTiposComprobantePagoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposMovimientoTiposComprobantePagoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposMovimientoTiposComprobantePago>> ShowallAsync(string codClaseMovimiento, string codTipoMovimiento)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposMovimientoTiposComprobantePago>(
            SP.SCHEMALogistica.TiposMovimientoTiposComprobantePago.Showall,
            new { COD_CLASE_MOVIMIENTO = codClaseMovimiento, COD_TIPO_MOVIMIENTO = codTipoMovimiento },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposMovimientoTiposComprobantePago?> ShowbyidAsync(string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO, string COD_TIPO_COMPROBANTE_PAGO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposMovimientoTiposComprobantePago>(
            SP.SCHEMALogistica.TiposMovimientoTiposComprobantePago.Showbyid,
            new { COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO, COD_TIPO_COMPROBANTE_PAGO },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string codClaseMovimiento, string codTipoMovimiento)
    {
        using var conn = _db.CreateConnection();
        // Columnas explícitas, no por convención de prefijo: el SHOWCOMBOX de esta tabla
        // arranca con a.COD_CLASE_MOVIMIENTO, así que "la primera COD_*" sería la clase y no
        // el comprobante. El combo mandaría la clase como identificador y reventaría recién al
        // grabar, con un FK contra un valor que nunca fue la clave.
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.TiposMovimientoTiposComprobantePago.Showcombox,
            "COD_TIPO_COMPROBANTE_PAGO", "DES_TIPO_COMPROBANTE_PAGO",
            new { COD_CLASE_MOVIMIENTO = codClaseMovimiento, COD_TIPO_MOVIMIENTO = codTipoMovimiento });
    }

    public async Task<IEnumerable<TiposMovimientoTiposComprobantePago>> SearchAsync(string codClaseMovimiento, string codTipoMovimiento, TiposMovimientoTiposComprobantePagoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposMovimientoTiposComprobantePago>(
            SP.SCHEMALogistica.TiposMovimientoTiposComprobantePago.Search,
            new { COD_CLASE_MOVIMIENTO = codClaseMovimiento, COD_TIPO_MOVIMIENTO = codTipoMovimiento, SEARCH = dto.Search ?? string.Empty },
            commandType: CommandType.StoredProcedure);
    }

    public Task<WriteResult> InsertAsync(TiposMovimientoTiposComprobantePagoInsertDto dto, string codUsuario, string codEstacion)
        => EjecutarAsync(SP.SCHEMALogistica.TiposMovimientoTiposComprobantePago.Insert, dto, codUsuario, codEstacion);

    public Task<WriteResult> UpdateAsync(TiposMovimientoTiposComprobantePagoInsertDto dto, string codUsuario, string codEstacion)
        => EjecutarAsync(SP.SCHEMALogistica.TiposMovimientoTiposComprobantePago.Update, dto, codUsuario, codEstacion);

    /// <summary>Alta y modificación mandan los mismos parámetros al mismo tipo de SP; lo único
    /// que cambia es cuál. Un solo método evita que los dos se desincronicen el día que la
    /// tabla gane una columna.</summary>
    private async Task<WriteResult> EjecutarAsync(string sp, TiposMovimientoTiposComprobantePagoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_MOVIMIENTO", dto.COD_CLASE_MOVIMIENTO);
        p.Add("COD_TIPO_MOVIMIENTO", dto.COD_TIPO_MOVIMIENTO);
        p.Add("COD_TIPO_COMPROBANTE_PAGO", dto.COD_TIPO_COMPROBANTE_PAGO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("FLG_CONTROL_CORRELATIVO", dto.FLG_CONTROL_CORRELATIVO);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(sp, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = sp,
                SqlSentence = SqlSentenceHelper.Build(sp, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO, string COD_TIPO_COMPROBANTE_PAGO)
    {
        using var conn = _db.CreateConnection();
        var p = new { COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO, COD_TIPO_COMPROBANTE_PAGO };
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMALogistica.TiposMovimientoTiposComprobantePago.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.TiposMovimientoTiposComprobantePago.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposMovimientoTiposComprobantePago.Delete, p)
            };
        }
    }
}
