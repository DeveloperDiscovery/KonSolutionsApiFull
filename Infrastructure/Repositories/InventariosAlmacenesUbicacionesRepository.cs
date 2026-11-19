using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class InventariosAlmacenesUbicacionesRepository : IInventariosAlmacenesUbicacionesRepository
{
    private readonly IDbConnectionFactory _db;
    public InventariosAlmacenesUbicacionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<InventariosAlmacenesUbicaciones>> ShowallAsync(int numInventario, string codAlmacen)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<InventariosAlmacenesUbicaciones>(SP.SCHEMALogistica.InventariosAlmacenesUbicaciones.Showall,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen }, commandType: CommandType.StoredProcedure);
    }

    public async Task<InventariosAlmacenesUbicaciones?> ShowbyidAsync(int numInventario, string codAlmacen, string codUbicacionAlmacen)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<InventariosAlmacenesUbicaciones>(SP.SCHEMALogistica.InventariosAlmacenesUbicaciones.Showbyid,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, COD_UBICACION_ALMACEN = codUbicacionAlmacen }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(int numInventario, string codAlmacen)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.InventariosAlmacenesUbicaciones.Showcombox,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen });
    }

    public async Task<IEnumerable<InventariosAlmacenesUbicaciones>> SearchAsync(InventariosAlmacenesUbicacionesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<InventariosAlmacenesUbicaciones>(SP.SCHEMALogistica.InventariosAlmacenesUbicaciones.Search,
            new { NUM_INVENTARIO = dto.NUM_INVENTARIO, COD_ALMACEN = dto.COD_ALMACEN, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>El SP _INSERT es upsert (actualiza si ya existe, inserta si no) — no hay SP _UPDATE
    /// para esta tabla de asignación pura.</summary>
    public async Task<WriteResult> InsertAsync(InventariosAlmacenesUbicacionesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_INVENTARIO", dto.NUM_INVENTARIO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("COD_UBICACION_ALMACEN", dto.COD_UBICACION_ALMACEN);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosAlmacenesUbicaciones.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosAlmacenesUbicaciones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosAlmacenesUbicaciones.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int numInventario, string codAlmacen, string codUbicacionAlmacen)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosAlmacenesUbicaciones.Delete,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, COD_UBICACION_ALMACEN = codUbicacionAlmacen }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosAlmacenesUbicaciones.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosAlmacenesUbicaciones.Delete, new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, COD_UBICACION_ALMACEN = codUbicacionAlmacen })
        };
    }
}
}
