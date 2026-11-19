using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad LOTES_CONTROL - tipos equivalentes a la BD.</summary>
public class LotesControl
{
    public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    public string DES_LOTE_CONTROL { get; set; } = string.Empty;
    public DateTime? FEC_LOTE_CONTROL { get; set; }
    public string COD_TIPO_LOTE_CONTROL { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
    public string? NUM_LOTE_CONTROL_ENTIDAD { get; set; }
    public decimal? VAL_TOTAL { get; set; }

    // La graba el procedimiento desde siempre; faltaba acá, así que la columna quedaba en NULL.
    public DateTime? FEC_VENCIMIENTO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
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
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_TIPO_LOTE_CONTROL { get; set; } = string.Empty;
}

public class LotesControlInsertDto
{
    /// <summary>Se manda vacío: lo asigna el servicio con el siguiente correlativo. Se acepta
    /// con valor para migraciones y para lotes que ya vienen numerados desde afuera.</summary>
    public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    [Required] public string DES_LOTE_CONTROL { get; set; } = string.Empty;
    public DateTime? FEC_LOTE_CONTROL { get; set; }
    // Clave foránea que la tabla admite vacía: quién decide si puede faltar es la base,
    // no el modelo del pedido. Exigirla acá rechazaba lotes sin tipo antes de consultarla.
    public string? COD_TIPO_LOTE_CONTROL { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
    public string? NUM_LOTE_CONTROL_ENTIDAD { get; set; }
    public decimal? VAL_TOTAL { get; set; }

    // La graba el procedimiento desde siempre; faltaba acá, así que la columna quedaba en NULL.
    public DateTime? FEC_VENCIMIENTO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    // Los pone el servidor desde el usuario autenticado, no el cliente. Declarados como
    // no anulables, ASP.NET los da por obligatorios y rechaza el pedido cuando llegan en
    // null —que es lo correcto que mande un cliente que no los conoce—.
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
}

public class LotesControlUpdateDto
{
    [Required] public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    [Required] public string DES_LOTE_CONTROL { get; set; } = string.Empty;
    public DateTime? FEC_LOTE_CONTROL { get; set; }
    // Clave foránea que la tabla admite vacía: quién decide si puede faltar es la base,
    // no el modelo del pedido. Exigirla acá rechazaba lotes sin tipo antes de consultarla.
    public string? COD_TIPO_LOTE_CONTROL { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
    public string? NUM_LOTE_CONTROL_ENTIDAD { get; set; }
    public decimal? VAL_TOTAL { get; set; }

    // La graba el procedimiento desde siempre; faltaba acá, así que la columna quedaba en NULL.
    public DateTime? FEC_VENCIMIENTO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    // Los pone el servidor desde el usuario autenticado, no el cliente. Declarados como
    // no anulables, ASP.NET los da por obligatorios y rechaza el pedido cuando llegan en
    // null —que es lo correcto que mande un cliente que no los conoce—.
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
}

public class LotesControlSearchDto
{
    public string? Search { get; set; }
}