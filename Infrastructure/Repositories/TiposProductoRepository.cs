using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposProductoRepository : ITiposProductoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposProductoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposProducto>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposProducto>(SP.SCHEMAEstandar.TiposProducto.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposProducto?> ShowbyidAsync(string COD_TIPO_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposProducto>(SP.SCHEMAEstandar.TiposProducto.Showbyid, new { COD_TIPO_PRODUCTO = COD_TIPO_PRODUCTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.TiposProducto.Showcombox);
    }

    public async Task<IEnumerable<TiposProducto>> SearchAsync(TiposProductoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposProducto>(SP.SCHEMAEstandar.TiposProducto.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    // El SP hace upsert (UPDATE, si no encuentra -> INSERT) — ya no existe un
    // PROC_ESTANDAR_TIPOS_PRODUCTO_UPDATE separado, se eliminó UpdateAsync.
    public async Task<WriteResult> InsertAsync(TiposProductoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PRODUCTO", dto.COD_TIPO_PRODUCTO);
        p.Add("DES_TIPO_PRODUCTO", dto.DES_TIPO_PRODUCTO);
        p.Add("FLG_COMPRADO", dto.FLG_COMPRADO ?? false);
        p.Add("FLG_SERVICIO", dto.FLG_SERVICIO ?? false);
        p.Add("FLG_GASTOS", dto.FLG_GASTOS ?? false);
        p.Add("FLG_INDUSTRIALIZADO", dto.FLG_INDUSTRIALIZADO ?? false);
        p.Add("FLG_GENERICO", dto.FLG_GENERICO ?? false);
        p.Add("FLG_DESARROLLO", dto.FLG_DESARROLLO ?? false);
        p.Add("FLG_GASTO_ADUANERO", dto.FLG_GASTO_ADUANERO ?? false);
        p.Add("FLG_FINANCIERO", dto.FLG_FINANCIERO ?? false);
        p.Add("COD_TIPO_ESTADO_INICIAL", dto.COD_TIPO_ESTADO_INICIAL);
        p.Add("COD_ESTADO_INICIAL", dto.COD_ESTADO_INICIAL);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposProducto.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposProducto.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposProducto.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposProducto.Delete, new { COD_TIPO_PRODUCTO = COD_TIPO_PRODUCTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposProducto.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposProducto.Delete, new { COD_TIPO_PRODUCTO = COD_TIPO_PRODUCTO })
        };
    }
}
}
