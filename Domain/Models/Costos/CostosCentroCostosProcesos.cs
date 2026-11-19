using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Costos;

/// <summary>Entidad COSTOS_CENTRO_COSTOS_PROCESOS - tipos equivalentes a la BD.</summary>
public class CostosCentroCostosProcesos
{
    public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    public string COD_PROCESO { get; set; } = string.Empty;
    public string COD_SUB_PROCESO { get; set; } = string.Empty;
    public decimal? VAL_COSTO_DISTRIBUIDO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_CENTRO_COSTO { get; set; } = string.Empty;
    public string DES_ALIAS { get; set; } = string.Empty;
    public string DES_EQUIPO_PRODUCCION_PRINCIPAL { get; set; } = string.Empty;
    public string DES_LINEA_PRODUCCION { get; set; } = string.Empty;
    public string DES_NOMBRE_ORDEN_PRODUCCION { get; set; } = string.Empty;
    public string DES_NOMBRE_ORDEN_PRODUCCION_ITEM { get; set; } = string.Empty;
    public string DES_NOMBRE_ORDEN_PRODUCCION_UNIDAD { get; set; } = string.Empty;
    public string DES_SUB_PROCESO { get; set; } = string.Empty;
}

public class CostosCentroCostosProcesosInsertDto
{
    [Required] public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_SUB_PROCESO { get; set; } = string.Empty;
    public decimal? VAL_COSTO_DISTRIBUIDO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CostosCentroCostosProcesosUpdateDto
{
    [Required] public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_SUB_PROCESO { get; set; } = string.Empty;
    public decimal? VAL_COSTO_DISTRIBUIDO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CostosCentroCostosProcesosSearchDto
{
    public string? Search { get; set; }
}