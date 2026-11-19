using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ServidoresRepository : IServidoresRepository
{
    private readonly IDbConnectionFactory _db;
    public ServidoresRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Servidores>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Servidores>(SP.SCHEMAEntorno.Servidores.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Servidores?> ShowbyidAsync(string COD_KEY_SERVIDOR)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Servidores>(SP.SCHEMAEntorno.Servidores.Showbyid, new { COD_KEY_SERVIDOR = COD_KEY_SERVIDOR }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEntorno.Servidores.Showcombox);
    }

    public async Task<IEnumerable<Servidores>> SearchAsync(ServidoresSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Servidores>(SP.SCHEMAEntorno.Servidores.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ServidoresInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_KEY_SERVIDOR", dto.COD_KEY_SERVIDOR);
        p.Add("COD_IP", dto.COD_IP);
        p.Add("DES_KEY_IP", dto.DES_KEY_IP);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.Servidores.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.Servidores.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.Servidores.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_KEY_SERVIDOR)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.Servidores.Delete, new { COD_KEY_SERVIDOR = COD_KEY_SERVIDOR }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.Servidores.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.Servidores.Delete, new { COD_KEY_SERVIDOR = COD_KEY_SERVIDOR })
        };
    }
}

}
