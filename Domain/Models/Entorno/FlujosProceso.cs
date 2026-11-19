using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Entorno;

/// <summary>Entidad FLUJOS_PROCESO - tipos equivalentes a la BD.</summary>
public class FlujosProceso
{
    public int? NUM_FLUJO_PROCESO { get; set; }
    public string DES_FLUJO_PROCESO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_DEFINIDORA { get; set; }
    public string COD_TIPO_FLUJO_PROCESO { get; set; } = string.Empty;
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    public string DES_OBSERVACIONES { get; set; } = string.Empty;
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
    public string DES_TIPO_FLUJO_PROCESO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class FlujosProcesoInsertDto
{
    public int? NUM_FLUJO_PROCESO { get; set; }
    [Required] public string DES_FLUJO_PROCESO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_DEFINIDORA { get; set; }
    [Required] public string COD_TIPO_FLUJO_PROCESO { get; set; } = string.Empty;
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    [Required] public string DES_OBSERVACIONES { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class FlujosProcesoUpdateDto
{
    public int? NUM_FLUJO_PROCESO { get; set; }
    [Required] public string DES_FLUJO_PROCESO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_DEFINIDORA { get; set; }
    [Required] public string COD_TIPO_FLUJO_PROCESO { get; set; } = string.Empty;
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    [Required] public string DES_OBSERVACIONES { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class FlujosProcesoSearchDto
{
    public string? Search { get; set; }
}