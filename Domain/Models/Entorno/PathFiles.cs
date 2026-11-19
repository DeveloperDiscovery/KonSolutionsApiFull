using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Entorno;

/// <summary>Entidad ENTORNO.PATHFILES - tipos equivalentes a la BD.</summary>
public class PathFiles
{
    public string COD_KEY_PATHFILE { get; set; } = string.Empty;
    public string DES_KEY_PATHFILE { get; set; } = string.Empty;
    public string? COD_KEY_SERVIDOR { get; set; }
    public string? DES_PATH_FILE { get; set; }
    public bool? FLG_YEAR { get; set; }
    public bool? FLG_MONTH { get; set; }
    public bool? FLG_WEEK { get; set; }
    public bool? FLG_DAY { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string? DES_KEY_IP { get; set; }
}

public class PathFilesInsertDto
{
    [Required] public string COD_KEY_PATHFILE { get; set; } = string.Empty;
    [Required] public string DES_KEY_PATHFILE { get; set; } = string.Empty;
    public string? COD_KEY_SERVIDOR { get; set; }
    public bool? FLG_YEAR { get; set; }
    public bool? FLG_MONTH { get; set; }
    public bool? FLG_WEEK { get; set; }
    public bool? FLG_DAY { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PathFilesSearchDto
{
    public string? Search { get; set; }
}
