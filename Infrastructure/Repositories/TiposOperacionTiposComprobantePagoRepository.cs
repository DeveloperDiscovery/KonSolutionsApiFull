using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposOperacionTiposComprobantePagoRepository : ITiposOperacionTiposComprobantePagoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposOperacionTiposComprobantePagoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposOperacionTiposComprobantePago>> ShowallAsync(string codClaseOperacion, string codTipoOperacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposOperacionTiposComprobantePago>(SP.SCHEMAComercial.TiposOperacionTiposComprobantePago.Showall,
            new { COD_CLASE_OPERACION = codClaseOperacion, COD_TIPO_OPERACION = codTipoOperacion }, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposOperacionTiposComprobantePago?> ShowbyidAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_COMPROBANTE_PAGO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposOperacionTiposComprobantePago>(SP.SCHEMAComercial.TiposOperacionTiposComprobantePago.Showbyid, new { COD_CLASE_OPERACION = COD_CLASE_OPERACION, COD_TIPO_OPERACION = COD_TIPO_OPERACION, COD_TIPO_COMPROBANTE_PAGO = COD_TIPO_COMPROBANTE_PAGO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.TiposOperacionTiposComprobantePago.Showcombox);
    }

    public async Task<IEnumerable<TiposOperacionTiposComprobantePago>> SearchAsync(string codClaseOperacion, string codTipoOperacion, TiposOperacionTiposComprobantePagoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposOperacionTiposComprobantePago>(SP.SCHEMAComercial.TiposOperacionTiposComprobantePago.Search,
            new { COD_CLASE_OPERACION = codClaseOperacion, COD_TIPO_OPERACION = codTipoOperacion, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposOperacionTiposComprobantePagoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("COD_TIPO_COMPROBANTE_PAGO", dto.COD_TIPO_COMPROBANTE_PAGO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposOperacionTiposComprobantePago.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposOperacionTiposComprobantePago.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposOperacionTiposComprobantePago.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_COMPROBANTE_PAGO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposOperacionTiposComprobantePago.Delete, new { COD_CLASE_OPERACION = COD_CLASE_OPERACION, COD_TIPO_OPERACION = COD_TIPO_OPERACION, COD_TIPO_COMPROBANTE_PAGO = COD_TIPO_COMPROBANTE_PAGO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposOperacionTiposComprobantePago.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposOperacionTiposComprobantePago.Delete, new { COD_CLASE_OPERACION = COD_CLASE_OPERACION, COD_TIPO_OPERACION = COD_TIPO_OPERACION, COD_TIPO_COMPROBANTE_PAGO = COD_TIPO_COMPROBANTE_PAGO })
        };
    }
}

}