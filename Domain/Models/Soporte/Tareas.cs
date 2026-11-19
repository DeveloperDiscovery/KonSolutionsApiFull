using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Soporte;

/// <summary>Entidad TAREAS - tipos equivalentes a la BD.</summary>
public class Tareas
{
    public int? COD_TAREA { get; set; }
    public string DES_TAREA { get; set; } = string.Empty;
}

public class TareasInsertDto
{
    public int? COD_TAREA { get; set; }
    [Required] public string DES_TAREA { get; set; } = string.Empty;
}

public class TareasUpdateDto
{
    public int? COD_TAREA { get; set; }
    [Required] public string DES_TAREA { get; set; } = string.Empty;
}

public class TareasSearchDto
{
    public string? Search { get; set; }
}