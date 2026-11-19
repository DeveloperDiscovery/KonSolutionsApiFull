using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad PERSONAL_ASISTENCIA - tipos equivalentes a la BD.</summary>
public class PersonalAsistencia
{
    public int? NUM_PLANILLA_REMUNERATIVA { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    public string COD_UNIDAD_MEDIDA_TIEMPO { get; set; } = string.Empty;
    public int? CAN_TIEMPO { get; set; }
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
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_PLANILLA_REMUNERATIVA { get; set; } = string.Empty;
    public string DES_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
    public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
}

public class PersonalAsistenciaInsertDto
{
    public int? NUM_PLANILLA_REMUNERATIVA { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA_TIEMPO { get; set; } = string.Empty;
    public int? CAN_TIEMPO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalAsistenciaUpdateDto
{
    public int? NUM_PLANILLA_REMUNERATIVA { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA_TIEMPO { get; set; } = string.Empty;
    public int? CAN_TIEMPO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalAsistenciaSearchDto
{
    public string? Search { get; set; }
}