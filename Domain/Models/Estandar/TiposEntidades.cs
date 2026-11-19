using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad TIPOS_ENTIDADES - tipos equivalentes a la BD.</summary>
public class TiposEntidades
{
    public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    public string DES_TIPO_ENTIDAD { get; set; } = string.Empty;
    public bool? FLG_PERSONAL_PROPIO { get; set; }
}

public class TiposEntidadesInsertDto
{
    [Required] public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    [Required] public string DES_TIPO_ENTIDAD { get; set; } = string.Empty;
    public bool? FLG_PERSONAL_PROPIO { get; set; }
}

public class TiposEntidadesUpdateDto
{
    [Required] public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    [Required] public string DES_TIPO_ENTIDAD { get; set; } = string.Empty;
    public bool? FLG_PERSONAL_PROPIO { get; set; }
}

public class TiposEntidadesSearchDto
{
    public string? Search { get; set; }
}