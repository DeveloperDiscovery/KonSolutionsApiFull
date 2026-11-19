using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Proyectos;

/// <summary>Entidad PROYECTOS_FASES - tipos equivalentes a la BD.</summary>
public class ProyectosFases
{
    public int? NUM_PROYECTO { get; set; }
    public int? COD_FASE { get; set; }
    public string DES_FASE { get; set; } = string.Empty;
    public DateTime? FEC_INICIO_PROGRAMADO { get; set; }
    public DateTime? FEC_FINAL_PROGRAMADO { get; set; }
    public DateTime? FEC_INICIO_EJECUTADO { get; set; }
    public DateTime? FEC_FINAL_EJECUTADO { get; set; }
    public decimal? COD_ENTIDAD_RESPONSABLE { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_FASE_PADRE { get; set; }
    public int? COD_HITO_PROYECTO { get; set; }
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
    public string DES_HITO_PROYECTO { get; set; } = string.Empty;
    public string DES_PROYECTO { get; set; } = string.Empty;
}

public class ProyectosFasesInsertDto
{
    public int? NUM_PROYECTO { get; set; }
    public int? COD_FASE { get; set; }
    [Required] public string DES_FASE { get; set; } = string.Empty;
    public DateTime? FEC_INICIO_PROGRAMADO { get; set; }
    public DateTime? FEC_FINAL_PROGRAMADO { get; set; }
    public DateTime? FEC_INICIO_EJECUTADO { get; set; }
    public DateTime? FEC_FINAL_EJECUTADO { get; set; }
    public decimal? COD_ENTIDAD_RESPONSABLE { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public int? COD_FASE_PADRE { get; set; }
    public int? COD_HITO_PROYECTO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProyectosFasesUpdateDto
{
    public int? NUM_PROYECTO { get; set; }
    public int? COD_FASE { get; set; }
    [Required] public string DES_FASE { get; set; } = string.Empty;
    public DateTime? FEC_INICIO_PROGRAMADO { get; set; }
    public DateTime? FEC_FINAL_PROGRAMADO { get; set; }
    public DateTime? FEC_INICIO_EJECUTADO { get; set; }
    public DateTime? FEC_FINAL_EJECUTADO { get; set; }
    public decimal? COD_ENTIDAD_RESPONSABLE { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public int? COD_FASE_PADRE { get; set; }
    public int? COD_HITO_PROYECTO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProyectosFasesSearchDto
{
    public string? Search { get; set; }
}