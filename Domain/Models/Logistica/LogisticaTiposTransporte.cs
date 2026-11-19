using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad TIPOS_TRANSPORTE (LOGISTICA) - tipos equivalentes a la BD.</summary>
public class LogisticaTiposTransporte
{
    public string COD_TIPO_TRANSPORTE { get; set; } = string.Empty;
    public string? DES_TIPO_TRANSPORTE { get; set; }
    public string? COD_GRUPO_PRODUCTO { get; set; }
    public string? COD_FAMILIA_PRODUCTO { get; set; }
    public string? COD_SUB_FAMILIA_PRODUCTO { get; set; }
    public bool? FLG_TERRESTRE { get; set; }
    public bool? FLG_MARITIMO { get; set; }
    public bool? FLG_FLUVIAL { get; set; }
    public bool? FLG_AEREO { get; set; }
    public string? DES_GRUPO_PRODUCTO { get; set; }
    public string? DES_FAMILIA_PRODUCTO { get; set; }
    public string? DES_SUB_FAMILIA_PRODUCTO { get; set; }
}

public class LogisticaTiposTransporteInsertDto
{
    [Required] public string COD_TIPO_TRANSPORTE { get; set; } = string.Empty;
    public string? DES_TIPO_TRANSPORTE { get; set; }
    public string? COD_GRUPO_PRODUCTO { get; set; }
    public string? COD_FAMILIA_PRODUCTO { get; set; }
    public string? COD_SUB_FAMILIA_PRODUCTO { get; set; }
    public bool? FLG_TERRESTRE { get; set; }
    public bool? FLG_MARITIMO { get; set; }
    public bool? FLG_FLUVIAL { get; set; }
    public bool? FLG_AEREO { get; set; }
}

public class LogisticaTiposTransporteUpdateDto
{
    [Required] public string COD_TIPO_TRANSPORTE { get; set; } = string.Empty;
    public string? DES_TIPO_TRANSPORTE { get; set; }
    public string? COD_GRUPO_PRODUCTO { get; set; }
    public string? COD_FAMILIA_PRODUCTO { get; set; }
    public string? COD_SUB_FAMILIA_PRODUCTO { get; set; }
    public bool? FLG_TERRESTRE { get; set; }
    public bool? FLG_MARITIMO { get; set; }
    public bool? FLG_FLUVIAL { get; set; }
    public bool? FLG_AEREO { get; set; }
}

public class LogisticaTiposTransporteSearchDto
{
    public string? Search { get; set; }
}
