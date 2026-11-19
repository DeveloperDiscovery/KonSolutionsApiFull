using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class InventariosAlmacenesProductosRepository : IInventariosAlmacenesProductosRepository
{
    private readonly IDbConnectionFactory _db;
    public InventariosAlmacenesProductosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<InventariosAlmacenesProductos>> ShowallAsync(int numInventario, string codAlmacen)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<InventariosAlmacenesProductos>(SP.SCHEMALogistica.InventariosAlmacenesProductos.Showall,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen }, commandType: CommandType.StoredProcedure);
    }

    public async Task<InventariosAlmacenesProductos?> ShowbyidAsync(int numInventario, string codAlmacen, string codProducto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<InventariosAlmacenesProductos>(SP.SCHEMALogistica.InventariosAlmacenesProductos.Showbyid,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, COD_PRODUCTO = codProducto }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(int numInventario, string codAlmacen)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.InventariosAlmacenesProductos.Showcombox,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen });
    }

    public async Task<IEnumerable<InventariosAlmacenesProductos>> SearchAsync(InventariosAlmacenesProductosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<InventariosAlmacenesProductos>(SP.SCHEMALogistica.InventariosAlmacenesProductos.Search,
            new { NUM_INVENTARIO = dto.NUM_INVENTARIO, COD_ALMACEN = dto.COD_ALMACEN, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>El SP _INSERT es upsert — no hay SP _UPDATE para esta tabla de asignación pura.</summary>
    public async Task<WriteResult> InsertAsync(InventariosAlmacenesProductosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_INVENTARIO", dto.NUM_INVENTARIO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosAlmacenesProductos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosAlmacenesProductos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosAlmacenesProductos.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int numInventario, string codAlmacen, string codProducto)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosAlmacenesProductos.Delete,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, COD_PRODUCTO = codProducto }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosAlmacenesProductos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosAlmacenesProductos.Delete, new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, COD_PRODUCTO = codProducto })
        };
    }
}
}
