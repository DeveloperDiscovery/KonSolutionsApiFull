using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProyectosHitosRepository : IProyectosHitosRepository
{
    private readonly IDbConnectionFactory _db;
    public ProyectosHitosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProyectosHitos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosHitos>(SP.SCHEMAProyectos.ProyectosHitos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProyectosHitos?> ShowbyidAsync(int NUM_PROYECTO, int COD_HITO_PROYECTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProyectosHitos>(SP.SCHEMAProyectos.ProyectosHitos.Showbyid, new { NUM_PROYECTO = NUM_PROYECTO, COD_HITO_PROYECTO = COD_HITO_PROYECTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProyectos.ProyectosHitos.Showcombox, "NUM_PROYECTO", "DES_HITO_PROYECTO");
    }

    public async Task<IEnumerable<ProyectosHitos>> SearchAsync(ProyectosHitosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosHitos>(SP.SCHEMAProyectos.ProyectosHitos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProyectosHitosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("COD_HITO_PROYECTO", dto.COD_HITO_PROYECTO);
        p.Add("DES_HITO_PROYECTO", dto.DES_HITO_PROYECTO);
        p.Add("FEC_FINAL_PROGRAMADO", dto.FEC_FINAL_PROGRAMADO);
        p.Add("FEC_INICIO_EJECUTADO", dto.FEC_INICIO_EJECUTADO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosHitos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosHitos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosHitos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProyectosHitosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("COD_HITO_PROYECTO", dto.COD_HITO_PROYECTO);
        p.Add("DES_HITO_PROYECTO", dto.DES_HITO_PROYECTO);
        p.Add("FEC_FINAL_PROGRAMADO", dto.FEC_FINAL_PROGRAMADO);
        p.Add("FEC_INICIO_EJECUTADO", dto.FEC_INICIO_EJECUTADO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosHitos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosHitos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosHitos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PROYECTO, int COD_HITO_PROYECTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosHitos.Delete, new { NUM_PROYECTO = NUM_PROYECTO, COD_HITO_PROYECTO = COD_HITO_PROYECTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosHitos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosHitos.Delete, new { NUM_PROYECTO = NUM_PROYECTO, COD_HITO_PROYECTO = COD_HITO_PROYECTO })
        };
    }
}

}