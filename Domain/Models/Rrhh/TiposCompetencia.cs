using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad TIPOS_COMPETENCIA - tipos equivalentes a la BD.</summary>
public class TiposCompetencia
{
    public string COD_TIPO_COMPETENCIA { get; set; } = string.Empty;
    public string DES_TIPO_COMPETENCIA { get; set; } = string.Empty;
    public int? NUM_NIVEL { get; set; }
    public int? NUM_NIVEL_MINIMO { get; set; }
    public int? NUM_NIVEL_MAXIMO { get; set; }
}

public class TiposCompetenciaInsertDto
{
    [Required] public string COD_TIPO_COMPETENCIA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_COMPETENCIA { get; set; } = string.Empty;
    public int? NUM_NIVEL { get; set; }
    public int? NUM_NIVEL_MINIMO { get; set; }
    public int? NUM_NIVEL_MAXIMO { get; set; }
}

public class TiposCompetenciaUpdateDto
{
    [Required] public string COD_TIPO_COMPETENCIA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_COMPETENCIA { get; set; } = string.Empty;
    public int? NUM_NIVEL { get; set; }
    public int? NUM_NIVEL_MINIMO { get; set; }
    public int? NUM_NIVEL_MAXIMO { get; set; }
}

public class TiposCompetenciaSearchDto
{
    public string? Search { get; set; }
}