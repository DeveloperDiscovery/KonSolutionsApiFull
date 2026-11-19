using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProyectosFasesRepository : IProyectosFasesRepository
{
    private readonly IDbConnectionFactory _db;
    public ProyectosFasesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProyectosFases>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosFases>(SP.SCHEMAProyectos.ProyectosFases.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProyectosFases?> ShowbyidAsync(int NUM_PROYECTO, int COD_FASE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProyectosFases>(SP.SCHEMAProyectos.ProyectosFases.Showbyid, new { NUM_PROYECTO = NUM_PROYECTO, COD_FASE = COD_FASE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProyectos.ProyectosFases.Showcombox, "NUM_PROYECTO", "DES_FASE");
    }

    public async Task<IEnumerable<ProyectosFases>> SearchAsync(ProyectosFasesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosFases>(SP.SCHEMAProyectos.ProyectosFases.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProyectosFasesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("COD_FASE", dto.COD_FASE);
        p.Add("DES_FASE", dto.DES_FASE);
        p.Add("FEC_INICIO_PROGRAMADO", dto.FEC_INICIO_PROGRAMADO);
        p.Add("FEC_FINAL_PROGRAMADO", dto.FEC_FINAL_PROGRAMADO);
        p.Add("FEC_INICIO_EJECUTADO", dto.FEC_INICIO_EJECUTADO);
        p.Add("FEC_FINAL_EJECUTADO", dto.FEC_FINAL_EJECUTADO);
        p.Add("COD_ENTIDAD_RESPONSABLE", dto.COD_ENTIDAD_RESPONSABLE);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_FASE_PADRE", dto.COD_FASE_PADRE);
        p.Add("COD_HITO_PROYECTO", dto.COD_HITO_PROYECTO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosFases.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosFases.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosFases.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProyectosFasesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("COD_FASE", dto.COD_FASE);
        p.Add("DES_FASE", dto.DES_FASE);
        p.Add("FEC_INICIO_PROGRAMADO", dto.FEC_INICIO_PROGRAMADO);
        p.Add("FEC_FINAL_PROGRAMADO", dto.FEC_FINAL_PROGRAMADO);
        p.Add("FEC_INICIO_EJECUTADO", dto.FEC_INICIO_EJECUTADO);
        p.Add("FEC_FINAL_EJECUTADO", dto.FEC_FINAL_EJECUTADO);
        p.Add("COD_ENTIDAD_RESPONSABLE", dto.COD_ENTIDAD_RESPONSABLE);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_FASE_PADRE", dto.COD_FASE_PADRE);
        p.Add("COD_HITO_PROYECTO", dto.COD_HITO_PROYECTO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosFases.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosFases.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosFases.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PROYECTO, int COD_FASE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosFases.Delete, new { NUM_PROYECTO = NUM_PROYECTO, COD_FASE = COD_FASE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosFases.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosFases.Delete, new { NUM_PROYECTO = NUM_PROYECTO, COD_FASE = COD_FASE })
        };
    }
}

}