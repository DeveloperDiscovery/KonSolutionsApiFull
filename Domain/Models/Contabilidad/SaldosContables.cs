using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Contabilidad;

/// <summary>Entidad SALDOS_CONTABLES - tipos equivalentes a la BD.</summary>
public class SaldosContables
{
    public int? NUM_PERIODO_CONTABLE { get; set; }
    public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
    public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_INICIAL_DEBE { get; set; }
    public decimal? VAL_INICIAL_HABER { get; set; }
    public decimal? VAL_MOVIMIENTO_DEBE { get; set; }
    public decimal? VAL_MOVIMIENTO_HABER { get; set; }
    public decimal? VAL_AJUSTE_DEBE { get; set; }
    public decimal? VAL_AJUSTE_HABER { get; set; }
    public decimal? VAL_CIERRE_DEBE { get; set; }
    public decimal? VAL_CIERRE_HABER { get; set; }
    public decimal? VAL_SALDO_DEBE { get; set; }
    public decimal? VAL_SALDO_HABER { get; set; }
    public string DES_COMENTARIO_CIERRE { get; set; } = string.Empty;
}

public class SaldosContablesInsertDto
{
    public int? NUM_PERIODO_CONTABLE { get; set; }
    [Required] public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_INICIAL_DEBE { get; set; }
    public decimal? VAL_INICIAL_HABER { get; set; }
    public decimal? VAL_MOVIMIENTO_DEBE { get; set; }
    public decimal? VAL_MOVIMIENTO_HABER { get; set; }
    public decimal? VAL_AJUSTE_DEBE { get; set; }
    public decimal? VAL_AJUSTE_HABER { get; set; }
    public decimal? VAL_CIERRE_DEBE { get; set; }
    public decimal? VAL_CIERRE_HABER { get; set; }
    public decimal? VAL_SALDO_DEBE { get; set; }
    public decimal? VAL_SALDO_HABER { get; set; }
}

public class SaldosContablesUpdateDto
{
    public int? NUM_PERIODO_CONTABLE { get; set; }
    [Required] public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_INICIAL_DEBE { get; set; }
    public decimal? VAL_INICIAL_HABER { get; set; }
    public decimal? VAL_MOVIMIENTO_DEBE { get; set; }
    public decimal? VAL_MOVIMIENTO_HABER { get; set; }
    public decimal? VAL_AJUSTE_DEBE { get; set; }
    public decimal? VAL_AJUSTE_HABER { get; set; }
    public decimal? VAL_CIERRE_DEBE { get; set; }
    public decimal? VAL_CIERRE_HABER { get; set; }
    public decimal? VAL_SALDO_DEBE { get; set; }
    public decimal? VAL_SALDO_HABER { get; set; }
}

public class SaldosContablesSearchDto
{
    public string? Search { get; set; }
}