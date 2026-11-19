using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Proyectos;

/// <summary>Entidad PROYECTOS_RECURSO_HUMANO_ASIGNADO - tipos equivalentes a la BD.</summary>
public class ProyectosRecursoHumanoAsignado
{
    public int? NUM_PROYECTO { get; set; }
    public int? COD_TAREA { get; set; }
    public decimal? COD_ENTIDAD_ASIGNADA { get; set; }
    public string COD_ROL_TAREA { get; set; } = string.Empty;
    public DateTime? FEC_INICIO_PROGRAMADO { get; set; }
    public DateTime? FEC_FINAL_PROGRAMADO { get; set; }
    public DateTime? FEC_INICIO_EJECUTADO { get; set; }
    public DateTime? FEC_FINAL_EJECUTADO { get; set; }
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
    public string DES_ROL_TAREA { get; set; } = string.Empty;
    public string DES_TAREA { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class ProyectosRecursoHumanoAsignadoInsertDto
{
    public int? NUM_PROYECTO { get; set; }
    public int? COD_TAREA { get; set; }
    public decimal? COD_ENTIDAD_ASIGNADA { get; set; }
    [Required] public string COD_ROL_TAREA { get; set; } = string.Empty;
    public DateTime? FEC_INICIO_PROGRAMADO { get; set; }
    public DateTime? FEC_FINAL_PROGRAMADO { get; set; }
    public DateTime? FEC_INICIO_EJECUTADO { get; set; }
    public DateTime? FEC_FINAL_EJECUTADO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProyectosRecursoHumanoAsignadoUpdateDto
{
    public int? NUM_PROYECTO { get; set; }
    public int? COD_TAREA { get; set; }
    public decimal? COD_ENTIDAD_ASIGNADA { get; set; }
    [Required] public string COD_ROL_TAREA { get; set; } = string.Empty;
    public DateTime? FEC_INICIO_PROGRAMADO { get; set; }
    public DateTime? FEC_FINAL_PROGRAMADO { get; set; }
    public DateTime? FEC_INICIO_EJECUTADO { get; set; }
    public DateTime? FEC_FINAL_EJECUTADO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProyectosRecursoHumanoAsignadoSearchDto
{
    public string? Search { get; set; }
}