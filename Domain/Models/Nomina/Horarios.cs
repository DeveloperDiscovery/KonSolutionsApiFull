using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Nomina;

/// <summary>Entidad HORARIOS - tipos equivalentes a la BD.</summary>
public class Horarios
{
    public string COD_HORARIO { get; set; } = string.Empty;
    public string DES_HORARIO { get; set; } = string.Empty;
    public string COD_TIPO_HORARIO { get; set; } = string.Empty;
    public int? COD_AREA { get; set; }
    public bool? FLG_CALCULO_HORAS_EXTRAS_ANTERIOR { get; set; }
    public bool? FLG_CALCULO_HORAS_EXTRAS_POSTERIOR { get; set; }
    public bool? FLG_HORARIO_PUESTO_LABORAL { get; set; }
    public int? NUM_MINUTOS_TOLERANCIA_INGRESO { get; set; }
    public int? NUM_MINUTOS_ALMUERZO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_AREA { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_TIPO_HORARIO { get; set; } = string.Empty;
}

public class HorariosInsertDto
{
    [Required] public string COD_HORARIO { get; set; } = string.Empty;
    [Required] public string DES_HORARIO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_HORARIO { get; set; } = string.Empty;
    public int? COD_AREA { get; set; }
    public bool? FLG_CALCULO_HORAS_EXTRAS_ANTERIOR { get; set; }
    public bool? FLG_CALCULO_HORAS_EXTRAS_POSTERIOR { get; set; }
    public bool? FLG_HORARIO_PUESTO_LABORAL { get; set; }
    public int? NUM_MINUTOS_TOLERANCIA_INGRESO { get; set; }
    public int? NUM_MINUTOS_ALMUERZO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class HorariosUpdateDto
{
    [Required] public string COD_HORARIO { get; set; } = string.Empty;
    [Required] public string DES_HORARIO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_HORARIO { get; set; } = string.Empty;
    public int? COD_AREA { get; set; }
    public bool? FLG_CALCULO_HORAS_EXTRAS_ANTERIOR { get; set; }
    public bool? FLG_CALCULO_HORAS_EXTRAS_POSTERIOR { get; set; }
    public bool? FLG_HORARIO_PUESTO_LABORAL { get; set; }
    public int? NUM_MINUTOS_TOLERANCIA_INGRESO { get; set; }
    public int? NUM_MINUTOS_ALMUERZO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class HorariosSearchDto
{
    public string? Search { get; set; }
}