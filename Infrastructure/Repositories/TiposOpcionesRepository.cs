using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposOpcionesRepository : ITiposOpcionesRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposOpcionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposOpciones>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposOpciones>(SP.SCHEMASeguridad.TiposOpciones.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposOpciones?> ShowbyidAsync(string COD_TIPO_OPCION_APLICACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposOpciones>(SP.SCHEMASeguridad.TiposOpciones.Showbyid, new { COD_TIPO_OPCION_APLICACION = COD_TIPO_OPCION_APLICACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASeguridad.TiposOpciones.Showcombox);
    }

    public async Task<IEnumerable<TiposOpciones>> SearchAsync(TiposOpcionesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposOpciones>(SP.SCHEMASeguridad.TiposOpciones.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposOpcionesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_OPCION_APLICACION", dto.COD_TIPO_OPCION_APLICACION);
        p.Add("DES_TIPO_OPCION_APLICACION", dto.DES_TIPO_OPCION_APLICACION);
        p.Add("FLG_MODULO", dto.FLG_MODULO ?? false);
        p.Add("FLG_SEPARADOR", dto.FLG_SEPARADOR ?? false);
        p.Add("FLG_CARPETA", dto.FLG_CARPETA ?? false);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.TiposOpciones.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.TiposOpciones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.TiposOpciones.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposOpcionesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_OPCION_APLICACION", dto.COD_TIPO_OPCION_APLICACION);
        p.Add("DES_TIPO_OPCION_APLICACION", dto.DES_TIPO_OPCION_APLICACION);
        p.Add("FLG_MODULO", dto.FLG_MODULO ?? false);
        p.Add("FLG_SEPARADOR", dto.FLG_SEPARADOR ?? false);
        p.Add("FLG_CARPETA", dto.FLG_CARPETA ?? false);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.TiposOpciones.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.TiposOpciones.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.TiposOpciones.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_OPCION_APLICACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.TiposOpciones.Delete, new { COD_TIPO_OPCION_APLICACION = COD_TIPO_OPCION_APLICACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.TiposOpciones.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.TiposOpciones.Delete, new { COD_TIPO_OPCION_APLICACION = COD_TIPO_OPCION_APLICACION })
        };
    }
}

}