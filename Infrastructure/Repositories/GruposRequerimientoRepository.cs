using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class GruposRequerimientoRepository : IGruposRequerimientoRepository
{
    private readonly IDbConnectionFactory _db;
    public GruposRequerimientoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<GruposRequerimiento>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GruposRequerimiento>(SP.SCHEMAPlaneamiento.GruposRequerimiento.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<GruposRequerimiento?> ShowbyidAsync(int COD_GRUPO_PLANEAMIENTO, int COD_GRUPO_REQUERIMIENTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<GruposRequerimiento>(SP.SCHEMAPlaneamiento.GruposRequerimiento.Showbyid, new { COD_GRUPO_PLANEAMIENTO = COD_GRUPO_PLANEAMIENTO, COD_GRUPO_REQUERIMIENTO = COD_GRUPO_REQUERIMIENTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAPlaneamiento.GruposRequerimiento.Showcombox);
    }

    public async Task<IEnumerable<GruposRequerimiento>> SearchAsync(GruposRequerimientoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GruposRequerimiento>(SP.SCHEMAPlaneamiento.GruposRequerimiento.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(GruposRequerimientoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_GRUPO_PLANEAMIENTO", dto.COD_GRUPO_PLANEAMIENTO);
        p.Add("COD_GRUPO_REQUERIMIENTO", dto.COD_GRUPO_REQUERIMIENTO);
        p.Add("COD_CLASE_PROCESO", dto.COD_CLASE_PROCESO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.GruposRequerimiento.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.GruposRequerimiento.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.GruposRequerimiento.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(GruposRequerimientoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_GRUPO_PLANEAMIENTO", dto.COD_GRUPO_PLANEAMIENTO);
        p.Add("COD_GRUPO_REQUERIMIENTO", dto.COD_GRUPO_REQUERIMIENTO);
        p.Add("COD_CLASE_PROCESO", dto.COD_CLASE_PROCESO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.GruposRequerimiento.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.GruposRequerimiento.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.GruposRequerimiento.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_GRUPO_PLANEAMIENTO, int COD_GRUPO_REQUERIMIENTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.GruposRequerimiento.Delete, new { COD_GRUPO_PLANEAMIENTO = COD_GRUPO_PLANEAMIENTO, COD_GRUPO_REQUERIMIENTO = COD_GRUPO_REQUERIMIENTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.GruposRequerimiento.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.GruposRequerimiento.Delete, new { COD_GRUPO_PLANEAMIENTO = COD_GRUPO_PLANEAMIENTO, COD_GRUPO_REQUERIMIENTO = COD_GRUPO_REQUERIMIENTO })
        };
    }
}

}