using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Proyectos;

/// <summary>Entidad PROYECTOS_TAREAS - tipos equivalentes a la BD.</summary>
public class ProyectosTareas
{
    public int? NUM_PROYECTO { get; set; }
    public int? COD_TAREA { get; set; }
    public string DES_TAREA { get; set; } = string.Empty;
    public DateTime? FEC_INICIO_PROGRAMADO { get; set; }
    public DateTime? FEC_FINAL_PROGRAMADO { get; set; }
    public DateTime? FEC_INICIO_EJECUTADO { get; set; }
    public DateTime? FEC_FINAL_EJECUTADO { get; set; }
    public bool? FLG_CONTROL_EJECUCION_MANUAL { get; set; }
    public bool? FLG_REQUIERE_CONFIRMACION { get; set; }
    public decimal? COD_ENTIDAD_CONFIRMADORA { get; set; }
    public string COD_UNIDAD_MEDIDA_TAREAS { get; set; } = string.Empty;
    public decimal? CAN_PROGRAMADAS { get; set; }
    public decimal? CAN_EJECUTADAS { get; set; }
    public decimal? TAS_EJECUCION { get; set; }
    public int? COD_FASE { get; set; }
    public decimal? COD_ENTIDAD_RESPONSABLE { get; set; }
    public decimal? VAL_PRESUPUESTO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_FASE { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_UNIDAD_MEDIDA_TAREAS { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class ProyectosTareasInsertDto
{
    public int? NUM_PROYECTO { get; set; }
    public int? COD_TAREA { get; set; }
    [Required] public string DES_TAREA { get; set; } = string.Empty;
    public DateTime? FEC_INICIO_PROGRAMADO { get; set; }
    public DateTime? FEC_FINAL_PROGRAMADO { get; set; }
    public DateTime? FEC_INICIO_EJECUTADO { get; set; }
    public DateTime? FEC_FINAL_EJECUTADO { get; set; }
    public bool? FLG_CONTROL_EJECUCION_MANUAL { get; set; }
    public bool? FLG_REQUIERE_CONFIRMACION { get; set; }
    public decimal? COD_ENTIDAD_CONFIRMADORA { get; set; }
    public string? COD_UNIDAD_MEDIDA_TAREAS { get; set; }
    public decimal? CAN_PROGRAMADAS { get; set; }
    public decimal? CAN_EJECUTADAS { get; set; }
    public decimal? TAS_EJECUCION { get; set; }
    public int? COD_FASE { get; set; }
    public decimal? COD_ENTIDAD_RESPONSABLE { get; set; }
    public decimal? VAL_PRESUPUESTO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProyectosTareasUpdateDto
{
    public int? NUM_PROYECTO { get; set; }
    public int? COD_TAREA { get; set; }
    [Required] public string DES_TAREA { get; set; } = string.Empty;
    public DateTime? FEC_INICIO_PROGRAMADO { get; set; }
    public DateTime? FEC_FINAL_PROGRAMADO { get; set; }
    public DateTime? FEC_INICIO_EJECUTADO { get; set; }
    public DateTime? FEC_FINAL_EJECUTADO { get; set; }
    public bool? FLG_CONTROL_EJECUCION_MANUAL { get; set; }
    public bool? FLG_REQUIERE_CONFIRMACION { get; set; }
    public decimal? COD_ENTIDAD_CONFIRMADORA { get; set; }
    public string? COD_UNIDAD_MEDIDA_TAREAS { get; set; }
    public decimal? CAN_PROGRAMADAS { get; set; }
    public decimal? CAN_EJECUTADAS { get; set; }
    public decimal? TAS_EJECUCION { get; set; }
    public int? COD_FASE { get; set; }
    public decimal? COD_ENTIDAD_RESPONSABLE { get; set; }
    public decimal? VAL_PRESUPUESTO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProyectosTareasSearchDto
{
    public string? Search { get; set; }
}