using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad NUEVE_BOX_DESEMPENO_POTENCIAL - tipos equivalentes a la BD.</summary>
public class NueveBoxDesempenoPotencial
{
    public string COD_DESEMPENO_9BOX { get; set; } = string.Empty;
    public string COD_POTENCIAL_9BOX { get; set; } = string.Empty;
    public string DES_EVALUACION { get; set; } = string.Empty;
    public int? NUM_EVALUCION { get; set; }
}

public class NueveBoxDesempenoPotencialInsertDto
{
    [Required] public string COD_DESEMPENO_9BOX { get; set; } = string.Empty;
    [Required] public string COD_POTENCIAL_9BOX { get; set; } = string.Empty;
    [Required] public string DES_EVALUACION { get; set; } = string.Empty;
    public int? NUM_EVALUCION { get; set; }
}

public class NueveBoxDesempenoPotencialUpdateDto
{
    [Required] public string COD_DESEMPENO_9BOX { get; set; } = string.Empty;
    [Required] public string COD_POTENCIAL_9BOX { get; set; } = string.Empty;
    [Required] public string DES_EVALUACION { get; set; } = string.Empty;
    public int? NUM_EVALUCION { get; set; }
}

public class NueveBoxDesempenoPotencialSearchDto
{
    public string? Search { get; set; }
}