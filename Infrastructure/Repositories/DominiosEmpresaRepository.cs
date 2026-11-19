using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class DominiosEmpresaRepository : IDominiosEmpresaRepository
{
    private readonly IDbConnectionFactory _db;
    public DominiosEmpresaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<DominiosEmpresa>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DominiosEmpresa>(SP.SCHEMAEntorno.DominiosEmpresa.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<DominiosEmpresa?> ShowbyidAsync(byte COD_DOMINIO_EMPRESA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<DominiosEmpresa>(SP.SCHEMAEntorno.DominiosEmpresa.Showbyid, new { COD_DOMINIO_EMPRESA = COD_DOMINIO_EMPRESA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<DominiosEmpresa>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DominiosEmpresa>(SP.SCHEMAEntorno.DominiosEmpresa.Showcombox, commandType: CommandType.StoredProcedure);
    }

    /// <summary>COD_DOMINIO_EMPRESA se envía como 0 al crear — el SP INSERT hace UPDATE
    /// primero (0 no matchea ningún código existente) y cae al INSERT, devolviendo el
    /// SCOPE_IDENTITY() asignado como resultset.</summary>
    public async Task<WriteResult> InsertAsync(DominiosEmpresaInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_DOMINIO_EMPRESA", dto.COD_DOMINIO_EMPRESA);
        p.Add("DES_DOMINIO_EMPRESA", dto.DES_DOMINIO_EMPRESA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.DominiosEmpresa.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.DominiosEmpresa.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.DominiosEmpresa.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(byte COD_DOMINIO_EMPRESA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.DominiosEmpresa.Delete, new { COD_DOMINIO_EMPRESA = COD_DOMINIO_EMPRESA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.DominiosEmpresa.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.DominiosEmpresa.Delete, new { COD_DOMINIO_EMPRESA = COD_DOMINIO_EMPRESA })
        };
    }
}
}
