using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad PERSONAL_EXPERIENCIA_LABORAL - tipos equivalentes a la BD.</summary>
public class PersonalExperienciaLaboral
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? COD_ENTIDAD_EMPRESA { get; set; }
    public string DES_ENTIDAD_EMPRESA { get; set; } = string.Empty;
    public int? COD_AREA { get; set; }
    public string DES_AREA { get; set; } = string.Empty;
    public int? COD_CARGO { get; set; }
    public string DES_CARGO { get; set; } = string.Empty;
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_TERMINO { get; set; }
    public string DES_EXPERIENCIA { get; set; } = string.Empty;
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
    public decimal? COD_ENTIDAD { get; set; }
}

public class PersonalExperienciaLaboralInsertDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? COD_ENTIDAD_EMPRESA { get; set; }
    [Required] public string DES_ENTIDAD_EMPRESA { get; set; } = string.Empty;
    public int? COD_AREA { get; set; }
    [Required] public string DES_AREA { get; set; } = string.Empty;
    public int? COD_CARGO { get; set; }
    [Required] public string DES_CARGO { get; set; } = string.Empty;
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_TERMINO { get; set; }
    [Required] public string DES_EXPERIENCIA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalExperienciaLaboralUpdateDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? COD_ENTIDAD_EMPRESA { get; set; }
    [Required] public string DES_ENTIDAD_EMPRESA { get; set; } = string.Empty;
    public int? COD_AREA { get; set; }
    [Required] public string DES_AREA { get; set; } = string.Empty;
    public int? COD_CARGO { get; set; }
    [Required] public string DES_CARGO { get; set; } = string.Empty;
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_TERMINO { get; set; }
    [Required] public string DES_EXPERIENCIA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalExperienciaLaboralSearchDto
{
    public string? Search { get; set; }
}