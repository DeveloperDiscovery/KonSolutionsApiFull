using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad PEDIDOS_COMERCIALES_SECUENCIAS - tipos equivalentes a la BD.</summary>
public class PedidosComercialesSecuencias
{
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? CAN_SOLICITADA { get; set; }
    public decimal? CAN_TOLERANCIA_POSITIVA { get; set; }
    public decimal? CAN_TOLERANCIA_NEGATIVA { get; set; }
    public decimal? TAS_TOLERANCIA_POSITIVA { get; set; }
    public decimal? TAS_TOLERANCIA_NEGATIVA { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_PEDIDO_COMERCIAL { get; set; } = string.Empty;
}

public class PedidosComercialesSecuenciasInsertDto
{
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? CAN_SOLICITADA { get; set; }
    public decimal? CAN_TOLERANCIA_POSITIVA { get; set; }
    public decimal? CAN_TOLERANCIA_NEGATIVA { get; set; }
    public decimal? TAS_TOLERANCIA_POSITIVA { get; set; }
    public decimal? TAS_TOLERANCIA_NEGATIVA { get; set; }
}

public class PedidosComercialesSecuenciasUpdateDto
{
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? CAN_SOLICITADA { get; set; }
    public decimal? CAN_TOLERANCIA_POSITIVA { get; set; }
    public decimal? CAN_TOLERANCIA_NEGATIVA { get; set; }
    public decimal? TAS_TOLERANCIA_POSITIVA { get; set; }
    public decimal? TAS_TOLERANCIA_NEGATIVA { get; set; }
}

public class PedidosComercialesSecuenciasSearchDto
{
    public string? Search { get; set; }
}