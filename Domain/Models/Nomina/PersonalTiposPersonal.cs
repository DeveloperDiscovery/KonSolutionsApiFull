using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Nomina;

/// <summary>Entidad PERSONAL_TIPOS_PERSONAL - tipos equivalentes a la BD.</summary>
public class PersonalTiposPersonal
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string COD_TIPO_PERSONAL { get; set; } = string.Empty;
    public DateTime? FEC_INGRESO { get; set; }
    public DateTime? FEC_SALIDA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_TIPO_PERSONAL { get; set; } = string.Empty;
}

public class PersonalTiposPersonalInsertDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_TIPO_PERSONAL { get; set; } = string.Empty;
    public DateTime? FEC_INGRESO { get; set; }
    public DateTime? FEC_SALIDA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalTiposPersonalUpdateDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_TIPO_PERSONAL { get; set; } = string.Empty;
    public DateTime? FEC_INGRESO { get; set; }
    public DateTime? FEC_SALIDA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalTiposPersonalSearchDto
{
    public string? Search { get; set; }
}