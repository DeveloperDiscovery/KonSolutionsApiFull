using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad TIPOS_RESERVA_STOCK - tipos equivalentes a la BD.</summary>
public class TiposReservaStock
{
    public string COD_TIPO_RESERVA { get; set; } = string.Empty;
    public string DES_TIPO_RESERVA { get; set; } = string.Empty;

    // Marcas que el procedimiento espera desde siempre y que faltaban acá: sin ellas
    // el INSERT rechaza el alta por parámetro no especificado.
    public bool? FLG_VENTA { get; set; }   // reservas que respaldan una venta
    public bool? FLG_PRODUCCION { get; set; }   // reservas que alimentan producción
}

public class TiposReservaStockInsertDto
{
    [Required] public string COD_TIPO_RESERVA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_RESERVA { get; set; } = string.Empty;

    // Marcas que el procedimiento espera desde siempre y que faltaban acá: sin ellas
    // el INSERT rechaza el alta por parámetro no especificado.
    public bool? FLG_VENTA { get; set; }   // reservas que respaldan una venta
    public bool? FLG_PRODUCCION { get; set; }   // reservas que alimentan producción
}

public class TiposReservaStockUpdateDto
{
    [Required] public string COD_TIPO_RESERVA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_RESERVA { get; set; } = string.Empty;

    // Marcas que el procedimiento espera desde siempre y que faltaban acá: sin ellas
    // el INSERT rechaza el alta por parámetro no especificado.
    public bool? FLG_VENTA { get; set; }   // reservas que respaldan una venta
    public bool? FLG_PRODUCCION { get; set; }   // reservas que alimentan producción
}

public class TiposReservaStockSearchDto
{
    public string? Search { get; set; }
}