using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Seguridad;

/// <summary>Entidad USUARIOS_ALMACENES_TIPOS_MOVIMIENTO - tipos equivalentes a la BD.</summary>
public class UsuariosAlmacenesTiposMovimiento
{
    public string COD_USUARIO { get; set; } = string.Empty;
    public string COD_ALMACEN { get; set; } = string.Empty;
    public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_CORREO_INSTITUCIONAL { get; set; } = string.Empty;
    public string DES_USUARIO { get; set; } = string.Empty;
}

public class UsuariosAlmacenesTiposMovimientoInsertDto
{
    [Required] public string COD_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class UsuariosAlmacenesTiposMovimientoUpdateDto
{
    [Required] public string COD_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class UsuariosAlmacenesTiposMovimientoSearchDto
{
    public string? Search { get; set; }
}