using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Contabilidad;

/// <summary>Entidad CUENTAS_NATURALEZA - tipos equivalentes a la BD.</summary>
public class CuentasNaturaleza
{
    public string COD_CUENTA_NATURALEZA { get; set; } = string.Empty;
    public string DES_CUENTA_NATURALEZA { get; set; } = string.Empty;
    public string DES_CUENTA_NATURALEZA_ALTERNATIVA { get; set; } = string.Empty;
}

public class CuentasNaturalezaInsertDto
{
    [Required] public string COD_CUENTA_NATURALEZA { get; set; } = string.Empty;
    [Required] public string DES_CUENTA_NATURALEZA { get; set; } = string.Empty;
    [Required] public string DES_CUENTA_NATURALEZA_ALTERNATIVA { get; set; } = string.Empty;
}

public class CuentasNaturalezaUpdateDto
{
    [Required] public string COD_CUENTA_NATURALEZA { get; set; } = string.Empty;
    [Required] public string DES_CUENTA_NATURALEZA { get; set; } = string.Empty;
    [Required] public string DES_CUENTA_NATURALEZA_ALTERNATIVA { get; set; } = string.Empty;
}

public class CuentasNaturalezaSearchDto
{
    public string? Search { get; set; }
}