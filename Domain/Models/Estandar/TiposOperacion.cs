using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad TIPOS_OPERACION - tipos equivalentes a la BD.</summary>
public class TiposOperacion
{
    public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public string DES_TIPO_OPERACION { get; set; } = string.Empty;
    public bool? FLG_POSITIVO { get; set; }
    public bool? FLG_PROVISION { get; set; }
    public bool? FLG_CANCELACION { get; set; }
    public bool? FLG_BIEN { get; set; }
    public bool? FLG_DEVOLUCION { get; set; }
    public bool? FLG_ANULACION { get; set; }
    public bool? FLG_ANTICIPO { get; set; }
    public bool? FLG_CENTRO_COSTO { get; set; }
    public bool? FLG_PROYECTO { get; set; }
    public bool? FLG_LOTE_CONTROL { get; set; }
    public bool? FLG_CAJA { get; set; }
    public bool? FLG_BANCO { get; set; }
    public bool? FLG_EXTORNO { get; set; }
    public bool? FLG_COMERCIO_EXTERIOR { get; set; }
    public bool? FLG_ATIENDE_REQUERIMIENTO_COMPRA { get; set; }
    public bool? FLG_COMPARACION_PRECIOS_COMPRA { get; set; }
    public bool? FLG_INCIDENCIA_PRODUCTO { get; set; }
    public bool? FLG_CARGO { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO_DEFAULT { get; set; }
    public string COD_TIPO_OPERACION_SUNAT { get; set; } = string.Empty;
    public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    public string COD_TIPO_PRODUCTO_DEFECTO { get; set; } = string.Empty;
    public int? COD_TIPO_PEDIDO_COMERCIAL { get; set; }
    public int? COD_TIPO_PRECIO { get; set; }
    public int? COD_CONDICION_PAGO { get; set; }
    public string COD_CLASE_OPERACION_SINCRONIZADA { get; set; } = string.Empty;
    public string COD_TIPO_OPERACION_SINCRONIZADA { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO_RELACIONADO { get; set; }
    public int? COD_TIPO_ESTADO_DETALLE_RELACIONADO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CLASE_OPERACION { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_TIPO_ENTIDAD { get; set; } = string.Empty;
    public string DES_TIPO_PEDIDO_COMERCIAL { get; set; } = string.Empty;
    public string DES_TIPO_PRECIO { get; set; } = string.Empty;
    public string DES_TIPO_PRODUCTO { get; set; } = string.Empty;
    public string COD_TIPO_PRODUCTO { get; set; } = string.Empty;
    public string? DES_CLASE_OPERACION_SINCRONIZADA { get; set; }
    public string? DES_TIPO_OPERACION_SINCRONIZADA { get; set; }
    public string? DES_TIPO_ESTADO_RELACIONADO { get; set; }
}

public class TiposOperacionInsertDto
{
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_OPERACION { get; set; } = string.Empty;
    public bool? FLG_POSITIVO { get; set; }
    public bool? FLG_PROVISION { get; set; }
    public bool? FLG_CANCELACION { get; set; }
    public bool? FLG_BIEN { get; set; }
    public bool? FLG_DEVOLUCION { get; set; }
    public bool? FLG_ANULACION { get; set; }
    public bool? FLG_ANTICIPO { get; set; }
    public bool? FLG_CENTRO_COSTO { get; set; }
    public bool? FLG_PROYECTO { get; set; }
    public bool? FLG_LOTE_CONTROL { get; set; }
    public bool? FLG_CAJA { get; set; }
    public bool? FLG_BANCO { get; set; }
    public bool? FLG_EXTORNO { get; set; }
    public bool? FLG_COMERCIO_EXTERIOR { get; set; }
    public bool? FLG_ATIENDE_REQUERIMIENTO_COMPRA { get; set; }
    public bool? FLG_COMPARACION_PRECIOS_COMPRA { get; set; }
    public bool? FLG_INCIDENCIA_PRODUCTO { get; set; }
    public bool? FLG_CARGO { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO_DEFAULT { get; set; }
    [Required] public string COD_TIPO_OPERACION_SUNAT { get; set; } = string.Empty;
    [Required] public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    [Required] public string COD_TIPO_PRODUCTO_DEFECTO { get; set; } = string.Empty;
    public int? COD_TIPO_PEDIDO_COMERCIAL { get; set; }
    public int? COD_TIPO_PRECIO { get; set; }
    public int? COD_CONDICION_PAGO { get; set; }
    public string COD_CLASE_OPERACION_SINCRONIZADA { get; set; } = string.Empty;
    public string COD_TIPO_OPERACION_SINCRONIZADA { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO_RELACIONADO { get; set; }
    public int? COD_TIPO_ESTADO_DETALLE_RELACIONADO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposOperacionUpdateDto
{
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_OPERACION { get; set; } = string.Empty;
    public bool? FLG_POSITIVO { get; set; }
    public bool? FLG_PROVISION { get; set; }
    public bool? FLG_CANCELACION { get; set; }
    public bool? FLG_BIEN { get; set; }
    public bool? FLG_DEVOLUCION { get; set; }
    public bool? FLG_ANULACION { get; set; }
    public bool? FLG_ANTICIPO { get; set; }
    public bool? FLG_CENTRO_COSTO { get; set; }
    public bool? FLG_PROYECTO { get; set; }
    public bool? FLG_LOTE_CONTROL { get; set; }
    public bool? FLG_CAJA { get; set; }
    public bool? FLG_BANCO { get; set; }
    public bool? FLG_EXTORNO { get; set; }
    public bool? FLG_COMERCIO_EXTERIOR { get; set; }
    public bool? FLG_ATIENDE_REQUERIMIENTO_COMPRA { get; set; }
    public bool? FLG_COMPARACION_PRECIOS_COMPRA { get; set; }
    public bool? FLG_INCIDENCIA_PRODUCTO { get; set; }
    public bool? FLG_CARGO { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO_DEFAULT { get; set; }
    [Required] public string COD_TIPO_OPERACION_SUNAT { get; set; } = string.Empty;
    [Required] public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    [Required] public string COD_TIPO_PRODUCTO_DEFECTO { get; set; } = string.Empty;
    public int? COD_TIPO_PEDIDO_COMERCIAL { get; set; }
    public int? COD_TIPO_PRECIO { get; set; }
    public int? COD_CONDICION_PAGO { get; set; }
    public string COD_CLASE_OPERACION_SINCRONIZADA { get; set; } = string.Empty;
    public string COD_TIPO_OPERACION_SINCRONIZADA { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO_RELACIONADO { get; set; }
    public int? COD_TIPO_ESTADO_DETALLE_RELACIONADO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposOperacionSearchDto
{
    public string? Search { get; set; }
}

/// <summary>Resultado de PROC_ESTANDAR_TIPOS_OPERACION_LOADCOMBOS: tipos de entidad, tipos
/// de pedido comercial, tipos de precio, condiciones de pago, clases de operación y estados
/// (filtrados por @COD_TIPO_ESTADO_CLASES_OPERACION) en un solo viaje para el diálogo
/// de Tipos de Operación.</summary>
public class TiposOperacionLoadCombosResult
{
    public int CodTipoEstadoClasesOperacion { get; set; }
    public List<KonSolutionsApi.Common.ComboboxItem> TiposEntidad        { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> TiposPedidoComercial { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> TiposPrecio          { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> CondicionesPago      { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> ClasesOperacion      { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> Estados              { get; set; } = new();
}