using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class InventariosImagenRepository : IInventariosImagenRepository
{
    private readonly IDbConnectionFactory _db;
    public InventariosImagenRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<InventariosImagen>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<InventariosImagen>(SP.SCHEMALogistica.InventariosImagen.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<InventariosImagen?> ShowbyidAsync(int NUM_INVENTARIO, string COD_ALMACEN, string COD_UBICACION_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<InventariosImagen>(SP.SCHEMALogistica.InventariosImagen.Showbyid, new { NUM_INVENTARIO = NUM_INVENTARIO, COD_ALMACEN = COD_ALMACEN, COD_UBICACION_ALMACEN = COD_UBICACION_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.InventariosImagen.Showcombox);
    }

    public async Task<IEnumerable<InventariosImagen>> SearchAsync(InventariosImagenSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<InventariosImagen>(SP.SCHEMALogistica.InventariosImagen.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(InventariosImagenInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_INVENTARIO", dto.NUM_INVENTARIO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("COD_UBICACION_ALMACEN", dto.COD_UBICACION_ALMACEN);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("NUM_LOTE_CONTROL", dto.NUM_LOTE_CONTROL);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("CAN_PRODUCTO_INVENTARIADA", dto.CAN_PRODUCTO_INVENTARIADA);
        p.Add("CAN_PRODUCTO_DIFERENCIA", dto.CAN_PRODUCTO_DIFERENCIA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosImagen.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosImagen.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosImagen.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(InventariosImagenUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_INVENTARIO", dto.NUM_INVENTARIO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("COD_UBICACION_ALMACEN", dto.COD_UBICACION_ALMACEN);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("NUM_LOTE_CONTROL", dto.NUM_LOTE_CONTROL);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("CAN_PRODUCTO_INVENTARIADA", dto.CAN_PRODUCTO_INVENTARIADA);
        p.Add("CAN_PRODUCTO_DIFERENCIA", dto.CAN_PRODUCTO_DIFERENCIA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosImagen.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosImagen.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosImagen.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_INVENTARIO, string COD_ALMACEN, string COD_UBICACION_ALMACEN)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosImagen.Delete, new { NUM_INVENTARIO = NUM_INVENTARIO, COD_ALMACEN = COD_ALMACEN, COD_UBICACION_ALMACEN = COD_UBICACION_ALMACEN }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosImagen.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosImagen.Delete, new { NUM_INVENTARIO = NUM_INVENTARIO, COD_ALMACEN = COD_ALMACEN, COD_UBICACION_ALMACEN = COD_UBICACION_ALMACEN })
        };
    }
}

}