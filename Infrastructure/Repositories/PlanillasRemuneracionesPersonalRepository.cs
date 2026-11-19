using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PlanillasRemuneracionesPersonalRepository : IPlanillasRemuneracionesPersonalRepository
{
    private readonly IDbConnectionFactory _db;
    public PlanillasRemuneracionesPersonalRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PlanillasRemuneracionesPersonal>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PlanillasRemuneracionesPersonal>(SP.SCHEMANomina.PlanillasRemuneracionesPersonal.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PlanillasRemuneracionesPersonal?> ShowbyidAsync(int NUM_PLANILLA_REMUNERATIVA, decimal COD_ENTIDAD_PERSONAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PlanillasRemuneracionesPersonal>(SP.SCHEMANomina.PlanillasRemuneracionesPersonal.Showbyid, new { NUM_PLANILLA_REMUNERATIVA = NUM_PLANILLA_REMUNERATIVA, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANomina.PlanillasRemuneracionesPersonal.Showcombox);
    }

    public async Task<IEnumerable<PlanillasRemuneracionesPersonal>> SearchAsync(PlanillasRemuneracionesPersonalSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PlanillasRemuneracionesPersonal>(SP.SCHEMANomina.PlanillasRemuneracionesPersonal.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PlanillasRemuneracionesPersonalInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PLANILLA_REMUNERATIVA", dto.NUM_PLANILLA_REMUNERATIVA);
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("VAL_NETO_PAGAR", dto.VAL_NETO_PAGAR);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.PlanillasRemuneracionesPersonal.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.PlanillasRemuneracionesPersonal.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.PlanillasRemuneracionesPersonal.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PlanillasRemuneracionesPersonalUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PLANILLA_REMUNERATIVA", dto.NUM_PLANILLA_REMUNERATIVA);
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("VAL_NETO_PAGAR", dto.VAL_NETO_PAGAR);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.PlanillasRemuneracionesPersonal.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.PlanillasRemuneracionesPersonal.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.PlanillasRemuneracionesPersonal.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PLANILLA_REMUNERATIVA, decimal COD_ENTIDAD_PERSONAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.PlanillasRemuneracionesPersonal.Delete, new { NUM_PLANILLA_REMUNERATIVA = NUM_PLANILLA_REMUNERATIVA, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.PlanillasRemuneracionesPersonal.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.PlanillasRemuneracionesPersonal.Delete, new { NUM_PLANILLA_REMUNERATIVA = NUM_PLANILLA_REMUNERATIVA, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL })
        };
    }
}

}