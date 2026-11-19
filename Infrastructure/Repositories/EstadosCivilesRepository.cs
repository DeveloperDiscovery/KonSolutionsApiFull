using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EstadosCivilesRepository : IEstadosCivilesRepository
{
    private readonly IDbConnectionFactory _db;
    public EstadosCivilesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EstadosCiviles>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EstadosCiviles>(SP.SCHEMARrhh.EstadosCiviles.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EstadosCiviles?> ShowbyidAsync(string COD_ESTADO_CIVIL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EstadosCiviles>(SP.SCHEMARrhh.EstadosCiviles.Showbyid, new { COD_ESTADO_CIVIL = COD_ESTADO_CIVIL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.EstadosCiviles.Showcombox);
    }

    public async Task<IEnumerable<EstadosCiviles>> SearchAsync(EstadosCivilesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EstadosCiviles>(SP.SCHEMARrhh.EstadosCiviles.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EstadosCivilesInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ESTADO_CIVIL", dto.COD_ESTADO_CIVIL);
        p.Add("DES_ESTADO_CIVIL", dto.DES_ESTADO_CIVIL);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EstadosCiviles.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EstadosCiviles.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EstadosCiviles.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EstadosCivilesUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ESTADO_CIVIL", dto.COD_ESTADO_CIVIL);
        p.Add("DES_ESTADO_CIVIL", dto.DES_ESTADO_CIVIL);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EstadosCiviles.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EstadosCiviles.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EstadosCiviles.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ESTADO_CIVIL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.EstadosCiviles.Delete, new { COD_ESTADO_CIVIL = COD_ESTADO_CIVIL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.EstadosCiviles.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.EstadosCiviles.Delete, new { COD_ESTADO_CIVIL = COD_ESTADO_CIVIL })
        };
    }
}

}