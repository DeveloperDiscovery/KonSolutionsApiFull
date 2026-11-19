using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad TIPOS_REQUERIMIENTO - tipos equivalentes a la BD.</summary>
public class TiposRequerimiento
{
    public string COD_TIPO_REQUERIMIENTO { get; set; } = string.Empty;
    public string DES_TIPO_REQUERIMIENTO { get; set; } = string.Empty;
    public string? COD_TIPO_RESERVA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }

    // Marcas que el procedimiento espera desde siempre y que faltaban acá: sin ellas
    // el INSERT rechaza el alta por parámetro no especificado.
    public bool? FLG_TRANSFERENCIA { get; set; }   // transferencias entre almacenes
    public bool? FLG_PRODUCCION { get; set; }   // pedidos para producción
    public bool? FLG_COMPRA { get; set; }   // pedidos que derivan en compra
    public bool? FLG_CONSUMO_INTERNO { get; set; }   // consumo interno
    public bool? FLG_REPOSICION { get; set; }
    public bool? FLG_VENTA { get; set; }

    // Independiente de las seis de arriba: aquellas son excluyentes entre sí, ésta no.
    // Un tipo puede ser de compra Y de consumo de producción a la vez.
    public bool? FLG_CONSUMO_PRODUCCION { get; set; }

    // ── Solo lectura: traído por SEARCH/SHOWALL/SHOWBYID/SHOWCOMBOX vía JOIN ──
    public string DES_TIPO_RESERVA { get; set; } = string.Empty;
    public string DES_TIPO_ESTADO { get; set; } = string.Empty;
}

public class TiposRequerimientoInsertDto
{
    [Required] public string COD_TIPO_REQUERIMIENTO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_REQUERIMIENTO { get; set; } = string.Empty;
    public string? COD_TIPO_RESERVA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }

    // Marcas que el procedimiento espera desde siempre y que faltaban acá: sin ellas
    // el INSERT rechaza el alta por parámetro no especificado.
    public bool? FLG_TRANSFERENCIA { get; set; }   // transferencias entre almacenes
    public bool? FLG_PRODUCCION { get; set; }   // pedidos para producción
    public bool? FLG_COMPRA { get; set; }   // pedidos que derivan en compra
    public bool? FLG_CONSUMO_INTERNO { get; set; }   // consumo interno
    public bool? FLG_REPOSICION { get; set; }
    public bool? FLG_VENTA { get; set; }

    // Independiente de las seis de arriba: aquellas son excluyentes entre sí, ésta no.
    // Un tipo puede ser de compra Y de consumo de producción a la vez.
    public bool? FLG_CONSUMO_PRODUCCION { get; set; }
}

public class TiposRequerimientoUpdateDto
{
    [Required] public string COD_TIPO_REQUERIMIENTO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_REQUERIMIENTO { get; set; } = string.Empty;
    public string? COD_TIPO_RESERVA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }

    // Marcas que el procedimiento espera desde siempre y que faltaban acá: sin ellas
    // el INSERT rechaza el alta por parámetro no especificado.
    public bool? FLG_TRANSFERENCIA { get; set; }   // transferencias entre almacenes
    public bool? FLG_PRODUCCION { get; set; }   // pedidos para producción
    public bool? FLG_COMPRA { get; set; }   // pedidos que derivan en compra
    public bool? FLG_CONSUMO_INTERNO { get; set; }   // consumo interno
    public bool? FLG_REPOSICION { get; set; }
    public bool? FLG_VENTA { get; set; }

    // Independiente de las seis de arriba: aquellas son excluyentes entre sí, ésta no.
    // Un tipo puede ser de compra Y de consumo de producción a la vez.
    public bool? FLG_CONSUMO_PRODUCCION { get; set; }
}

public class TiposRequerimientoSearchDto
{
    public string? Search { get; set; }
}