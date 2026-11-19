using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Activos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposActivoRepository : ITiposActivoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposActivoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposActivo>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposActivo>(SP.SCHEMAActivos.TiposActivo.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposActivo?> ShowbyidAsync(string COD_TIPO_ACTIVO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposActivo>(SP.SCHEMAActivos.TiposActivo.Showbyid, new { COD_TIPO_ACTIVO = COD_TIPO_ACTIVO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAActivos.TiposActivo.Showcombox);
    }

    public async Task<IEnumerable<TiposActivo>> SearchAsync(TiposActivoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposActivo>(SP.SCHEMAActivos.TiposActivo.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposActivoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_ACTIVO", dto.COD_TIPO_ACTIVO);
        p.Add("DES_TIPO_ACTIVO", dto.DES_TIPO_ACTIVO);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAActivos.TiposActivo.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAActivos.TiposActivo.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAActivos.TiposActivo.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposActivoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_ACTIVO", dto.COD_TIPO_ACTIVO);
        p.Add("DES_TIPO_ACTIVO", dto.DES_TIPO_ACTIVO);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAActivos.TiposActivo.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAActivos.TiposActivo.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAActivos.TiposActivo.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_ACTIVO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAActivos.TiposActivo.Delete, new { COD_TIPO_ACTIVO = COD_TIPO_ACTIVO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAActivos.TiposActivo.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAActivos.TiposActivo.Delete, new { COD_TIPO_ACTIVO = COD_TIPO_ACTIVO })
        };
    }
}

}