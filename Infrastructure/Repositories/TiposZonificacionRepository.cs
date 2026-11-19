using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Zonificacion;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposZonificacionRepository : ITiposZonificacionRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposZonificacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposZonificacion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposZonificacion>(SP.SCHEMAZonificacion.TiposZonificacion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposZonificacion?> ShowbyidAsync(string COD_TIPO_ZONIFICACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposZonificacion>(SP.SCHEMAZonificacion.TiposZonificacion.Showbyid, new { COD_TIPO_ZONIFICACION = COD_TIPO_ZONIFICACION }, commandType: CommandType.StoredProcedure);
    }

    // El SP _SHOWCOMBOX devuelve las columnas de la tabla (COD_*/DES_*), no "Id"/"Texto":
    // la traducción a ComboboxItem se hace acá, en el repositorio.
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAZonificacion.TiposZonificacion.Showcombox);
    }

    public async Task<IEnumerable<TiposZonificacion>> SearchAsync(TiposZonificacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposZonificacion>(SP.SCHEMAZonificacion.TiposZonificacion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposZonificacionInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_ZONIFICACION", dto.COD_TIPO_ZONIFICACION);
        p.Add("DES_TIPO_ZONIFICACION", dto.DES_TIPO_ZONIFICACION);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
        // Marca qué tipos de zona llevan mapa (los pisos). El plano se dibuja sobre ellos.
        p.Add("FLG_MAPEABLE", dto.FLG_MAPEABLE);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAZonificacion.TiposZonificacion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAZonificacion.TiposZonificacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAZonificacion.TiposZonificacion.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_ZONIFICACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAZonificacion.TiposZonificacion.Delete, new { COD_TIPO_ZONIFICACION = COD_TIPO_ZONIFICACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAZonificacion.TiposZonificacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAZonificacion.TiposZonificacion.Delete, new { COD_TIPO_ZONIFICACION = COD_TIPO_ZONIFICACION })
        };
    }
}

}