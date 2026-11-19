using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Contabilidad;

/// <summary>Entidad TIPOS_CUENTA_CONTABLE - tipos equivalentes a la BD.</summary>
public class TiposCuentaContable
{
    public string COD_TIPO_CUENTA_CONTABLE { get; set; } = string.Empty;
    public string DES_TIPO_CUENTA_CONTABLE { get; set; } = string.Empty;
    public string? COD_CUENTA_NATURALEZA { get; set; }
    public bool? FLG_AFECTA_BALANCE { get; set; }
    public bool? FLG_AFECTA_RESULTADO { get; set; }

    // Solo lectura (JOIN a CONTABILIDAD.CUENTAS_NATURALEZA).
    public string? DES_CUENTA_NATURALEZA { get; set; }
    public string? DES_CUENTA_NATURALEZA_ALTERNATIVA { get; set; }
}

public class TiposCuentaContableInsertDto
{
    [Required] public string COD_TIPO_CUENTA_CONTABLE { get; set; } = string.Empty;
    [Required] public string DES_TIPO_CUENTA_CONTABLE { get; set; } = string.Empty;
    public string? COD_CUENTA_NATURALEZA { get; set; }
    public bool? FLG_AFECTA_BALANCE { get; set; }
    public bool? FLG_AFECTA_RESULTADO { get; set; }
}

public class TiposCuentaContableUpdateDto
{
    [Required] public string COD_TIPO_CUENTA_CONTABLE { get; set; } = string.Empty;
    [Required] public string DES_TIPO_CUENTA_CONTABLE { get; set; } = string.Empty;
    public string? COD_CUENTA_NATURALEZA { get; set; }
    public bool? FLG_AFECTA_BALANCE { get; set; }
    public bool? FLG_AFECTA_RESULTADO { get; set; }
}

public class TiposCuentaContableSearchDto
{
    public string? Search { get; set; }
}
