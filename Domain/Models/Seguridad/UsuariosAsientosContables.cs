using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Seguridad;

/// <summary>Entidad USUARIOS_ASIENTOS_CONTABLES - tipos equivalentes a la BD.</summary>
public class UsuariosAsientosContables
{
    public string COD_USUARIO { get; set; } = string.Empty;
    public string COD_ASIENTO_CONTABLE { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_ASIENTO_CONTABLE { get; set; } = string.Empty;
    public string DES_CORREO_INSTITUCIONAL { get; set; } = string.Empty;
    public string DES_USUARIO { get; set; } = string.Empty;
}

public class UsuariosAsientosContablesInsertDto
{
    [Required] public string COD_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_ASIENTO_CONTABLE { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class UsuariosAsientosContablesUpdateDto
{
    [Required] public string COD_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_ASIENTO_CONTABLE { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class UsuariosAsientosContablesSearchDto
{
    public string? Search { get; set; }
}