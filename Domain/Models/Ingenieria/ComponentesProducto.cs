using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Entidad COMPONENTES_PRODUCTO - tipos equivalentes a la BD.</summary>
public class ComponentesProducto
{
    public string COD_COMPONENTE_PRODUCTO { get; set; } = string.Empty;
    public string DES_COMPONENTE_PRODUCTO { get; set; } = string.Empty;
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
}

public class ComponentesProductoInsertDto
{
    [Required] public string COD_COMPONENTE_PRODUCTO { get; set; } = string.Empty;
    [Required] public string DES_COMPONENTE_PRODUCTO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ComponentesProductoUpdateDto
{
    [Required] public string COD_COMPONENTE_PRODUCTO { get; set; } = string.Empty;
    [Required] public string DES_COMPONENTE_PRODUCTO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ComponentesProductoSearchDto
{
    public string? Search { get; set; }
}