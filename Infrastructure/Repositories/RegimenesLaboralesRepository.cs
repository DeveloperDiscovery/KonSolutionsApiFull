using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class RegimenesLaboralesRepository : IRegimenesLaboralesRepository
{
    private readonly IDbConnectionFactory _db;
    public RegimenesLaboralesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<RegimenesLaborales>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RegimenesLaborales>(SP.SCHEMARrhh.RegimenesLaborales.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<RegimenesLaborales?> ShowbyidAsync(string COD_REGIMEN_LABORAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RegimenesLaborales>(SP.SCHEMARrhh.RegimenesLaborales.Showbyid, new { COD_REGIMEN_LABORAL = COD_REGIMEN_LABORAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.RegimenesLaborales.Showcombox);
    }

    public async Task<IEnumerable<RegimenesLaborales>> SearchAsync(RegimenesLaboralesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RegimenesLaborales>(SP.SCHEMARrhh.RegimenesLaborales.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(RegimenesLaboralesInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_REGIMEN_LABORAL", dto.COD_REGIMEN_LABORAL);
        p.Add("DES_REGIMEN_LABORAL", dto.DES_REGIMEN_LABORAL);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.RegimenesLaborales.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.RegimenesLaborales.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.RegimenesLaborales.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(RegimenesLaboralesUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_REGIMEN_LABORAL", dto.COD_REGIMEN_LABORAL);
        p.Add("DES_REGIMEN_LABORAL", dto.DES_REGIMEN_LABORAL);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.RegimenesLaborales.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.RegimenesLaborales.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.RegimenesLaborales.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_REGIMEN_LABORAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.RegimenesLaborales.Delete, new { COD_REGIMEN_LABORAL = COD_REGIMEN_LABORAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.RegimenesLaborales.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.RegimenesLaborales.Delete, new { COD_REGIMEN_LABORAL = COD_REGIMEN_LABORAL })
        };
    }
}

}