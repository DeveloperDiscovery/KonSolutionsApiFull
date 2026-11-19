using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposEspecificacionTecnicaRepository : ITiposEspecificacionTecnicaRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposEspecificacionTecnicaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposEspecificacionTecnica>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposEspecificacionTecnica>(SP.SCHEMAIngenieria.TiposEspecificacionTecnica.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposEspecificacionTecnica?> ShowbyidAsync(string COD_TIPO_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposEspecificacionTecnica>(SP.SCHEMAIngenieria.TiposEspecificacionTecnica.Showbyid, new { COD_TIPO_ESPECIFICACION_TECNICA = COD_TIPO_ESPECIFICACION_TECNICA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.TiposEspecificacionTecnica.Showcombox);
    }

    public async Task<IEnumerable<TiposEspecificacionTecnica>> SearchAsync(TiposEspecificacionTecnicaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposEspecificacionTecnica>(SP.SCHEMAIngenieria.TiposEspecificacionTecnica.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposEspecificacionTecnicaInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_ESPECIFICACION_TECNICA", dto.COD_TIPO_ESPECIFICACION_TECNICA);
        p.Add("DES_TIPO_ESPECIFICACION_TECNICA", dto.DES_TIPO_ESPECIFICACION_TECNICA);
        p.Add("FLG_LISTA", dto.FLG_LISTA ?? false);
        p.Add("FLG_ENTERO", dto.FLG_ENTERO ?? false);
        p.Add("FLG_NUMERICO", dto.FLG_NUMERICO ?? false);
        p.Add("FLG_CADENA", dto.FLG_CADENA ?? false);
        p.Add("FLG_FECHA_HORA", dto.FLG_FECHA_HORA ?? false);
        p.Add("FLG_FECHA", dto.FLG_FECHA ?? false);
        p.Add("FLG_HORA", dto.FLG_HORA ?? false);
        p.Add("FLG_LOGICO", dto.FLG_LOGICO ?? false);
        p.Add("FLG_COLOR", dto.FLG_COLOR ?? false);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.TiposEspecificacionTecnica.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.TiposEspecificacionTecnica.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.TiposEspecificacionTecnica.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposEspecificacionTecnicaUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_ESPECIFICACION_TECNICA", dto.COD_TIPO_ESPECIFICACION_TECNICA);
        p.Add("DES_TIPO_ESPECIFICACION_TECNICA", dto.DES_TIPO_ESPECIFICACION_TECNICA);
        p.Add("FLG_LISTA", dto.FLG_LISTA ?? false);
        p.Add("FLG_ENTERO", dto.FLG_ENTERO ?? false);
        p.Add("FLG_NUMERICO", dto.FLG_NUMERICO ?? false);
        p.Add("FLG_CADENA", dto.FLG_CADENA ?? false);
        p.Add("FLG_FECHA_HORA", dto.FLG_FECHA_HORA ?? false);
        p.Add("FLG_FECHA", dto.FLG_FECHA ?? false);
        p.Add("FLG_HORA", dto.FLG_HORA ?? false);
        p.Add("FLG_LOGICO", dto.FLG_LOGICO ?? false);
        p.Add("FLG_COLOR", dto.FLG_COLOR ?? false);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.TiposEspecificacionTecnica.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.TiposEspecificacionTecnica.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.TiposEspecificacionTecnica.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.TiposEspecificacionTecnica.Delete, new { COD_TIPO_ESPECIFICACION_TECNICA = COD_TIPO_ESPECIFICACION_TECNICA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.TiposEspecificacionTecnica.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.TiposEspecificacionTecnica.Delete, new { COD_TIPO_ESPECIFICACION_TECNICA = COD_TIPO_ESPECIFICACION_TECNICA })
        };
    }
}

}