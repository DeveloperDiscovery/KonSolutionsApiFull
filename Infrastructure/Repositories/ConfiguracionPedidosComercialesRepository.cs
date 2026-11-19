using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ConfiguracionPedidosComercialesRepository : IConfiguracionPedidosComercialesRepository
{
    private readonly IDbConnectionFactory _db;
    public ConfiguracionPedidosComercialesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ConfiguracionPedidosComerciales>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ConfiguracionPedidosComerciales>(SP.SCHEMAComercial.ConfiguracionPedidosComerciales.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ConfiguracionPedidosComerciales?> ShowbyidAsync(string COD_TIPO_ENTIDAD)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ConfiguracionPedidosComerciales>(SP.SCHEMAComercial.ConfiguracionPedidosComerciales.Showbyid, new { COD_TIPO_ENTIDAD = COD_TIPO_ENTIDAD }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.ConfiguracionPedidosComerciales.Showcombox);
    }

    public async Task<IEnumerable<ConfiguracionPedidosComerciales>> SearchAsync(ConfiguracionPedidosComercialesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ConfiguracionPedidosComerciales>(SP.SCHEMAComercial.ConfiguracionPedidosComerciales.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ConfiguracionPedidosComercialesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_ENTIDAD", dto.COD_TIPO_ENTIDAD);
        p.Add("COD_GRUPO_PRODUCTO", dto.COD_GRUPO_PRODUCTO);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.ConfiguracionPedidosComerciales.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.ConfiguracionPedidosComerciales.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.ConfiguracionPedidosComerciales.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ConfiguracionPedidosComercialesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_ENTIDAD", dto.COD_TIPO_ENTIDAD);
        p.Add("COD_GRUPO_PRODUCTO", dto.COD_GRUPO_PRODUCTO);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.ConfiguracionPedidosComerciales.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.ConfiguracionPedidosComerciales.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.ConfiguracionPedidosComerciales.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_ENTIDAD)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.ConfiguracionPedidosComerciales.Delete, new { COD_TIPO_ENTIDAD = COD_TIPO_ENTIDAD }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.ConfiguracionPedidosComerciales.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.ConfiguracionPedidosComerciales.Delete, new { COD_TIPO_ENTIDAD = COD_TIPO_ENTIDAD })
        };
    }
}

}