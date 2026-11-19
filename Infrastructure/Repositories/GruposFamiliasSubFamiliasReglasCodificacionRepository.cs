using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class GruposFamiliasSubFamiliasReglasCodificacionRepository : IGruposFamiliasSubFamiliasReglasCodificacionRepository
{
    private readonly IDbConnectionFactory _db;
    public GruposFamiliasSubFamiliasReglasCodificacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<GruposFamiliasSubFamiliasReglasCodificacion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GruposFamiliasSubFamiliasReglasCodificacion>(SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasReglasCodificacion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<GruposFamiliasSubFamiliasReglasCodificacion?> ShowbyidAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<GruposFamiliasSubFamiliasReglasCodificacion>(SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasReglasCodificacion.Showbyid, new { COD_GRUPO_PRODUCTO = COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO = COD_FAMILIA_PRODUCTO, COD_SUB_FAMILIA_PRODUCTO = COD_SUB_FAMILIA_PRODUCTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasReglasCodificacion.Showcombox);
    }

    public async Task<IEnumerable<GruposFamiliasSubFamiliasReglasCodificacion>> SearchAsync(GruposFamiliasSubFamiliasReglasCodificacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GruposFamiliasSubFamiliasReglasCodificacion>(SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasReglasCodificacion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(GruposFamiliasSubFamiliasReglasCodificacionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_GRUPO_PRODUCTO", dto.COD_GRUPO_PRODUCTO);
        p.Add("COD_FAMILIA_PRODUCTO", dto.COD_FAMILIA_PRODUCTO);
        p.Add("COD_SUB_FAMILIA_PRODUCTO", dto.COD_SUB_FAMILIA_PRODUCTO);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("COD_GRUPO_ESPECIFICACION_TECNICA", dto.COD_GRUPO_ESPECIFICACION_TECNICA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasReglasCodificacion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasReglasCodificacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasReglasCodificacion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(GruposFamiliasSubFamiliasReglasCodificacionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_GRUPO_PRODUCTO", dto.COD_GRUPO_PRODUCTO);
        p.Add("COD_FAMILIA_PRODUCTO", dto.COD_FAMILIA_PRODUCTO);
        p.Add("COD_SUB_FAMILIA_PRODUCTO", dto.COD_SUB_FAMILIA_PRODUCTO);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("COD_GRUPO_ESPECIFICACION_TECNICA", dto.COD_GRUPO_ESPECIFICACION_TECNICA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasReglasCodificacion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasReglasCodificacion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasReglasCodificacion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasReglasCodificacion.Delete, new { COD_GRUPO_PRODUCTO = COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO = COD_FAMILIA_PRODUCTO, COD_SUB_FAMILIA_PRODUCTO = COD_SUB_FAMILIA_PRODUCTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasReglasCodificacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasReglasCodificacion.Delete, new { COD_GRUPO_PRODUCTO = COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO = COD_FAMILIA_PRODUCTO, COD_SUB_FAMILIA_PRODUCTO = COD_SUB_FAMILIA_PRODUCTO })
        };
    }
}

}