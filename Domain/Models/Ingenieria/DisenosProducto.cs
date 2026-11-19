using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Entidad DISENOS_PRODUCTO - tipos equivalentes a la BD.</summary>
public class DisenosProducto
{
    public string COD_DISENO { get; set; } = string.Empty;
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
    public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public string COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; } = string.Empty;
    public string COD_TIPO_DISENO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_DISENO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_UNIDAD_MEDIDA { get; set; } = string.Empty;
}

public class DisenosProductoInsertDto
{
    [Required] public string COD_DISENO { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string DES_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; } = string.Empty;
    [Required] public string COD_TIPO_DISENO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class DisenosProductoUpdateDto
{
    [Required] public string COD_DISENO { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string DES_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; } = string.Empty;
    [Required] public string COD_TIPO_DISENO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class DisenosProductoSearchDto
{
    public string? Search { get; set; }
}