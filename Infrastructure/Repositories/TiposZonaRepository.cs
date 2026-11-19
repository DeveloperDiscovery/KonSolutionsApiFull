using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposZonaRepository : ITiposZonaRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposZonaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposZona>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposZona>(SP.SCHEMARrhh.TiposZona.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposZona?> ShowbyidAsync(string COD_TIPO_ZONA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposZona>(SP.SCHEMARrhh.TiposZona.Showbyid, new { COD_TIPO_ZONA = COD_TIPO_ZONA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.TiposZona.Showcombox);
    }

    public async Task<IEnumerable<TiposZona>> SearchAsync(TiposZonaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposZona>(SP.SCHEMARrhh.TiposZona.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposZonaInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_ZONA", dto.COD_TIPO_ZONA);
        p.Add("DES_TIPO_ZONA", dto.DES_TIPO_ZONA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposZona.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposZona.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposZona.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposZonaUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_ZONA", dto.COD_TIPO_ZONA);
        p.Add("DES_TIPO_ZONA", dto.DES_TIPO_ZONA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposZona.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposZona.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposZona.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_ZONA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposZona.Delete, new { COD_TIPO_ZONA = COD_TIPO_ZONA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposZona.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposZona.Delete, new { COD_TIPO_ZONA = COD_TIPO_ZONA })
        };
    }
}

}