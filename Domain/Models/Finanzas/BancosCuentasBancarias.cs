using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Finanzas;

/// <summary>Entidad BANCOS_CUENTAS_BANCARIAS - tipos equivalentes a la BD.</summary>
public class BancosCuentasBancarias
{
    public string COD_BANCO { get; set; } = string.Empty;
    public string NUM_CUENTA_BANCARIA { get; set; } = string.Empty;
    public string? NUM_CUENTA_BANCARIA_CCI { get; set; }
    public string COD_TIPO_CUENTA_BANCARIA { get; set; } = string.Empty;
    public string COD_MONEDA { get; set; } = string.Empty;
    public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
    public decimal? VAL_SALDO_INICIAL { get; set; }
    public decimal? VAL_MOVIMIENTOS_INGRESO { get; set; }
    public decimal? VAL_MOVIMIENTOS_SALIDA { get; set; }
    public decimal? VAL_SALDO_FINAL { get; set; }
    public bool? FLG_PRINCIPAL { get; set; }
    public bool? FLG_HABILITADA { get; set; }
    public bool? FLG_CUENTA_EMPRESA { get; set; }
    public bool? FLG_COBROS { get; set; }
    public bool? FLG_PAGOS { get; set; }
    public bool? FLG_RESTRINGIDA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_BANCO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_MONEDA { get; set; } = string.Empty;
    public string DES_TIPO_CUENTA_BANCARIA { get; set; } = string.Empty;
}

public class BancosCuentasBancariasInsertDto
{
    [Required] public string COD_BANCO { get; set; } = string.Empty;
    [Required] public string NUM_CUENTA_BANCARIA { get; set; } = string.Empty;
    public string? NUM_CUENTA_BANCARIA_CCI { get; set; }
    [Required] public string COD_TIPO_CUENTA_BANCARIA { get; set; } = string.Empty;
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    [Required] public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
    public decimal? VAL_SALDO_INICIAL { get; set; }
    public decimal? VAL_MOVIMIENTOS_INGRESO { get; set; }
    public decimal? VAL_MOVIMIENTOS_SALIDA { get; set; }
    public decimal? VAL_SALDO_FINAL { get; set; }
    public bool? FLG_PRINCIPAL { get; set; }
    public bool? FLG_HABILITADA { get; set; }
    public bool? FLG_CUENTA_EMPRESA { get; set; }
    public bool? FLG_COBROS { get; set; }
    public bool? FLG_PAGOS { get; set; }
    public bool? FLG_RESTRINGIDA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
}

public class BancosCuentasBancariasUpdateDto
{
    [Required] public string COD_BANCO { get; set; } = string.Empty;
    [Required] public string NUM_CUENTA_BANCARIA { get; set; } = string.Empty;
    public string? NUM_CUENTA_BANCARIA_CCI { get; set; }
    [Required] public string COD_TIPO_CUENTA_BANCARIA { get; set; } = string.Empty;
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    [Required] public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
    public decimal? VAL_SALDO_INICIAL { get; set; }
    public decimal? VAL_MOVIMIENTOS_INGRESO { get; set; }
    public decimal? VAL_MOVIMIENTOS_SALIDA { get; set; }
    public decimal? VAL_SALDO_FINAL { get; set; }
    public bool? FLG_PRINCIPAL { get; set; }
    public bool? FLG_HABILITADA { get; set; }
    public bool? FLG_CUENTA_EMPRESA { get; set; }
    public bool? FLG_COBROS { get; set; }
    public bool? FLG_PAGOS { get; set; }
    public bool? FLG_RESTRINGIDA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
}

public class BancosCuentasBancariasSearchDto
{
    public string? Search { get; set; }
}
