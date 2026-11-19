using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposCentroCostoRepository : ITiposCentroCostoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposCentroCostoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposCentroCosto>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposCentroCosto>(SP.SCHEMAContabilidad.TiposCentroCosto.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposCentroCosto?> ShowbyidAsync(string COD_TIPO_CENTRO_COSTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposCentroCosto>(SP.SCHEMAContabilidad.TiposCentroCosto.Showbyid, new { COD_TIPO_CENTRO_COSTO = COD_TIPO_CENTRO_COSTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAContabilidad.TiposCentroCosto.Showcombox);
    }

    public async Task<IEnumerable<TiposCentroCosto>> SearchAsync(TiposCentroCostoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposCentroCosto>(SP.SCHEMAContabilidad.TiposCentroCosto.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposCentroCostoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CENTRO_COSTO", dto.COD_TIPO_CENTRO_COSTO);
        p.Add("DES_TIPO_CENTRO_COSTO", dto.DES_TIPO_CENTRO_COSTO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.TiposCentroCosto.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.TiposCentroCosto.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.TiposCentroCosto.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposCentroCostoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CENTRO_COSTO", dto.COD_TIPO_CENTRO_COSTO);
        p.Add("DES_TIPO_CENTRO_COSTO", dto.DES_TIPO_CENTRO_COSTO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.TiposCentroCosto.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.TiposCentroCosto.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.TiposCentroCosto.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_CENTRO_COSTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.TiposCentroCosto.Delete, new { COD_TIPO_CENTRO_COSTO = COD_TIPO_CENTRO_COSTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.TiposCentroCosto.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.TiposCentroCosto.Delete, new { COD_TIPO_CENTRO_COSTO = COD_TIPO_CENTRO_COSTO })
        };
    }
}

}