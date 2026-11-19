using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad PERSONAL_IMPLEMENTOS - tipos equivalentes a la BD.</summary>
public class PersonalImplementos
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public string COD_PRODUCTO_IMPLEMENTO { get; set; } = string.Empty;
    public string COD_TIPO_IMPLEMENTO_LABORAL { get; set; } = string.Empty;
    public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? COD_ITEM_TECNICO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
    public string DES_TIPO_IMPLEMENTO_LABORAL { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
    public string COD_PRODUCTO { get; set; } = string.Empty;
}

public class PersonalImplementosInsertDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    [Required] public string COD_PRODUCTO_IMPLEMENTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_IMPLEMENTO_LABORAL { get; set; } = string.Empty;
    [Required] public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? COD_ITEM_TECNICO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalImplementosUpdateDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    [Required] public string COD_PRODUCTO_IMPLEMENTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_IMPLEMENTO_LABORAL { get; set; } = string.Empty;
    [Required] public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? COD_ITEM_TECNICO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalImplementosSearchDto
{
    public string? Search { get; set; }
}