using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Produccion;

/// <summary>Entidad PROCESOS - tipos equivalentes a la BD.</summary>
public class Procesos
{
    public string COD_PROCESO { get; set; } = string.Empty;
    public string DES_PROCESO { get; set; } = string.Empty;
    public string DES_ALIAS { get; set; } = string.Empty;
    public string COD_CLASE_PROCESO { get; set; } = string.Empty;
    public bool? FLG_ORDEN_FABRICACION { get; set; }
    public string DES_NOMBRE_ORDEN_PRODUCCION { get; set; } = string.Empty;
    public string DES_NOMBRE_ORDEN_PRODUCCION_ITEM { get; set; } = string.Empty;
    public string DES_NOMBRE_ORDEN_PRODUCCION_UNIDAD { get; set; } = string.Empty;
    public string DES_EQUIPO_PRODUCCION_PRINCIPAL { get; set; } = string.Empty;
    public string DES_LINEA_PRODUCCION { get; set; } = string.Empty;
    public bool? FLG_EMBALAJE_FINAL { get; set; }
    public bool? FLG_CONTROL_CALIDAD { get; set; }
    public bool? FLG_PROCESO_MANTENIMIENTO { get; set; }
    public string COD_TIPO_MERMA { get; set; } = string.Empty;
    public int? VAL_ULTIMO_CORRELATIVO { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CLASE_PROCESO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_TIPO_MERMA { get; set; } = string.Empty;
}

public class ProcesosInsertDto
{
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    [Required] public string DES_PROCESO { get; set; } = string.Empty;
    public string? DES_ALIAS { get; set; }
    public string? COD_CLASE_PROCESO { get; set; }
    public bool? FLG_ORDEN_FABRICACION { get; set; }
    public string? DES_NOMBRE_ORDEN_PRODUCCION { get; set; }
    public string? DES_NOMBRE_ORDEN_PRODUCCION_ITEM { get; set; }
    public string? DES_NOMBRE_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public string? DES_EQUIPO_PRODUCCION_PRINCIPAL { get; set; }
    public string? DES_LINEA_PRODUCCION { get; set; }
    public bool? FLG_EMBALAJE_FINAL { get; set; }
    public bool? FLG_CONTROL_CALIDAD { get; set; }
    public bool? FLG_PROCESO_MANTENIMIENTO { get; set; }
    public string? COD_TIPO_MERMA { get; set; }
    public int? VAL_ULTIMO_CORRELATIVO { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProcesosUpdateDto
{
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    [Required] public string DES_PROCESO { get; set; } = string.Empty;
    public string? DES_ALIAS { get; set; }
    public string? COD_CLASE_PROCESO { get; set; }
    public bool? FLG_ORDEN_FABRICACION { get; set; }
    public string? DES_NOMBRE_ORDEN_PRODUCCION { get; set; }
    public string? DES_NOMBRE_ORDEN_PRODUCCION_ITEM { get; set; }
    public string? DES_NOMBRE_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public string? DES_EQUIPO_PRODUCCION_PRINCIPAL { get; set; }
    public string? DES_LINEA_PRODUCCION { get; set; }
    public bool? FLG_EMBALAJE_FINAL { get; set; }
    public bool? FLG_CONTROL_CALIDAD { get; set; }
    public bool? FLG_PROCESO_MANTENIMIENTO { get; set; }
    public string? COD_TIPO_MERMA { get; set; }
    public int? VAL_ULTIMO_CORRELATIVO { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProcesosSearchDto
{
    public string? Search { get; set; }
}

/// <summary>Resultado de PROC_PRODUCCION_PROCESOS_LOADCOMBOS: clases de proceso y tipos de
/// merma (los 2 combos independientes de ProcesosDialog.razor) en un solo viaje.</summary>
public class ProcesosLoadCombosResult
{
    public List<KonSolutionsApi.Common.ComboboxItem> ClasesProceso { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> TiposMerma    { get; set; } = new();
}