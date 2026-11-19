using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PlanillaRemuneracionesRepository : IPlanillaRemuneracionesRepository
{
    private readonly IDbConnectionFactory _db;
    public PlanillaRemuneracionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PlanillaRemuneraciones>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PlanillaRemuneraciones>(SP.SCHEMANomina.PlanillaRemuneraciones.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PlanillaRemuneraciones?> ShowbyidAsync(int NUM_PLANILLA_REMUNERATIVA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PlanillaRemuneraciones>(SP.SCHEMANomina.PlanillaRemuneraciones.Showbyid, new { NUM_PLANILLA_REMUNERATIVA = NUM_PLANILLA_REMUNERATIVA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANomina.PlanillaRemuneraciones.Showcombox, "NUM_PLANILLA_REMUNERATIVA", "DES_PLANILLA_REMUNERATIVA");
    }

    public async Task<IEnumerable<PlanillaRemuneraciones>> SearchAsync(PlanillaRemuneracionesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PlanillaRemuneraciones>(SP.SCHEMANomina.PlanillaRemuneraciones.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PlanillaRemuneracionesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PLANILLA_REMUNERATIVA", dto.NUM_PLANILLA_REMUNERATIVA);
        p.Add("DES_PLANILLA_REMUNERATIVA", dto.DES_PLANILLA_REMUNERATIVA);
        p.Add("COD_TIPO_PLANILLA", dto.COD_TIPO_PLANILLA);
        p.Add("NUM_PERIODO_REMUNERATIVO", dto.NUM_PERIODO_REMUNERATIVO);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("NUM_AUTORIZACION", dto.NUM_AUTORIZACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.PlanillaRemuneraciones.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.PlanillaRemuneraciones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.PlanillaRemuneraciones.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PlanillaRemuneracionesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PLANILLA_REMUNERATIVA", dto.NUM_PLANILLA_REMUNERATIVA);
        p.Add("DES_PLANILLA_REMUNERATIVA", dto.DES_PLANILLA_REMUNERATIVA);
        p.Add("COD_TIPO_PLANILLA", dto.COD_TIPO_PLANILLA);
        p.Add("NUM_PERIODO_REMUNERATIVO", dto.NUM_PERIODO_REMUNERATIVO);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("NUM_AUTORIZACION", dto.NUM_AUTORIZACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.PlanillaRemuneraciones.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.PlanillaRemuneraciones.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.PlanillaRemuneraciones.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PLANILLA_REMUNERATIVA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.PlanillaRemuneraciones.Delete, new { NUM_PLANILLA_REMUNERATIVA = NUM_PLANILLA_REMUNERATIVA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.PlanillaRemuneraciones.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.PlanillaRemuneraciones.Delete, new { NUM_PLANILLA_REMUNERATIVA = NUM_PLANILLA_REMUNERATIVA })
        };
    }
}

}