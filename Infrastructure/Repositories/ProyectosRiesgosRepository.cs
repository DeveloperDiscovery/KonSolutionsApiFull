using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProyectosRiesgosRepository : IProyectosRiesgosRepository
{
    private readonly IDbConnectionFactory _db;
    public ProyectosRiesgosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProyectosRiesgos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosRiesgos>(SP.SCHEMAProyectos.ProyectosRiesgos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProyectosRiesgos?> ShowbyidAsync(int NUM_PROYECTO, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProyectosRiesgos>(SP.SCHEMAProyectos.ProyectosRiesgos.Showbyid, new { NUM_PROYECTO = NUM_PROYECTO, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProyectos.ProyectosRiesgos.Showcombox, "NUM_PROYECTO", "DES_RIESGO");
    }

    public async Task<IEnumerable<ProyectosRiesgos>> SearchAsync(ProyectosRiesgosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosRiesgos>(SP.SCHEMAProyectos.ProyectosRiesgos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProyectosRiesgosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("DES_RIESGO", dto.DES_RIESGO);
        p.Add("COD_PROBABILIDAD_RIESGO", dto.COD_PROBABILIDAD_RIESGO);
        p.Add("COD_IMPACTO_RIESGO", dto.COD_IMPACTO_RIESGO);
        p.Add("COD_PLAN_MITIGACION", dto.COD_PLAN_MITIGACION);
        p.Add("COD_FASE", dto.COD_FASE);
        p.Add("COD_TAREA", dto.COD_TAREA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosRiesgos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosRiesgos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosRiesgos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProyectosRiesgosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("DES_RIESGO", dto.DES_RIESGO);
        p.Add("COD_PROBABILIDAD_RIESGO", dto.COD_PROBABILIDAD_RIESGO);
        p.Add("COD_IMPACTO_RIESGO", dto.COD_IMPACTO_RIESGO);
        p.Add("COD_PLAN_MITIGACION", dto.COD_PLAN_MITIGACION);
        p.Add("COD_FASE", dto.COD_FASE);
        p.Add("COD_TAREA", dto.COD_TAREA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosRiesgos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosRiesgos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosRiesgos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PROYECTO, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosRiesgos.Delete, new { NUM_PROYECTO = NUM_PROYECTO, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosRiesgos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosRiesgos.Delete, new { NUM_PROYECTO = NUM_PROYECTO, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}