using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS - tipos equivalentes a la BD.</summary>
public class EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas
{
    public decimal? COD_ENTIDAD { get; set; }
    public string NUM_PURCHASE_ORDER { get; set; } = string.Empty;
    public int? NUM_SECUENCIA { get; set; }
    public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? COD_ITEM_TECNICO { get; set; }
    public bool? FLG_HEADER { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
}

public class EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasInsertDto
{
    public decimal? COD_ENTIDAD { get; set; }
    [Required] public string NUM_PURCHASE_ORDER { get; set; } = string.Empty;
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? COD_ITEM_TECNICO { get; set; }
    public bool? FLG_HEADER { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasUpdateDto
{
    public decimal? COD_ENTIDAD { get; set; }
    [Required] public string NUM_PURCHASE_ORDER { get; set; } = string.Empty;
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? COD_ITEM_TECNICO { get; set; }
    public bool? FLG_HEADER { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasSearchDto
{
    public string? Search { get; set; }
}