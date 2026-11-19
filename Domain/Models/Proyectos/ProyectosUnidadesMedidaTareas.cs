using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Proyectos;

/// <summary>Entidad PROYECTOS_UNIDADES_MEDIDA_TAREAS - tipos equivalentes a la BD.</summary>
public class ProyectosUnidadesMedidaTareas
{
    public string COD_UNIDAD_MEDIDA_TAREAS { get; set; } = string.Empty;
    public string DES_UNIDAD_MEDIDA_TAREAS { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class ProyectosUnidadesMedidaTareasInsertDto
{
    [Required] public string COD_UNIDAD_MEDIDA_TAREAS { get; set; } = string.Empty;
    [Required] public string DES_UNIDAD_MEDIDA_TAREAS { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class ProyectosUnidadesMedidaTareasUpdateDto
{
    [Required] public string COD_UNIDAD_MEDIDA_TAREAS { get; set; } = string.Empty;
    [Required] public string DES_UNIDAD_MEDIDA_TAREAS { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class ProyectosUnidadesMedidaTareasSearchDto
{
    public string? Search { get; set; }
}