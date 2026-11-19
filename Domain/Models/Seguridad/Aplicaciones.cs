using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Seguridad;

public class Aplicaciones
{
    public string COD_APLICACION { get; set; } = string.Empty;
    public string DES_APLICACION { get; set; } = string.Empty;
    public bool? FLG_APP_MOVIL { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string DES_BACKCOLOR        { get; set; } = string.Empty;
    public string DES_FORECOLOR        { get; set; } = string.Empty;
    public string DES_ESTADO           { get; set; } = string.Empty;
    public string DES_BACKCOLOR_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR_ESTADO { get; set; } = string.Empty;
}

public class AplicacionesInsertDto
{
    [Required] public string COD_APLICACION { get; set; } = string.Empty;
    [Required] public string DES_APLICACION { get; set; } = string.Empty;
    public bool? FLG_APP_MOVIL { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public string  DES_BACKCOLOR { get; set; } = "";
    public string  DES_FORECOLOR { get; set; } = "";
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
}

public class AplicacionesUpdateDto
{
    [Required] public string COD_APLICACION { get; set; } = string.Empty;
    [Required] public string DES_APLICACION { get; set; } = string.Empty;
    public bool? FLG_APP_MOVIL { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public string  DES_BACKCOLOR { get; set; } = "";
    public string  DES_FORECOLOR { get; set; } = "";
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
}

public class AplicacionesSearchDto
{
    public string? Search { get; set; }
}