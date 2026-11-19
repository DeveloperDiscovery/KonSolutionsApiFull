using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad FORMAS_PAGO - tipos equivalentes a la BD.</summary>
public class FormasPago
{
    public int? COD_FORMA_PAGO { get; set; }
    public string DES_FORMA_PAGO { get; set; } = string.Empty;
    public bool? FLG_EFECTIVO { get; set; }
    public string COD_FORMA_PAGO_SUNAT { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class FormasPagoInsertDto
{
    public int? COD_FORMA_PAGO { get; set; }
    [Required] public string DES_FORMA_PAGO { get; set; } = string.Empty;
    public bool? FLG_EFECTIVO { get; set; }
    [Required] public string COD_FORMA_PAGO_SUNAT { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class FormasPagoUpdateDto
{
    public int? COD_FORMA_PAGO { get; set; }
    [Required] public string DES_FORMA_PAGO { get; set; } = string.Empty;
    public bool? FLG_EFECTIVO { get; set; }
    [Required] public string COD_FORMA_PAGO_SUNAT { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class FormasPagoSearchDto
{
    public string? Search { get; set; }
}