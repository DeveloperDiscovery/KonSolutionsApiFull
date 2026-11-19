using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Finanzas;

/// <summary>Entidad ENTIDADES_CUENTAS_BANCARIAS - tipos equivalentes a la BD.</summary>
public class EntidadesCuentasBancarias
{
    public decimal? COD_ENTIDAD_BANCARIA { get; set; }
    public string COD_BANCO { get; set; } = string.Empty;
    public string NUM_CUENTA_BANCARIA { get; set; } = string.Empty;
    public string? NUM_CUENTA_BANCARIA_CCI { get; set; }
    public string? DES_CUENTA_BANCARIA { get; set; }
    public bool? FLG_PRINCIPAL { get; set; }
    public bool? FLG_HABILITADA { get; set; }
    public bool? FLG_CUENTA_EMPRESA { get; set; }
    public bool? FLG_COBROS { get; set; }
    public bool? FLG_PAGOS { get; set; }
    public bool? FLG_RESTRINGIDA { get; set; }
    public string COD_TIPO_CUENTA_BANCARIA { get; set; } = string.Empty;
    public string COD_MONEDA { get; set; } = string.Empty;
    public string DES_BANCO { get; set; } = string.Empty;
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string? DES_PREFIJO_USUARIO_COMERCIAL { get; set; }
    public string DES_TIPO_CUENTA_BANCARIA { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class EntidadesCuentasBancariasInsertDto
{
    public decimal? COD_ENTIDAD_BANCARIA { get; set; }
    [Required] public string COD_BANCO { get; set; } = string.Empty;
    [Required] public string NUM_CUENTA_BANCARIA { get; set; } = string.Empty;
    public string? NUM_CUENTA_BANCARIA_CCI { get; set; }
    public string? DES_CUENTA_BANCARIA { get; set; }
    public bool? FLG_PRINCIPAL { get; set; }
    public bool? FLG_HABILITADA { get; set; }
    public bool? FLG_CUENTA_EMPRESA { get; set; }
    public bool? FLG_COBROS { get; set; }
    public bool? FLG_PAGOS { get; set; }
    public bool? FLG_RESTRINGIDA { get; set; }
    [Required] public string COD_TIPO_CUENTA_BANCARIA { get; set; } = string.Empty;
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
}

public class EntidadesCuentasBancariasUpdateDto
{
    public decimal? COD_ENTIDAD_BANCARIA { get; set; }
    [Required] public string COD_BANCO { get; set; } = string.Empty;
    [Required] public string NUM_CUENTA_BANCARIA { get; set; } = string.Empty;
    public string? NUM_CUENTA_BANCARIA_CCI { get; set; }
    public string? DES_CUENTA_BANCARIA { get; set; }
    public bool? FLG_PRINCIPAL { get; set; }
    public bool? FLG_HABILITADA { get; set; }
    public bool? FLG_CUENTA_EMPRESA { get; set; }
    public bool? FLG_COBROS { get; set; }
    public bool? FLG_PAGOS { get; set; }
    public bool? FLG_RESTRINGIDA { get; set; }
    [Required] public string COD_TIPO_CUENTA_BANCARIA { get; set; } = string.Empty;
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
}

public class EntidadesCuentasBancariasSearchDto
{
    public string? Search { get; set; }
}
