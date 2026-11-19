using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Seguridad;

/// <summary>Entidad ROLES_USUARIO_USUARIOS - tipos equivalentes a la BD.</summary>
public class RolesUsuarioUsuarios
{
    public string COD_ROL_USUARIO { get; set; } = string.Empty;
    public string COD_USUARIO { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_ROL_USUARIO { get; set; } = string.Empty;
    public string DES_CORREO_INSTITUCIONAL { get; set; } = string.Empty;
    public string DES_USUARIO { get; set; } = string.Empty;
}

public class RolesUsuarioUsuariosInsertDto
{
    [Required] public string COD_ROL_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_USUARIO { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class RolesUsuarioUsuariosUpdateDto
{
    [Required] public string COD_ROL_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_USUARIO { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class RolesUsuarioUsuariosSearchDto
{
    public string? Search { get; set; }
}