using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad PERSONAL_BENEFICIARIOS - tipos equivalentes a la BD.</summary>
public class PersonalBeneficiarios
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public decimal? COD_ENTIDAD_BENEFECIARIO { get; set; }
    public string COD_TIPO_PARENTESCO { get; set; } = string.Empty;
    public decimal? TAS_BENEFICIO { get; set; }
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
    public string DES_TIPO_PARENTESCO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class PersonalBeneficiariosInsertDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public decimal? COD_ENTIDAD_BENEFECIARIO { get; set; }
    [Required] public string COD_TIPO_PARENTESCO { get; set; } = string.Empty;
    public decimal? TAS_BENEFICIO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalBeneficiariosUpdateDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public decimal? COD_ENTIDAD_BENEFECIARIO { get; set; }
    [Required] public string COD_TIPO_PARENTESCO { get; set; } = string.Empty;
    public decimal? TAS_BENEFICIO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalBeneficiariosSearchDto
{
    public string? Search { get; set; }
}