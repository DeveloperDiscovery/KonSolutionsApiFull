using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposAutorizacionRepository : ITiposAutorizacionRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposAutorizacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposAutorizacion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposAutorizacion>(SP.SCHEMAEntorno.TiposAutorizacion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposAutorizacion?> ShowbyidAsync(string COD_TIPO_AUTORIZACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposAutorizacion>(SP.SCHEMAEntorno.TiposAutorizacion.Showbyid, new { COD_TIPO_AUTORIZACION = COD_TIPO_AUTORIZACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEntorno.TiposAutorizacion.Showcombox);
    }

    public async Task<IEnumerable<TiposAutorizacion>> SearchAsync(TiposAutorizacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposAutorizacion>(SP.SCHEMAEntorno.TiposAutorizacion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposAutorizacionInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_AUTORIZACION", dto.COD_TIPO_AUTORIZACION);
        p.Add("DES_TIPO_AUTORIZACION", dto.DES_TIPO_AUTORIZACION);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.TiposAutorizacion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.TiposAutorizacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.TiposAutorizacion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposAutorizacionUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_AUTORIZACION", dto.COD_TIPO_AUTORIZACION);
        p.Add("DES_TIPO_AUTORIZACION", dto.DES_TIPO_AUTORIZACION);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.TiposAutorizacion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.TiposAutorizacion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.TiposAutorizacion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_AUTORIZACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.TiposAutorizacion.Delete, new { COD_TIPO_AUTORIZACION = COD_TIPO_AUTORIZACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.TiposAutorizacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.TiposAutorizacion.Delete, new { COD_TIPO_AUTORIZACION = COD_TIPO_AUTORIZACION })
        };
    }
}

}