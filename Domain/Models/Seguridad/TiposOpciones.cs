using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Seguridad;

public class TiposOpciones
{
    public string COD_TIPO_OPCION_APLICACION { get; set; } = string.Empty;
    public string DES_TIPO_OPCION_APLICACION { get; set; } = string.Empty;
    public bool? FLG_MODULO { get; set; }
    public bool? FLG_SEPARADOR { get; set; }
    public bool? FLG_CARPETA { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposOpcionesInsertDto
{
    [Required] public string COD_TIPO_OPCION_APLICACION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_OPCION_APLICACION { get; set; } = string.Empty;
    public bool? FLG_MODULO { get; set; }
    public bool? FLG_SEPARADOR { get; set; }
    public bool? FLG_CARPETA { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposOpcionesUpdateDto
{
    [Required] public string COD_TIPO_OPCION_APLICACION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_OPCION_APLICACION { get; set; } = string.Empty;
    public bool? FLG_MODULO { get; set; }
    public bool? FLG_SEPARADOR { get; set; }
    public bool? FLG_CARPETA { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposOpcionesSearchDto
{
    public string? Search { get; set; }
}