using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Networking;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class GruposNetworkingAsesoresComerialesRepository : IGruposNetworkingAsesoresComerialesRepository
{
    private readonly IDbConnectionFactory _db;
    public GruposNetworkingAsesoresComerialesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<GruposNetworkingAsesoresComeriales>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GruposNetworkingAsesoresComeriales>(SP.SCHEMANetworking.GruposNetworkingAsesoresComeriales.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<GruposNetworkingAsesoresComeriales?> ShowbyidAsync(int NUM_GRUPO_NETWORKING, decimal COD_ENTIDAD_ASESOR_COMERCIAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<GruposNetworkingAsesoresComeriales>(SP.SCHEMANetworking.GruposNetworkingAsesoresComeriales.Showbyid, new { NUM_GRUPO_NETWORKING = NUM_GRUPO_NETWORKING, COD_ENTIDAD_ASESOR_COMERCIAL = COD_ENTIDAD_ASESOR_COMERCIAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANetworking.GruposNetworkingAsesoresComeriales.Showcombox, "NUM_GRUPO_NETWORKING", "DES_COMERCIAL");
    }

    public async Task<IEnumerable<GruposNetworkingAsesoresComeriales>> SearchAsync(GruposNetworkingAsesoresComerialesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GruposNetworkingAsesoresComeriales>(SP.SCHEMANetworking.GruposNetworkingAsesoresComeriales.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(GruposNetworkingAsesoresComerialesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_GRUPO_NETWORKING", dto.NUM_GRUPO_NETWORKING);
        p.Add("COD_ENTIDAD_ASESOR_COMERCIAL", dto.COD_ENTIDAD_ASESOR_COMERCIAL);
        p.Add("FEC_INGRESO", dto.FEC_INGRESO);
        p.Add("FEC_SALIDA", dto.FEC_SALIDA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.GruposNetworkingAsesoresComeriales.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.GruposNetworkingAsesoresComeriales.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.GruposNetworkingAsesoresComeriales.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(GruposNetworkingAsesoresComerialesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_GRUPO_NETWORKING", dto.NUM_GRUPO_NETWORKING);
        p.Add("COD_ENTIDAD_ASESOR_COMERCIAL", dto.COD_ENTIDAD_ASESOR_COMERCIAL);
        p.Add("FEC_INGRESO", dto.FEC_INGRESO);
        p.Add("FEC_SALIDA", dto.FEC_SALIDA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.GruposNetworkingAsesoresComeriales.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.GruposNetworkingAsesoresComeriales.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.GruposNetworkingAsesoresComeriales.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_GRUPO_NETWORKING, decimal COD_ENTIDAD_ASESOR_COMERCIAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.GruposNetworkingAsesoresComeriales.Delete, new { NUM_GRUPO_NETWORKING = NUM_GRUPO_NETWORKING, COD_ENTIDAD_ASESOR_COMERCIAL = COD_ENTIDAD_ASESOR_COMERCIAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.GruposNetworkingAsesoresComeriales.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.GruposNetworkingAsesoresComeriales.Delete, new { NUM_GRUPO_NETWORKING = NUM_GRUPO_NETWORKING, COD_ENTIDAD_ASESOR_COMERCIAL = COD_ENTIDAD_ASESOR_COMERCIAL })
        };
    }
}

}