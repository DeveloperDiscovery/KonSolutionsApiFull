using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposCostoRepository : ITiposCostoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposCostoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposCosto>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposCosto>(SP.SCHEMACostos.TiposCosto.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposCosto?> ShowbyidAsync(string COD_TIPO_COSTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposCosto>(SP.SCHEMACostos.TiposCosto.Showbyid, new { COD_TIPO_COSTO = COD_TIPO_COSTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMACostos.TiposCosto.Showcombox);
    }

    public async Task<IEnumerable<TiposCosto>> SearchAsync(TiposCostoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposCosto>(SP.SCHEMACostos.TiposCosto.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposCostoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_COSTO", dto.COD_TIPO_COSTO);
        p.Add("DES_TIPO_COSTO", dto.DES_TIPO_COSTO);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.TiposCosto.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.TiposCosto.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.TiposCosto.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposCostoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_COSTO", dto.COD_TIPO_COSTO);
        p.Add("DES_TIPO_COSTO", dto.DES_TIPO_COSTO);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.TiposCosto.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.TiposCosto.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.TiposCosto.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_COSTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.TiposCosto.Delete, new { COD_TIPO_COSTO = COD_TIPO_COSTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.TiposCosto.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.TiposCosto.Delete, new { COD_TIPO_COSTO = COD_TIPO_COSTO })
        };
    }
}

}