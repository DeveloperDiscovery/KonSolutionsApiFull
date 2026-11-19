using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposValorCalculoRepository : ITiposValorCalculoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposValorCalculoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposValorCalculo>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposValorCalculo>(SP.SCHEMAComercial.TiposValorCalculo.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposValorCalculo?> ShowbyidAsync(string COD_TIPO_VALOR_CALCULO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposValorCalculo>(SP.SCHEMAComercial.TiposValorCalculo.Showbyid, new { COD_TIPO_VALOR_CALCULO = COD_TIPO_VALOR_CALCULO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.TiposValorCalculo.Showcombox);
    }

    public async Task<IEnumerable<TiposValorCalculo>> SearchAsync(TiposValorCalculoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposValorCalculo>(SP.SCHEMAComercial.TiposValorCalculo.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposValorCalculoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_VALOR_CALCULO", dto.COD_TIPO_VALOR_CALCULO);
        p.Add("DES_TIPO_VALOR_CALCULO", dto.DES_TIPO_VALOR_CALCULO);
        p.Add("COD_TIPO_IMPUESTO", dto.COD_TIPO_IMPUESTO);
        p.Add("FLG_POSITIVO", dto.FLG_POSITIVO ?? false);
        p.Add("FLG_IMPUESTO", dto.FLG_IMPUESTO ?? false);
        p.Add("FLG_CALCULA", dto.FLG_CALCULA ?? false);
        p.Add("FLG_REGIMEN_TRIBUTARIO", dto.FLG_REGIMEN_TRIBUTARIO ?? false);
        p.Add("FLG_ANTICIPO", dto.FLG_ANTICIPO ?? false);
        p.Add("DES_ETIQUETA_XML", dto.DES_ETIQUETA_XML);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposValorCalculo.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposValorCalculo.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposValorCalculo.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposValorCalculoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_VALOR_CALCULO", dto.COD_TIPO_VALOR_CALCULO);
        p.Add("DES_TIPO_VALOR_CALCULO", dto.DES_TIPO_VALOR_CALCULO);
        p.Add("COD_TIPO_IMPUESTO", dto.COD_TIPO_IMPUESTO);
        p.Add("FLG_POSITIVO", dto.FLG_POSITIVO ?? false);
        p.Add("FLG_IMPUESTO", dto.FLG_IMPUESTO ?? false);
        p.Add("FLG_CALCULA", dto.FLG_CALCULA ?? false);
        p.Add("FLG_REGIMEN_TRIBUTARIO", dto.FLG_REGIMEN_TRIBUTARIO ?? false);
        p.Add("FLG_ANTICIPO", dto.FLG_ANTICIPO ?? false);
        p.Add("DES_ETIQUETA_XML", dto.DES_ETIQUETA_XML);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposValorCalculo.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposValorCalculo.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposValorCalculo.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_VALOR_CALCULO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposValorCalculo.Delete, new { COD_TIPO_VALOR_CALCULO = COD_TIPO_VALOR_CALCULO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposValorCalculo.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposValorCalculo.Delete, new { COD_TIPO_VALOR_CALCULO = COD_TIPO_VALOR_CALCULO })
        };
    }
}

}