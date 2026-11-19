using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Proyectos;

/// <summary>Entidad PROYECTOS_RIESGOS - tipos equivalentes a la BD.</summary>
public class ProyectosRiesgos
{
    public int? NUM_PROYECTO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string DES_RIESGO { get; set; } = string.Empty;
    public int? COD_PROBABILIDAD_RIESGO { get; set; }
    public int? COD_IMPACTO_RIESGO { get; set; }
    public string COD_PLAN_MITIGACION { get; set; } = string.Empty;
    public int? COD_FASE { get; set; }
    public int? COD_TAREA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_PROYECTO { get; set; } = string.Empty;
    public string DES_IMPACTO_RIESGO { get; set; } = string.Empty;
    public string DES_PROBABILIDAD_RIESGO { get; set; } = string.Empty;
}

public class ProyectosRiesgosInsertDto
{
    public int? NUM_PROYECTO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string DES_RIESGO { get; set; } = string.Empty;
    public int? COD_PROBABILIDAD_RIESGO { get; set; }
    public int? COD_IMPACTO_RIESGO { get; set; }
    public string? COD_PLAN_MITIGACION { get; set; }
    public int? COD_FASE { get; set; }
    public int? COD_TAREA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProyectosRiesgosUpdateDto
{
    public int? NUM_PROYECTO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string DES_RIESGO { get; set; } = string.Empty;
    public int? COD_PROBABILIDAD_RIESGO { get; set; }
    public int? COD_IMPACTO_RIESGO { get; set; }
    public string? COD_PLAN_MITIGACION { get; set; }
    public int? COD_FASE { get; set; }
    public int? COD_TAREA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProyectosRiesgosSearchDto
{
    public string? Search { get; set; }
}