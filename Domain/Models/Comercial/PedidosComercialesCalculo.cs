using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad PEDIDOS_COMERCIALES_CALCULO - tipos equivalentes a la BD.</summary>
public class PedidosComercialesCalculo
{
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public string COD_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
    public string COD_TIPO_IMPUESTO { get; set; } = string.Empty;
    public decimal? TAS_IMPUESTO { get; set; }
    public decimal? VAL_CALCULO { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_PEDIDO_COMERCIAL { get; set; } = string.Empty;
    public string DES_TIPO_IMPUESTO { get; set; } = string.Empty;
    public string DES_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
}

public class PedidosComercialesCalculoInsertDto
{
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    [Required] public string COD_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_IMPUESTO { get; set; } = string.Empty;
    public decimal? TAS_IMPUESTO { get; set; }
    public decimal? VAL_CALCULO { get; set; }
}

public class PedidosComercialesCalculoUpdateDto
{
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    [Required] public string COD_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_IMPUESTO { get; set; } = string.Empty;
    public decimal? TAS_IMPUESTO { get; set; }
    public decimal? VAL_CALCULO { get; set; }
}

public class PedidosComercialesCalculoSearchDto
{
    public string? Search { get; set; }
}