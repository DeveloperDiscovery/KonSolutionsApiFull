using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProyectosProbabilidadRiesgoRepository : IProyectosProbabilidadRiesgoRepository
{
    private readonly IDbConnectionFactory _db;
    public ProyectosProbabilidadRiesgoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProyectosProbabilidadRiesgo>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosProbabilidadRiesgo>(SP.SCHEMAProyectos.ProyectosProbabilidadRiesgo.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProyectosProbabilidadRiesgo?> ShowbyidAsync(int COD_PROBABILIDAD_RIESGO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProyectosProbabilidadRiesgo>(SP.SCHEMAProyectos.ProyectosProbabilidadRiesgo.Showbyid, new { COD_PROBABILIDAD_RIESGO = COD_PROBABILIDAD_RIESGO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProyectos.ProyectosProbabilidadRiesgo.Showcombox);
    }

    public async Task<IEnumerable<ProyectosProbabilidadRiesgo>> SearchAsync(ProyectosProbabilidadRiesgoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosProbabilidadRiesgo>(SP.SCHEMAProyectos.ProyectosProbabilidadRiesgo.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProyectosProbabilidadRiesgoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PROBABILIDAD_RIESGO", dto.COD_PROBABILIDAD_RIESGO);
        p.Add("DES_PROBABILIDAD_RIESGO", dto.DES_PROBABILIDAD_RIESGO);
        p.Add("VAL_PROBABILIDAD", dto.VAL_PROBABILIDAD);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosProbabilidadRiesgo.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosProbabilidadRiesgo.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosProbabilidadRiesgo.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProyectosProbabilidadRiesgoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PROBABILIDAD_RIESGO", dto.COD_PROBABILIDAD_RIESGO);
        p.Add("DES_PROBABILIDAD_RIESGO", dto.DES_PROBABILIDAD_RIESGO);
        p.Add("VAL_PROBABILIDAD", dto.VAL_PROBABILIDAD);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosProbabilidadRiesgo.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosProbabilidadRiesgo.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosProbabilidadRiesgo.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_PROBABILIDAD_RIESGO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosProbabilidadRiesgo.Delete, new { COD_PROBABILIDAD_RIESGO = COD_PROBABILIDAD_RIESGO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosProbabilidadRiesgo.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosProbabilidadRiesgo.Delete, new { COD_PROBABILIDAD_RIESGO = COD_PROBABILIDAD_RIESGO })
        };
    }
}

}