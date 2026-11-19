using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad TIPOS_ESTADOS - tipos equivalentes a la BD.</summary>
public class TiposEstados
{
    public int? COD_TIPO_ESTADO { get; set; }
    public string DES_TIPO_ESTADO { get; set; } = string.Empty;
    public byte[]? IMG_PICTURE { get; set; }
}

public class TiposEstadosInsertDto
{
    public int? COD_TIPO_ESTADO { get; set; }
    [Required] public string DES_TIPO_ESTADO { get; set; } = string.Empty;
    public byte[]? IMG_PICTURE { get; set; }
}

public class TiposEstadosUpdateDto
{
    public int? COD_TIPO_ESTADO { get; set; }
    [Required] public string DES_TIPO_ESTADO { get; set; } = string.Empty;
    public byte[]? IMG_PICTURE { get; set; }
}

public class TiposEstadosSearchDto
{
    public string? Search { get; set; }
}