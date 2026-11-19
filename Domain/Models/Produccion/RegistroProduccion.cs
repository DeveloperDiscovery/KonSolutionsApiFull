using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Produccion;

/// <summary>Entidad REGISTRO_PRODUCCION - tipos equivalentes a la BD.</summary>
public class RegistroProduccion
{
    public int? NUM_REGISTRO_PRODUCCION { get; set; }
    public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
    public string COD_PROCESO { get; set; } = string.Empty;
    public string COD_SUB_PROCESO { get; set; } = string.Empty;
    public int? COD_RUTA_PRODUCCION { get; set; }
    public int? NUM_SECUENCIA_RUTA_PRODUCCION { get; set; }
    public string COD_LINEA_PRODUCCION { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_REGISTRO { get; set; }
    public string COD_CLASE_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public string COD_TIPO_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public string COD_CATEGORIA_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public int? NUM_CONTENEDOR_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public int? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? CAN_PRODUCIDA { get; set; }
    public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCIDA_SECUNDARIA { get; set; }
    public string COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; } = string.Empty;
    public int? NUM_REGISTRO_PRODUCCION_ASOCIADO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CATEGORIA_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public string DES_CLASE_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_LINEA_PRODUCCION { get; set; } = string.Empty;
    public string DES_RUTA_PRODUCCION { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_TIPO_MOVIMIENTO { get; set; } = string.Empty;
    public string DES_TIPO_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class RegistroProduccionInsertDto
{
    public int? NUM_REGISTRO_PRODUCCION { get; set; }
    [Required] public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_SUB_PROCESO { get; set; } = string.Empty;
    public int? COD_RUTA_PRODUCCION { get; set; }
    public int? NUM_SECUENCIA_RUTA_PRODUCCION { get; set; }
    [Required] public string COD_LINEA_PRODUCCION { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_REGISTRO { get; set; }
    [Required] public string COD_CLASE_OPERACION_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_CATEGORIA_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public int? NUM_CONTENEDOR_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public int? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? CAN_PRODUCIDA { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCIDA_SECUNDARIA { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; } = string.Empty;
    public int? NUM_REGISTRO_PRODUCCION_ASOCIADO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class RegistroProduccionUpdateDto
{
    public int? NUM_REGISTRO_PRODUCCION { get; set; }
    [Required] public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_SUB_PROCESO { get; set; } = string.Empty;
    public int? COD_RUTA_PRODUCCION { get; set; }
    public int? NUM_SECUENCIA_RUTA_PRODUCCION { get; set; }
    [Required] public string COD_LINEA_PRODUCCION { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_REGISTRO { get; set; }
    [Required] public string COD_CLASE_OPERACION_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_CATEGORIA_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public int? NUM_CONTENEDOR_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public int? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? CAN_PRODUCIDA { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCIDA_SECUNDARIA { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; } = string.Empty;
    public int? NUM_REGISTRO_PRODUCCION_ASOCIADO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class RegistroProduccionSearchDto
{
    public string? Search { get; set; }
}