using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Nomina;

/// <summary>Entidad PERSONAL_MARCACIONES - tipos equivalentes a la BD.</summary>
public class PersonalMarcaciones
{
    public decimal? NUM_MARCACION { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public DateTime? FEC_MARCACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class PersonalMarcacionesInsertDto
{
    public decimal? NUM_MARCACION { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public DateTime? FEC_MARCACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalMarcacionesUpdateDto
{
    public decimal? NUM_MARCACION { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public DateTime? FEC_MARCACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalMarcacionesSearchDto
{
    public string? Search { get; set; }
}