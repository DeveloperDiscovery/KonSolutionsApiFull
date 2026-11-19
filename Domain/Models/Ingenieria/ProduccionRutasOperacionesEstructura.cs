using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Entidad PRODUCCION_RUTAS_OPERACIONES_ESTRUCTURA - tipos equivalentes a la BD.</summary>
public class ProduccionRutasOperacionesEstructura
{
    public int? COD_RUTA_PRODUCCION { get; set; }
    public int? NUM_ESTRUCTURA_RUTA_PROCESO { get; set; }
    public string COD_ESTRUCTURA_RUTA_PROCESO { get; set; } = string.Empty;
    public bool? FLG_PROCESO { get; set; }
    public bool? FLG_SUB_PROCESO { get; set; }
    public bool? FLG_CLASE_OPERACION_PRODUCCION { get; set; }
    public bool? FLG_TIPO_OPERACION_PRODUCCION { get; set; }
    public string COD_PROCESO { get; set; } = string.Empty;
    public string COD_SUB_PROCESO { get; set; } = string.Empty;
    public string COD_TIPO_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public string COD_TIPO_LINEA_PRODUCCION { get; set; } = string.Empty;
    public string COD_UNIDAD_MEDIDA_TIEMPO { get; set; } = string.Empty;
    public decimal? CAN_LEAD_TIME { get; set; }
    public bool? FLG_MANUAL { get; set; }
    public bool? FLG_CONTROL_CALIDAD { get; set; }
    public bool? FLG_REGISTRA_INGRESO { get; set; }
    public bool? FLG_REGISTRA_SALIDA { get; set; }
    public bool? FLG_PRINCIPAL { get; set; }
    public bool? FLG_CONTENEDOR_PRODUCCION { get; set; }
    public int? NUM_ESTRUCTURA_RUTA_PROCESO_PADRE { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_RUTA_PRODUCCION { get; set; } = string.Empty;
    public string DES_ALIAS { get; set; } = string.Empty;
    public string DES_EQUIPO_PRODUCCION_PRINCIPAL { get; set; } = string.Empty;
    public string DES_LINEA_PRODUCCION { get; set; } = string.Empty;
    public string DES_NOMBRE_ORDEN_PRODUCCION { get; set; } = string.Empty;
    public string DES_NOMBRE_ORDEN_PRODUCCION_ITEM { get; set; } = string.Empty;
    public string DES_NOMBRE_ORDEN_PRODUCCION_UNIDAD { get; set; } = string.Empty;
    public string DES_SUB_PROCESO { get; set; } = string.Empty;
    public string DES_TIPO_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public string DES_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
}

public class ProduccionRutasOperacionesEstructuraInsertDto
{
    public int? COD_RUTA_PRODUCCION { get; set; }
    public int? NUM_ESTRUCTURA_RUTA_PROCESO { get; set; }
    [Required] public string COD_ESTRUCTURA_RUTA_PROCESO { get; set; } = string.Empty;
    public bool? FLG_PROCESO { get; set; }
    public bool? FLG_SUB_PROCESO { get; set; }
    public bool? FLG_CLASE_OPERACION_PRODUCCION { get; set; }
    public bool? FLG_TIPO_OPERACION_PRODUCCION { get; set; }
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_SUB_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_LINEA_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA_TIEMPO { get; set; } = string.Empty;
    public decimal? CAN_LEAD_TIME { get; set; }
    public bool? FLG_MANUAL { get; set; }
    public bool? FLG_CONTROL_CALIDAD { get; set; }
    public bool? FLG_REGISTRA_INGRESO { get; set; }
    public bool? FLG_REGISTRA_SALIDA { get; set; }
    public bool? FLG_PRINCIPAL { get; set; }
    public bool? FLG_CONTENEDOR_PRODUCCION { get; set; }
    public int? NUM_ESTRUCTURA_RUTA_PROCESO_PADRE { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProduccionRutasOperacionesEstructuraUpdateDto
{
    public int? COD_RUTA_PRODUCCION { get; set; }
    public int? NUM_ESTRUCTURA_RUTA_PROCESO { get; set; }
    [Required] public string COD_ESTRUCTURA_RUTA_PROCESO { get; set; } = string.Empty;
    public bool? FLG_PROCESO { get; set; }
    public bool? FLG_SUB_PROCESO { get; set; }
    public bool? FLG_CLASE_OPERACION_PRODUCCION { get; set; }
    public bool? FLG_TIPO_OPERACION_PRODUCCION { get; set; }
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_SUB_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_LINEA_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA_TIEMPO { get; set; } = string.Empty;
    public decimal? CAN_LEAD_TIME { get; set; }
    public bool? FLG_MANUAL { get; set; }
    public bool? FLG_CONTROL_CALIDAD { get; set; }
    public bool? FLG_REGISTRA_INGRESO { get; set; }
    public bool? FLG_REGISTRA_SALIDA { get; set; }
    public bool? FLG_PRINCIPAL { get; set; }
    public bool? FLG_CONTENEDOR_PRODUCCION { get; set; }
    public int? NUM_ESTRUCTURA_RUTA_PROCESO_PADRE { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProduccionRutasOperacionesEstructuraSearchDto
{
    public string? Search { get; set; }
}