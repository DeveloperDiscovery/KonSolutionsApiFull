using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Sin colisión de nombres de columna en este SP — Dapper automático alcanza. Sin
/// UpdateAsync: tabla de asignación pura, solo Insert(=Asignar, upsert)/Delete(=Quitar), ver
/// comentario en el Domain Model.</summary>
public class InventariosAlmacenesPuntoTomaPersonalRepository : IInventariosAlmacenesPuntoTomaPersonalRepository
{
    private readonly IDbConnectionFactory _db;
    public InventariosAlmacenesPuntoTomaPersonalRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<InventariosAlmacenesPuntoTomaPersonal>> ShowallAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<InventariosAlmacenesPuntoTomaPersonal>(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaPersonal.Showall,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, NUM_PUNTO_TOMA_INVENTARIO = numPuntoTomaInventario },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<InventariosAlmacenesPuntoTomaPersonal?> ShowbyidAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario, decimal codEntidadAsignada)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<InventariosAlmacenesPuntoTomaPersonal>(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaPersonal.Showbyid,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, NUM_PUNTO_TOMA_INVENTARIO = numPuntoTomaInventario, COD_ENTIDAD_ASIGNADA = codEntidadAsignada },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>Pese al nombre "SHOWCOMBOX", este SP devuelve todas las columnas de la tabla
    /// (igual que SHOWALL), no Id/Texto.</summary>
    public async Task<IEnumerable<InventariosAlmacenesPuntoTomaPersonal>> ShowcomboxAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<InventariosAlmacenesPuntoTomaPersonal>(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaPersonal.Showcombox,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, NUM_PUNTO_TOMA_INVENTARIO = numPuntoTomaInventario },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<InventariosAlmacenesPuntoTomaPersonal>> SearchAsync(InventariosAlmacenesPuntoTomaPersonalSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<InventariosAlmacenesPuntoTomaPersonal>(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaPersonal.Search,
            new { NUM_INVENTARIO = dto.NUM_INVENTARIO, COD_ALMACEN = dto.COD_ALMACEN, NUM_PUNTO_TOMA_INVENTARIO = dto.NUM_PUNTO_TOMA_INVENTARIO, SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>Asignar: el propio SP es un UPSERT (UPDATE de auditoría si ya existe, INSERT si
    /// no) — reasignar algo ya asignado no es un error, es inofensivo.</summary>
    public async Task<WriteResult> InsertAsync(InventariosAlmacenesPuntoTomaPersonalInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_INVENTARIO", dto.NUM_INVENTARIO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("NUM_PUNTO_TOMA_INVENTARIO", dto.NUM_PUNTO_TOMA_INVENTARIO);
        p.Add("COD_ENTIDAD_ASIGNADA", dto.COD_ENTIDAD_ASIGNADA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaPersonal.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaPersonal.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaPersonal.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario, decimal codEntidadAsignada)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaPersonal.Delete,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, NUM_PUNTO_TOMA_INVENTARIO = numPuntoTomaInventario, COD_ENTIDAD_ASIGNADA = codEntidadAsignada },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaPersonal.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosAlmacenesPuntoTomaPersonal.Delete, new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, NUM_PUNTO_TOMA_INVENTARIO = numPuntoTomaInventario, COD_ENTIDAD_ASIGNADA = codEntidadAsignada })
        };
    }
}
}
