using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposRequerimientoRepository : ITiposRequerimientoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposRequerimientoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposRequerimiento>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposRequerimiento>(SP.SCHEMALogistica.TiposRequerimiento.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposRequerimiento?> ShowbyidAsync(string COD_TIPO_REQUERIMIENTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposRequerimiento>(SP.SCHEMALogistica.TiposRequerimiento.Showbyid, new { COD_TIPO_REQUERIMIENTO = COD_TIPO_REQUERIMIENTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.TiposRequerimiento.Showcombox);
    }

    public async Task<IEnumerable<TiposRequerimiento>> SearchAsync(TiposRequerimientoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposRequerimiento>(SP.SCHEMALogistica.TiposRequerimiento.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposRequerimientoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_REQUERIMIENTO", dto.COD_TIPO_REQUERIMIENTO);
        p.Add("DES_TIPO_REQUERIMIENTO", dto.DES_TIPO_REQUERIMIENTO);
        p.Add("COD_TIPO_RESERVA", dto.COD_TIPO_RESERVA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("FLG_TRANSFERENCIA", dto.FLG_TRANSFERENCIA ?? false);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION ?? false);
        p.Add("FLG_COMPRA", dto.FLG_COMPRA ?? false);
        p.Add("FLG_CONSUMO_INTERNO", dto.FLG_CONSUMO_INTERNO ?? false);
        p.Add("FLG_REPOSICION", dto.FLG_REPOSICION ?? false);
        p.Add("FLG_VENTA", dto.FLG_VENTA ?? false);
        p.Add("FLG_CONSUMO_PRODUCCION", dto.FLG_CONSUMO_PRODUCCION ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposRequerimiento.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposRequerimiento.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposRequerimiento.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposRequerimientoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_REQUERIMIENTO", dto.COD_TIPO_REQUERIMIENTO);
        p.Add("DES_TIPO_REQUERIMIENTO", dto.DES_TIPO_REQUERIMIENTO);
        p.Add("COD_TIPO_RESERVA", dto.COD_TIPO_RESERVA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("FLG_TRANSFERENCIA", dto.FLG_TRANSFERENCIA ?? false);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION ?? false);
        p.Add("FLG_COMPRA", dto.FLG_COMPRA ?? false);
        p.Add("FLG_CONSUMO_INTERNO", dto.FLG_CONSUMO_INTERNO ?? false);
        p.Add("FLG_REPOSICION", dto.FLG_REPOSICION ?? false);
        p.Add("FLG_VENTA", dto.FLG_VENTA ?? false);
        p.Add("FLG_CONSUMO_PRODUCCION", dto.FLG_CONSUMO_PRODUCCION ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposRequerimiento.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposRequerimiento.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposRequerimiento.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_REQUERIMIENTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposRequerimiento.Delete, new { COD_TIPO_REQUERIMIENTO = COD_TIPO_REQUERIMIENTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposRequerimiento.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposRequerimiento.Delete, new { COD_TIPO_REQUERIMIENTO = COD_TIPO_REQUERIMIENTO })
        };
    }
}

}