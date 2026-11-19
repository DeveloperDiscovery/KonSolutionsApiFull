namespace KonSolutionsApi.Domain.Models.Seguridad;

/// <summary>Lo que un rol puede hacer sobre UNA entidad, tal como lo devuelve
/// PROC_SEGURIDAD_PERMISOS_ROL_SHOWALL: una fila por entidad con los cuatro verbos.
///
/// La entidad es el nombre de la tabla (`LOGISTICA.ALMACENES`), no el de la opción de menú:
/// así el permiso sobrevive a que la opción se mueva de aplicación, que es justo lo que el
/// usuario preguntó cuando armamos esto.</summary>
public class PermisoApi
{
    public string DES_OBJETO { get; set; } = string.Empty;
    public bool FLG_ACCION_GET { get; set; }
    public bool FLG_ACCION_POST { get; set; }
    public bool FLG_ACCION_PUT { get; set; }
    public bool FLG_ACCION_DELETE { get; set; }

    public bool Permite(string verbo) => verbo.ToUpperInvariant() switch
    {
        "GET" => FLG_ACCION_GET,
        "POST" => FLG_ACCION_POST,
        "PUT" or "PATCH" => FLG_ACCION_PUT,
        "DELETE" => FLG_ACCION_DELETE,
        _ => false
    };
}

/// <summary>Cómo se comporta el guardián. Existe porque encender el control de golpe sobre
/// 475 controladores dejaría a medio sistema sin poder trabajar el primer día: primero se
/// mira qué se estaría bloqueando, después se bloquea.</summary>
public enum ModoPermisos
{
    /// <summary>Registra en SEGURIDAD.PERMISOS_AUDITORIA lo que habría rechazado, y deja
    /// pasar. Es el modo de arranque: sirve para descubrir qué entidades quedaron sin
    /// configurar antes de que le corten el trabajo a alguien.</summary>
    Auditoria,
    /// <summary>Rechaza con 403 y registra. Es el modo definitivo.</summary>
    Bloqueo,
    /// <summary>Apagado por completo, ni siquiera consulta. Para diagnosticar si un problema
    /// viene de acá.</summary>
    Desactivado
}
