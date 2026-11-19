using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class AutorizacionesEventosRepository : IAutorizacionesEventosRepository
{
    private readonly IDbConnectionFactory _db;
    public AutorizacionesEventosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<AutorizacionesEventos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AutorizacionesEventos>(SP.SCHEMAEntorno.AutorizacionesEventos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<AutorizacionesEventos?> ShowbyidAsync(int NUM_AUTORIZACION, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<AutorizacionesEventos>(SP.SCHEMAEntorno.AutorizacionesEventos.Showbyid, new { NUM_AUTORIZACION = NUM_AUTORIZACION, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEntorno.AutorizacionesEventos.Showcombox);
    }

    public async Task<IEnumerable<AutorizacionesEventos>> SearchAsync(AutorizacionesEventosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AutorizacionesEventos>(SP.SCHEMAEntorno.AutorizacionesEventos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(AutorizacionesEventosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_AUTORIZACION", dto.NUM_AUTORIZACION);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_USUARIO", dto.COD_USUARIO);
        p.Add("DES_EVENTO", dto.DES_EVENTO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.AutorizacionesEventos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.AutorizacionesEventos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.AutorizacionesEventos.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_AUTORIZACION, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.AutorizacionesEventos.Delete, new { NUM_AUTORIZACION = NUM_AUTORIZACION, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.AutorizacionesEventos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.AutorizacionesEventos.Delete, new { NUM_AUTORIZACION = NUM_AUTORIZACION, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}