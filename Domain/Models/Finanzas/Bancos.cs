using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Finanzas;

/// <summary>Entidad BANCOS - tipos equivalentes a la BD.</summary>
public class Bancos
{
    public string COD_BANCO { get; set; } = string.Empty;
    public string DES_BANCO { get; set; } = string.Empty;
    public string COD_BANCO_SUNAT { get; set; } = string.Empty;
    public string? COD_PAIS { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;

    // Solo lectura (JOIN a ESTANDAR.PAISES).
    public string? DES_PAIS { get; set; }
    public string? DES_BANDERA_EMOJI { get; set; }
}

public class BancosInsertDto
{
    [Required] public string COD_BANCO { get; set; } = string.Empty;
    [Required] public string DES_BANCO { get; set; } = string.Empty;
    [Required] public string COD_BANCO_SUNAT { get; set; } = string.Empty;
    public string? COD_PAIS { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
}

public class BancosUpdateDto
{
    [Required] public string COD_BANCO { get; set; } = string.Empty;
    [Required] public string DES_BANCO { get; set; } = string.Empty;
    [Required] public string COD_BANCO_SUNAT { get; set; } = string.Empty;
    public string? COD_PAIS { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
}

public class BancosSearchDto
{
    public string? Search { get; set; }
}
