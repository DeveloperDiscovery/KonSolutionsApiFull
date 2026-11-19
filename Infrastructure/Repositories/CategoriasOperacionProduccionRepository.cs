using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CategoriasOperacionProduccionRepository : ICategoriasOperacionProduccionRepository
{
    private readonly IDbConnectionFactory _db;
    public CategoriasOperacionProduccionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CategoriasOperacionProduccion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CategoriasOperacionProduccion>(SP.SCHEMAProduccion.CategoriasOperacionProduccion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CategoriasOperacionProduccion?> ShowbyidAsync(string COD_CATEGORIA_OPERACION_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CategoriasOperacionProduccion>(SP.SCHEMAProduccion.CategoriasOperacionProduccion.Showbyid, new { COD_CATEGORIA_OPERACION_PRODUCCION = COD_CATEGORIA_OPERACION_PRODUCCION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProduccion.CategoriasOperacionProduccion.Showcombox);
    }

    public async Task<IEnumerable<CategoriasOperacionProduccion>> SearchAsync(CategoriasOperacionProduccionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CategoriasOperacionProduccion>(SP.SCHEMAProduccion.CategoriasOperacionProduccion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CategoriasOperacionProduccionInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CATEGORIA_OPERACION_PRODUCCION", dto.COD_CATEGORIA_OPERACION_PRODUCCION);
        p.Add("DES_CATEGORIA_OPERACION_PRODUCCION", dto.DES_CATEGORIA_OPERACION_PRODUCCION);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.CategoriasOperacionProduccion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.CategoriasOperacionProduccion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.CategoriasOperacionProduccion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CategoriasOperacionProduccionUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CATEGORIA_OPERACION_PRODUCCION", dto.COD_CATEGORIA_OPERACION_PRODUCCION);
        p.Add("DES_CATEGORIA_OPERACION_PRODUCCION", dto.DES_CATEGORIA_OPERACION_PRODUCCION);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.CategoriasOperacionProduccion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.CategoriasOperacionProduccion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.CategoriasOperacionProduccion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CATEGORIA_OPERACION_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.CategoriasOperacionProduccion.Delete, new { COD_CATEGORIA_OPERACION_PRODUCCION = COD_CATEGORIA_OPERACION_PRODUCCION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.CategoriasOperacionProduccion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.CategoriasOperacionProduccion.Delete, new { COD_CATEGORIA_OPERACION_PRODUCCION = COD_CATEGORIA_OPERACION_PRODUCCION })
        };
    }
}

}