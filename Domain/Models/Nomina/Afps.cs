using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Nomina;

/// <summary>Entidad AFPS - tipos equivalentes a la BD.</summary>
public class Afps
{
    public string COD_AFP { get; set; } = string.Empty;
    public string DES_AFP { get; set; } = string.Empty;
    public bool? FLG_PRIVADA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
}

public class AfpsInsertDto
{
    [Required] public string COD_AFP { get; set; } = string.Empty;
    [Required] public string DES_AFP { get; set; } = string.Empty;
    public bool? FLG_PRIVADA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class AfpsUpdateDto
{
    [Required] public string COD_AFP { get; set; } = string.Empty;
    [Required] public string DES_AFP { get; set; } = string.Empty;
    public bool? FLG_PRIVADA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class AfpsSearchDto
{
    public string? Search { get; set; }
}