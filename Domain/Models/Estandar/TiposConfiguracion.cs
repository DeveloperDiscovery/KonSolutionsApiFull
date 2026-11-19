using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad TIPOS_CONFIGURACION - tipos equivalentes a la BD.</summary>
public class TiposConfiguracion
{
    public string COD_TIPO_CONFIGURACION { get; set; } = string.Empty;
    public string? DES_TIPO_CONFIGURACION { get; set; }
}

public class TiposConfiguracionInsertDto
{
    [Required, MaxLength(3)] public string COD_TIPO_CONFIGURACION { get; set; } = string.Empty;
    [MaxLength(50)] public string? DES_TIPO_CONFIGURACION { get; set; }
}

public class TiposConfiguracionUpdateDto
{
    [Required, MaxLength(3)] public string COD_TIPO_CONFIGURACION { get; set; } = string.Empty;
    [MaxLength(50)] public string? DES_TIPO_CONFIGURACION { get; set; }
}
