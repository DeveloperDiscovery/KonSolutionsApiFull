using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class RolesUsuarioAlmacenesTiposMovimientoRepository : IRolesUsuarioAlmacenesTiposMovimientoRepository
{
    private readonly IDbConnectionFactory _db;
    public RolesUsuarioAlmacenesTiposMovimientoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<RolesUsuarioAlmacenesTiposMovimiento>> ShowallAsync(string COD_ROL_USUARIO, string COD_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RolesUsuarioAlmacenesTiposMovimiento>(
            SP.SCHEMASeguridad.RolesUsuarioAlmacenesTiposMovimiento.Showall,
            new { COD_ROL_USUARIO, COD_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Tipos habilitados en el almacén que el rol aún no tiene.
    ///
    /// OJO: el SP _DISPONIBLES no trae DES_TIPO_MOVIMIENTO ni DES_CLASE_MOVIMIENTO —sólo los
    /// códigos—, así que la grilla cae al código cuando falta la descripción. Se acompaña un
    /// parche SQL que agrega esos dos campos al SELECT.</summary>
    public async Task<IEnumerable<RolesUsuarioAlmacenesTiposMovimiento>> DisponiblesAsync(string COD_ROL_USUARIO, string COD_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RolesUsuarioAlmacenesTiposMovimiento>(
            SP.SCHEMASeguridad.RolesUsuarioAlmacenesTiposMovimiento.Disponibles,
            new { COD_ROL_USUARIO, COD_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    public async Task<RolesUsuarioAlmacenesTiposMovimiento?> ShowbyidAsync(string COD_ROL_USUARIO, string COD_ALMACEN, string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RolesUsuarioAlmacenesTiposMovimiento>(
            SP.SCHEMASeguridad.RolesUsuarioAlmacenesTiposMovimiento.Showbyid,
            new { COD_ROL_USUARIO, COD_ALMACEN, COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>Columnas explícitas, por lo mismo que en RolesUsuarioAlmacenes: la primera
    /// COD_* de este SP de enlace es COD_ROL_USUARIO. Por convención, el combo mostraba el rol
    /// y mandaba su código como si fuera el del tipo de movimiento.</summary>
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_ROL_USUARIO, string COD_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn,
            SP.SCHEMASeguridad.RolesUsuarioAlmacenesTiposMovimiento.Showcombox,
            columnaId: "COD_TIPO_MOVIMIENTO", columnaTexto: "DES_TIPO_MOVIMIENTO",
            parametros: new { COD_ROL_USUARIO, COD_ALMACEN });
    }

    public async Task<IEnumerable<RolesUsuarioAlmacenesTiposMovimiento>> SearchAsync(RolesUsuarioAlmacenesTiposMovimientoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RolesUsuarioAlmacenesTiposMovimiento>(
            SP.SCHEMASeguridad.RolesUsuarioAlmacenesTiposMovimiento.Search,
            new { COD_ROL_USUARIO = dto.COD_ROL_USUARIO, COD_ALMACEN = dto.COD_ALMACEN, SEARCH = dto.Search ?? string.Empty },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(RolesUsuarioAlmacenesTiposMovimientoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ROL_USUARIO", dto.COD_ROL_USUARIO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("COD_CLASE_MOVIMIENTO", dto.COD_CLASE_MOVIMIENTO);
        p.Add("COD_TIPO_MOVIMIENTO", dto.COD_TIPO_MOVIMIENTO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMASeguridad.RolesUsuarioAlmacenesTiposMovimiento.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.RolesUsuarioAlmacenesTiposMovimiento.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.RolesUsuarioAlmacenesTiposMovimiento.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ROL_USUARIO, string COD_ALMACEN, string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMASeguridad.RolesUsuarioAlmacenesTiposMovimiento.Delete,
            new { COD_ROL_USUARIO, COD_ALMACEN, COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.RolesUsuarioAlmacenesTiposMovimiento.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.RolesUsuarioAlmacenesTiposMovimiento.Delete, new { COD_ROL_USUARIO, COD_ALMACEN, COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO })
        };
    }
}
}
