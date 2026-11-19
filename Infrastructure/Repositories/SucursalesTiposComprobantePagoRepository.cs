using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SucursalesTiposComprobantePagoRepository : ISucursalesTiposComprobantePagoRepository
{
    private readonly IDbConnectionFactory _db;
    public SucursalesTiposComprobantePagoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<SucursalesTiposComprobantePago>> ShowallAsync(string COD_SUCURSAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SucursalesTiposComprobantePago>(
            SP.SCHEMAEstandar.SucursalesTiposComprobantePago.Showall,
            new { COD_SUCURSAL = COD_SUCURSAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<SucursalesTiposComprobantePago?> ShowbyidAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<SucursalesTiposComprobantePago>(
            SP.SCHEMAEstandar.SucursalesTiposComprobantePago.Showbyid,
            new { COD_SUCURSAL = COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO = COD_TIPO_COMPROBANTE_PAGO },
            commandType: CommandType.StoredProcedure);
    }

    // El SP _SHOWCOMBOX devuelve las columnas de la tabla (COD_*/DES_*), no "Id"/"Texto":
    // la traducción a ComboboxItem se hace acá, en el repositorio.
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_SUCURSAL)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.SucursalesTiposComprobantePago.Showcombox,
            new { COD_SUCURSAL = COD_SUCURSAL });
    }

    public async Task<IEnumerable<SucursalesTiposComprobantePago>> SearchAsync(SucursalesTiposComprobantePagoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SucursalesTiposComprobantePago>(
            SP.SCHEMAEstandar.SucursalesTiposComprobantePago.Search,
            new { COD_SUCURSAL = dto.COD_SUCURSAL, SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(SucursalesTiposComprobantePagoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_TIPO_COMPROBANTE_PAGO", dto.COD_TIPO_COMPROBANTE_PAGO);
        p.Add("NUM_SERIE_COMPROBANTE", dto.NUM_SERIE_COMPROBANTE);
        p.Add("NUM_CORRELATIVO_ULTIMO", dto.NUM_CORRELATIVO_ULTIMO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAEstandar.SucursalesTiposComprobantePago.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.SucursalesTiposComprobantePago.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.SucursalesTiposComprobantePago.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAEstandar.SucursalesTiposComprobantePago.Delete,
            new { COD_SUCURSAL = COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO = COD_TIPO_COMPROBANTE_PAGO },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.SucursalesTiposComprobantePago.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.SucursalesTiposComprobantePago.Delete, new { COD_SUCURSAL = COD_SUCURSAL, COD_TIPO_COMPROBANTE_PAGO = COD_TIPO_COMPROBANTE_PAGO })
        };
    }
}
}
