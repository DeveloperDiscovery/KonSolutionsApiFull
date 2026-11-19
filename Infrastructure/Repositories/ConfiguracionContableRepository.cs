using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ConfiguracionContableRepository : IConfiguracionContableRepository
{
    private readonly IDbConnectionFactory _db;
    public ConfiguracionContableRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ConfiguracionContable>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ConfiguracionContable>(SP.SCHEMAContabilidad.ConfiguracionContable.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ConfiguracionContable?> ShowbyidAsync(int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ConfiguracionContable>(SP.SCHEMAContabilidad.ConfiguracionContable.Showbyid, new { NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAContabilidad.ConfiguracionContable.Showcombox);
    }

    public async Task<IEnumerable<ConfiguracionContable>> SearchAsync(ConfiguracionContableSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ConfiguracionContable>(SP.SCHEMAContabilidad.ConfiguracionContable.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ConfiguracionContableInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_TIPO_PERIODO_CONTABLE", dto.COD_TIPO_PERIODO_CONTABLE);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.ConfiguracionContable.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.ConfiguracionContable.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.ConfiguracionContable.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ConfiguracionContableUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_TIPO_PERIODO_CONTABLE", dto.COD_TIPO_PERIODO_CONTABLE);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.ConfiguracionContable.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.ConfiguracionContable.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.ConfiguracionContable.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.ConfiguracionContable.Delete, new { NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.ConfiguracionContable.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.ConfiguracionContable.Delete, new { NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}