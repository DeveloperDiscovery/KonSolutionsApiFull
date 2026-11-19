using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Networking;

/// <summary>Entidad CONFIGURACION - tipos equivalentes a la BD.</summary>
public class Configuracion
{
    public int? NUM_SECUENCIA { get; set; }
    public string COD_TIPO_ENTIDAD_ASESOR_COMERCIAL { get; set; } = string.Empty;
    public string COD_TIPO_PERIODO_LIQUIDACION { get; set; } = string.Empty;
    public string COD_TIPO_PERIODO_VENTAS { get; set; } = string.Empty;
    public string DES_GRUPO_NETWORKING { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_PREFIJO { get; set; } = string.Empty;
    public string DES_TIPO_PERIODO { get; set; } = string.Empty;
    public string COD_TIPO_PERIODO { get; set; } = string.Empty;
}

public class ConfiguracionInsertDto
{
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_TIPO_ENTIDAD_ASESOR_COMERCIAL { get; set; } = string.Empty;
    [Required] public string COD_TIPO_PERIODO_LIQUIDACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_PERIODO_VENTAS { get; set; } = string.Empty;
    [Required] public string DES_GRUPO_NETWORKING { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ConfiguracionUpdateDto
{
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_TIPO_ENTIDAD_ASESOR_COMERCIAL { get; set; } = string.Empty;
    [Required] public string COD_TIPO_PERIODO_LIQUIDACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_PERIODO_VENTAS { get; set; } = string.Empty;
    [Required] public string DES_GRUPO_NETWORKING { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ConfiguracionSearchDto
{
    public string? Search { get; set; }
}