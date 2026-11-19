using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad TIPOS_IMPLEMENTO_LABORAL - tipos equivalentes a la BD.</summary>
public class TiposImplementoLaboral
{
    public string COD_TIPO_IMPLEMENTO_LABORAL { get; set; } = string.Empty;
    public string DES_TIPO_IMPLEMENTO_LABORAL { get; set; } = string.Empty;
    public int? NUM_MESES_REPOSICION { get; set; }
    public bool? FLG_CONSIDERAR_FECHA_ENTREGA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
}

public class TiposImplementoLaboralInsertDto
{
    [Required] public string COD_TIPO_IMPLEMENTO_LABORAL { get; set; } = string.Empty;
    [Required] public string DES_TIPO_IMPLEMENTO_LABORAL { get; set; } = string.Empty;
    public int? NUM_MESES_REPOSICION { get; set; }
    public bool? FLG_CONSIDERAR_FECHA_ENTREGA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposImplementoLaboralUpdateDto
{
    [Required] public string COD_TIPO_IMPLEMENTO_LABORAL { get; set; } = string.Empty;
    [Required] public string DES_TIPO_IMPLEMENTO_LABORAL { get; set; } = string.Empty;
    public int? NUM_MESES_REPOSICION { get; set; }
    public bool? FLG_CONSIDERAR_FECHA_ENTREGA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposImplementoLaboralSearchDto
{
    public string? Search { get; set; }
}