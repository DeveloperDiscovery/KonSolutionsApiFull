using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProyectosPlanesMitigacionRiesgosRepository : IProyectosPlanesMitigacionRiesgosRepository
{
    private readonly IDbConnectionFactory _db;
    public ProyectosPlanesMitigacionRiesgosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProyectosPlanesMitigacionRiesgos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosPlanesMitigacionRiesgos>(SP.SCHEMAProyectos.ProyectosPlanesMitigacionRiesgos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProyectosPlanesMitigacionRiesgos?> ShowbyidAsync(int NUM_PROYECTO, string COD_PLAN_MITIGACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProyectosPlanesMitigacionRiesgos>(SP.SCHEMAProyectos.ProyectosPlanesMitigacionRiesgos.Showbyid, new { NUM_PROYECTO = NUM_PROYECTO, COD_PLAN_MITIGACION = COD_PLAN_MITIGACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProyectos.ProyectosPlanesMitigacionRiesgos.Showcombox, "NUM_PROYECTO", "DES_PLAN_MITIGACION");
    }

    public async Task<IEnumerable<ProyectosPlanesMitigacionRiesgos>> SearchAsync(ProyectosPlanesMitigacionRiesgosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosPlanesMitigacionRiesgos>(SP.SCHEMAProyectos.ProyectosPlanesMitigacionRiesgos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProyectosPlanesMitigacionRiesgosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("COD_PLAN_MITIGACION", dto.COD_PLAN_MITIGACION);
        p.Add("DES_PLAN_MITIGACION", dto.DES_PLAN_MITIGACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosPlanesMitigacionRiesgos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosPlanesMitigacionRiesgos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosPlanesMitigacionRiesgos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProyectosPlanesMitigacionRiesgosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("COD_PLAN_MITIGACION", dto.COD_PLAN_MITIGACION);
        p.Add("DES_PLAN_MITIGACION", dto.DES_PLAN_MITIGACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosPlanesMitigacionRiesgos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosPlanesMitigacionRiesgos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosPlanesMitigacionRiesgos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PROYECTO, string COD_PLAN_MITIGACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosPlanesMitigacionRiesgos.Delete, new { NUM_PROYECTO = NUM_PROYECTO, COD_PLAN_MITIGACION = COD_PLAN_MITIGACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosPlanesMitigacionRiesgos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosPlanesMitigacionRiesgos.Delete, new { NUM_PROYECTO = NUM_PROYECTO, COD_PLAN_MITIGACION = COD_PLAN_MITIGACION })
        };
    }
}

}