using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class BeneficiosLaboralesRepository : IBeneficiosLaboralesRepository
{
    private readonly IDbConnectionFactory _db;
    public BeneficiosLaboralesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<BeneficiosLaborales>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<BeneficiosLaborales>(SP.SCHEMANomina.BeneficiosLaborales.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<BeneficiosLaborales?> ShowbyidAsync(string COD_BENEFICIO_LABORAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<BeneficiosLaborales>(SP.SCHEMANomina.BeneficiosLaborales.Showbyid, new { COD_BENEFICIO_LABORAL = COD_BENEFICIO_LABORAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANomina.BeneficiosLaborales.Showcombox);
    }

    public async Task<IEnumerable<BeneficiosLaborales>> SearchAsync(BeneficiosLaboralesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<BeneficiosLaborales>(SP.SCHEMANomina.BeneficiosLaborales.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(BeneficiosLaboralesInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_BENEFICIO_LABORAL", dto.COD_BENEFICIO_LABORAL);
        p.Add("DES_BENEFICIO_LABORAL", dto.DES_BENEFICIO_LABORAL);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.BeneficiosLaborales.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.BeneficiosLaborales.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.BeneficiosLaborales.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(BeneficiosLaboralesUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_BENEFICIO_LABORAL", dto.COD_BENEFICIO_LABORAL);
        p.Add("DES_BENEFICIO_LABORAL", dto.DES_BENEFICIO_LABORAL);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.BeneficiosLaborales.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.BeneficiosLaborales.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.BeneficiosLaborales.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_BENEFICIO_LABORAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.BeneficiosLaborales.Delete, new { COD_BENEFICIO_LABORAL = COD_BENEFICIO_LABORAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.BeneficiosLaborales.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.BeneficiosLaborales.Delete, new { COD_BENEFICIO_LABORAL = COD_BENEFICIO_LABORAL })
        };
    }
}

}