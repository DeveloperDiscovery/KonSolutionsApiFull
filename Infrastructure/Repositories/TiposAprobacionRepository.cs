using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Aprobaciones;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposAprobacionRepository : ITiposAprobacionRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposAprobacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposAprobacion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposAprobacion>(SP.SCHEMAAprobaciones.TiposAprobacion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposAprobacion?> ShowbyidAsync(string COD_TIPO_APROBACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposAprobacion>(SP.SCHEMAAprobaciones.TiposAprobacion.Showbyid, new { COD_TIPO_APROBACION = COD_TIPO_APROBACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAAprobaciones.TiposAprobacion.Showcombox);
    }

    public async Task<IEnumerable<TiposAprobacion>> SearchAsync(TiposAprobacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposAprobacion>(SP.SCHEMAAprobaciones.TiposAprobacion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposAprobacionInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_APROBACION", dto.COD_TIPO_APROBACION);
        p.Add("DES_TIPO_APROBACION", dto.DES_TIPO_APROBACION);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAAprobaciones.TiposAprobacion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAAprobaciones.TiposAprobacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAAprobaciones.TiposAprobacion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo juego de parámetros que el alta: el SP _UPDATE se
    /// genera con el mismo contrato que el _INSERT.</summary>
    public async Task<WriteResult> UpdateAsync(TiposAprobacionUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_APROBACION", dto.COD_TIPO_APROBACION);
        p.Add("DES_TIPO_APROBACION", dto.DES_TIPO_APROBACION);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAAprobaciones.TiposAprobacion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAAprobaciones.TiposAprobacion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAAprobaciones.TiposAprobacion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_APROBACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAAprobaciones.TiposAprobacion.Delete, new { COD_TIPO_APROBACION = COD_TIPO_APROBACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAAprobaciones.TiposAprobacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAAprobaciones.TiposAprobacion.Delete, new { COD_TIPO_APROBACION = COD_TIPO_APROBACION })
        };
    }
}

}