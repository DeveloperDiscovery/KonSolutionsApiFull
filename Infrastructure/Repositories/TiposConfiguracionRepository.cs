using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposConfiguracionRepository : ITiposConfiguracionRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposConfiguracionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposConfiguracion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposConfiguracion>(SP.SCHEMAEstandar.TiposConfiguracion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposConfiguracion?> ShowbyidAsync(string COD_TIPO_CONFIGURACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposConfiguracion>(SP.SCHEMAEstandar.TiposConfiguracion.Showbyid, new { COD_TIPO_CONFIGURACION = COD_TIPO_CONFIGURACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.TiposConfiguracion.Showcombox);
    }

    public async Task<WriteResult> InsertAsync(TiposConfiguracionInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CONFIGURACION", dto.COD_TIPO_CONFIGURACION);
        p.Add("DES_TIPO_CONFIGURACION", dto.DES_TIPO_CONFIGURACION);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposConfiguracion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposConfiguracion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposConfiguracion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposConfiguracionUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CONFIGURACION", dto.COD_TIPO_CONFIGURACION);
        p.Add("DES_TIPO_CONFIGURACION", dto.DES_TIPO_CONFIGURACION);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposConfiguracion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposConfiguracion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposConfiguracion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_CONFIGURACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposConfiguracion.Delete, new { COD_TIPO_CONFIGURACION = COD_TIPO_CONFIGURACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposConfiguracion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposConfiguracion.Delete, new { COD_TIPO_CONFIGURACION = COD_TIPO_CONFIGURACION })
        };
    }
}

}
