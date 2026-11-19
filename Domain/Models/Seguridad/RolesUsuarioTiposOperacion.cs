using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Seguridad;

/// <summary>Entidad SEGURIDAD.ROLES_USUARIO_TIPOS_OPERACION — qué tipos de operación puede
/// ejecutar un rol. Hija de SEGURIDAD.ROLES_USUARIO.
/// PK: COD_ROL_USUARIO + COD_CLASE_OPERACION + COD_TIPO_OPERACION.
///
/// Asimetría de los SP a tener presente: _SHOWALL exige rol Y clase de operación, mientras que
/// _DISPONIBLES pide sólo el rol y devuelve todas las clases juntas. Por eso el filtro por
/// clase de la pantalla se aplica en el servidor para los asignados y en memoria para los
/// disponibles.</summary>
public class RolesUsuarioTiposOperacion
{
    public string COD_ROL_USUARIO { get; set; } = string.Empty;
    public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }

    // Solo lectura (JOIN con CLASES_OPERACION, TIPOS_OPERACION y ROLES_USUARIO).
    public string? DES_ROL_USUARIO { get; set; }
    public string? DES_CLASE_OPERACION { get; set; }
    public string? DES_TIPO_OPERACION { get; set; }
}

public class RolesUsuarioTiposOperacionInsertDto
{
    [Required] public string COD_ROL_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
}

public class RolesUsuarioTiposOperacionLoteDto
{
    [Required] public string COD_ROL_USUARIO { get; set; } = string.Empty;
    public List<RolesUsuarioTiposOperacionClave> Tipos { get; set; } = new();
}

/// <summary>Par clase+tipo: el código de tipo sólo es único dentro de su clase.</summary>
public class RolesUsuarioTiposOperacionClave
{
    public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    public string COD_TIPO_OPERACION { get; set; } = string.Empty;
}

public class RolesUsuarioTiposOperacionSearchDto
{
    public string? COD_ROL_USUARIO { get; set; }
    public string? COD_CLASE_OPERACION { get; set; }
    public string? Search { get; set; }
}

public class RolesUsuarioTiposOperacionLoteResult
{
    public int Procesados { get; set; }
    public List<string> Fallidos { get; set; } = new();
    public string? DesError { get; set; }
}
