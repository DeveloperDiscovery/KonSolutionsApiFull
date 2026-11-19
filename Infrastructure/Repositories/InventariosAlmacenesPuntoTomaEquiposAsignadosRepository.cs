using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Sin colisión de nombres de columna en estos SPs (ver comentario en el Domain
/// Model) — Dapper automático (QueryAsync&lt;T&gt;) alcanza.</summary>
public class InventariosAlmacenesPuntoTomaEquiposAsignadosRepository : IInventariosAlmacenesPuntoTomaEquiposAsignadosRepository
{
    private readonly IDbConnectionFactory _db;
    public InventariosAlmacenesPuntoTomaEquiposAsignadosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<InventariosAlmacenesPuntoTomaEquiposAsignados>> ShowallAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<InventariosAlmacenesPuntoTomaEquiposAsignados>(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaEquiposAsignados.Showall,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, NUM_PUNTO_TOMA_INVENTARIO = numPuntoTomaInventario },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<InventariosAlmacenesPuntoTomaEquiposAsignados?> ShowbyidAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario, string codProductoEquipo)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<InventariosAlmacenesPuntoTomaEquiposAsignados>(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaEquiposAsignados.Showbyid,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, NUM_PUNTO_TOMA_INVENTARIO = numPuntoTomaInventario, COD_PRODUCTO_EQUIPO = codProductoEquipo },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>Pese al nombre "SHOWCOMBOX", este SP devuelve todas las columnas de la tabla
    /// (igual que SHOWALL), no Id/Texto.</summary>
    public async Task<IEnumerable<InventariosAlmacenesPuntoTomaEquiposAsignados>> ShowcomboxAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<InventariosAlmacenesPuntoTomaEquiposAsignados>(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaEquiposAsignados.Showcombox,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, NUM_PUNTO_TOMA_INVENTARIO = numPuntoTomaInventario },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<InventariosAlmacenesPuntoTomaEquiposAsignados>> SearchAsync(InventariosAlmacenesPuntoTomaEquiposAsignadosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<InventariosAlmacenesPuntoTomaEquiposAsignados>(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaEquiposAsignados.Search,
            new { NUM_INVENTARIO = dto.NUM_INVENTARIO, COD_ALMACEN = dto.COD_ALMACEN, NUM_PUNTO_TOMA_INVENTARIO = dto.NUM_PUNTO_TOMA_INVENTARIO, SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>Alta pura: si la clave ya existe, el SP lanza KEY_REGISTRO_EXISTENTE.</summary>
    public async Task<WriteResult> InsertAsync(InventariosAlmacenesPuntoTomaEquiposAsignadosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_INVENTARIO", dto.NUM_INVENTARIO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("NUM_PUNTO_TOMA_INVENTARIO", dto.NUM_PUNTO_TOMA_INVENTARIO);
        p.Add("COD_PRODUCTO_EQUIPO", dto.COD_PRODUCTO_EQUIPO);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaEquiposAsignados.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaEquiposAsignados.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaEquiposAsignados.Insert, p)
        };
    }
}

    /// <summary>Modificación: solo actualiza CAN_PRODUCTO + auditoría, la clave no se toca.</summary>
    public async Task<WriteResult> UpdateAsync(InventariosAlmacenesPuntoTomaEquiposAsignadosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_INVENTARIO", dto.NUM_INVENTARIO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("NUM_PUNTO_TOMA_INVENTARIO", dto.NUM_PUNTO_TOMA_INVENTARIO);
        p.Add("COD_PRODUCTO_EQUIPO", dto.COD_PRODUCTO_EQUIPO);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaEquiposAsignados.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaEquiposAsignados.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaEquiposAsignados.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario, string codProductoEquipo)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaEquiposAsignados.Delete,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, NUM_PUNTO_TOMA_INVENTARIO = numPuntoTomaInventario, COD_PRODUCTO_EQUIPO = codProductoEquipo },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaEquiposAsignados.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaEquiposAsignados.Delete, new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, NUM_PUNTO_TOMA_INVENTARIO = numPuntoTomaInventario, COD_PRODUCTO_EQUIPO = codProductoEquipo })
        };
    }
}
}
