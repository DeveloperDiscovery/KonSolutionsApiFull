using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad TIPOS_EMPRESA - tipos equivalentes a la BD.</summary>
public class TiposEmpresa
{
    public string COD_TIPO_EMPRESA { get; set; } = string.Empty;
    public string DES_TIPO_EMPRESA { get; set; } = string.Empty;
    public bool? FLG_PRIVADA { get; set; }
}

public class TiposEmpresaInsertDto
{
    [Required] public string COD_TIPO_EMPRESA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_EMPRESA { get; set; } = string.Empty;
    public bool? FLG_PRIVADA { get; set; }
}

public class TiposEmpresaUpdateDto
{
    [Required] public string COD_TIPO_EMPRESA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_EMPRESA { get; set; } = string.Empty;
    public bool? FLG_PRIVADA { get; set; }
}

public class TiposEmpresaSearchDto
{
    public string? Search { get; set; }
}