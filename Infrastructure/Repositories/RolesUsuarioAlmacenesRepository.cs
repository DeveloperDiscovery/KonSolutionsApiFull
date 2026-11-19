using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class RolesUsuarioAlmacenesRepository : IRolesUsuarioAlmacenesRepository
{
    private readonly IDbConnectionFactory _db;
    public RolesUsuarioAlmacenesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<RolesUsuarioAlmacenes>> ShowallAsync(string COD_ROL_USUARIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RolesUsuarioAlmacenes>(
            SP.SCHEMASeguridad.RolesUsuarioAlmacenes.Showall,
            new { COD_ROL_USUARIO }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>El SP devuelve sólo COD_ALMACEN y DES_ALMACEN; el resto de las propiedades de
    /// la entidad quedan vacías. Se reutiliza el mismo tipo a propósito: la grilla de
    /// disponibles muestra exactamente esas dos columnas.</summary>
    public async Task<IEnumerable<RolesUsuarioAlmacenes>> DisponiblesAsync(string COD_ROL_USUARIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RolesUsuarioAlmacenes>(
            SP.SCHEMASeguridad.RolesUsuarioAlmacenes.Disponibles,
            new { COD_ROL_USUARIO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<RolesUsuarioAlmacenes?> ShowbyidAsync(string COD_ROL_USUARIO, string COD_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RolesUsuarioAlmacenes>(
            SP.SCHEMASeguridad.RolesUsuarioAlmacenes.Showbyid,
            new { COD_ROL_USUARIO, COD_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>COLUMNAS EXPLÍCITAS, no por convención de prefijo.
    ///
    /// Este es un SP de tabla de enlace: su primera columna COD_* es COD_ROL_USUARIO, no
    /// COD_ALMACEN. Leído por convención, el combo mostraba el ROL en todas las filas —"SEG -
    /// JEFE DE SEGURIDAD DE TI" repetido tantas veces como almacenes tenga el rol— y, peor,
    /// mandaba el código del rol como si fuera el del almacén.
    ///
    /// Eso último no se veía: el daño aparecía después, en el combo de Tipos de Movimiento,
    /// que se pide por rol Y ALMACÉN y no devolvía nada porque el "almacén" era un rol.</summary>
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_ROL_USUARIO)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASeguridad.RolesUsuarioAlmacenes.Showcombox,
            columnaId: "COD_ALMACEN", columnaTexto: "DES_ALMACEN", parametros: new { COD_ROL_USUARIO });
    }

    public async Task<IEnumerable<RolesUsuarioAlmacenes>> SearchAsync(RolesUsuarioAlmacenesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RolesUsuarioAlmacenes>(
            SP.SCHEMASeguridad.RolesUsuarioAlmacenes.Search,
            new { COD_ROL_USUARIO = dto.COD_ROL_USUARIO, SEARCH = dto.Search ?? string.Empty },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(RolesUsuarioAlmacenesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ROL_USUARIO", dto.COD_ROL_USUARIO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMASeguridad.RolesUsuarioAlmacenes.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.RolesUsuarioAlmacenes.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.RolesUsuarioAlmacenes.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ROL_USUARIO, string COD_ALMACEN)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMASeguridad.RolesUsuarioAlmacenes.Delete,
            new { COD_ROL_USUARIO, COD_ALMACEN }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.RolesUsuarioAlmacenes.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.RolesUsuarioAlmacenes.Delete, new { COD_ROL_USUARIO, COD_ALMACEN })
        };
    }
}
}
