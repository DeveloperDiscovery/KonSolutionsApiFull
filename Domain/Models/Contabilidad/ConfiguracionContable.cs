using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Contabilidad;

/// <summary>Entidad CONFIGURACION_CONTABLE - tipos equivalentes a la BD.</summary>
public class ConfiguracionContable
{
    public int? NUM_SECUENCIA { get; set; }
    public string COD_TIPO_PERIODO_CONTABLE { get; set; } = string.Empty;
    public string DES_PREFIJO { get; set; } = string.Empty;
    public string DES_TIPO_PERIODO { get; set; } = string.Empty;
    public string COD_TIPO_PERIODO { get; set; } = string.Empty;
}

public class ConfiguracionContableInsertDto
{
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_TIPO_PERIODO_CONTABLE { get; set; } = string.Empty;
}

public class ConfiguracionContableUpdateDto
{
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_TIPO_PERIODO_CONTABLE { get; set; } = string.Empty;
}

public class ConfiguracionContableSearchDto
{
    public string? Search { get; set; }
}