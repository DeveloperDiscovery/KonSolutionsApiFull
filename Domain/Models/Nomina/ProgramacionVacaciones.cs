using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Nomina;

/// <summary>Entidad PROGRAMACION_VACACIONES - tipos equivalentes a la BD.</summary>
public class ProgramacionVacaciones
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    public string DES_DETALLE { get; set; } = string.Empty;
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
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
    public string DES_MOTIVO { get; set; } = string.Empty;
}

public class ProgramacionVacacionesInsertDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    [Required] public string DES_DETALLE { get; set; } = string.Empty;
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProgramacionVacacionesUpdateDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    [Required] public string DES_DETALLE { get; set; } = string.Empty;
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProgramacionVacacionesSearchDto
{
    public string? Search { get; set; }
}