using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad CONFIGURACION_PEDIDOS_COMERCIALES - tipos equivalentes a la BD.</summary>
public class ConfiguracionPedidosComerciales
{
    public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
}

public class ConfiguracionPedidosComercialesInsertDto
{
    [Required] public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    [Required] public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ConfiguracionPedidosComercialesUpdateDto
{
    [Required] public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    [Required] public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ConfiguracionPedidosComercialesSearchDto
{
    public string? Search { get; set; }
}