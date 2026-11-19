using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class UsuariosRepository : IUsuariosRepository
{
    private readonly IDbConnectionFactory _db;
    public UsuariosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Usuarios>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Usuarios>(SP.SCHEMASeguridad.Usuarios.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Usuarios?> ShowbyidAsync(string COD_USUARIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Usuarios>(SP.SCHEMASeguridad.Usuarios.Showbyid, new { COD_USUARIO = COD_USUARIO }, commandType: CommandType.StoredProcedure);
    }

    // PROC_SEGURIDAD_USUARIOS_SHOWCOMBOX no acepta parámetros (confirmado: "no tiene
    // parámetros y se han proporcionado argumentos") — se trae todo y se filtra acá.
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string? search = null)
    {
        using var conn = _db.CreateConnection();
        var items = await ComboboxReader.LeerAsync(conn, SP.SCHEMASeguridad.Usuarios.Showcombox);
        if (!string.IsNullOrWhiteSpace(search))
            items = items.Where(i => i.Id.Contains(search, StringComparison.OrdinalIgnoreCase)
                                   || i.Texto.Contains(search, StringComparison.OrdinalIgnoreCase));
        return items.ToList();
    }

    public async Task<IEnumerable<Usuarios>> SearchAsync(UsuariosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Usuarios>(SP.SCHEMASeguridad.Usuarios.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(UsuariosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_USUARIO", dto.COD_USUARIO);
        p.Add("DES_USUARIO", dto.DES_USUARIO);
        p.Add("COD_PERFIL_USUARIO", dto.COD_PERFIL_USUARIO);
        p.Add("DES_CORREO_INSTITUCIONAL", dto.DES_CORREO_INSTITUCIONAL);
        p.Add("NUM_TELEFONO", dto.NUM_TELEFONO);
        p.Add("COD_PASSWORD",
            string.IsNullOrEmpty(dto.COD_PASSWORD) ? null : PasswordHasher.Hash(dto.COD_PASSWORD),
            System.Data.DbType.String);   // null al crear (normal); si viene definido, se hashea
        p.Add("FLG_VALIDA_DIRECTORIO_ACTIVO", dto.FLG_VALIDA_DIRECTORIO_ACTIVO ?? false);
        p.Add("FLG_LOGIN_CREDENCIALES", dto.FLG_LOGIN_CREDENCIALES ?? false);
        p.Add("FLG_LOGIN_FACE", dto.FLG_LOGIN_FACE ?? false);
        p.Add("FLG_LOGIN_HUELLA_DACTILAR", dto.FLG_LOGIN_HUELLA_DACTILAR ?? false);
        p.Add("FLG_REGISTRO_FACIAL", dto.FLG_REGISTRO_FACIAL ?? false);
        p.Add("NUM_MAXIMO_INTENTOS", dto.NUM_MAXIMO_INTENTOS);
        p.Add("NUM_INTENTOS", dto.NUM_INTENTOS);
        p.Add("NUM_DIAS_PASSWORD_RESET", dto.NUM_DIAS_PASSWORD_RESET);
        p.Add("IMG_FOTO", dto.IMG_FOTO, System.Data.DbType.Binary);
        p.Add("IMG_FIRMA", dto.IMG_FIRMA, System.Data.DbType.Binary);
        p.Add("FEC_ULTIMA_PASSWORD_CHANGE", (DateTime?)null, System.Data.DbType.Date);   // Sin cambio aún
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD, System.Data.DbType.Decimal);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.Usuarios.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.Usuarios.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.Usuarios.Insert, p)
        };
    }
}

    public async Task ActualizarRegistroFacialAsync(string codUsuario, bool valor)
    {
        var u = await ShowbyidAsync(codUsuario);
        if (u is null) return;
        var dto = new UsuariosInsertDto
        {
            COD_USUARIO              = u.COD_USUARIO,
            DES_USUARIO              = u.DES_USUARIO,
            COD_PERFIL_USUARIO       = u.COD_PERFIL_USUARIO,
            DES_CORREO_INSTITUCIONAL = u.DES_CORREO_INSTITUCIONAL,
            NUM_TELEFONO             = u.NUM_TELEFONO,
            COD_PASSWORD             = null,   // no se retoca la contraseña
            FLG_VALIDA_DIRECTORIO_ACTIVO = u.FLG_VALIDA_DIRECTORIO_ACTIVO,
            FLG_LOGIN_CREDENCIALES   = u.FLG_LOGIN_CREDENCIALES,
            FLG_LOGIN_FACE           = u.FLG_LOGIN_FACE,
            FLG_LOGIN_HUELLA_DACTILAR = u.FLG_LOGIN_HUELLA_DACTILAR,
            FLG_REGISTRO_FACIAL      = valor,
            NUM_MAXIMO_INTENTOS      = u.NUM_MAXIMO_INTENTOS,
            NUM_INTENTOS             = u.NUM_INTENTOS,
            NUM_DIAS_PASSWORD_RESET  = u.NUM_DIAS_PASSWORD_RESET,
            IMG_FOTO                 = u.IMG_FOTO,
            IMG_FIRMA                = u.IMG_FIRMA,
            COD_TIPO_ESTADO          = u.COD_TIPO_ESTADO,
            COD_ESTADO               = u.COD_ESTADO,
            COD_USUARIO_REGISTRO     = codUsuario,
            COD_ESTACION_REGISTRO    = "SISTEMA"
        };
        await InsertAsync(dto, "SISTEMA", "SISTEMA");
    }

    public async Task<WriteResult> DeleteAsync(string COD_USUARIO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.Usuarios.Delete, new { COD_USUARIO = COD_USUARIO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.Usuarios.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.Usuarios.Delete, new { COD_USUARIO = COD_USUARIO })
        };
    }
}

    public async Task<IEnumerable<dynamic>> EstadosAsync(UsuariosEstadosDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_USUARIO", dto.COD_USUARIO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_ACTUALIZACION", codUsuario);
        p.Add("COD_ESTACION_ACTUALIZACION", codEstacion);
        p.Add("FEC_ACTUALIZACION", DateTime.Now);
        return await conn.QueryAsync(SP.SCHEMASeguridad.Usuarios.Estados, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<dynamic>> PasswordAsync(UsuariosPasswordDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();

        // No repetir la contraseña anterior: se compara contra el hash actual ANTES de
        // sobreescribirlo. BCrypt.Verify rehashea el texto plano recibido con el mismo
        // salt del hash guardado y compara — nunca se "desencripta" nada.
        // Se obtiene vía el SP existente (PROC_SEGURIDAD_USUARIOS_SHOWBYID), nunca con
        // SELECT directo contra la tabla: la cuenta de aplicación solo tiene EXECUTE.
        var usuarioActual = await ShowbyidAsync(dto.COD_USUARIO);

        if (PasswordHasher.Verify(dto.COD_PASSWORD, usuarioActual?.COD_PASSWORD))
            throw new InvalidOperationException("La nueva contraseña no puede ser igual a la anterior.");

        var p = new DynamicParameters();
        p.Add("COD_USUARIO", dto.COD_USUARIO);
        p.Add("COD_PASSWORD", PasswordHasher.Hash(dto.COD_PASSWORD));   // se guarda el hash, nunca el texto plano
        p.Add("COD_USUARIO_ACTUALIZACION", codUsuario);
        p.Add("COD_ESTACION_ACTUALIZACION", codEstacion);
        return await conn.QueryAsync(SP.SCHEMASeguridad.Usuarios.Password, p, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Reset de password: ejecuta PROC_SEGURIDAD_USUARIOS_PASSWORDRESET, que deja
    /// el COD_PASSWORD en NULL para forzar la redefinición en el próximo login.
    /// Solo recibe el usuario y los datos de auditoría (no password).
    /// </summary>
    public async Task<IEnumerable<dynamic>> PasswordResetAsync(UsuariosPasswordResetDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_USUARIO", dto.COD_USUARIO);
        p.Add("COD_USUARIO_ACTUALIZACION", codUsuario);
        p.Add("COD_ESTACION_ACTUALIZACION", codEstacion);
        return await conn.QueryAsync(SP.SCHEMASeguridad.Usuarios.PasswordReset, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<dynamic>> ResetintentosAsync(UsuariosResetintentosDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_USUARIO", dto.COD_USUARIO);
        p.Add("COD_USUARIO_ACTUALIZACION", codUsuario);
        p.Add("COD_ESTACION_ACTUALIZACION", codEstacion);
        p.Add("FEC_ACTUALIZACION", DateTime.Now);
        return await conn.QueryAsync(SP.SCHEMASeguridad.Usuarios.Resetintentos, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<dynamic>> ValidaloginAsync(UsuariosValidaloginDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_USUARIO", dto.COD_USUARIO);
        p.Add("COD_PASSWORD", dto.COD_PASSWORD);
        return await conn.QueryAsync(SP.SCHEMASeguridad.Usuarios.Validalogin, p, commandType: CommandType.StoredProcedure);
    }

    /// <summary>PROC_SEGURIDAD_USUARIOS_LOADCOMBOS: Perfil + Dominios de correo (ya vienen
    /// aliados Id/Texto vía mapeo por prefijo — PROC_ENTORNO_DOMINIOS_EMPRESA_SHOWCOMBOX
    /// devuelve columnas reales COD_DOMINIO_EMPRESA/DES_DOMINIO_EMPRESA) y Estados
    /// (COD_ESTADO/DES_ESTADO/colores) en un solo viaje. Si codTipoEstado &lt;= 0, se resuelve
    /// el de SEGURIDAD.USUARIOS automáticamente (mismo mecanismo que EstadosPorTablaController).</summary>
    public async Task<UsuariosLoadCombosResult> LoadcombosAsync(int codTipoEstado)
    {
        using var conn = _db.CreateConnection();

        if (codTipoEstado <= 0)
        {
            var tabla = await conn.QueryFirstOrDefaultAsync<TablaTipoEstado>(
                "PROC_ESTANDAR_TABLAS_TIPOS_ESTADO_SHOWBYID",
                new { DES_TABLA = "SEGURIDAD.USUARIOS" },
                commandType: CommandType.StoredProcedure);
            codTipoEstado = tabla?.COD_TIPO_ESTADO ?? 0;
        }

        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMASeguridad.Usuarios.Loadcombos,
            new { COD_TIPO_ESTADO = codTipoEstado },
            commandType: CommandType.StoredProcedure);

        var perfiles        = MapCombo(await multi.ReadAsync<dynamic>());
        var dominiosEmpresa = MapCombo(await multi.ReadAsync<dynamic>());
        var estadosFilas    = await multi.ReadAsync<dynamic>();

        var estados = estadosFilas.Select(f =>
        {
            var d = (IDictionary<string, object>)f;
            var codStr = d.TryGetValue("COD_ESTADO", out var id) ? id?.ToString() ?? "" : "";
            return new EstadoItem
            {
                CodEstado = int.TryParse(codStr, out var n) ? n : 0,
                DesEstado = d.TryGetValue("DES_ESTADO", out var tx) ? tx?.ToString() ?? "" : "",
                BackColor = d.TryGetValue("DES_BACKCOLOR", out var bc) ? bc?.ToString() ?? "" : "",
                ForeColor = d.TryGetValue("DES_FORECOLOR", out var fc) ? fc?.ToString() ?? "" : "",
                EsDefault = d.TryGetValue("FLG_DEFAULT", out var df) && df is not null && Convert.ToBoolean(df)
            };
        }).ToList();

        return new UsuariosLoadCombosResult
        {
            CodTipoEstado   = codTipoEstado,
            Perfiles        = perfiles,
            DominiosEmpresa = dominiosEmpresa,
            Estados         = estados
        };
    }

    /// <summary>Mapea filas dinámicas a ComboboxItem detectando la columna de código
    /// (primera COD_*) y la de descripción (primera DES_*/NOMBRE_*) — nunca busca columnas
    /// literales "Id"/"Texto".</summary>
    private static List<ComboboxItem> MapCombo(IEnumerable<dynamic> filas)
    {
        var list = new List<ComboboxItem>();
        foreach (var f in filas)
        {
            var d = (IDictionary<string, object>)f;
            var idCol = d.Keys.FirstOrDefault(k => k.StartsWith("COD_", StringComparison.OrdinalIgnoreCase));
            var txtCol = d.Keys.FirstOrDefault(k => k.StartsWith("DES_", StringComparison.OrdinalIgnoreCase) || k.StartsWith("NOMBRE_", StringComparison.OrdinalIgnoreCase));
            var id = idCol != null ? d[idCol]?.ToString() : null;
            var tx = txtCol != null ? d[txtCol]?.ToString() : null;
            list.Add(new ComboboxItem { Id = id ?? "", Texto = tx ?? "" });
        }
        return list;
    }

    private class TablaTipoEstado { public int COD_TIPO_ESTADO { get; set; } }

}