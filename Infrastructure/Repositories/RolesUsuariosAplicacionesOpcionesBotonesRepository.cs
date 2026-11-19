using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories; 

public class RolesUsuariosAplicacionesOpcionesBotonesRepository : IRolesUsuariosAplicacionesOpcionesBotonesRepository
{
    private readonly IDbConnectionFactory _db;
    public RolesUsuariosAplicacionesOpcionesBotonesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<RolesUsuariosAplicacionesOpcionesBotones>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RolesUsuariosAplicacionesOpcionesBotones>(SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpcionesBotones.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<RolesUsuariosAplicacionesOpcionesBotones?> ShowbyidAsync(string COD_ROL_USUARIO, string COD_APLICACION, string COD_OPCION_APLICACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RolesUsuariosAplicacionesOpcionesBotones>(SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpcionesBotones.Showbyid, new { COD_ROL_USUARIO = COD_ROL_USUARIO, COD_APLICACION = COD_APLICACION, COD_OPCION_APLICACION = COD_OPCION_APLICACION }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Trae TODOS los botones de TODAS las opciones para el rol indicado, en una sola
    /// llamada. Usado al login (y al cambiar de rol activo) para precargar permisos en
    /// memoria del frontend, evitando una llamada por cada pantalla que se abra después.
    /// </summary>
    /// <summary>
    /// Trae los 3 resultados deduplicados del SP TREEVIEW (aplicaciones, opciones, botones) —
    /// los íconos ya no viajan repetidos por cada botón, solo una vez por app/opción.
    /// </summary>
    public async Task<TreeviewResult> TreeviewAsync(string codRolUsuario)
    {
        using var conn = _db.CreateConnection();
        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpcionesBotones.Treeview,
            new { COD_ROL_USUARIO = codRolUsuario },
            commandType: CommandType.StoredProcedure);

        var aplicaciones = (await multi.ReadAsync<AplicacionTreeviewItem>()).ToList();
        var opciones      = (await multi.ReadAsync<OpcionTreeviewItem>()).ToList();
        var botones       = (await multi.ReadAsync<BotonTreeviewItemLite>()).ToList();

        // Los íconos NO salen por acá: van por TREEVIEWLOGOS, que el frontend pide aparte y
        // sin bloquear el primer render.
        //
        // El SP todavía los selecciona, y para el rol SEG eso son ~5 MB de PNG. Serializados a
        // JSON pasan de 7 MB, y lo caro no es la red: es que Blazor WebAssembly los deserialice
        // en el browser. Medido, ese solo GET tardaba 23 segundos mientras el SP resolvía en
        // menos de dos.
        //
        // Anularlos acá es media solución —de SQL a la API siguen viajando— pero es la mitad
        // que duele, y no depende de tocar la base. La otra mitad es sacar las columnas del
        // SELECT del SP; cuando eso pase, estas líneas quedan sin efecto y no molestan.
        foreach (var a in aplicaciones) a.IMG_ICONO_APLICACION = null;
        foreach (var o in opciones)     o.IMG_ICONO_OPCION     = null;

        return new TreeviewResult { Aplicaciones = aplicaciones, Opciones = opciones, Botones = botones };
    }

    /// <summary>Experimento: los íconos de TODO el árbol del rol, en un SP aparte del
    /// texto (Treeview) — se pide en paralelo, sin bloquear el primer render del menú.
    /// Incluye el 3er resultset de íconos por botón (con fallback en el frontend al
    /// ícono calculado cuando venga NULL — ver AccionesBotones.razor).</summary>
    public async Task<TreeviewLogosResult> TreeviewLogosAsync(string codRolUsuario)
    {
        using var conn = _db.CreateConnection();
        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpcionesBotones.TreeviewLogos,
            new { COD_ROL_USUARIO = codRolUsuario },
            commandType: CommandType.StoredProcedure);

        var aplicaciones = (await multi.ReadAsync<AplicacionLogoItem>()).ToList();
        var opciones      = (await multi.ReadAsync<OpcionLogoItem>()).ToList();
        var botones       = (await multi.ReadAsync<BotonLogoItem>()).ToList();
        // 4to resultset (deduplicado por ICONO_ID) — no todas las versiones del SP lo traen
        // todavía; si no hay más resultsets, se queda vacío y ResolverIconos (frontend) no
        // hace nada, sin romper (ver su propio comentario de tolerancia al formato viejo).
        var iconos        = multi.IsConsumed ? new List<IconoItem>() : (await multi.ReadAsync<IconoItem>()).ToList();

        return new TreeviewLogosResult { Aplicaciones = aplicaciones, Opciones = opciones, Botones = botones, Iconos = iconos };
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpcionesBotones.Showcombox);
    }

    public async Task<IEnumerable<RolesUsuariosAplicacionesOpcionesBotones>> SearchAsync(RolesUsuariosAplicacionesOpcionesBotonesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RolesUsuariosAplicacionesOpcionesBotones>(SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpcionesBotones.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(RolesUsuariosAplicacionesOpcionesBotonesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ROL_USUARIO", dto.COD_ROL_USUARIO);
        p.Add("COD_APLICACION", dto.COD_APLICACION);
        p.Add("COD_OPCION_APLICACION", dto.COD_OPCION_APLICACION);
        p.Add("COD_BOTON_OPCION", dto.COD_BOTON_OPCION);
        p.Add("FLG_HABILITADO", dto.FLG_HABILITADO ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpcionesBotones.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpcionesBotones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpcionesBotones.Insert, p)
        };
    }
}

    public async Task<WriteResult> UpdateAsync(RolesUsuariosAplicacionesOpcionesBotonesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ROL_USUARIO", dto.COD_ROL_USUARIO);
        p.Add("COD_APLICACION", dto.COD_APLICACION);
        p.Add("COD_OPCION_APLICACION", dto.COD_OPCION_APLICACION);
        p.Add("COD_BOTON_OPCION", dto.COD_BOTON_OPCION);
        p.Add("FLG_HABILITADO", dto.FLG_HABILITADO ?? false);
        // El SP _UPDATE recibe los mismos nombres que el de alta y mapea adentro a las columnas
        // de ACTUALIZACION: el generador lo hace así para que el contrato de parámetros no cambie
        // entre insertar y modificar.
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpcionesBotones.Update, p,
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpcionesBotones.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpcionesBotones.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ROL_USUARIO, string COD_APLICACION, string COD_OPCION_APLICACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpcionesBotones.Delete, new { COD_ROL_USUARIO = COD_ROL_USUARIO, COD_APLICACION = COD_APLICACION, COD_OPCION_APLICACION = COD_OPCION_APLICACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpcionesBotones.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpcionesBotones.Delete, new { COD_ROL_USUARIO = COD_ROL_USUARIO, COD_APLICACION = COD_APLICACION, COD_OPCION_APLICACION = COD_OPCION_APLICACION })
        };
    }
}

    public async Task<IEnumerable<RolesUsuariosAplicacionesOpcionesBotones>> ShowByOpcionAsync(string codRolUsuario, string codAplicacion, string codOpcion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ROL_USUARIO",       codRolUsuario);
        p.Add("COD_APLICACION",        codAplicacion);
        p.Add("COD_OPCION_APLICACION", codOpcion);
        return await conn.QueryAsync<RolesUsuariosAplicacionesOpcionesBotones>(
            SP.SCHEMASeguridad.RolesUsuariosAplicacionesOpcionesBotones.Showall, p,
            commandType: CommandType.StoredProcedure);
    }

}