using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EstructuraProductoRepository : IEstructuraProductoRepository
{
    private readonly IDbConnectionFactory _db;
    public EstructuraProductoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EstructuraProducto>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EstructuraProducto>(SP.SCHEMAEstandar.EstructuraProducto.Showall,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<EstructuraProducto?> ShowbyidAsync(string codGrupoProducto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EstructuraProducto>(SP.SCHEMAEstandar.EstructuraProducto.Showbyid,
            new { COD_GRUPO_PRODUCTO = codGrupoProducto }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.EstructuraProducto.Showcombox);
    }

    public async Task<IEnumerable<EstructuraProducto>> SearchAsync(EstructuraProductoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EstructuraProducto>(SP.SCHEMAEstandar.EstructuraProducto.Search,
            new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EstructuraProductoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_GRUPO_PRODUCTO", dto.COD_GRUPO_PRODUCTO);
        p.Add("FLG_INCLUIR_ALIAS_GRUPO_PRODUCTO", dto.FLG_INCLUIR_ALIAS_GRUPO_PRODUCTO ?? false);
        p.Add("FLG_INCLUIR_FAMILIA_PRODUCTO", dto.FLG_INCLUIR_FAMILIA_PRODUCTO ?? false);
        p.Add("FLG_INCLUIR_ALIAS_FAMILIA_PRODUCTO", dto.FLG_INCLUIR_ALIAS_FAMILIA_PRODUCTO ?? false);
        p.Add("FLG_INCLUIR_SUB_FAMILIA_PRODUCTO", dto.FLG_INCLUIR_SUB_FAMILIA_PRODUCTO ?? false);
        p.Add("FLG_INCLUIR_ALIAS_SUB_FAMILIA_PRODUCTO", dto.FLG_INCLUIR_ALIAS_SUB_FAMILIA_PRODUCTO ?? false);
        p.Add("LNG_CORRELATIVO", dto.LNG_CORRELATIVO);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.EstructuraProducto.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEstandar.EstructuraProducto.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.EstructuraProducto.Insert, p)
            };
        }
    }

    public async Task<WriteResult> UpdateAsync(EstructuraProductoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_GRUPO_PRODUCTO", dto.COD_GRUPO_PRODUCTO);
        p.Add("FLG_INCLUIR_ALIAS_GRUPO_PRODUCTO", dto.FLG_INCLUIR_ALIAS_GRUPO_PRODUCTO ?? false);
        p.Add("FLG_INCLUIR_FAMILIA_PRODUCTO", dto.FLG_INCLUIR_FAMILIA_PRODUCTO ?? false);
        p.Add("FLG_INCLUIR_ALIAS_FAMILIA_PRODUCTO", dto.FLG_INCLUIR_ALIAS_FAMILIA_PRODUCTO ?? false);
        p.Add("FLG_INCLUIR_SUB_FAMILIA_PRODUCTO", dto.FLG_INCLUIR_SUB_FAMILIA_PRODUCTO ?? false);
        p.Add("FLG_INCLUIR_ALIAS_SUB_FAMILIA_PRODUCTO", dto.FLG_INCLUIR_ALIAS_SUB_FAMILIA_PRODUCTO ?? false);
        p.Add("LNG_CORRELATIVO", dto.LNG_CORRELATIVO);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.EstructuraProducto.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEstandar.EstructuraProducto.Update,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.EstructuraProducto.Update, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(string codGrupoProducto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_GRUPO_PRODUCTO", codGrupoProducto);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.EstructuraProducto.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEstandar.EstructuraProducto.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.EstructuraProducto.Delete, p)
            };
        }
    }
}
