using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class GruposPlaneamientoRepository : IGruposPlaneamientoRepository
{
    private readonly IDbConnectionFactory _db;
    public GruposPlaneamientoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<GruposPlaneamiento>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GruposPlaneamiento>(SP.SCHEMAPlaneamiento.GruposPlaneamiento.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<GruposPlaneamiento?> ShowbyidAsync(int COD_GRUPO_PLANEAMIENTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<GruposPlaneamiento>(SP.SCHEMAPlaneamiento.GruposPlaneamiento.Showbyid, new { COD_GRUPO_PLANEAMIENTO = COD_GRUPO_PLANEAMIENTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAPlaneamiento.GruposPlaneamiento.Showcombox);
    }

    public async Task<IEnumerable<GruposPlaneamiento>> SearchAsync(GruposPlaneamientoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GruposPlaneamiento>(SP.SCHEMAPlaneamiento.GruposPlaneamiento.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(GruposPlaneamientoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_GRUPO_PLANEAMIENTO", dto.COD_GRUPO_PLANEAMIENTO);
        p.Add("DES_GRUPO_PLANEAMIENTO", dto.DES_GRUPO_PLANEAMIENTO);
        p.Add("COD_ENTIDAD_CLIENTE", dto.COD_ENTIDAD_CLIENTE);
        p.Add("FEC_DESPACHO_INICIO", dto.FEC_DESPACHO_INICIO);
        p.Add("FEC_DESPACHO_FINAL", dto.FEC_DESPACHO_FINAL);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.GruposPlaneamiento.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.GruposPlaneamiento.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.GruposPlaneamiento.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(GruposPlaneamientoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_GRUPO_PLANEAMIENTO", dto.COD_GRUPO_PLANEAMIENTO);
        p.Add("DES_GRUPO_PLANEAMIENTO", dto.DES_GRUPO_PLANEAMIENTO);
        p.Add("COD_ENTIDAD_CLIENTE", dto.COD_ENTIDAD_CLIENTE);
        p.Add("FEC_DESPACHO_INICIO", dto.FEC_DESPACHO_INICIO);
        p.Add("FEC_DESPACHO_FINAL", dto.FEC_DESPACHO_FINAL);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.GruposPlaneamiento.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.GruposPlaneamiento.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.GruposPlaneamiento.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_GRUPO_PLANEAMIENTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.GruposPlaneamiento.Delete, new { COD_GRUPO_PLANEAMIENTO = COD_GRUPO_PLANEAMIENTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.GruposPlaneamiento.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.GruposPlaneamiento.Delete, new { COD_GRUPO_PLANEAMIENTO = COD_GRUPO_PLANEAMIENTO })
        };
    }
}

}