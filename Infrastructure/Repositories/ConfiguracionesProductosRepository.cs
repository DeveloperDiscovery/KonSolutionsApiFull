using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ConfiguracionesProductosRepository : IConfiguracionesProductosRepository
{
    private readonly IDbConnectionFactory _db;
    public ConfiguracionesProductosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ConfiguracionesProductos>> ShowallAsync(string codTipoConfiguracion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ConfiguracionesProductos>(SP.SCHEMAEstandar.ConfiguracionesProductos.Showall,
            new { COD_TIPO_CONFIGURACION = codTipoConfiguracion }, commandType: CommandType.StoredProcedure);
    }

    public async Task<ConfiguracionesProductos?> ShowbyidAsync(string codTipoConfiguracion, short numSecuencia)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ConfiguracionesProductos>(SP.SCHEMAEstandar.ConfiguracionesProductos.Showbyid,
            new { COD_TIPO_CONFIGURACION = codTipoConfiguracion, NUM_SECUENCIA = numSecuencia }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string codTipoConfiguracion)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.ConfiguracionesProductos.Showcombox,
            new { COD_TIPO_CONFIGURACION = codTipoConfiguracion });
    }

    public async Task<IEnumerable<ConfiguracionesProductos>> SearchAsync(string codTipoConfiguracion, string? search)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ConfiguracionesProductos>(SP.SCHEMAEstandar.ConfiguracionesProductos.Search,
            new { COD_TIPO_CONFIGURACION = codTipoConfiguracion, SEARCH = search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ConfiguracionesProductosInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CONFIGURACION", dto.COD_TIPO_CONFIGURACION);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_GRUPO_PRODUCTO", dto.COD_GRUPO_PRODUCTO);
        p.Add("COD_FAMILIA_PRODUCTO", dto.COD_FAMILIA_PRODUCTO);
        p.Add("COD_SUB_FAMILIA_PRODUCTO", dto.COD_SUB_FAMILIA_PRODUCTO);
        p.Add("COD_USUARIO_REGISTRO", dto.COD_USUARIO_REGISTRO);
        p.Add("COD_ESTACION_REGISTRO", dto.COD_ESTACION_REGISTRO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.ConfiguracionesProductos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.ConfiguracionesProductos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.ConfiguracionesProductos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ConfiguracionesProductosUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CONFIGURACION", dto.COD_TIPO_CONFIGURACION);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_GRUPO_PRODUCTO", dto.COD_GRUPO_PRODUCTO);
        p.Add("COD_FAMILIA_PRODUCTO", dto.COD_FAMILIA_PRODUCTO);
        p.Add("COD_SUB_FAMILIA_PRODUCTO", dto.COD_SUB_FAMILIA_PRODUCTO);
        p.Add("COD_USUARIO_REGISTRO", dto.COD_USUARIO_REGISTRO);
        p.Add("COD_ESTACION_REGISTRO", dto.COD_ESTACION_REGISTRO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.ConfiguracionesProductos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.ConfiguracionesProductos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.ConfiguracionesProductos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string codTipoConfiguracion, short numSecuencia)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.ConfiguracionesProductos.Delete,
            new { COD_TIPO_CONFIGURACION = codTipoConfiguracion, NUM_SECUENCIA = numSecuencia }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.ConfiguracionesProductos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.ConfiguracionesProductos.Delete, new { COD_TIPO_CONFIGURACION = codTipoConfiguracion, NUM_SECUENCIA = numSecuencia })
        };
    }
}

}
