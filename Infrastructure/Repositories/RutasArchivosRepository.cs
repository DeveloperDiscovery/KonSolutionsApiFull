using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class RutasArchivosRepository : IRutasArchivosRepository
{
    private readonly IDbConnectionFactory _db;
    public RutasArchivosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<RutasArchivos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RutasArchivos>(SP.SCHEMARrhh.RutasArchivos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<RutasArchivos?> ShowbyidAsync(int NUM_RUTA_ARCHIVO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RutasArchivos>(SP.SCHEMARrhh.RutasArchivos.Showbyid, new { NUM_RUTA_ARCHIVO = NUM_RUTA_ARCHIVO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.RutasArchivos.Showcombox);
    }

    public async Task<IEnumerable<RutasArchivos>> SearchAsync(RutasArchivosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RutasArchivos>(SP.SCHEMARrhh.RutasArchivos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(RutasArchivosInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_RUTA_ARCHIVO", dto.NUM_RUTA_ARCHIVO);
        p.Add("DES_RUTA_ARCHIVO", dto.DES_RUTA_ARCHIVO);
        p.Add("KEY_RUTA_ARCHIVO", dto.KEY_RUTA_ARCHIVO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.RutasArchivos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.RutasArchivos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.RutasArchivos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(RutasArchivosUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_RUTA_ARCHIVO", dto.NUM_RUTA_ARCHIVO);
        p.Add("DES_RUTA_ARCHIVO", dto.DES_RUTA_ARCHIVO);
        p.Add("KEY_RUTA_ARCHIVO", dto.KEY_RUTA_ARCHIVO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.RutasArchivos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.RutasArchivos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.RutasArchivos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_RUTA_ARCHIVO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.RutasArchivos.Delete, new { NUM_RUTA_ARCHIVO = NUM_RUTA_ARCHIVO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.RutasArchivos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.RutasArchivos.Delete, new { NUM_RUTA_ARCHIVO = NUM_RUTA_ARCHIVO })
        };
    }
}

}