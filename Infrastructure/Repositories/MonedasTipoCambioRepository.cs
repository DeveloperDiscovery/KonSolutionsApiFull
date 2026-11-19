using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class MonedasTipoCambioRepository : IMonedasTipoCambioRepository
{
    private readonly IDbConnectionFactory _db;
    public MonedasTipoCambioRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<MonedasTipoCambio>> ShowallAsync(string? codMoneda = null)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_MONEDA", codMoneda);
        return await conn.QueryAsync<MonedasTipoCambio>(SP.SCHEMAEstandar.MonedasTipoCambio.Showall, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<MonedasTipoCambio?> ShowbyidAsync(string COD_MONEDA, DateTime FEC_TIPO_CAMBIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<MonedasTipoCambio>(SP.SCHEMAEstandar.MonedasTipoCambio.Showbyid, new { COD_MONEDA = COD_MONEDA, FEC_TIPO_CAMBIO = FEC_TIPO_CAMBIO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.MonedasTipoCambio.Showcombox);
    }

    public async Task<IEnumerable<MonedasTipoCambio>> SearchAsync(MonedasTipoCambioSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MonedasTipoCambio>(SP.SCHEMAEstandar.MonedasTipoCambio.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(MonedasTipoCambioInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("FEC_TIPO_CAMBIO", dto.FEC_TIPO_CAMBIO);
        p.Add("FAC_COMERCIAL_COMPRA", dto.FAC_COMERCIAL_COMPRA);
        p.Add("FAC_COMERCIAL_VENTA", dto.FAC_COMERCIAL_VENTA);
        p.Add("FAC_SUNAT_COMPRA", dto.FAC_SUNAT_COMPRA);
        p.Add("FAC_SUNAT_VENTA", dto.FAC_SUNAT_VENTA);
        p.Add("FAC_PARALELO_COMPRA", dto.FAC_PARALELO_COMPRA);
        p.Add("FAC_PARALELO_VENTA", dto.FAC_PARALELO_VENTA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.MonedasTipoCambio.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.MonedasTipoCambio.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.MonedasTipoCambio.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(MonedasTipoCambioUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("FEC_TIPO_CAMBIO", dto.FEC_TIPO_CAMBIO);
        p.Add("FAC_COMERCIAL_COMPRA", dto.FAC_COMERCIAL_COMPRA);
        p.Add("FAC_COMERCIAL_VENTA", dto.FAC_COMERCIAL_VENTA);
        p.Add("FAC_SUNAT_COMPRA", dto.FAC_SUNAT_COMPRA);
        p.Add("FAC_SUNAT_VENTA", dto.FAC_SUNAT_VENTA);
        p.Add("FAC_PARALELO_COMPRA", dto.FAC_PARALELO_COMPRA);
        p.Add("FAC_PARALELO_VENTA", dto.FAC_PARALELO_VENTA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.MonedasTipoCambio.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.MonedasTipoCambio.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.MonedasTipoCambio.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_MONEDA, DateTime FEC_TIPO_CAMBIO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.MonedasTipoCambio.Delete, new { COD_MONEDA = COD_MONEDA, FEC_TIPO_CAMBIO = FEC_TIPO_CAMBIO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.MonedasTipoCambio.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.MonedasTipoCambio.Delete, new { COD_MONEDA = COD_MONEDA, FEC_TIPO_CAMBIO = FEC_TIPO_CAMBIO })
        };
    }
}

    public async Task<TipoCambioResultado?> ObtenerAsync(string codMoneda, DateTime? fecTipoCambio)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TipoCambioResultado>(
            SP.SCHEMAEstandar.MonedasTipoCambio.Obtener,
            new { COD_MONEDA = codMoneda, FEC_TIPO_CAMBIO = fecTipoCambio },
            commandType: CommandType.StoredProcedure);
    }

}