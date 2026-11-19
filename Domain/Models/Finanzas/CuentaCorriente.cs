using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Finanzas;

/// <summary>Entidad CUENTA_CORRIENTE - tipos equivalentes a la BD.</summary>
public class CuentaCorriente
{
    public decimal? COD_ENTIDAD { get; set; }
    public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_IMPORTE_SALDO { get; set; }
    public string DES_TIPO_ENTIDAD { get; set; } = string.Empty;
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_MONEDA { get; set; } = string.Empty;
}

public class CuentaCorrienteInsertDto
{
    public decimal? COD_ENTIDAD { get; set; }
    [Required] public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_IMPORTE_SALDO { get; set; }
}

public class CuentaCorrienteUpdateDto
{
    public decimal? COD_ENTIDAD { get; set; }
    [Required] public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_IMPORTE_SALDO { get; set; }
}

public class CuentaCorrienteSearchDto
{
    public string? Search { get; set; }
}