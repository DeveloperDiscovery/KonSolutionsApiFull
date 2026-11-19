using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PlanillasRemuneracionesFormulasRepository : IPlanillasRemuneracionesFormulasRepository
{
    private readonly IDbConnectionFactory _db;
    public PlanillasRemuneracionesFormulasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PlanillasRemuneracionesFormulas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PlanillasRemuneracionesFormulas>(SP.SCHEMANomina.PlanillasRemuneracionesFormulas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PlanillasRemuneracionesFormulas?> ShowbyidAsync(string COD_TIPO_PLANILLA, int COD_CONCEPTO_REMUNERATIVO, int NUM_SECUENCIA_FORMULA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PlanillasRemuneracionesFormulas>(SP.SCHEMANomina.PlanillasRemuneracionesFormulas.Showbyid, new { COD_TIPO_PLANILLA = COD_TIPO_PLANILLA, COD_CONCEPTO_REMUNERATIVO = COD_CONCEPTO_REMUNERATIVO, NUM_SECUENCIA_FORMULA = NUM_SECUENCIA_FORMULA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANomina.PlanillasRemuneracionesFormulas.Showcombox);
    }

    public async Task<IEnumerable<PlanillasRemuneracionesFormulas>> SearchAsync(PlanillasRemuneracionesFormulasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PlanillasRemuneracionesFormulas>(SP.SCHEMANomina.PlanillasRemuneracionesFormulas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PlanillasRemuneracionesFormulasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PLANILLA", dto.COD_TIPO_PLANILLA);
        p.Add("COD_CONCEPTO_REMUNERATIVO", dto.COD_CONCEPTO_REMUNERATIVO);
        p.Add("NUM_SECUENCIA_FORMULA", dto.NUM_SECUENCIA_FORMULA);
        p.Add("COD_CLASE_ELEMENTO_FORMULA", dto.COD_CLASE_ELEMENTO_FORMULA);
        p.Add("COD_TIPO_ELEMENTO_FORMULA", dto.COD_TIPO_ELEMENTO_FORMULA);
        p.Add("DES_ELEMENTO_FORMULA", dto.DES_ELEMENTO_FORMULA);
        p.Add("DES_ELEMENTO_MASCARA_FORMULA", dto.DES_ELEMENTO_MASCARA_FORMULA);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("NUM_ORDEN_CALCULO", dto.NUM_ORDEN_CALCULO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.PlanillasRemuneracionesFormulas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.PlanillasRemuneracionesFormulas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.PlanillasRemuneracionesFormulas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PlanillasRemuneracionesFormulasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PLANILLA", dto.COD_TIPO_PLANILLA);
        p.Add("COD_CONCEPTO_REMUNERATIVO", dto.COD_CONCEPTO_REMUNERATIVO);
        p.Add("NUM_SECUENCIA_FORMULA", dto.NUM_SECUENCIA_FORMULA);
        p.Add("COD_CLASE_ELEMENTO_FORMULA", dto.COD_CLASE_ELEMENTO_FORMULA);
        p.Add("COD_TIPO_ELEMENTO_FORMULA", dto.COD_TIPO_ELEMENTO_FORMULA);
        p.Add("DES_ELEMENTO_FORMULA", dto.DES_ELEMENTO_FORMULA);
        p.Add("DES_ELEMENTO_MASCARA_FORMULA", dto.DES_ELEMENTO_MASCARA_FORMULA);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("NUM_ORDEN_CALCULO", dto.NUM_ORDEN_CALCULO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.PlanillasRemuneracionesFormulas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.PlanillasRemuneracionesFormulas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.PlanillasRemuneracionesFormulas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_PLANILLA, int COD_CONCEPTO_REMUNERATIVO, int NUM_SECUENCIA_FORMULA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.PlanillasRemuneracionesFormulas.Delete, new { COD_TIPO_PLANILLA = COD_TIPO_PLANILLA, COD_CONCEPTO_REMUNERATIVO = COD_CONCEPTO_REMUNERATIVO, NUM_SECUENCIA_FORMULA = NUM_SECUENCIA_FORMULA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.PlanillasRemuneracionesFormulas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.PlanillasRemuneracionesFormulas.Delete, new { COD_TIPO_PLANILLA = COD_TIPO_PLANILLA, COD_CONCEPTO_REMUNERATIVO = COD_CONCEPTO_REMUNERATIVO, NUM_SECUENCIA_FORMULA = NUM_SECUENCIA_FORMULA })
        };
    }
}

}