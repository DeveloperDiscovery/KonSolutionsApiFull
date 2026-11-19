using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposNotificacionAccionesRepository : ITiposNotificacionAccionesRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposNotificacionAccionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposNotificacionAcciones>> ShowallAsync(string codTipoNotificacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposNotificacionAcciones>(SP.SCHEMAEntorno.TiposNotificacionAcciones.Showall,
            new { COD_TIPO_NOTIFICACION = codTipoNotificacion }, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposNotificacionAcciones?> ShowbyidAsync(string codTipoNotificacion, int numSecuencia)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposNotificacionAcciones>(SP.SCHEMAEntorno.TiposNotificacionAcciones.Showbyid,
            new { COD_TIPO_NOTIFICACION = codTipoNotificacion, NUM_SECUENCIA = numSecuencia }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string codTipoNotificacion)
    {
        using var conn = _db.CreateConnection();
        // La clave del combo es NUM_SECUENCIA, no COD_*, así que se usa el overload de
        // ComboboxReader con nombre de columna explícito en vez de la convención por prefijo.
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEntorno.TiposNotificacionAcciones.Showcombox,
            "NUM_SECUENCIA", "DES_ACCION", new { COD_TIPO_NOTIFICACION = codTipoNotificacion });
    }

    public async Task<IEnumerable<TiposNotificacionAcciones>> SearchAsync(TiposNotificacionAccionesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposNotificacionAcciones>(SP.SCHEMAEntorno.TiposNotificacionAcciones.Search,
            new { COD_TIPO_NOTIFICACION = dto.COD_TIPO_NOTIFICACION, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposNotificacionAccionesInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_NOTIFICACION", dto.COD_TIPO_NOTIFICACION);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("DES_ACCION", dto.DES_ACCION);
        p.Add("FLG_LECTURA", dto.FLG_LECTURA);
        p.Add("FLG_APROBAR", dto.FLG_APROBAR);
        p.Add("FLG_RECHAZO", dto.FLG_RECHAZO);
        p.Add("FLG_OBSERVAR", dto.FLG_OBSERVAR);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.TiposNotificacionAcciones.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.TiposNotificacionAcciones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.TiposNotificacionAcciones.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string codTipoNotificacion, int numSecuencia)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.TiposNotificacionAcciones.Delete,
            new { COD_TIPO_NOTIFICACION = codTipoNotificacion, NUM_SECUENCIA = numSecuencia }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.TiposNotificacionAcciones.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.TiposNotificacionAcciones.Delete, new { COD_TIPO_NOTIFICACION = codTipoNotificacion, NUM_SECUENCIA = numSecuencia })
        };
    }
}
}
