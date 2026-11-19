using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad CONDICIONES_PAGO - tipos equivalentes a la BD.</summary>
public class CondicionesPago
{
    public int? COD_CONDICION_PAGO { get; set; }
    public string DES_CONDICION_PAGO { get; set; } = string.Empty;
    public bool? FLG_CREDITO { get; set; }
    public int? CAN_DIAS { get; set; }
    public bool? FLG_DIAS_CALENDARIO { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class CondicionesPagoInsertDto
{
    public int? COD_CONDICION_PAGO { get; set; }
    [Required] public string DES_CONDICION_PAGO { get; set; } = string.Empty;
    public bool? FLG_CREDITO { get; set; }
    public int? CAN_DIAS { get; set; }
    public bool? FLG_DIAS_CALENDARIO { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class CondicionesPagoUpdateDto
{
    public int? COD_CONDICION_PAGO { get; set; }
    [Required] public string DES_CONDICION_PAGO { get; set; } = string.Empty;
    public bool? FLG_CREDITO { get; set; }
    public int? CAN_DIAS { get; set; }
    public bool? FLG_DIAS_CALENDARIO { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class CondicionesPagoSearchDto
{
    public string? Search { get; set; }
}