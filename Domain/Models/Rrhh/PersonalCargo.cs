using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad PERSONAL_CARGO - tipos equivalentes a la BD.</summary>
public class PersonalCargo
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? COD_CARGO { get; set; }
    public DateTime? FEC_INGRESO { get; set; }
    public DateTime? FEC_SALIDA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CARGO { get; set; } = string.Empty;
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class PersonalCargoInsertDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? COD_CARGO { get; set; }
    public DateTime? FEC_INGRESO { get; set; }
    public DateTime? FEC_SALIDA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalCargoUpdateDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? COD_CARGO { get; set; }
    public DateTime? FEC_INGRESO { get; set; }
    public DateTime? FEC_SALIDA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalCargoSearchDto
{
    public string? Search { get; set; }
}