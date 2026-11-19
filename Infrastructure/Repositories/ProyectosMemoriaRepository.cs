using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProyectosMemoriaRepository : IProyectosMemoriaRepository
{
    private readonly IDbConnectionFactory _db;
    public ProyectosMemoriaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProyectosMemoria>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosMemoria>(SP.SCHEMAProyectos.ProyectosMemoria.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProyectosMemoria?> ShowbyidAsync(int NUM_PROYECTO, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProyectosMemoria>(SP.SCHEMAProyectos.ProyectosMemoria.Showbyid, new { NUM_PROYECTO = NUM_PROYECTO, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProyectos.ProyectosMemoria.Showcombox);
    }

    public async Task<IEnumerable<ProyectosMemoria>> SearchAsync(ProyectosMemoriaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosMemoria>(SP.SCHEMAProyectos.ProyectosMemoria.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProyectosMemoriaInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_FASE", dto.COD_FASE);
        p.Add("COD_TAREA", dto.COD_TAREA);
        p.Add("DES_MEMORIA", dto.DES_MEMORIA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("NUM_SECUENCIA_EVENTO", dto.NUM_SECUENCIA_EVENTO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosMemoria.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosMemoria.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosMemoria.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProyectosMemoriaUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_FASE", dto.COD_FASE);
        p.Add("COD_TAREA", dto.COD_TAREA);
        p.Add("DES_MEMORIA", dto.DES_MEMORIA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("NUM_SECUENCIA_EVENTO", dto.NUM_SECUENCIA_EVENTO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosMemoria.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosMemoria.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosMemoria.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PROYECTO, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosMemoria.Delete, new { NUM_PROYECTO = NUM_PROYECTO, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosMemoria.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosMemoria.Delete, new { NUM_PROYECTO = NUM_PROYECTO, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}