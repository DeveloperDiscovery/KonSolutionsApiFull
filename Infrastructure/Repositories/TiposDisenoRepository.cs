using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposDisenoRepository : ITiposDisenoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposDisenoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposDiseno>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposDiseno>(SP.SCHEMAIngenieria.TiposDiseno.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposDiseno?> ShowbyidAsync(string COD_TIPO_DISENO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposDiseno>(SP.SCHEMAIngenieria.TiposDiseno.Showbyid, new { COD_TIPO_DISENO = COD_TIPO_DISENO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.TiposDiseno.Showcombox);
    }

    public async Task<IEnumerable<TiposDiseno>> SearchAsync(TiposDisenoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposDiseno>(SP.SCHEMAIngenieria.TiposDiseno.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposDisenoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_DISENO", dto.COD_TIPO_DISENO);
        p.Add("DES_TIPO_DISENO", dto.DES_TIPO_DISENO);
        p.Add("FLG_PROPIO", dto.FLG_PROPIO ?? false);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION ?? false);
        p.Add("FLG_MUESTRA", dto.FLG_MUESTRA ?? false);
        p.Add("FLG_COTIZACION", dto.FLG_COTIZACION ?? false);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.TiposDiseno.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.TiposDiseno.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.TiposDiseno.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_DISENO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.TiposDiseno.Delete, new { COD_TIPO_DISENO = COD_TIPO_DISENO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.TiposDiseno.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.TiposDiseno.Delete, new { COD_TIPO_DISENO = COD_TIPO_DISENO })
        };
    }
}

}