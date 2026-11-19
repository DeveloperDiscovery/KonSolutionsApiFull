using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class AfpsRepository : IAfpsRepository
{
    private readonly IDbConnectionFactory _db;
    public AfpsRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Afps>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Afps>(SP.SCHEMANomina.Afps.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Afps?> ShowbyidAsync(string COD_AFP)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Afps>(SP.SCHEMANomina.Afps.Showbyid, new { COD_AFP = COD_AFP }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANomina.Afps.Showcombox);
    }

    public async Task<IEnumerable<Afps>> SearchAsync(AfpsSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Afps>(SP.SCHEMANomina.Afps.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(AfpsInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_AFP", dto.COD_AFP);
        p.Add("DES_AFP", dto.DES_AFP);
        p.Add("FLG_PRIVADA", dto.FLG_PRIVADA ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.Afps.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.Afps.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.Afps.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(AfpsUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_AFP", dto.COD_AFP);
        p.Add("DES_AFP", dto.DES_AFP);
        p.Add("FLG_PRIVADA", dto.FLG_PRIVADA ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.Afps.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.Afps.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.Afps.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_AFP)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.Afps.Delete, new { COD_AFP = COD_AFP }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.Afps.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.Afps.Delete, new { COD_AFP = COD_AFP })
        };
    }
}

}