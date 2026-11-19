using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO - tipos equivalentes a la BD.</summary>
public class EntidadesPurcharseOrdersEspecificacionesDespacho
{
    public decimal? COD_ENTIDAD { get; set; }
    public string NUM_PURCHASE_ORDER { get; set; } = string.Empty;
    public int? NUM_SECUENCIA { get; set; }
    public decimal? CAN_EMBALAJE { get; set; }
    public string COD_PRODUCTO_EMBALAJE_INNERPACK { get; set; } = string.Empty;
    public string COD_PRODUCTO_EMBALAJE_MASTERPACK { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_DISENO_ENTIDAD { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
    public string COD_PRODUCTO { get; set; } = string.Empty;
}

public class EntidadesPurcharseOrdersEspecificacionesDespachoInsertDto
{
    public decimal? COD_ENTIDAD { get; set; }
    [Required] public string NUM_PURCHASE_ORDER { get; set; } = string.Empty;
    public int? NUM_SECUENCIA { get; set; }
    public decimal? CAN_EMBALAJE { get; set; }
    [Required] public string COD_PRODUCTO_EMBALAJE_INNERPACK { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO_EMBALAJE_MASTERPACK { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EntidadesPurcharseOrdersEspecificacionesDespachoUpdateDto
{
    public decimal? COD_ENTIDAD { get; set; }
    [Required] public string NUM_PURCHASE_ORDER { get; set; } = string.Empty;
    public int? NUM_SECUENCIA { get; set; }
    public decimal? CAN_EMBALAJE { get; set; }
    [Required] public string COD_PRODUCTO_EMBALAJE_INNERPACK { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO_EMBALAJE_MASTERPACK { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EntidadesPurcharseOrdersEspecificacionesDespachoSearchDto
{
    public string? Search { get; set; }
}