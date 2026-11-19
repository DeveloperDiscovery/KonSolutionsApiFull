using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad MONEDAS - tipos equivalentes a la BD.</summary>
public class Monedas
{
    public string COD_MONEDA { get; set; } = string.Empty;
    public string DES_MONEDA { get; set; } = string.Empty;
    public int? COD_MONEDA_SUNAT { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
}

public class MonedasInsertDto
{
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    [Required] public string DES_MONEDA { get; set; } = string.Empty;
    public int? COD_MONEDA_SUNAT { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class MonedasUpdateDto
{
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    [Required] public string DES_MONEDA { get; set; } = string.Empty;
    public int? COD_MONEDA_SUNAT { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class MonedasSearchDto
{
    public string? Search { get; set; }
}