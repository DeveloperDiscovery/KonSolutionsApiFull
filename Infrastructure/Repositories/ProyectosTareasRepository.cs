using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProyectosTareasRepository : IProyectosTareasRepository
{
    private readonly IDbConnectionFactory _db;
    public ProyectosTareasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProyectosTareas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosTareas>(SP.SCHEMAProyectos.ProyectosTareas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProyectosTareas?> ShowbyidAsync(int NUM_PROYECTO, int COD_TAREA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProyectosTareas>(SP.SCHEMAProyectos.ProyectosTareas.Showbyid, new { NUM_PROYECTO = NUM_PROYECTO, COD_TAREA = COD_TAREA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProyectos.ProyectosTareas.Showcombox, "NUM_PROYECTO", "DES_TAREA");
    }

    public async Task<IEnumerable<ProyectosTareas>> SearchAsync(ProyectosTareasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosTareas>(SP.SCHEMAProyectos.ProyectosTareas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProyectosTareasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("COD_TAREA", dto.COD_TAREA);
        p.Add("DES_TAREA", dto.DES_TAREA);
        p.Add("FEC_INICIO_PROGRAMADO", dto.FEC_INICIO_PROGRAMADO);
        p.Add("FEC_FINAL_PROGRAMADO", dto.FEC_FINAL_PROGRAMADO);
        p.Add("FEC_INICIO_EJECUTADO", dto.FEC_INICIO_EJECUTADO);
        p.Add("FEC_FINAL_EJECUTADO", dto.FEC_FINAL_EJECUTADO);
        p.Add("FLG_CONTROL_EJECUCION_MANUAL", dto.FLG_CONTROL_EJECUCION_MANUAL ?? false);
        p.Add("FLG_REQUIERE_CONFIRMACION", dto.FLG_REQUIERE_CONFIRMACION ?? false);
        p.Add("COD_ENTIDAD_CONFIRMADORA", dto.COD_ENTIDAD_CONFIRMADORA);
        p.Add("COD_UNIDAD_MEDIDA_TAREAS", dto.COD_UNIDAD_MEDIDA_TAREAS);
        p.Add("CAN_PROGRAMADAS", dto.CAN_PROGRAMADAS);
        p.Add("CAN_EJECUTADAS", dto.CAN_EJECUTADAS);
        p.Add("TAS_EJECUCION", dto.TAS_EJECUCION);
        p.Add("COD_FASE", dto.COD_FASE);
        p.Add("COD_ENTIDAD_RESPONSABLE", dto.COD_ENTIDAD_RESPONSABLE);
        p.Add("VAL_PRESUPUESTO", dto.VAL_PRESUPUESTO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosTareas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosTareas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosTareas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProyectosTareasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("COD_TAREA", dto.COD_TAREA);
        p.Add("DES_TAREA", dto.DES_TAREA);
        p.Add("FEC_INICIO_PROGRAMADO", dto.FEC_INICIO_PROGRAMADO);
        p.Add("FEC_FINAL_PROGRAMADO", dto.FEC_FINAL_PROGRAMADO);
        p.Add("FEC_INICIO_EJECUTADO", dto.FEC_INICIO_EJECUTADO);
        p.Add("FEC_FINAL_EJECUTADO", dto.FEC_FINAL_EJECUTADO);
        p.Add("FLG_CONTROL_EJECUCION_MANUAL", dto.FLG_CONTROL_EJECUCION_MANUAL ?? false);
        p.Add("FLG_REQUIERE_CONFIRMACION", dto.FLG_REQUIERE_CONFIRMACION ?? false);
        p.Add("COD_ENTIDAD_CONFIRMADORA", dto.COD_ENTIDAD_CONFIRMADORA);
        p.Add("COD_UNIDAD_MEDIDA_TAREAS", dto.COD_UNIDAD_MEDIDA_TAREAS);
        p.Add("CAN_PROGRAMADAS", dto.CAN_PROGRAMADAS);
        p.Add("CAN_EJECUTADAS", dto.CAN_EJECUTADAS);
        p.Add("TAS_EJECUCION", dto.TAS_EJECUCION);
        p.Add("COD_FASE", dto.COD_FASE);
        p.Add("COD_ENTIDAD_RESPONSABLE", dto.COD_ENTIDAD_RESPONSABLE);
        p.Add("VAL_PRESUPUESTO", dto.VAL_PRESUPUESTO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosTareas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosTareas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosTareas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PROYECTO, int COD_TAREA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosTareas.Delete, new { NUM_PROYECTO = NUM_PROYECTO, COD_TAREA = COD_TAREA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosTareas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosTareas.Delete, new { NUM_PROYECTO = NUM_PROYECTO, COD_TAREA = COD_TAREA })
        };
    }
}

}