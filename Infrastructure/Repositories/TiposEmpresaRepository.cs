using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposEmpresaRepository : ITiposEmpresaRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposEmpresaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposEmpresa>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposEmpresa>(SP.SCHEMAEstandar.TiposEmpresa.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposEmpresa?> ShowbyidAsync(string COD_TIPO_EMPRESA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposEmpresa>(SP.SCHEMAEstandar.TiposEmpresa.Showbyid, new { COD_TIPO_EMPRESA = COD_TIPO_EMPRESA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string? search = null)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.TiposEmpresa.Showcombox, new { SEARCH = search });
    }

    public async Task<IEnumerable<TiposEmpresa>> SearchAsync(TiposEmpresaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposEmpresa>(SP.SCHEMAEstandar.TiposEmpresa.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposEmpresaInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_EMPRESA", dto.COD_TIPO_EMPRESA);
        p.Add("DES_TIPO_EMPRESA", dto.DES_TIPO_EMPRESA);
        p.Add("FLG_PRIVADA", dto.FLG_PRIVADA ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposEmpresa.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposEmpresa.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposEmpresa.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposEmpresaUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_EMPRESA", dto.COD_TIPO_EMPRESA);
        p.Add("DES_TIPO_EMPRESA", dto.DES_TIPO_EMPRESA);
        p.Add("FLG_PRIVADA", dto.FLG_PRIVADA ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposEmpresa.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposEmpresa.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposEmpresa.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_EMPRESA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposEmpresa.Delete, new { COD_TIPO_EMPRESA = COD_TIPO_EMPRESA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposEmpresa.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposEmpresa.Delete, new { COD_TIPO_EMPRESA = COD_TIPO_EMPRESA })
        };
    }
}

}