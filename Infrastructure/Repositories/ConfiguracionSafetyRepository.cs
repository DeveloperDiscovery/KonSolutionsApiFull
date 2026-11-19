using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Safety;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ConfiguracionSafetyRepository : IConfiguracionSafetyRepository
{
    private readonly IDbConnectionFactory _db;
    public ConfiguracionSafetyRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ConfiguracionSafety>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ConfiguracionSafety>(SP.SCHEMASafety.ConfiguracionSafety.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ConfiguracionSafety?> ShowbyidAsync(int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ConfiguracionSafety>(SP.SCHEMASafety.ConfiguracionSafety.Showbyid, new { NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASafety.ConfiguracionSafety.Showcombox);
    }

    public async Task<IEnumerable<ConfiguracionSafety>> SearchAsync(ConfiguracionSafetySearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ConfiguracionSafety>(SP.SCHEMASafety.ConfiguracionSafety.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ConfiguracionSafetyInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("POS_EPC", dto.POS_EPC);
        p.Add("POS_ANTENA", dto.POS_ANTENA);
        p.Add("POS_RSSI", dto.POS_RSSI);
        p.Add("POS_DATE_TIME", dto.POS_DATE_TIME);
        p.Add("POS_ID_READER", dto.POS_ID_READER);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.ConfiguracionSafety.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.ConfiguracionSafety.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.ConfiguracionSafety.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ConfiguracionSafetyUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("POS_EPC", dto.POS_EPC);
        p.Add("POS_ANTENA", dto.POS_ANTENA);
        p.Add("POS_RSSI", dto.POS_RSSI);
        p.Add("POS_DATE_TIME", dto.POS_DATE_TIME);
        p.Add("POS_ID_READER", dto.POS_ID_READER);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.ConfiguracionSafety.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.ConfiguracionSafety.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.ConfiguracionSafety.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.ConfiguracionSafety.Delete, new { NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.ConfiguracionSafety.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.ConfiguracionSafety.Delete, new { NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}