using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ClasesProductosRepository : IClasesProductosRepository
{
    private readonly IDbConnectionFactory _db;
    public ClasesProductosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ClasesProductos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ClasesProductos>(SP.SCHEMACostos.ClasesProductos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ClasesProductos?> ShowbyidAsync(string COD_CLASE_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ClasesProductos>(SP.SCHEMACostos.ClasesProductos.Showbyid, new { COD_CLASE_PRODUCTO = COD_CLASE_PRODUCTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMACostos.ClasesProductos.Showcombox);
    }

    public async Task<IEnumerable<ClasesProductos>> SearchAsync(ClasesProductosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ClasesProductos>(SP.SCHEMACostos.ClasesProductos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ClasesProductosInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_PRODUCTO", dto.COD_CLASE_PRODUCTO);
        p.Add("DES_CLASE_PRODUCTO", dto.DES_CLASE_PRODUCTO);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.ClasesProductos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.ClasesProductos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.ClasesProductos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ClasesProductosUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_PRODUCTO", dto.COD_CLASE_PRODUCTO);
        p.Add("DES_CLASE_PRODUCTO", dto.DES_CLASE_PRODUCTO);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.ClasesProductos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.ClasesProductos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.ClasesProductos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CLASE_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.ClasesProductos.Delete, new { COD_CLASE_PRODUCTO = COD_CLASE_PRODUCTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.ClasesProductos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.ClasesProductos.Delete, new { COD_CLASE_PRODUCTO = COD_CLASE_PRODUCTO })
        };
    }
}

}