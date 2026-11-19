using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Developer;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class MensajesRepository : IMensajesRepository
{
    private readonly IDbConnectionFactory _db;
    public MensajesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Mensajes>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Mensajes>(SP.SCHEMADeveloper.Mensajes.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Mensajes?> ShowbyidAsync(string COD_KEY_MENSAJE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Mensajes>(SP.SCHEMADeveloper.Mensajes.Showbyid, new { COD_KEY_MENSAJE = COD_KEY_MENSAJE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMADeveloper.Mensajes.Showcombox);
    }

    public async Task<IEnumerable<Mensajes>> SearchAsync(MensajesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Mensajes>(SP.SCHEMADeveloper.Mensajes.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(MensajesInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_KEY_MENSAJE", dto.COD_KEY_MENSAJE);
        p.Add("DES_MENSAJE", dto.DES_MENSAJE);
        p.Add("DES_ESQUEMA", dto.DES_ESQUEMA);
        p.Add("FLG_HABILITADO", dto.FLG_HABILITADO ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMADeveloper.Mensajes.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMADeveloper.Mensajes.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMADeveloper.Mensajes.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(MensajesUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_KEY_MENSAJE", dto.COD_KEY_MENSAJE);
        p.Add("DES_MENSAJE", dto.DES_MENSAJE);
        p.Add("DES_ESQUEMA", dto.DES_ESQUEMA);
        p.Add("FLG_HABILITADO", dto.FLG_HABILITADO ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMADeveloper.Mensajes.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMADeveloper.Mensajes.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMADeveloper.Mensajes.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_KEY_MENSAJE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMADeveloper.Mensajes.Delete, new { COD_KEY_MENSAJE = COD_KEY_MENSAJE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMADeveloper.Mensajes.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMADeveloper.Mensajes.Delete, new { COD_KEY_MENSAJE = COD_KEY_MENSAJE })
        };
    }
}

}