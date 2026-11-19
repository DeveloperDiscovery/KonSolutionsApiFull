using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Controlcalidad;

/// <summary>Entidad TIPOS_DEFECTOS_TECNICOS - tipos equivalentes a la BD.</summary>
public class TiposDefectosTecnicos
{
    public string COD_CLASE_DEFECTO_TECNICO { get; set; } = string.Empty;
    public string COD_TIPO_DEFECTO_TECNICO { get; set; } = string.Empty;
    public string DES_DEFECTO_TECNICO { get; set; } = string.Empty;
    public bool? FLG_OBSERVACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CLASE_DEFECTO_TECNICO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
}

public class TiposDefectosTecnicosInsertDto
{
    [Required] public string COD_CLASE_DEFECTO_TECNICO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_DEFECTO_TECNICO { get; set; } = string.Empty;
    [Required] public string DES_DEFECTO_TECNICO { get; set; } = string.Empty;
    public bool? FLG_OBSERVACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposDefectosTecnicosUpdateDto
{
    [Required] public string COD_CLASE_DEFECTO_TECNICO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_DEFECTO_TECNICO { get; set; } = string.Empty;
    [Required] public string DES_DEFECTO_TECNICO { get; set; } = string.Empty;
    public bool? FLG_OBSERVACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposDefectosTecnicosSearchDto
{
    public string? Search { get; set; }
}