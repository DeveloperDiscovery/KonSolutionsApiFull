using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Networking;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ConfiguracionRepository : IConfiguracionRepository
{
    private readonly IDbConnectionFactory _db;
    public ConfiguracionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Configuracion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Configuracion>(SP.SCHEMANetworking.Configuracion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Configuracion?> ShowbyidAsync(int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Configuracion>(SP.SCHEMANetworking.Configuracion.Showbyid, new { NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANetworking.Configuracion.Showcombox);
    }

    public async Task<IEnumerable<Configuracion>> SearchAsync(ConfiguracionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Configuracion>(SP.SCHEMANetworking.Configuracion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ConfiguracionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_TIPO_ENTIDAD_ASESOR_COMERCIAL", dto.COD_TIPO_ENTIDAD_ASESOR_COMERCIAL);
        p.Add("COD_TIPO_PERIODO_LIQUIDACION", dto.COD_TIPO_PERIODO_LIQUIDACION);
        p.Add("COD_TIPO_PERIODO_VENTAS", dto.COD_TIPO_PERIODO_VENTAS);
        p.Add("DES_GRUPO_NETWORKING", dto.DES_GRUPO_NETWORKING);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.Configuracion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.Configuracion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.Configuracion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ConfiguracionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_TIPO_ENTIDAD_ASESOR_COMERCIAL", dto.COD_TIPO_ENTIDAD_ASESOR_COMERCIAL);
        p.Add("COD_TIPO_PERIODO_LIQUIDACION", dto.COD_TIPO_PERIODO_LIQUIDACION);
        p.Add("COD_TIPO_PERIODO_VENTAS", dto.COD_TIPO_PERIODO_VENTAS);
        p.Add("DES_GRUPO_NETWORKING", dto.DES_GRUPO_NETWORKING);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.Configuracion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.Configuracion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.Configuracion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.Configuracion.Delete, new { NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.Configuracion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.Configuracion.Delete, new { NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}