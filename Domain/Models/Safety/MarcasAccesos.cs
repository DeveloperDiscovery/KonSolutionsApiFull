using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Safety;

/// <summary>Entidad MARCAS_ACCESOS - tipos equivalentes a la BD.</summary>
public class MarcasAccesos
{
    public int? NUM_READ_TAG { get; set; }
    public string DES_READ_TAG { get; set; } = string.Empty;
}

public class MarcasAccesosInsertDto
{
    public int? NUM_READ_TAG { get; set; }
    [Required] public string DES_READ_TAG { get; set; } = string.Empty;
}

public class MarcasAccesosUpdateDto
{
    public int? NUM_READ_TAG { get; set; }
    [Required] public string DES_READ_TAG { get; set; } = string.Empty;
}

public class MarcasAccesosSearchDto
{
    public string? Search { get; set; }
}