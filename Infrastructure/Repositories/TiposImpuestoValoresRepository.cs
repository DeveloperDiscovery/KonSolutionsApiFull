using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposImpuestoValoresRepository : ITiposImpuestoValoresRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposImpuestoValoresRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposImpuestoValores>> ShowallAsync(string codTipoImpuesto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposImpuestoValores>(SP.SCHEMAComercial.TiposImpuestoValores.Showall, new { COD_TIPO_IMPUESTO = codTipoImpuesto }, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposImpuestoValores?> ShowbyidAsync(string COD_TIPO_IMPUESTO, DateTime FEC_INICIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposImpuestoValores>(SP.SCHEMAComercial.TiposImpuestoValores.Showbyid, new { COD_TIPO_IMPUESTO = COD_TIPO_IMPUESTO, FEC_INICIO = FEC_INICIO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.TiposImpuestoValores.Showcombox);
    }

    public async Task<IEnumerable<TiposImpuestoValores>> SearchAsync(TiposImpuestoValoresSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposImpuestoValores>(SP.SCHEMAComercial.TiposImpuestoValores.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposImpuestoValoresInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_IMPUESTO", dto.COD_TIPO_IMPUESTO);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_FINAL", dto.FEC_FINAL);
        p.Add("TAS_IMPUESTO", dto.TAS_IMPUESTO);
        p.Add("VAL_IMPUESTO", dto.VAL_IMPUESTO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposImpuestoValores.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposImpuestoValores.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposImpuestoValores.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposImpuestoValoresUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_IMPUESTO", dto.COD_TIPO_IMPUESTO);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_FINAL", dto.FEC_FINAL);
        p.Add("TAS_IMPUESTO", dto.TAS_IMPUESTO);
        p.Add("VAL_IMPUESTO", dto.VAL_IMPUESTO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposImpuestoValores.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposImpuestoValores.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposImpuestoValores.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_IMPUESTO, DateTime FEC_INICIO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposImpuestoValores.Delete, new { COD_TIPO_IMPUESTO = COD_TIPO_IMPUESTO, FEC_INICIO = FEC_INICIO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposImpuestoValores.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposImpuestoValores.Delete, new { COD_TIPO_IMPUESTO = COD_TIPO_IMPUESTO, FEC_INICIO = FEC_INICIO })
        };
    }
}

}