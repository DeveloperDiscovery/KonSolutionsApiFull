using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ConfiguracionUsuarioRepository : IConfiguracionUsuarioRepository
{
    private readonly IDbConnectionFactory _db;
    public ConfiguracionUsuarioRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ConfiguracionUsuario>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ConfiguracionUsuario>(
            SP.SCHEMAEstandar.ConfiguracionUsuario.Showall,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<ConfiguracionUsuario?> ShowbyidAsync(string COD_USUARIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ConfiguracionUsuario>(
            SP.SCHEMAEstandar.ConfiguracionUsuario.Showbyid,
            new { COD_USUARIO },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.ConfiguracionUsuario.Showcombox);
    }

    public async Task<IEnumerable<ConfiguracionUsuario>> SearchAsync(ConfiguracionUsuarioSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ConfiguracionUsuario>(
            SP.SCHEMAEstandar.ConfiguracionUsuario.Search,
            new { SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> SaveAsync(ConfiguracionUsuarioSaveDto dto, string codUsuarioSesion, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_USUARIO",                 dto.COD_USUARIO);
        p.Add("TAS_ZOOM_WEB",                dto.TAS_ZOOM_WEB);
        p.Add("NUM_MINUTOS_LOCKOUT_SCREEN",  dto.NUM_MINUTOS_LOCKOUT_SCREEN);
        p.Add("IMG_FONDO_WORKSPACE",         dto.IMG_FONDO_WORKSPACE, DbType.Binary, size: -1);
        p.Add("FLG_NOTIFICACION_ACTIVA", dto.FLG_NOTIFICACION_ACTIVA ?? false);
        p.Add("FLG_USUARIO_SURDO", dto.FLG_USUARIO_SURDO ?? false);
        p.Add("FLG_TECLADO_VIRTUAL", dto.FLG_TECLADO_VIRTUAL ?? false);
        p.Add("NUM_ROWS_PAGE",               dto.NUM_ROWS_PAGE);
        p.Add("COD_TIPO_MENU_OPCIONES",      dto.COD_TIPO_MENU_OPCIONES);
        p.Add("COD_USUARIO_REGISTRO",        codUsuarioSesion);
        p.Add("COD_ESTACION_REGISTRO",       codEstacion);

try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAEstandar.ConfiguracionUsuario.Insert, p,
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.ConfiguracionUsuario.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.ConfiguracionUsuario.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_USUARIO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAEstandar.ConfiguracionUsuario.Delete,
            new { COD_USUARIO },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.ConfiguracionUsuario.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.ConfiguracionUsuario.Delete, new { COD_USUARIO })
        };
    }
}
}
