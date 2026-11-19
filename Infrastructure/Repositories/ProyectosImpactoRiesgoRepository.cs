using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProyectosImpactoRiesgoRepository : IProyectosImpactoRiesgoRepository
{
    private readonly IDbConnectionFactory _db;
    public ProyectosImpactoRiesgoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProyectosImpactoRiesgo>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosImpactoRiesgo>(SP.SCHEMAProyectos.ProyectosImpactoRiesgo.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProyectosImpactoRiesgo?> ShowbyidAsync(int COD_IMPACTO_RIESGO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProyectosImpactoRiesgo>(SP.SCHEMAProyectos.ProyectosImpactoRiesgo.Showbyid, new { COD_IMPACTO_RIESGO = COD_IMPACTO_RIESGO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProyectos.ProyectosImpactoRiesgo.Showcombox);
    }

    public async Task<IEnumerable<ProyectosImpactoRiesgo>> SearchAsync(ProyectosImpactoRiesgoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosImpactoRiesgo>(SP.SCHEMAProyectos.ProyectosImpactoRiesgo.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProyectosImpactoRiesgoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_IMPACTO_RIESGO", dto.COD_IMPACTO_RIESGO);
        p.Add("DES_IMPACTO_RIESGO", dto.DES_IMPACTO_RIESGO);
        p.Add("VAL_PROBABILIDAD", dto.VAL_PROBABILIDAD);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosImpactoRiesgo.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosImpactoRiesgo.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosImpactoRiesgo.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProyectosImpactoRiesgoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_IMPACTO_RIESGO", dto.COD_IMPACTO_RIESGO);
        p.Add("DES_IMPACTO_RIESGO", dto.DES_IMPACTO_RIESGO);
        p.Add("VAL_PROBABILIDAD", dto.VAL_PROBABILIDAD);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosImpactoRiesgo.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosImpactoRiesgo.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosImpactoRiesgo.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_IMPACTO_RIESGO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosImpactoRiesgo.Delete, new { COD_IMPACTO_RIESGO = COD_IMPACTO_RIESGO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosImpactoRiesgo.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosImpactoRiesgo.Delete, new { COD_IMPACTO_RIESGO = COD_IMPACTO_RIESGO })
        };
    }
}

}