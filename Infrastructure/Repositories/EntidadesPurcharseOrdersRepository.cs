using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EntidadesPurcharseOrdersRepository : IEntidadesPurcharseOrdersRepository
{
    private readonly IDbConnectionFactory _db;
    public EntidadesPurcharseOrdersRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EntidadesPurcharseOrders>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesPurcharseOrders>(SP.SCHEMAComercial.EntidadesPurcharseOrders.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EntidadesPurcharseOrders?> ShowbyidAsync(decimal COD_ENTIDAD, string NUM_PURCHASE_ORDER)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EntidadesPurcharseOrders>(SP.SCHEMAComercial.EntidadesPurcharseOrders.Showbyid, new { COD_ENTIDAD = COD_ENTIDAD, NUM_PURCHASE_ORDER = NUM_PURCHASE_ORDER }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.EntidadesPurcharseOrders.Showcombox);
    }

    public async Task<IEnumerable<EntidadesPurcharseOrders>> SearchAsync(EntidadesPurcharseOrdersSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesPurcharseOrders>(SP.SCHEMAComercial.EntidadesPurcharseOrders.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EntidadesPurcharseOrdersInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("NUM_PURCHASE_ORDER", dto.NUM_PURCHASE_ORDER);
        p.Add("COD_DISENO", dto.COD_DISENO);
        p.Add("DES_DISENO_ENTIDAD", dto.DES_DISENO_ENTIDAD);
        p.Add("FEC_DISENO_ENTIDAD", dto.FEC_DISENO_ENTIDAD);
        p.Add("FLG_ITERAR_EMBALAJE", dto.FLG_ITERAR_EMBALAJE ?? false);
        p.Add("FLG_PURCHASE_ORDER_GENERICA", dto.FLG_PURCHASE_ORDER_GENERICA ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesPurcharseOrders.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesPurcharseOrders.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesPurcharseOrders.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EntidadesPurcharseOrdersUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("NUM_PURCHASE_ORDER", dto.NUM_PURCHASE_ORDER);
        p.Add("COD_DISENO", dto.COD_DISENO);
        p.Add("DES_DISENO_ENTIDAD", dto.DES_DISENO_ENTIDAD);
        p.Add("FEC_DISENO_ENTIDAD", dto.FEC_DISENO_ENTIDAD);
        p.Add("FLG_ITERAR_EMBALAJE", dto.FLG_ITERAR_EMBALAJE ?? false);
        p.Add("FLG_PURCHASE_ORDER_GENERICA", dto.FLG_PURCHASE_ORDER_GENERICA ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesPurcharseOrders.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesPurcharseOrders.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesPurcharseOrders.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, string NUM_PURCHASE_ORDER)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesPurcharseOrders.Delete, new { COD_ENTIDAD = COD_ENTIDAD, NUM_PURCHASE_ORDER = NUM_PURCHASE_ORDER }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesPurcharseOrders.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesPurcharseOrders.Delete, new { COD_ENTIDAD = COD_ENTIDAD, NUM_PURCHASE_ORDER = NUM_PURCHASE_ORDER })
        };
    }
}

}