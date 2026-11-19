using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposPlanillaConceptosRemunerativosRepository : ITiposPlanillaConceptosRemunerativosRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposPlanillaConceptosRemunerativosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposPlanillaConceptosRemunerativos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposPlanillaConceptosRemunerativos>(SP.SCHEMANomina.TiposPlanillaConceptosRemunerativos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposPlanillaConceptosRemunerativos?> ShowbyidAsync(string COD_TIPO_PLANILLA, int COD_CONCEPTO_REMUNERATIVO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposPlanillaConceptosRemunerativos>(SP.SCHEMANomina.TiposPlanillaConceptosRemunerativos.Showbyid, new { COD_TIPO_PLANILLA = COD_TIPO_PLANILLA, COD_CONCEPTO_REMUNERATIVO = COD_CONCEPTO_REMUNERATIVO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANomina.TiposPlanillaConceptosRemunerativos.Showcombox);
    }

    public async Task<IEnumerable<TiposPlanillaConceptosRemunerativos>> SearchAsync(TiposPlanillaConceptosRemunerativosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposPlanillaConceptosRemunerativos>(SP.SCHEMANomina.TiposPlanillaConceptosRemunerativos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposPlanillaConceptosRemunerativosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PLANILLA", dto.COD_TIPO_PLANILLA);
        p.Add("COD_CONCEPTO_REMUNERATIVO", dto.COD_CONCEPTO_REMUNERATIVO);
        p.Add("COD_CUENTA_CONTABLE_DEBE", dto.COD_CUENTA_CONTABLE_DEBE);
        p.Add("COD_CUENTA_CONTABLE_HABER", dto.COD_CUENTA_CONTABLE_HABER);
        p.Add("DES_FORMULA", dto.DES_FORMULA);
        p.Add("DES_FORMULA_MASCARA", dto.DES_FORMULA_MASCARA);
        p.Add("NUM_ORDEN_CALCULO", dto.NUM_ORDEN_CALCULO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.TiposPlanillaConceptosRemunerativos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.TiposPlanillaConceptosRemunerativos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.TiposPlanillaConceptosRemunerativos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposPlanillaConceptosRemunerativosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PLANILLA", dto.COD_TIPO_PLANILLA);
        p.Add("COD_CONCEPTO_REMUNERATIVO", dto.COD_CONCEPTO_REMUNERATIVO);
        p.Add("COD_CUENTA_CONTABLE_DEBE", dto.COD_CUENTA_CONTABLE_DEBE);
        p.Add("COD_CUENTA_CONTABLE_HABER", dto.COD_CUENTA_CONTABLE_HABER);
        p.Add("DES_FORMULA", dto.DES_FORMULA);
        p.Add("DES_FORMULA_MASCARA", dto.DES_FORMULA_MASCARA);
        p.Add("NUM_ORDEN_CALCULO", dto.NUM_ORDEN_CALCULO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.TiposPlanillaConceptosRemunerativos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.TiposPlanillaConceptosRemunerativos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.TiposPlanillaConceptosRemunerativos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_PLANILLA, int COD_CONCEPTO_REMUNERATIVO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.TiposPlanillaConceptosRemunerativos.Delete, new { COD_TIPO_PLANILLA = COD_TIPO_PLANILLA, COD_CONCEPTO_REMUNERATIVO = COD_CONCEPTO_REMUNERATIVO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.TiposPlanillaConceptosRemunerativos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.TiposPlanillaConceptosRemunerativos.Delete, new { COD_TIPO_PLANILLA = COD_TIPO_PLANILLA, COD_CONCEPTO_REMUNERATIVO = COD_CONCEPTO_REMUNERATIVO })
        };
    }
}

}