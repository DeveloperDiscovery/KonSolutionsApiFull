using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Finanzas;

/// <summary>Entidad TIPOS_CUENTAS_BANCARIAS - tipos equivalentes a la BD.</summary>
public class TiposCuentasBancarias
{
    public string COD_TIPO_CUENTA_BANCARIA { get; set; } = string.Empty;
    public string DES_TIPO_CUENTA_BANCARIA { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposCuentasBancariasInsertDto
{
    [Required] public string COD_TIPO_CUENTA_BANCARIA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_CUENTA_BANCARIA { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposCuentasBancariasUpdateDto
{
    [Required] public string COD_TIPO_CUENTA_BANCARIA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_CUENTA_BANCARIA { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposCuentasBancariasSearchDto
{
    public string? Search { get; set; }
}