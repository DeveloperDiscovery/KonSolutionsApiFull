using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad INCOTERMS_TIPOS_TRANSPORTE - tipos equivalentes a la BD.</summary>
public class IncotermsTiposTransporte
{
    public string COD_INCOTERM { get; set; } = string.Empty;
    public string COD_TIPO_TRANSPORTE { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_INCOTERM { get; set; } = string.Empty;
    public string DES_TIPO_TRANSPORTE { get; set; } = string.Empty;
}

public class IncotermsTiposTransporteInsertDto
{
    [Required] public string COD_INCOTERM { get; set; } = string.Empty;
    [Required] public string COD_TIPO_TRANSPORTE { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class IncotermsTiposTransporteUpdateDto
{
    [Required] public string COD_INCOTERM { get; set; } = string.Empty;
    [Required] public string COD_TIPO_TRANSPORTE { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class IncotermsTiposTransporteSearchDto
{
    public string? Search { get; set; }
}