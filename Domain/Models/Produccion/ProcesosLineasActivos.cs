using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Produccion;

/// <summary>Entidad PROCESOS_LINEAS_ACTIVOS - tipos equivalentes a la BD.</summary>
public class ProcesosLineasActivos
{
    public string COD_PROCESO { get; set; } = string.Empty;
    public string COD_ACTIVO { get; set; } = string.Empty;
    public string COD_LINEA_PRODUCCION { get; set; } = string.Empty;
    public int? NUM_ORDEN_LINEA { get; set; }
    public string COD_UNIDAD_MEDIDA_CAPACIDAD_PRODUCCION { get; set; } = string.Empty;
    public string COD_UNIDAD_MEDIDA_TIEMPO_ESTANDAR_PRODUCCION { get; set; } = string.Empty;
    public decimal? CAN_CAPACIDAD_PRODUCCION { get; set; }
    public decimal? TAS_EFICIENCIA_ESTIMADA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_ACTIVO { get; set; } = string.Empty;
    public string DES_MARCA { get; set; } = string.Empty;
    public string DES_MODELO { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_SERIE { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_ALIAS { get; set; } = string.Empty;
    public string DES_EQUIPO_PRODUCCION_PRINCIPAL { get; set; } = string.Empty;
    public string DES_LINEA_PRODUCCION { get; set; } = string.Empty;
    public string DES_NOMBRE_ORDEN_PRODUCCION { get; set; } = string.Empty;
    public string DES_NOMBRE_ORDEN_PRODUCCION_ITEM { get; set; } = string.Empty;
    public string DES_NOMBRE_ORDEN_PRODUCCION_UNIDAD { get; set; } = string.Empty;
    public string DES_PROCESO { get; set; } = string.Empty;
}

public class ProcesosLineasActivosInsertDto
{
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_ACTIVO { get; set; } = string.Empty;
    [Required] public string COD_LINEA_PRODUCCION { get; set; } = string.Empty;
    public int? NUM_ORDEN_LINEA { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA_CAPACIDAD_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA_TIEMPO_ESTANDAR_PRODUCCION { get; set; } = string.Empty;
    public decimal? CAN_CAPACIDAD_PRODUCCION { get; set; }
    public decimal? TAS_EFICIENCIA_ESTIMADA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProcesosLineasActivosUpdateDto
{
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_ACTIVO { get; set; } = string.Empty;
    [Required] public string COD_LINEA_PRODUCCION { get; set; } = string.Empty;
    public int? NUM_ORDEN_LINEA { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA_CAPACIDAD_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA_TIEMPO_ESTANDAR_PRODUCCION { get; set; } = string.Empty;
    public decimal? CAN_CAPACIDAD_PRODUCCION { get; set; }
    public decimal? TAS_EFICIENCIA_ESTIMADA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProcesosLineasActivosSearchDto
{
    public string? Search { get; set; }
}