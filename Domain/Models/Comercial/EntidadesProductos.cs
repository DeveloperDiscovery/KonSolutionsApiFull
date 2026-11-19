using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad ENTIDADES_PRODUCTOS - tipos equivalentes a la BD.</summary>
public class EntidadesProductos
{
    public decimal? COD_ENTIDAD { get; set; }
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public string COD_PRODUCTO_ENTIDAD { get; set; } = string.Empty;
    public string DES_PRODUCTO_ENTIDAD { get; set; } = string.Empty;
    public string COD_UNIDAD_MEDIDA_ATENCION { get; set; } = string.Empty;
    public decimal? FAC_CONVERSION { get; set; }
    public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_PRECIO_ENTIDAD { get; set; }
    public decimal? TAS_DESCUENTO { get; set; }
    public int? CAN_DIAS_ATENCION { get; set; }
    public bool? FLG_DIAS_HABILES { get; set; }
    public decimal? CAN_TOLERANCIA_EXCESO { get; set; }
    public bool? FLG_TOLERANCIA_EMBALAJE { get; set; }
    public decimal? CAN_TOLERANCIA_DEFICIT { get; set; }
    public decimal? TAS_TOLERANCIA_EXCESO { get; set; }
    public decimal? TAS_TOLERANCIA_DEFICIT { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_MONEDA { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
    public string DES_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
}

public class EntidadesProductosInsertDto
{
    public decimal? COD_ENTIDAD { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO_ENTIDAD { get; set; } = string.Empty;
    [Required] public string DES_PRODUCTO_ENTIDAD { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA_ATENCION { get; set; } = string.Empty;
    [Range(typeof(decimal), "0.000001", "9999.999999", ParseLimitsInInvariantCulture = true, ErrorMessage = "El factor de conversión tiene que ser mayor que cero (máximo 9999.999999).")]
    public decimal? FAC_CONVERSION { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_PRECIO_ENTIDAD { get; set; }
    public decimal? TAS_DESCUENTO { get; set; }
    public int? CAN_DIAS_ATENCION { get; set; }
    public bool? FLG_DIAS_HABILES { get; set; }
    public decimal? CAN_TOLERANCIA_EXCESO { get; set; }
    public bool? FLG_TOLERANCIA_EMBALAJE { get; set; }
    public decimal? CAN_TOLERANCIA_DEFICIT { get; set; }
    public decimal? TAS_TOLERANCIA_EXCESO { get; set; }
    public decimal? TAS_TOLERANCIA_DEFICIT { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EntidadesProductosUpdateDto
{
    public decimal? COD_ENTIDAD { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO_ENTIDAD { get; set; } = string.Empty;
    [Required] public string DES_PRODUCTO_ENTIDAD { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA_ATENCION { get; set; } = string.Empty;
    [Range(typeof(decimal), "0.000001", "9999.999999", ParseLimitsInInvariantCulture = true, ErrorMessage = "El factor de conversión tiene que ser mayor que cero (máximo 9999.999999).")]
    public decimal? FAC_CONVERSION { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_PRECIO_ENTIDAD { get; set; }
    public decimal? TAS_DESCUENTO { get; set; }
    public int? CAN_DIAS_ATENCION { get; set; }
    public bool? FLG_DIAS_HABILES { get; set; }
    public decimal? CAN_TOLERANCIA_EXCESO { get; set; }
    public bool? FLG_TOLERANCIA_EMBALAJE { get; set; }
    public decimal? CAN_TOLERANCIA_DEFICIT { get; set; }
    public decimal? TAS_TOLERANCIA_EXCESO { get; set; }
    public decimal? TAS_TOLERANCIA_DEFICIT { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EntidadesProductosSearchDto
{
    public string? Search { get; set; }
}