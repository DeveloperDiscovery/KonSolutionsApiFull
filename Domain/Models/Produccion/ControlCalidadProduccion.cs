using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Produccion;

/// <summary>Entidad CONTROL_CALIDAD_PRODUCCION - tipos equivalentes a la BD.</summary>
public class ControlCalidadProduccion
{
    public int? NUM_CONTROL_CALIDAD { get; set; }
    public string DES_CONTROL_CALIDAD { get; set; } = string.Empty;
    public string COD_PROCESO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public int? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public decimal? COD_ENTIDAD_EVALUADOR { get; set; }
    public decimal? COD_ENTIDAD_APROBADOR { get; set; }
    public int? NUM_APROBACION { get; set; }
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
    public decimal? COD_ENTIDAD { get; set; }
}

public class ControlCalidadProduccionInsertDto
{
    public int? NUM_CONTROL_CALIDAD { get; set; }
    [Required] public string DES_CONTROL_CALIDAD { get; set; } = string.Empty;
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public int? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public decimal? COD_ENTIDAD_EVALUADOR { get; set; }
    public decimal? COD_ENTIDAD_APROBADOR { get; set; }
    public int? NUM_APROBACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ControlCalidadProduccionUpdateDto
{
    public int? NUM_CONTROL_CALIDAD { get; set; }
    [Required] public string DES_CONTROL_CALIDAD { get; set; } = string.Empty;
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public int? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public decimal? COD_ENTIDAD_EVALUADOR { get; set; }
    public decimal? COD_ENTIDAD_APROBADOR { get; set; }
    public int? NUM_APROBACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ControlCalidadProduccionSearchDto
{
    public string? Search { get; set; }
}