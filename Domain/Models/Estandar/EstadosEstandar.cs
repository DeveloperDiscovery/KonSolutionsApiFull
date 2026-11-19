using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad ESTADOS_ESTANDAR - tipos equivalentes a la BD.</summary>
public class EstadosEstandar
{
    public int? COD_INDICE { get; set; }
    public int? COD_ESTADO { get; set; }
    public string DES_ESTADO { get; set; } = string.Empty;
    public string? COD_ESTADO_ALIAS { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public byte[]? IMG_PICTURE { get; set; }
}

public class EstadosEstandarInsertDto
{
    public int? COD_INDICE { get; set; }
    public int? COD_ESTADO { get; set; }
    [Required] public string DES_ESTADO { get; set; } = string.Empty;
    public string? COD_ESTADO_ALIAS { get; set; }
    [Required] public string DES_BACKCOLOR { get; set; } = string.Empty;
    [Required] public string DES_FORECOLOR { get; set; } = string.Empty;
    public byte[]? IMG_PICTURE { get; set; }
}

public class EstadosEstandarUpdateDto
{
    public int? COD_INDICE { get; set; }
    public int? COD_ESTADO { get; set; }
    [Required] public string DES_ESTADO { get; set; } = string.Empty;
    public string? COD_ESTADO_ALIAS { get; set; }
    [Required] public string DES_BACKCOLOR { get; set; } = string.Empty;
    [Required] public string DES_FORECOLOR { get; set; } = string.Empty;
    public byte[]? IMG_PICTURE { get; set; }
}

public class EstadosEstandarSearchDto
{
    public string? Search { get; set; }
}