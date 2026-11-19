using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad PEDIDOS_COMERCIALES_ESPECIFICACIONES_TECNICAS_BASE - tipos equivalentes a la BD.</summary>
public class PedidosComercialesEspecificacionesTecnicasBase
{
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_PEDIDO_COMERCIAL { get; set; } = string.Empty;
}

public class PedidosComercialesEspecificacionesTecnicasBaseInsertDto
{
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    [Required] public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    [Required] public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PedidosComercialesEspecificacionesTecnicasBaseUpdateDto
{
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    [Required] public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    [Required] public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PedidosComercialesEspecificacionesTecnicasBaseSearchDto
{
    public string? Search { get; set; }
}