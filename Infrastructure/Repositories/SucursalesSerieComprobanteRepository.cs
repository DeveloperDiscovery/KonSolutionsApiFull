using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SucursalesSerieComprobanteRepository : ISucursalesSerieComprobanteRepository
{
    private readonly IDbConnectionFactory _db;
    public SucursalesSerieComprobanteRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>Las series de UN tipo de comprobante en UNA sucursal.
    ///
    /// Los dos parámetros no son opcionales: el SP los exige. Esto se llamaba sin ninguno, así
    /// que cualquier lectura moría con el error 201 ("expects parameter @COD_SUCURSAL") antes
    /// de devolver una sola fila. Nunca se notó porque ninguna pantalla lo usaba todavía.</summary>
    public async Task<IEnumerable<SucursalesSerieComprobante>> ShowallAsync(string codSucursal, string codTipoComprobantePago)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SucursalesSerieComprobante>(
            SP.SCHEMAEstandar.SucursalesSerieComprobante.Showall,
            new { COD_SUCURSAL = codSucursal, COD_TIPO_COMPROBANTE_PAGO = codTipoComprobantePago },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<SucursalesSerieComprobante?> ShowbyidAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO, int NUM_SERIE_COMPROBANTE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<SucursalesSerieComprobante>(SP.SCHEMAEstandar.SucursalesSerieComprobante.Showbyid, new { COD_SUCURSAL = COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO = COD_TIPO_COMPROBANTE_PAGO, NUM_SERIE_COMPROBANTE = NUM_SERIE_COMPROBANTE }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Columnas explícitas: la primera COD_* de este SP es COD_SUCURSAL, o sea la
    /// clave del abuelo. Por convención, el combo mostraría la sucursal y mandaría su código
    /// como si fuera el de la serie.</summary>
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string codSucursal, string codTipoComprobantePago)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.SucursalesSerieComprobante.Showcombox,
            "NUM_SERIE_COMPROBANTE", "DES_TIPO_COMPROBANTE_PAGO",
            new { COD_SUCURSAL = codSucursal, COD_TIPO_COMPROBANTE_PAGO = codTipoComprobantePago });
    }

    public async Task<IEnumerable<SucursalesSerieComprobante>> SearchAsync(string codSucursal, string codTipoComprobantePago, SucursalesSerieComprobanteSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SucursalesSerieComprobante>(
            SP.SCHEMAEstandar.SucursalesSerieComprobante.Search,
            new { COD_SUCURSAL = codSucursal, COD_TIPO_COMPROBANTE_PAGO = codTipoComprobantePago, SEARCH = dto.Search ?? string.Empty },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(SucursalesSerieComprobanteInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_TIPO_COMPROBANTE_PAGO", dto.COD_TIPO_COMPROBANTE_PAGO);
        p.Add("NUM_SERIE_COMPROBANTE", dto.NUM_SERIE_COMPROBANTE);
        p.Add("NUM_ULTIMO_CORRELATIVO", dto.NUM_ULTIMO_CORRELATIVO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.SucursalesSerieComprobante.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.SucursalesSerieComprobante.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.SucursalesSerieComprobante.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(SucursalesSerieComprobanteUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_TIPO_COMPROBANTE_PAGO", dto.COD_TIPO_COMPROBANTE_PAGO);
        p.Add("NUM_SERIE_COMPROBANTE", dto.NUM_SERIE_COMPROBANTE);
        p.Add("NUM_ULTIMO_CORRELATIVO", dto.NUM_ULTIMO_CORRELATIVO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.SucursalesSerieComprobante.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.SucursalesSerieComprobante.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.SucursalesSerieComprobante.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO, int NUM_SERIE_COMPROBANTE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.SucursalesSerieComprobante.Delete, new { COD_SUCURSAL = COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO = COD_TIPO_COMPROBANTE_PAGO, NUM_SERIE_COMPROBANTE = NUM_SERIE_COMPROBANTE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.SucursalesSerieComprobante.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.SucursalesSerieComprobante.Delete, new { COD_SUCURSAL = COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO = COD_TIPO_COMPROBANTE_PAGO, NUM_SERIE_COMPROBANTE = NUM_SERIE_COMPROBANTE })
        };
    }
}

}