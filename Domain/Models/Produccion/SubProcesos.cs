using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Produccion;

/// <summary>Entidad SUB_PROCESOS - tipos equivalentes a la BD.</summary>
public class SubProcesos
{
    public string COD_PROCESO { get; set; } = string.Empty;
    public string COD_SUB_PROCESO { get; set; } = string.Empty;
    public string DES_SUB_PROCESO { get; set; } = string.Empty;
    public string DES_ALIAS { get; set; } = string.Empty;
    public bool? FLG_ORDEN_FABRICACION { get; set; }
    public string DES_NOMBRE_ORDEN_PRODUCCION { get; set; } = string.Empty;
    public string DES_NOMBRE_ORDEN_PRODUCCION_ITEM { get; set; } = string.Empty;
    public string DES_NOMBRE_ORDEN_PRODUCCION_UNIDAD { get; set; } = string.Empty;
    public string DES_EQUIPO_PRODUCCION_PRINCIPAL { get; set; } = string.Empty;
    public string DES_LINEA_PRODUCCION { get; set; } = string.Empty;
    public bool? FLG_EMBALAJE_FINAL { get; set; }
    public bool? FLG_CONTROL_CALIDAD { get; set; }
    public string COD_TIPO_MERMA { get; set; } = string.Empty;
    public int? VAL_ULTIMO_CORRELATIVO { get; set; }
    public int? VAL_ULTIMO_CORRELATIVO_CONTENEDOR { get; set; }
    public byte[]? IMG_ICONO { get; set; }
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
    public string DES_PROCESO { get; set; } = string.Empty;
    public string DES_TIPO_MERMA { get; set; } = string.Empty;
}

public class SubProcesosInsertDto
{
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_SUB_PROCESO { get; set; } = string.Empty;
    [Required] public string DES_SUB_PROCESO { get; set; } = string.Empty;
    public string? DES_ALIAS { get; set; }
    public bool? FLG_ORDEN_FABRICACION { get; set; }
    public string? DES_NOMBRE_ORDEN_PRODUCCION { get; set; }
    public string? DES_NOMBRE_ORDEN_PRODUCCION_ITEM { get; set; }
    public string? DES_NOMBRE_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public string? DES_EQUIPO_PRODUCCION_PRINCIPAL { get; set; }
    public string? DES_LINEA_PRODUCCION { get; set; }
    public bool? FLG_EMBALAJE_FINAL { get; set; }
    public bool? FLG_CONTROL_CALIDAD { get; set; }
    public string? COD_TIPO_MERMA { get; set; }
    public int? VAL_ULTIMO_CORRELATIVO { get; set; }
    public int? VAL_ULTIMO_CORRELATIVO_CONTENEDOR { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SubProcesosUpdateDto
{
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_SUB_PROCESO { get; set; } = string.Empty;
    [Required] public string DES_SUB_PROCESO { get; set; } = string.Empty;
    public string? DES_ALIAS { get; set; }
    public bool? FLG_ORDEN_FABRICACION { get; set; }
    public string? DES_NOMBRE_ORDEN_PRODUCCION { get; set; }
    public string? DES_NOMBRE_ORDEN_PRODUCCION_ITEM { get; set; }
    public string? DES_NOMBRE_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public string? DES_EQUIPO_PRODUCCION_PRINCIPAL { get; set; }
    public string? DES_LINEA_PRODUCCION { get; set; }
    public bool? FLG_EMBALAJE_FINAL { get; set; }
    public bool? FLG_CONTROL_CALIDAD { get; set; }
    public string? COD_TIPO_MERMA { get; set; }
    public int? VAL_ULTIMO_CORRELATIVO { get; set; }
    public int? VAL_ULTIMO_CORRELATIVO_CONTENEDOR { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SubProcesosSearchDto
{
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    public string? Search { get; set; }
}