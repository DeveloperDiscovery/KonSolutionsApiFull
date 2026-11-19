using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Controlcalidad;

/// <summary>Entidad CONTROL_CALIDAD_PRODUCCION_ESPECIFICACIONES - tipos equivalentes a la BD.</summary>
public class ControlCalidadProduccionEspecificaciones
{
    public int? NUM_CONTROL_CALIDAD { get; set; }
    public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? COD_ITEM_TECNICO { get; set; }
    public string VAL_ESPACIFICACION_TENICA { get; set; } = string.Empty;
    public string COD_CLASE_DEFECTO_TECNICO { get; set; } = string.Empty;
    public string COD_TIPO_DEFECTO_TECNICO { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CONTROL_CALIDAD { get; set; } = string.Empty;
    public string DES_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public string DES_PREFIJO { get; set; } = string.Empty;
    public string DES_SINGULAR { get; set; } = string.Empty;
    public string DES_SUFIJO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_DEFECTO_TECNICO { get; set; } = string.Empty;
}

public class ControlCalidadProduccionEspecificacionesInsertDto
{
    public int? NUM_CONTROL_CALIDAD { get; set; }
    [Required] public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? COD_ITEM_TECNICO { get; set; }
    [Required] public string VAL_ESPACIFICACION_TENICA { get; set; } = string.Empty;
    [Required] public string COD_CLASE_DEFECTO_TECNICO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_DEFECTO_TECNICO { get; set; } = string.Empty;
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ControlCalidadProduccionEspecificacionesUpdateDto
{
    public int? NUM_CONTROL_CALIDAD { get; set; }
    [Required] public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? COD_ITEM_TECNICO { get; set; }
    [Required] public string VAL_ESPACIFICACION_TENICA { get; set; } = string.Empty;
    [Required] public string COD_CLASE_DEFECTO_TECNICO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_DEFECTO_TECNICO { get; set; } = string.Empty;
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ControlCalidadProduccionEspecificacionesSearchDto
{
    public string? Search { get; set; }
}