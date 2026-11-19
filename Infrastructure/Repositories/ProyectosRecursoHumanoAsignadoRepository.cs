using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProyectosRecursoHumanoAsignadoRepository : IProyectosRecursoHumanoAsignadoRepository
{
    private readonly IDbConnectionFactory _db;
    public ProyectosRecursoHumanoAsignadoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProyectosRecursoHumanoAsignado>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosRecursoHumanoAsignado>(SP.SCHEMAProyectos.ProyectosRecursoHumanoAsignado.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProyectosRecursoHumanoAsignado?> ShowbyidAsync(int NUM_PROYECTO, int COD_TAREA, decimal COD_ENTIDAD_ASIGNADA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProyectosRecursoHumanoAsignado>(SP.SCHEMAProyectos.ProyectosRecursoHumanoAsignado.Showbyid, new { NUM_PROYECTO = NUM_PROYECTO, COD_TAREA = COD_TAREA, COD_ENTIDAD_ASIGNADA = COD_ENTIDAD_ASIGNADA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProyectos.ProyectosRecursoHumanoAsignado.Showcombox, "NUM_PROYECTO", "DES_COMERCIAL");
    }

    public async Task<IEnumerable<ProyectosRecursoHumanoAsignado>> SearchAsync(ProyectosRecursoHumanoAsignadoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosRecursoHumanoAsignado>(SP.SCHEMAProyectos.ProyectosRecursoHumanoAsignado.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProyectosRecursoHumanoAsignadoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("COD_TAREA", dto.COD_TAREA);
        p.Add("COD_ENTIDAD_ASIGNADA", dto.COD_ENTIDAD_ASIGNADA);
        p.Add("COD_ROL_TAREA", dto.COD_ROL_TAREA);
        p.Add("FEC_INICIO_PROGRAMADO", dto.FEC_INICIO_PROGRAMADO);
        p.Add("FEC_FINAL_PROGRAMADO", dto.FEC_FINAL_PROGRAMADO);
        p.Add("FEC_INICIO_EJECUTADO", dto.FEC_INICIO_EJECUTADO);
        p.Add("FEC_FINAL_EJECUTADO", dto.FEC_FINAL_EJECUTADO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosRecursoHumanoAsignado.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosRecursoHumanoAsignado.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosRecursoHumanoAsignado.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProyectosRecursoHumanoAsignadoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("COD_TAREA", dto.COD_TAREA);
        p.Add("COD_ENTIDAD_ASIGNADA", dto.COD_ENTIDAD_ASIGNADA);
        p.Add("COD_ROL_TAREA", dto.COD_ROL_TAREA);
        p.Add("FEC_INICIO_PROGRAMADO", dto.FEC_INICIO_PROGRAMADO);
        p.Add("FEC_FINAL_PROGRAMADO", dto.FEC_FINAL_PROGRAMADO);
        p.Add("FEC_INICIO_EJECUTADO", dto.FEC_INICIO_EJECUTADO);
        p.Add("FEC_FINAL_EJECUTADO", dto.FEC_FINAL_EJECUTADO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosRecursoHumanoAsignado.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosRecursoHumanoAsignado.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosRecursoHumanoAsignado.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PROYECTO, int COD_TAREA, decimal COD_ENTIDAD_ASIGNADA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosRecursoHumanoAsignado.Delete, new { NUM_PROYECTO = NUM_PROYECTO, COD_TAREA = COD_TAREA, COD_ENTIDAD_ASIGNADA = COD_ENTIDAD_ASIGNADA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosRecursoHumanoAsignado.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosRecursoHumanoAsignado.Delete, new { NUM_PROYECTO = NUM_PROYECTO, COD_TAREA = COD_TAREA, COD_ENTIDAD_ASIGNADA = COD_ENTIDAD_ASIGNADA })
        };
    }
}

}