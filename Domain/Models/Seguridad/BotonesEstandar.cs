using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Seguridad;

public class BotonesEstandar
{
    public string COD_BOTON_OPCION_ESTANDAR { get; set; } = string.Empty;
    public string DES_BOTON_OPCION_ESTANDAR { get; set; } = string.Empty;
    public bool? FLG_GRILLA { get; set; }
    public bool? FLG_HEADER { get; set; }
    public bool? FLG_DIALOG { get; set; }
    public bool? FLG_ACCION_POST { get; set; }
    public bool? FLG_ACCION_PUT { get; set; }
    public bool? FLG_ACCION_DELETE { get; set; }
    public bool? FLG_ACCION_GET { get; set; }
    public string? DES_METODO { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
}

public class BotonesEstandarInsertDto
{
    [Required] public string COD_BOTON_OPCION_ESTANDAR { get; set; } = string.Empty;
    [Required] public string DES_BOTON_OPCION_ESTANDAR { get; set; } = string.Empty;
    public bool? FLG_GRILLA { get; set; }
    public bool? FLG_HEADER { get; set; }
    public bool? FLG_DIALOG { get; set; }
    public bool? FLG_ACCION_POST { get; set; }
    public bool? FLG_ACCION_PUT { get; set; }
    public bool? FLG_ACCION_DELETE { get; set; }
    public bool? FLG_ACCION_GET { get; set; }
    public string? DES_METODO { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class BotonesEstandarUpdateDto
{
    [Required] public string COD_BOTON_OPCION_ESTANDAR { get; set; } = string.Empty;
    [Required] public string DES_BOTON_OPCION_ESTANDAR { get; set; } = string.Empty;
    public bool? FLG_GRILLA { get; set; }
    public bool? FLG_HEADER { get; set; }
    public bool? FLG_DIALOG { get; set; }
    public bool? FLG_ACCION_POST { get; set; }
    public bool? FLG_ACCION_PUT { get; set; }
    public bool? FLG_ACCION_DELETE { get; set; }
    public bool? FLG_ACCION_GET { get; set; }
    public string? DES_METODO { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class BotonesEstandarSearchDto
{
    public string? Search { get; set; }
}