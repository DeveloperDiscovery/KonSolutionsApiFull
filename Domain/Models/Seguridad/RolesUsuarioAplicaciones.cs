using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Seguridad;

public class RolesUsuarioAplicaciones
{
    public string COD_ROL_USUARIO { get; set; } = string.Empty;
    public string COD_APLICACION { get; set; } = string.Empty;
    public bool? FLG_ACCESO_PERMITIDO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
}

public class RolesUsuarioAplicacionesInsertDto
{
    [Required] public string COD_ROL_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_APLICACION { get; set; } = string.Empty;
    public bool? FLG_ACCESO_PERMITIDO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class RolesUsuarioAplicacionesUpdateDto
{
    [Required] public string COD_ROL_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_APLICACION { get; set; } = string.Empty;
    public bool? FLG_ACCESO_PERMITIDO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class RolesUsuarioAplicacionesSearchDto
{
    public string? Search { get; set; }
}