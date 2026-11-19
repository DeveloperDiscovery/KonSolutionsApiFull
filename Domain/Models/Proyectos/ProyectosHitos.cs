using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Proyectos;

/// <summary>Entidad PROYECTOS_HITOS - tipos equivalentes a la BD.</summary>
public class ProyectosHitos
{
    public int? NUM_PROYECTO { get; set; }
    public int? COD_HITO_PROYECTO { get; set; }
    public string DES_HITO_PROYECTO { get; set; } = string.Empty;
    public DateTime? FEC_FINAL_PROGRAMADO { get; set; }
    public DateTime? FEC_INICIO_EJECUTADO { get; set; }
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
}

public class ProyectosHitosInsertDto
{
    public int? NUM_PROYECTO { get; set; }
    public int? COD_HITO_PROYECTO { get; set; }
    [Required] public string DES_HITO_PROYECTO { get; set; } = string.Empty;
    public DateTime? FEC_FINAL_PROGRAMADO { get; set; }
    public DateTime? FEC_INICIO_EJECUTADO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProyectosHitosUpdateDto
{
    public int? NUM_PROYECTO { get; set; }
    public int? COD_HITO_PROYECTO { get; set; }
    [Required] public string DES_HITO_PROYECTO { get; set; } = string.Empty;
    public DateTime? FEC_FINAL_PROGRAMADO { get; set; }
    public DateTime? FEC_INICIO_EJECUTADO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProyectosHitosSearchDto
{
    public string? Search { get; set; }
}