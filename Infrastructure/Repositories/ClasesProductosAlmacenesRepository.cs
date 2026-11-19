using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ClasesProductosAlmacenesRepository : IClasesProductosAlmacenesRepository
{
    private readonly IDbConnectionFactory _db;
    public ClasesProductosAlmacenesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ClasesProductosAlmacenes>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ClasesProductosAlmacenes>(SP.SCHEMACostos.ClasesProductosAlmacenes.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ClasesProductosAlmacenes?> ShowbyidAsync(string COD_CLASE_PRODUCTO, string COD_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ClasesProductosAlmacenes>(SP.SCHEMACostos.ClasesProductosAlmacenes.Showbyid, new { COD_CLASE_PRODUCTO = COD_CLASE_PRODUCTO, COD_ALMACEN = COD_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMACostos.ClasesProductosAlmacenes.Showcombox);
    }

    public async Task<IEnumerable<ClasesProductosAlmacenes>> SearchAsync(ClasesProductosAlmacenesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ClasesProductosAlmacenes>(SP.SCHEMACostos.ClasesProductosAlmacenes.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ClasesProductosAlmacenesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_PRODUCTO", dto.COD_CLASE_PRODUCTO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.ClasesProductosAlmacenes.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.ClasesProductosAlmacenes.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.ClasesProductosAlmacenes.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CLASE_PRODUCTO, string COD_ALMACEN)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.ClasesProductosAlmacenes.Delete, new { COD_CLASE_PRODUCTO = COD_CLASE_PRODUCTO, COD_ALMACEN = COD_ALMACEN }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.ClasesProductosAlmacenes.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.ClasesProductosAlmacenes.Delete, new { COD_CLASE_PRODUCTO = COD_CLASE_PRODUCTO, COD_ALMACEN = COD_ALMACEN })
        };
    }
}

}