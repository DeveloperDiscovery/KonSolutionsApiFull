using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Contabilidad;

/// <summary>Entidad SALDOS_CUENTA_CORRIENTE - tipos equivalentes a la BD.</summary>
public class SaldosCuentaCorriente
{
    public int? NUM_PERIODO_CONTABLE { get; set; }
    public decimal? COD_ENTIDAD_CONTABLE { get; set; }
    public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
    public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_IMPORTE_DEBE { get; set; }
    public decimal? VAL_IMPORTE_HABER { get; set; }
    public string DES_CUENTA_CONTABLE { get; set; } = string.Empty;
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_MONEDA { get; set; } = string.Empty;
    public string DES_COMENTARIO_CIERRE { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class SaldosCuentaCorrienteInsertDto
{
    public int? NUM_PERIODO_CONTABLE { get; set; }
    public decimal? COD_ENTIDAD_CONTABLE { get; set; }
    [Required] public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_IMPORTE_DEBE { get; set; }
    public decimal? VAL_IMPORTE_HABER { get; set; }
}

public class SaldosCuentaCorrienteUpdateDto
{
    public int? NUM_PERIODO_CONTABLE { get; set; }
    public decimal? COD_ENTIDAD_CONTABLE { get; set; }
    [Required] public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_IMPORTE_DEBE { get; set; }
    public decimal? VAL_IMPORTE_HABER { get; set; }
}

public class SaldosCuentaCorrienteSearchDto
{
    public string? Search { get; set; }
}