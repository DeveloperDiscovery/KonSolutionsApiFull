using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TablasEspecificacionesTecnicasRepository : ITablasEspecificacionesTecnicasRepository
{
    private readonly IDbConnectionFactory _db;
    public TablasEspecificacionesTecnicasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TablasEspecificacionesTecnicas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TablasEspecificacionesTecnicas>(SP.SCHEMAIngenieria.TablasEspecificacionesTecnicas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TablasEspecificacionesTecnicas?> ShowbyidAsync(string COD_TABLA_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TablasEspecificacionesTecnicas>(SP.SCHEMAIngenieria.TablasEspecificacionesTecnicas.Showbyid, new { COD_TABLA_ESPECIFICACION_TECNICA = COD_TABLA_ESPECIFICACION_TECNICA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.TablasEspecificacionesTecnicas.Showcombox);
    }

    public async Task<IEnumerable<TablasEspecificacionesTecnicas>> SearchAsync(TablasEspecificacionesTecnicasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TablasEspecificacionesTecnicas>(SP.SCHEMAIngenieria.TablasEspecificacionesTecnicas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TablasEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TABLA_ESPECIFICACION_TECNICA", dto.COD_TABLA_ESPECIFICACION_TECNICA);
        p.Add("DES_FOREING_KEY", dto.DES_FOREING_KEY);
        p.Add("DES_SET", dto.DES_SET);
        p.Add("FLG_HABILITADA", dto.FLG_HABILITADA ?? false);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.TablasEspecificacionesTecnicas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.TablasEspecificacionesTecnicas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.TablasEspecificacionesTecnicas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TablasEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TABLA_ESPECIFICACION_TECNICA", dto.COD_TABLA_ESPECIFICACION_TECNICA);
        p.Add("DES_FOREING_KEY", dto.DES_FOREING_KEY);
        p.Add("DES_SET", dto.DES_SET);
        p.Add("FLG_HABILITADA", dto.FLG_HABILITADA ?? false);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.TablasEspecificacionesTecnicas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.TablasEspecificacionesTecnicas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.TablasEspecificacionesTecnicas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TABLA_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.TablasEspecificacionesTecnicas.Delete, new { COD_TABLA_ESPECIFICACION_TECNICA = COD_TABLA_ESPECIFICACION_TECNICA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.TablasEspecificacionesTecnicas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.TablasEspecificacionesTecnicas.Delete, new { COD_TABLA_ESPECIFICACION_TECNICA = COD_TABLA_ESPECIFICACION_TECNICA })
        };
    }
}

}