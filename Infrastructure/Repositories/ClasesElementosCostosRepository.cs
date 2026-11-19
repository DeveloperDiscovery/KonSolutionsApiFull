using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ClasesElementosCostosRepository : IClasesElementosCostosRepository
{
    private readonly IDbConnectionFactory _db;
    public ClasesElementosCostosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ClasesElementosCostos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ClasesElementosCostos>(SP.SCHEMACostos.ClasesElementosCostos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ClasesElementosCostos?> ShowbyidAsync(string COD_CLASE_COSTO, string COD_ELEMENTO_COSTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ClasesElementosCostos>(SP.SCHEMACostos.ClasesElementosCostos.Showbyid, new { COD_CLASE_COSTO = COD_CLASE_COSTO, COD_ELEMENTO_COSTO = COD_ELEMENTO_COSTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMACostos.ClasesElementosCostos.Showcombox);
    }

    public async Task<IEnumerable<ClasesElementosCostos>> SearchAsync(ClasesElementosCostosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ClasesElementosCostos>(SP.SCHEMACostos.ClasesElementosCostos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ClasesElementosCostosInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_COSTO", dto.COD_CLASE_COSTO);
        p.Add("COD_ELEMENTO_COSTO", dto.COD_ELEMENTO_COSTO);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.ClasesElementosCostos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.ClasesElementosCostos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.ClasesElementosCostos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ClasesElementosCostosUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_COSTO", dto.COD_CLASE_COSTO);
        p.Add("COD_ELEMENTO_COSTO", dto.COD_ELEMENTO_COSTO);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.ClasesElementosCostos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.ClasesElementosCostos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.ClasesElementosCostos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CLASE_COSTO, string COD_ELEMENTO_COSTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.ClasesElementosCostos.Delete, new { COD_CLASE_COSTO = COD_CLASE_COSTO, COD_ELEMENTO_COSTO = COD_ELEMENTO_COSTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.ClasesElementosCostos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.ClasesElementosCostos.Delete, new { COD_CLASE_COSTO = COD_CLASE_COSTO, COD_ELEMENTO_COSTO = COD_ELEMENTO_COSTO })
        };
    }
}

}