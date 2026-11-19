using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposProgramacionRepository : ITiposProgramacionRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposProgramacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposProgramacion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposProgramacion>(SP.SCHEMAPlaneamiento.TiposProgramacion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposProgramacion?> ShowbyidAsync(string COD_TIPO_PROGRAMACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposProgramacion>(SP.SCHEMAPlaneamiento.TiposProgramacion.Showbyid, new { COD_TIPO_PROGRAMACION = COD_TIPO_PROGRAMACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAPlaneamiento.TiposProgramacion.Showcombox);
    }

    public async Task<IEnumerable<TiposProgramacion>> SearchAsync(TiposProgramacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposProgramacion>(SP.SCHEMAPlaneamiento.TiposProgramacion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposProgramacionInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PROGRAMACION", dto.COD_TIPO_PROGRAMACION);
        p.Add("DES_TIPO_PROGRAMACION", dto.DES_TIPO_PROGRAMACION);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION ?? false);
        p.Add("FLG_TIEMPO_MUERTO", dto.FLG_TIEMPO_MUERTO ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.TiposProgramacion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.TiposProgramacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.TiposProgramacion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposProgramacionUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PROGRAMACION", dto.COD_TIPO_PROGRAMACION);
        p.Add("DES_TIPO_PROGRAMACION", dto.DES_TIPO_PROGRAMACION);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION ?? false);
        p.Add("FLG_TIEMPO_MUERTO", dto.FLG_TIEMPO_MUERTO ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.TiposProgramacion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.TiposProgramacion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.TiposProgramacion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_PROGRAMACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.TiposProgramacion.Delete, new { COD_TIPO_PROGRAMACION = COD_TIPO_PROGRAMACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.TiposProgramacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.TiposProgramacion.Delete, new { COD_TIPO_PROGRAMACION = COD_TIPO_PROGRAMACION })
        };
    }
}

}