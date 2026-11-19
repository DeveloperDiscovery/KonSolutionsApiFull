using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad TIPOS_EMBALADO - tipos equivalentes a la BD.</summary>
public class TiposEmbalado
{
    public string COD_TIPO_EMBALADO { get; set; } = string.Empty;
    public string DES_TIPO_EMBALADO { get; set; } = string.Empty;
    public bool? FLG_PREPACK { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
}

public class TiposEmbaladoInsertDto
{
    [Required] public string COD_TIPO_EMBALADO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_EMBALADO { get; set; } = string.Empty;
    public bool? FLG_PREPACK { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposEmbaladoUpdateDto
{
    [Required] public string COD_TIPO_EMBALADO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_EMBALADO { get; set; } = string.Empty;
    public bool? FLG_PREPACK { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposEmbaladoSearchDto
{
    public string? Search { get; set; }
}