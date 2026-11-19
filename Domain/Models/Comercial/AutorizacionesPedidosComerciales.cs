using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad AUTORIZACIONES_PEDIDOS_COMERCIALES - tipos equivalentes a la BD.</summary>
public class AutorizacionesPedidosComerciales
{
    public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_AUTORIZADA { get; set; }
    public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_MINIMO { get; set; }
    public decimal? VAL_MAXIMO { get; set; }
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
    public string DES_TIPO_OPERACION { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class AutorizacionesPedidosComercialesInsertDto
{
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_AUTORIZADA { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_MINIMO { get; set; }
    public decimal? VAL_MAXIMO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class AutorizacionesPedidosComercialesUpdateDto
{
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_AUTORIZADA { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? VAL_MINIMO { get; set; }
    public decimal? VAL_MAXIMO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class AutorizacionesPedidosComercialesSearchDto
{
    public string? Search { get; set; }
}