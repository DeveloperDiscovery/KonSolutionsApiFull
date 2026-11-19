using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ConceptosRemunerativosRepository : IConceptosRemunerativosRepository
{
    private readonly IDbConnectionFactory _db;
    public ConceptosRemunerativosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ConceptosRemunerativos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ConceptosRemunerativos>(SP.SCHEMANomina.ConceptosRemunerativos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ConceptosRemunerativos?> ShowbyidAsync(int COD_CONCEPTO_REMUNERATIVO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ConceptosRemunerativos>(SP.SCHEMANomina.ConceptosRemunerativos.Showbyid, new { COD_CONCEPTO_REMUNERATIVO = COD_CONCEPTO_REMUNERATIVO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANomina.ConceptosRemunerativos.Showcombox);
    }

    public async Task<IEnumerable<ConceptosRemunerativos>> SearchAsync(ConceptosRemunerativosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ConceptosRemunerativos>(SP.SCHEMANomina.ConceptosRemunerativos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ConceptosRemunerativosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CONCEPTO_REMUNERATIVO", dto.COD_CONCEPTO_REMUNERATIVO);
        p.Add("DES_CONCEPTO_REMUNERATIVO", dto.DES_CONCEPTO_REMUNERATIVO);
        p.Add("DES_ALIAS", dto.DES_ALIAS);
        p.Add("COD_TIPO_REMUNERATIVO", dto.COD_TIPO_REMUNERATIVO);
        p.Add("FLG_TIEMPO", dto.FLG_TIEMPO ?? false);
        p.Add("FLG_FIJO", dto.FLG_FIJO ?? false);
        p.Add("FLG_TASA", dto.FLG_TASA ?? false);
        p.Add("COD_BENEFICIO_LABORAL", dto.COD_BENEFICIO_LABORAL);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.ConceptosRemunerativos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.ConceptosRemunerativos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.ConceptosRemunerativos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ConceptosRemunerativosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CONCEPTO_REMUNERATIVO", dto.COD_CONCEPTO_REMUNERATIVO);
        p.Add("DES_CONCEPTO_REMUNERATIVO", dto.DES_CONCEPTO_REMUNERATIVO);
        p.Add("DES_ALIAS", dto.DES_ALIAS);
        p.Add("COD_TIPO_REMUNERATIVO", dto.COD_TIPO_REMUNERATIVO);
        p.Add("FLG_TIEMPO", dto.FLG_TIEMPO ?? false);
        p.Add("FLG_FIJO", dto.FLG_FIJO ?? false);
        p.Add("FLG_TASA", dto.FLG_TASA ?? false);
        p.Add("COD_BENEFICIO_LABORAL", dto.COD_BENEFICIO_LABORAL);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.ConceptosRemunerativos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.ConceptosRemunerativos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.ConceptosRemunerativos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_CONCEPTO_REMUNERATIVO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.ConceptosRemunerativos.Delete, new { COD_CONCEPTO_REMUNERATIVO = COD_CONCEPTO_REMUNERATIVO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.ConceptosRemunerativos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.ConceptosRemunerativos.Delete, new { COD_CONCEPTO_REMUNERATIVO = COD_CONCEPTO_REMUNERATIVO })
        };
    }
}

}