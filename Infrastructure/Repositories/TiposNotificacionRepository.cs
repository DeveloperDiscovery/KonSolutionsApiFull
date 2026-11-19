using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposNotificacionRepository : ITiposNotificacionRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposNotificacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposNotificacion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposNotificacion>(
            SP.SCHEMAEntorno.TiposNotificacion.Showall,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposNotificacion?> ShowbyidAsync(string COD_TIPO_NOTIFICACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposNotificacion>(
            SP.SCHEMAEntorno.TiposNotificacion.Showbyid,
            new { COD_TIPO_NOTIFICACION },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEntorno.TiposNotificacion.Showcombox);
    }

    public async Task<IEnumerable<TiposNotificacion>> SearchAsync(TiposNotificacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposNotificacion>(
            SP.SCHEMAEntorno.TiposNotificacion.Search,
            new { SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposNotificacionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_NOTIFICACION", dto.COD_TIPO_NOTIFICACION);
        p.Add("DES_TIPO_NOTIFICACION", dto.DES_TIPO_NOTIFICACION);
        p.Add("FLG_APROBACION", dto.FLG_APROBACION ?? false);
        p.Add("FLG_CALENDARIO", dto.FLG_CALENDARIO ?? false);
        p.Add("FLG_MENSAJE", dto.FLG_MENSAJE ?? false);
        p.Add("COD_TIPO_ESTADO",       dto.COD_TIPO_ESTADO);
        p.Add("IMG_ICONO",             dto.IMG_ICONO, DbType.Binary, size: -1);

try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAEntorno.TiposNotificacion.Insert, p,
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.TiposNotificacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.TiposNotificacion.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_NOTIFICACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAEntorno.TiposNotificacion.Delete,
            new { COD_TIPO_NOTIFICACION },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.TiposNotificacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.TiposNotificacion.Delete, new { COD_TIPO_NOTIFICACION })
        };
    }
}
}
