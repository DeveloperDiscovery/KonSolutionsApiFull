using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Seguridad;

/// <summary>Entidad ROLES_USUARIOS_APLICACIONES_OPCIONES - tipos equivalentes a la BD.</summary>
public class RolesUsuariosAplicacionesOpciones
{
    public string COD_ROL_USUARIO { get; set; } = string.Empty;
    public string COD_APLICACION { get; set; } = string.Empty;
    public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    public bool? FLG_ACCESO_PERMITIDO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_FORMULARIO { get; set; } = string.Empty;
    public string DES_OPCION_APLICACION { get; set; } = string.Empty;
    public string DES_ROL_USUARIO { get; set; } = string.Empty;
}

public class RolesUsuariosAplicacionesOpcionesInsertDto
{
    [Required] public string COD_ROL_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_APLICACION { get; set; } = string.Empty;
    [Required] public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    public bool? FLG_ACCESO_PERMITIDO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class RolesUsuariosAplicacionesOpcionesUpdateDto
{
    [Required] public string COD_ROL_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_APLICACION { get; set; } = string.Empty;
    [Required] public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    public bool? FLG_ACCESO_PERMITIDO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class RolesUsuariosAplicacionesOpcionesSearchDto
{
    public string? Search { get; set; }
}