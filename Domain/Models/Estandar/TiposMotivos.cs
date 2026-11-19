using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad TIPOS_MOTIVOS - tipos equivalentes a la BD.</summary>
public class TiposMotivos
{
    public int? COD_TIPO_MOTIVO { get; set; }
    public string DES_TIPO_MOTIVO { get; set; } = string.Empty;
    public byte[]? IMG_PICTURE { get; set; }
}

public class TiposMotivosInsertDto
{
    public int? COD_TIPO_MOTIVO { get; set; }
    [Required] public string DES_TIPO_MOTIVO { get; set; } = string.Empty;
    public byte[]? IMG_PICTURE { get; set; }
}

public class TiposMotivosUpdateDto
{
    public int? COD_TIPO_MOTIVO { get; set; }
    [Required] public string DES_TIPO_MOTIVO { get; set; } = string.Empty;
    public byte[]? IMG_PICTURE { get; set; }
}

public class TiposMotivosSearchDto
{
    public string? Search { get; set; }
}