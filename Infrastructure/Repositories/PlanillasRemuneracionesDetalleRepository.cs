using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PlanillasRemuneracionesDetalleRepository : IPlanillasRemuneracionesDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public PlanillasRemuneracionesDetalleRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PlanillasRemuneracionesDetalle>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PlanillasRemuneracionesDetalle>(SP.SCHEMANomina.PlanillasRemuneracionesDetalle.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PlanillasRemuneracionesDetalle?> ShowbyidAsync(int NUM_PLANILLA_REMUNERATIVA, decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PlanillasRemuneracionesDetalle>(SP.SCHEMANomina.PlanillasRemuneracionesDetalle.Showbyid, new { NUM_PLANILLA_REMUNERATIVA = NUM_PLANILLA_REMUNERATIVA, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_CONCEPTO_REMUNERATIVO = COD_CONCEPTO_REMUNERATIVO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANomina.PlanillasRemuneracionesDetalle.Showcombox);
    }

    public async Task<IEnumerable<PlanillasRemuneracionesDetalle>> SearchAsync(PlanillasRemuneracionesDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PlanillasRemuneracionesDetalle>(SP.SCHEMANomina.PlanillasRemuneracionesDetalle.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PlanillasRemuneracionesDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PLANILLA_REMUNERATIVA", dto.NUM_PLANILLA_REMUNERATIVA);
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_CONCEPTO_REMUNERATIVO", dto.COD_CONCEPTO_REMUNERATIVO);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("COD_UNIDAD_MEDIDA_TIEMPO", dto.COD_UNIDAD_MEDIDA_TIEMPO);
        p.Add("CAN_TIEMPO", dto.CAN_TIEMPO);
        p.Add("VAL_CONCEPTO_REMUNERATIVO", dto.VAL_CONCEPTO_REMUNERATIVO);
        p.Add("TAS_CONCEPTO_REMUNERATIVO", dto.TAS_CONCEPTO_REMUNERATIVO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.PlanillasRemuneracionesDetalle.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.PlanillasRemuneracionesDetalle.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.PlanillasRemuneracionesDetalle.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PlanillasRemuneracionesDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PLANILLA_REMUNERATIVA", dto.NUM_PLANILLA_REMUNERATIVA);
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_CONCEPTO_REMUNERATIVO", dto.COD_CONCEPTO_REMUNERATIVO);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("COD_UNIDAD_MEDIDA_TIEMPO", dto.COD_UNIDAD_MEDIDA_TIEMPO);
        p.Add("CAN_TIEMPO", dto.CAN_TIEMPO);
        p.Add("VAL_CONCEPTO_REMUNERATIVO", dto.VAL_CONCEPTO_REMUNERATIVO);
        p.Add("TAS_CONCEPTO_REMUNERATIVO", dto.TAS_CONCEPTO_REMUNERATIVO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.PlanillasRemuneracionesDetalle.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.PlanillasRemuneracionesDetalle.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.PlanillasRemuneracionesDetalle.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PLANILLA_REMUNERATIVA, decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.PlanillasRemuneracionesDetalle.Delete, new { NUM_PLANILLA_REMUNERATIVA = NUM_PLANILLA_REMUNERATIVA, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_CONCEPTO_REMUNERATIVO = COD_CONCEPTO_REMUNERATIVO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.PlanillasRemuneracionesDetalle.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.PlanillasRemuneracionesDetalle.Delete, new { NUM_PLANILLA_REMUNERATIVA = NUM_PLANILLA_REMUNERATIVA, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_CONCEPTO_REMUNERATIVO = COD_CONCEPTO_REMUNERATIVO })
        };
    }
}

}