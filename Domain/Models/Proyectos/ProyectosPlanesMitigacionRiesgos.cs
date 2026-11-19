using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Proyectos;

/// <summary>Entidad PROYECTOS_PLANES_MITIGACION_RIESGOS - tipos equivalentes a la BD.</summary>
public class ProyectosPlanesMitigacionRiesgos
{
    public int? NUM_PROYECTO { get; set; }
    public string COD_PLAN_MITIGACION { get; set; } = string.Empty;
    public string DES_PLAN_MITIGACION { get; set; } = string.Empty;
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
}

public class ProyectosPlanesMitigacionRiesgosInsertDto
{
    public int? NUM_PROYECTO { get; set; }
    [Required] public string COD_PLAN_MITIGACION { get; set; } = string.Empty;
    [Required] public string DES_PLAN_MITIGACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProyectosPlanesMitigacionRiesgosUpdateDto
{
    public int? NUM_PROYECTO { get; set; }
    [Required] public string COD_PLAN_MITIGACION { get; set; } = string.Empty;
    [Required] public string DES_PLAN_MITIGACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProyectosPlanesMitigacionRiesgosSearchDto
{
    public string? Search { get; set; }
}