using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ConceptosFinancierosRepository : IConceptosFinancierosRepository
{
    private readonly IDbConnectionFactory _db;
    public ConceptosFinancierosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ConceptosFinancieros>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ConceptosFinancieros>(SP.SCHEMAFinanzas.ConceptosFinancieros.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ConceptosFinancieros?> ShowbyidAsync(string COD_TIPO_CONCEPTO_FINANCIERO, string COD_CONCEPTO_FINANCIERO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ConceptosFinancieros>(SP.SCHEMAFinanzas.ConceptosFinancieros.Showbyid, new { COD_TIPO_CONCEPTO_FINANCIERO = COD_TIPO_CONCEPTO_FINANCIERO, COD_CONCEPTO_FINANCIERO = COD_CONCEPTO_FINANCIERO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAFinanzas.ConceptosFinancieros.Showcombox);
    }

    public async Task<IEnumerable<ConceptosFinancieros>> SearchAsync(ConceptosFinancierosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ConceptosFinancieros>(SP.SCHEMAFinanzas.ConceptosFinancieros.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ConceptosFinancierosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CONCEPTO_FINANCIERO", dto.COD_TIPO_CONCEPTO_FINANCIERO);
        p.Add("COD_CONCEPTO_FINANCIERO", dto.COD_CONCEPTO_FINANCIERO);
        p.Add("DES_CONCEPTO_FINANCIERO", dto.DES_CONCEPTO_FINANCIERO);
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("COD_CUENTA_CONTABLE", dto.COD_CUENTA_CONTABLE);
        p.Add("COD_CENTRO_COSTO", dto.COD_CENTRO_COSTO);
        p.Add("COD_PRODUCTO_GASTO", dto.COD_PRODUCTO_GASTO);
        p.Add("NUM_REGISTRO", dto.NUM_REGISTRO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ConceptosFinancieros.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ConceptosFinancieros.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ConceptosFinancieros.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ConceptosFinancierosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CONCEPTO_FINANCIERO", dto.COD_TIPO_CONCEPTO_FINANCIERO);
        p.Add("COD_CONCEPTO_FINANCIERO", dto.COD_CONCEPTO_FINANCIERO);
        p.Add("DES_CONCEPTO_FINANCIERO", dto.DES_CONCEPTO_FINANCIERO);
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("COD_CUENTA_CONTABLE", dto.COD_CUENTA_CONTABLE);
        p.Add("COD_CENTRO_COSTO", dto.COD_CENTRO_COSTO);
        p.Add("COD_PRODUCTO_GASTO", dto.COD_PRODUCTO_GASTO);
        p.Add("NUM_REGISTRO", dto.NUM_REGISTRO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ConceptosFinancieros.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ConceptosFinancieros.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ConceptosFinancieros.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_CONCEPTO_FINANCIERO, string COD_CONCEPTO_FINANCIERO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.ConceptosFinancieros.Delete, new { COD_TIPO_CONCEPTO_FINANCIERO = COD_TIPO_CONCEPTO_FINANCIERO, COD_CONCEPTO_FINANCIERO = COD_CONCEPTO_FINANCIERO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.ConceptosFinancieros.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.ConceptosFinancieros.Delete, new { COD_TIPO_CONCEPTO_FINANCIERO = COD_TIPO_CONCEPTO_FINANCIERO, COD_CONCEPTO_FINANCIERO = COD_CONCEPTO_FINANCIERO })
        };
    }
}

}