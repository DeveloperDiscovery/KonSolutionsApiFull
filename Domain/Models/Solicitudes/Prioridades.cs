using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Solicitudes;

/// <summary>Entidad PRIORIDADES - tipos equivalentes a la BD.</summary>
public class Prioridades
{
    public string COD_PRIORIDAD { get; set; } = string.Empty;
    public string DES_PRIORIDAD { get; set; } = string.Empty;
    public int? VAL_PRIORIDAD { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class PrioridadesInsertDto
{
    [Required] public string COD_PRIORIDAD { get; set; } = string.Empty;
    [Required] public string DES_PRIORIDAD { get; set; } = string.Empty;
    public int? VAL_PRIORIDAD { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class PrioridadesUpdateDto
{
    [Required] public string COD_PRIORIDAD { get; set; } = string.Empty;
    [Required] public string DES_PRIORIDAD { get; set; } = string.Empty;
    public int? VAL_PRIORIDAD { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class PrioridadesSearchDto
{
    public string? Search { get; set; }
}