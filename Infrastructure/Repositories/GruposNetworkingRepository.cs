using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Networking;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class GruposNetworkingRepository : IGruposNetworkingRepository
{
    private readonly IDbConnectionFactory _db;
    public GruposNetworkingRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<GruposNetworking>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GruposNetworking>(SP.SCHEMANetworking.GruposNetworking.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<GruposNetworking?> ShowbyidAsync(int NUM_GRUPO_NETWORKING)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<GruposNetworking>(SP.SCHEMANetworking.GruposNetworking.Showbyid, new { NUM_GRUPO_NETWORKING = NUM_GRUPO_NETWORKING }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANetworking.GruposNetworking.Showcombox, "NUM_GRUPO_NETWORKING", "DES_GRUPO_NETWORKING");
    }

    public async Task<IEnumerable<GruposNetworking>> SearchAsync(GruposNetworkingSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GruposNetworking>(SP.SCHEMANetworking.GruposNetworking.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(GruposNetworkingInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_GRUPO_NETWORKING", dto.NUM_GRUPO_NETWORKING);
        p.Add("DES_GRUPO_NETWORKING", dto.DES_GRUPO_NETWORKING);
        p.Add("DES_ALIAS_NETWORKING", dto.DES_ALIAS_NETWORKING);
        p.Add("NUM_UNIDAD_NEGOCIO", dto.NUM_UNIDAD_NEGOCIO);
        p.Add("NUM_CANAL_VENTA", dto.NUM_CANAL_VENTA);
        p.Add("NUM_SUB_CANAL_VENTA", dto.NUM_SUB_CANAL_VENTA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.GruposNetworking.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.GruposNetworking.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.GruposNetworking.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(GruposNetworkingUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_GRUPO_NETWORKING", dto.NUM_GRUPO_NETWORKING);
        p.Add("DES_GRUPO_NETWORKING", dto.DES_GRUPO_NETWORKING);
        p.Add("DES_ALIAS_NETWORKING", dto.DES_ALIAS_NETWORKING);
        p.Add("NUM_UNIDAD_NEGOCIO", dto.NUM_UNIDAD_NEGOCIO);
        p.Add("NUM_CANAL_VENTA", dto.NUM_CANAL_VENTA);
        p.Add("NUM_SUB_CANAL_VENTA", dto.NUM_SUB_CANAL_VENTA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.GruposNetworking.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.GruposNetworking.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.GruposNetworking.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_GRUPO_NETWORKING)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.GruposNetworking.Delete, new { NUM_GRUPO_NETWORKING = NUM_GRUPO_NETWORKING }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.GruposNetworking.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.GruposNetworking.Delete, new { NUM_GRUPO_NETWORKING = NUM_GRUPO_NETWORKING })
        };
    }
}

}