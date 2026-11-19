using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Proyectos;

/// <summary>Entidad PROYECTOS_SUB_TAREAS - tipos equivalentes a la BD.</summary>
public class ProyectosSubTareas
{
    public int? NUM_PROYECTO { get; set; }
    public int? COD_TAREA { get; set; }
    public int? COD_SUB_TAREA { get; set; }
    public string DES_SUB_TAREA { get; set; } = string.Empty;
    public bool? FLG_TERMINADA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_TAREA { get; set; } = string.Empty;
}

public class ProyectosSubTareasInsertDto
{
    public int? NUM_PROYECTO { get; set; }
    public int? COD_TAREA { get; set; }
    public int? COD_SUB_TAREA { get; set; }
    [Required] public string DES_SUB_TAREA { get; set; } = string.Empty;
    public bool? FLG_TERMINADA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProyectosSubTareasUpdateDto
{
    public int? NUM_PROYECTO { get; set; }
    public int? COD_TAREA { get; set; }
    public int? COD_SUB_TAREA { get; set; }
    [Required] public string DES_SUB_TAREA { get; set; } = string.Empty;
    public bool? FLG_TERMINADA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProyectosSubTareasSearchDto
{
    public string? Search { get; set; }
}